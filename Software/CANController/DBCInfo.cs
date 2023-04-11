using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI;

namespace CANController
{
    internal class SignalInfo {
        public String SignalName;
        public String SignalType;
        public int SignalStartBit;
        public int SignalBitSize;
        public String SignalByteOrder;
        public String SignalValueType;
        public double Factor;
        public double Offset;
        public double Min;
        public double Max;
        public String Unit;
        public String Receiver;
    }
    internal class MessageInfo {
        public int MessageId;
        public string MessageName;
        public string MessageSize;
        public string Transmitter;
        public ArrayList messageDetail = new ArrayList();
        public SignalInfo stringToSignalInfo(String str)
        {
            SignalInfo signalInfo = new SignalInfo();
            String [] Words = str.Split(' ');
            signalInfo.SignalName = Words[1];

            int Index = 2;
            if (!Words[Index].Equals(":"))
            {
                signalInfo.SignalType = Words[Index];
                Index++;
            }
            Index++;

            String [] temp = Words[Index].Split(new char[2] {'|','@'});
            signalInfo.SignalStartBit = Convert.ToInt32(temp[0]);
            signalInfo.SignalBitSize = Convert.ToInt32(temp[1]);
            signalInfo.SignalByteOrder = "" + temp[2][0];
            signalInfo.SignalValueType = "" + temp[2][1];
            Index++;

            temp = Words[Index].Split(new char[3] { '(', ',', ')'});
            signalInfo.Factor = Convert.ToDouble(temp[1]);
            signalInfo.Offset = Convert.ToDouble(temp[2]);
            Index++;

            temp = Words[Index].Split(new char[3] { '[', '|', ']' });
            signalInfo.Min = Convert.ToDouble(temp[1]);
            signalInfo.Max = Convert.ToDouble(temp[2]);
            Index++;

            signalInfo.Unit = Words[Index].Replace("\"","");
            Index++;

            signalInfo.Receiver = Words[Index];
            return signalInfo;
        }
    }
    internal class DBCInfo
    {
        //Version
        public String VersionData;

        //NS_
        public String [] NewSymbols;

        //BS_
        public String Baudrate;

        //BU_
        public String [] NodeName;
        public String [] ValueTable;

        //BO_
        public ArrayList messageInfo = new ArrayList();

        //BA_DEF_
        public String [] BA_DEF_;

        //BA_DEF_DEF_
        public String [] BA_DEF_DEF_;

        //BA_
        public String[] BA_;

        //VAL_
        public String[] VAL_;

    }
}
