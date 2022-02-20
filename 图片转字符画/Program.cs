using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using 获取字符编码集;
using System.IO;

namespace 图片转字符画
{
    class Program
    {
        static void Main(string[] args)
        {
            FileControl fileControl = new FileControl();
            ToWordImages toWord = new ToWordImages();
            WordPixel wordPixel = new WordPixel();
            bool IsNotNull = true;
            int i = 0;
            string[] STR = new string[]
            {
            "龖齱輪龠馬國典田乙卜丶。",
            "█▇▆▅▄▃▂▁",
            " #,.0123456789:;@ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz$",
            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz",
            " 1234567890",
            "M@WB08Za2SX7r;i:;. ",
            "@#MBHAGh93X25Sisr;:, ",
            "MMMMMMM@@@@@@@WWWWWWWWWBBBBBBBB000000008888888ZZZZZZZZZaZaaaaaa2222222SSSSSSSXXXXXXXXXXX7777777rrrrrrr;;;;;;;;iiiiiiiii:::::::,:,,,,,,.........       ",
            "@@@@@@@######MMMBBHHHAAAA&&GGhh9933XXX222255SSSiiiissssrrrrrrr;;;;;;;;:::::::,,,,,,,........        ",
            "#WMBRXVYIti+=;:,. ",
            "##XXxxx+++===---;;,,...    ",
            "@%#*+=-:. ",
            "01 ",
            };
            string str = STR[1];
            string txtPath;

            start:

            Console.WriteLine("您可以选择一种填充字符,目前有以下几种:\n");

            for (int index = 0; index < STR.Length; index++)
            {
                Console.WriteLine("第{0}种", index);
                Console.WriteLine(STR[index] + "\r\n");
            }
            Console.WriteLine("请输入数字选择\n");
            int num = int.Parse(Console.ReadLine());
            str = STR[num];

            //把字符集排序
            STR[num] = wordPixel.OrderDesWordNumer(ref str);
            Console.WriteLine("您选择了第{0}种", num);
            Console.WriteLine(STR[num]);

            Console.WriteLine("输入目录");
            string path = Console.ReadLine()+@"\";
            string DirectoryPath = Path.GetDirectoryName(path);

            string imageName = "image";
            string imageclass = "png";
            string txtname = "imagetxt";
            Console.WriteLine("输入图片名(不包含序列号)");
            imageName = Console.ReadLine();
            Console.WriteLine("输入图片后缀");
            imageclass = Console.ReadLine();

            Console.WriteLine("输入保存txt文件的文件名");
            txtname = Console.ReadLine();



            IsNotNull = true;
            List<string> Text = new List<string>();
            i = 1;

            Console.WriteLine(DirectoryPath + string.Format(@"\{0}{1:000}.{2}", imageName, i, imageclass));
            while (IsNotNull)
            {
                Bitmap bitMap = toWord.myShowImage(DirectoryPath + string.Format(@"\{0}{1:000}.{2}", imageName,i, imageclass));
                if (bitMap == null)
                {
                    Console.WriteLine("bitMap == null");
                    break;
                }
                txtPath = DirectoryPath + string.Format(@"\{0}{1}.txt", txtname, i);

                string text = ToWordImages.RGB2Gray(bitMap, str);

                Text.Add(text);

                if (wordPixel.MySaveStrToTxtFile(text, txtPath, Encoding.Unicode) == null)
                {
                    IsNotNull = false;
                }
                Console.WriteLine(i);
                i++;
            }
            Console.WriteLine("是否继续转换别的图片帧");
            Console.WriteLine("1、是，2、否");
            if (Console.ReadLine() == "1")
            {
                goto start;
            }

        }
    }
}
