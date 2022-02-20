using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using Microsoft.DirectX.AudioVideoPlayback;
using System.Text;
using System.Threading.Tasks;

namespace 获取字符编码集
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
            int FrameRate = 10;

            Console.WriteLine("输入视频文件所在目录");
            string path = Console.ReadLine();
            //Console.WriteLine("请输入视频频的帧速率");
            //int frame = int.Parse(Console.ReadLine());
            string DirectoryPath = Path.GetDirectoryName(path);
            string ImagePath = DirectoryPath + @"\image.png";
            string SoundPath = DirectoryPath + @"\1.wav";
            Console.WriteLine("输入exit退出\r\n回车继续");
            string cmd = Console.ReadLine();

            List<string> Text = new List<string>();

            while (cmd!="exit")
            {
                Console.WriteLine("输入0将视频转换为字符画");
                Console.WriteLine("输入1播放字符画，并保存为图片");
                Console.WriteLine("输入2将图片转换为视频帧，并将视频帧和声音合并");
                Console.WriteLine("输入3播放字符画");
                Console.WriteLine("输入4图片转字符画");

                switch (Console.ReadLine())
                {
                    case "0":
                        string txtPath;

                        Console.WriteLine("您可以选择一种填充字符,目前有以下几种:\n");

                        for(int index = 0; index < STR.Length; index++)
                        {
                            Console.WriteLine("第{0}种",index);
                            Console.WriteLine(STR[index]+"\r\n");
                        }
                        Console.WriteLine("请输入数字选择\n");
                        int num = int.Parse(Console.ReadLine());
                        str = STR[num];
                        //把字符集排序
                        STR[num] = wordPixel.OrderDesWordNumer(ref str);
                        Console.WriteLine("您选择了第{0}种", num);
                        Console.WriteLine(STR[num]);

                        Conversion.VideoToImages(path, ImagePath, "-r "+FrameRate+" -s 176*144");
                        Conversion.VideoToSound(path, SoundPath);

                        i = 1;
                        while (IsNotNull)
                        {
                            Bitmap bitMap = toWord.myShowImage(DirectoryPath + string.Format(@"\image{0}.png", i));
                            txtPath = DirectoryPath + string.Format(@"\image{0}.txt", i);
                            string text = ToWordImages.RGB2Gray(bitMap, str);

                            Text.Add(text);

                            if (wordPixel.MySaveStrToTxtFile(text, txtPath, Encoding.Unicode) == null)
                            {
                                IsNotNull = false;
                            }
                            Console.WriteLine(i);
                            i++;
                        }
                        //goto case "1";
                        goto case "3";
                        break;
                    case "1":
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Clear();

                        SoundPlayer sound = new SoundPlayer(SoundPath);
                        sound.Play();

                        IsNotNull = true;
                        int j = 1;

                        Bitmap bit = toWord.myShowImage(DirectoryPath + string.Format(@"\image{0}.png", j));

                        while (IsNotNull)
                        {
                            txtPath = DirectoryPath + string.Format(@"\image{0}.txt", j);
                            string txtToImagePath = DirectoryPath + string.Format(@"\txtimage{0}.png", j);
                            string txt = fileControl.GetOnceAllFromFileTxt(txtPath, Encoding.Unicode);
                            if (txt == null) IsNotNull = false;
                            Console.Clear();
                            Console.Write(txt);
                            //System.Threading.Thread.Sleep(1000 / frame);
                            // Bitmap bitmap = WordPixel.TextToBitmap(txt, new Font("宋体", 8, FontStyle.Regular), Rectangle.Empty, Color.Black, Color.Empty);
                            Bitmap bitmap = WordPixel.InTextBoxShowWord(txt, new Font("宋体",1 , FontStyle.Regular));
                            if (bitmap!=null)
                            bitmap.Save(txtToImagePath);

                            j++;
                            Console.WriteLine(i + "/" + j);
                        }
                        goto case "2";
                        break;
                    case "2":
                        Conversion.SoundAndImageToVideo(SoundPath, DirectoryPath + @"\txtimage.png", DirectoryPath + @"\1.avi");
                        break;
                    //---

                    case "3":

                        Stopwatch stopwatch1 = new Stopwatch();
                        if (IsNotNull)
                        {
                            i = 1;
                            int TimeCount = 0;
                            while (IsNotNull)
                            {
                                stopwatch1.Start();
                                txtPath = DirectoryPath + string.Format(@"\image{0}.txt", i);
                                string txt = fileControl.GetOnceAllFromFileTxt(txtPath, Encoding.Unicode);

                                Text.Add(txt);
                                i++;
                                int NowTime = (int)stopwatch1.ElapsedMilliseconds;
                                TimeCount += NowTime;
                                stopwatch1.Reset();
                                
                                Console.WriteLine(string.Format("第{0}帧 用时{1}毫秒 累计用时{2}毫秒",i,NowTime,TimeCount));
                                if (txt == null) IsNotNull = false;
                            }
                        }
                        IsNotNull = true;
                        Audio sound1 = new Audio(SoundPath);
                        int time1 = (int)(sound1.Duration * 1000);
                        int avgeTime1 = time1 / i;
                        Console.WriteLine(string.Format("共计{0}帧\n平局每帧时间为{1}\n总时长为{2}\n", i, avgeTime1, time1));
                        Console.WriteLine("请把字号设置为5-8号");

                        Console.WriteLine("是否(不预热)每帧从文件读取 输入 1 是");
                        bool IsFile = Console.ReadLine() != "1";
                        Console.ReadKey();
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;

                        stopwatch1.Start();

                        sound1.Play();
                        //int waitCount = 0;
                        //假设有sum帧，时间为t 平均每帧的时间为 t/sum 
                        if (IsFile)
                        {
                            i = 1;
                            foreach (var item in Text)
                            {
                                Console.Clear();
                                Console.Write(item);
                                i++;
                                int UserTime = (int)stopwatch1.ElapsedMilliseconds;
                                int RTime = avgeTime1 * i - UserTime;
                                //waitCount += RTime;
                                //string Title = string.Format("等待{0}|消耗{1}|平均{2},\r\n第{3}帧\r\n累计等待 {4}", RTime, avgeTime1 - RTime, avgeTime1, i, waitCount);
                                //Console.WriteLine(Title);

                                //Console.Title = "每帧读取" + Title;
                                if (RTime > 0)
                                {
                                    System.Threading.Thread.Sleep(RTime);
                                }
                                //waitCount += RTime;
                                //string Title = string.Format("等待{0}|消耗{1}|平均{2},\r\n第{3}帧\r\n累计等待 {4}", RTime, avgeTime1 - RTime, avgeTime1, i, waitCount);
                                //Console.WriteLine(Title);

                                //Console.Title = "预热" + Title;
                            }

                        }
                        else
                        {
                            i = 1;
                            while (IsNotNull)
                            {
                                txtPath = DirectoryPath + string.Format(@"\image{0}.txt", i);
                                string txt = fileControl.GetOnceAllFromFileTxt(txtPath, Encoding.Unicode);
                                if (txt == null) IsNotNull = false;
                                Console.Clear();
                                Console.Write(txt);
                                //System.Threading.Thread.Sleep(1000 / frame);
                                // Bitmap bitmap = WordPixel.TextToBitmap(txt, new Font("宋体", 8, FontStyle.Regular), Rectangle.Empty, Color.Black, Color.Empty);

                                i++;
                                int UserTime = (int)stopwatch1.ElapsedMilliseconds;
                                int RTime = avgeTime1 * i - UserTime;
                                //waitCount += RTime;
                                //string Title = string.Format("等待{0}|消耗{1}|平均{2},\r\n第{3}帧\r\n累计等待 {4}", RTime, avgeTime1 - RTime, avgeTime1, i, waitCount);
                                //Console.WriteLine(Title);

                                //Console.Title = "每帧读取" + Title;
                                if (RTime > 0)
                                {
                                    System.Threading.Thread.Sleep(RTime);
                                }
                                //waitCount += RTime;
                                //string Title = string.Format("等待{0}|消耗{1}|平均{2},\r\n第{3}帧\r\n累计等待 {4}", RTime, avgeTime1 - RTime, avgeTime1, i, waitCount);
                                //Console.WriteLine(Title);

                                //Console.Title = "每帧读取" + Title;

                            }
                        }
                        break;
                    case "4":
                        //string txtPath;

                        Console.WriteLine("您可以选择一种填充字符,目前有以下几种:\n");

                        for (int index = 0; index < STR.Length; index++)
                        {
                            Console.WriteLine("第{0}种", index);
                            Console.WriteLine(STR[index] + "\r\n");
                        }
                        Console.WriteLine("请输入数字选择\n");
                         num = int.Parse(Console.ReadLine());
                        str = STR[num];

                        //把字符集排序
                        STR[num] = wordPixel.OrderDesWordNumer(ref str);
                        Console.WriteLine("您选择了第{0}种", num);
                        Console.WriteLine(STR[num]);
                        Console.WriteLine("输入1修改图片后缀");
                        string imageclass = "png";
                        if (Console.ReadLine() == "1")
                        {
                            Console.WriteLine("输入图片后缀");
                            imageclass = Console.ReadLine();
                        }
                        IsNotNull = true;

                        i = 1;
                        while (IsNotNull)
                        {
                            Bitmap bitMap = toWord.myShowImage(DirectoryPath + string.Format(@"\image{0}.{1}", i, imageclass));
                            txtPath = DirectoryPath + string.Format(@"\image{0}.txt", i);
                            string text = ToWordImages.RGB2Gray(bitMap, str);

                            Text.Add(text);

                            if (wordPixel.MySaveStrToTxtFile(text, txtPath, Encoding.Unicode) == null)
                            {
                                IsNotNull = false;
                            }
                            Console.WriteLine(i);
                            i++;
                        }
                        stopwatch1 = new Stopwatch();
                        avgeTime1 = 1000 / FrameRate;
                        i = 1;
                        IsNotNull = true;
                        stopwatch1.Restart();
                        while (IsNotNull)
                        {
                            txtPath = DirectoryPath + string.Format(@"\image{0}.txt", i);
                            string txt = fileControl.GetOnceAllFromFileTxt(txtPath, Encoding.Unicode);
                            if (txt == null)
                            {
                                Console.ReadKey();
                                IsNotNull = false;
                            }
                            Console.Clear();
                            Console.Write(txt);
                            //System.Threading.Thread.Sleep(1000 / frame);
                            // Bitmap bitmap = WordPixel.TextToBitmap(txt, new Font("宋体", 8, FontStyle.Regular), Rectangle.Empty, Color.Black, Color.Empty);

                            i++;
                            int UserTime = (int)stopwatch1.ElapsedMilliseconds;
                            int RTime = avgeTime1 * i - UserTime;
                            //waitCount += RTime;
                            //string Title = string.Format("等待{0}|消耗{1}|平均{2},\r\n第{3}帧\r\n累计等待 {4}", RTime, avgeTime1 - RTime, avgeTime1, i, waitCount);
                            //Console.WriteLine(Title);

                            //Console.Title = "每帧读取" + Title;
                            if (RTime > 0)
                            {
                                System.Threading.Thread.Sleep(RTime);
                            }
                        }
                        break;
                //------
            }
                Console.WriteLine("输入exit退出\r\n回车继续");
                cmd=Console.ReadLine();
            }
            

            /*
            string path= Console.ReadLine();
            string DirectoryPath = Path.GetDirectoryName(path);
            Console.WriteLine("当前路径:" + DirectoryPath);
            string ImagePath = DirectoryPath + @"\image.png";
            Conversion.VideoToImages(path, ImagePath);
            string SoundPath = DirectoryPath + @"\1.mp3";
            Conversion.VideoToSound(path, SoundPath);
            string NewVideo= Conversion.ImageToVideo(ImagePath, DirectoryPath + @"1.mp4");
            NewVideo = Conversion.SoundAndVideoToVideo(SoundPath, NewVideo, DirectoryPath + @"\1.avi");
            Console.WriteLine(NewVideo);
            Console.ReadLine();*/

            /*
            FileControl fileControl = new FileControl();
            WordPixel wordPixel = new WordPixel();
            string str = "";
            string cmd = "";

            while (cmd != "退出" && cmd != "exit" && cmd != "0")
            {
                string inputstr = "提示:\n\r" +
                    "输入数字选择一下项:\n\r" +
                    "1、选择文件用于保存字符集\n\r" +
                    "2、将将保好的字符集文件按照像素大小进行排序并输出在屏幕\n\r" +
                    "3、选择图像并用指定的文件读取有序的字符集存储到变量中将它们转成字符画\n\r";
                Console.WriteLine(inputstr);
                cmd = Console.ReadLine();
                switch (cmd)
                {
                    case "1":
                        Console.WriteLine("输入文件路径不包括文件名");
                        string filename = Console.ReadLine();
                        Console.WriteLine("输入编码开开始位置，ASCLL一般从32开始");
                        int start = int.Parse(Console.ReadLine());
                        Console.WriteLine("输入编码开开始位置，ASCLL一般至128结束");
                        int end = int.Parse(Console.ReadLine());
                        Console.WriteLine(fileControl.PutToFileTxt(filename, Encoding.Unicode, start, end));
                        break;
                    case "2":
                        Console.WriteLine("2、将保好的字符集文件按照像素大小进行排序并输出在屏幕");
                        Console.WriteLine("输入文件路径（仅仅路径）");
                        filename = Console.ReadLine();
                        //读取一个没有排序的文件，并将它按照像素点排序
                        str = fileControl.GetFromFileTxt(filename, Encoding.Unicode);

                        wordPixel.MySaveStrToTxtFile(str, filename+@"/" + Encoding.Unicode.WebName+"-" + DateTime.Now.ToFileTime() + "yuanxu.txt", Encoding.UTF32);
                        Console.WriteLine(wordPixel.OrderDesWordNumer(ref str));
                        wordPixel.MySaveStrToTxtFile(str, filename+ @"/" + Encoding.Unicode.WebName + "-" + DateTime.Now.ToFileTime() + "jiangxu.txt", Encoding.UTF32);
                        break;
                    case "3":
                        ToWordImages toWord = new ToWordImages();
                        Console.WriteLine("中文字符行列比为1:1\n\r英文字符行列比为1:2");
                        Console.WriteLine("输入行间距");
                        int Row = int.Parse(Console.ReadLine());

                        Console.WriteLine("输入列间距");
                        int Col = int.Parse(Console.ReadLine());

                        Console.WriteLine("输入字符序列文件路径（仅仅路径）");
                        str = Console.ReadLine();
                        str = fileControl.GetFromFileTxt(str, Encoding.Unicode);
                        Console.WriteLine(str);
                        Console.WriteLine("输入图片文件路径和文件名");
                        string path = toWord.OpenFile();
                        Bitmap bitMap = toWord.myShowImage(path);

                        Console.WriteLine("输入字符文件的保存路径和文件名");
                        path = Console.ReadLine();
                        //将指定的图片转成字符画保存到文件中并输出到屏幕

                        str=wordPixel.MySaveStrToTxtFile(toWord.Generate(bitMap, Row, Col, str), path,Encoding.Unicode);

                        Console.WriteLine(str);
                        break;
                }
            }*/
        }
    }
}
