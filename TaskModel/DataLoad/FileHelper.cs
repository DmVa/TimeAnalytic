using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;
using TaskModel.Settings;

namespace TaskModel.DataLoad
{
    public class FileHelper
    {
        private const string SettingsFileExtension = ".setting";
        private const string ApplicationSettingsFileName = "setting.app";

        public string LoadDataDirectory { get; private set; }
        public string ExportDataDirectory { get; private set; }
        public string SettingsDataDirectory { get; private set; }
        public FileHelper ()
        {
            SetDataDirectory();
        }

        public void MoveDeployedRecources()
        {
            string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string resorcesFolder = Path.Combine(location, "DeployedResources");
            string settingsFolder = Path.Combine(resorcesFolder, "Settings");

            string targetDir = SettingsDataDirectory;

            if (Directory.Exists(settingsFolder))
            {
                DirectoryInfo  dir = new DirectoryInfo(settingsFolder);
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo info in  files)
                {
                    string onlyFileNmae = info.Name;
                    string targetFile = Path.Combine(targetDir, onlyFileNmae);
                    if (!File.Exists(targetFile))
                        File.Move(info.FullName, targetFile);
                }
            }
        }

        private void SetDataDirectory()
        {
            //string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //string exePath = Path.GetDirectoryName(location);
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string applicationPath = Path.Combine(basePath, "TimeAnalytic");
            if (!Directory.Exists(applicationPath))
                Directory.CreateDirectory(applicationPath);

            LoadDataDirectory = Path.Combine(applicationPath, "Data");
            if (!Directory.Exists(LoadDataDirectory))
                Directory.CreateDirectory(LoadDataDirectory);

            ExportDataDirectory = Path.Combine(applicationPath, "Export");
            if (!Directory.Exists(ExportDataDirectory))
                Directory.CreateDirectory(ExportDataDirectory);

            SettingsDataDirectory = Path.Combine(applicationPath, "Settings");
            if (!Directory.Exists(SettingsDataDirectory))
                Directory.CreateDirectory(SettingsDataDirectory);
        }

        public string GetLastModifiedFile()
        {
            DirectoryInfo dir = new DirectoryInfo(LoadDataDirectory);
            if (!dir.Exists)
                return string.Empty;
            var allowedExtensions = new[] { "xlsx", "xls" };
            FileInfo[] files = dir
           .GetFiles()
           .Where(file => allowedExtensions.Any(file.Name.ToLower().EndsWith))
           .ToArray();

            FileInfo last = files.OrderByDescending(x => x.LastWriteTime).FirstOrDefault();
            if (last != null)
                return last.FullName;
            return string.Empty;
        }

        public void SaveSetting(ModelSettings setings)
        {
            string fileName = setings.FileName;
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = setings.Name + SettingsFileExtension;
            }
            string fullFileName = Path.Combine(SettingsDataDirectory,fileName);
            setings.FileName = fileName;
            XmlSerializer ser = new XmlSerializer(typeof(ModelSettings));
            using (TextWriter writer = new StreamWriter(fullFileName, false))
            {
                ser.Serialize(writer, setings);
                writer.Close();
            }
        }

        public IEnumerable<ModelSettings> LoadAllSettings()
        {
            List<ModelSettings> settings = new List<ModelSettings>();
            string loadDir = SettingsDataDirectory;

            if (!Directory.Exists(loadDir))
            {
                return settings;
            }

            DirectoryInfo dir = new DirectoryInfo(loadDir);
            FileInfo[] files = dir.GetFiles("*" + SettingsFileExtension);
            foreach (FileInfo info in files)
            {
                string onlyFileNmae = info.Name;
                ModelSettings setting = LoadSetting(info.FullName);
                if (setting != null)
                    settings.Add(setting);
            }
            return settings;
        }
        

        public ModelSettings LoadSetting(string fileName)
        {
            if (!File.Exists(fileName))
                return null;
            ModelSettings settings = null;
            XmlSerializer ser = new XmlSerializer(typeof(ModelSettings));
            
            try
            {
                using (FileStream reader = new FileStream(fileName, FileMode.Open))
                {
                    settings = (ModelSettings)ser.Deserialize(reader);
                    settings.FileName = fileName;
                    reader.Close();
                }
            }
            catch (Exception)
            {

            }

            return settings;
        }

        public void SaveAppSettings(ApplicationSettings settings)
        {
            string fullFileName = Path.Combine(SettingsDataDirectory, ApplicationSettingsFileName);

            XmlSerializer ser = new XmlSerializer(typeof(ApplicationSettings));
            using (TextWriter writer = new StreamWriter(fullFileName, false))
            {
                ser.Serialize(writer, settings);
                writer.Close();
            }
        }
        public ApplicationSettings LoadAppSettings()
        {
            ApplicationSettings settings = null;
             XmlSerializer ser = new XmlSerializer(typeof(ApplicationSettings));
            string fullFileName = Path.Combine(SettingsDataDirectory, ApplicationSettingsFileName);
            if (!File.Exists(fullFileName))
                return null;
            try
            {
                using (FileStream reader = new FileStream(fullFileName, FileMode.Open))
                {
                    settings = (ApplicationSettings)ser.Deserialize(reader);
                    reader.Close();
                }
            }
            catch(Exception)
            {
                
            }
            
            return settings;
        }


        public void DeleteSetting(Settings.ModelSettings setting)
        {
            if (setting == null)
                throw new ApplicationException("setting not defined");

            if (string.IsNullOrEmpty(setting.FileName))
                throw new ApplicationException("setting file not defined");
            string fullName = Path.Combine(SettingsDataDirectory, setting.FileName);
            if (!File.Exists(fullName))
                throw new ApplicationException("setting file does not exists");
            File.Delete(fullName);
        }
    }
}
