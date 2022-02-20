using System;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 获取字符编码集
{
    public class WordPixel
    {
        Form form = new Form();
        Image img = Image.FromFile(@"E:\录屏\逆水寒\0000.png");
        Graphics g ;
        /// <summary>
        /// 按照ASCII码排序
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public string OrderDesWordNumer(string Str)
        {
            char[] ch = Str.ToCharArray();
            Array.Sort(ch);
            Str = new string(ch);
            return Str;
        }
        /// <summary>
        /// 将字符串按照像素点排序
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public string OrderDesWordNumer(ref string Str)
        {
            char[] ch = Str.ToCharArray();
            int[] chCount = new int[ch.Length];
            string RemoveStr = "";
            PictureBox pic;
            pic = new PictureBox();
            pic.Parent = form;
            pic.Show();
            Bitmap bitmap;

            for (int i = 0; i < ch.Length; i++)
            {
                bitmap = GetWordImage(ch[i].ToString());
                int a = GetWordPositNumber(bitmap);
                chCount[i] = a;
                if (a == 0)
                {
                    RemoveStr += ch[i].ToString();
                }

            }

            QuickSort(chCount, 0, ch.Length-1, ch);

            Array.Reverse(ch);

            Str = new string(ch);

            char[] remvoeChar = RemoveStr.ToCharArray();
            for (int i = 0; i < remvoeChar.Length; i++)
            {
                //重字符串中去除 RemoveStr
                Str.Replace(remvoeChar[i].ToString(), "");
            }
            return Str;
        }

        /// <summary>
        /// 对一个int型数组进行快速排序，并且char数组str会参照num数组的顺序
        /// </summary>
        /// <param name="num">要排序的数组</param>
        /// <param name="left">排序数组开始索引</param>
        /// <param name="right">排序数组结束索引</param>
        /// <param name="str">这个数组会参照前面的数组的顺序改变</param>
        public static void QuickSort(int[] num, int left, int right,char[] str)
        {
            if (left >= right)
                return;

            int key = num[left];

            char ch = str[left];

            int i = left;
            int j = right;

            while (i < j)
            {
                while (i < j && key < num[j])
                    j--;
                if (i >= j)
                {
                    num[i] = key;
                    str[i] = ch;
                    break;
                }
                num[i] = num[j];
                str[i] = str[j];

                while (i < j && key >= num[i])
                    i++;
                if (i >= j)
                {
                    num[i] = key;
                    str[i] = ch;
                    break;
                }
                num[j] = num[i];
                str[j] = str[i];
            }
            num[i] = key;
            str[i] = ch;
            QuickSort(num, left, i - 1,str);
            QuickSort(num, i + 1, right,str);
        }

        private Bitmap SaveImage(string str)
        {
            int width = 50;
            int height = 50;
            Bitmap _bit = new Bitmap(width, height);
            Image img = _bit;
            Graphics gBmp = Graphics.FromImage(img);
            Graphics g;

            SizeF sizeF = gBmp.MeasureString(str, new Font("宋体", 30, FontStyle.Regular));
            PointF point = new PointF((width - sizeF.Width) / 2, (height - sizeF.Height) / 2);
            gBmp.DrawString(str, new Font("宋体", 30, FontStyle.Regular), Brushes.Black, point);

            g = form.CreateGraphics();
            g.DrawImage(img,point);
            g.Clear(Color.White);
            Bitmap bitmap = new Bitmap(img);
            bitmap.Save("E:/save4.png");
            return bitmap;
        }

        /// <summary>
        /// 将文本字符串绘制出来并将绘制的图形返回
        /// </summary>
        /// <param name="text">要绘制的文本</param>
        /// <param name="font">绘制的字体样式</param>
        /// <param name="rect">绘制图形的大小</param>
        /// <param name="fontcolor">字体颜色</param>
        /// <param name="backColor">背景色</param>
        /// <returns></returns>
        public static Bitmap TextToBitmap(string text, Font font, Rectangle rect, Color fontcolor, Color backColor)
        {
            try
            {
                Graphics g;
                Bitmap bmp;
                StringFormat format = new StringFormat(StringFormatFlags.NoClip);
                if (text == null)
                    return null; 
                if (rect == Rectangle.Empty)
                {
                    bmp = new Bitmap(1, 1);
                    g = Graphics.FromImage(bmp);
                    //计算绘制文字所需的区域大小（根据宽度计算长度），重新创建矩形区域绘图
                    SizeF sizef = g.MeasureString(text, font, PointF.Empty, format);

                    int width = (int)(sizef.Width + 1);
                    int height = (int)(sizef.Height + 1);
                    rect = new Rectangle(0, 0, width, height);
                    bmp.Dispose();

                    bmp = new Bitmap(width, height);
                }
                else
                {
                    bmp = new Bitmap(rect.Width, rect.Height);
                }

                g = Graphics.FromImage(bmp);

                //使用ClearType字体功能
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                g.FillRectangle(new SolidBrush(backColor), rect);
                g.DrawString(text, font, Brushes.Black, rect, format);
                return bmp;
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 范例
        /// </summary>
        public void GraphicSave()
        {
            Graphics gBmp = Graphics.FromImage(img);

            //添加文字
            String str1 = "1ABCDE";
            String str2 = "2ABCDEABCDE";
            String str3 = "3ABCDEABCDEABCDE";
            Font font = new Font("微软雅黑", 16);
            SolidBrush sbrush1 = new SolidBrush(Color.Black);
            SolidBrush sbrush2 = new SolidBrush(Color.Red);

            //笔刷渐变效果
            //LinearGradientBrush sbrush3 = new LinearGradientBrush(new PointF(100, 100), new PointF(250,250), Color.Red ,Color.Black); 
            SolidBrush sbrush3 = new SolidBrush(Color.Blue);

            //绘制饼图
            SolidBrush s1 = new SolidBrush(Color.Pink);
            gBmp.FillPie(s1, 150, 150, 200, 150, 90, 200);
            SolidBrush s2 = new SolidBrush(Color.Purple);
            gBmp.FillPie(s2, 150, 150, 200, 150, 290, 70);
            SolidBrush s3 = new SolidBrush(Color.Green);
            gBmp.FillPie(s3, 150, 150, 200, 150, 0, 60);
            SolidBrush s4 = new SolidBrush(Color.Brown);
            gBmp.FillPie(s4, 150, 150, 200, 150, 60, 30);

            //文字位置中心点坐标
            Pen p1 = new Pen(Color.Green, 3);
            gBmp.DrawLine(p1, 0, 100, 200, 100);
            gBmp.DrawLine(p1, 100, 0, 100, 200);
            // Rectangle rect = new Rectangle(200,200,100,70);
            // gBmp.DrawEllipse(p1,rect);


            StringFormat format1 = new StringFormat();
            //指定字符串的水平对齐方式
            format1.Alignment = StringAlignment.Far;
            //表示字符串的垂直对齐方式
            format1.LineAlignment = StringAlignment.Center;
            StringFormat format2 = new StringFormat();
            //指定字符串的水平对齐方式
            format2.Alignment = StringAlignment.Center;
            //表示字符串的垂直对齐方式
            format2.LineAlignment = StringAlignment.Center;
            StringFormat format3 = new StringFormat();
            //指定字符串的水平对齐方式
            format3.Alignment = StringAlignment.Near;
            //表示字符串的垂直对齐方式
            format3.LineAlignment = StringAlignment.Center;

            //旋转角度和平移
            System.Drawing.Drawing2D.Matrix mtxRotate = gBmp.Transform;
            mtxRotate.RotateAt(30, new PointF(100, 100));
            gBmp.Transform = mtxRotate;

            gBmp.DrawString(str1, font, sbrush1, new PointF(100, 100), format1);
            gBmp.DrawString(str2, font, sbrush2, new PointF(100, 100), format2);
            gBmp.DrawString(str3, font, sbrush3, new PointF(100, 100), format3);


            g = form.CreateGraphics();
            g.DrawImage(img, 0, 0);
            Bitmap bitmap = new Bitmap(img);
            img.Save("E:/save3.png");
        }
        private Bitmap GetWordImage(string str)
        {
            return SaveImage(str); ;
        }

        private int GetWordPositNumber(Bitmap bitmap)
        {
            int number = 0;
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color origalColor = bitmap.GetPixel(j, i);
                    int index = origalColor.A;
                    if (index != 0)
                    {
                        number++;
                    }
                }
            }
            return number;
        }

        /// <summary>
        /// 将字符串保存到指定的文件中
        /// </summary>
        /// <param name="str">要保存的字符串</param>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public string MySaveStrToTxtFile(string str, string path,Encoding encoding)
        {
            try
            {
                if (str == null)
                    return null; 
                StreamWriter sw = new StreamWriter(path, false, encoding);
                sw.Write(str);
                sw.Flush();
                sw.Close();
                return str;
            }
            catch
            {
                return null;
            }
        }

        static TextBox textBox = new TextBox();
        static Form StaticFrom = new Form();
        //static Label label = new Label();
        /// <summary>
        /// 在TextBox上显示str，将这些文字保存为图片
        /// </summary>
        /// <param name="str">要显示的文字</param>
        /// <param name="font">要显示的字体和字号</param>
        /// <returns></returns>
        public static Bitmap InTextBoxShowWord(string str,Font font)
        {
            try
            {
                //StaticFrom.Show();
                //label.Parent = StaticFrom;
                //label.Show();
                //label.Font = font;
                //StaticFrom.Activate();
                ////label.Margin = new Padding(10, 10, 10, 10);
                ////label.Padding = new Padding(10, 10, 10, 10);
                //label.AutoSize = true;
                //label.BackColor = Color.Red; ; ;
                //label.ForeColor = Color.YellowGreen;
                ////label.Location = new Point(50, 50);
                //label.Dock = DockStyle.None;
                //label.Visible = true;
                //label.Enabled = true;
                //label.Text = str;

                StaticFrom.WindowState = FormWindowState.Maximized;
                StaticFrom.BackColor = Color.OrangeRed;
                StaticFrom.ForeColor = Color.PowderBlue;
                StaticFrom.AutoSizeMode = AutoSizeMode.GrowOnly;
                StaticFrom.Show();
                textBox.Show();
                textBox.Parent = StaticFrom;
                //textBox.Dock = DockStyle.Fill;
                textBox.Font = font;
                StaticFrom.Activate();
                //textBox.Margin = new Padding(0, 0, 0, 0);
                //textBox.Location = new Point(0, 0);

                StaticFrom.WindowState = FormWindowState.Maximized;
                //int width = StaticFrom.Width;
                //int height = StaticFrom.Height;
                //textBox.MaximumSize = new Size(width, height);
                //textBox.MinimumSize = new Size(width, height);
                textBox.WordWrap = true;
                textBox.BorderStyle = BorderStyle.None;
                textBox.TextAlign = HorizontalAlignment.Left;
                //textBox.MaxLength = int.MaxValue;
                textBox.BackColor = Color.White;
                textBox.ForeColor = Color.Black;
                //textBox.Visible = true;
                textBox.Multiline = true;
                textBox.Enabled = true;
                textBox.Clear();
                textBox.AppendText(str);
                
                //StaticFrom.ControlBox = true;

                Graphics graphics = Graphics.FromImage(new Bitmap(1, 1));
                SizeF sizeF = graphics.MeasureString(str, font);

                //label.SetBounds(label.Bounds.X, label.Bounds.Y, (int)sizeF.Width, (int)sizeF.Height);

                //textBox.SetBounds(textBox.Bounds.X, textBox.Bounds.Y, (int)sizeF.Width, (int)sizeF.Height);
                textBox.ClientSize = new Size((int)sizeF.Width, (int)sizeF.Height);
                

                Rectangle rectangle = textBox.RectangleToScreen(textBox.Bounds);
                //Bitmap bitmap = GetScreenCapture(point.X, point.Y, textBox.Bounds.Width, textBox.Bounds.Height);

                //Bitmap bitmap = GetScreenCapture(rectangle.X, rectangle.Y, textBox.Right, textBox.Bottom);
                Bitmap bitmap = CuntControlsImage(textBox);

                return bitmap;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }

        }

        /// <summary>
        /// 截图一个控件，并返回该图片
        /// </summary>
        /// <param name="control">需要进行截图的控件</param>
        /// <returns>截图的图片</returns>
        static Bitmap CuntControlsImage(Control control)
        {
            try
            {
                Graphics graphics = control.CreateGraphics();
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.AssumeLinear;
                Bitmap bitmap = new Bitmap(control.Width, control.Height);
                control.DrawToBitmap(bitmap, new Rectangle(control.Location,control.Size));
                return bitmap;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// 从屏幕x，y处截取Width，Height大小的图片，并返回该图片
        /// </summary>
        /// <param name="x">从屏幕上横向X处开始截取</param>
        /// <param name="y">屏幕上纵向Y处开始截取</param>
        /// <param name="width">截取的宽度</param>
        /// <param name="height">截取的高度</param>
        /// <returns>返回该图片</returns>
        private static Bitmap GetScreenCapture(int x,int y,int width,int height)
        {
            try
            {
                Bitmap tSrcBmp = new Bitmap(width,height); // 用于屏幕原始图片保存
                Graphics gp = Graphics.FromImage(tSrcBmp); 
                gp.CopyFromScreen(new Point(x,y), new Point(0,0),tSrcBmp.Size);
                gp.DrawImage(tSrcBmp,0,0);
                return tSrcBmp;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
    }
}
