using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 获取字符编码集
{
    public class ToWordImages
    {
        public Bitmap myShowImage(string path)
        {
            try
            {
                //picture.SizeMode = PictureBoxSizeMode.AutoSize;
                //picture.SetBounds(10, 10, 10, 10);
                //picture.Show();
                //picture.Parent = ActiveForm;
                Bitmap bitMap = new Bitmap(path);
                //picture.Image = bitMap;
                return bitMap;
            }
            catch
            {
                return null;
            }
        }
        public string OpenFile()
        {
            Console.WriteLine("请输入文件名和文件路径");
            return Console.ReadLine();
        }
        /// <summary>
        /// 将图片转换为字符画
        /// </summary>
        /// <param name="bitMap">要转换的图片</param>
        /// <param name="RowCount">行数</param>
        /// <param name="ColCount">列数</param>
        /// <param name="str">填充字符串</param>
        /// <returns>返回字符画</returns>
        public string Generate(Bitmap bitMap, int RowCount, int ColCount, string str)
        {
            try
            {
                StringBuilder ReturnStr = new StringBuilder();
                int h, w;
                /*int Row = bitMap.Height / RowCount, Col = bitMap.Height / ColCount;
                if (Col < 1 || Row < 1)
                {
                    Col = 1;
                    Row = RowCount / ColCount;
                }*/
                for (int i = 0; i < ColCount/*bitMap.Height / Col*/; i++)
                {
                    for (int j = 0; j < RowCount/*bitMap.Width / Row*/; j++)
                    {//将照片变为黑白
                        //w = j * Row;
                        //h = i * Col;
                        w = j * bitMap.Width / RowCount;//当前列位置为 列号乘以列间距  列间距=图片宽度/列数
                        h = i*bitMap.Height / ColCount;
                        if (w >= bitMap.Width)
                        {
                            w = bitMap.Width - 1;
                            j = RowCount;
                        }
                        if (h >= bitMap.Height)
                        {
                            h = bitMap.Height - 1;
                            i = ColCount;
                        }
                        Color origalColor = bitMap.GetPixel(w, h);
                        //int grayScale = (int)(origalColor.R * .3 + origalColor.G * .59 + origalColor.B * .11);
                        //不把照片转为黑白
                        //Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);
                        //bitMap.SetPixel(w, h, newColor);

                        int index = (int)((origalColor.R + origalColor.G + origalColor.B) / 768.0 * str.Length);
                        ReturnStr.Append(str[index]);
                    }
                    ReturnStr.Append("\r\n");
                }
                return ReturnStr.ToString();
            }
            catch
            {
                return null;
            }
        }

        public static void SetGrayscalePalette(Bitmap srcImg)

        {

            // check pixel format

            if (srcImg.PixelFormat != PixelFormat.Format8bppIndexed)

                throw new ArgumentException();

            // get palette

            ColorPalette cp = srcImg.Palette;

            // init palette

            for (int i = 0; i < 256; i++)
            {

                cp.Entries[i] = Color.FromArgb(i, i, i);

            }

            srcImg.Palette = cp;

        }

        public static Bitmap CreateGrayscaleImage(int width, int height)

        {

            // create new image

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

            // set palette to grayscale

            SetGrayscalePalette(bmp);

            // return new image

            return bmp;

        }
        public static string RGB2Gray(Bitmap srcBitmap,string str)
        {
            #region 旧的

            //try
            //{

            //    StringBuilder ReturnStr = new StringBuilder();
            //    int wide = srcBitmap.Width;

            //    int height = srcBitmap.Height;

            //    Rectangle rect = new Rectangle(0, 0, wide, height);

            //    BitmapData srcBmData = srcBitmap.LockBits(rect,

            //              ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            //    Bitmap dstBitmap = CreateGrayscaleImage(wide, height);

            //    BitmapData dstBmData = dstBitmap.LockBits(rect,

            //              ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

            //    System.IntPtr srcScan = srcBmData.Scan0;

            //    System.IntPtr dstScan = dstBmData.Scan0;

            //    unsafe //启动不安全代码

            //    {

            //        byte* srcP = (byte*)(void*)srcScan;

            //        byte* dstP = (byte*)(void*)dstScan;

            //        int srcOffset = srcBmData.Stride - wide * 3;

            //        int dstOffset = dstBmData.Stride - wide;

            //        byte red, green, blue;

            //        for (int y = 0; y < height; y++)

            //        {

            //            for (int x = 0; x < wide; x++, srcP += 3, dstP++)

            //            {

            //                blue = srcP[0];

            //                green = srcP[1];

            //                red = srcP[2];

            //                *dstP = (byte)(.299 * red + .587 * green + .114 * blue);

            //                int index = (int)((.299 * red + .587 * green + .114 * blue) / 256.0 * str.Length);

            //                ReturnStr.Append(str[index]);
            //            }
            //            srcP += srcOffset;

            //            dstP += dstOffset;

            //            Console.Write("█");

            //            ReturnStr.Append("\r\n");

            //        }

            //    }
            //    srcBitmap.UnlockBits(srcBmData);

            //    dstBitmap.UnlockBits(dstBmData);


            //    //return dstBitmap;
            //    return ReturnStr.ToString();
            //}
            //catch
            //{
            //    return null;
            //}
            #endregion
            #region 新的
            try
            {
                return RGB2Gray(srcBitmap, srcBitmap.Width, srcBitmap.Height, new PointF(0, 0), 1, str);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
            #endregion

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sBmp">图像</param>
        /// <param name="width">新的图像的宽</param>
        /// <param name="height">新图像的高</param>
        /// <param name="pOffset">原图偏移量</param>
        /// <param name="dPictureShowScale">缩放比例大于0</param>
        /// <param name="str">字符集</param>
        /// <returns></returns>
        public static string RGB2Gray(Bitmap sBmp, int width, int height,PointF pOffset,float dPictureShowScale,string str)
        {
            if (sBmp == null)
            {
                return null;
            }
            int OffSetY = 1;
            if (str[0] >= 32 && str[0] < 127)
            {
                OffSetY = 2;
            }
            StringBuilder stringBuilder = new StringBuilder();
            //获取图像的BitmapData对像 
            BitmapData sData = sBmp.LockBits(new Rectangle(0, 0, sBmp.Width, sBmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            //新的图像
            Bitmap dBmp = new Bitmap(width,height);
            BitmapData dData = dBmp.LockBits(new Rectangle(0, 0, dBmp.Width, dBmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            //循环处理 
            //将项目的“可编译不安全代码”属性设置为true就可以了，方法如下：项目属性对话框——配置属性——生成——允许不安全代码块设置true
            unsafe
            {
                byte* ptrSource = (byte*)(sData.Scan0);
                byte* ptrDes = (byte*)(dData.Scan0);
                int nHeight = dData.Height / 2;
                int nWidth = dData.Width / 2;
                int nOIndexY = 0;
                int nOIndexX = 0;

                byte red, blue, green;

                for (int y = 0; y < dData.Height; y+= OffSetY)
                {
                    nOIndexY = (int)Math.Round((pOffset.Y + y) * dPictureShowScale, 0, MidpointRounding.AwayFromZero);
                    for (int x = 0; x < dData.Width; x++)
                    {
                        int indexDes = 0;
                        int indexSource = 0;
                        nOIndexX = (int)Math.Round((pOffset.X + x) * dPictureShowScale, 0, MidpointRounding.AwayFromZero);
                        if (0 < nOIndexX && nOIndexX < sData.Width && 0 < nOIndexY && nOIndexY < sData.Height)
                        {
                            indexDes = y * dData.Stride + x * 3;
                            indexSource = nOIndexY * sData.Stride + nOIndexX * 3;
                            blue = ptrDes[indexDes] = ptrSource[indexSource];
                            green = ptrDes[indexDes + 1] = ptrSource[indexSource + 1];
                            red = ptrDes[indexDes + 2] = ptrSource[indexSource + 2];
                            //==============
                            int index = (int)((.299 * red + .587 * green + .114 * blue) / 256 * str.Length);
                            
                            stringBuilder.Append(str[index]);
                        }
                    }
                    stringBuilder.Append("\r\n");
                    Console.Write("█");
                }
                dBmp.UnlockBits(dData);
                sBmp.UnlockBits(sData);
            }
            return stringBuilder.ToString();
        }
    }
    public enum FileClass
    {
        Video,
        Audio,
        Images,
        Tex,
        All
    }
}
