using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Program1
{
    class Program
    {
        public class Crawler
        {
            private Queue<string> urls = new Queue<string>();
            private int count = 0;
            private string html;
            private string Lock = "";


            static void Main(string[] args)
            {
                Crawler myCrawler = new Crawler();

                string startUrl = "https://www.360.cn/";
                if (args.Length >= 1) startUrl = args[0];

                myCrawler.urls.Enqueue(startUrl);
                myCrawler.Crawl();

            }

            private void Crawl()
            {
                Console.WriteLine("开始爬行了。。。");
                DateTime startTime = DateTime.Now;
                while (true)
                {
                    string current = null;
                    if (urls.Count == 0) continue;

                    current = urls.Peek();

                    if (current == null || count > 50) break;
                    Console.WriteLine("爬行" + current + "页面！");

                    html = DownLoad(current);
                    urls.Dequeue();

                    count++;
                    //Parse();
                    Thread thread = new Thread(Parse);
                    thread.Start();
                 }
                
            Console.WriteLine("爬行结束！");
            DateTime endTime = DateTime.Now;
            TimeSpan runTime = endTime - startTime;
            Console.WriteLine(runTime.ToString());
            Console.WriteLine("");
            }

            private void Parse()
            {
                
                lock(Lock)
                {
                    string strRef = @"(href|HREF)[ ]*=[ ]*[""'][^""'#(img)]+[""']";
                    MatchCollection matches = new Regex(strRef).Matches(html);

                    foreach (Match match in matches)
                    {
                        strRef = match.Value.Substring(match.Value.IndexOf('=') + 1).Trim('"', '\\', '#', ' ', '>');
                        if (strRef.Length == 0) continue;
                        urls.Enqueue(strRef);
                    }
                }
               
            }

            private string DownLoad(string url)
            {
                try
                {
                    WebClient webClient = new WebClient();
                    webClient.Encoding = Encoding.UTF8;
                    string html = webClient.DownloadString(url);

                    string fileName = count.ToString();
                    File.WriteAllText(fileName, html, Encoding.UTF8);
                    return html;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return "";
                }
            }
        }
    }
}
