using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRLibrary.Mask_Patterns.Templates
{
    /// <summary>
    /// Inheritable class for making custom mask patterns
    /// </summary>
    public abstract class AMaskPattern : IMaskPattern
    {
        public AMaskPattern()
        {

        }

        public abstract bool isCalcPattern(int i, int j);

        public abstract bool[] getFormatKey();
    }
}
