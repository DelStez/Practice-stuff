using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MergeSort
{
    public class Tools
    {
        public void CreateCopy(string mainFile, string resultFile)
        {
            using (StreamReader originalFile = File.OpenText(mainFile))
            {
                using (StreamWriter copyFile = new StreamWriter(resultFile))
                {
                    //Reading main file
                    string line = String.Empty;
                    while ((line = originalFile.ReadLine()) != null)
                    {
                        copyFile.WriteLine(line);
                    }
                }
            }
        }
        public bool isSorter(string pathMainFile)
        {
            using (StreamReader af1 = File.OpenText(pathMainFile))
            {
                string a1 = "0";
                string a2 = "0";
                a1 = af1.ReadLine();
                a2 = af1.ReadLine();
                while (a1 != null && a2  != null)
                {
                    if (Convert.ToInt32(a1) > Convert.ToInt32(a2))
                        return false;
                    else
                    {
                        a1 = a2;
                        a2 = af1.ReadLine();
                    }
                }        
            }
            return true;
        }
    }
}