﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace cmd
{
    public class Class1
    {
        const int BYTES_TO_READ = sizeof(Int64);

        static bool FilesAreEqual(FileInfo first, FileInfo second)
        {
            if (first.Length != second.Length)
                return false;

            int iterations = (int)Math.Ceiling((double)first.Length / BYTES_TO_READ);

            using (FileStream fs1 = first.OpenRead())
            using (FileStream fs2 = second.OpenRead())
            {
                byte[] one = new byte[BYTES_TO_READ];
                byte[] two = new byte[BYTES_TO_READ];

                for (int i = 0; i < iterations; i++)
                {
                    fs1.Read(one, 0, BYTES_TO_READ);
                    fs2.Read(two, 0, BYTES_TO_READ);

                    if (BitConverter.ToInt64(one, 0) != BitConverter.ToInt64(two, 0))
                        return false;
                }
            }

            return true;
        }

      

        static void FileHandler(string s, string t)
        {
            DirectoryInfo src = new DirectoryInfo(s);
            FileInfo[] srcFiles = src.GetFiles();

            DirectoryInfo trg = new DirectoryInfo(t);
            FileInfo[] trgFiles = trg.GetFiles();

            var list = new List<KeyValuePair<string, string>>();
            foreach (FileInfo sr in srcFiles)
            {
                bool exists = false;
                foreach (FileInfo tr in trgFiles)
                {
                    //Console.WriteLine(sr.FullName);
                    if (sr.Name == tr.Name)
                    {
                        exists = true;
                        if (!FilesAreEqual(sr,tr))
                        {
                            list.Add(new KeyValuePair<string, string>(sr.FullName, tr.FullName.Replace(tr.Name, "new_" + tr.Name)));
                        }
                    }
                }
                if (!exists) { list.Add(new KeyValuePair<string, string>(sr.FullName, trg.FullName + "\\" + sr.Name)); }               
            }

            foreach (var i in list)
            {
                //Console.WriteLine(i.Key + "  ---  " + i.Value);
                File.Copy(i.Key, i.Value, true);
            }

            FolderHandler(s, t);
        }

        static void FolderHandler(string s, string t)
        {
            DirectoryInfo src = new DirectoryInfo(s);
            DirectoryInfo[] srcFolders = src.GetDirectories();

            DirectoryInfo trg = new System.IO.DirectoryInfo(t);
            DirectoryInfo[] trgFolders = trg.GetDirectories();
            foreach (DirectoryInfo sc in srcFolders)
            {
                bool exists = false;
                foreach (System.IO.DirectoryInfo tr in trgFolders)
                {
                    if (sc.Name == tr.Name) { 
                        exists = true;
                        FileHandler(sc.FullName, tr.FullName);
                    }
                }
                if (!exists) {
                    Console.WriteLine(sc.FullName);
                    Console.WriteLine(trg.FullName);
                    //DirectoryCopy(sc.FullName, trg.FullName,true);
                }
            }

        }

        public static void Main(string[] args ) {

            string source = @"C:\t1";
            string target = @"C:\t2";
            FileHandler(source, target);
            

            //foreach (string src in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
            //{
            //    foreach (string trg in Directory.GetFiles(target, "*.*", SearchOption.AllDirectories))
            //    {
            //    }
            //}

          
            Console.ReadKey();
        }
    }
}
