using System;
using System.IO;
using System.Text;
using System.Linq;

namespace ConvertData
{
    class File
    {
        private const string YOLO_DATA = "YoloData";
        private string _path{ get; set; }
        public File(string path) => _path = path;
        public StringBuilder ReadDataFromTxt(FileInfo txt)
        {
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(txt.Name))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] data = line.Split(" ");
                    if (data.Length != 5)
                        throw new Exception($"invalid quantity of arguments at {txt.Name}");
                    YoloData yoloDataLine = new YoloData(data, 640, 640);
                    sb.AppendLine(yoloDataLine.ToString());
                }
            }
            return sb;
        }

        public void CreateFile(StringBuilder sb, string fileName)
        {
            CheckDataPath();
            string path = $"{_path}\\{YOLO_DATA}\\{fileName}";
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(sb.ToString());
            }
        }

        private void CheckDataPath()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_path);
            if (!directoryInfo.GetDirectories().Any(x => x.Name.Equals(YOLO_DATA)))
                directoryInfo.CreateSubdirectory(YOLO_DATA);
        }

        public void MoveFile(string fileName, EnumFileType fileType)
        {
            if (fileType == EnumFileType.Txt)
                return;

            CheckDataPath();
            
            string original = $"{_path}\\{fileName}";
            string sufix = fileType == EnumFileType.Test ? "_test" : "_temp";
            
            fileName = fileName.Replace(sufix, string.Empty);
            string copy = $"{_path}\\{YOLO_DATA}\\{fileName}";

            System.IO.File.Copy(original, copy);
        }
    }
}
