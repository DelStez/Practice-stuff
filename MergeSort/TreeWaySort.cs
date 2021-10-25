using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MergeSort
{
    public class TreeWaySort
    {
        public string nameAdditional = "tempTw.ms";
        public string resultfile = "3Wresult.txt";
        public Tools tools = new Tools();
        public List<string> pathAdditional = new List<string>();
        public DateTime time1;
        public DateTime time2;
        public TimeSpan elapsedTime;
        

        public TreeWaySort(string pathMain)
        {
            for (int i = 0; i <= 3; i++)
            {
                var name = (i != 3) ? Convert.ToString(i) + nameAdditional : resultfile;
                pathAdditional.Add(name);
                FileStream temp = new FileStream(name, FileMode.OpenOrCreate);
                temp.Close();
                
            }
            tools.CreateCopy(pathMain, resultfile);
        }
        public string StartSort()
        {
            Sort(resultfile);
            return resultfile;
        }
        #region SortingThree
        public void SplitToFiles()
        { 
            //Creating additional files
            using (StreamReader mainFile = File.OpenText(resultfile))
            {
                using (StreamWriter fs1 = new StreamWriter("0"+nameAdditional), fs2 = new StreamWriter("1"+nameAdditional), fs3 = new StreamWriter("2"+nameAdditional))
                {
                    //Reading main file
                    string line = string.Empty;
                    int i = 0;
                    while (( line = mainFile.ReadLine()) != null)
                    {
                        if (i == 4) i = 0;
                        var t = new UTF8Encoding(true).GetBytes(Convert.ToString(line));
                        if (i % 3 == 0 && i % 2 != 0)
                            fs1.WriteLine(line);
                        else if(i % 2 == 0)
                            fs2.WriteLine(line);
                        else
                            fs3.WriteLine(line);
                        i++;
                    }
                }
            }
        }
        public void Sort(string pathMainFile)
        {
            int k = 1;
            string a1;
            string a2, a3;
            bool b;
            int f1 = 0, f2 = 0;
            time1 = DateTime.Now;
            while (true) 
            {
                if(tools.isSorter(pathMainFile)) 
                    break;
                SplitToFiles();
                using (StreamReader af1 = File.OpenText(pathAdditional[0]), af2 = File.OpenText(pathAdditional[1]),af3 = File.OpenText(pathAdditional[2]) )
                {
                    using (StreamWriter fs = new StreamWriter(pathMainFile))
                    {
                        string lineOfFile1 = string.Empty, lineOfFile2 = string.Empty; 
                        a1 = af1.ReadLine();
                        a2 = af2.ReadLine();
                        a3 = af3.ReadLine();
                        b = true;
                        while (a2 != null && a1 != null && a3 != null)
                        {
                            int i = 0, j = 0, z = 0;
                            while ((i < k || j < k || z < k) && a2 != null && a1 != null && a3 != null)
                            {
                                if (Convert.ToInt32(a1) < Convert.ToInt32(a2))
                                {
                                    if(Convert.ToInt32(a1) < Convert.ToInt32(a3))
                                    {
                                        fs.WriteLine(a1);
                                        a1 = af1.ReadLine();
                                        i++;
                                    }
                                    else
                                    {
                                        fs.WriteLine(a3);
                                        a3 = af3.ReadLine();
                                        z++;
                                    }

                                }
                                else
                                {
                                    if(Convert.ToInt32(a2) < Convert.ToInt32(a3))
                                    {
                                        fs.WriteLine(a2);
                                        a2 = af2.ReadLine();
                                        j++;
                                    }
                                    else
                                    {
                                        fs.WriteLine(a3);
                                        a3 = af3.ReadLine();
                                        z++;
                                    }
                                }
                            }
                            while (i < k && a1 != null)
                            {
                                fs.WriteLine(a1);
                                a1 = af1.ReadLine();
                                i++;
                            }
                            while (j < k && a2 != null)
                            {
                                fs.WriteLine(a2);
                                a2 = af2.ReadLine();
                                j++;
                            } 
                            while (z < k && a3 != null)
                            {
                                fs.WriteLine(a3);
                                a3 = af3.ReadLine();
                                z++;
                            }
                        }
                        while (a1 != null)
                        {
                            fs.WriteLine(a1);
                            a1 = af1.ReadLine();
                        }
                        while (a2 != null)
                        {
                            fs.WriteLine(a2);
                            a2 = af2.ReadLine();
                        }
                        while (a3 != null)
                        {
                            fs.WriteLine(a3);
                            a3 = af3.ReadLine();
                        }
                        k*=3;
                    }
                }
            }
            
            time2 = DateTime.Now;
            elapsedTime = time2 - time1;
       
        }
        

        #endregion
        

    }

    public class TwoWaySort
    {
        public string mainCopyPath = "2WSort.txt";
        public string nameAdditional = "tempTw.mms";
        public Tools tools = new Tools();
        public List<string> pathAdditional = new List<string>();
        public DateTime time1;
        public DateTime time2;
        public TimeSpan elapsedTime;

        public TwoWaySort(string pathMain)
        {
            for (int i = 0; i <= 3; i++)
            {
                var name = (i != 3) ? Convert.ToString(i) + nameAdditional : mainCopyPath;
                pathAdditional.Add(name);
                FileStream temp = new FileStream(name, FileMode.OpenOrCreate);
                temp.Close();
                
            }
            tools.CreateCopy(pathMain, mainCopyPath);
        }
        #region Sorting

        public string startSort()
        {
            SimpleMergeSort(mainCopyPath);
            return mainCopyPath;
        }

        public void SplitMainFile(string pathMainFile)
        {
            using (StreamReader mainFile = File.OpenText(pathMainFile))
            {
                using (StreamWriter fs1 = new StreamWriter(pathAdditional[0]), fs2 = new StreamWriter(pathAdditional[1]))
                {
                    //Reading main file
                    string line = string.Empty;
                    int i = 0;
                    while (( line = mainFile.ReadLine()) != null)
                    {
                    
                        var t = new UTF8Encoding(true).GetBytes(Convert.ToString(line));
                        if (i % 2 == 0)
                            fs1.WriteLine(line);
                        else
                            fs2.WriteLine(line);
                        i++;
                    }
                    
                }
            }
        }
        
        
        public void SimpleMergeSort(string pathMainFile)
        {
            int k = 1;
            string a1;
            string a2;
            bool b;
            int f1 = 0, f2 = 0;
            time1 = DateTime.Now;
            while (true) 
            {
                if(tools.isSorter(pathMainFile)) 
                    break;
                SplitMainFile(pathMainFile);
                using (StreamReader af1 = File.OpenText(pathAdditional[0]), af2 = File.OpenText(pathAdditional[1]))
                {
                    using (StreamWriter fs = new StreamWriter(pathMainFile))
                    {
                        string lineOfFile1 = string.Empty, lineOfFile2 = string.Empty; 
                        a1 = af1.ReadLine();
                        a2 = af2.ReadLine();
                        b = true;
                        while (a2 != null && a1 != null)
                        {
                            int i = 0, j = 0;
                            while ((i < k || j < k) && a2 != null && a1 != null)
                            {
                                if (Convert.ToInt32(a1) < Convert.ToInt32(a2))
                                {
                                    fs.WriteLine(a1);
                                    a1 = af1.ReadLine();
                                    i++;
                                }
                                else
                                {
                                    fs.WriteLine(a2);
                                    a2 = af2.ReadLine();
                                    j++;
                                }
                            }
                            while (i < k && a1 != null)
                            {
                                fs.WriteLine(a1);
                                a1 = af1.ReadLine();
                                i++;
                            }
                            while (j < k && a2 != null)
                            {
                                fs.WriteLine(a2);
                                a2 = af2.ReadLine();
                                j++;
                            } 
                        }
                        while (a1 != null)
                        {
                            fs.WriteLine(a1);
                            a1 = af1.ReadLine();
                        }
                        while (a2 != null)
                        {
                            fs.WriteLine(a2);
                            a2 = af2.ReadLine();
                        } 
                        k*=2;
                    }
                }
            }
            time2 = DateTime.Now;
            elapsedTime = time2 - time1;
        }

        #endregion
        
       
    }
}
