using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLLToScript
{
    public class DLLToScript
    {
        public DLLToScript()
        {

        }

        public void GetFiles()
        {
            //var curDir = Directory.GetCurrentDirectory();
            string curDir = @"C:\Users\nguyen tam\Desktop\Github\test";

            string[] myFiles = Directory.GetFiles(curDir);
            foreach (string myFile in myFiles)
            {
                string strbase64 = myFile.EncodeFileToStringBase64();
                var info = myFile.GetInfoDLL();

                Console.WriteLine(myFile);
            }
        }

        public void ExportToSqlFile()
        {
            string result = "USE APPLICATION ";
            result += "GO";



        }
    }
}
