using QRLibrary.DataStreamStructure.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRLibrary.DataStreamStructure
{
    /// <summary>
    /// A default class for Extended Channel Interpretation payloads
    /// Uses one additional field
    /// eciAssignmentNumber a variable length eci assignment number
    /// </summary>
    public class AQRDataStreamExtendedChannelPayload : AQRDataStream
    {
        #region Properties

        /// <summary>
        /// The ECI Assignment number (Variable length)
        /// </summary>
        public IQRDataStreamData eciAssignmentNumber { get; set; }

        #endregion

        #region constructors

        public AQRDataStreamExtendedChannelPayload(IQRDataStreamEncodingFormat format, IEnumerable<IQRDataStreamData> payload = null) : base(format, payload)
        {
            eciAssignmentNumber = payload.ToArray()[0] ?? new AQRDataStreamData("Assignment Number", new bool[0]);
        }

        public AQRDataStreamExtendedChannelPayload(string format, IEnumerable<IQRDataStreamData> payload = null) : base(format, payload)
        {
            eciAssignmentNumber = payload.ToArray()[0] ?? new AQRDataStreamData("Assignment Number", new bool[0]);
        }

        #endregion

        #region override methods

        public override void encodeData(string data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the format code concatenated to the character count, and the data bit stream
        /// </summary>
        /// <returns>returns a boolean array representing the format code and the character count and the data bit stream.</returns>
        public override bool[] getAllData()
        {
            List<bool> result = new List<bool>(base.getFormatCode());
            result.AddRange(eciAssignmentNumber.getData());
            return result.ToArray();
        }

        /// <summary>
        /// Returns the payload which is represented by the character count and the data bit stream
        /// !!! IT DOES NOT USE THE PAYLOAD PROPERTY OF THE PARENT CLASS !!!
        /// </summary>
        /// <returns>Returns a boolean array representing the character count concatenated with the data bit stream.</returns>
        public override bool[] getPayloadCode()
        {
            List<bool> result = new List<bool>(eciAssignmentNumber.getData());
            return result.ToArray();
        }

        #endregion
    }
}
