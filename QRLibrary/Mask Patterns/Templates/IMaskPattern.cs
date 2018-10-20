using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRLibrary.Mask_Patterns.Templates
{
    /// <summary>
    /// Interface for implementing mask patterns
    /// </summary>
    public interface IMaskPattern
    {
        /// <summary>
        /// Gets the 3 bit key for this mask
        /// </summary>
        /// <returns>Returns a boolean array representing this 3 bit key</returns>
        bool[] getFormatKey();

        /// <summary>
        /// Determines if the given coordinate is a dark, or light square
        /// </summary>
        /// <param name="i">Row specifier</param>
        /// <param name="j">Column specifier</param>
        /// <returns>Returns a boolean representing if there is a dark spot there.</returns>
        bool isCalcPattern(int i, int j);
    }
}
