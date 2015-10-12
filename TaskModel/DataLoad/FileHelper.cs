using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TaskModel.DataLoad
{
    public class FileHelper
    {
        public string DataDirectory { get; private set; }
        public FileHelper ()
        {
            SetDataDirectory();
         
        }

        private void SetDataDirectory()
        {
            string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string exePath = Path.GetDirectoryName(location);
            DataDirectory = Path.Combine(exePath, "Data");
            if (!Directory.Exists(DataDirectory))
                DataDirectory = exePath;
        }
        public string GetLastModifiedFile()
        {
            DirectoryInfo dir = new DirectoryInfo(DataDirectory);
            if (!dir.Exists)
                return string.Empty;
            var allowedExtensions = new[] { "xslx", "xls" };
            FileInfo[] files = dir
           .GetFiles()
           .Where(file => allowedExtensions.Any(file.Name.ToLower().EndsWith))
           .ToArray();

            FileInfo last = files.OrderByDescending(x => x.LastWriteTime).FirstOrDefault();
            if (last != null)
                return last.FullName;
            return string.Empty;
        }
    }
}
