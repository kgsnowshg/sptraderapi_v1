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
    class positions
    {
        public static int update(double hsi)
        {
            DateTime dt = DateTime.Now;
            int hour = Convert.ToInt32(dt.Hour.ToString());//13
            int min = Convert.ToInt32(dt.Minute.ToString());//13
            int sec = Convert.ToInt32(dt.Second.ToString());//13
            int totalsec = Convert.ToInt32(hour) * 60 * 60 + Convert.ToInt32(min) * 60 + Convert.ToInt32(sec);
            string time = dt.ToString();//2005-11-5 13:47:04
            string jsonString = File.ReadAllText("json/user.json", Encoding.Default);
            JObject abcs = JObject.Parse(jsonString);
            JToken uposition = abcs["user"][0]["position"];
            JToken uprice = abcs["user"][0]["price"];
            JToken ucase = abcs["user"][0]["cases"];
            int uuposition = Convert.ToInt32(uposition);
            int uuprice = Convert.ToInt32(uprice);
            string uucase = ucase.ToString();
            string[] ary2 = uucase.Split(':');
            int myStringCount = 0;
            int uuhour = 0;
            int uumin = 0;
            int uusec = 0;
            int uutime = 0;
            for (int i = 0; i < uucase.Length; i++)
            {
                myStringCount++;
            }
            if (myStringCount > 5)
            {
                uuhour = Convert.ToInt32(ary2[0]);
                uumin = Convert.ToInt32(ary2[1]);
                uusec=Convert.ToInt32(ary2[2]);
                uutime= Convert.ToInt32(uuhour) * 60 * 60 + Convert.ToInt32(uumin) * 60 + Convert.ToInt32(uusec);
                if (uuposition > 0 && (hsi > (uuprice + 30)) && (ary2[5] == "50qtybuy"))
                {
                    string jsonStrings = File.ReadAllText("json/user.json", Encoding.Default);
                    JObject abcss = JObject.Parse(jsonStrings);
                    abcs["user"][0]["price"] = 0;
                    abcs["user"][0]["position"] = 0;
                    abcs["user"][0]["cases"] = 0;
                    string convertString = Convert.ToString(abcs);//将json装换为string
                    File.WriteAllText("json/user.json", convertString);//将内容写进jon文件中
                                                                       //更新log
                    StreamReader sr = new StreamReader(@"json/log2.txt");
                    string result = sr.ReadToEnd();
                    sr.Close();
                    StreamWriter sw = new StreamWriter(@"json/log2.txt");
                    sw.WriteLine(result);
                    sw.WriteLine("price: " + hsi.ToString());
                    sw.WriteLine("B/S:-1");
                    sw.WriteLine("0:0:0:0:0:CASE:+30");
                    sw.WriteLine("time: " + time + "," + totalsec.ToString());
                    sw.Close();
                }

                else if (uuposition < 0 && (hsi < (uuprice - 30)) && (ary2[5] == "50qtysell"))
                {
                    string jsonStrings = File.ReadAllText("json/user.json", Encoding.Default);
                    JObject abcss = JObject.Parse(jsonStrings);
                    abcs["user"][0]["price"] = 0;
                    abcs["user"][0]["position"] = 0;
                    abcs["user"][0]["cases"] = 0;
                    string convertString = Convert.ToString(abcs);//将json装换为string
                    File.WriteAllText("json/user.json", convertString);//将内容写进jon文件中//更新log
                    StreamReader sr = new StreamReader(@"json/log2.txt");
                    string result = sr.ReadToEnd();
                    sr.Close();
                    StreamWriter sw = new StreamWriter(@"json/log2.txt");
                    sw.WriteLine(result);
                    sw.WriteLine("price: " + hsi.ToString());
                    sw.WriteLine("B/S:1");
                    sw.WriteLine("0:0:0:0:0:CASE:-30");
                    sw.WriteLine("time: " + time + "," + totalsec.ToString());
                    sw.Close();

                }
                else if ((ary2[5] == "fastbuy") || (ary2[5] == "peibuycase1"))
                {
                    if ((uuposition > 0 && (hsi > (uuprice + 17))))
                    {
                        string jsonStrings = File.ReadAllText("json/user.json", Encoding.Default);
                        JObject abcss = JObject.Parse(jsonStrings);
                        abcs["user"][0]["price"] = 0;
                        abcs["user"][0]["position"] = 0;
                        abcs["user"][0]["cases"] = 0;
                        string convertString = Convert.ToString(abcs);//将json装换为string
                        File.WriteAllText("json/user.json", convertString);//将内容写进jon文件中
                                                                           //更新log
                        StreamReader sr = new StreamReader(@"json/log2.txt");
                        string result = sr.ReadToEnd();
                        sr.Close();
                        StreamWriter sw = new StreamWriter(@"json/log2.txt");
                        sw.WriteLine(result);
                        sw.WriteLine("price: " + hsi.ToString());
                        sw.WriteLine("B/S:-1");
                        sw.WriteLine("0:0:0:0:0:CASE:+17");
                        sw.WriteLine("time: " + time + "," + totalsec.ToString());
                        sw.Close();
                    }
                    else if((uuposition > 0 && (Convert.ToInt32(uutime))>totalsec) && (hsi > (uuprice + 10)))
                    {
                        string jsonStrings = File.ReadAllText("json/user.json", Encoding.Default);
                        JObject abcss = JObject.Parse(jsonStrings);
                        abcs["user"][0]["price"] = 0;
                        abcs["user"][0]["position"] = 0;
                        abcs["user"][0]["cases"] = 0;
                        string convertString = Convert.ToString(abcs);//将json装换为string
                        File.WriteAllText("json/user.json", convertString);//将内容写进jon文件中
                                                                           //更新log
                        StreamReader sr = new StreamReader(@"json/log2.txt");
                        string result = sr.ReadToEnd();
                        sr.Close();
                        StreamWriter sw = new StreamWriter(@"json/log2.txt");
                        sw.WriteLine(result);
                        sw.WriteLine("price: " + hsi.ToString());
                        sw.WriteLine("B/S:-1");
                        sw.WriteLine("0:0:0:0:0:CASE:+10");
                        sw.WriteLine("time: " + time + "," + totalsec.ToString());
                        sw.Close();
                    }

                }

                else if ((ary2[5] == "fastsell") || (ary2[5] == "peisellcase1"))
                {
                    if (uuposition < 0 && (hsi < (uuprice - 17)))
                    {
                        string jsonStrings = File.ReadAllText("json/user.json", Encoding.Default);
                        JObject abcss = JObject.Parse(jsonStrings);
                        abcs["user"][0]["price"] = 0;
                        abcs["user"][0]["position"] = 0;
                        abcs["user"][0]["cases"] = 0;
                        string convertString = Convert.ToString(abcs);//将json装换为string
                        File.WriteAllText("json/user.json", convertString);//将内容写进jon文件中
                                                                           //更新log
                        StreamReader sr = new StreamReader(@"json/log2.txt");
                        string result = sr.ReadToEnd();
                        sr.Close();
                        StreamWriter sw = new StreamWriter(@"json/log2.txt");
                        sw.WriteLine(result);
                        sw.WriteLine("price: " + hsi.ToString());
                        sw.WriteLine("B/S:1");
                        sw.WriteLine("0:0:0:0:0:CASE:-17");
                        sw.WriteLine("time: " + time + "," + totalsec.ToString());
                        sw.Close();
                    }
                    else if (uuposition < 0 && (hsi < (uuprice - 10)) && (Convert.ToInt32(uutime)) > totalsec)
                    {
                        string jsonStrings = File.ReadAllText("json/user.json", Encoding.Default);
                        JObject abcss = JObject.Parse(jsonStrings);
                        abcs["user"][0]["price"] = 0;
                        abcs["user"][0]["position"] = 0;
                        abcs["user"][0]["cases"] = 0;
                        string convertString = Convert.ToString(abcs);//将json装换为string
                        File.WriteAllText("json/user.json", convertString);//将内容写进jon文件中
                                                                           //更新log
                        StreamReader sr = new StreamReader(@"json/log2.txt");
                        string result = sr.ReadToEnd();
                        sr.Close();
                        StreamWriter sw = new StreamWriter(@"json/log2.txt");
                        sw.WriteLine(result);
                        sw.WriteLine("price: " + hsi.ToString());
                        sw.WriteLine("B/S:1");
                        sw.WriteLine("0:0:0:0:0:CASE:-10");
                        sw.WriteLine("time: " + time + "," + totalsec.ToString());
                        sw.Close();
                    }
                }
                else if ((uuposition > 0 && (hsi < (uuprice - 30))))
                {
                    string jsonStrings = File.ReadAllText("json/user.json", Encoding.Default);
                    JObject abcss = JObject.Parse(jsonStrings);
                    abcs["user"][0]["price"] = 0;
                    abcs["user"][0]["position"] = 0;
                    abcs["user"][0]["cases"] = 0;
                    string convertString = Convert.ToString(abcs);//将json装换为string
                    File.WriteAllText("json/user.json", convertString);//将内容写进jon文件中
                                                                       //更新log
                    StreamReader sr = new StreamReader(@"json/log2.txt");
                    string result = sr.ReadToEnd();
                    sr.Close();
                    StreamWriter sw = new StreamWriter(@"json/log2.txt");
                    sw.WriteLine(result);
                    sw.WriteLine("price: " + hsi.ToString());
                    sw.WriteLine("B/S:-1");
                    sw.WriteLine("0:0:0:0:0:CASE:-30");
                    sw.WriteLine("time: " + time + "," + totalsec.ToString());
                    sw.Close();
                }
                else if ((uuposition < 0 && (hsi > (uuprice + 30))))
                {
                    string jsonStrings = File.ReadAllText("json/user.json", Encoding.Default);
                    JObject abcss = JObject.Parse(jsonStrings);
                    abcs["user"][0]["price"] = 0;
                    abcs["user"][0]["position"] = 0;
                    abcs["user"][0]["cases"] = 0;
                    string convertString = Convert.ToString(abcs);//将json装换为string
                    File.WriteAllText("json/user.json", convertString);//将内容写进jon文件中
                                                                       //更新log
                    StreamReader sr = new StreamReader(@"json/log2.txt");
                    string result = sr.ReadToEnd();
                    sr.Close();
                    StreamWriter sw = new StreamWriter(@"json/log2.txt");
                    sw.WriteLine(result);
                    sw.WriteLine("price: " + hsi.ToString());
                    sw.WriteLine("B/S:1");
                    sw.WriteLine("0:0:0:0:0:CASE:-30");
                    sw.WriteLine("time: " + time + "," + totalsec.ToString());
                    sw.Close();
                }
            }


            return 0;
        }
    }
}
