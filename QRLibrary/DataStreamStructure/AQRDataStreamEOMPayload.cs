using QRLibrary.DataStreamStructure.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRLibrary.DataStreamStructure
{
    /// <summary>
    /// A default structure class for an End of Message (EOM) payload
    /// It contains no data aside from the format specifier
    /// </summary>
    public class AQRDataStreamEOMPayload : AQRDataStream
    {
        #region constructors

        public AQRDataStreamEOMPayload(IQRDataStreamEncodingFormat format, IEnumerable<IQRDataStreamData> payload = null) : base(format, payload)
        {
        }

        public AQRDataStreamEOMPayload(string format, IEnumerable<IQRDataStreamData> payload = null) : base(format, payload)
        {
        }

        #endregion

        public override void encodeData(string data)
        {
        }
    }
}
