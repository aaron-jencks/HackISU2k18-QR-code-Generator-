using QRLibrary.DataStreamStructure.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRLibrary.DataStreamStructure
{
    /// <summary>
    /// A default payload structure for structure payloads
    /// Uses 3 additional fields
    /// symbolPosition 4 bits specifying symbol position
    /// symbolCount 4 bits specifying the number of symbols
    /// parity 8 bits specifying the parity of the symbols
    /// </summary>
    public class AQRDataStreamStructurePayload : AQRDataStream
    {
        #region Properties

        /// <summary>
        /// 4 bits specifying the symbol position
        /// </summary>
        public IQRDataStreamData symbolPosition { get; set; }

        /// <summary>
        /// 4 bits specifying the number of symbols
        /// </summary>
        public IQRDataStreamData symbolCount { get; set; }

        /// <summary>
        /// 8 bits specifying the symbol parity.
        /// </summary>
        public IQRDataStreamData parity { get; set; }

        #endregion

        public AQRDataStreamStructurePayload(IQRDataStreamEncodingFormat format, IEnumerable<IQRDataStreamData> payload = null) : base(format, payload)
        {
            symbolPosition = payload.ToArray()[0] ?? new AQRDataStreamData("Symbol Position", new bool[4]);
            symbolCount = payload.ToArray()[1] ?? new AQRDataStreamData("Data Bit Stream", new bool[4]);
            parity = payload.ToArray()[2] ?? new AQRDataStreamData("Parity", new bool[8]);
        }

        public AQRDataStreamStructurePayload(string format, IEnumerable<IQRDataStreamData> payload = null) : base(format, payload)
        {
            symbolPosition = payload.ToArray()[0] ?? new AQRDataStreamData("Symbol Position", new bool[4]);
            symbolCount = payload.ToArray()[1] ?? new AQRDataStreamData("Data Bit Stream", new bool[4]);
            parity = payload.ToArray()[2] ?? new AQRDataStreamData("Parity", new bool[8]);
        }

        #region override methods

        /// <summary>
        /// Returns the format code concatenated to the character count, and the data bit stream
        /// </summary>
        /// <returns>returns a boolean array representing the format code and the character count and the data bit stream.</returns>
        public override bool[] getAllData()
        {
            List<bool> result = new List<bool>(base.getFormatCode());
            result.AddRange(symbolPosition.getData());
            result.AddRange(symbolCount.getData());
            result.AddRange(parity.getData());
            return result.ToArray();
        }

        /// <summary>
        /// Returns the payload which is represented by the character count and the data bit stream
        /// !!! IT DOES NOT USE THE PAYLOAD PROPERTY OF THE PARENT CLASS !!!
        /// </summary>
        /// <returns>Returns a boolean array representing the character count concatenated with the data bit stream.</returns>
        public override bool[] getPayloadCode()
        {
            List<bool> result = new List<bool>(symbolPosition.getData());
            result.AddRange(symbolCount.getData());
            result.AddRange(parity.getData());
            return result.ToArray();
        }

        public override void encodeData(string data)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
