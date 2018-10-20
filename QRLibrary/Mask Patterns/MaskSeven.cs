using QRLibrary.Mask_Patterns.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRLibrary.Mask_Patterns
{
    public class MaskSeven : AMaskPattern
    {
        public override bool[] getFormatKey()
        {
            return new bool[] { false, false, true };
        }

        public override bool isCalcPattern(int i, int j)
        {
            return (((i / 2) + (j / 3)) % 2) == 0;
        }
    }
}
