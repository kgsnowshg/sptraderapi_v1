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
    class testbuy
    {
        public static int testbs(string bs,string code, string price,string qty, string ClOrderId)
        {
            //讀取user
            DateTime dt = DateTime.Now;
            int hour = Convert.ToInt32(dt.Hour.ToString());//13
            int min = Convert.ToInt32(dt.Minute.ToString());//13
            int sec = Convert.ToInt32(dt.Second.ToString());//13
            int nowsec = Convert.ToInt32(hour) * 60 * 60 + Convert.ToInt32(min) * 60 + Convert.ToInt32(sec);
            int wcount = Spapidll.countorder();
            int uposition = Spapidll.GetPosByProduct(code);
            string[] ary = ClOrderId.Split(':');
            int countresult = Spapidll.countorder();
            realposition.delmorewait(countresult, code);
            string record = File.ReadAllText("real/recordlasttrade.json", Encoding.Default);
            JObject records = JObject.Parse(record);
            JToken totalsectoken = records["user"][0]["time"];
            JToken casestoken = records["user"][0]["cases"];
            string cases = casestoken.ToString();
            int totalsec = Convert.ToInt32(totalsectoken);
            string[] caseary = cases.Split(':');
            if (ary[5] == "peisellcase2" || ary[5] == "peibuycase2")
            {
                if ((wcount == 0) && uposition != 0)
                {
                    char buy_sell = '0';
                    if (bs == "1")
                    {
                        buy_sell = 'B';
                    }
                    if (bs == "2")
                    {
                        buy_sell = 'S';
                    }
                    string DecInPrice = "0";
                    trade.writetrade(code, buy_sell.ToString(), price, qty, ClOrderId);
                }
            }
            else if (ary[5] == "3timesell" || ary[5] == "3timebuy")
            {
                if ((wcount == 0) && uposition == 0)
                {
                    char buy_sell = '0';
                    if (bs == "1")
                    {
                        buy_sell = 'B';
                    }
                    if (bs == "2")
                    {
                        buy_sell = 'S';
                    }
                    string DecInPrice = "0";
                    if (nowsec > totalsec + 120)
                    {
                        trade.writetrade(code, buy_sell.ToString(), price, qty, ClOrderId);
                    }
                }
            }
            else
            {
                if ((wcount == 0) && uposition != 0)
                {
                    char buy_sell = '0';
                    if (bs == "1")
                    {
                        buy_sell = 'B';
                    }
                    if (bs == "2")
                    {
                        buy_sell = 'S';
                    }
                    string DecInPrice = "0";
                    trade.writetrade(code, buy_sell.ToString(), price, qty, ClOrderId);
                }
            }
            return 0;
        }

        public static int testch(string ChangePrice)
        {
            string jsonStrings = File.ReadAllText("real/waitlist.json", Encoding.Default);
            JObject abc = JObject.Parse(jsonStrings);
            JToken idtoken = abc["user"][0]["id"];
            JToken casestoken = abc["user"][0]["cases"];
            int iid = Convert.ToInt32(idtoken);
            string cases = casestoken.ToString();
            string id = idtoken.ToString();
            if (iid!=0)
            {
                Spapidll.ChangeOrder(id, ChangePrice, cases);
            }
            return 0;
        }

        public static int testdel(string code)
        {
            string jsonStrings = File.ReadAllText("real/waitlist.json", Encoding.Default);
            JObject abc = JObject.Parse(jsonStrings);
            JToken idtoken = abc["user"][0]["id"];
            int iid = Convert.ToInt32(idtoken);
            string id = idtoken.ToString();
            if (iid != 0)
            {
                Spapidll.DeleteOrder(id, code);
            }
            return 0;
        }
    }
}
