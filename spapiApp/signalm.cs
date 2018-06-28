using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;//IsoDateTimeConverter
using System.IO;
namespace spapiApp
{
    class signalm
    {
        public static string[] hsi(int hour, int min, int sec, double hsi)
        {
            string jsonStrings = File.ReadAllText("real/position.json", Encoding.Default);
            JObject abc = JObject.Parse(jsonStrings);
            JToken uprice = abc["user"][0]["price"];
            JToken uposition = abc["user"][0]["position"];
            JToken ucases = abc["user"][0]["cases"];
            JToken ubs = abc["user"][0]["BS"];
            int uuprice = Convert.ToInt32(uprice);
            string uucase = ucases.ToString();
            int totalsec = Convert.ToInt32(hour) * 60 * 60 + Convert.ToInt32(min) * 60 + Convert.ToInt32(sec);
            int uuposition = Convert.ToInt32(uposition);
            string[] positioncase = uucase.Split(':');
            int myStringCount = 0;
            for (int i = 0; i < uucase.Length; i++)
            {
                myStringCount++;
            }
            
            if (myStringCount > 5)
            {

            }
            int buys = 0;
            int sells = 0;
            int path;
            string cases = "";
            //獲得過去三十分鐘的值
            int action1 = 0;
            string[] arr = new string[23];
            int[,] value = new int[50, 7];
            string time = hour.ToString() + ":" + min.ToString() + ":" + sec.ToString();
            arr[0] = "0";
            for (int i = 0; i < 50; i++)
            {
                path = min - i;
                string str = string.Join(" ", getmin.min1(hour, path, sec));
                string[] ary = str.Split(' ');
                value[i, 0] = Convert.ToInt32(ary[0]); //現在
                value[i, 1] = Convert.ToInt32(ary[1]); //最高
                value[i, 2] = Convert.ToInt32(ary[2]); //最低
                value[i, 3] = Convert.ToInt32(ary[3]); //成交量
                value[i, 4] = Convert.ToInt32(ary[4]); //AREA最高
                value[i, 5] = Convert.ToInt32(ary[5]); //AREA最低
                value[i, 6] = Convert.ToInt32(ary[6]); //AREA現在
            }
            int qty = value[0, 3] - value[1, 3];
            int[] min10hvalue = new int[10];
            int[] min10lvalue = new int[10];
            int[] min10h = new int[10];
            int[] min10l = new int[10];
            int[] min10n = new int[10];
            for (int i=0;i<10;i++)
            {
                min10hvalue[i] = value[i, 1]; //最高
                min10lvalue[i] = value[i, 2]; //最低
                min10h[i] = value[i, 4]; //AREA最高
                min10l[i] = value[i, 5]; //AREA最低
                min10n[i] = value[i, 6]; //AREA現在
            }
            int min10hvalues = 0;
            int min10lvalues = 0;
            int areamin10h= 0;
            int areamin10l= 0;
            int areamin10n= 0;
            var result10h = from item in min10h group item by item into gro orderby gro.Count() descending select new { num = gro.Key, nums = gro.Count() };
            foreach (var item in result10h.Take(1))
            {
                areamin10h = item.num;
            };
            var result10l = from item in min10h group item by item into gro orderby gro.Count() descending select new { num = gro.Key, nums = gro.Count() };
            foreach (var item in result10l.Take(1))
            {
                areamin10l = item.num;
            };
            var result10n = from item in min10h group item by item into gro orderby gro.Count() descending select new { num = gro.Key, nums = gro.Count() };
            foreach (var item in result10n.Take(1))
            {
                areamin10n = item.num;
            };
            ArrayList list10hvalue = new ArrayList(min10hvalue);
            ArrayList list10lvalue = new ArrayList(min10lvalue);
            ArrayList list10h = new ArrayList(min10h);
            ArrayList list10l = new ArrayList(min10l);
            list10hvalue.Sort();
            list10lvalue.Sort();
            list10h.Sort();
            list10l.Sort();
            int arealmin10 = Convert.ToInt32(list10l[0]);
            int arealmax10 = Convert.ToInt32(list10l[list10l.Count - 1]);
            int areahmin10 = Convert.ToInt32(list10h[0]);
            int areahmax10 = Convert.ToInt32(list10h[list10h.Count - 1]);
            int min10valuel = Convert.ToInt32(list10lvalue[0]);
            int min10valuelh = Convert.ToInt32(list10lvalue[list10lvalue.Count - 1]); //10分鐘最低的最大值
            int min10valueh = Convert.ToInt32(list10hvalue[0]);//10分鐘最高的最小值
            int min10valuehh = Convert.ToInt32(list10hvalue[list10hvalue.Count - 1]);//10分鐘最高的最大值
            if ((arealmax10- arealmin10) > 4)
            {
                string setting = File.ReadAllText("json/setting.json", Encoding.Default);
                JObject settings = JObject.Parse(setting);
                settings["user"][0]["low"] = min10valuelh;
                string settingsString = Convert.ToString(settings);//将json装换为string
                File.WriteAllText("json/setting.json", settingsString);//将内容写进jon文件中
            }

            if ((areahmax10 - areahmin10) > 4)
            {
                string setting = File.ReadAllText("json/setting.json", Encoding.Default);
                JObject settings = JObject.Parse(setting);
                settings["user"][0]["high"] = min10valueh;
                string settingsString = Convert.ToString(settings);//将json装换为string
                File.WriteAllText("json/setting.json", settingsString);//将内容写进jon文件中
            }

            string getsetting = File.ReadAllText("json/setting.json", Encoding.Default);
            JObject getsettings = JObject.Parse(getsetting);
            JToken lowsettingtoken = getsettings["user"][0]["low"];
            JToken highsettingtoken = getsettings["user"][0]["high"];
            int lowsetting = Convert.ToInt32(lowsettingtoken);
            int highsetting = Convert.ToInt32(highsettingtoken);

            int[] min20h = new int[10];
            int[] min20l = new int[10];
            int[] min20n = new int[10];
            for (int i = 0; i < 10; i++)
            {
                min20h[i] = value[i, 4]; //AREA最高
                min20l[i] = value[i, 5]; //AREA最低
                min20n[i] = value[i, 6]; //AREA現在
            }
            int areamin20h = 0;
            int areamin20l = 0;
            int areamin20n = 0;
            var result20h = from item in min20h group item by item into gro orderby gro.Count() descending select new { num = gro.Key, nums = gro.Count() };
            foreach (var item in result20h.Take(1))
            {
                areamin20h = item.num;
            };
            var result20l = from item in min20h group item by item into gro orderby gro.Count() descending select new { num = gro.Key, nums = gro.Count() };
            foreach (var item in result20l.Take(1))
            {
                areamin20l = item.num;
            };
            var result20n = from item in min20h group item by item into gro orderby gro.Count() descending select new { num = gro.Key, nums = gro.Count() };
            foreach (var item in result10n.Take(1))
            {
                areamin20n = item.num;
            };

            ArrayList list20h = new ArrayList(min20h);
            ArrayList list20l = new ArrayList(min20l);
            list20h.Sort();
            list20l.Sort();
            int areamin20 = Convert.ToInt32(list20l[0]);
            int areammax20 = Convert.ToInt32(list20h[list20h.Count - 1]);

            int[] min30h = new int[10];
            int[] min30l = new int[10];
            int[] min30n = new int[10];
            for (int i = 0; i < 10; i++)
            {
                min30h[i] = value[i, 4]; //AREA最高
                min30l[i] = value[i, 5]; //AREA最低
                min30n[i] = value[i, 6]; //AREA現在
            }
            int areamin30h = 0;
            int areamin30l = 0;
            int areamin30n = 0;
            var result30h = from item in min30h group item by item into gro orderby gro.Count() descending select new { num = gro.Key, nums = gro.Count() };
            foreach (var item in result30h.Take(1))
            {
                areamin30h = item.num;
            };
            var result30l = from item in min30h group item by item into gro orderby gro.Count() descending select new { num = gro.Key, nums = gro.Count() };
            foreach (var item in result30l.Take(1))
            {
                areamin30l = item.num;
            };
            var result30n = from item in min30h group item by item into gro orderby gro.Count() descending select new { num = gro.Key, nums = gro.Count() };
            foreach (var item in result30n.Take(1))
            {
                areamin30n = item.num;
            };

            ArrayList list30h = new ArrayList(min30h);
            ArrayList list30l = new ArrayList(min30l);
            list30h.Sort();
            list30l.Sort();
            int areamin30 = Convert.ToInt32(list30l[0]);
            int areammax30 = Convert.ToInt32(list30h[list30h.Count - 1]);

            //獲得過去十分鐘最高/最低
            int high10 = value[0, 1];
            int low10 = value[0, 2];
            int high20 = value[0, 1];
            int low20 = value[0, 2];
            int high30 = value[0, 1];
            int low30 = value[0, 2];
            int high50 = value[0, 1];
            int low50 = value[0, 2];
            for (int i = 0; i < 9; i++)
            {
                if (value[i+1, 1]> value[i, 1])
                {
                    high10 = value[i + 1, 1];
                }
                if (value[i + 1, 1] < value[i, 1])
                {
                    low10 = value[i + 1, 1];
                }
            }
            //獲得過去二十分鐘最高/最低
            for (int i = 0; i < 19; i++)
            {
                if (value[i + 1, 1] > value[i, 1])
                {
                    high20 = value[i + 1, 1];
                }
                if (value[i + 1, 1] < value[i, 1])
                {
                    low20 = value[i + 1, 1];
                }
            }
            //獲得過去三十分鐘最高/最低
            for (int i = 0; i < 29; i++)
            {
                if (value[i + 1, 1] > value[i, 1])
                {
                    high30 = value[i + 1, 1];
                }
                if (value[i + 1, 1] < value[i, 1])
                {
                    low30 = value[i + 1, 1];
                }
            }
            //獲得過去五十分鐘最高/最低
            for (int i = 0; i < 49; i++)
            {
                if (value[i + 1, 1] > value[i, 1])
                {
                    high50 = value[i + 1, 1];
                }
                if (value[i + 1, 1] < value[i, 1])
                {
                    low50 = value[i + 1, 1];
                }
            }
            arr[4] = qty.ToString();
            arr[5] = high10.ToString();
            arr[6] = low10.ToString();
            arr[7] = high20.ToString();
            arr[8] = low20.ToString();
            arr[9] = high30.ToString();
            arr[10] = low30.ToString();
            arr[11] = high50.ToString();
            arr[12] = low50.ToString();
            arr[13] = areamin10h.ToString();
            arr[14] = areamin10l.ToString();
            arr[15] = areamin10n.ToString();
            arr[16] = areamin20h.ToString();
            arr[17] = areamin20l.ToString();
            arr[18] = areamin20n.ToString();
            arr[19] = areamin30h.ToString();
            arr[20] = areamin30l.ToString();
            arr[21] = areamin30n.ToString();
            int calculatelow = (value[1, 2]) - (value[2, 2]);
            int calculatelow2 = (value[0, 2]) - (value[1, 2]);
            int sumlow = 0;
            if (calculatelow == 0 || calculatelow2 == 0)
            {
                sumlow = 0;
            }
            else
            {
                sumlow = calculatelow / calculatelow2;
            }
            int safebuy = 0;
            int safesell = 0;
            int safebuy2 = 1; //此為平倉信號 因此為1

            if (((value[0, 1]) > (value[0, 2]) + 25) && ((value[0, 1]) > (value[0, 0]) + 10))
            {
                safebuy2 = 0;
            }
                if ((highsetting) < ((value[0, 0]) - 15) || ((value[0, 0]) - 15) < (highsetting))
            {
                safebuy = 1;
            }
            if ((lowsetting) < ((value[0, 0]) - 15) || ((value[0, 0]) + 15) < (lowsetting))
            {
                safesell = 1;
            }
            if (sec>30 && safesell == 1 && safebuy==1 && safebuy2 ==1 && sumlow < 2 && (value[1, 1]) > (value[2, 1]) && (value[1, 2]) > (value[2, 2]) && (value[2, 1]) > (value[3, 1]) && (value[2, 2]) > (value[3, 2]))
            {
                int buylow = (value[1, 2]) - (value[2, 2]);
                if (buylow <= 200)
                {
                    arr[0] = "1"; //0為無需執行,1為有signal,2為平倉
                    arr[1] = "1"; //1為買入,2為賣出
                    arr[2] = (value[0, 0]).ToString();
                    arr[3] = time + ":" + arr[2] + ":nowbuy:3timebuy";
                    user.savelog(Convert.ToInt32(arr[2]), Convert.ToInt32(arr[1]), arr[3]);
                    return arr;
                }
                else
                {
                    if (uuposition < 0)
                    {
                        arr[0] = "2"; //0為無需執行,1為有signal,2為平倉
                        arr[1] = "1"; //1為買入,2為賣出
                        arr[2] = (value[0, 0]).ToString();
                        arr[3] = time + ":" + arr[2] + ":nowbuy:3timebuy";
                        user.savelog(Convert.ToInt32(arr[2]), Convert.ToInt32(arr[1]), arr[3]);
                        return arr;
                    }
                    else
                    {
                        cases = "the buylow was exceed";
                        log.error(cases);
                    }
                }
                return arr;
            }
            int safesell2 = 1;//此為平倉信號 因此為1
            if (((value[0, 2]) < (value[0, 1]) - 25) && ((value[0, 2]) < (value[0, 0]) - 10))
            {
                safesell2 = 0;
            }
            if (sec>30 && safebuy==1 && safesell==1 && safesell2 == 1 && sumlow <2 && (value[1, 1]) < (value[2, 1]) && (value[1, 2]) < (value[2, 2]) && (value[2, 1]) < (value[3, 1]) && (value[2, 2]) < (value[3, 2]))
            {
                int sellhigh = (value[2, 1]) - (value[1, 1]);
                if (sellhigh <= 200)
                {
                    arr[0] = "1";
                    arr[1] = "2";
                    arr[2] = (value[0, 0]).ToString();
                    arr[3] = time + ":" + arr[2] + ":nowsell:3timesell";
                    user.savelog(Convert.ToInt32(arr[2]), Convert.ToInt32(arr[1]), arr[3]);
                    return arr;
                }
                else
                {
                    if (uuposition > 0)
                    {
                        arr[0] = "2"; //0為無需執行,1為有signal,2為平倉
                        arr[1] = "2"; //1為買入,2為賣出
                        arr[2] = ((value[0, 0])).ToString();
                        arr[3] = time + ":" + arr[2] + ":nowsell:3timesell";
                        user.savelog(Convert.ToInt32(arr[2]), Convert.ToInt32(arr[1]), arr[3]);
                        return arr;
                    }
                    else
                    {
                        cases = "the sellhigh was exceed";
                        log.error(cases);
                    }
                }
            }

            //平倉盤
            if (((value[0, 1]) > (value[0, 2]) + 18) && ((value[0, 1]) > (value[0, 0]) + 10) && value[1, 1] < ((value[1, 2]) - 2))  //轉做賣出
            {
                arr[0] = "2"; 
                arr[1] = "2";
                arr[2] = ((value[0, 0])).ToString();
                arr[3] = time + ":" + arr[2] + ":nowsell:peisellcase1";
                return arr;
            }
            if (((value[0, 2]) < (value[0, 1]) - 18) && ((value[0, 2]) < (value[0, 0]) - 10) && value[1, 2] > ((value[2, 2]) + 2))//轉做買入
            {
                arr[0] = "2";
                arr[1] = "1";
                arr[2] = ((value[0, 0])).ToString();
                arr[3] = time + ":" + arr[2] + ":nowbuy:peibuycase1";
                return arr;
            }

            if ((value[0, 2]) < (value[1, 2]))
            {
                if (uuposition > 0)
                {
                    if (positioncase[5] == "50qtybuy")
                    {
                        if (((value[0, 0]) - uuprice) > 9)
                        {
                            arr[0] = "2";
                            arr[1] = "2";
                            arr[2] = ((value[0, 0])).ToString();
                            arr[3] = time + ":" + arr[2] + ":nowsell:peisellcase2";
                            return arr;
                        }
                        else if (((value[0, 0]) - uuprice) < 0)
                        {
                            arr[0] = "2";
                            arr[1] = "2";
                            arr[2] = ((value[0, 0])).ToString();
                            arr[3] = time + ":" + arr[2] + ":5min30pricesell:5min30pricesell";
                            return arr;
                        }
                    }
                    else if (positioncase[5] == "3timebuy" || positioncase[5] == "selfbuy")
                    {
                        if ((((value[0, 0]) - uuprice) > 25) || (((value[0, 0]) - uuprice)>10 &&totalsec<33660))
                        {
                            arr[0] = "2";
                            arr[1] = "2";
                            arr[2] = ((value[0, 0])).ToString();
                            arr[3] = time + ":" + arr[2] + ":nowsell:peisellcase2";
                            return arr;
                        }
                    }
                    else
                    {
                        arr[0] = "2";
                        arr[1] = "2";
                        arr[2] = ((value[0, 0])).ToString();
                        arr[3] = time + ":" + arr[2] + ":nowsell:peisellcase2";
                        return arr;
                    }
                }
            }

            if ((value[0, 2]) > (value[1, 2]))
            {
                if (uuposition < 0)
                {
                    if (positioncase[5] == "50qtysell")
                    {
                        if (((value[0, 0]) - uuprice) < -9)
                        {
                            arr[0] = "2";
                            arr[1] = "1";
                            arr[2] = ((value[0, 0])).ToString();
                            arr[3] = time + ":" + arr[2] + ":nowbuy:peibuycase2";
                            return arr;
                        }
                        else if (((value[0, 0]) - uuprice) > 0)
                        {
                            arr[0] = "2";
                            arr[1] = "1";
                            arr[2] = ((value[0, 0])).ToString();
                            arr[3] = time + ":" + arr[2] + ":5min30pricebuy:5min30pricebuy";
                            return arr;
                        }
                    }
                    else if (positioncase[5] == "3timesell" || positioncase[5] == "selfsell")
                    {
                        if ((((value[0, 0]) - uuprice) < -25) || (((value[0, 0]) - uuprice) < -10 && totalsec<33660))
                        {
                            arr[0] = "2";
                            arr[1] = "1";
                            arr[2] = ((value[0, 0])).ToString();
                            arr[3] = time + ":" + arr[2] + ":nowbuy:peibuycase2";
                            return arr;
                        }
                    }
                    else
                    {
                        arr[0] = "2";
                        arr[1] = "1";
                        arr[2] = ((value[0, 0])).ToString();
                        arr[3] = time + ":" + arr[2] + ":nowbuy:peibuycase2";
                        return arr;
                    }
                }

            }
            return arr;
        }


        public static int hsia(int hour, int min, int sec, int hsi)
        {
            int buys = 0;
            int sells = 0;
            string cases = "";
            //獲得過去三十分鐘的值
            int action1 = 0;
            int[,] value = new int[30, 4];
            for (int i = 0; i < 30; i++)
            {
                int path = min - i;
                string str = string.Join(" ", getmin.min2(hour, path, sec));
                string[] ary = str.Split(' ');
                value[i, 0] = Convert.ToInt32(ary[0]);
                value[i, 1] = Convert.ToInt32(ary[1]);
                value[i, 2] = Convert.ToInt32(ary[2]);
                value[i, 3] = Convert.ToInt32(ary[3]);
            }

            //獲得過去十分鐘最高/最低
            int high10 = value[0, 1];
            int low10 = value[0, 2];
            int high20 = value[0, 1];
            int low20 = value[0, 2];
            int high30 = value[0, 1];
            int low30 = value[0, 2];
            for (int i = 0; i < 9; i++)
            {
                if (value[i + 1, 1] > value[i, 1])
                {
                    high10 = value[i + 1, 1];
                }
                if (value[i + 1, 1] < value[i, 1])
                {
                    low10 = value[i + 1, 1];
                }
            }
            //獲得過去二十分鐘最高/最低
            for (int i = 0; i < 19; i++)
            {
                if (value[i + 1, 1] > value[i, 1])
                {
                    high20 = value[i + 1, 1];
                }
                if (value[i + 1, 1] < value[i, 1])
                {
                    low20 = value[i + 1, 1];
                }
            }
            //獲得過去三十分鐘最高/最低
            for (int i = 0; i < 29; i++)
            {
                if (value[i + 1, 1] > value[i, 1])
                {
                    high30 = value[i + 1, 1];
                }
                if (value[i + 1, 1] < value[i, 1])
                {
                    low30 = value[i + 1, 1];
                }
            }
            return 0;
        }
    }
}
