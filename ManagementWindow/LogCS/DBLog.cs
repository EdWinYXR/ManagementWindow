using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager
{
    public enum LogLevel
    {
        TRACE_LOG_LEVEL,
        DEBUG_LOG_LEVEL,
        INFO_LOG_LEVEL,
        WARN_LOG_LEVEL,
        ERROR_LOG_LEVEL
    }
    public class CGlobalParams
    {
        private static CGlobalParams m_Instance = new CGlobalParams();

        static public CGlobalParams Instance
        {
            get
            {
                return m_Instance;
            }
        }

        public static string CurrentDirectory//文件目录
        { set; get; }

        public static string DefaultConnectName
        { set; get; }

        public static bool HostEnabled
        { set; get; }

        public static int SysMode
        { set; get; }

        public static ServerMode ServerMode
        { get; set; }
    }
    public enum ServerMode
    {
        TM,
        MRMS
    }
    public class CDBLog
    {
        private static CDBLog m_instance;

        private object m_syncObject = new object();
        private string m_filename;
        private string m_Directory;
        private TimeSpan m_RetainTime;
        private int m_Index = 1;
        private string m_BaseName;
        private int maxFileCount = 20;
        static CDBLog()
        {
            m_instance = new CDBLog();
        }

        public static CDBLog Instance
        {
            get
            {
                return m_instance;
            }
        }

        public void Init(int retainTime = 7)
        {
            m_Directory = CGlobalParams.CurrentDirectory + "\\log\\DB";
            if (!Directory.Exists(m_Directory))
            {
                Directory.CreateDirectory(m_Directory);
            }
            m_RetainTime = new TimeSpan(retainTime, 0, 0, 0);
            m_BaseName = DateTime.Now.ToString("yyyy_MM_dd_");
            for (int i = 1; i < maxFileCount + 1; i++)
            {
                string fileName = string.Format("{0}\\{1}{2}.txt", m_Directory, m_BaseName, i);
                if (File.Exists(fileName))
                {
                    m_filename = fileName;
                    m_Index = i;
                }
                else
                {
                    break;
                }
            }
            if (m_Index == maxFileCount)
            {
                m_Index = 1;
                m_filename = string.Format("{0}\\{1}{2}.txt", m_Directory, m_BaseName, m_Index);
                File.Delete(m_filename);
            }
            DeleteHistoryFile();
        }

        public void WriteLog(LogLevel logLevel, string logMessage)
        {
            try
            {
                lock (m_syncObject)
                {
                    string baseName = DateTime.Now.ToString("yyyy_MM_dd_");
                    if (!m_BaseName.Equals(baseName))
                    {
                        m_BaseName = baseName;
                        m_Index = 1;
                    }

                    m_filename = string.Format("{0}\\{1}{2}.txt", m_Directory, m_BaseName, m_Index);
                    //File.Delete(m_filename);
                    DeleteHistoryFile();

                    string levelStr = "";
                    if (logLevel == LogLevel.TRACE_LOG_LEVEL)
                        levelStr = "<TRACE> ";
                    else if (logLevel == LogLevel.DEBUG_LOG_LEVEL)
                        levelStr = "<DEBUG> ";
                    else if (logLevel == LogLevel.INFO_LOG_LEVEL)
                        levelStr = "<INFO > ";
                    else if (logLevel == LogLevel.WARN_LOG_LEVEL)
                        levelStr = "<WARN > ";
                    else if (logLevel == LogLevel.ERROR_LOG_LEVEL)
                        levelStr = "<ERROR> ";
                    else
                        levelStr = "<#####> ";

                    using (StreamWriter writer = File.AppendText(m_filename))
                    {
                        string strLog = string.Format("{0} {1}--{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), levelStr, logMessage);
                        writer.WriteLine(strLog);
                    }
                    CheckFileSize();
                }
            }
            catch
            {
            }
        }

        private void CheckFileSize()
        {
            if (!File.Exists(m_filename))
                return;
            FileInfo fileInfo = new FileInfo(m_filename);
            fileInfo.Refresh();
            if (fileInfo.Length >= 200000)
            {
                m_Index++;
                if (m_Index == maxFileCount + 1)
                    m_Index = 1;
                m_filename = string.Format("{0}\\{1}{2}.txt", m_Directory, m_BaseName, m_Index);
                File.Delete(m_filename);
            }
        }

        private void DeleteHistoryFile()
        {
            string[] txtFiles = Directory.GetFiles(m_Directory);
            DateTime deleteTime = DateTime.Now - m_RetainTime;
            foreach (string it in txtFiles)
            {
                if (File.GetLastWriteTime(it) <= deleteTime)
                {
                    File.Delete(it);
                }
            }
        }
    }
}
