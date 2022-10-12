using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace ConvertData
{
    class Program
    {
        public string Path { get; set; }
        public static File _file { get; set; }
        static void Main(string[] args)
        {
            try
            {
                string directory = Assembly.GetExecutingAssembly().Location;
                DirectoryInfo di = new DirectoryInfo(directory).Parent;
                _file = new File(di.FullName);
                List<FileInfo> txtFiles = di.GetFiles().Where(x => x.Extension.Equals(".txt")).ToList();
                List<FileInfo> jpgTestFiles = di.GetFiles().Where(x => x.Extension.Equals(".jpg") && x.Name.Contains("_test")).ToList();
                List<FileInfo> jpgTemplateFiles = di.GetFiles().Where(x => x.Extension.Equals(".jpg") && x.Name.Contains("_temp")).ToList();
                Conversion(txtFiles);
                Copy(jpgTestFiles, EnumFileType.Test);
            }
            catch (Exception ex)
            {
                throw ex;
            }           
            Console.ReadKey();
        }

        private static void Copy(List<FileInfo> files, EnumFileType fileType)
        {
            Console.WriteLine($"Copy started");
            foreach (FileInfo test in files)
            {
                _file.MoveFile(test.Name, fileType);
                Console.WriteLine($"{test.Name} Copied");
            }
            Console.WriteLine($"Copy Completed");
        }

        private static void Conversion(List<FileInfo> txtFiles)
        {
            Console.WriteLine($"Conversion started");
            foreach (FileInfo txt in txtFiles)
            {
                var sb = _file.ReadDataFromTxt(txt);
                _file.CreateFile(sb, txt.Name);
                Console.WriteLine($"{txt.Name} converted");
            }
            Console.WriteLine("Conversion Completed\n");
        }

    }

    
}
