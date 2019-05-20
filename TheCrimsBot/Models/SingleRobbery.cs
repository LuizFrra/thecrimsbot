using System.Collections.Generic;

namespace TheCrimsBot.Models
{
public class SingleRobbery
{
    public int id { get; set; }
    //public string name { get; set; }
    public int difficulty { get; set; }
    public int energy { get; set; }
    //public int spirit { get; set; }
    //public string spiritname { get; set; }
    //public int type { get; set; }
    public string translated_name { get; set; }
    //public int max_reward { get; set; }
    //public int min_reward { get; set; }
    //public List<object> rewards { get; set; }
    //public int user_power { get; set; }
    public int successprobability { get; set; }
    //public string successprobability_image { get; set; }
    //public string long_name { get; set; }

    public override string ToString()
    {
        string toString = $@"
        id = {id}
        difficulty = {difficulty}
        energy = {energy}
        successprobability = {successprobability}
        ";

        return toString;
    }
}
}