using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Numerics;
using System.Collections.Generic;
using System.Collections;
using System.Security.Cryptography;

namespace NeedhamSchroeder_Server
{
    // "Боб" является сервером 

    class Program
    {
        //Входные данные:
        //вектор
        static byte[] IV = new byte[] { 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7 };
        //ключ
        static byte[] key = new byte[] { 0x8, 0x7, 0x5, 0x6, 0x3, 0x2, 0x1, 0x3, 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7 };
        static int port = 11000;
        static void Main(string[] args)
        {
            //прошлушивание портов
            string repons = ListeningStart(IV, key, port);
            Console.ReadLine();
        }
        public static string ListeningStart(byte[] IV, byte[] key, int port)
        {
            string data = null, result = null;

            // буфер для входящих данных
            byte[] bytes = new Byte[1024];

            byte[] nonce = new byte[64];

            //насторйка портов
            IPAddress addr = IPAddress.Loopback;
            IPEndPoint localEndPoint = new IPEndPoint(addr, port);

            // tcp/ip
            Socket listener = new Socket(AddressFamily.InterNetwork,
               SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Program is suspended while waiting for an incoming connection.
                Socket handler = listener.Accept();
                data = null;

                // An incoming connection needs to be processed.
                while (true)
                {
                    bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    //data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    data += getString(bytes);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        data = data.Substring(0, data.IndexOf("<EOF>"));
                        break;
                    }
                }

                result = data;

                string toSend;

                int count = 0;

                //Определение типа сообщения
                switch (result)
                {
                    case "startChat":
                        Random rnd = new Random();
                        rnd.NextBytes(nonce);
                        nonce = getBytes("1111111111111111");
                        toSend = encryptMessage(key, IV, nonce);
                        break;
                    default:
                        if (count == 0)
                        {
                            count++;
                            string[] splitString = result.Split(new string[] { "987654321" }, StringSplitOptions.None);
                            string decryptedTicket = Decrypt(splitString[0].Substring(0, splitString[0].Length - 2), key, IV);
                            string[] splitTicket = decryptedTicket.Split(new string[] { "987654321" }, StringSplitOptions.None);

                            string sharedKey = splitTicket[0];
                            string originalNonce = splitTicket[1].Substring(0, splitTicket[1].Length - 3);
                            string nonce2 = Decrypt(splitString[1], getBytes(sharedKey), IV);
                            Int64 temp;
                            Int64.TryParse(nonce2, out temp);
                            string nonce2Minus = (temp - 1).ToString("D32");
                            string nonce3 = NextInt64().ToString("D32");
                            toSend = encryptMessage(getBytes(sharedKey), IV, getBytes(nonce2Minus + nonce3));

                        }
                        else if (count == 1)
                        {
                            return "finished";
                        }
                        else
                        {
                            toSend = null;
                        }

                        break;

                }
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
        //шифрование
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

            string result = getString(encResult);
            byte[] test = getBytes(result);
            return result;

        }
        //перевод в строку
        public static string getString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
        //перевод в байты
        public static byte[] getBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        // расшифровка
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
        public static Int64 NextInt64()
        {
            Random rnd = new Random();
            var buffer = new byte[sizeof(Int64)];
            rnd.NextBytes(buffer);
            return BitConverter.ToInt64(buffer, 0);
        }
    }

}
