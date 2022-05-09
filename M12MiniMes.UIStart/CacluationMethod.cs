using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M12MiniMes.UIStart
{
   public static class CacluationMethod
    {
        public static string GetHMS(int time)
        {
            int H = (int)(time/3600);
            int H1= (time%3600);
            int M= (int)(H1/60);
            int M1 = (int)(H1%60);
            int S = (int)M1;
            string str =H.ToString()+":"+M.ToString()+":"+S.ToString();
            return str;
        }

    }
}
