using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRLibrary.DataStreamStructure.Templates
{
    /// <summary>
    /// Interface for creating customized data structures for the data encoded in the QR code.
    /// </summary>
    public interface IQRDataStream
    {
        /// <summary>
        /// Gets the Formatting code for the data stream object
        /// </summary>
        /// <returns>Returns the result of calling the getDataArray() method of the AQRDataStreamEncodingFormat object</returns>
        bool[] getFormatCode();

        /// <summary>
        /// Gets the payload data contained in the data stream object
        /// </summary>
        /// <returns>Returns a list of booleans dependent on the encoding format</returns>
        bool[] getPayloadCode();

        /// <summary>
        /// Returns both the format code and the payload code.
        /// </summary>
        /// <returns>Returns a list of booleans dependent on the type of encoding format.</returns>
        bool[] getAllData();

        /// <summary>
        /// Takes the given string and encodes it into the current bit stream, modifying it's fields as necessary
        /// </summary>
        /// <param name="data"></param>
        void encodeData(string data);
    }
}
