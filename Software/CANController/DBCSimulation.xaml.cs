using CAN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CANController
{
    /// <summary>
    /// DBCSimulation.xaml 的交互逻辑
    /// </summary>
    public partial class DBCSimulation : Window, INotifyPropertyChanged
    {
        public DBCSimulation()
        {
            InitializeComponent();
        }

        public DBCInfo dbcInfo = new DBCInfo();

        public void Can_ReceviedData(object sender, CANFrameInfoArgs e)
        {
            FrameInfo message = e.CanFrameInfo;
            //CurrentProgress += "接收时间：" + DateTime.Now.ToString() + "   帧ID： " + message.FrameID + "   帧格式：" + message.FrameFormat + "   " + message.FrameType + "   数据长度：" + message.Data.Length / 2 + "   帧数据：";
            //for (int i = 0; i < message.Data.Length; i = i + 2)
            //{
            //    CurrentProgress += "" + message.Data[i] + message.Data[i + 1] + " ";
            //}
            RefreshData(message);
        }

        Dictionary<string, double> signal_values = new Dictionary<string, double>();
        private void RefreshData(FrameInfo message)
        {
            MessageInfo messageInfo = null;
            //根据ID找对应报文名字
            foreach (MessageInfo temp in dbcInfo.messageInfo) {
                //Console.WriteLine("FrameID = " + message.FrameID + "   MessageID = " + temp.MessageId);
                if (temp.MessageId.Equals(message.FrameID)){
                    messageInfo = temp;
                    break;
                }
            }
            Console.WriteLine(message.Data);
            String binary_values = convertHexToBitArray(message.Data);
            #region 输出binary_values检查
            //int count = 0;
            //for (int i = 0; i < binary_values.Length; i++)
            //{
            //    if (count == 8)
            //    {
            //        Console.WriteLine();
            //        count = 0;
            //    }
            //    Console.Write(binary_values[i] + " ");
            //    count++;
            //}
            //Console.WriteLine();
            //Console.WriteLine();
            #endregion
            if (messageInfo != null)
            {
                foreach (SignalInfo temp in messageInfo.SignalInfo)
                {
                    if (temp.SignalByteOrder.Equals("1"))
                    {
                        String str = Reverse(binary_values.Substring(temp.SignalStartBit, temp.SignalBitSize));
                        double num = temp.Factor * Convert.ToInt32(str, 2) + temp.Offset;
                        //Console.WriteLine(temp.SignalName + " = " + num);
                        if (signal_values.ContainsKey(temp.SignalName))
                        {
                            signal_values[temp.SignalName] = num;
                        }
                        else 
                        { 
                            signal_values.Add(temp.SignalName, num); 
                        }
                    }
                }
            }
        }

        public string convertHexToBitArray(string hexData)
        {
            string binary_values = "";
            for (int i = 0; i < hexData.Length; i = i + 2) {
                binary_values = binary_values + Reverse(convertHexCharToBit(hexData[i + 1]));
                binary_values = binary_values + Reverse(convertHexCharToBit(hexData[i]));
            }
            return binary_values;
        }

        static string Reverse(string text)
        {
            char[] charArray = text.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private String convertHexCharToBit(char temp)
        {
            if (temp == '0') return "0000";
            if (temp == '1') return "0001";
            if (temp == '2') return "0010";
            if (temp == '3') return "0011";
            if (temp == '4') return "0100";
            if (temp == '5') return "0101";
            if (temp == '6') return "0110";
            if (temp == '7') return "0111";
            if (temp == '8') return "1000";
            if (temp == '9') return "1001";
            if (temp == 'A') return "1010";
            if (temp == 'B') return "1011";
            if (temp == 'C') return "1100";
            if (temp == 'D') return "1101";
            if (temp == 'E') return "1110";
            if (temp == 'F') return "1111";
            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public void Draw()
        {
            Panel.Children.Clear();
            //绘画消息框
            ObservableCollection<MessageInfo> messageInfo = dbcInfo.messageInfo;
            foreach (MessageInfo msg in messageInfo) {
                BrushConverter brushConverter = new BrushConverter();

                Grid grid = new Grid();
                grid.Name = msg.MessageName;
                TextBlock MSGtextBlock = new TextBlock();
                MSGtextBlock.Text = msg.MessageName;
                MSGtextBlock.FontSize = 14;
                MSGtextBlock.Foreground = (Brush)brushConverter.ConvertFromString("#FFFFFFFF");
                MSGtextBlock.FontWeight = FontWeights.Bold;
                MSGtextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                MSGtextBlock.VerticalAlignment = VerticalAlignment.Center;
                MSGtextBlock.Background = (Brush)brushConverter.ConvertFromString("#FFe67e22");
                grid.Children.Add(MSGtextBlock);

                Canvas canvas = new Canvas();
                canvas.Height = 400;
                canvas.Background = (Brush)brushConverter.ConvertFromString("#FFecf0f1");
                canvas.Name = msg.MessageName + "Canvas";
                int canvasleft = 10;
                int canvastop = 10;
                //绘画信号框
                foreach (SignalInfo sig in msg.SignalInfo) {
                    Grid siggrid = new Grid();
                    siggrid.Height = 100;
                    siggrid.Width = 100;
                    siggrid.Name = sig.SignalName;
                    siggrid.MouseLeftButtonDown += SIGGrid_MouseLeftButtonDown; ;
                    siggrid.RowDefinitions.Add(new RowDefinition());
                    siggrid.RowDefinitions.Add(new RowDefinition());

                    TextBlock SIGtextBlock = new TextBlock();
                    SIGtextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    SIGtextBlock.VerticalAlignment = VerticalAlignment.Center;
                    SIGtextBlock.Text = sig.SignalName;
                    SIGtextBlock.FontSize = 9;
                    SIGtextBlock.Margin = new Thickness(5);
                    Grid.SetRow(SIGtextBlock,0);
                    //添加信号对应控件
                    TextBox textBox = new TextBox();
                    textBox.Name = sig.SignalName;
                    //绑定
                    Binding binding = new Binding();


                    textBox.Margin = new Thickness(5);
                    Grid.SetRow(textBox,1);

                    siggrid.Children.Add(SIGtextBlock);
                    siggrid.Children.Add(textBox);
                    Canvas.SetLeft(siggrid, canvasleft);
                    Canvas.SetTop(siggrid, canvastop);
                    canvasleft += 30;
                    canvastop += 30;
                    canvas.Children.Add(siggrid);
                }
                Panel.Children.Add(grid);
                Panel.Children.Add(canvas);
            }
        }

        Grid SelectedGrid;
        private void SIGGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock;
            if (e.Source.GetType() == typeof(TextBlock))
            {
                textBlock = (TextBlock)e.Source;
                SelectedGrid = (Grid)VisualTreeHelper.GetParent(textBlock);
            }
            else return;
            //Console.WriteLine("点击了" + SelectedGrid.Name +"控件");
            SelectedGrid.MouseMove += SIGGrid_MouseMove;
            SelectedGrid.MouseLeftButtonUp += SIGGrid_MouseLeftButtonUp;
        }

        private void SIGGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SelectedGrid.Opacity = 1;
            SelectedGrid.MouseMove -= SIGGrid_MouseMove;
            SelectedGrid.MouseLeftButtonUp -= SIGGrid_MouseLeftButtonUp;
        }

        private void SIGGrid_MouseMove(object sender, MouseEventArgs e)
        {
            Point point = e.GetPosition((Canvas)VisualTreeHelper.GetParent(SelectedGrid));
            //Console.WriteLine("   X = " + point.X + "    Y = " + point.Y);
            SelectedGrid.Opacity = 0.5;
            Canvas.SetLeft(SelectedGrid, point.X - SelectedGrid.Height/4);
            Canvas.SetTop(SelectedGrid, point.Y - SelectedGrid.Width/4);
        }
    }
}
