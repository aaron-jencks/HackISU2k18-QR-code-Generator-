using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRLibrary.DataStreamFormatSpecifiers.Templates
{
    public class AQRDataStreamEncodingFormat : IQRDataStreamEncodingFormat
    {
        /// <summary>
        /// The boolean data representing the binary code
        /// </summary>
        protected bool[] dataArray { get; set; }

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

        public virtual bool[] getDataArray()
        {
            return dataArray;
        }
    }
}
