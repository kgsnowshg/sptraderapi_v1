using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;//IsoDateTimeConverter
using System.IO;
namespace spapiApp
{
    class trade
    {
        public static int writetrade(string code, string bs, string price, string qty, string cases)
        {
            DateTime dt = DateTime.Now;
            int hour = Convert.ToInt32(dt.Hour.ToString());//13
            int min = Convert.ToInt32(dt.Minute.ToString());//13
            int sec = Convert.ToInt32(dt.Second.ToString());//13
            int nowsec = Convert.ToInt32(hour) * 60 * 60 + Convert.ToInt32(min) * 60 + Convert.ToInt32(sec);

            string jsonStrings = File.ReadAllText("real/trade.json", Encoding.Default);
            JObject abc = JObject.Parse(jsonStrings);
            JToken totalsectoken = abc["user"][0]["totalsec"];
            int totalsec = Convert.ToInt32(totalsectoken);
            if (nowsec>totalsec+10)
            {
                string update = File.ReadAllText("real/trade.json", Encoding.Default);
                JObject updates = JObject.Parse(update);
                updates["user"][0]["action"] = 1;
                updates["user"][0]["code"] = code;
                updates["user"][0]["bs"] = bs;
                updates["user"][0]["price"] = price;
                updates["user"][0]["qty"] = qty;
                updates["user"][0]["cases"] = cases;
                updates["user"][0]["totalsec"] = nowsec;
                string updatestring = Convert.ToString(updates);//将json装换为string
                File.WriteAllText("real/trade.json", updatestring);//将内容写进jon文件中
            }
            return 0;
        }

        public static int tradeoperate()
        {
            DateTime dt = DateTime.Now;
            int hour = Convert.ToInt32(dt.Hour.ToString());//13
            int min = Convert.ToInt32(dt.Minute.ToString());//13
            int sec = Convert.ToInt32(dt.Second.ToString());//13
            int nowsec = Convert.ToInt32(hour) * 60 * 60 + Convert.ToInt32(min) * 60 + Convert.ToInt32(sec);
            string jsonStrings = File.ReadAllText("real/trade.json", Encoding.Default);
            JObject abc = JObject.Parse(jsonStrings);
            JToken actiontoken = abc["user"][0]["action"];
            JToken codetoken = abc["user"][0]["code"];
            JToken bstoken = abc["user"][0]["bs"];
            JToken pricetoekn = abc["user"][0]["price"];
            JToken qtytoekn = abc["user"][0]["qty"];
            JToken casestoekn = abc["user"][0]["cases"];

            int action = Convert.ToInt32(actiontoken);
            string code = codetoken.ToString();
            string bs = bstoken.ToString();
            string price = pricetoekn.ToString();
            string qty = qtytoekn.ToString();
            string cases = casestoekn.ToString();


            if (action ==1)
            {
                string update = File.ReadAllText("real/trade.json", Encoding.Default);
                JObject updates = JObject.Parse(update);
                updates["user"][0]["action"] = 0;
                updates["user"][0]["code"] = 0;
                updates["user"][0]["bs"] = 0;
                updates["user"][0]["price"] = 0;
                updates["user"][0]["qty"] = 0;
                updates["user"][0]["cases"] = 0;
                updates["user"][0]["totalsec"] = nowsec;
                string updatestring = Convert.ToString(updates);//将json装换为string
                File.WriteAllText("real/trade.json", updatestring);//将内容写进jon文件中

                Spapidll.AddOrder(Convert.ToChar(bs), price, code, qty, cases, 0.ToString());
            }
            return 0;
        }

    }
}
