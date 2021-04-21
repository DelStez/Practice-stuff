using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Word;

namespace FindSecretInWordFile
{
    class Program
    {
        public static Encoding encoding = Encoding.GetEncoding("windows-1251");
        public static Encoding encoding1 = Encoding.GetEncoding("koi8r");
        public static Encoding encoding2 = Encoding.GetEncoding("cp866");
        public static string path = "C:\\Users\\Kanda\\Desktop\\variant02.docx";
        static void BitArrayToString(List<bool> bits)
        {
            byte[] strArr = new byte[bits.Count];

            for (int i = 0, k = 0; i < bits.Count; i += 8, k++)
            {
                List<bool> t = new List<bool>(bits.Skip(i).Take(8).ToArray());
                t.Reverse();
                BitArray n = new BitArray(t.ToArray());
                n.CopyTo(strArr, k);

            }
            string result = encoding2.GetString(strArr);
            Console.WriteLine(result);
        }
        static void Main(string[] args)
        {
            Application application = new Application();
            Document document = application.Documents.Open(path);
            List<bool> findMess = new List<bool>();
            int count = document.Characters.Count;
            for (int i = 1; i <= count; i++)
            {
                findMess.Add(false);
                Console.WriteLine("{0} - {1} - {2} - {3} - {4}-{5}", document.Characters[i].Text,
                document.Characters[i].Font.NameAscii.ToString(),
                document.Characters[i].Font.Color.ToString(),
                document.Characters[i].Font.Size.ToString(),
                document.Characters[i].Font.Scaling.ToString(), document.Characters[i].Font.TextColor.ToString());
                //if (document.Characters[i].Font.NameAscii.ToString() != "Segoe Print" ||
                //    document.Characters[i].Font.Size != 12 ||
                //    document.Characters[i].Font.Scaling != 100 || document.Characters[i].Text != ((char)182).ToString())
                //{
                //    findMess.Add(true);
                //    Console.WriteLine("{0} - {1} - {2} - {3} - {4}", document.Characters[i].Text,
                //    document.Characters[i].Font.NameAscii.ToString(),
                //    document.Characters[i].Font.Color.ToString(),
                //    document.Characters[i].Font.Size.ToString(),
                //    document.Characters[i].Font.Scaling.ToString());
                //}
                //else if (document.Characters[i].Text != ((char)182).ToString())
                //{
                //    findMess.Add(false);
                //    Console.WriteLine("{0} - {1} - {2} - {3} - {4}", document.Characters[i].Text,
                //    document.Characters[i].Font.NameAscii.ToString(),
                //    document.Characters[i].Font.Color.ToString(),
                //    document.Characters[i].Font.Size.ToString(),
                //    document.Characters[i].Font.Scaling.ToString());
                //}

            }
            BitArrayToString(findMess);
            Console.ReadLine();
            application.Quit();
        }
    }
}
