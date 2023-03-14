using CAN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CANController
{
    internal class CANtest
    {
        static void Main(string[] args)
        {
            CANhelper can = new CANhelper();
            can.CanSetting.SetCAN((uint)0, //deviceIndex 设备索引号
                                  (byte)0, //canIndex CAN的路索引号
                                  (byte)CanFilterType.DualFilter, //filterType 过滤类型
                                  (byte)CanMode.NormalMode, //canMode CAN模式
                                  "00000000", //canCode 过滤码
                                  "FFFFFFFF", //canMask 掩码
                                  "00", //时间高位
                                  "1C"); //时间低位,默认 4F 2F
            can.ConnectCANDevice(); //连接CAN设备
            Console.WriteLine("连接成功");
            can.StartCAN(); //启动CAN设备
            Console.WriteLine("启动成功");
            can.ReceviedData += Can_ReceviedData;
        }

        private static void Can_ReceviedData(object sender, CANFrameInfoArgs e)
        {
            Console.WriteLine("收到消息");
            //FrameInfo message = e.CanFrameInfo.get;
            //throw new NotImplementedException();
        }
    }
}
