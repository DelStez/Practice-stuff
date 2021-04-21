using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace GOST34
{
    class Program
    {
        public static string c3 = "FF00FFFF000000FFFF0000FF00FFFF0000FF00FF00FF00FFFF00FF00FF00FF00";
        public static byte[] c24 = new byte[]
        {
            0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0
        };

        public static int[,] sbox = {
               { 9, 6,  3,  2,  8,  11,  1,  7,  10, 4,  14, 15, 12, 0,  13, 5},
               { 3, 7,  14, 9,  8,  10,  15, 0,  5,  2,  6,  12, 11, 4,  13, 1},
               { 14, 4, 6, 2,   11,  3,  13, 8,  12, 15, 5,  10, 0,  7,  1,  9},
               { 14, 7, 10, 12, 13,  1,  3,  9,  0,  2,  11, 4,  15, 8,  5,  6},
               { 11, 5, 1,   9,  8,  13, 15, 0,  14,  4,  2,  3,  12,  7,  10,  6},
               { 3,   10,   13, 12, 1,   2,  0,  11,  7,  5, 9,  4,  8,  15,  14,  6},
               { 1,  13,   2,   9,  7,  10,   6,   0, 8, 12,  4, 5,  15,  3,  11,  14},
               { 11,   10,   15,   5,   0,   12,   14,   8,   6,   2,   3,   9,   1,  7,  13,  4}
        };

        static void Main(string[] args)
        {
            long p = 31991;
            long a = -3;
            long b = 1000;
            long n = 32089;
            string message = "This is message, length=32 bytes";
            byte[] h = new byte[32];
            byte[] bytes = Encoding.ASCII.GetBytes(message);
            Console.WriteLine(ByteArrayToString(bytes));
            //bytes = Check(bytes);
            GetHout(h, bytes);
            Random random = new Random();
            long k = random.Next(1, (int)n-1);
            Console.Read();

        }
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        public static byte[] Check(byte[] m)
        {
            List<byte> n = m.ToList();
            while (n.Count % 32 != 0)
                n.Add(0);
            byte[] result = n.ToArray();
            return result;
        }
        //Функция сжатия внутренних итераций (по ГОСТ “шаговая функция хэширования”) 

        //Генерирования ключей;
        #region GenKey
        public static List<byte[]> GenerateKeys(byte[] m, byte[] h)
        {
            byte[] U = h;
            byte[] V = m;
            var p = new BitArray(U).Xor(new BitArray(V));
            byte[] W = new byte[32];
            p.CopyTo(W,0);
            List<byte[]> Keys = new List<byte[]>();
            byte[] t = new byte[32];
            W.CopyTo(t, 0);
            Keys.Add(P(W.Reverse().ToArray()));
            for (int j = 2; j <= 4; j++)
            {
                    
                    p = new BitArray(A(U.Reverse().ToArray())).Xor(j == 3 ? ConvertHexToBitArray(c3) : new BitArray(c24));
                    p.CopyTo(U, 0);
                    p = new BitArray(A(A(V.Reverse().ToArray()).Reverse().ToArray()));
                    p.CopyTo(V, 0);
                    p = new BitArray(U).Xor(new BitArray(V));
                    p.CopyTo(W,0);
                    Keys.Add(P(W.Reverse().ToArray()));
            }
            return Keys;
        }
        public static BitArray ConvertHexToBitArray(string hexData)
        {
            if (hexData == null)
                return null;

            BitArray ba = new BitArray(4 * hexData.Length);
            for (int i = 0; i < hexData.Length; i++)
            {
                byte b = byte.Parse(hexData[i].ToString(), NumberStyles.HexNumber);
                for (int j = 0; j < 4; j++)
                {
                    ba.Set(i * 4 + j, (b & (1 << (3 - j))) != 0);
                }
            }
            return ba;
        }
        public static byte[] A(byte[] currentMblock)
        {
            BitArray getBits = new BitArray(currentMblock);
            bool[] n1 = new bool[256];
            getBits.CopyTo(n1, 0);
            List<bool> n = new List<bool>(n1);
            BitArray y1 = new BitArray(n.Take(64).ToArray());
            BitArray y2 = new BitArray(n.Skip(64).Take(64).ToArray());
            BitArray y3 = new BitArray(n.Skip(128).Take(64).ToArray());
            BitArray y4 = new BitArray(n.Skip(192).Take(64).ToArray());
            var t = y1.Xor(y2);
            t.CopyTo(n1, 0); y4.CopyTo(n1, 64); y3.CopyTo(n1, 128); y2.CopyTo(n1, 192);
            byte[] A = new byte[32];
            new BitArray(n1).CopyTo(A, 0);
            return A;
        }

        public static byte[] P(byte[] currentMblock)
        {
            byte[] n = new byte[32];
            int k = 0, j = 0;
            int index = 31, maxk = 31;
            while(index !=-1)
            {
               
                n[maxk - k] = currentMblock[index];
                k += 4;
                index--;
                j++;
                if (k == 32)
                {
                    j = 0;
                    maxk--;
                    k = 0;
                }

            }
            return n;
        }
        //функция Эйлера
        static int Eyler(int n)
        {
            int res = n, en = Convert.ToInt32(Math.Sqrt(n) + 1);
            for (int i = 2; i <= en; i++)
                if ((n % i) == 0)
                {
                    while ((n % i) == 0)
                        n /= i;
                    res -= (res / i);
                }
            if (n > 1) res -= (res / n);
            return res;
        }
        #endregion GenKey
        //Шифрующего преобразования;
        #region cipherChanges
        public static byte[] CipherChange(byte[] h, byte[] m)
        {
            BitArray bitArray = new BitArray(h);
            bool[] n1 = new bool[256];
            bitArray.CopyTo(n1, 0);
            bool[] n2 = new bool[256];
            List<byte[]> keys = new List<byte[]>(GenerateKeys(m, h));
            for (int i = 0, k = 0; i < 256; i += 64, k += 1)
            {
                byte[] temp = new byte[8];
                var t = n1.Skip(i).Take(64).ToArray();
                new BitArray(t).CopyTo(temp, 0);
                new BitArray(Feistel(temp, keys[k])).CopyTo(n2, i);
            }
            byte[] result = new byte[32];
            new BitArray(n2.Reverse().ToArray()).CopyTo(result, 0);
            return result;
        }

        public byte[] ConvertToByte(ulong[] fl)
        {
            byte[] temp = new byte[8];
            byte[] encrByteFile = new byte[fl.Length * 8];

            for (int i = 0; i < fl.Length; i++)
            {
                temp = BitConverter.GetBytes(fl[i]);

                for (int j = 0; j < temp.Length; j++)
                    encrByteFile[j + i * 8] = temp[j];
            }

            return encrByteFile;
        }
        public uint[] GetUIntKeyArray(byte[] byteKey)
        {
            uint[] key = new uint[8];

            for (int i = 0; i < key.Length; i++)
            {
                key[i] = BitConverter.ToUInt32(byteKey, i * 4);
            }

            return key;
        }
        public ulong[] GetULongDataArray(byte[] byteData)
        {
            ulong[] data = new ulong[byteData.Length / 8];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = BitConverter.ToUInt64(byteData, i * 8);
            }

            return data;
        }
        public static byte[] Feistel(byte[] data, byte[] key) // Фейстель
        {
            BitArray n = new BitArray(data);
            bool[] n1 = new bool[256];
            n.CopyTo(n1,0);
            BitArray A = new BitArray(n1.ToList().Take(32).ToArray());
            BitArray B = new BitArray(n1.ToList().Skip(32).Take(32).ToArray());
            int k = 0;
            for (int i = 0; i < 32; i++)
            {
                if (i > 23)
                {
                    BitArray t = new BitArray(A);
                    A = B.Xor(F(A, new BitArray(n1.ToList().Skip(k).Take(32).ToArray())));
                    k++;
                    if (k == 8)
                    {
                        k = 0;
                    }
                }
                else
                {
                    BitArray t = new BitArray(A);
                    A = B.Xor(F(A, new BitArray(n1.ToList().Skip(k).Take(32).ToArray())));
                    k--;
                    if (k == -1)
                    {
                        k = 7;
                    }
                }
            }
            byte[] result = new byte[8];
            A.CopyTo(result, 0);
            B.CopyTo(result, 3);
            return result;
        }

        private static BitArray F(BitArray A, BitArray B)// функция F 
        {
            A = A.Xor(B);
            bool[] n = new bool[32];
            A.CopyTo(n,0);
            bool[] result = new bool[32];
            for (int i = 0, k = 0; i < 32; i+=4,k++)
            {
                int t = getIntFromBitArray(new BitArray(n.Skip(i).Take(4).ToArray()));
                byte b = Convert.ToByte(sbox[k,t]);
                bool[] temp = new bool[32];
                new BitArray(b).CopyTo(temp, 0);
                temp.Take(4).ToArray().CopyTo(result, i);
            }
            bool[] resultShift = new bool[32];
            result.Skip(11).Take(result.Length - 11).ToArray().CopyTo(resultShift, 0);
            result.Take(11).ToArray().CopyTo(resultShift, result.Length - 11);
            return new BitArray(resultShift);
        }
        public static int getIntFromBitArray(BitArray bitArray)
        {
            int value = 0;
            for (int i = 0; i < bitArray.Count; i++)
            {
                if (bitArray[i])
                    value += Convert.ToInt16(Math.Pow(2, i));
            }
            return value;
        }
        #endregion cipherChanges
        // Перемешивающего преобразования

        #region Hash
        public static BitArray PsiFunc(BitArray current)
        {
            bool[] getArray = new bool[256];
            current.CopyTo(getArray, 0);
            List<bool[]> temp = new List<bool[]>();
            for (int i = 0; i < 256; i += 16)
            {
                temp.Add(getArray.Skip(i).Take(16).ToArray());
            }
            bool[] result = new bool[256];
            var t = new BitArray(temp[0]).Xor(new BitArray(temp[1]))
                                         .Xor(new BitArray(temp[2]))
                                         .Xor(new BitArray(temp[3]))
                                         .Xor(new BitArray(temp[12]))
                                         .Xor(new BitArray(temp[15]));
            t.CopyTo(result, 0);
            for (int i = 0, k = 16; k < 256; i++, k += 16)
            {
                temp[15 - i].CopyTo(result, k);
            }
            return new BitArray(result);
        }
        public static void GetHout(byte[] h, byte[] m)
        {

            BitArray S = new BitArray(CipherChange(h, m));
            BitArray Mblock = new BitArray(m);
            BitArray Hin = new BitArray(h);
            for (int i = 0; i < 12; i++)
                S = PsiFunc(S);
            var t = PsiFunc(S.Xor(Mblock));
            var r = t.Xor(Hin);
            for (int i = 0; i < 61; i++)
                r = PsiFunc(r);
            byte[] hout = new byte[32];
            r.CopyTo(hout, 0);
            Console.WriteLine(ByteArrayToString(hout));
        }
        #endregion Hash

    }
}
