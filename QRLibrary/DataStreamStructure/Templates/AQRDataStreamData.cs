using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRLibrary.DataStreamStructure.Templates
{
    /// <summary>
    /// A parent class that implements the IQRDataStreamData interface.
    /// </summary>
    public class AQRDataStreamData : IQRDataStreamData
    {
        #region Properties

        /// <summary>
        /// String identifier representing how to interpret this data
        /// </summary>
        protected string identifier { get; set; }

        /// <summary>
        /// Boolean data stored in this object
        /// </summary>
        protected bool[] data { get; set; }

        /// <summary>
        /// Number of bits in this data block
        /// </summary>
        protected int bitCount { get; set; }

        #endregion

        #region Constructors

        public AQRDataStreamData(string id, bool[] data)
        {
            this.data = data;
            identifier = id;
            bitCount = data.Length;
        }

        public AQRDataStreamData(string id, string data)
        {
            data.Trim();
            this.data = new bool[data.Length];
            for(int i = 0; i < data.Length; i++)
            {
                switch(data[i])
                {
                    case '0':
                        this.data[i] = false;
                        break;
                    case '1':
                        this.data[i] = true;
                        break;
                    default:
                        throw new Exception("Unrecognized character in data string!");
                }
            }

            identifier = id;
            bitCount = this.data.Length;
        }

        #endregion

        #region Interface Methods

        public int getBitCount()
        {
            return bitCount;
        }

        public bool[] getData()
        {
            return data;
        }

        public string getIdentifier()
        {
            return identifier;
        }

        #endregion
    }
}
