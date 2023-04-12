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
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections;

namespace CANController
{
    /// <summary>
    /// DBCDetail.xaml 的交互逻辑
    /// </summary>
    public partial class DBCDetail : Window, INotifyPropertyChanged
    {
        public DBCDetail()
        {
            InitializeComponent();

            datagrid.DataContext = this;
        }

        #region 定义各种面板变量,实现接口
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        private String _fileName;
        public String fileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                OnPropertyChanged("fileName");
            }
        }
        public String Baudrate
        {
            get { return dbcInfo.Baudrate; }
            set
            {
                OnPropertyChanged("Baudrate");
            }
        }

        private ObservableCollection<MessageInfo> _MessageInfo = new ObservableCollection<MessageInfo>();
        public ObservableCollection<MessageInfo> MessageInfo 
        {
            get { return _MessageInfo; }
            set
            {
                _MessageInfo = value;
                OnPropertyChanged("MessageInfo");
            }
        }

        private ObservableCollection<String> _MessageName = new ObservableCollection<String>();
        public ObservableCollection<String> MessageName
        {
            get { return _MessageName; }
            set
            {
                _MessageName = value;
                OnPropertyChanged("MessageName");
            }
        }

        #endregion
        private void RefreshData()
        {
            Baudrate = dbcInfo.Baudrate;
            for (int i = 1; i <= dbcInfo.messageInfo.Count; i++) {
                MessageName.Add(dbcInfo.messageInfo[i].MessageName);
                MessageInfo.Add(dbcInfo.messageInfo[i]);
            }
        }
        private void DBC_Drop(object sender, DragEventArgs e)
        {
            String filePath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            //Console.WriteLine(filePath);
            fileName = filePath.Split(new char[2] { '\\', '.' })[filePath.Split(new char[2]{ '\\','.'}).Length - 2];
            //Console.WriteLine(fileName);
            string Data = File.ReadAllText(filePath);
            AnalyDBCData(Data);
            RefreshData();
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
        int MessageCount = 0;
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
                    //Console.WriteLine("String ID = " + Words[1]);
                    messageInfo.MessageId = System.Convert.ToInt32(Words[1]).ToString("X3");
                    messageInfo.MessageName = Words[2].Substring(0, Words[2].Length - 1);
                    messageInfo.MessageSize = Words[3];
                    messageInfo.Transmitter = Words[4];
                    i++;
                    int SignalCount = 0;
                    while (!(line[i].Equals("") || line[i].Equals(newLine)))
                    {
                        SignalCount++;
                        messageInfo.messageDetail.Add(SignalCount,messageInfo.stringToSignalInfo(line[i]));
                        i++;
                    }
                    MessageCount++;
                    dbcInfo.messageInfo.Add(MessageCount,messageInfo);
                    //Console.WriteLine(dbcInfo.messageInfo[MessageCount].MessageId); 
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
    }
}
