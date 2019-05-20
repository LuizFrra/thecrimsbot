using System;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Collections.Generic;
using TheCrimsBot.Models;

namespace TheCrimsBot.Parser
{
    public class TCParser
    {
        private string html;

        public TCParser(string html) => this.html = html;

        public string getHtml() => html;
        
        public void setHtml(string html) => this.html = html; 

        public void getTokenAndAction(out string token, out string action)
        {
            HtmlDocument document = new HtmlDocument();
            document.OptionFixNestedTags = true;
            document.LoadHtml(html);

            token = document.DocumentNode.SelectSingleNode("//input[@name='_token']").GetAttributeValue("value", "");
            action = document.DocumentNode.SelectSingleNode("//div//form[@class='form-signin']//input[@name='action']")
                                                                .GetAttributeValue("value", "");
        }

        public User getUserData()
        {
            HtmlDocument document = new HtmlDocument();
            document.OptionFixNestedTags = true;
            document.LoadHtml(html);
        
            string jsDataHtml = document.DocumentNode.SelectSingleNode("//script[contains(., 'window.userState')]").InnerHtml;

            Regex rx = new Regex(@"""\w+"":(""\w+""|(\d+))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(jsDataHtml);
            
            string stateName = Regex.Match(jsDataHtml, @"'(?:\d+[a-z]|[a-z]+\d)[a-z\d]*'").Value;
            stateName = stateName.Replace("'", "");

            jsDataHtml = "{";
            
            foreach(Match match in matches)
                jsDataHtml += match.Value + ",";
            
            jsDataHtml = jsDataHtml.Remove(jsDataHtml.Length - 1) + "}";
            User user = JsonConvert.DeserializeObject<User>(jsDataHtml);
            
            user.stateName = stateName;

            return user;
        }

        public Robberies getRobberiesData()
        {
            return JsonConvert.DeserializeObject<Robberies>(html);   
        }

        public NightClubs getNightClubsData()
        {
            return JsonConvert.DeserializeObject<NightClubs>(html);
        }

        public NightClubDrugs getNightClubDrugs()
        {
            return JsonConvert.DeserializeObject<NightClubDrugs>(html);
        }

    }
}