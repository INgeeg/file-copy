using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace cmd
{
    public class Class1
    {

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

        void FileHandler(string s, string t)
        {
            System.IO.DirectoryInfo src = new System.IO.DirectoryInfo(s);
            System.IO.FileInfo[] srcFiles = src.GetFiles();

            System.IO.DirectoryInfo trg = new System.IO.DirectoryInfo(t);
            System.IO.FileInfo[] trgFiles = trg.GetFiles();

            foreach (System.IO.FileInfo sr in srcFiles)
            {
                foreach (System.IO.FileInfo tr in trgFiles)
                {
                    Console.WriteLine(sr.Name + "  " + tr.Name);
                }
            }

        }

        public static void Main(string[] args ) {

            string source = @"C:\t1";
            string target = @"C:\t2";
      
            foreach (string src in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
            {
                foreach (string trg in Directory.GetFiles(target, "*.*", SearchOption.AllDirectories))
                {
                    Console.WriteLine(src + "  ---  " + trg);
                }
            }

          
            Console.ReadKey();
        }
    }
}
