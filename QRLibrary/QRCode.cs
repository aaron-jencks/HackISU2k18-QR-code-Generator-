using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRLibrary.DataStreamFormatSpecifiers.Templates;
using QRLibrary.DataStreamStructure;
using QRLibrary.DataStreamStructure.Templates;
using QRLibrary.Error_Correction_Levels.Levels;
using QRLibrary.Mask_Patterns;
using QRLibrary.Mask_Patterns.Templates;

namespace QRLibrary
{
    /// <summary>
    /// Generic class for a QR Code, contains static methods for manipulating the stream data as well
    /// as data for an individual QR code.
    /// </summary>
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
                    temp.characterCountBitCount = 13;   // Updates the bit count for the character count field
                    temp.bitsPerCharacter = 11; // Per symbol
                    stream = temp;
                    break;
                case DataStreamEncodingMode.Byte:
                    temp = new AQRDataStreamCharacterPayload(format);
                    // The default value for the bit count for the character count field is okay here.
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
        public static AQRDataStreamCharacterPayload generateCharacterStream(string data, bool use_alpha = false)
        {
            DataStreamEncodingMode mode = (use_alpha) ? DataStreamEncodingMode.AlphaNumeric : DataStreamEncodingMode.Byte;
            AQRDataStreamCharacterPayload result = (AQRDataStreamCharacterPayload)generateTypicalStructure(mode);
            result.encodeData(data);
            return result;
        }

        /// <summary>
        /// Determines if the string contains only characters A-Z, Space, $, *, +, -, ., /, :
        /// </summary>
        /// <param name="s">String to check</param>
        /// <returns>Returns if the string is alphanumeric or not</returns>
        public static bool isAlphaNumeric(string s)
        {
            string test = " $*+-./:";
            foreach(char c in s)
            {
                if (c < 'A' || c > 'Z')
                    if (!test.Contains(c))
                        return false;
            }
            return true;
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
            if (num > Math.Pow(2, bits))
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

        #region Properties

        /// <summary>
        /// The data currently encoded in the QR Code
        /// </summary>
        public List<AQRDataStream> EncodedData { get; set; }

        /// <summary>
        /// Determines the calculation used for filling the mask spaces
        /// </summary>
        public IMaskPattern maskPattern { get; set; } = new MaskFive();

        /// <summary>
        /// Determines the error correction level that the decoder needs to use to find the data.
        /// </summary>
        public IECLevel errorCorrectionLevel { get; set; } = new AECLevel(ErrorCorrectionLevel.Low);

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new QR Code with not data encoded
        /// </summary>
        public QRCode()
        {
            EncodedData = new List<AQRDataStream>();
        }

        /// <summary>
        /// Creates a new QR Code and encodes the given data into the QR Code
        /// </summary>
        /// <param name="data">Data to be encoded</param>
        public QRCode(IEnumerable<string> data)
        {
            EncodedData = new List<AQRDataStream>();
            foreach (string s in data)
                EncodeString(s);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Encodes a string into the list of data streams used in this QR Code
        /// </summary>
        /// <param name="data"></param>
        public void EncodeString(string data)
        {
            string dataTemp = data;
            do
            {
                DataStreamEncodingMode mode = (isAlphaNumeric(data)) ? DataStreamEncodingMode.AlphaNumeric : DataStreamEncodingMode.Byte;
                AQRDataStreamCharacterPayload temp = (AQRDataStreamCharacterPayload)generateTypicalStructure(mode);

                // Splits the string if it's too big
                if (data.Length > Math.Pow(2, temp.characterCountBitCount))
                {
                    dataTemp = data.Substring(0, (int)(Math.Pow(2, temp.characterCountBitCount) - 1));
                    data = data.Substring((int)(Math.Pow(2, temp.characterCountBitCount)));
                }
                else
                    data = "";

                temp.encodeData(dataTemp);
                EncodedData.Add(temp);
            } while (data.Length > 0);
        }

        /// <summary>
        /// Finds the dependent formatting bits (9-0) that are calculated as follows
        /// bits 14-10 form a polynomial with bit 14 being the coefficient of x^5
        /// the polynomial formed by these bits is multiplied by x^10
        /// That polynomial is then divided by X^10 + X^8 + X^4 + X^2 + X + 1
        /// The remainder polynomial's coefficients make up bits 9-0
        /// </summary>
        /// <returns>Returns a boolean array representing bits 9-0</returns>
        protected bool[] findFormattingBits()
        {
            List<bool> SignificantFormatData = new List<bool>(5);
            SignificantFormatData.AddRange(errorCorrectionLevel.getBooleanData());
            SignificantFormatData.AddRange(maskPattern.getFormatKey());

            bool[] temp_bs = new bool[16];

            // Set up the polynomial for division
            for(int i = 0; i < 5; i++)
                if (SignificantFormatData[i])
                    temp_bs[15 - i] = true;

            // Generator X^10 + X^8 + X^5 + X^4 + X^2 + X + 1
            bool[] generator = new bool[] {true, true, true, false, true, true, false, false, true, false, true,
            false, false, false, false, false };

            // Does the polynomial division
            List<bool> actual_remainder = new List<bool>(10);
            bool[] remainder = (bool[])temp_bs.Clone();
            while(remainder.Contains(true))
            {
                // XOR the remainder and the divisor
                for(int i = 0; i < remainder.Length; i++)
                {
                    remainder[i] = remainder[i] ^ generator[i];
                }

                // Adds the last element of the generator to the actual remainder
                actual_remainder.Add(generator[generator.Length - 1]);
                // Maintains the remainder list size of 10
                if (actual_remainder.Count > 10)
                    actual_remainder.RemoveAt(0);


                // Shifts the generator values down one position
                bool prev = false;
                for(int i = 0; i < generator.Length; i++)
                {
                    bool temp = generator[i];
                    generator[i] = prev;
                    prev = temp;
                }
            }

            return actual_remainder.ToArray();
        }

        /// <summary>
        /// Finds the highest degree of the given polynomial assuming the index represents the base
        /// ie index 0 represents X^0 and index 1 represents X and so on...
        /// </summary>
        /// <param name="p">The polynomical to check</param>
        /// <returns>Returns the highest degreee of the polynomial returns 0 if there wasn't one</returns>
        protected int findHighestDegree(bool[] p)
        {
            for (int i = p.Length - 1; i >= 0; i--)
                if (p[i])
                    return i;
            return 0;
        }

        /// <summary>
        /// Returns the boolean matrix generated by this QR Code when all of the components are added together
        /// </summary>
        /// <returns>Returns a 2D matrix of booleans representing the dots of the matrix [rows, columns]</returns>
        public bool[,] GetBooleanMatrix()
        {
            Queue<bool> bitStream = new Queue<bool>();

            #region Calculates the number of rows and columns

            /// find the total square mass of the booleans in the QR code data
            /// once you insert the 3 corner squares it's going to displace 222 squares which should add:
            /// starting as a column, then alternate between adding rows and columns until no more squares
            /// are displaced
            /// once you add the dashed lines, this will displace x more squares, repeat the process
            /// Now, starting from the left, add in the position markers displacing 25 squares each 
            /// for every 17 rows you have minus the corner squares.
            /// For the surplus created by these, add columns to the right of the current column being calculated
            /// 

            // Calculates the total square coverage of the data being encoded
            // THE READING OF THE DATA BACKWARDS MAKES THE READING OF THE MSB SO EASY LATER ON!!!
            int count = 0;
            for(int i = EncodedData.Count - 1; i >= 0; i--)
            {
                bool[] temp = EncodedData[i].getAllData();
                count += temp.Length;
                foreach(bool b in temp)
                    bitStream.Enqueue(b);   // Appends the data to the list of data AND DOES IT BACKWARDS
            }
            count *= 2;
            int rows = (int)Math.Sqrt((double)count);
            int columns = (int)Math.Sqrt((double)count);

            // If the values aren't perfectly square, add a column
            if (rows * columns < count)
                columns++;

            // Adds rows and columns until the three corner blocks are accounted for
            int displaced = 222;
            bool toggle = false;
            do
            {
                if (toggle)
                {
                    columns += 1;
                    displaced -= rows;
                }
                else
                {
                    rows += 1;
                    displaced -= columns;
                }

                toggle = !toggle;
            }
            while (displaced > 0);

            // Accounts for the dashed lines
            rows++;
            columns++;

            // Handles accounting for the positioner blocks
            int positionerCount = 0;

            // Takes care of the mandatory positioner block
            if (++positionerCount * 25 > rows)
            {
                if (toggle)
                {
                    columns += 1;
                    displaced -= rows;
                }
                else
                {
                    rows += 1;
                    displaced -= columns;
                }

                toggle = !toggle;

                positionerCount = 0;
            }

            for (int i = 0; i < rows - 35; i += rows / 17)
            {
                if (++positionerCount * 25 > rows)
                {
                    if (toggle)
                    {
                        columns += 1;
                        displaced -= rows;
                    }
                    else
                    {
                        rows += 1;
                        displaced -= columns;
                    }

                    toggle = !toggle;

                    positionerCount = 0;
                }
            }

            for (int j = 0; j < columns - 35; j += columns / 17)
            {
                for (int i = 0; i < rows - 35; i += rows / 17)
                {
                    if (++positionerCount * 25 > rows)
                    {
                        if (toggle)
                        {
                            columns += 1;
                            displaced -= rows;
                        }
                        else
                        {
                            rows += 1;
                            displaced -= columns;
                        }

                        toggle = !toggle;

                        positionerCount = 0;
                    }
                }
            }

            if (positionerCount > 0)
            {
                if (toggle)
                {
                    columns += 1;
                    displaced -= rows;
                }
                else
                {
                    rows += 1;
                    displaced -= columns;
                }

                toggle = !toggle;
            }

            #endregion

            bool[,] layout = new bool[rows, columns];
            bool[,] object_mask = new bool[rows, columns];

            // Calculates the constant regions (spacing indicators, position indicators, etc...)
            for(int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    #region Constant data

                    if ((j == 0 && (i < 7 || i > rows - 8)) || (i == 0 && (j < 7 || j > columns - 8)) ||
                        (i == 6 && (j < 7 || j > columns - 8)) || (j == 6 && (i < 7 || i > rows - 8)) ||
                        ((i == rows - 7 || i == rows - 1) && (j < 7)) || ((j == columns - 7 || j == columns - 1) && (i < 7)) ||
                        (((i >= 2 && i <= 4) ||
                        (i >= rows - 5 && i <= rows - 3 && !(j >= columns - 5 && j <= columns - 3)))
                        && ((j >= 2 && j <= 4) ||
                        (j >= columns - 5 && j <= columns - 3 && !(i >= rows - 5 && i <= rows - 3))))) // Corner squares
                    {
                        layout[i, j] = true;
                        object_mask[i, j] = true;
                    }
                    else if (i == 6 && j > 7 && j < columns - 8 && (j % 2) == 0)    // Dashed horizontal
                        layout[i, j] = true;
                    else if (j == 6 && i > 7 && i < rows - 8 && (i % 2) == 0)    // Dshed Vertical
                        layout[i, j] = true;
                    else if ((((i >= 4 && i <= 8) || (j >= 4 && j <= 8)) &&
                        (((j > 10 && i <= 10) || (i > 10 && j <= 10)) && j < columns - 8 && i < rows - 8)) &&
                        ((((i - 8) % 20) == 2) || (((j - 8) % 20) == 2) ||
                        (((i - 8) % 20) == 18) || (((j - 8) % 20) == 18) ||
                        ((((i - 8) % 20) == 0) && (j == 4 || j == 6 || j == 8)) ||
                        ((((j - 8) % 20) == 0) && (i == 4 || i == 6 || i == 8)) ||
                        (((((i - 8) % 20) == 1) || (((i - 8) % 20) == 19)) && (j == 4 || j == 8)) ||
                        (((((j - 8) % 20) == 1) || (((j - 8) % 20) == 19)) && (i == 4 || i == 8)))) // Positioners on the dashed lines
                    {
                        layout[i, j] = true;
                        object_mask[i, j] = true;
                    }
                    else if (((i - 4 != 0 && j - 4 != 0) && (j > 10 && i > 10 && j < columns - 8 && i < rows - 8)) &&
                        ((((((j - 8) % 20) == 0) && (((i - 8) % 20) != 1) && (((i - 8) % 20) != 19)) ||
                        (((j - 8) % 20) == 2) || (((j - 8) % 20) == 18) ||
                        ((((j - 8) % 20) == 1) && (((i - 8) % 20) != 1)) ||
                        ((((j - 8) % 20) == 19) && (((i - 8) % 20) != 19))) &&
                        ((((((i - 8) % 20) == 0) && (((j - 8) % 20) != 1) && (((j - 8) % 20) != 19)) ||
                        ((i - 8) % 20) == 2 || ((i - 8) % 20) == 18) ||
                        ((((i - 8) % 20) == 1) && (((j - 8) % 20) != 19)) ||
                        ((((i - 8) % 20) == 19) && (((j - 8) % 20 != 1))))))    // Positioners not on the dashed lines
                    {
                        layout[i, j] = true;
                        object_mask[i, j] = true;
                    }

                    #endregion
                }
            }

            // Saves the formatting bits that should be calculated now (9-0)
            bool[] formats = findFormattingBits();
            bool[] ECL = errorCorrectionLevel.getBooleanData();
            bool[] Mask = maskPattern.getFormatKey();

            for(int i = columns - 1; i >= 0; i -= 2)
            {
                for(int j = rows - 1; j >= 0; j--)
                {
                    #region Corners

                    if(j == 8 && i > columns - 9)
                    {
                        // Top Right format block
                        layout[rows - 8, 8] = formats[7];
                        layout[rows - 7, 8] = formats[6];
                        layout[rows - 6, 8] = formats[5];
                        layout[rows - 5, 8] = formats[4];
                        layout[rows - 4, 8] = formats[3];
                        layout[rows - 3, 8] = formats[2];
                        layout[rows - 2, 8] = formats[1];
                        layout[rows - 1, 8] = formats[0];
                        break;
                    }
                    else if(j <= 8 && i <= 8)
                    {
                        // Top Left format block
                        layout[8, 0] = ECL[0];
                        layout[8, 1] = ECL[1];
                        layout[8, 2] = Mask[0];
                        layout[8, 3] = Mask[1];
                        layout[8, 4] = Mask[2];
                        layout[8, 5] = formats[9];
                        layout[8, 7] = formats[8];
                        layout[8, 8] = formats[7];
                        layout[7, 8] = formats[6];
                        layout[5, 8] = formats[5];
                        layout[4, 8] = formats[4];
                        layout[3, 8] = formats[3];
                        layout[2, 8] = formats[2];
                        layout[1, 8] = formats[1];
                        layout[0, 8] = formats[0];
                        break;
                    }
                    else if(j > rows - 8 && i <= 8)
                    {
                        // Bottom Left Corner
                        layout[rows - 7, 8] = formats[8];
                        layout[rows - 6, 8] = formats[9];
                        layout[rows - 5, 8] = Mask[2];
                        layout[rows - 4, 8] = Mask[1];
                        layout[rows - 3, 8] = Mask[0];
                        layout[rows - 2, 8] = ECL[1];
                        layout[rows - 1, 8] = ECL[0];
                        break;
                    }

                    #endregion

                    #region corner block breaker

                    // Breaks when the iterator is about to enter the formatting zone, or quiet zone
                    if ((j < 9 && i > columns - 10) ||
                        (i < 9 && (j < 9 || j > rows - 10)))
                        break;

                    #endregion

                    #region standard snake

                    // We hit a position block, which isn't accurately represented in the object map
                    // Therefore we need to fast forward until we hit the next wall
                    // Otherwise we should never hit two trues in the object map at the same time
                    // except for when we hit the corner blocks, and we're going to abort before we even hit that
                    // since there's supposed to be a quiet zone.
                    if(object_mask[i, j] && object_mask[i - 1, j])
                    {
                        do { j--; } while (object_mask[i, j] || object_mask[i - 1, j]);
                    }

                    if (!object_mask[i, j])
                        layout[i, j] = bitStream.Dequeue();
                    if (!object_mask[i - 1, j])
                        layout[i - 1, j] = bitStream.Dequeue();

                    #endregion
                }
            }

            return layout;
        }

        #endregion
    }
}
