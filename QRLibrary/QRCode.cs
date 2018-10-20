﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRLibrary.DataStreamFormatSpecifiers.Templates;
using QRLibrary.DataStreamStructure.Templates;

namespace QRLibrary
{
    public class QRCode
    {
        #region Static Methods

        /// <summary>
        /// Generates a Encoding format object from an enumerated typical stream formats
        /// </summary>
        /// <param name="mode">The Data Stream encoding mode</param>
        /// <returns>Returns an encoding format object</returns>
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

        /// <summary>
        /// Creates a generic structure using the encoding format.
        /// </summary>
        /// <param name="mode">The data stream encoding mode</param>
        /// <returns>Returns an initialized data streaming object</returns>
        public static AQRDataStream generateTypicalStructure(DataStreamEncodingMode mode)
        {

        }

        #endregion
    }
}
