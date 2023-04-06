using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CANController
{
    public class AirConditionInfo
    {
        public double TAM;
        public double TR;
        public double TE;
        public double RH;
        public double TS;

        public int BLMLVL;
        public double AIRMIXDR;
        public double AIRMIXPA;
        public double INLET;
        public double OUTLET;
        public int INLETDIR;
        public int OUTLETDIR;
        public int RRDEFSTATUS;

        public int POWER = 0;

        #region MSG01属性转化方法
        public void STR_TAM(String str) {
            TAM = -30.8 + 0.32 * System.Convert.ToInt32("0x" + str, 16);
        }
        public void STR_TR(String str)
        {
            TR = -6.5 + 0.25 * System.Convert.ToInt32("0x" + str, 16);
        }
        public void STR_TE(String str)
        {
            TE = -30.0 + 0.1 * System.Convert.ToInt32("0x" + str, 16);
        }
        public void STR_RH(String str)
        {
            RH = 0.1 + 0.0016 * System.Convert.ToInt32("0x" + str, 16);
        }
        public void STR_TS(String str)
        {
            TS = 17.5 * System.Convert.ToInt32("0x" + str, 16);
        }
        #endregion

        #region MSG02属性转化方法
        public void STR_BLMLVL(String str){
            BLMLVL = System.Convert.ToInt32("0x" + str, 16);
        }
        public void STR_AIRMIXDR(String str)
        {
            AIRMIXDR = -0.14 + 0.005 * System.Convert.ToInt32("0x" + str, 16);
        }
        public void STR_AIRMIXPA(String str)
        {
            AIRMIXPA = -0.14 + 0.005 * System.Convert.ToInt32("0x" + str, 16);
        }
        public void STR_INLET(String str)
        {
            INLET = -0.14 + 0.005 * System.Convert.ToInt32("0x" + str, 16);
        }
        public void STR_OUTLET(String str)
        {
            OUTLET = -0.14 + 0.005 * System.Convert.ToInt32("0x" + str, 16);
        }
        public void STR_INLETDIR(String str)
        {
            INLETDIR = System.Convert.ToInt32("0x" + str, 16) & 0b00000001;
        }
        public void STR_OUTLETDIR(String str)
        {
            OUTLETDIR = System.Convert.ToInt32("0x" + str, 16) & 0b00000111;
        }
        public void STR_RRDEFSTATUS(String str)
        {
            RRDEFSTATUS = System.Convert.ToInt32("0x" + str, 16) & 0b00000001;
        }
        #endregion
    }
}
