using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CANController
{
    public class PassDataWinFormEventArgs : EventArgs
    {

        public PassDataWinFormEventArgs()
        {
            //
        }
        public PassDataWinFormEventArgs(string FrameID, string FrameData, int FrameNumber)
        {
            this.frameID = FrameID;
            this.frameData = FrameData;
            this.frameNumber = FrameNumber;
        }

        public string frameID
        {
            get; set;
        }
        public string frameData
        {
            get; set;
        }
        public int frameNumber
        {
            get; set;
        }
    }
}
