using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace webclientsample
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            Console.Title = assemblyName + " v0.1 | Total requests: " + i;
            Console.WriteLine(assemblyName + " v0.1 - Built on 04/10/2020");
            Console.WriteLine("Today is " + DateTime.Now.ToShortDateString() + Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.Red;

            var reqInfo = new Dictionary<string, Tuple<DateTime, string, string, string, string, string>>();

            #region first
            var request = new Tuple<DateTime, string, string, string, string, string>
                (new DateTime(2014, 12, 2),
                "https://github.com/nm11g-rgb",
                "POSTData",
                "https://nevinmanimala.com",
                "contains",
                "Response");
            reqInfo.Add("example", request); 
            #endregion

            while (true)
            {
                //DateTime now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                Console.WriteLine("\n[" + DateTime.Now.ToString("HH:mm:ss") + "] Submitting data...");

                foreach (KeyValuePair<string, Tuple<DateTime, string, string, string, string, string>> kvp in reqInfo)
                {
                    //Console.WriteLine("Key = {0}, Value = {1}",
                    //    kvp.Key, kvp.Value);
                    if (kvp.Value.Item1 >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
                    {
                        Mango.Core(kvp);
                        i++;


                        if (DateTime.Now.Hour == new Random().Next(7,9) && DateTime.Now.Minute >= 20)
                        {
                            Thread.Sleep(new Random().Next(700000, 1800000));
                            Match auth_token = null;


                            foreach (KeyValuePair<string, Tuple<DateTime, string, string, string, string, string>> kvp2 in Daily)
                            {
                                if (kvp2.Value.Item1 >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
                                {
                                    Thread.Sleep(new Random().Next(70000, 250000));
                                    Mango.Core(kvp2);
                                    i++;
                                }
                                else { Console.WriteLine("[" + DateTime.Now.ToString("HH:mm:ss") + "][" + kvp2.Key + "] Skip"); }
                            }

                            Console.WriteLine("\n[" + DateTime.Now.ToString("HH:mm:ss") + "] n=" + Daily.Count);
                            Thread.Sleep(900000);
                        }
                    }
                    else { Console.WriteLine("[" + DateTime.Now.ToString("HH:mm:ss") + "] Skip"); }
                }

                Console.Title = assemblyName + " v0.1 | Total requests: " + i;
                Mango.Sleep();
            }
        }
    }
}
