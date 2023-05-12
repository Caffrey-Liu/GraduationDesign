using CAN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CANController
{
    /// <summary>
    /// DBCSimulation.xaml 的交互逻辑
    /// </summary>
    public partial class DBCSimulation : Window
    {
        public DBCInfo dbcInfo = new DBCInfo();
        public DBCSimulation()
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            InitializeComponent();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        public void Can_ReceviedData(object sender, CANFrameInfoArgs e)
        {
            FrameInfo message = e.CanFrameInfo;
            RefreshData(message);
        }

        Dictionary<string, Data> signal_values = new Dictionary<string, Data>();
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
            //Console.WriteLine(message.Data);
            
            if (messageInfo != null)
            {
                foreach (SignalInfo temp in messageInfo.SignalInfo)
                {
                    //intel序
                    if (temp.SignalByteOrder.Equals("1"))
                    {
                        String binary_values_intel = convertHexToBitArray(message.Data,1);
                        String str = "";
                        if (temp.SignalBitSize <= 8)
                            str = Reverse(binary_values_intel.Substring(temp.SignalStartBit, temp.SignalBitSize));
                        else {
                            int StartBit = temp.SignalStartBit;
                            int BitSize = temp.SignalBitSize;
                            while (BitSize >= 8) {
                                int number = 8 - (StartBit % 8);
                                str = str + Reverse(binary_values_intel.Substring(StartBit, number));
                                BitSize = BitSize - number;
                                StartBit = StartBit + number;
                            }
                            str = str + Reverse(binary_values_intel.Substring(StartBit, BitSize));
                        }
                        //if (temp.SignalName.Equals("TE"))
                        //    Console.WriteLine(str);
                        double num = temp.Factor * Convert.ToInt32(str, 2) + temp.Offset;
                        if (signal_values.ContainsKey(temp.SignalName))
                        {
                            signal_values[temp.SignalName].data = num;
                        }
                        else 
                        { 
                            Data data = new Data();
                            data.setType(temp.SignalBitSize);
                            data.data = num;
                            data.Max = temp.Max;
                            data.Min = temp.Min;
                            signal_values.Add(temp.SignalName, data); 
                        }
                        if (dataDraw.ContainsKey(temp.SignalName)) dataDraw[temp.SignalName].Modes.Refresh(num); 
                    }
                    //motorola序
                    if (temp.SignalByteOrder.Equals("0")) 
                    {
                        String binary_values_motorola = convertHexToBitArray(message.Data, 0);
                        #region 输出binary_values检查
                        //int count = 0;
                        //for (int i = 0; i < binary_values_motorola.Length; i++)
                        //{
                        //    if (count == 8)
                        //    {
                        //        Console.WriteLine();
                        //        count = 0;
                        //    }
                        //    Console.Write(binary_values_motorola[i] + " ");
                        //    count++;
                        //}
                        //Console.WriteLine();
                        //Console.WriteLine();
                        #endregion
                        int start = (1 + (temp.SignalStartBit / 8) * 2) * 8 -1 - temp.SignalStartBit;
                        String str = binary_values_motorola.Substring(start, temp.SignalBitSize);
                        double num = temp.Factor * Convert.ToInt32(str, 2) + temp.Offset;
                        if (signal_values.ContainsKey(temp.SignalName))
                        {
                            signal_values[temp.SignalName].data = num;
                        }
                        else
                        {
                            Data data = new Data();
                            data.setType(temp.SignalBitSize);
                            data.data = num;
                            data.Max = temp.Max;
                            data.Min = temp.Min;
                            signal_values.Add(temp.SignalName, data);
                        }
                        if (dataDraw.ContainsKey(temp.SignalName)) dataDraw[temp.SignalName].Modes.Refresh(num);
                    }
                }
            }
        }

        public string convertHexToBitArray(string hexData,int order)
        {
            string binary_values = "";
            if (order == 1)
            {
                for (int i = 0; i < hexData.Length; i = i + 2)
                {
                    binary_values = binary_values + Reverse(convertHexCharToBit(hexData[i + 1]));
                    binary_values = binary_values + Reverse(convertHexCharToBit(hexData[i]));
                }
            }
            else 
            {
                for (int i = 0; i < hexData.Length; i = i + 1)
                {
                    binary_values = binary_values + convertHexCharToBit(hexData[i]);
                }
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

        public class Data : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public int type;
            public double Max;
            public double Min;

            private double _data;
            public double data
            {
                get
                {
                    return _data;
                }
                set
                {
                    _data = value;
                    setImg(value);
                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("data"));
                    }
                }
            }

            private string _image;
            public string image
            {
                get
                {
                    return _image;
                }
                set
                {
                    _image = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("image"));
                    }
                }
            }

            void setImg(double num) {
                if (type == 0) {
                    if ((int)num == 0) {
                        image = "./Image/switch0.png";
                    }
                    if ((int)num == 1) {
                        image = "./Image/switch1.png";
                    }
                }
                if (type == 1) {
                    if ((int)num == 0) { image = "./Image/state1.png";}
                    if ((int)num == 1) { image = "./Image/state2.png"; }
                    if ((int)num == 2) { image = "./Image/state3.png"; }
                    if ((int)num == 3) { image = "./Image/state4.png"; }
                    if ((int)num == 4) { image = "./Image/state5.png"; }
                    if ((int)num == 5) { image = "./Image/state6.png"; }
                    if ((int)num == 6) { image = "./Image/state7.png"; }
                    if ((int)num == 7) { image = "./Image/state8.png"; }
                }
                if (type == 2) { }
            }
            public void setType(int SignalBitSize) {
                if (SignalBitSize == 1) { type = 0;}
                else if (SignalBitSize < 5) { type = 1;}
                else { type = 2;}
            }
        }
        public void Draw()
        {
            Panel.Children.Clear();
            //绘画消息框
            ObservableCollection<MessageInfo> messageInfo = dbcInfo.messageInfo;
            foreach (MessageInfo msg in messageInfo) {
                BrushConverter brushConverter = new BrushConverter();

                Grid grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.Name = msg.MessageName;
                TextBlock MSGNameBlock = new TextBlock();
                MSGNameBlock.Text = "  消息帧名称：  " + msg.MessageName + "  ";
                MSGNameBlock.FontSize = 20;
                MSGNameBlock.Margin = new Thickness(5);
                MSGNameBlock.Foreground = (Brush)brushConverter.ConvertFromString("#FFFFFFFF");
                MSGNameBlock.FontWeight = FontWeights.Bold;
                MSGNameBlock.HorizontalAlignment = HorizontalAlignment.Center;
                MSGNameBlock.VerticalAlignment = VerticalAlignment.Center;
                MSGNameBlock.Background = (Brush)brushConverter.ConvertFromString("#FFe67e22");
                Grid.SetColumn(MSGNameBlock, 0);
                grid.Children.Add(MSGNameBlock);

                TextBlock MSGFromBlock = new TextBlock();
                MSGFromBlock.Text = "  发送节点：  " + msg.Transmitter + "  ";
                MSGFromBlock.FontSize = 20;
                MSGFromBlock.Margin = new Thickness(5);
                MSGFromBlock.Foreground = (Brush)brushConverter.ConvertFromString("#FFFFFFFF");
                MSGFromBlock.FontWeight = FontWeights.Bold;
                MSGFromBlock.HorizontalAlignment = HorizontalAlignment.Center;
                MSGFromBlock.VerticalAlignment = VerticalAlignment.Center;
                MSGFromBlock.Background = (Brush)brushConverter.ConvertFromString("#FFe67e22");
                Grid.SetColumn(MSGFromBlock, 1);
                grid.Children.Add(MSGFromBlock);

                Canvas canvas = new Canvas();
                canvas.Height = 300;
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
                    siggrid.MouseLeftButtonDown += SIGGrid_MouseLeftButtonDown;
                    siggrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(3,GridUnitType.Star)});
                    siggrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1,GridUnitType.Star)});

                    TextBlock SIGtextBlock = new TextBlock();
                    SIGtextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    SIGtextBlock.VerticalAlignment = VerticalAlignment.Center;
                    SIGtextBlock.Text = "  " + sig.SignalName + "  ";
                    SIGtextBlock.FontSize = 12;
                    SIGtextBlock.FontWeight = FontWeights.Bold;
                    SIGtextBlock.Foreground = (Brush)brushConverter.ConvertFromString("#FFFFFFFF");
                    SIGtextBlock.Background = (Brush)brushConverter.ConvertFromString("#FF1e3799");
                    SIGtextBlock.Margin = new Thickness(5);
                    Grid.SetRow(SIGtextBlock,1);
                    
                    //添加信号对应控件
                    //绑定
                    Data data;
                    if (signal_values.ContainsKey(sig.SignalName))
                    {
                        data = signal_values[sig.SignalName];
                    }
                    else {
                        data = new Data();
                        data.setType(sig.SignalBitSize);
                        signal_values.Add(sig.SignalName, data);
                    }

                    if (data.type == 2)
                    {
                        Binding bindingTxt = new Binding();
                        bindingTxt.Source = data;
                        bindingTxt.Path = new PropertyPath("data");
                        TextBox textBox = new TextBox();
                        textBox.Name = sig.SignalName;
                        textBox.SetBinding(TextBox.TextProperty, bindingTxt);
                        textBox.Margin = new Thickness(5);
                        textBox.FontWeight = FontWeights.Bold;
                        textBox.FontSize = 16;
                        textBox.FontFamily = new FontFamily("Bahnschrift SemiBold");
                        textBox.VerticalContentAlignment = VerticalAlignment.Center;
                        textBox.HorizontalContentAlignment = HorizontalAlignment.Center;
                        textBox.Foreground = (Brush)brushConverter.ConvertFromString("#FFFFFFFF");
                        textBox.Background = (Brush)brushConverter.ConvertFromString("#FF0984e3");
                        Grid.SetRow(textBox, 0);

                        siggrid.Children.Add(textBox);
                    }
                    else
                    {
                        Binding bindingImg = new Binding();
                        bindingImg.Source = data;
                        bindingImg.Path = new PropertyPath("image");
                        Image image = new Image();
                        image.Name = sig.SignalName;
                        image.SetBinding(Image.SourceProperty, bindingImg);
                        Grid.SetRow(image, 0);

                        siggrid.Children.Add(image);
                    }


                    siggrid.Children.Add(SIGtextBlock);
                    Border border = new Border();
                    border.Background = (Brush)brushConverter.ConvertFromString("#FFe84078");
                    border.Opacity = 0;
                    border.Name = sig.SignalName;
                    Grid.SetRowSpan(border, 2);
                    siggrid.Children.Add(border);
                    Canvas.SetLeft(siggrid, canvasleft);
                    Canvas.SetTop(siggrid, canvastop);
                    canvasleft += 150;
                    if (canvasleft > 610) {
                        canvasleft = 10;
                        canvastop += 150;
                    }
                    
                    canvas.Children.Add(siggrid);
                }
                Panel.Children.Add(grid);
                Panel.Children.Add(canvas);
            }
        }

        Grid SelectedGrid;
        Dictionary<String, DataDraw> dataDraw = new Dictionary<string, DataDraw>();
        private void SIGGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border border;
            if (e.Source.GetType() == typeof(Border))
            {
                border = (Border)e.Source;
                SelectedGrid = (Grid)VisualTreeHelper.GetParent(border);
            }
            else return;
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                //Console.WriteLine("按下CTRL,打开数据绘图界面");
                DataDraw DD;
                if (dataDraw.ContainsKey(border.Name))
                {
                    DD = dataDraw[border.Name];
                    if (!DD.IsVisible) DD.Show();
                    DD.Modes.Refresh(signal_values[border.Name].data);
                }
                else if(signal_values[border.Name].type == 2)
                {
                    DD = new DataDraw(signal_values[border.Name].Max, signal_values[border.Name].Min);
                    DD.Show();
                    WindowInteropHelper parentHelper = new WindowInteropHelper(this);
                    WindowInteropHelper childHelper = new WindowInteropHelper(DD);

                    Win32Native.SetParent(childHelper.Handle, parentHelper.Handle);

                    DD.WindowState = WindowState.Normal;

                    dataDraw.Add(border.Name, DD);

                    if (signal_values[border.Name].data != 0)
                    DD.Modes.Refresh(signal_values[border.Name].data);
                }
            }
            else
            {
                //Console.WriteLine("点击了" + SelectedGrid.Name +"控件");
                SelectedGrid.MouseMove += SIGGrid_MouseMove;
                SelectedGrid.MouseLeftButtonUp += SIGGrid_MouseLeftButtonUp;
            }
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
            SelectedGrid.Opacity = 0.6;
            Canvas.SetLeft(SelectedGrid, point.X - SelectedGrid.Height/2);
            Canvas.SetTop(SelectedGrid, point.Y - SelectedGrid.Width/2);
        }
    }
}
