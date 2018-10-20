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
            characterCount = payload.ToArray()[0] ?? new AQRDataStreamData("Character Count", new bool[characterCountBitCount]);
            dataBitStream = payload.ToArray()[1] ?? new AQRDataStreamData("Data Bit Stream", new bool[0]);
        }

        public AQRDataStreamStructurePayload(string format, IEnumerable<IQRDataStreamData> payload = null) : base(format, payload)
        {
            characterCount = payload.ToArray()[0] ?? new AQRDataStreamData("Character Count", new bool[characterCountBitCount]);
            dataBitStream = payload.ToArray()[1] ?? new AQRDataStreamData("Data Bit Stream", new bool[0]);
        }
    }
}
