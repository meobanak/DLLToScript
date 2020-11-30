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
                AssemblyModel info = myFile.GetInfoDLL();
                //info.DATA = strbase64;

                var result = ToQuery(info);

                Console.WriteLine(myFile);
            }
        }

        private string ToQuery(AssemblyModel model)
        {
            string result = "IF EXISTS(SELECT 1 FROM Application..L_Applications(NOLOCK) WHERE OriginalFileName = '" + model.OriginalFileName + "')"
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
            "N'" + model.DATA + "', " + "\n" +
           model.CreatedDate +", " + model.CreatedDate + ")";
            return result;
        }

        private string UpdateQuery(AssemblyModel model)
        {
            string result = "UPDATE Application..L_Applications " + "\n" +
            "SET Version = '" + model.Version + "'," + "\n" +
            "DATA = N'" + model.DATA + "'" + "\n" +
            "WHERE OriginalFileName = '" + model.OriginalFileName;
            return result;
        }

        public void ExportToSqlFile()
        {
            string result = "USE APPLICATION ";
            result += "GO";



        }
    }
}
