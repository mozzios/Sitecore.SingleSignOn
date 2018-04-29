using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;

namespace Sitecore.SingleSignOn.Utility.Logging
{
    public static class LogHelper
    {
        private static string _logPath;
        private static string _myDocumentLogPath;
        public static List<string> _logList = new List<string>();

        public static string SystemDefaultLogPath(string application)
        {
            Contract.Ensures(!string.IsNullOrEmpty(MyDocumentLogPath));
            _logPath = MyDocumentLogPath + string.Format(@"{0}\Log\Log_{1}.txt", application, DateTime.Now.ToString("ddMMyyyy"));
            var info = new FileInfo(_logPath);

            if (info.Directory != null && !info.Directory.Exists)
            {
                if (info.DirectoryName != null)
                    Directory.CreateDirectory(info.DirectoryName);
            }

            return _logPath;
        }

        public static string MyDocumentLogPath
        {
            set
            {
                Contract.Ensures(!string.IsNullOrEmpty(value));
                _myDocumentLogPath = value;
            }
            get
            {
                if (string.IsNullOrEmpty(_myDocumentLogPath))
                    _myDocumentLogPath = string.Format(@"{0}\{1}\", @"C:\", @"Log_SitecoreSingleSignOn");

                return _myDocumentLogPath;
            }
        }

        public static void WriteLog(string application, string section, string message, bool writeToDrive = true)
        {
            try
            {
                string logTxt = Environment.NewLine + string.Format("{0}>>{1}>>{2}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), section, message);

                if (writeToDrive)
                {
                    using (StreamWriter file = new StreamWriter(SystemDefaultLogPath(application), true))
                    {
                        file.Write(logTxt);
                    }
                }

                if (writeToDrive)
                {
                    _logList.Add(string.Format("{0}>>>{1}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), message));
                }
            }
            catch
            {
            }
        }

        public static string WriteFile(string fullPath, string content)
        {
            try
            {
                Contract.Ensures(!string.IsNullOrEmpty(fullPath));

                FileInfo info = new FileInfo(fullPath);

                if (!info.Directory.Exists)
                    Directory.CreateDirectory(info.DirectoryName);

                using (StreamWriter file = new StreamWriter(fullPath, true))
                {
                    file.WriteLine(content);
                }
            }
            catch
            {
                return string.Empty;
            }
            return fullPath;
        }
    }
}
