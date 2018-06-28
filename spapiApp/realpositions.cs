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
    class realpositions
    {
        public static int update(double hsi, string code)
        {
            DateTime dt = DateTime.Now;
            int hour = Convert.ToInt32(dt.Hour.ToString());//13
            int min = Convert.ToInt32(dt.Minute.ToString());//13
            int sec = Convert.ToInt32(dt.Second.ToString());//13
            int totalsec = Convert.ToInt32(hour) * 60 * 60 + Convert.ToInt32(min) * 60 + Convert.ToInt32(sec);
            string time = dt.ToString();//2005-11-5 13:47:04
            string jsonString = File.ReadAllText("real/position.json", Encoding.Default);
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
                uusec = Convert.ToInt32(ary2[2]);
                uutime = Convert.ToInt32(uuhour) * 60 * 60 + Convert.ToInt32(uumin) * 60 + Convert.ToInt32(uusec);
                if (uuposition > 0 && (hsi > (uuprice + 30)) && (ary2[5] == "50qtybuy"))
                {
                    testbuy.testbs("2", code, hsi.ToString(), "1", "0:0:0:0:nowsell:CASE:+30");
                }

                else if (uuposition < 0 && (hsi < (uuprice - 30)) && (ary2[5] == "50qtysell"))
                {

                    testbuy.testbs("1", code, hsi.ToString(), "1", "0:0:0:0:nowbuy:CASE:+30");
                }
                else if ((ary2[5] == "fastbuy") || (ary2[5] == "peibuycase1"))
                {
                    if ((uuposition > 0 && (hsi > (uuprice + 17))))
                    {
                        testbuy.testbs("2", code, hsi.ToString(), "1", "0:0:0:0:nowsell:CASE:+17");
                    }
                    else if ((uuposition > 0 && (Convert.ToInt32(uutime)) > totalsec) && (hsi > (uuprice + 10)))
                    {
                        testbuy.testbs("2", code, hsi.ToString(), "1", "0:0:0:0:nowsell:CASE:+10");
                    }

                }

                else if ((ary2[5] == "fastsell") || (ary2[5] == "peisellcase1"))
                {
                    if (uuposition < 0 && (hsi < (uuprice - 17)))
                    {
                        testbuy.testbs("1", code, hsi.ToString(), "1", "0:0:0:0:nowbuy:CASE:+17");
                    }
                    else if (uuposition < 0 && (hsi < (uuprice - 10)) && (Convert.ToInt32(uutime)) > totalsec)
                    {
                        testbuy.testbs("1", code, hsi.ToString(), "1", "0:0:0:0:nowbuy:CASE:+10");
                    }
                }
                else if ((uuposition > 0 && (hsi < (uuprice - 35))))
                {
                    testbuy.testbs("2", code, hsi.ToString(), "1", "0:0:0:0:nowsell:CASE:-35");
                }
                else if ((uuposition < 0 && (hsi > (uuprice + 35))))
                {
                    testbuy.testbs("1", code, hsi.ToString(), "1", "0:0:0:0:nowbuy:CASE:-35");
                }
                else if ((ary2[5] == "peisellcase2")|| (ary2[5] == "peisellcase1"))
                {
                    if (uuposition < 0 && (hsi < (uuprice -2)))
                    {
                        testbuy.testbs("1", code, hsi.ToString(), "1", "0:0:0:0:nowbuy:peibuycase2");
                    }
                }
                else if ((ary2[5] == "peibuycase2")|| (ary2[5] == "peibuycase1"))
                {
                    if (uuposition > 0 && (hsi > (uuprice + 2)))
                    {
                        testbuy.testbs("2", code, hsi.ToString(), "1", "0:0:0:0:nowbuy:peisellcase2");
                    }
                }
                else if ((ary2[5] == "CASE:-35"))
                {
                    if (uuposition < 0 && (hsi < (uuprice - 2)))
                    {
                        testbuy.testbs("1", code, hsi.ToString(), "1", "0:0:0:0:nowbuy:peibuycase2");
                    }
                }
                else if ((ary2[5] == "CASE:-35"))
                {
                    if (uuposition > 0 && (hsi > (uuprice + 2)))
                    {
                        testbuy.testbs("2", code, hsi.ToString(), "1", "0:0:0:0:nowbuy:peisellcase2");
                    }
                }
            }


            return 0;
        }
    }
}
