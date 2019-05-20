using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using TheCrimsBot.Models;
using TheCrimsBot.Parser;

namespace TheCrimsBot.Requests
{
    public class TCRequests
    {
        private Uri baseAddress;

        private CookieContainer cookContainer;

        private HttpClientHandler handler;

        private HttpClient client;

        public User user { get; private set; }

        public Robberies robberies { get; private set; }

        public NightClubs nightClubs { get; private set; }
        
        public NightClubDrugs nightClubDrugs { get; private set; }

        private string html;

        public TCRequests()
        {
            baseAddress = new Uri("https://www.thecrims.com/");
            cookContainer = new CookieContainer();
            handler = new HttpClientHandler(){ CookieContainer = cookContainer };
            client = new HttpClient(handler){ BaseAddress = baseAddress };

            var homePageResult = client.GetAsync("/"); 
            homePageResult.Result.EnsureSuccessStatusCode();
            html = homePageResult.Result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        }

        public User login(string username, string password)
        {
            TCParser parser = new TCParser(html);
            string token, action;
            parser.getTokenAndAction(out token, out action);

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("_token", token),
                new KeyValuePair<string, string>("action", action),
                new KeyValuePair<string, string>("pl", ""),
                new KeyValuePair<string, string>("env", "")
            });
            
            var loginResult = client.PostAsync("/login", content);
            loginResult.Result.EnsureSuccessStatusCode();

            parser.setHtml(loginResult.Result.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            //Console.WriteLine(loginResult.Result.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            user = parser.getUserData();
            //Console.WriteLine(user.stateName);
            client.DefaultRequestHeaders.Add("x-request", user.stateName);
            //Console.WriteLine(user.ToString());

            return user;
        }

        private Robberies getRobberies()
        {
            Console.WriteLine("Pegando os assaltos disponiveis");
            var robberiesResult = client.GetAsync("/api/v1/robberies");
            robberiesResult.Result.EnsureSuccessStatusCode();
            string robberiesHtml = robberiesResult.Result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            TCParser parser = new TCParser(robberiesHtml);
            //Console.WriteLine(robberiesHtml);
            robberies = parser.getRobberiesData();
            
            //string stateName = user.stateName;
            user = robberies.user;
            //user.stateName = stateName;
    
            return robberies;
        }

        public void doRobbery(int minSuccess)
        {
            getRobberies();
            SingleRobbery robbery = robberies.getBestRobbery(minSuccess);
            int bestRobID = robbery.id;
            Console.WriteLine("Assaltando : " + robbery.translated_name);
            if(user.stamina < robbery.energy)
                buyDrug();
            
            string myJson = "{\"id\":" + bestRobID + "}";
            var response = client.PostAsync("/api/v1/rob", new StringContent(myJson, Encoding.UTF8, "application/json"));
            string robberiesHtml = response.Result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            response.Result.EnsureSuccessStatusCode();
            
            TCParser parser = new TCParser(robberiesHtml);
            robberies = parser.getRobberiesData();
        }

        private void getNightClubs()
        {
            Console.WriteLine("Pegando os clubs Disponiveis");
            var nightClubsResult = client.GetAsync("/api/v1/nightclubs");
            nightClubsResult.Result.EnsureSuccessStatusCode();
            string nightClubsHtml = nightClubsResult.Result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            TCParser parser = new TCParser(nightClubsHtml);
            
            nightClubs = parser.getNightClubsData();
        }

        private void goInNightClub()
        {
            getNightClubs();
            Console.WriteLine("Entrando em um club");
            string bestNightClubId = nightClubs.getBestNightClub(user.respect).id;
            string myJson = "{\"id\": " + "\"" + bestNightClubId + "\"" + "}";
            var response = client.PostAsync("/api/v1/nightclub", new StringContent(myJson, Encoding.UTF8, "application/json"));
            Console.WriteLine(myJson);
            response.Result.EnsureSuccessStatusCode();
            string nightClubsHtml = response.Result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            
            TCParser parser = new TCParser(nightClubsHtml);
            nightClubDrugs = parser.getNightClubDrugs();
            
            user = nightClubDrugs.user;
        }

        public void buyDrug()
        {
            goInNightClub();
            Console.WriteLine("Comprando drogas.");
            int drugId = 0;
            double quantity = 0;
            nightClubDrugs.buyBestDrug(out drugId, out quantity, user.stamina, user.cash);
            string myJson = "{\"id\":" + drugId + ", \"quantity\": \""+ (int)quantity +"\"}";
            Console.WriteLine(myJson);
            var response = client.PostAsync("/api/v1/nightclub/drug", new StringContent(myJson, Encoding.UTF8, "application/json"));

            response.Result.EnsureSuccessStatusCode();
            //Console.WriteLine(response.Result.Content.ReadAsStringAsync().GetAwaiter().GetResult());
        }
    }
}