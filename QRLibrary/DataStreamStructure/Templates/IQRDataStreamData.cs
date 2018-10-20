using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRLibrary.DataStreamStructure.Templates
{
    /// <summary>
    /// Inteface for creating customized data objects for data streams
    /// </summary>
    public interface IQRDataStreamData
    {
        /// <summary>
        /// Returns the data in the data object
        /// </summary>
        /// <returns>Returns the data in this object as a boolean array</returns>
        bool[] getData();

        /// <summary>
        /// Returns the number of bits allocated to this field
        /// </summary>
        /// <returns>Returns a integer stating how many bits are in this field</returns>
        int getBitCount();

        /// <summary>
        /// Returns the string identifier for this data object
        /// </summary>
        /// <returns>Returns a string identifying how to interpret this object</returns>
        string getIdentifier();
    }
}
