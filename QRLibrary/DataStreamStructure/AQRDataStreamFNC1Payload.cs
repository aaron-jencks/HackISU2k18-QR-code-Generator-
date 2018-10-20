using QRLibrary.DataStreamStructure.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRLibrary.DataStreamStructure
{
    /// <summary>
    /// A default structure for FNC data streams in the first position
    /// uses no additional fields
    /// </summary>
    public class AQRDataStreamFNC1Payload : AQRDataStream
    {

        #region constructors

        public AQRDataStreamFNC1Payload(IQRDataStreamEncodingFormat format, IEnumerable<IQRDataStreamData> payload = null) : base(format, payload)
        {
        }

        public AQRDataStreamFNC1Payload(string format, IEnumerable<IQRDataStreamData> payload = null) : base(format, payload)
        {
        }

        #endregion

        public override void encodeData(string data)
        {
            throw new NotImplementedException();
        }
    }
}
