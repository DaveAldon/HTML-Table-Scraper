using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace FacultyApp
{
    class Run
    {
        public string name;
        public bool start()
        {
            bool didSucceed = false;
            List<string> dayList = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };

            List<string> days = new List<string>();
            int index = 0;
            string time = "";
            string value = "";

            Scrape scraper = new Scrape();
            List<HtmlNode> x = scraper.BeginScrape();

            bool first = true;

            foreach (HtmlNode item in x)
            {
                if (first)
                {
                    first = false;
                }

                List<HtmlNode> cell = item.Elements("td").ToList();
                foreach (HtmlNode events in cell)
                {
                    value = events.InnerText;
                    if (index == 0)
                    {
                        index = index + 1;
                        time = value;
                        continue;
                    }

                    days.Add(dayList[index - 1] + "$" + time + "$" + value);
                    index = index + 1;
                }
                index = 0;
            }
            
            GiveData giveDB = new GiveData();
            giveDB.name = name;
            didSucceed = giveDB.DBGather(days);
            return didSucceed;
        }
    }
}
