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
    class realuser
    {

        public static string[] getwaitlist()
        {
            string jsonStrings = File.ReadAllText("real/waitlist.json", Encoding.Default);
            JObject abc = JObject.Parse(jsonStrings);
            JToken id = abc["user"][0]["id"];
            JToken price = abc["user"][0]["price"];
            JToken position = abc["user"][0]["position"];
            JToken cases = abc["user"][0]["cases"];
            JToken time = abc["user"][0]["time"];

            int iprice = Convert.ToInt32(price);
            int iposition = Convert.ToInt32(position);
            string icases = cases.ToString();
            string itime = cases.ToString();
            string[] array = new string[5];
            array[0] = id.ToString();
            array[1] = iprice.ToString();
            array[2] = iposition.ToString();
            array[3] = icases;
            array[4] = itime;
            return array;
        }

        public static string saveuser(double hsi, string str, int hour, int min, int sec)
        {
            string[] ary = str.Split('|');
            int id = Convert.ToInt32(ary[0]);
            int price = Convert.ToInt32(ary[1]);
            int position = Convert.ToInt32(ary[2]);
            string cases = ary[3].ToString();
            string time = ary[4].ToString();
            string[] ary2 = cases.Split(':');
            int myStringCount = 0;
            for (int i = 0; i < cases.Length; i++)
            {
                myStringCount++;
            }
            if (myStringCount > 5)
            {
                int refhour = Convert.ToInt32(ary2[0]);
                int refmin = Convert.ToInt32(ary2[1]);
                int refsec = Convert.ToInt32(ary2[2]);
                int change = 0;
                int totalsec = Convert.ToInt32(hour) * 60 * 60 + Convert.ToInt32(min) * 60 + Convert.ToInt32(sec);
                int uusec = Convert.ToInt32(refhour) * 60 * 60 + Convert.ToInt32(refmin) * 60 + Convert.ToInt32(refsec);
                if (totalsec > (uusec + 55))
                {
                    change = 1;
                }
                if (ary2[4] == "5min30pricesell")
                {
                    if (hsi > price - 30 || refhour > hour || refmin - 4 > min)
                    {
                        testbuy.testch(hsi.ToString());
                    }
                }
                if (ary2[4] == "5min30pricebuy")
                {
                    if (hsi > price - 30 || refhour > hour || refmin - 4 > min)
                    {
                        testbuy.testch(hsi.ToString());
                    }
                }
                if (ary2[4] == "nowbuy")
                {
                    testbuy.testch(hsi.ToString());
                }
                if (ary2[4] == "nowsell")
                {
                    testbuy.testch(hsi.ToString());
                }
                if (ary2[4] == "1minbuy" && change == 1)
                {
                    testbuy.testch(hsi.ToString());
                }
                if (ary2[4] == "1minsell" && change == 1)
                {
                    testbuy.testch(hsi.ToString());
                }
            }
            
            return "0";
        }



        public static string savewaitlist(int price, int position, string cases)
        {
            DateTime dt = DateTime.Now;
            int hour = Convert.ToInt32(dt.Hour.ToString());//13
            int min = Convert.ToInt32(dt.Minute.ToString());//13
            int sec = Convert.ToInt32(dt.Second.ToString());//13
            int totalsec = Convert.ToInt32(hour) * 60 * 60 + Convert.ToInt32(min) * 60 + Convert.ToInt32(sec);
            string time = dt.ToString();//2005-11-5 13:47:04
            string jsonString = File.ReadAllText("json/user.json", Encoding.Default);
            JObject abcs = JObject.Parse(jsonString);
            abcs["user"][0]["price"] = price;
            abcs["user"][0]["position"] = position;
            abcs["user"][0]["cases"] = cases;
            abcs["user"][0]["time"] = time + "," + totalsec.ToString();
            string convertString = Convert.ToString(abcs);//将json装换为string
            File.WriteAllText("json/waitlist.json", convertString);//将内容写进jon文件中

            StreamReader srs = new StreamReader(@"json/log.txt");
            string result = srs.ReadToEnd();
            srs.Close();
            StreamWriter sws = new StreamWriter(@"json/log.txt");
            sws.WriteLine(result);
            sws.WriteLine("price: " + price.ToString());
            sws.WriteLine("B/S: " + position.ToString());
            sws.WriteLine("CASE: " + cases);
            sws.WriteLine("time: " + time + ":" + totalsec.ToString());
            sws.Close();
            return "0";
        }
    }
}
