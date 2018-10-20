using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRLibrary
{
    /// <summary>
    /// Inteface for creating customized encoding formats for the QR code class
    /// </summary>
    public interface IQRDataStreamEncodingFormat
    {
        /// <summary>
        /// Gets the data Array of the object in the form of a boolean array
        /// </summary>
        /// <returns>Returns a boolean array that represents the data encoding format</returns>
        bool[] getDataArray();
    }
}
