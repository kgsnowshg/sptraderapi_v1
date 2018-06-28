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
    class realoperate
    {
        public static string deal(string result1, string result2, string result3, string result4, string result5, string result6, string code)
        {
            //讀取waitlist
            string getwaitStrings = File.ReadAllText("real/waitlist.json", Encoding.Default);
            JObject getwaitlist = JObject.Parse(getwaitStrings);
            JToken wlpositiontoken = getwaitlist["user"][0]["position"];
            int wposition = Convert.ToInt32(wlpositiontoken);


            //讀取user
            string jsonStrings = File.ReadAllText("real/position.json", Encoding.Default);
            JObject abc = JObject.Parse(jsonStrings);
            JToken uprice = abc["user"][0]["price"];
            JToken uposition = abc["user"][0]["position"];
            JToken ucases = abc["user"][0]["cases"];
            JToken ubs = abc["user"][0]["BS"];
            int uuprice = Convert.ToInt32(uprice);
            int uuposition = Convert.ToInt32(uposition);
            string uucases = ucases.ToString();
            string uubs = ubs.ToString();

            //分析
            string[,] ary = new string[6, 23];
            string[] ary1 = result1.Split('|');
            string[] ary2 = result2.Split('|');
            string[] ary3 = result3.Split('|');
            string[] ary4 = result4.Split('|');
            string[] ary5 = result5.Split('|');
            string[] ary6 = result6.Split('|');
            int ii = 0; //loop result
            int cancel = 0; //平倉
            int buy = 0;
            int sell = 0;
            int buysell = 1;
            int bs = 0; //1為買入,2為賣出
            int signal = 0; //決定是否買賣
            string cases; //描述
            int action = 0;//獲得信號, 1為買入/賣出,2為平倉
            int row = 0; //ary[row]
            int price = 0; //交易價格
            foreach (string i in ary1)
            {
                ary[0, ii] = i;
                ii++;
            }
            ii = 0;
            foreach (string i in ary2)
            {
                ary[1, ii] = i;
                ii++;
            }
            ii = 0;
            foreach (string i in ary3)
            {
                ary[2, ii] = i;
                ii++;
            }
            ii = 0;
            foreach (string i in ary4)
            {
                ary[3, ii] = i;
                ii++;
            }
            ii = 0;
            foreach (string i in ary5)
            {
                ary[4, ii] = i;
                ii++;
            }
            ii = 0;
            foreach (string i in ary6)
            {
                ary[5, ii] = i;
                ii++;
            }

            for (int i = 0; i < 6; i++)
            {
                if ((Convert.ToInt32(ary[i, 0]) > 0) && (Convert.ToInt32(ary[i, 0]) > action))
                {
                    action = Convert.ToInt32(ary[i, 0]);
                    if (action == 1)
                    {
                        row = i;
                        if (Convert.ToInt32(ary[i, 1]) == 1)
                        {
                            buy = 1;
                        }
                        if (Convert.ToInt32(ary[i, 1]) == 2)
                        {
                            sell = 1;
                        }
                    }
                    if (action == 2 && uuposition != 0)
                    {
                        buy = 0;
                        sell = 0;
                        buysell = 1;
                        if (Convert.ToInt32(ary[i, 1]) == 1)
                        {
                            bs = 1;
                        }
                        if (Convert.ToInt32(ary[i, 1]) == 2)
                        {
                            bs = 2;
                        }
                        price = Convert.ToInt32(ary[i, 2]);
                        cases = ary[i, 3];

                        //檢查waitlist
                        if (bs == 1 && wposition == 0 && uuposition == -1)
                        {
                            testbuy.testbs("1", code, price.ToString(), "1", cases);
                            //加入waitlist
                        }
                        if (bs == 1 && wposition == -1 && uuposition == -1)
                        {
                            testbuy.testdel(code);
                            testbuy.testbs("1", code, price.ToString(), "1", cases);
                            //加入waitlist
                        }
                        if (bs == 2 && wposition == 0 && uuposition == 1)
                        {
                            testbuy.testbs("2", code, price.ToString(), "1", cases);
                            //加入waitlist
                        }
                        if (bs == 2 && wposition == 1 && uuposition == 1)
                        {
                            testbuy.testdel(code);
                            testbuy.testbs("2", code, price.ToString(), "1", cases);
                            //加入waitlist
                        }

                    }
                }
            }
            signal = buy + sell;
            if (signal > 0)
            {

                bs = Convert.ToInt32(ary[row, 1]);
                price = Convert.ToInt32(ary[row, 2]);
                cases = ary[row, 3];
                if (uuposition == 0)
                {
                    if (bs == 1 && wposition == 0)
                    {
                        testbuy.testbs("1", code, price.ToString(), "1", cases);
                        //加入waitlist
                    }
                    if (bs == 1 && wposition == -1)
                    {
                        testbuy.testdel(code);
                        testbuy.testbs("1", code, price.ToString(), "1", cases);
                        //加入waitlist
                    }
                    if (bs == 2 && wposition == 0)
                    {
                        testbuy.testbs("2", code, price.ToString(), "1", cases);
                        //加入waitlist
                    }
                    if (bs == 2 && wposition == 1)
                    {
                        testbuy.testdel(code);
                        testbuy.testbs("2", code, price.ToString(), "1", cases);
                        //加入waitlist
                    }
                }
                if (uuposition == 1)
                {
                    if (bs == 2 && wposition == 1)
                    {
                        testbuy.testdel(code);
                        testbuy.testbs("2", code, price.ToString(), "2", cases);
                        //加入waitlist
                    }
                    if (bs == 2 && wposition == 0)
                    {
                        testbuy.testbs("2", code, price.ToString(), "2", cases);
                        //加入waitlist
                    }
                }
                if (uuposition == -1)
                {
                    if (bs == 1 && wposition == -1)
                    {
                        testbuy.testdel(code);
                        testbuy.testbs("1", code, price.ToString(), "2", cases);
                        //加入waitlist
                    }
                    if (bs == 1 && wposition == 0)
                    {
                        testbuy.testbs("1", code, price.ToString(), "2", cases);
                        //加入waitlist
                    }
                }
            }

            return "0";
        }


        public static string operation(string result, int price, int position, int events)
        {

            return "0";
        }
    }
}
