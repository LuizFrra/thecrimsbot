using System.Collections.Generic;

namespace TheCrimsBot.Models
{
    public class UserAttributes
    {
        public int respect { get; set; }

        public int tolerance { get; set; }

        public int strenght { get; set; }

        public int charisma { get; set; }

        public int intelligence { get; set; }

        public int stamina { get; set; }

        public int tickets { get; set; }

        public UserAttributes(Dictionary<string, string> jsonData)
        {
            respect = int.Parse(jsonData["respect"]);
            tolerance = int.Parse(jsonData["tolerance"]);
            strenght = int.Parse(jsonData["strength"]);
            charisma = int.Parse(jsonData["charisma"]);
            intelligence = int.Parse(jsonData["intelligence"]);
            stamina = int.Parse(jsonData["stamina"]);
            tickets = int.Parse(jsonData["tickets"]);
        }

        public override string ToString()
        {
            string toString = $@"
            respect = {respect}
            tolerane = {tolerance}
            strenght = {strenght}
            charisma = {charisma}
            intelligence = {intelligence}
            stamina = {stamina}
            tickets = {tickets}
            ";
            
            return toString;
        }
    }
}