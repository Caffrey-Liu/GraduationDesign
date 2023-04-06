using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace CANController
{
    internal class PanelMSG
    {
        public char ILETSET;
        public char OLETSET;
        public char TSETDR;
        public char TSETPA;
        public char BLWSET;
        public char RRDEF;
        public char FRDEF = '\0';
        public char ACON = '\0';
        public char ACMODE = '\0';

        public void getValueFromAirConditionInfo(AirConditionInfo airConditionInfo, char acon) {
            ILETSET = (char)airConditionInfo.INLETDIR;
            OLETSET = (char)airConditionInfo.OUTLETDIR;
            TSETDR = (char)12;
            TSETPA = (char)12;
            BLWSET = (char)airConditionInfo.BLMLVL;
            RRDEF = (char)airConditionInfo.RRDEFSTATUS;
            FRDEF = '0';
            ACON = acon;
            ACMODE = '0';
        }

        public string getCANFrameData() {
            int c1 = (((char)(OLETSET << 4)) & 0b01110000) + ILETSET;
            int c2 = TSETDR;
            int c3 = TSETPA;
            int c4 = ((((ACMODE << 7) & 0b10000000) + ((ACON << 6) & 0b01000000) + ((FRDEF << 5) & 0b00100000) + ((RRDEF << 4) & 0b00010000)) + (BLWSET & 0b00001111));
            return c1.ToString("X2") + " " + c2.ToString("X2") + " " + c3.ToString("X2") + " " + c4.ToString("X2");
        }
    }
}
