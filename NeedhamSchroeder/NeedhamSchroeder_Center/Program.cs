using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NeedhamSchroeder_Center
{
    class Program
    {
        static void Main(string[] args)
        {
            bool useCBC = true;
            byte[] IV = new byte[] { 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7 };
            byte[] key = new byte[] { 0x8, 0x7, 0x5, 0x6, 0x3, 0x2, 0x1, 0x3, 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7 };
            byte[] aliceKey = new byte[] { 0x7, 0x6, 0x5, 0x4, 0x3, 0x2, 0x1, 0x0, 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7 };

            string response = StartListening("asdf", IV, key, aliceKey);
            Console.WriteLine("kdc recieves N1, \"Alice Wants Bob\", Kbob{Nb}");
            Console.WriteLine("kdc sends N1, \"Bob\", shared Key, and a ticket to Bob");
            Console.ReadLine();
        }
        public static string StartListening(string toSend, byte[] IV, byte[] key, byte[] keyAlice)
        {

            string data = null;
            string toReturn = null;
            byte[] bytes = new Byte[1024];

            IPAddress addr = IPAddress.Loopback;

            IPEndPoint localEndPoint = new IPEndPoint(addr, 12000);

            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);
                Socket handler = listener.Accept();
                data = null;
                while (true)
                {
                    bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data += getString(bytes);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        data = data.Substring(0, data.IndexOf("<EOF>"));
                        break;
                    }
                }

                string[] messageArray = data.Split(new string[] { "987654321" }, StringSplitOptions.None);

                toReturn = Decrypt(messageArray[1], key, IV);

                string sharedKey = getSharedKey();

                // construct message to send
                string ticketMessage = sharedKey + "987654321" + toReturn;

                string ticket = encryptMessage(key, IV, getBytes(ticketMessage));

                string firstPart = encryptMessage(keyAlice, IV, getBytes(messageArray[0] + "987654321" + sharedKey + "987654321" + ticket));

                toSend = firstPart;
                handler.Send(getBytes(toSend + "<EOF>"));
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return data;
        }


        public static string Decrypt(string cipherBlock, byte[] key, byte[] IV)
        {
            byte[] toEncryptArray = getBytes(cipherBlock);
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = key;
            tdes.IV = IV;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.Zeros;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();

            return getString(resultArray);
        }

        public static string getString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);

        }

        public static byte[] getBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string getSharedKey()
        {
            byte[] bytes = new byte[] { 0x4, 0x3, 0x2, 0x1, 0x4, 0x3, 0x2, 0x1, 0x1, 0x2, 0x3, 0x4, 0x1, 0x2, 0x3, 0x4 };

            return getString(bytes);
        }

        public static string encryptMessage(byte[] key, byte[] IV, byte[] message)
        {
            byte[] keyBytes = key;
            byte[] messageBytes = message;
            byte[] ivBytes = IV;

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyBytes;
            tdes.IV = ivBytes;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.Zeros;
            ICryptoTransform encryptor = tdes.CreateEncryptor();
            byte[] encResult = encryptor.TransformFinalBlock(messageBytes, 0, messageBytes.Length);
            tdes.Clear();
            string toReturn = getString(encResult);
            byte[] test = getBytes(toReturn);
            return toReturn;

        }
    }
}
