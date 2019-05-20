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

        public User user { get; private set;}

        public Robberies robberies { get; private set;}

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

            client.DefaultRequestHeaders.Add("x-request", user.stateName);
            //Console.WriteLine(user.ToString());

            return user;
        }

        public Robberies getRobberies()
        {
            var robberiesResult = client.GetAsync("/api/v1/robberies");
            robberiesResult.Result.EnsureSuccessStatusCode();
            string robberiesHtml = robberiesResult.Result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            TCParser parser = new TCParser(robberiesHtml);
            
            robberies = parser.getRobberiesData();
            
            //Console.WriteLine(robberies.getBestRobbery(95).ToString());
            return robberies;
        }

        public void doRobbery(int minSuccess)
        {
            int bestRobID = robberies.getBestRobbery(minSuccess).id;
            string myJson = "{\"id\":" + bestRobID + "}";
            var response = client.PostAsync("/api/v1/rob", new StringContent(myJson, Encoding.UTF8, "application/json"));
            response.Result.EnsureSuccessStatusCode();
            string robberiesHtml = response.Result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            
            TCParser parser = new TCParser(robberiesHtml);
            robberies = parser.getRobberiesData();
        }

        public void getNightClubs()
        {
            
        }
    }
}