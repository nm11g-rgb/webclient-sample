using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace webclientsample
{
    partial class Mango
    {
        public static void Core(KeyValuePair<string, Tuple<DateTime, string, string, string, string, string>> kvp)
        {
            WebClient client = new WebClient();
            Random randNum = new Random();
            string agent = "Mozilla/5.0";
            //foreach (var item in kvp)
            //{
                client.Headers[HttpRequestHeader.UserAgent] = agent;
                client.Headers[HttpRequestHeader.Referer] = kvp.Value.Item4;
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string info = kvp.Value.Item3;


                try
                {
                    string data = client.UploadString(kvp.Value.Item2, info);
                    //Console.WriteLine("[" + DateTime.Now.ToShortTimeString() + "] " + data);
                    //Console.WriteLine(info);

                    if (data.Contains(kvp.Value.Item5))
                    {
                        Console.WriteLine("[" + DateTime.Now.ToString("HH:mm:ss") + "][" + kvp.Key + "] " + kvp.Value.Item6);
                    }
                    else
                    {
                        Console.WriteLine("[" + DateTime.Now.ToString("HH:mm:ss") + "][" + kvp.Key + "] Error");
                    }

                }
                catch (WebException e)
                {
                    Recover(e.Message, randNum);
                }
            //}
            client.Dispose();
        }

        public static void Sleep()
        {
            Random randNum = new Random();
            if (DateTime.Now.Hour == 1 && DateTime.Now.Minute >= 20)
            {
                int longsleep = new Random().Next(4, 6);
                TimeSpan interval = new TimeSpan(longsleep, randNum.Next(0, 20), randNum.Next(0, 60));
                Console.WriteLine("Sleeping until " + DateTime.Now.Add(interval).ToShortTimeString());
                Thread.Sleep(interval);
            }
            else
            {
                //int sleep = randNum.Next(943000, 1500000);
                int sleep = randNum.Next(120000, 450000);
                int sleep2 = sleep;
                Console.WriteLine("-Sleeping for " + Math.Round(TimeSpan.FromMilliseconds(sleep2).TotalMinutes, 2) + " minutes-");
                Thread.Sleep(sleep2);
            }

        }

        public static void cleanExit()
        {
            Console.WriteLine("Press any key to exit...");
            Console.Read();
            Environment.Exit(0);
        }

        public static void Recover(string info, Random randNum)
        {
            Console.WriteLine("[" + DateTime.Now.ToShortTimeString() + "] Error: " + info);
            Console.WriteLine("[" + DateTime.Now.ToShortTimeString() + "] Restarting...");
            int sleep = randNum.Next(15000, 45777);
            int sleepS = sleep;
            Console.WriteLine("-Sleeping for " + Math.Round(TimeSpan.FromMilliseconds(sleepS).TotalSeconds, 2) + " seconds (" + Math.Round(TimeSpan.FromMilliseconds(sleepS).TotalMinutes, 2) + "m)-");
            Thread.Sleep(sleepS);
        }
    }
}
