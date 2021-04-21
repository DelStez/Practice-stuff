using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Security.Cryptography;

namespace NeedhamSchroeder_Client
{
    static class Program
    {
        public const int numberSize = 32;

        static void Main(string[] args)
        {
                byte[] aliceKey = new byte[] { 0x8, 0x7, 0x5, 0x6, 0x3, 0x2, 0x1, 0x3, 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7 };
                byte[] IV = new byte[] { 0x0, 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7 };

                string message = "startChat";

                Console.WriteLine("Алиса сообщила Бобу о начале обмена");
                string bobsFirstMessage = sendMessage(message, 11000);
                string nonce1 = getNonce();
                string messageForKDC = nonce1 + "987654321" + bobsFirstMessage;

                Console.WriteLine("Alice отправляет N1, \"Alice Wants Bob\"");
                string kdcResponse = sendMessage(messageForKDC, 12000);
                Console.WriteLine("Алиса получила ответ от центра");
                string kdcResponseDecrypted = Decrypt(kdcResponse, aliceKey, IV);

                string[] messageSplits = kdcResponseDecrypted.Split(new string[] { "987654321" }, StringSplitOptions.None);
                if (nonce1 != messageSplits[0])
                {
                    return;
                }
                // Получение общего ключа
                string sharedKey = messageSplits[1];
                string ticket = messageSplits[2];
                Int64 nonce2 = NextInt64();
                string encryptedNonce2 = encryptMessage(getBytes(sharedKey), IV, getBytes(nonce2.ToString()));

                Console.WriteLine("Алиса отправляет билет Бобу вместе с N2, зашифрованным с помощью общего ключа");
                string bobsResponse = sendMessage(ticket + "987654321" + encryptedNonce2, 11001);
                string bobDecrypted = Decrypt(bobsResponse, getBytes(sharedKey), IV);

                // извлечение информации
                string nonce2Minus = bobDecrypted.Substring(0, 32);
                string nonce3 = bobDecrypted.Substring(32, 32);

                Int64 nonce2After;
                Int64.TryParse(nonce2Minus, out nonce2After);

                if (nonce2After + 1 != nonce2)
                {
                    return;
                }

                Int64 nonce3Minus;
                Int64.TryParse(nonce3, out nonce3Minus);
                nonce3Minus--;
                Console.WriteLine("Алиса отвечает кодом nonce3Minus, зашифрованным общим ключом");
                sendMessage(nonce3Minus.ToString(), 11002);

                Console.Read();
            Console.ReadLine();
        }

            public static string sendMessage(string toSend, int port)
            {
                byte[] bytes = new byte[1024];

                string result = null;
                try
                {

                    IPAddress ipAddress = IPAddress.Loopback;
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                    Socket sender = new Socket(AddressFamily.InterNetwork,
                        SocketType.Stream, ProtocolType.Tcp);
                    try
                    {
                        sender.Connect(remoteEP);
                        byte[] msg = getBytes(toSend + "<EOF>");
                        int bytesSent = sender.Send(msg);
                        int bytesRec = sender.Receive(bytes);
                        result = getString(bytes);
                        result = result.Substring(0, result.IndexOf("<EOF>"));
                        sender.Shutdown(SocketShutdown.Both);
                        sender.Close();

                    }
                    catch (ArgumentNullException ane)
                    {
                        Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                        return null;
                    }
                    catch (SocketException se)
                    {
                        Console.WriteLine("SocketException : {0}", se.ToString());
                        return null;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Unexpected exception : {0}", e.ToString());
                        return null;
                    }


                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                return result;
            }

            public static string getNonce()
            {
                Random rnd = new Random();
                byte[] result = new byte[64];
                rnd.NextBytes(result);
                return getString(result);
            }

            public static Int64 NextInt64()
            {
                Random rnd = new Random();
                var buffer = new byte[sizeof(Int64)];
                rnd.NextBytes(buffer);
                return BitConverter.ToInt64(buffer, 0);
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
