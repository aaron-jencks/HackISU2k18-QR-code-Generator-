using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRLibrary.DataStreamFormatSpecifiers.Templates
{
    /// <summary>
    /// Inheritable class for that implements the IQRDataStreamEncodingFormat Interface
    /// </summary>
    public class AQRDataStreamEncodingFormat : IQRDataStreamEncodingFormat
    {
        #region Properties

        /// <summary>
        /// The boolean data representing the binary code
        /// </summary>
        protected bool[] dataArray { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new data stream format code from the given array
        /// </summary>
        /// <param name="data">The new data format code</param>
        public AQRDataStreamEncodingFormat(bool[] data)
        {
            dataArray = data;
        }

        /// <summary>
        /// Creates a new Data Stream Format code from the given string.
        /// </summary>
        /// <param name="data">String specifying the data in the format "0100010100101..."</param>
        public AQRDataStreamEncodingFormat(string data)
        {
            data = data.Trim();
            dataArray = new bool[data.Length];
            for(int i = 0; i < data.Length; i++)
            {
                switch(data[i])
                {
                    case '0':
                        dataArray[i] = false;
                        break;
                    case '1':
                        dataArray[i] = true;
                        break;
                    default:
                        throw new Exception("Character not recognized in data string");
                }
            }

        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the current encoding mode of this object
        /// </summary>
        /// <returns></returns>
        public  DataStreamEncodingMode getEncodingMode()
        {
            switch(ToString())
            {
                case "0001":
                    return DataStreamEncodingMode.Numeric;
                case "0010":
                    return DataStreamEncodingMode.AlphaNumeric;
                case "0100":
                    return DataStreamEncodingMode.Byte;
                case "1000":
                    return DataStreamEncodingMode.Kanji;
                case "0011":
                    return DataStreamEncodingMode.StructuredAppend;
                case "0111":
                    return DataStreamEncodingMode.ExtendedChannel;
                case "0101":
                    return DataStreamEncodingMode.FNC1_1;
                case "1001":
                    return DataStreamEncodingMode.FNC1_2;
                case "0000":
                    return DataStreamEncodingMode.EOM;
                default:
                    throw new Exception("No valid encoding mode found!");
            }
        }

        public virtual bool[] getDataArray()
        {
            return dataArray;
        }

        /// <summary>
        /// Returns the string representing the data array in the format "00101011010..."
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = "";
            foreach (bool b in dataArray)
                result += (b) ? "1" : "0";
            return result;
        }

        #endregion
    }
}
