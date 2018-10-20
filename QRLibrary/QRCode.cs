using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRLibrary.DataStreamFormatSpecifiers.Templates;

namespace QRLibrary
{
    public class QRCode
    {
        #region Static Methods

        public static AQRDataStreamEncodingFormat generateEncodingFormat(DataStreamEncodingMode mode)
        {
            string code = "";
            switch(mode)
            {
                case DataStreamEncodingMode.AlphaNumeric:
                    code = "0010";
                    break;
                case DataStreamEncodingMode.Byte:
                    code = "0100";
                    break;
                case DataStreamEncodingMode.EOM:
                    code = "0000";
                    break;
                case DataStreamEncodingMode.ExtendedChannel:
                    code = "0111";
                    break;
                case DataStreamEncodingMode.FNC1_1:
                    code = "0101";
                    break;
                case DataStreamEncodingMode.FNC1_2:
                    code = "1001";
                    break;
                case DataStreamEncodingMode.Kanji:
                    code = "1000";
                    break;
                case DataStreamEncodingMode.Numeric:
                    code = "0001";
                    break;
                case DataStreamEncodingMode.StructuredAppend:
                    code = "0011";
                    break;
                default:
                    throw new Exception("Unrecognized encoding mode");
            }

            return new AQRDataStreamEncodingFormat(code);
        }

        #endregion
    }
}
