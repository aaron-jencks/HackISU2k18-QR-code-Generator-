using QRLibrary.DataStreamStructure.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRLibrary.DataStreamStructure
{
    /// <summary>
    /// A default structure for an FNC payload in the second position
    /// uses one additional field
    /// applicationIndicator - an 8 bit application indicator
    /// </summary>
    public class AQRDataStreamFNC2Payload : AQRDataStream
    {
        #region Properties

        /// <summary>
        /// The 8 bit Application Indicator
        /// </summary>
        public IQRDataStreamData applicationIndicator { get; set; }

        #endregion

        #region constructors

        public AQRDataStreamFNC2Payload(IQRDataStreamEncodingFormat format, IEnumerable<IQRDataStreamData> payload = null) : base(format, payload)
        {
            applicationIndicator = payload.ToArray()[0] ?? new AQRDataStreamData("Assignment Number", new bool[0]);
            this.payload.RemoveAt(0);
        }

        public AQRDataStreamFNC2Payload(string format, IEnumerable<IQRDataStreamData> payload = null) : base(format, payload)
        {
            applicationIndicator = payload.ToArray()[0] ?? new AQRDataStreamData("Assignment Number", new bool[0]);
            this.payload.RemoveAt(0);
        }

        #endregion

        #region override methods

        /// <summary>
        /// Returns the format code concatenated to the character count, and the data bit stream
        /// </summary>
        /// <returns>returns a boolean array representing the format code and the character count and the data bit stream.</returns>
        public override bool[] getAllData()
        {
            List<bool> result = new List<bool>(base.getFormatCode());
            result.AddRange(applicationIndicator.getData());
            result.AddRange(getPayloadCode());
            return result.ToArray();
        }

        #endregion
    }
}
