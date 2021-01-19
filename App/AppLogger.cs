using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace App
{
    public class AppLogger : ILogger
    {
        public void LogError(Exception ex)
        {
            string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string folderName = Path.Combine(projectPath, "Logs");
            System.IO.Directory.CreateDirectory(folderName);
            string filePath = Path.Combine(folderName, "Log.txt");
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("-----------------------------------------------------------------------------");
                writer.WriteLine("Date : " + DateTime.Now.ToString());
                writer.WriteLine();

                while (ex != null)
                {
                    writer.WriteLine(ex.GetType().FullName);
                    writer.WriteLine("Message : " + ex.Message);
                    writer.WriteLine("StackTrace : " + ex.StackTrace);

                    ex = ex.InnerException;
                }
            }
        }
    }
}
