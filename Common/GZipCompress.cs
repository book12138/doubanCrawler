using System;
using System.IO;
using System.Reflection;
using System.Text;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Zip;

namespace Common
{
    /// <summary>
    /// gzip 压缩
    /// </summary>
    public class GZipCompress
    {
        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <returns>解压后的字符串</returns>
        public string DeCompress(Stream stream)
        {
            string result = string.Empty;
            try
            {
                GZipInputStream s = new GZipInputStream(stream);

                using (MemoryStream ms = new MemoryStream())
                {
                    int size = 0;
                    byte[] temp = new byte[2048];
                    while (true)
                    {
                        size = s.Read(temp, 0, temp.Length);
                        if (size > 0)
                            ms.Write(temp);
                        else
                            break;
                    }
                    result = Encoding.UTF8.GetString(ms.ToArray());
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }
    }
}
