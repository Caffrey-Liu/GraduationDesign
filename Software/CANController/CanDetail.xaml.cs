using CAN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CANController
{
    /// <summary>
    /// CanDetail.xaml 的交互逻辑
    /// </summary>
    public partial class CanDetail : Window
    {
        public CanDetail()
        {
            InitializeComponent();
        }
        public void Can_ReceviedData(object sender, CANFrameInfoArgs e)
        {
            Console.WriteLine("2号窗收到消息");
            FrameInfo message = e.CanFrameInfo;
            //test.Text = "ID   " + message.FrameID + "   Data    " + message.Data;
            Console.WriteLine("ID   " + message.FrameID + "   Data    " + message.Data);
            //throw new NotImplementedException();
        }


    }
}
