using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace HTTP_IP_Collector
{
    class Program
    {
        static bool keepDoing = true;
        static int ip1 = 0;
        static int ip2 = 0;
        static int ip3 = 0;
        static int ip4 = 0;
        static List<string> okIPS = new List<string>();
        static string CurrentIP = "1.1.1.1";
        static int maxthreads = 32;
        static List<Thread> ThreadList = new List<Thread>();
        static bool neverending = false;
        static void Main(string[] args)
        {
            #region somethingToCheck
            p(true, "Checking Connection...SKIPPED");

            //if (CheckForInternetConnection())
            //{
            //    p(true, "[+] Connection established...", null, ConsoleColor.Green);
            //}
            //else
            //{
            //    p(true, "[x] Cannot detect internet connection...", null, ConsoleColor.Red);
            //}
            p(true, "Reading instructions...");
            p(true, "[+] OK...", null, ConsoleColor.Green);
            #endregion
            #region Main



            p(true, @"
   _______    _____     ____        __          
  /  _/ _ \  / ___/__  / / /__ ____/ /____  ____
 _/ // ___/ / /__/ _ \/ / / -_) __/ __/ _ \/ __/
/___/_/     \___/\___/_/_/\__/\__/\__/\___/_/   
", null, ConsoleColor.Cyan);
            Console.ForegroundColor = ConsoleColor.White;
            p(true, @"    [   By Alizer/PHC-Yasuo Main   ]");


            Console.ResetColor();
            p(true, "");
            p(true, "Responses will be stored at \"responses.txt\"", null, ConsoleColor.White, false);
            if (!File.Exists("responses.txt"))
            {
                var f = File.Create("responses.txt");
                f.Close();
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\nMethod type [GET/POST/DELETE/MOVE/HEAD/PATCH/OPTIONS/PROPATCH] enter none for default (GET): ");
            Console.ForegroundColor = ConsoleColor.White;
            string method = Console.ReadLine();
            if (method == "")
            {
                method = "GET";
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(@"
User-Agent:
1. None.
2. Mozilla/5.0 (Windows NT 5.1; rv:7.0.1) Gecko/20100101 Firefox/7.0.1
3. Mozilla/4.0 (compatible; MSIE 9.0; Windows NT 6.1)
Enter none for default: ");
            Console.ForegroundColor = ConsoleColor.White;
            string key = Console.ReadLine();
            string useragent = null;
            if (key == "2")
            {
                useragent = @"Mozilla/5.0 (Windows NT 5.1; rv:7.0.1) Gecko/20100101 Firefox/7.0.1";
            }
            if (key == "3")
            {
                useragent = @"Mozilla/4.0 (compatible; MSIE 9.0; Windows NT 6.1)";
            }
            else if (key == "")
            {
                useragent = @"Mozilla/4.0 (compatible; MSIE 9.0; Windows NT 6.1)";
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\nProxy [Proxy:Port] enter none for none: ");
            Console.ForegroundColor = ConsoleColor.White;
            string proxy = null;
            proxy = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(@"
Threads (enter none for default[32]): ");
            Console.ForegroundColor = ConsoleColor.White;
            string threads = "32";
            threads = Console.ReadLine();
            if (threads =="")
            {
                threads = "32";
            }


            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(@"
Loop (neverending survey) default [FALSE]: ");
            Console.ForegroundColor = ConsoleColor.White;
            string loop = Console.ReadLine();
            if (bool.TryParse(loop,out neverending))
            {
                neverending = bool.Parse(loop);
            }

            p(true, "Survey Started " + DateTime.Now, null, ConsoleColor.Cyan);
            maxthreads = int.Parse(threads);

            Console.WriteLine(@"
Method: " + method + @"
Useragent: " + useragent + @"
Proxy:Port: " + proxy + @"
Maximum Threads:" + threads + @"
Loops:" + neverending.ToString() + @"
");

           

            Thread.Sleep(1100);

            Thread watcher = new Thread(() =>
            {
                while (ThreadList.Count >= 1)
                {
                  
                }
                p(true, "All threads exited.", null, ConsoleColor.Magenta);
         
            });
            
            while (maxthreads >= 1)
            {
                Thread mThread = new Thread(() => {
                    Survey(method, useragent, proxy);
                
                });
                ThreadList.Add(mThread);
                mThread.Start();
                maxthreads -= 1;
            }
            watcher.Start();
            p(true,"\nAll threads deployed..."+DateTime.Now,null,ConsoleColor.Cyan);
            if (neverending)
            {
                p(true, "\nThreads stays active..." + DateTime.Now, null, ConsoleColor.Yellow);
            }
            Console.ReadKey();

            #endregion
        }
        public static void Survey(string method, string useragent, string proxy)
        {
            while (keepDoing)
            {
                #region newIPs
                bool ip1ok = false;
                bool ip2ok = false;
                bool ip3ok = false;
                bool ip4ok = false;
                int i1;
                int i2;
                int i3;
                int i4;
                Random rnd = new Random(DateTime.Now.Millisecond);
             
                    i1 = rnd.Next(1, 255);

                 
                        ip1 = i1;
                  
                        ip1ok = true;
                    

               

                    i2 = rnd.Next(1, 255);
                  
                        ip2 = i2;
                      
                        ip2ok = true;

                    


                    i3 = rnd.Next(1, 255);
                   
                        ip3 = i3;
                     
                        ip3ok = true;


               

                    i4 = rnd.Next(1, 255);
                 
                        ip4 = i4;
                string madeIP = ip1.ToString() + '.' + ip2.ToString() + '.' + ip3.ToString() + '.' + ip4.ToString();
                if (!neverending)
                {
                    if (okIPS.Contains(madeIP))
                    {
                        return;
                    }
                    okIPS.Add(madeIP);
                }
                #endregion
                GetRequest getR = new GetRequest();
                string responsess = getR.pr(true, madeIP, method, useragent, proxy);

                if (responsess != null)
                {
                    if (!File.ReadAllLines("responses.txt").Contains(responsess))
                    {
                        try
                        {
                            File.AppendAllText("responses.txt", responsess + "\n");
                        }
                        catch
                        {
                            if (!File.Exists("responses_overflow.txt"))
                            {
                                File.Create("responses_overflow.txt").Close();
                            }
                            File.AppendAllText("responses_overflow.txt", responsess + "\n");
                        }
                    }
                  
               //     File.WriteAllText("responses.txt", File.ReadAllText("responses.txt").Replace("\n",""));
                }

            }
        }
        static void p(bool english, string print_EN, string print_TAGALOG = null, ConsoleColor color = ConsoleColor.White, bool writeline = true)
        {
            if (english)
            {
                Console.ForegroundColor = color;
                if (writeline)
                    Console.WriteLine(print_EN);
                else
                    Console.Write(print_EN);

            }
            else
            {
                Console.WriteLine(print_TAGALOG);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
      
        static void pause()
        {
            Console.ReadLine();
        }
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
              
                using (client.OpenRead("http://clients3.google.com/"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
