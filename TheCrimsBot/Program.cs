using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using TheCrimsBot.Requests;
using TheCrimsBot.Parser;
using TheCrimsBot.Models;

namespace TheCrimsBot
{
    class Program
    {
        static void Main(string[] args)
        {

            TCRequests req = new TCRequests();
            req.login("SrPoe", "b9bb8b27fbb3");
            req.getRobberies();
            req.doRobbery(95);
            Console.WriteLine("Assalto feito");
            req.doRobbery(95);
            //string html = System.IO.File.ReadAllText("tc.txt");
            //TCParser parser = new TCParser(html);
            //parser.getRobberiesData();
            //Console.WriteLine(user.ToString());
        }
    }
}
