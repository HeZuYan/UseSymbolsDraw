using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 获取字符编码集
{
    class Conversion
    {
        /// <summary>
        /// 从視頻轉提取圖片幀，完成返回真否则返回假
        /// </summary>
        /// <param name="path">視頻路徑</param>
        /// <param name="SaveImagePath">图片保存路径</param>
        /// <returns>返回图片路径</returns>
        public static string VideoToImages(string path,string SaveImagePath, string Size)
        {

            string DirectoryNamePath = Path.GetDirectoryName(path);
            string EXENmae = "ffmpeg.exe";
            Process process = new Process();
            //要啟動進程的名字
            process.StartInfo.FileName = EXENmae;
            process.StartInfo.UseShellExecute = false;
            //讀取輸入
            process.StartInfo.RedirectStandardInput = true;
            //打開輸出
            process.StartInfo.RedirectStandardOutput = true;
            //顯示窗口
            process.StartInfo.CreateNoWindow = false;

            process.StartInfo.Arguments = @"-i " + path +@" "
                 +Size +@" " + Path.GetDirectoryName(SaveImagePath) + @"\" + Path.GetFileNameWithoutExtension(SaveImagePath) + "%d" + Path.GetExtension(SaveImagePath);
            //啟動外部程序
            try
            {
                process.Start();
                process.WaitForExit();
                process.Close();
                return SaveImagePath;
            }
            catch
            {
                return null;
            }
        }
        public static string VideoToImages(string path, string SaveImagePath)
        {
            return VideoToImages(path, SaveImagePath,"");
        }
        /// <summary>
        /// 从视频中提取声音文件，完成返回真否则返回假
        /// </summary>
        /// <param name="path">视频所在路径</param>
        /// <param name="SaveSoundPath">声音文件保存路径</param>
        /// <returns>返回声音路径</returns>
        public static string VideoToSound(string path,string SaveSoundPath)
        {
            string DirectoryNamePath = Path.GetDirectoryName(path);
            string EXENmae = "ffmpeg.exe";
            Process process = new Process();
            //要啟動進程的名字
            process.StartInfo.FileName = EXENmae;
            process.StartInfo.UseShellExecute = false;
            //讀取輸入
            process.StartInfo.RedirectStandardInput = true;
            //打開輸出
            process.StartInfo.RedirectStandardOutput = true;
            //顯示窗口
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.Arguments = @"-i " + path + @" -vn -ar 44100 -ac 2 -ab 192 -f wav " + SaveSoundPath;
            //啟動外部程序
            try
            {
                process.Start();
                process.WaitForExit();
                process.Close();
                return SaveSoundPath;
            }
            catch
            {
                return null;
            }
        }/// <summary>
         /// 将图片帧转换为视频
         /// </summary>
         /// <param name="path">视频路径</param>
         /// <param name="SaveVideo">视频保存路径</param>
         /// <returns>返回视频的路径</returns>
        public static string ImageToVideo(string path,string SaveVideo)
        {

            string DirectoryNamePath = Path.GetDirectoryName(path);
            string EXENmae = "ffmpeg.exe";
            Process process = new Process();
            //要啟動進程的名字
            process.StartInfo.FileName = EXENmae;
            process.StartInfo.UseShellExecute = false;
            //讀取輸入
            process.StartInfo.RedirectStandardInput = true;
            //打開輸出
            process.StartInfo.RedirectStandardOutput = true;
            //顯示窗口
            process.StartInfo.CreateNoWindow = false;

            process.StartInfo.Arguments = @"-f image2 -i " + Path.GetDirectoryName(path) + Path.GetFileName(path) + "%d" + Path.GetExtension(path) + @" "+SaveVideo;
            //啟動外部程序
            try
            {
                process.Start();
                process.WaitForExit();
                process.Close();
                return SaveVideo;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 将一个声音文件和视频文件合成新的视频
        /// </summary>
        /// <param name="SoundPath">声音源路径</param>
        /// <param name="VideoPath">视频源路径</param>
        /// <param name="SavePath">保存视频路径</param>
        /// <returns>返回新视频的路径</returns>
        public static string SoundAndVideoToVideo(string SoundPath, string VideoPath, string SavePath)
        {
            string EXENmae = "ffmpeg.exe";
            Process process = new Process();
            //要啟動進程的名字
            process.StartInfo.FileName = EXENmae;
            process.StartInfo.UseShellExecute = false;
            //讀取輸入
            process.StartInfo.RedirectStandardInput = true;
            //打開輸出
            process.StartInfo.RedirectStandardOutput = true;
            //顯示窗口
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.Arguments = @"-i " + SoundPath + @" -i " + VideoPath + @" " + SavePath;
            //啟動外部程序
            try
            {
                process.Start();
                process.WaitForExit();
                process.Close();
                return SavePath;
            }
            catch
            {
                return null;
            }
        }
        public static string SoundAndImageToVideo(string SoundPath, string IamgePath, string SavePath)
        {
            string EXENmae = "ffmpeg.exe";
            Process process = new Process();
            //要啟動進程的名字
            process.StartInfo.FileName = EXENmae;
            process.StartInfo.UseShellExecute = false;
            //讀取輸入
            process.StartInfo.RedirectStandardInput = true;
            //打開輸出
            process.StartInfo.RedirectStandardOutput = true;
            //顯示窗口
            process.StartInfo.CreateNoWindow = false;
            //ffmpeg - i 001.mp3 - i darkdoor.% 3d.jpg  darkdoor.avi
            process.StartInfo.Arguments = @"-i " + SoundPath + @" -i " + Path.GetDirectoryName(IamgePath)+@"\" +Path.GetFileNameWithoutExtension(IamgePath)+"%d" + Path.GetExtension(IamgePath) + @" " + SavePath;
            //啟動外部程序
            try
            {
                process.Start();
                process.WaitForExit();
                process.Close();
                return SavePath;
            }
            catch
            {
                return null;
            }
        }
    }
}
