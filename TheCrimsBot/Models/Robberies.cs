using System.Collections.Generic;
using System.Linq;
namespace TheCrimsBot.Models
{
    public class Robberies
    {
        public List<SingleRobbery> single_robberies { get; set; }

        public User user { get; set;}
    
        public SingleRobbery getBestRobbery(int minSuccess)
        {
            return single_robberies.OrderByDescending(id => id.id).First(x => x.successprobability >= minSuccess);
            //SingleRobbery robbery = single_robberies.
        }
    } 
}