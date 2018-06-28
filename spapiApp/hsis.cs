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
    class hsis
    {
        public static int min(int hour, int min, int sec, double open, double last, double qty, double bid1, double bid2, double bid3, double bid4, double bid5, double qbid1, double qbid2, double qbid3, double qbid4, double qbid5, double ask1, double ask2, double ask3, double ask4, double ask5, double qask1, double qask2, double qask3, double qask4, double qask5)
        {
            int totalsec = Convert.ToInt32(hour) * 60 * 60 + Convert.ToInt32(min) * 60 + Convert.ToInt32(sec);
            double area = last - open;
            area= Convert.ToInt16(Math.Ceiling(area / 15));


            string jsonString = File.ReadAllText("json/hsi.json", Encoding.Default);
            JObject abcs = JObject.Parse(jsonString);
            JToken lowtoken = abcs[hour.ToString()][Convert.ToInt32(min)]["low"];
            JToken hightoken = abcs[hour.ToString()][Convert.ToInt32(min)]["high"];
            abcs[hour.ToString()][Convert.ToInt32(min)]["now"] = last;
            abcs[hour.ToString()][Convert.ToInt32(min)]["areanow"] = area;
            abcs[hour.ToString()][Convert.ToInt32(min)]["qty"] = qty;
            double low = Convert.ToDouble(lowtoken);
            double high = Convert.ToDouble(hightoken);
            if (last > high)
            {
                abcs[hour.ToString()][Convert.ToInt32(min)]["high"] = last;
                abcs[hour.ToString()][Convert.ToInt32(min)]["areahigh"] = area;
            }
            if (last < low)
            {
                abcs[hour.ToString()][Convert.ToInt32(min)]["low"] = last;
                abcs[hour.ToString()][Convert.ToInt32(min)]["arealow"] = area;
            }
            string convertString = Convert.ToString(abcs);//将json装换为string
            File.WriteAllText("json/hsi.json", convertString);//将内容写进jon文件中

            string jsonString2 = File.ReadAllText("secjson/" + totalsec + ".json", Encoding.Default);
            JObject abcs2 = JObject.Parse(jsonString2);
            JToken lowtokens = abcs2[totalsec.ToString()][0]["low"];
            JToken hightokens = abcs2[totalsec.ToString()][0]["high"];
            double lows = Convert.ToDouble(lowtokens);
            double highs = Convert.ToDouble(hightokens);
            if (last > highs)
            {
                abcs2[totalsec.ToString()][0]["high"] = last;
            }
            if (last < lows)
            {
                abcs2[totalsec.ToString()][0]["low"] = last;
            }
            abcs2[totalsec.ToString()][0]["sec"] = last;
            abcs2[totalsec.ToString()][0]["qty"] = qty;
            abcs2[totalsec.ToString()][0]["bid1"] = bid1;
            abcs2[totalsec.ToString()][0]["bid2"] = bid2;
            abcs2[totalsec.ToString()][0]["bid3"] = bid3;
            abcs2[totalsec.ToString()][0]["bid4"] = bid4;
            abcs2[totalsec.ToString()][0]["bid5"] = bid5;
            abcs2[totalsec.ToString()][0]["qbid1"] = qbid1;
            abcs2[totalsec.ToString()][0]["qbid2"] = qbid2;
            abcs2[totalsec.ToString()][0]["qbid3"] = qbid3;
            abcs2[totalsec.ToString()][0]["qbid4"] = qbid4;
            abcs2[totalsec.ToString()][0]["qbid5"] = qbid5;
            abcs2[totalsec.ToString()][0]["ask1"] = ask1;
            abcs2[totalsec.ToString()][0]["ask2"] = ask2;
            abcs2[totalsec.ToString()][0]["ask3"] = ask3;
            abcs2[totalsec.ToString()][0]["ask4"] = ask4;
            abcs2[totalsec.ToString()][0]["ask5"] = ask5;
            abcs2[totalsec.ToString()][0]["qask1"] = qask1;
            abcs2[totalsec.ToString()][0]["qask2"] = qask2;
            abcs2[totalsec.ToString()][0]["qask3"] = qask3;
            abcs2[totalsec.ToString()][0]["qask4"] = qask4;
            abcs2[totalsec.ToString()][0]["qask5"] = qask5;

            string convertString2 = Convert.ToString(abcs2);//将json装换为string
            File.WriteAllText("secjson/" + totalsec + ".json", convertString2);//将内容写进jon文件中

            return 0;
        }
    }
}
