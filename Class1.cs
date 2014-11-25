using System;
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

        void FolderHandler(string s,string t) {
            System.IO.DirectoryInfo src = new System.IO.DirectoryInfo(s);
            System.IO.DirectoryInfo[] srcFolders = src.GetDirectories();

            System.IO.DirectoryInfo trg = new System.IO.DirectoryInfo(t);
            System.IO.DirectoryInfo[] trgFolders = trg.GetDirectories();
            foreach (System.IO.DirectoryInfo sc in srcFolders)
            {
                foreach (System.IO.DirectoryInfo tr in trgFolders)
                {
                    Console.WriteLine(sc.Name + "  " + tr.Name);
                }
            }

        }

        static void FileHandler(string s, string t)
        {
            DirectoryInfo src = new DirectoryInfo(s);
            FileInfo[] srcFiles = src.GetFiles();

            DirectoryInfo trg = new DirectoryInfo(t);
            FileInfo[] trgFiles = trg.GetFiles();

            ArrayList files = new ArrayList(); 

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
                            exists = false;
                        }
                    }
                }
                if (!exists) { files.Add(sr.FullName); }               
            }


            foreach (string i in files)
            {
                Console.WriteLine(i);
            }

        }

        public static void Main(string[] args ) {

            string source = @"C:\t1";
            string target = @"C:\t2";



            FileHandler(source, target);
            foreach (string src in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
            {
                foreach (string trg in Directory.GetFiles(target, "*.*", SearchOption.AllDirectories))
                {
                    //Console.WriteLine(src + "  ---  " + trg);
                    //if (src == trg)
                    //{
                    //    Console.WriteLine("same file");

                    //    if (src.GetHashCode() != src.GetHashCode())
                    //    {
                    //        Console.WriteLine("same file but diff content");

                    //        File.Copy(src, src.Replace(src, "_" + trg), true);
                    //    }
                    //}

                }
            }

          
            Console.ReadKey();
        }
    }
}
