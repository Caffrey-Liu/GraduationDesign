using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI;

namespace CANController
{
    public class SignalInfo {
        public String SignalName { get; set; }
        public String SignalType { get; set; }
        public int SignalStartBit { get; set; }
        public int SignalBitSize { get; set; }
        public String SignalByteOrder { get; set; }
        public String SignalValueType { get; set; }
        public double Factor { get; set; }
        public double Offset { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public String Unit { get; set; }
        public String Receiver { get; set; }
    }
    public class MessageInfo {
        public String MessageId { get; set; }
        public String MessageName { get; set; }
        public String MessageSize { get; set; }
        public String Transmitter { get; set; }
        public ObservableCollection<SignalInfo> _SignalInfo = new ObservableCollection<SignalInfo>();
        public ObservableCollection<SignalInfo> SignalInfo
        {
            get { return _SignalInfo; }
            set { _SignalInfo = value; }
        }
        public SignalInfo stringToSignalInfo(String str)
        {
            SignalInfo signalInfo = new SignalInfo();
            Regex.Replace(str, "\\s{2,}", " ");
            String [] Words = str.Split(' ');
            signalInfo.SignalName = Words[2];

            int Index = 3;
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
    public class DBCInfo
    {
        //Version
        public String VersionData { get; set; }

        //NS_
        public String[] NewSymbols { get; set; }

        //BS_
        public String Baudrate { get; set; } = "9500";

        //BU_
        public String [] NodeName { get; set; }
        public String [] ValueTable { get; set; }

        //BO_
        public ObservableCollection<MessageInfo> _messageInfo = new ObservableCollection<MessageInfo>();
        public ObservableCollection<MessageInfo> messageInfo {
            get { return _messageInfo; }
            set { _messageInfo = value; }
        }

        //BA_DEF_
        public String [] BA_DEF_ { get; set; }

        //BA_DEF_DEF_
        public String [] BA_DEF_DEF_ { get; set; }

        //BA_
        public String[] BA_ { get; set; }

        //VAL_
        public String[] VAL_ { get; set; }

    }
}
