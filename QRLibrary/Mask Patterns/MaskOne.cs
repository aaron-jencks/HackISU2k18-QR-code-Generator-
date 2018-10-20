using QRLibrary.Mask_Patterns.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRLibrary.Mask_Patterns
{
    public class MaskOne : AMaskPattern
    {
        public override bool[] getFormatKey()
        {
            return new bool[] { true, true, true };
        }

        public override bool isCalcPattern(int i, int j)
        {
            return (j % 3) == 0;
        }
    }
}
