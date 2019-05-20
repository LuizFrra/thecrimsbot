using System;
using System.Collections.Generic;

namespace TheCrimsBot.Models
{
    public class User
    {
        public string id { get; set; }
        
        public string eid { get; set; }

        public string push_id { get; set; }

        public string username { get; set; }

        public int respect { get; set; }

        public int addiction { get; set; }

        public UserAttributes attributes { get; set; }

        public string stateName {get; set; }

        public User(Dictionary<string, string> jsonData)
        {
            id = jsonData["id"];
            //eid = jsonData["eid"];
            push_id = jsonData["push_id"];
            username = jsonData["username"];
            respect = int.Parse(jsonData["respect"]);
            addiction = int.Parse(jsonData["addiction"]);
            
            attributes = new UserAttributes(jsonData);
        }

        public override string ToString()
        {
            string toString = $@"
            id = {id}
            eid = {eid}
            push_id = {push_id}
            userName = {username}
            respect = {respect}
            addiction = {addiction}
            stateName = {stateName}" + attributes.ToString();
            
            return toString;
        }
    }
}