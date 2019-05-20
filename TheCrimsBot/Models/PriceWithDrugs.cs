namespace TheCrimsBot.Models
{
    public class PricesWithDrug
    {
        //public int id { get; set; }
        //public int business_id { get; set; }
        public int drug_id { get; set; }
        public int price { get; set; }
        //public bool deletable { get; set; }
        public Drug drug { get; set; }
    }  
}