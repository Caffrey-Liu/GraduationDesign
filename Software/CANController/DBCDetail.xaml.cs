using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace CANController
{
    /// <summary>
    /// DBCDetail.xaml 的交互逻辑
    /// </summary>
    public partial class DBCDetail : Window
    {
        public DBCDetail()
        {
            InitializeComponent();
        }

        private void DBC_Drop(object sender, DragEventArgs e)
        {
            String fileName = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            //Console.WriteLine(fileName);
            string Data = File.ReadAllText(fileName);
            AnalyDBCData(Data);
            //Console.WriteLine(Data);
        }

        private void DBC_DrapEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Link;
            }
            else {
                e.Effects = DragDropEffects.None;
            }
        }

        DBCInfo dbcInfo = new DBCInfo();
        HashSet<string> keyWords = new HashSet<string>() {"VERSION", "NS_", "BS_:", "BU_:", "BO_", "BA_DEF_", "BA_DEF_DEF_", "BA_", "VAL_"};
        private void AnalyDBCData(String Data) {
            String[] line = Data.Split('\n');
            //Console.WriteLine(line.Length);
            String newLine = "" + (char)13;
            #region 文本解析
            for (int i = 0; i < line.Length; i++) {
                //去除空行和换行
                if (line[i].Equals("") || line[i].Equals(newLine)) continue;
                String[] Words = line[i].Split(' ');

                //Version
                if (Words[0].Equals("VERSION")) dbcInfo.VersionData = Words[1];

                //NS_
                if (Words[0].Equals("NS_")) {
                    i++;
                    int StartIndex = i;
                    int num = 0;
                    while (!(line[i].Equals("") || line[i].Equals(newLine))) {
                        i++;
                        num++;
                    }
                    dbcInfo.NewSymbols = line.Skip(StartIndex).Take(num).ToArray();
                }
                //Console.WriteLine(dbcInfo.NewSymbols.Length);
                //for (int i = 0; i < dbcInfo.NewSymbols.Length; i++)
                //{
                //    Console.WriteLine(dbcInfo.NewSymbols[i]);
                //}

                //BS_:
                if (Words[0].Equals("BS_")) {
                    if (Words.Length > 1) dbcInfo.Baudrate = Words[1];
                    else dbcInfo.Baudrate = "";
                }

                //BU_:
                if (Words[0].Equals("BU_")) {
                    dbcInfo.NodeName = Words.Skip(1).ToArray();
                    i++;
                    int StartIndex = i;
                    int num = 0;
                    while (!(line[i].Equals("") || line[i].Equals(newLine)))
                    {
                        i++;
                        num++;
                    }
                    dbcInfo.ValueTable = line.Skip(StartIndex).Take(num).ToArray();
                }

                //BO_
                if (Words[0].Equals("BO_"))
                {
                    MessageInfo messageInfo = new MessageInfo();
                    messageInfo.MessageId = System.Convert.ToInt32("0x" + Words[1], 16);
                    messageInfo.MessageName = Words[2].Substring(0, Words[2].Length - 1);
                    messageInfo.MessageSize = Words[3];
                    messageInfo.Transmitter = Words[4];
                    i++;
                    while (!(line[i].Equals("") || line[i].Equals(newLine)))
                    {
                        messageInfo.messageDetail.Add(messageInfo.stringToSignalInfo(line[i]));
                        i++;
                    }
                    dbcInfo.messageInfo.Add(messageInfo);
                    //Console.WriteLine(dbcInfo.messageInfo.Count);
                }

                //BA_DEF_
                if (Words[0].Equals("BA_DEF_")) {
                    int StartIndex = i;
                    int num = 0;
                    while (Words[0].Equals("BA_DEF_"))
                    {
                        i++;
                        num++;
                        Words = line[i].Split(' ');
                    }
                    dbcInfo.BA_DEF_ = line.Skip(StartIndex).Take(num).ToArray();
                }

                //BA_DEF_DEF_
                if (Words[0].Equals("BA_DEF_DEF_"))
                {
                    int StartIndex = i;
                    int num = 0;
                    while (Words[0].Equals("BA_DEF_DEF_"))
                    {
                        i++;
                        num++;
                        Words = line[i].Split(' ');
                    }
                    dbcInfo.BA_DEF_DEF_ = line.Skip(StartIndex).Take(num).ToArray();
                }

                //BA_
                if (Words[0].Equals("BA_"))
                {
                    int StartIndex = i;
                    int num = 0;
                    while (Words[0].Equals("BA_"))
                    {
                        i++;
                        num++;
                        Words = line[i].Split(' ');
                    }
                    dbcInfo.BA_ = line.Skip(StartIndex).Take(num).ToArray();
                }

                //VAL_
                if (Words[0].Equals("VAL_"))
                {
                    int StartIndex = i;
                    int num = 0;
                    while (Words[0].Equals("VAL_"))
                    {
                        i++;
                        num++;
                        Words = line[i].Split(' ');
                    }
                    dbcInfo.VAL_ = line.Skip(StartIndex).Take(num).ToArray();
                }
            }
            #endregion
        }

        public string getProperties<T>(T t)
        {
            string tStr = string.Empty;
            if (t == null)
            {
                return tStr;
            }
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return tStr;
            }
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    tStr += string.Format("{0}:{1},", name, value);
                }
                else
                {
                    getProperties(value);
                }
            }
            return tStr;
        }
    }
}
