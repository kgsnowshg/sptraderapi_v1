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
    class realposition
    {
        public static int updateposition(int position)
        {
            string update = File.ReadAllText("real/position.json", Encoding.Default);
            JObject updates = JObject.Parse(update);
            updates["user"][0]["position"] = position;
            string updatestring = Convert.ToString(updates);//将json装换为string
            File.WriteAllText("real/position.json", updatestring);//将内容写进jon文件中
            return 0;
        }


        public static int updaterecord(int position)
        {
            string jsonStrings = File.ReadAllText("real/position.json", Encoding.Default);
            JObject abc = JObject.Parse(jsonStrings);
            JToken positiontoken = abc["user"][0]["position"];
            int positions = Convert.ToInt32(positiontoken);
            if (positions!=0)
            {
                string bs = "0";
                if (positions > 0)
                {
                    bs = "B";
                }
                if (positions < 0)
                {
                    bs = "S";
                }
                string result= string.Join("|", Spapidll.LastGetTrade());
                string[] ary = result.Split('|');
                if (ary[0]=="1")
                {
                    string update = File.ReadAllText("real/position.json", Encoding.Default);
                    JObject updates = JObject.Parse(update);
                    updates["user"][0]["id"] = ary[1];
                    updates["user"][0]["BS"] = bs;
                    updates["user"][0]["price"] = ary[3];
                    updates["user"][0]["cases"] = ary[4];
                    string updatestring = Convert.ToString(updates);//将json装换为string
                    File.WriteAllText("real/position.json", updatestring);//将内容写进jon文件中
                }
            }
            return 0;
        }
        public static int recordlasttrade()
        {
            string result = string.Join("|", Spapidll.LastGetTrade());
            string[] ary = result.Split('|');
            DateTime dt = DateTime.Now;
            int hour = Convert.ToInt32(dt.Hour.ToString());//13
            int min = Convert.ToInt32(dt.Minute.ToString());//13
            int sec = Convert.ToInt32(dt.Second.ToString());//13
            int totalsec = Convert.ToInt32(hour) * 60 * 60 + Convert.ToInt32(min) * 60 + Convert.ToInt32(sec);
            if (ary[0] == "1")
            {
                string update = File.ReadAllText("real/recordlasttrade.json", Encoding.Default);
                JObject updates = JObject.Parse(update);
                updates["user"][0]["id"] = ary[1];
                updates["user"][0]["price"] = ary[3];
                updates["user"][0]["cases"] = ary[4];
                updates["user"][0]["time"] = totalsec;
                string updatestring = Convert.ToString(updates);//将json装换为string
                File.WriteAllText("real/recordlasttrade.json", updatestring);//将内容写进jon文件中
            }
            return 0;
        }
        public static int delmorewait(int count,string code)
        {
            if (count > 1)
            {
                testbuy.testdel(code);
            }
            return 0;
        }
    }
}
