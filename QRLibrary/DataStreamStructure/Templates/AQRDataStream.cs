using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRLibrary.DataStreamFormatSpecifiers.Templates;

namespace QRLibrary.DataStreamStructure.Templates
{
    public class AQRDataStream : IQRDataStream
    {
        #region properties

        /// <summary>
        /// The Encoding format for the data stream
        /// </summary>
        public IQRDataStreamEncodingFormat format { get; set; }

        /// <summary>
        /// The payload of the data stream can have any number, depends on the format
        /// </summary>
        public List<IQRDataStreamData> payload { get; set; }

        #endregion

        #region Constructors

        public AQRDataStream(IQRDataStreamEncodingFormat format, IEnumerable<IQRDataStreamData> payload = null)
        {
            this.format = format;
            this.payload = payload.ToList() ?? new List<IQRDataStreamData>();
        }

        public AQRDataStream(string format, IEnumerable<IQRDataStreamData> payload = null)
        {
            this.format = new AQRDataStreamEncodingFormat(format);
            this.payload = payload.ToList() ?? new List<IQRDataStreamData>();
        }

        #endregion

        #region Inteface methods

        public virtual bool[] getAllData()
        {
            List<bool> result = new List<bool>();

            result.AddRange(format.getDataArray());

            foreach (IQRDataStreamData d in payload)
                result.AddRange(d.getData());

            return result.ToArray();
        }

        public virtual bool[] getFormatCode()
        {
            return format.getDataArray();
        }

        public virtual bool[] getPayloadCode()
        {
            List<bool> result = new List<bool>();

            foreach (IQRDataStreamData d in payload)
                result.AddRange(d.getData());

            return result.ToArray();
        }

        #endregion
    }
}
