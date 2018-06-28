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
    class waitsignal
    {
        public static int fastoperate(string operates, string operate2, string price, string cases)
        {
            string[] ary = cases.Split(':');
            string update = File.ReadAllText("json/waitsignal.json", Encoding.Default);
            JObject updates = JObject.Parse(update);
            updates["user"][0]["operate"] = operates;
            updates["user"][0]["position"] = operate2;
            updates["user"][0]["price"] = price;
            updates["user"][0]["cases"] = cases;
            string updatestring = Convert.ToString(updates);//将json装换为string
            File.WriteAllText("json/waitlist.json", updatestring);//将内容写进jon文件中
            return 0;
        }
        public static int set0()
        {
            string update = File.ReadAllText("json/waitsignal.json", Encoding.Default);
            JObject updates = JObject.Parse(update);
            updates["user"][0]["operate"] = 0;
            updates["user"][0]["position"] = 0;
            updates["user"][0]["price"] = 0;
            updates["user"][0]["cases"] = 0;
            string updatestring = Convert.ToString(updates);//将json装换为string
            File.WriteAllText("json/waitlist.json", updatestring);//将内容写进jon文件中
            return 0;
        }

        public static string[] controlwaitsignal(double hsi,string code)
        {
            DateTime dt = DateTime.Now;
            int hour = Convert.ToInt32(dt.Hour.ToString());//13
            int min = Convert.ToInt32(dt.Minute.ToString());//13
            int sec = Convert.ToInt32(dt.Second.ToString());//13
            int nowsec = Convert.ToInt32(hour) * 60 * 60 + Convert.ToInt32(min) * 60 + Convert.ToInt32(sec);
            string update = File.ReadAllText("json/waitsignal.json", Encoding.Default);
            JObject updates = JObject.Parse(update);
            JToken operates = updates["user"][0]["operates"];
            JToken position = updates["user"][0]["position"];
            JToken price = updates["user"][0]["price"];
            JToken cases = updates["user"][0]["cases"];
            int ioperates = Convert.ToInt32(operates);
            int iposition = Convert.ToInt32(position);
            int iprice = Convert.ToInt32(price);
            string scases = cases.ToString();
            string[] ary = scases.Split(':');
            int myStringCount = 0;
            int totalsec = 100000;
            for (int i = 0; i < scases.Length; i++)
            {
                myStringCount++;
            }
            if (myStringCount > 5)
            {
                totalsec = Convert.ToInt32(ary[0]) * 60 * 60 + Convert.ToInt32(ary[1]) * 60 + Convert.ToInt32(ary[2]);
            }
            string jsonStrings = File.ReadAllText("real/recordlasttrade.json", Encoding.Default);
            JObject abc = JObject.Parse(jsonStrings);
            JToken casestoken = abc["user"][0]["cases"];
            JToken timetoken = abc["user"][0]["time"];
            string recordcase = casestoken.ToString();
            int lastsec = Convert.ToInt32(timetoken);
            string time = hour.ToString() + ":" + min.ToString() + ":" + sec.ToString();
            string casesbuy= time + ":" + hsi.ToString() + ":nowbuy:fastbuy";
            string casessell = time + ":" + hsi.ToString() + ":nowbuy:fastbuy";
            string[] arr = new string[4];
            
            if ((nowsec > (lastsec + 10) && recordcase == "CASE:+17:::::") || (nowsec > (lastsec + 10) && recordcase == "CASE:+10:::::") || ((recordcase != "CASE:+10:::::") && (recordcase != "CASE:+17:::::")))
            {
                if (nowsec > (totalsec + 7) && iposition == 1)
                {
                    if (hsi > iprice)
                    {
                        arr[0] = "2";
                        arr[1] = "1"; //買入
                        arr[2] = hsi.ToString();
                        arr[3] = time + ":" + arr[2] + ":nowbuy:fastbuy";
                        return arr;
                    }
                    else
                    {
                        arr[0] = "2";
                        arr[1] = "2"; //賣出
                        arr[2] = hsi.ToString();
                        arr[3] = time + ":" + arr[2] + ":nowbuy:fastsell";
                        return arr;
                    }
                }
                if (nowsec > (totalsec + 7) && iposition == -1)
                {
                    if (hsi < iprice)
                    {
                        arr[0] = "2";
                        arr[1] = "2"; //賣出
                        arr[2] = hsi.ToString();
                        arr[3] = time + ":" + arr[2] + ":nowbuy:fastsell";
                        testbuy.testbs("2", code, hsi.ToString(), "1", scases);
                        return arr;
                    }
                    else
                    {
                        arr[0] = "2";
                        arr[1] = "1"; //買入
                        arr[2] = hsi.ToString();
                        arr[3] = time + ":" + arr[2] + ":nowbuy:fastbuy";
                        return arr;
                    }
                }
            }
            arr[0] = "0";
            return arr;
        }
    }
}
