using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRLibrary
{
    public interface IQRDataStreamEncodingFormat
    {
        /// <summary>
        /// Gets the data Array of the object in the form of a boolean array
        /// </summary>
        /// <returns>Returns a boolean array that represents the data encoding format</returns>
        bool[] getDataArray();
    }
}
