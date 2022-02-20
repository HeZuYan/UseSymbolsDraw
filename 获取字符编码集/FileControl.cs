using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 获取字符编码集
{
    public class FileControl
    {
        /// <summary>
        /// 读取一个文本文件并返回它包含的文本
        /// </summary>
        /// <param name="path">文本路径</param>
        /// <param name="encoding">指定的编码方式</param>
        /// <returns></returns>
        public string GetFromFileTxt(string path, Encoding encoding)
        {
            string str = "";
            StreamReader streamReader = new StreamReader(path+encoding.WebName+".txt", encoding, true);
            while (streamReader.Peek() > -1)
            {
                str = streamReader.ReadLine();
            }
            return str;
        }

        /// <summary>
        /// 一次读取文件中的所有文本
        /// </summary>
        /// <param name="path">文本文件的路径</param>
        /// <param name="encoding">读取采用的编码</param>
        /// <returns>返回读取到的文本</returns>
        public string GetOnceAllFromFileTxt(string path,Encoding encoding)
        {
            string str = "";
            try
            {
                StreamReader streamReader = new StreamReader(path, encoding, true);
                while (streamReader.Peek() > -1)
                {
                    str = streamReader.ReadToEnd();
                }
                return str;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 选择一个路径用于保存编码指定编码集的文本
        /// </summary>
        /// <param name="path">文本文件保存路径</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="Start">开始位置 8位ASCLL最多128 即1111 1111</param>
        /// <param name="End">结束位置</param>
        /// <returns></returns>
        public string PutToFileTxt(string path, Encoding encoding, int Start, int End)
        {
            char ch; ;
            string str = "";
            StreamWriter sw = new StreamWriter(path + encoding.WebName + ".txt", true, encoding);
            for (int i = Start; i <= End; i++)
            {
                ch = (char)i;
                str += ch;
            }
            sw.Write(str);
            sw.Flush();
            sw.Close();
            return str;
        }

        public string GetDefaultEncodingWebName
        {           
            get{ return Encoding.Default.WebName; }
        }
    }
}
