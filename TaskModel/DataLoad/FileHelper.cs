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
            //string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //string exePath = Path.GetDirectoryName(location);
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string applicationPath = Path.Combine(basePath, "TimeAnalytic");
            if (!Directory.Exists(applicationPath))
                Directory.CreateDirectory(applicationPath);

            DataDirectory = Path.Combine(applicationPath, "Data");
            if (!Directory.Exists(DataDirectory))
                Directory.CreateDirectory(DataDirectory);
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
