using System.Collections.Generic;

namespace TheCrimsBot.Models
{
    public class Nightclub
    {
        public string id { get; set; }
        //public string name { get; set; }
        public int entrance_fee { get; set; }
        public int min_respect { get; set; }
        public int level { get; set; }
        public string channel { get; set; }
        public List<PricesWithDrug> prices_with_drugs { get; set; }
        //public string description { get; set; }
        //public int type { get; set; }
        //public int business_id { get; set; }
        //public int cashowed { get; set; }
        //public string image { get; set; }//        public string owner_id { get; set; }
        //public int user_id { get; set; }
    }
}