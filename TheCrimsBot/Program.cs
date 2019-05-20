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
            //req.getNightClubs();
            //req.goInNightClub();
            //req.buyDrug();
            //req.getRobberies();
            while(true)
            {
                Random rnd = new Random();
                int first = rnd.Next(1, 3) * 1000;
                int second = rnd.Next(1, 9) * 100;
                int third = rnd.Next(1, 9) * 10;
                int fourth = rnd.Next(1, 9);
                req.doRobbery(90);
                Console.WriteLine("Assalto feito");
                System.Threading.Thread.Sleep(first + second + third + fourth);
            }
            //Console.WriteLine("Assalto feito");
            //req.doRobbery(95);
            //string html = System.IO.File.ReadAllText("tc.txt");
            //TCParser parser = new TCParser(html);
            //parser.getRobberiesData();
            //Console.WriteLine(user.ToString());
        }
    }
}
