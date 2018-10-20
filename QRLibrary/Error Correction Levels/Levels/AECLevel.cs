using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRLibrary.Error_Correction_Levels.Levels
{
    public class AECLevel : IECLevel
    {
        public ErrorCorrectionLevel mode { get; set; }

        public AECLevel(ErrorCorrectionLevel level)
        {
            mode = level;
        }

        public bool[] getBooleanData()
        {
            bool[] result = new bool[3];

            switch(mode)
            {
                case ErrorCorrectionLevel.High:
                    return new bool[] { false, false };
                case ErrorCorrectionLevel.Low:
                    return new bool[] { true, true };
                case ErrorCorrectionLevel.Medium:
                    return new bool[] { true, false };
                case ErrorCorrectionLevel.Q:
                    return new bool[] { false, true };
                default:
                    throw new Exception("Unrecognized Errror Correction Level mode!");
            }
        }
    }
}
