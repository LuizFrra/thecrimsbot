using System.Collections.Generic;
using System.Linq;

namespace TheCrimsBot.Models
{ 
    public class NightClubs
    {
        public List<Nightclub> nightclubs { get; set; }


        public Nightclub getBestNightClub(int myRespect)
        {
            return nightclubs.OrderBy(p => p.entrance_fee).First(r => myRespect > r.min_respect);
        }
    }
}