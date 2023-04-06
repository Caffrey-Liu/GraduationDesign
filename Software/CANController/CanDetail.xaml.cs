using CAN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;

namespace CANController
{
    /// <summary>
    /// CanDetail.xaml 的交互逻辑
    /// </summary>
    public partial class CanDetail : Window , INotifyPropertyChanged
    {
        public CanDetail()
        {
            InitializeComponent();
        }

        public void Can_ReceviedData(object sender, CANFrameInfoArgs e)
        {
            FrameInfo message = e.CanFrameInfo;
            //Console.WriteLine("2号窗收到消息");
            //Console.WriteLine("ID   " + message.FrameID + "   Data    " + message.Data);
            CurrentProgress += "接收时间：" + DateTime.Now.ToString() + "   帧ID： " + message.FrameID + "   帧格式：" + message.FrameFormat + "   " + message.FrameType + "   数据长度：" + message.Data.Length/2 + "   帧数据：";
            for (int i = 0; i < message.Data.Length; i = i + 2) {
                CurrentProgress += "" + message.Data[i] + message.Data[i+1] + " ";
            }
            CurrentProgress += '\n';
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        private String _CurrentProgress = "";
        public String CurrentProgress
        {
            get { return _CurrentProgress; }
            set
            {
                _CurrentProgress = value;
                OnPropertyChanged("CurrentProgress");
            }
        }
        public delegate void PassDataBetweenFormHandler(object sender, PassDataWinFormEventArgs e);
        public event PassDataBetweenFormHandler PassDataBetweenForm;
        private void CAN_SendData(object sender, EventArgs e)
        {
            String frameID, frameData;
            int frameNumber;
            Int32.TryParse(FrameNumber.Text, out frameNumber);
            frameID = FrameID.Text;
            frameData = FrameData.Text;
            PassDataWinFormEventArgs args = new PassDataWinFormEventArgs(frameID, frameData, frameNumber);

            PassDataBetweenForm(this, args);
        }
    }
}
