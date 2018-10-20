using QRLibrary.Mask_Patterns.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRLibrary.Mask_Patterns
{
    public class MaskEight : AMaskPattern
    {
        public override bool[] getFormatKey()
        {
            return new bool[]{ false, false, false};
        }

        public override bool isCalcPattern(int i, int j)
        {
            return (((i * j) % 2) + ((i * j) % 3)) == 0;
        }
    }
}
