using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tempWinForms
{
    class Cryptographer
    {
        public static string Decipher(string str)
        {
            string res = string.Empty;

            int i = 0;

            while (true)
            {
                if (i >= str.Length - 1) break;

                if (CryptKey.IsMyBorder(str[i]))
                {
                    string temp = str.Substring(i, 4);

                    i += 4;

                    var cryptKey = new CryptKey(temp);

                    while (!CryptKey.IsMyBorder(str[i]))
                    {
                        if (cryptKey.Seporetor == str[i])
                        {
                            if (cryptKey.SeporetorPositions.ToString() == "a")
                            {
                                int j = 0;

                                while (_numbers.Contains(str[--j + i])) { }

                                res += (char)((int.Parse(str.Substring(++j + i, Math.Abs(j))) + 'a') / (cryptKey.Offset - 30));
                            }
                            else if (cryptKey.SeporetorPositions.ToString() == "b")
                            {
                                int j = 0;

                                while (++j + i < str.Length && _numbers.Contains(str[j + i])) { }

                                res += (char)((int.Parse(str.Substring(i + 1, j - 1)) + 'b') / (cryptKey.Offset - 30));
                            }
                        }

                        i++;

                        if (i >= str.Length) break;
                    }

                    i--;
                }
                else
                {
                    i++;
                }
            }
            return res;
        }

        public static string Cipher(string str)
        {
            string res = string.Empty;

            Random random = new Random();

            for (int i = 0; i < str.Length;)
            {
                int size = random.Next(1, str.Length % 10 + 3);

                CryptKey cryptKey = new CryptKey(random);

                res += cryptKey.ToString();

                for (int j = 0; j < size; j++)
                {
                    if (j + i >= str.Length)
                        break;

                    res += GenerateRandomString(cryptKey, random);

                    if (cryptKey.SeporetorPositions.ToString() == "b")
                    {
                        res += cryptKey.Seporetor;

                        res += (int)str[i + j] * ((int)cryptKey.Offset - 30) - (int)'b';
                    }
                    else if (cryptKey.SeporetorPositions.ToString() == "a")
                    {
                        res += (int)str[i + j] * ((int)cryptKey.Offset - 30) - (int)'a';

                        res += cryptKey.Seporetor;
                    }
                }

                i += size;
            }

            return res;
        }

        private class CryptKey
        {
            public override string ToString()
            {
                char[] temp = { Border, Seporetor, SeporetorPositions.ToString().ToCharArray()[0], Offset };

                return new string(temp);
            }

            public CryptKey(Random random)
            {
                _border = _borders[random.Next(0, _borders.Length - 1)];

                _seporetor = GenerateRandomChar(random, _borders.Concat(_numbers).ToArray());

                _seporetorPosition = new SeporetorPositions(random);

                _offset = GenerateRandomChar( random, _borders.Concat(_numbers).ToArray());
            }

            public CryptKey(string cryptKey)
            {
                if (cryptKey.Length != 4)
                    throw new Exception("не ключ");

                _border = cryptKey[0];

                _seporetor = cryptKey[1];

                _seporetorPosition = new SeporetorPositions(cryptKey[2]);

                _offset = cryptKey[3];
            }

            public static bool IsMyBorder(char border) => _borders.Contains(border);

            private readonly char _border;

            private readonly char _seporetor;

            private readonly SeporetorPositions _seporetorPosition;

            private readonly char _offset;

            public char Border => _border;
            public char Seporetor  => _seporetor;
            public SeporetorPositions SeporetorPositions => _seporetorPosition;
            public char Offset => _offset;
        }

        class SeporetorPositions
        {
            public SeporetorPositions(Random random)
            {
                SeporPosition = random.Next(0, 101) % 2 == 1 ? SeporetorPosition.a : SeporetorPosition.b;
            }

            public SeporetorPositions(char seporetorPosition)
            {
                SeporPosition = ('a' == seporetorPosition) ? SeporetorPosition.a :
                    ('b' == seporetorPosition) ? SeporetorPosition.b : throw new Exception("не позиция");
            }

            public override string ToString() => SeporPosition.ToString();

            public SeporetorPosition SeporPosition { set; get; }

            public enum SeporetorPosition
            {
                b = -1,
                a = 1
            }
        }

        private static string GenerateRandomString(CryptKey cryptKey, Random random)
        {
            char[] badValues = _borders.Concat(cryptKey.Seporetor.ToString()).ToArray();

            var badValuesWhithNombers = badValues.Concat(_numbers).ToArray();

            int length = random.Next(2, 5);

            StringBuilder sbuilder = new StringBuilder();

            sbuilder.Append(GenerateRandomChar(random, badValuesWhithNombers));

            for (int x = 1; x < length - 1; ++x)
            {
                sbuilder.Append(GenerateRandomChar(random, badValuesWhithNombers));

                sbuilder.Append((int)GenerateRandomChar(random, badValues) * ((int)cryptKey.Offset - 30) - (int)'a');

                sbuilder.Append(GenerateRandomChar(random, badValuesWhithNombers));
            }

            sbuilder.Append(GenerateRandomChar(random, badValuesWhithNombers));

            return sbuilder.ToString();
        }

        private static char GenerateRandomChar(Random random, char[] badValues)
        {
            char ch;
            do
            {
                ch = (char)random.Next(32, 126);

            } while (badValues.Contains(ch));

            return ch;
        }

        private static readonly char[] _borders = { '"', '\'', '[', ']', '{', '}', '(', ')', '/', '\\', ';' };

        private static readonly char[] _numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    }
}
