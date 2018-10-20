using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRLibrary.Mask_Patterns.Templates
{
    public interface IMaskPattern
    {
        bool isCalcPattern(int i, int j);
    }
}
