using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;//IsoDateTimeConverter
namespace spapiApp
{
    class hsia
    {
        public static int min(int hour, int min, int sec, int hsi)
        {
            int totalsec = Convert.ToInt32(hour) * 60 * 60 + Convert.ToInt32(min) * 60 + Convert.ToInt32(sec);

            string jsonString = File.ReadAllText("json/hsia.json", Encoding.Default);
            JObject abcs = JObject.Parse(jsonString);
            JToken lowtoken = abcs[hour.ToString()][Convert.ToInt32(min)]["low"];
            JToken hightoken = abcs[hour.ToString()][Convert.ToInt32(min)]["high"];
            abcs[hour.ToString()][Convert.ToInt32(min)]["now"] = hsi;
            double low = Convert.ToDouble(lowtoken);
            double high = Convert.ToDouble(hightoken);
            if (hsi > high)
            {
                abcs[hour.ToString()][Convert.ToInt32(min)]["high"] = hsi;
            }
            if (hsi < low)
            {
                abcs[hour.ToString()][Convert.ToInt32(min)]["low"] = hsi;
            }
            string convertString = Convert.ToString(abcs);//将json装换为string
            File.WriteAllText("json/hsia.json", convertString);//将内容写进jon文件中

            string jsonString2 = File.ReadAllText("sechsiajson/" + totalsec + ".json", Encoding.Default);
            JObject abcs2 = JObject.Parse(jsonString2);
            abcs2[totalsec.ToString()][0]["sec"] = hsi;

            string convertString2 = Convert.ToString(abcs2);//将json装换为string
            File.WriteAllText("sechsiajson/" + totalsec + ".json", convertString2);//将内容写进jon文件中

            return 0;
        }
    }
}
