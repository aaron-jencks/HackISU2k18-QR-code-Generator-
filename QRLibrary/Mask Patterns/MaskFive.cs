using QRLibrary.Mask_Patterns.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRLibrary.Mask_Patterns
{
    public class MaskFive : AMaskPattern
    {
        public override bool[] getFormatKey()
        {
            return new bool[] { false, true, true };
        }

        public override bool isCalcPattern(int i, int j)
        {
            return ((((i * j) % 3) + i * j) % 2) == 0;
        }
    }
}
