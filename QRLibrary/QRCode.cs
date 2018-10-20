using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRLibrary.DataStreamFormatSpecifiers.Templates;
using QRLibrary.DataStreamStructure;
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
            AQRDataStreamCharacterPayload temp;
            AQRDataStreamEncodingFormat format = generateEncodingFormat(mode);
            AQRDataStream stream;

            switch (mode)
            {
                case DataStreamEncodingMode.AlphaNumeric:
                    temp = new AQRDataStreamCharacterPayload(format);
                    temp.bitsPerCharacter = 11; // Per symbol
                    stream = temp;
                    break;
                case DataStreamEncodingMode.Byte:
                    temp = new AQRDataStreamCharacterPayload(format);
                    temp.bitsPerCharacter = 8;
                    stream = temp;
                    break;
                case DataStreamEncodingMode.Kanji:
                    temp = new AQRDataStreamCharacterPayload(format);
                    temp.bitsPerCharacter = 13;
                    stream = temp;
                    break;
                case DataStreamEncodingMode.Numeric:
                    temp = new AQRDataStreamCharacterPayload(format);
                    temp.bitsPerCharacter = 10; // Per 3 digits
                    stream = temp;
                    break;
                case DataStreamEncodingMode.EOM:
                    stream = new AQRDataStreamEOMPayload(format);
                    break;
                case DataStreamEncodingMode.ExtendedChannel:
                    stream = new AQRDataStreamExtendedChannelPayload(format);
                    break;
                case DataStreamEncodingMode.FNC1_1:
                    stream = new AQRDataStreamFNC1Payload(format);
                    break;
                case DataStreamEncodingMode.FNC1_2:
                    stream = new AQRDataStreamFNC2Payload(format);
                    break;
                case DataStreamEncodingMode.StructuredAppend:
                    stream = new AQRDataStreamStructurePayload(format);
                    break;
                default:
                    throw new Exception("Unrecognized encoding mode");
            }

            return stream;
        }

        /// <summary>
        /// Creates a basic byte formatted bit stream using the string provided
        /// </summary>
        /// <param name="data">Data to be encoded</param>
        /// <returns></returns>
        public static bool[] generateCharacterStream(string data)
        {

        }

        /// <summary>
        /// Calculates the alphanumeric value for what is considered an alphanumeric character by the QR code standard.
        /// </summary>
        /// <param name="c">Character to search for</param>
        /// <returns>Returns the integer value represented by the character or throws an exception if not found.</returns>
        public static int FindAlphaNumericNumber(char c)
        {
            if (c <= '9' && c >= '0')
                return Convert.ToInt32("" + c);
            if (c >= 'A' && c <= 'Z')
                return 10 + (c - 'A');
            else
                switch (c)
                {
                    case ' ':
                        return 36;
                    case '$':
                        return 37;
                    case '%':
                        return 38;
                    case '*':
                        return 39;
                    case '+':
                        return 40;
                    case '-':
                        return 41;
                    case '.':
                        return 42;
                    case '/':
                        return 43;
                    case ':':
                        return 44;
                    default:
                        throw new Exception("Invalid alhpanumeric character!");
                }
        }

        /// <summary>
        /// Finds the alphanumeric pair as specified by the QR code standard
        /// V = 45 * a + b
        /// </summary>
        /// <param name="a">Character a</param>
        /// <param name="b">Character b</param>
        /// <returns>Returns the alphanumeric pair of the two characters</returns>
        public static int FindAlphaNumericPair(char a, char b)
        {
            return 45 * FindAlphaNumericNumber(a) + FindAlphaNumericNumber(b);
        }

        /// <summary>
        /// Converts an integer into the given number of bits represented by a boolean array
        /// </summary>
        /// <param name="num">The number to be converted</param>
        /// <param name="bits">The number of bits to use</param>
        /// <returns>Returns a boolean array representing the integer in binary code</returns>
        public static bool[] ConvertToBoolean(int num, int bits)
        {
            if (num > Math.Pow(bits, 2))
                throw new InvalidOperationException("Number to conver to binary cannot be larger than the largest binary number allowed by the bit count!");
            bool[] result = new bool[bits];
            for (int i = bits; i >= 0; i--)
            {
                if (((num >> i) & 1) == 1)
                    result[bits - 1 - i] = true;
            }
            return result;
        }

        #endregion
    }
}
