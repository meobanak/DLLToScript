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
            string result = "";
            //var curDir = Directory.GetCurrentDirectory();
            string curDir = @"D:\test";

            string[] myFiles = Directory.GetFiles(curDir);
            foreach (string myFile in myFiles)
            {
                if(Path.GetExtension(myFile) == ".dll")
                {
                    string strbase64 = myFile.EncodeFileToStringBase64();
                    AssemblyModel info = myFile.GetInfoDLL();
                    info.DATA = strbase64;
                    result += ToQuery(info) + "\n" + "\n";
                    Console.WriteLine(myFile + " Ok");
                }
            }

            if (String.IsNullOrEmpty(result))
                return;

            var path = Path.Combine(curDir, "Script_" + DateTime.Now.ToString("yyyyMMdd_HHMMss") + ".sql");
            byte[] data = Encoding.UTF8.GetBytes(result);

            File.WriteAllBytes(path, data);
            Console.ReadLine();
        }

        private string ToQuery(AssemblyModel model)
        {
            string result = "IF NOT EXISTS(SELECT 1 FROM Application..L_Applications(NOLOCK) WHERE OriginalFileName = '" + model.OriginalFileName + "')"
                +"\n" + "BEGIN" + "\n" +
                InsertQuery(model)
                + "\n" + "END" + "\n" +
                "\n" + "ELSE" +
                "\n" + "BEGIN" + "\n" +
                UpdateQuery(model) + "\n" +
                "END" + "\n" ;

            return result;
        }
        
        private string InsertQuery(AssemblyModel model)
        {
            string result = "INSERT INTO Application..L_Applications " +
            "(ProductName,OriginalFileName,Version,DATA,CreatedDate, ModifiedDate)" + "\n" +
            " VALUES(" + "'" + model.ProductName + "', '"
           +  model.OriginalFileName + "', '" + model.Version + "'," + "\n" +
            "'" + model.DATA + "', " + "\n" +
            "'" + model.CreatedDate +"', '" + model.CreatedDate + "')";
            return result;
        }

        private string UpdateQuery(AssemblyModel model)
        {
            string result = "UPDATE Application..L_Applications " + "\n" +
            "SET Version = '" + model.Version + "'," + "\n" +
            "DATA = '" + model.DATA + "'" + "\n" +
            "WHERE OriginalFileName = '" + model.OriginalFileName + "'";
            return result;
        }

        public void ExportToSqlFile()
        {
           
        }
    }
}
