using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRLibrary.DataStreamFormatSpecifiers.Templates;
using QRLibrary.DataStreamStructure.Templates;

namespace QRLibrary.DataStreamStructure
{
    /// <summary>
    /// A default structure for character payload data streams
    /// Uses two additional fields in the payload
    /// characterCount is a variable bit field that determines how many characters are in the stream
    /// dataBitStream is the stream of characters
    /// </summary>
    public class AQRDataStreamCharacterPayload : AQRDataStream
    {
        #region Properties

        /// <summary>
        /// A variable bit field that determines how many characters are in the stream
        /// </summary>
        public IQRDataStreamData characterCount { get; set; }

        /// <summary>
        /// The stream of characters
        /// </summary>
        public IQRDataStreamData dataBitStream { get; set; }

        /// <summary>
        /// Determines how many bits are in the character count field
        /// </summary>
        public int characterCountBitCount { get; set; } = 16;

        /// <summary>
        /// The number of bits allocated per unit of symbol for the specific type
        /// </summary>
        public int bitsPerCharacter { get; set; }

        #endregion

        #region Constructors

        public AQRDataStreamCharacterPayload(IQRDataStreamEncodingFormat format, IEnumerable<IQRDataStreamData> payload = null) : base(format, payload)
        {
            characterCount = payload?.ToArray()[0] ?? new AQRDataStreamData("Character Count", new bool[characterCountBitCount]);
            dataBitStream = payload?.ToArray()[1] ?? new AQRDataStreamData("Data Bit Stream", new bool[0]);
        }

        public AQRDataStreamCharacterPayload(string format, IEnumerable<IQRDataStreamData> payload = null) : base(format, payload)
        {
            characterCount = payload?.ToArray()[0] ?? new AQRDataStreamData("Character Count", new bool[characterCountBitCount]);
            dataBitStream = payload?.ToArray()[1] ?? new AQRDataStreamData("Data Bit Stream", new bool[0]);
        }

        #endregion

        #region override methods

        public override void encodeData(string data)
        {
            List<bool> temp = new List<bool>();

            switch(format.getEncodingMode())
            {
                case DataStreamEncodingMode.AlphaNumeric:
                    // If the number of characters is not even, it appends a space to make it even
                    if (data.Length % 2 != 0)
                        data += " ";

                    // Updates the character count
                    characterCount = new AQRDataStreamData("Character Count",
                        QRCode.ConvertToBoolean(data.Length / 2, characterCountBitCount));

                    temp = new List<bool>((data.Length / 2) * bitsPerCharacter);
                    for (int i = 0; i < data.Length; i += 2)
                    {
                        int pair = QRCode.FindAlphaNumericPair(data[i], data[i + 1]);
                        temp.AddRange(QRCode.ConvertToBoolean(pair, bitsPerCharacter));
                    }
                    break;
                case DataStreamEncodingMode.Byte:
                    // Updates the character count
                    characterCount = new AQRDataStreamData("Character Count",
                        QRCode.ConvertToBoolean(data.Length, characterCountBitCount));

                    temp = new List<bool>(data.Length * bitsPerCharacter);
                    foreach (char c in data)
                        temp.AddRange(QRCode.ConvertToBoolean(c, bitsPerCharacter));
                    break;
                case DataStreamEncodingMode.Kanji:
                    throw new NotImplementedException();
                    break;
                case DataStreamEncodingMode.Numeric:
                    throw new NotImplementedException();
                    break;
                default:
                    throw new Exception("No valid encoding method found!");
            }
        }

        /// <summary>
        /// Returns the format code concatenated to the character count, and the data bit stream
        /// </summary>
        /// <returns>returns a boolean array representing the format code and the character count and the data bit stream.</returns>
        public override bool[] getAllData()
        {
            List<bool> result = new List<bool>(base.getFormatCode());
            result.AddRange(characterCount.getData());
            result.AddRange(dataBitStream.getData());
            return result.ToArray();
        }

        /// <summary>
        /// Returns the payload which is represented by the character count and the data bit stream
        /// !!! IT DOES NOT USE THE PAYLOAD PROPERTY OF THE PARENT CLASS !!!
        /// </summary>
        /// <returns>Returns a boolean array representing the character count concatenated with the data bit stream.</returns>
        public override bool[] getPayloadCode()
        {
            List<bool> result = new List<bool>(characterCount.getData());
            result.AddRange(dataBitStream.getData());
            return result.ToArray();
        }

        #endregion
    }
}
