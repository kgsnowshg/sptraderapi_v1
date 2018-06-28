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
    class setting
    {
        public static int buy(int count, string code)
        {
            if (count == 0)
            {
                string jsonStrings = File.ReadAllText("setting/setting.json", Encoding.Default);
                JObject abc = JObject.Parse(jsonStrings);
                JToken price = abc["buy"][0]["price"];
                JToken operate = abc["buy"][0]["operate"];
                int ioperate = Convert.ToInt32(operate);
                int iprice = Convert.ToInt32(price);
                if (ioperate > 0)
                {
                    string jsonStrings2 = File.ReadAllText("setting/setting.json", Encoding.Default);
                    JObject abc2 = JObject.Parse(jsonStrings2);
                    abc2["buy"][0]["price"] = 0;
                    abc2["buy"][0]["operate"] = 0;
                    string updatestring2 = Convert.ToString(abc2);//将json装换为string
                    File.WriteAllText("setting/setting.json", updatestring2);//将内容写进jon文件中
                    Spapidll.AddOrder('B', iprice.ToString(), code, 1.ToString(), "0:0:0:0:0:selfbuy", 0.ToString());
                }
            }
            return 0;
        }
        public static int sell(int count, string code)
        {
            if (count == 0)
            {
                string jsonStrings = File.ReadAllText("setting/setting.json", Encoding.Default);
                JObject abc = JObject.Parse(jsonStrings);
                JToken price = abc["sell"][0]["price"];
                JToken operate = abc["sell"][0]["operate"];
                int ioperate = Convert.ToInt32(operate);
                int iprice = Convert.ToInt32(price);
                if (ioperate > 0)
                {
                    string jsonStrings2 = File.ReadAllText("setting/setting.json", Encoding.Default);
                    JObject abc2 = JObject.Parse(jsonStrings2);
                    abc2["sell"][0]["price"] = 0;
                    abc2["sell"][0]["operate"] = 0;
                    string updatestring2 = Convert.ToString(abc2);//将json装换为string
                    File.WriteAllText("setting/setting.json", updatestring2);//将内容写进jon文件中
                    Spapidll.AddOrder('S', iprice.ToString(), code, 1.ToString(), "0:0:0:0:0:selfsell", 0.ToString());
                }
            }
            return 0;
        }
        public static int delwait(int count, string code)
        {
            string jsonStrings = File.ReadAllText("setting/setting.json", Encoding.Default);
            JObject abc = JObject.Parse(jsonStrings);
            JToken operate = abc["delwait"][0]["operate"];
            int ioperate = Convert.ToInt32(operate);
            if (count > 0 && ioperate==1)
            {
                testbuy.testdel(code);
            }
            return 0;
        }
        public static int program()
        {
            string jsonStrings = File.ReadAllText("setting/setting.json", Encoding.Default);
            JObject abc = JObject.Parse(jsonStrings);
            JToken open = abc["program"][0]["open"];
            int iopen = Convert.ToInt32(open);
            return iopen;
        }
    }
}