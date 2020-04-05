using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CloudDisk.CoreLibrary.utils
{
    public class LogUtils
    {
        public static void Log(string msg)
        {
            try
            {
                DateTime dtNow = DateTime.Now;
                string fileName = string.Format("{0}_Logs.txt", dtNow.ToString("yyyy-MM-dd"));
                fileName = Path.Combine(ApplicationSettings.LogsFolderPath, fileName);
                string writeLine = string.Format("[{0}] {1}", dtNow.ToString("yyyy-MM-dd HH:mm:ss.sss"), msg);

                if (!Directory.Exists(ApplicationSettings.LogsFolderPath))
                    Directory.CreateDirectory(ApplicationSettings.LogsFolderPath);

                using (StreamWriter wFile = new StreamWriter(fileName,true))
                {
                    wFile.WriteLine(writeLine);
                }
                Console.WriteLine(writeLine);
            }
            catch
            {

            }
        }

        public static void LogE(Exception ex,string additionalMessage = "")
        {
            StringBuilder sbErrorDetail = new StringBuilder();
            
            sbErrorDetail.AppendLine("----- [ EXCEPTION ] -----");

            if (!string.IsNullOrEmpty(additionalMessage))
                sbErrorDetail.AppendFormat("Additional Message : {0} ]\n", additionalMessage);
            
            sbErrorDetail.AppendLine(ex.Message);
            sbErrorDetail.AppendLine(ex.StackTrace);
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                sbErrorDetail.AppendLine("=================================================");
                sbErrorDetail.AppendLine(ex.Message);
                sbErrorDetail.AppendLine(ex.StackTrace);                
            }
            Log(sbErrorDetail.ToString());
        }
    }
}
