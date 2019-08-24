using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace HTTP_IP_Collector
{
    public  class GetRequest
    {
       public string  pr(bool ok, string ip, string method, string useragent, string proxy)
        {
            try
            {
                
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://" + ip);
                Console.Write("\n"+ip);
                webRequest.AllowAutoRedirect = false;
                webRequest.Method = method;
                try
                {
                    if (proxy != null)
                    {
                        string[] wordsfromprox = proxy.Split(':');
                        webRequest.Proxy = new WebProxy(wordsfromprox[0],int.Parse(wordsfromprox[1]));
                        
                      
                    }
                }
                catch { }
                try
                {
                    webRequest.UserAgent = useragent;
                }
                catch { }
                webRequest.Timeout = 5000;
                string resp;
                try
                {
                    HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
                    HttpStatusCode wRespStatusCode = response.StatusCode;
                    Console.ForegroundColor = ConsoleColor.Red;
                    resp = " Response: " + response.StatusCode.ToString() + "\n";
                    resp  = ip + " Status:" + ((int)response.StatusCode).ToString();
                    Console.ForegroundColor = ConsoleColor.White;
                    return resp;

                    //Returns "MovedPermanently", not 301 which is what I want.
                }
                catch (WebException we)
                {
                    try
                    {
                        resp = "";
                        var wRespStatusCode = ((HttpWebResponse)we.Response).StatusCode;
                        Console.ForegroundColor = ConsoleColor.Red;
                        resp = " Response: " + ((int)wRespStatusCode).ToString() + "\n";
                        resp  = ip + " Status: " + ((int)wRespStatusCode).ToString();
                        Console.ForegroundColor = ConsoleColor.White;
                        return resp;

                    }
                    catch
                    {
                        Console.Write(" Response: " + "Timeout" + "\n");

                     
                    }
                }
            
            }
            catch (Exception exc) {

                Console.WriteLine("Error: "+exc.Message);
                Console.ReadLine();
            }
            return null;
        }
    }
}
