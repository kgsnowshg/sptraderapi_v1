using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
namespace spapiApp
{
    class url
    {
        public static string geturlresult()
        {
            string url = "http://www.etnet.com.hk/www/tc/stocks/indexes_detail.php?subtype=hsi";
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);

            try
            {
                HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                WebResponse myResponse = httpRequest.GetResponse();

                //Streamreader讀取回覆
                StreamReader sr = new StreamReader(myResponse.GetResponseStream());

                //將全文轉成string
                string result = sr.ReadToEnd();


                //關掉StreamReader
                sr.Close();
                result = result.Replace("\n", "");
                result = result.Replace("\"", "<");
                result = result.Replace(",", "");
                string patterns = @"WarrantQuoteFigureArrow<>.*?<";//匹配模式
                Regex regexs = new Regex(patterns, RegexOptions.IgnoreCase);
                MatchCollection matches2 = regexs.Matches(result);
                StringBuilder sbs = new StringBuilder();//存放匹配结果
                foreach (Match match in matches2)
                {
                    string value = match.Value;
                    sbs.AppendLine(value);
                }
                httpResponse.Close();

                string results = sbs.ToString();
                results = results.Replace("WarrantQuoteFigureArrow<>", "");
                results = results.Replace("<", "");
                string resultss = "0";
                System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[A-Za-z]+$");
                bool s = reg1.IsMatch(results);
                if (s == true)
                {
                    Double valuess = Math.Round(Convert.ToDouble(results), 0);
                    resultss = valuess.ToString();
                }
                else
                {
                    resultss = "0";
                }
                return resultss;

            }

            catch (WebException)
            {
                return "0";
            }
        }
    }
}
