using LiveCharts.Wpf;
using LiveCharts;
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
    /// DataDraw.xaml 的交互逻辑
    /// </summary>
    public partial class DataDraw : Window
    {
        public Mode Modes;
        public DataDraw(double Max, double Min)
        {
            InitializeComponent();
            Modes = new Mode(Max,Min);
            this.DataContext = Modes;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        public class ViewModelBase : System.ComponentModel.INotifyPropertyChanged
        {
            public virtual event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
            /// <param name="propertyName"></param>
            protected virtual void OnPropertyChanged(string propertyName)
            {
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
            /// <param name="propertyName"></param>
            protected virtual void OnPropertyChanged<T>(System.Linq.Expressions.Expression<Func<T>> propertyExpression)
            {
                var propertyName = (propertyExpression.Body as System.Linq.Expressions.MemberExpression).Member.Name;
                this.OnPropertyChanged(propertyName);
            }
        }

        public class Mode : ViewModelBase
        {
            public SeriesCollection LineSeriesCollection { get; set; }

            private double axisXMax;
            public double AxisXMax
            {
                get { return axisXMax; }
                set { axisXMax = value; this.OnPropertyChanged("AxisXMax"); }
            }

            private double axisXMin;
            public double AxisXMin
            {
                get { return axisXMin; }
                set { axisXMin = value; this.OnPropertyChanged("AxisXMin"); }
            }

            private double axisYMax;
            public double AxisYMax
            {
                get { return axisYMax; }
                set { axisYMax = value; this.OnPropertyChanged("AxisYMax"); }
            }
            private double axisYMin;
            public double AxisYMin
            {
                get { return axisYMin; }
                set { axisYMin = value; this.OnPropertyChanged("AxisYMin"); }
            }

            private Random Randoms = new Random();

            public Func<double, string> CustomFormatterX { get; set; }
            public Func<double, string> CustomFormatterY { get; set; }


            //绑定的X轴数据
            private ChartValues<double> ValueList { get; set; }

            //表中最大容纳个数
            private int TabelShowCount = 10;


            private string CustomFormattersX(double val)
            {
                return val.ToString();
            }

            private string CustomFormattersY(double val)
            {
                return val.ToString();
            }


            public void Refresh(double Value)
            {
                //向图表中添加数据
                ValueList.Add(Value);

                //确保Y轴曲线不会超过图表
                int maxY = (int)ValueList.Max();
                AxisYMax = maxY + maxY/4;

                //Y轴保持数据居中（曲线会上下晃动）
                //int minY = ValueList.Count == 1 ? 0 : (int)ValueList.Min();
                //AxisYMin = minY - 10;

                //y轴的设置
                if (ValueList.Count > TabelShowCount)
                {
                    AxisXMax = ValueList.Count - 1;
                    AxisXMin = ValueList.Count - TabelShowCount;
                }

                //这里如果移除数组，图表曲线会原地起伏，就没有X轴移动的动画效果了
                //if (ValueList.Count > 20)
                //{
                //    ValueList.RemoveAt(0);
                //}
            }

            public Mode(double Max, double Min)
            {
                AxisXMax = 15;
                AxisXMin = 0;
                AxisYMax = Max;
                AxisYMin = Min;

                ValueList = new ChartValues<double>();
                LineSeriesCollection = new SeriesCollection();

                CustomFormatterX = CustomFormattersX;
                CustomFormatterY = CustomFormattersY;

                LineSeries lineseries = new LineSeries();
                lineseries.DataLabels = true;
                lineseries.Values = ValueList;
                lineseries.FontWeight = FontWeights.Bold;
                LineSeriesCollection.Add(lineseries);
            }
        }
    }
}
