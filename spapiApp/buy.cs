using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spapiApp
{
    class buy
    {
        public static int case1(int hour, int min, int sec, double hsi)
        {
            int buy = 0;
            string cases = "";
            //第一個情況
            int action = 0;
            int[,] value = new int[11, 7];
            int totalsec = Convert.ToInt32(hour) * 60 * 60 + Convert.ToInt32(min) * 60 + Convert.ToInt32(sec);
            for (int i = 0; i < 10; i++)
            {
                int path = totalsec - (i * 2);
                string str = string.Join(" ", getsec.hsisec(path));
                string[] ary = str.Split(' ');
                value[i,0] = Convert.ToInt32(ary[0]);
                value[i,1] = Convert.ToInt32(ary[1]);
                value[i,2] = Convert.ToInt32(ary[2]);
                value[i,3] = Convert.ToInt32(ary[3]);
                value[i,4] = Convert.ToInt32(ary[4]);
                value[i,5] = Convert.ToInt32(ary[5]);
                value[i,6] = Convert.ToInt32(ary[6]);
                if (value[i, 6]>50)
                {
                    action = 1;
                }
            }
            if (action==1)
            {
                int paths = totalsec - 100;
                string str = string.Join(" ", getsec.hsisec(paths));
                string[] ary = str.Split(' ');
                value[11, 0] = Convert.ToInt32(ary[0]);
                if (hsi> (value[11, 0])+12)
                {
                    buy = 1;
                    cases = cases+"case1";
                }
            }

            
            //進行買入
            if (buy==1)
            {
                log.buy(Convert.ToInt32(hsi), cases);
            }
            return 0;
        }

        public static int case2(int buylow,int hsi)
        {
            if (buylow<=7)
            {
                string cases = "";
                buylow = buylow + 1;
                hsi = hsi + buylow;
                cases = "buylow";
                log.buy(hsi,cases);
            }
            else
            {
                string cases = "the buylow was exceed";
                log.error( cases);
            }
            return 0;
        }
    }
}
