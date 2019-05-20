using System.Collections.Generic;
using System.Linq;

namespace TheCrimsBot.Models
{
    public class NightClubDrugs
    {
        public User user { get; set; }

        public Nightclub nightclub { get; set; }


        public void buyBestDrug(out int id, out double quantity, int myStamina, int myMoney)
        {
            id = 0;
            quantity = 0;

            var drug =nightclub.prices_with_drugs.OrderByDescending(x => (((100 - myStamina)/x.drug.stamina) * x.price))
                    .Last((x => (((100 - myStamina)/x.drug.stamina) * x.price) < myMoney));

            id = drug.drug_id;
            quantity = (100 - myStamina)/drug.drug.stamina;
        }
    }
}