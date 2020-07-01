using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;

namespace Лаба4
{
    static class GetChanel
    {
        public static void Chanel(string num)
        {

            string url = $"https://tv.yandex.ru/channel/rossiya-1-31?date ={num}";//2020 - 07 - 01

            List<Show> ListShow = new List<Show>();

            var htmlDoc = new HtmlWeb();
            var doc = htmlDoc.Load(url);

            var listTitle = doc.DocumentNode.SelectNodes("//span[@class='channel-schedule__text']");
            var listTime = doc.DocumentNode.SelectNodes("//time[@class='channel-schedule__time']");
            var listLink = doc.DocumentNode.SelectNodes("//a[@class='link channel-schedule__link']");

            for (int i = 0; i < listTitle.LongCount(); i++)
            {
                ListShow.Add(new Show
                {
                    title = listTitle[i].InnerText,
                    time = listTime[i].InnerText,
                    link = listLink[i].Attributes["href"].Value
                });
            }

            using (StreamWriter outputFile = new StreamWriter("MyFiles.txt"))
            {
                foreach (var n in ListShow)
                    outputFile.WriteLine($"[{n.title} {n.time}] ({n.link})\n");
            }
        }
    }
}
