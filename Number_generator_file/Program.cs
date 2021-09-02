using System;
using System.IO;
using System.Text;

namespace Number_generator_file
{
  internal class Program
  {
    public static int numberOfFiles, amountOfNumbers;// Never gonna give you up
    public static Random rd = new Random();
    
    public static void Main(string[] args)
    {
        Console.Write("What a number of Files?: ");
        numberOfFiles = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < numberOfFiles; i++)
        {
            Console.Write("What an amount of Numbers?: ");
            amountOfNumbers = Convert.ToInt32(Console.ReadLine());
            string nameFile = "temp-" + Convert.ToString(i)+".txt";
            using (FileStream writeFile = File.Create(nameFile))
            {
                for (int j = 0; j < amountOfNumbers ; j++)
                {
                    string number = Convert.ToString(rd.Next(0, 100));
                    if (j != amountOfNumbers - 1)
                    {
                        number += " ";
                    }

                    var temp = new UTF8Encoding(true).GetBytes(number);
                    writeFile.Write(temp, 0, temp.Length);
                }
            }
        }
    }
  }
}