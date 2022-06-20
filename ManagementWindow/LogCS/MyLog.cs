using System;
using System.IO;
using System.Text;
using System.Globalization;

namespace BaseClass.Tool
{
    public enum LogLevel
    {
        TRACE_LOG_LEVEL,
        DEBUG_LOG_LEVEL,
        INFO_LOG_LEVEL,
        WARN_LOG_LEVEL,
        ERROR_LOG_LEVEL
    }
    public class CMyLog
    {
        private static CMyLog m_instance;
        //private static object m_syncMyLog;

        private LogLevel m_level;
        private int m_fileCount;
        private int m_fileIndex;
        private string m_filename;
        private object m_syncObject;

        static CMyLog()
        {
            m_instance = new CMyLog();
        }

        //public static CMyLog Instance
        //{
        //    get
        //    {
        //        if (m_instance == null)
        //        {
        //            lock (m_syncMyLog)
        //            {
        //                if (m_instance == null)
        //                {
        //                    m_instance = new CMyLog();
        //                }
        //            }
        //        }
        //        return m_instance;
        //    }
        //}

        public static CMyLog Instance
        {
            get
            {
                return m_instance;
            }
        }

        private CMyLog()
        {
            m_syncObject = new object();
            string logLevel = CXmlParser.Instance.GetAttributeValue("Log", "Level");
            m_fileCount = int.Parse(CXmlParser.Instance.GetAttributeValue("Log", "FileCount"));
            if (logLevel == "Trace")
                m_level = LogLevel.TRACE_LOG_LEVEL;
            else if (logLevel == "Debug")
                m_level = LogLevel.DEBUG_LOG_LEVEL;
            else if (logLevel == "Info")
                m_level = LogLevel.INFO_LOG_LEVEL;
            else if (logLevel == "Warn")
                m_level = LogLevel.WARN_LOG_LEVEL;
            else if (logLevel == "Error")
                m_level = LogLevel.ERROR_LOG_LEVEL;
            else
                m_level = LogLevel.INFO_LOG_LEVEL;
            DateTime fileTime = DateTime.Now.AddYears(-1);
            string logDirectory = string.Format(@"{0}\log", CGlobalParams.CurrentDirectory);
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            string[] txtFiles = Directory.GetFiles(string.Format(@"{0}\log", CGlobalParams.CurrentDirectory), "log*.txt");
            m_filename = null;
            foreach (string currentFile in txtFiles)
            {
                if (File.GetLastWriteTime(currentFile) > fileTime)
                {
                    fileTime = File.GetLastWriteTime(currentFile);
                    m_filename = currentFile;
                }
            }
            if (m_filename != null)
            {
                string[] names = m_filename.Split(new char[] { '\\' });
                names = names[names.Length - 1].Split(new char[] { '.' });
                if (names.Length > 0)
                {
                    m_fileIndex = int.Parse(names[0].Substring(3));
                    CheckFileSize();
                    return;
                }
            }
            m_fileIndex = 1;
            m_filename = string.Format(@"{0}\log\log1.txt", CGlobalParams.CurrentDirectory);
        }

        public void WriteLog(LogLevel logLevel, string logMessage)
        {
            if (logLevel < m_level)
                return;
            try
            {
                lock (m_syncObject)
                {
                    using (StreamWriter writer = File.AppendText(m_filename))
                    {
                        //writer.Write("{0} ", GetCurrentTime());
                        writer.Write("{0} ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        if (logLevel == LogLevel.TRACE_LOG_LEVEL)
                            writer.Write("<TRACE> ");
                        else if (logLevel == LogLevel.DEBUG_LOG_LEVEL)
                            writer.Write("<DEBUG> ");
                        else if (logLevel == LogLevel.INFO_LOG_LEVEL)
                            writer.Write("<INFO > ");
                        else if (logLevel == LogLevel.WARN_LOG_LEVEL)
                            writer.Write("<WARN > ");
                        else if (logLevel == LogLevel.ERROR_LOG_LEVEL)
                            writer.Write("<ERROR> ");
                        else
                            writer.Write("<#####> ");
                        writer.WriteLine(logMessage);
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
            if (fileInfo.Length >= 10000000)
            {
                if (m_fileIndex < m_fileCount)
                {
                    m_fileIndex += 1;
                }
                else
                {
                    m_fileIndex = 1;
                }
                m_filename = string.Format(@"{0}\log\log{1}.txt", CGlobalParams.CurrentDirectory, m_fileIndex.ToString());
                File.Delete(m_filename);
            }
        }
    }
}
