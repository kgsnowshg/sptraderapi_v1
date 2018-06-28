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
    class user
    {
        public static string[] getuser()
        {
            string jsonStrings = File.ReadAllText("json/user.json", Encoding.Default);
            JObject abc = JObject.Parse(jsonStrings);
            JToken price = abc["user"]["price"];
            JToken postion = abc["user"]["postion"];
            JToken cases = abc["user"]["cases"];

            int iprice = Convert.ToInt32(price);
            int iposition = Convert.ToInt32(postion);
            string icases = (cases).ToString();
            string[] array = new string[4];
            array[0] = iprice.ToString();
            array[1] = iposition.ToString();
            array[2] = icases.ToString();
            return array;
        }

        public static string[] getwaitlist()
        {
            string jsonStrings = File.ReadAllText("json/waitlist.json", Encoding.Default);
            JObject abc = JObject.Parse(jsonStrings);
            JToken price = abc["user"][0]["price"];
            JToken position = abc["user"][0]["position"];
            JToken cases = abc["user"][0]["cases"];
            JToken time = abc["user"][0]["time"];

            int iprice = Convert.ToInt32(price);
            int iposition = Convert.ToInt32(position);
            string icases = cases.ToString();
            string itime = cases.ToString();
            string[] array = new string[4];
            array[0] = iprice.ToString();
            array[1] = iposition.ToString();
            array[2] = icases;
            array[3] = itime;
            return array;
        }

        public static string saveuser(double hsi,string str, int hour, int min, int sec)
        {
            string[] ary = str.Split('|');
            int price = Convert.ToInt32(ary[0]);
            int position = Convert.ToInt32(ary[1]);
            string cases = ary[2].ToString();
            string time = ary[3].ToString();
            string[] ary2= cases.Split(':');
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
                if (totalsec>(uusec+55))
                {
                    change = 1;
                }
                if (ary2[4] == "5min30pricesell")
                {
                    if (hsi > price - 30 || refhour > hour || refmin - 4 > min)
                    {
                        string update = File.ReadAllText("json/waitlist.json", Encoding.Default);
                        JObject updates = JObject.Parse(update);
                        updates["user"][0]["price"] = hsi;
                        string updatestring = Convert.ToString(updates);//将json装换为string
                        File.WriteAllText("json/waitlist.json", updatestring);//将内容写进jon文件中
                    }
                }
                if (ary2[4] == "5min30pricebuy")
                {
                    if (hsi > price - 30 || refhour > hour || refmin - 4 > min)
                    {
                        string update = File.ReadAllText("json/waitlist.json", Encoding.Default);
                        JObject updates = JObject.Parse(update);
                        updates["user"][0]["price"] = hsi;
                        string updatestring = Convert.ToString(updates);//将json装换为string
                        File.WriteAllText("json/waitlist.json", updatestring);//将内容写进jon文件中
                    }
                }
                if (ary2[4] == "nowbuy")
                {
                    string update = File.ReadAllText("json/waitlist.json", Encoding.Default);
                    JObject updates = JObject.Parse(update);
                    updates["user"][0]["price"] = hsi;
                    string updatestring = Convert.ToString(updates);//将json装换为string
                    File.WriteAllText("json/waitlist.json", updatestring);//将内容写进jon文件中
                }
                if (ary2[4] == "nowsell")
                {
                    string update = File.ReadAllText("json/waitlist.json", Encoding.Default);
                    JObject updates = JObject.Parse(update);
                    updates["user"][0]["price"] = hsi;
                    string updatestring = Convert.ToString(updates);//将json装换为string
                    File.WriteAllText("json/waitlist.json", updatestring);//将内容写进jon文件中
                }
                if (ary2[4] == "1minbuy" && change == 1)
                {
                    string update = File.ReadAllText("json/waitlist.json", Encoding.Default);
                    JObject updates = JObject.Parse(update);
                    updates["user"][0]["price"] = hsi;
                    string updatestring = Convert.ToString(updates);//将json装换为string
                    File.WriteAllText("json/waitlist.json", updatestring);//将内容写进jon文件中
                }
                if (ary2[4] == "1minsell" && change == 1)
                {
                    string update = File.ReadAllText("json/waitlist.json", Encoding.Default);
                    JObject updates = JObject.Parse(update);
                    updates["user"][0]["price"] = hsi;
                    string updatestring = Convert.ToString(updates);//将json装换为string
                    File.WriteAllText("json/waitlist.json", updatestring);//将内容写进jon文件中
                }
            }
            if ((position > 0 && price >= hsi) || (position < 0 && price <= hsi))
            {
                //更新持倉表user.json
                int totalsec = Convert.ToInt32(hour) * 60 * 60 + Convert.ToInt32(min) * 60 + Convert.ToInt32(sec);
                string jsonString = File.ReadAllText("json/user.json", Encoding.Default);
                JObject abcs = JObject.Parse(jsonString);
                JToken uposition = abcs["user"][0]["position"];
                int uuposition = Convert.ToInt32(uposition);
                abcs["user"][0]["price"] = price;
                abcs["user"][0]["position"] = uuposition+ position;
                abcs["user"][0]["cases"] = cases;
                string convertString = Convert.ToString(abcs);//将json装换为string
                File.WriteAllText("json/user.json", convertString);//将内容写进jon文件中

                //更新order表waitlist.json
                string jsonStrings = File.ReadAllText("json/waitlist.json", Encoding.Default);
                JObject abcss = JObject.Parse(jsonString);
                abcss["user"][0]["price"] = 0;
                abcss["user"][0]["position"] = 0;
                abcss["user"][0]["cases"] = 0;
                abcss["user"][0]["time"] = 0;
                string convertString2 = Convert.ToString(abcss);//将json装换为string
                File.WriteAllText("json/waitlist.json", convertString2);//将内容写进jon文件中

                //更新log
                StreamReader sr = new StreamReader(@"json/log2.txt");
                string result = sr.ReadToEnd();
                sr.Close();
                StreamWriter sw = new StreamWriter(@"json/log2.txt");
                sw.WriteLine(result);
                sw.WriteLine("price: " + price.ToString());
                sw.WriteLine("B/S:"+(uuposition + position).ToString());
                sw.WriteLine("CASE: " + cases);
                sw.WriteLine("time: " + time + "," + totalsec.ToString());
                sw.Close();
            }
            return "0";
        }

        public static string savelog(int price, int position, string cases)
        {
            DateTime dt = DateTime.Now;
            int hour = Convert.ToInt32(dt.Hour.ToString());//13
            int min = Convert.ToInt32(dt.Minute.ToString());//13
            int sec = Convert.ToInt32(dt.Second.ToString());//13
            int totalsec = Convert.ToInt32(hour) * 60 * 60 + Convert.ToInt32(min) * 60 + Convert.ToInt32(sec);
            string time = dt.ToString();//2005-11-5 13:47:04
            string jsonString = File.ReadAllText("json/log.txt", Encoding.Default);
            StreamReader sr = new StreamReader(@"json/log.txt");
            string result = sr.ReadToEnd();
            sr.Close();
            StreamWriter sw = new StreamWriter(@"json/log.txt");
            sw.WriteLine(result);
            sw.WriteLine("price: " + price.ToString());
            sw.WriteLine("B/S:" + position.ToString());
            sw.WriteLine("CASE: " + cases);
            sw.WriteLine("time: " + time + "," + totalsec.ToString());
            sw.Close();
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
            sws.WriteLine("B/S: "+ position.ToString());
            sws.WriteLine("CASE: " + cases);
            sws.WriteLine("time: " + time + ":" + totalsec.ToString());
            sws.Close();
            return "0";
        }
    }
}
