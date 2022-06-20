using System;
using System.IO;
using ManagementWindow;

namespace Net
{
    public class CNetLog
    {
        private static CNetLog m_instance;
       // public static CNetLog Instance = new Lazy<CNetLog>(() => new CNetLog()).Value;

        private object m_syncObject = new object();
        private string m_filename;
        private string m_Directory;
        private TimeSpan m_RetainTime;
        private int m_Index=1;
        private string m_BaseName;
        private int maxFileCount = 1000;
        static CNetLog()
        {
            m_instance = new CNetLog();
        }

        public static CNetLog Instance
        {
            get
            {
                return m_instance;
            }
        }
        public void Init(string CurrentDirectory, int retainTime=7)
        {
            m_Directory = CurrentDirectory + "\\Log\\Net";
            if (!Directory.Exists(m_Directory))
            {
                Directory.CreateDirectory(m_Directory);
            }
            m_RetainTime = new TimeSpan(retainTime, 0, 0, 0);
            m_BaseName = DateTime.Now.ToString("yyyy_MM_dd_");
            for (int i = 1; i < maxFileCount + 1; i++)
            {
                string fileName = string.Format("{0}\\{1}{2}.txt", m_Directory, m_BaseName, i);
                if(File.Exists(fileName))
                {
                    m_filename = fileName;
                    m_Index = i;
                }
                else
                {
                    break;
                }
            }
            if (m_Index == maxFileCount)  //m_Index == 1 || 
            {
                m_Index = 1;
                m_filename = string.Format("{0}\\{1}{2}.txt", m_Directory, m_BaseName, m_Index);
                File.Delete(m_filename);
            }
            DeleteHistoryFile();
        }

        public void WriteLog(string logMessage)
        {
            try
            {
                lock (m_syncObject)
                {
                    string baseName = DateTime.Now.ToString("yyyy_MM_dd_");
                    if(!m_BaseName.Equals(baseName))
                    {
                        m_BaseName = baseName;
                        m_Index = 1;
                    }

                    m_filename = string.Format("{0}\\{1}{2}.txt", m_Directory, m_BaseName, m_Index);
                   // File.Delete(m_filename);
                    DeleteHistoryFile();

                    using (StreamWriter writer = File.AppendText(m_filename))
                    {
                        string strLog = string.Format("{0}--{1}：{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), AppData.Instance.CurrentUser.username,logMessage);
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
            if (fileInfo.Length >= 10000000)
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
            foreach(string it in txtFiles)
            {
                if (File.GetLastWriteTime(it) <= deleteTime)
                {
                    File.Delete(it);
                }
            }
        }

    }
}
