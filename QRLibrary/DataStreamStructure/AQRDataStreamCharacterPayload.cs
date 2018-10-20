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
        protected int characterCountBitCount { get; set; } = 16;

        /// <summary>
        /// The number of bits allocated per unit of symbol for the specific type
        /// </summary>
        public int bitsPerCharacter { get; set; }

        #endregion

        #region Constructors

        public AQRDataStreamCharacterPayload(IQRDataStreamEncodingFormat format, IEnumerable<IQRDataStreamData> payload = null) : base(format, payload)
        {
            characterCount = payload.ToArray()[0] ?? new AQRDataStreamData("Character Count", new bool[characterCountBitCount]);
            dataBitStream = payload.ToArray()[1] ?? new AQRDataStreamData("Data Bit Stream", new bool[0]);
        }

        public AQRDataStreamCharacterPayload(string format, IEnumerable<IQRDataStreamData> payload = null) : base(format, payload)
        {
            characterCount = payload.ToArray()[0] ?? new AQRDataStreamData("Character Count", new bool[characterCountBitCount]);
            dataBitStream = payload.ToArray()[1] ?? new AQRDataStreamData("Data Bit Stream", new bool[0]);
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
