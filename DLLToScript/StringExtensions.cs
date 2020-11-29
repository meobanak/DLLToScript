using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DLLToScript
{
    public static class StringExtensions
    {
        public static string EncodeFileToStringBase64(this string path)
        {
            Byte[] bytes = File.ReadAllBytes(path);
            String file = Convert.ToBase64String(bytes);
            return file;
        }


        public static void DecodeStringBase64ToFile(this string b64Str, string path)
        {
            Byte[] bytes = Convert.FromBase64String(b64Str);
            File.WriteAllBytes(path, bytes);
        }

        public static Dictionary<string, object> GetInfoDLL(this string path)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            Assembly assembly = Assembly.LoadFile(path);
            result["Version"] = assembly.GetName().Version.ToString();
            result["ProductName"] = assembly.GetName().Name.ToString();
            result["OriginalFileName"] = assembly.ManifestModule.ToString();
            result["CreatedDate"] = DateTime.Today;
            return result;
        }
    }
}
