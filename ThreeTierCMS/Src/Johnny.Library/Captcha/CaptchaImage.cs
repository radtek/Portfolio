using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

using Johnny.Library.Cryptography;

namespace Johnny.Library.Captcha
{
    /*=================================================================================
     *======================================Notes======================================
     * This code was wrote using an article in the code project web site.
     * Web Site: http://www.codeproject.com
     * Article: Captcha Image
     * 
     * Some changes were done to this code
     *=================================================================================
     *====================================End of Notes=================================
     */
    /// <summary>
    /// Summary description for Captcha
    /// </summary>
    public class CaptchaImage
    {
        // Public properties (all read-only).
        public string Text
        {
            get { return this.text; }
        }
        public Color BackgroundColor
        {
            get { return this.bc; }
        }
        public Bitmap Image
        {
            get { return this.image; }
        }
        public int Width
        {
            get { return this.width; }
        }
        public int Height
        {
            get { return this.height; }
        }

        // Internal properties.
        private string text;
        private Color bc;
        private int width;
        private int height;
        private Bitmap image;

        public CaptchaImage(ref string s, int width, int height, int iFrom, int iTo)
        {
            s = RandomText.Generate(iFrom, iTo);
            this.text = s;
            this.bc = Color.White;
            this.width = width;
            this.height = height;
            this.GenerateImage();
        }

        public CaptchaImage(ref string s, int width, int height, int length)
        {
            s = RandomText.Generate(length);
            this.text = s;
            this.bc = Color.LightGoldenrodYellow;
            this.width = width;
            this.height = height;
            this.GenerateImageAll();
        }

        ~CaptchaImage()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                // Dispose of the bitmap.
                this.image.Dispose();
        }
        private FontFamily[] fonts = {
            new FontFamily("Times New Roman"),
            new FontFamily("Georgia"),
            new FontFamily("Arial"),
            new FontFamily("Comic Sans MS")
        };
        private Color[] colors = {
            Color.Red,
            Color.DarkGreen,
            Color.Brown,
            Color.Black,
            Color.DarkOrange
        };
        private void GenerateImage()
        {
            // Create a new 32-bit bitmap image.
            Bitmap bitmap = new Bitmap(this.width, this.height, PixelFormat.Format32bppArgb);

            // Create a graphics object for drawing.
            Graphics g = Graphics.FromImage(bitmap);
            Rectangle rect = new Rectangle(0, 0, this.width, this.height);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (SolidBrush b = new SolidBrush(bc))
            {
                g.FillRectangle(b, rect);
            }

            // Set up the text font.
            int emSize = (int)(this.width * 2 / text.Length);
            FontFamily family = fonts[RNG.Next(fonts.Length - 1)];
            Font font = new Font(family, emSize);
            // Adjust the font size until the text fits within the image.
            SizeF measured = new SizeF(0, 0);
            SizeF workingSize = new SizeF(this.width, this.height);
            while (emSize > 2 &&
                (measured = g.MeasureString(text, font)).Width > workingSize.Width ||
                measured.Height > workingSize.Height)
            {
                font.Dispose();
                font = new Font(family, emSize -= 2);
            }

            // Set up the text format.
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            // Create a path using the text and warp it randomly.
            GraphicsPath path = new GraphicsPath();
            path.AddString(this.text, font.FontFamily, (int)font.Style, font.Size, rect, format);

            // Set font color to a color that is visible within background color
            int bcR = Convert.ToInt32(bc.R);
            // This prevents font color from being near the bg color
            int red = RNG.Next(255), green = RNG.Next(255), blue = RNG.Next(255);
            while (red >= bcR && red - 20 <= bcR ||
                red < bcR && red + 20 >= bcR)
            {
                red = RNG.Next(0, 255);
            }
            SolidBrush sBrush = new SolidBrush(Color.FromArgb(red, green, blue));
            g.FillPath(sBrush, path);

            // Iterate over every pixel
            double distort = RNG.Next(5, 20) * (RNG.Next(10) == 1 ? 1 : -1);

            // Copy the image so that we're always using the original for source color
            using (Bitmap copy = (Bitmap)bitmap.Clone())
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // Adds a simple wave
                        int newX = (int)(x + (distort * Math.Sin(Math.PI * y / 84.0)));
                        int newY = (int)(y + (distort * Math.Cos(Math.PI * x / 44.0)));
                        if (newX < 0 || newX >= width) newX = 0;
                        if (newY < 0 || newY >= height) newY = 0;
                        bitmap.SetPixel(x, y, copy.GetPixel(newX, newY));
                    }
                }
            }

            // Clean up.
            font.Dispose();
            sBrush.Dispose();
            g.Dispose();

            // Set the image.
            this.image = bitmap;
        }

        private void GenerateImageAll()
        {
            if (text == null || text.Trim() == String.Empty)
                return;
            Bitmap bitmap = new Bitmap(this.width, this.height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
                        
            //Çå¿ÕÍ¼Æ¬±³¾°É«
            g.Clear(bc);

            Font font = new System.Drawing.Font("Arial", 11, (System.Drawing.FontStyle.Bold));
            //System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, bitmap.Width/5, bitmap.Height), Color.DarkGreen, Color.DarkGreen, 1.2f, true);
            //g.DrawString(text.Substring(0, 1), font, brush, 4, 2);

            //System.Drawing.Drawing2D.LinearGradientBrush brush2 = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(bitmap.Width / 5, 0, 2 * bitmap.Width / 5, bitmap.Height), Color.Red, Color.Red, 1.2f, true);
            //g.DrawString(text.Substring(1, 1), font, brush2, 4 + bitmap.Width / 5, 2);

            for (int ix = 0; ix < text.Length; ix++)
            {
                Color singleColor = colors[RNG.Next(colors.Length - 1)];
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(ix * bitmap.Width / text.Length, 0, (ix + 1) * bitmap.Width / text.Length, bitmap.Height), singleColor, singleColor, 0, true);
                g.DrawString(text.Substring(ix, 1), font, brush, 4 + ix * bitmap.Width / (text.Length + 1), 2);
            }

            //Éú³ÉËæ»úÉú³ÉÆ÷
            Random random = new Random();
            //»­Í¼Æ¬µÄ±³¾°ÔëÒôÏß
            //for (int i = 0; i < 25; i++)
            //{
            //    int x1 = random.Next(bitmap.Width);
            //    int x2 = random.Next(bitmap.Width);
            //    int y1 = random.Next(bitmap.Height);
            //    int y2 = random.Next(bitmap.Height);
            //    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            //}

            //»­Í¼Æ¬µÄÇ°¾°ÔëÒôµã
            /*for(int i=0; i<100; i++)
            {
                int x = random.Next(image.Width);
                int y = random.Next(image.Height);
                //image.SetPixel(x, y, Color.FromArgb(random.Next()));
                image.SetPixel(x,y,Color.Yellow );
            }
            */
            //»­Í¼Æ¬µÄ±ß¿òÏß
            //g.DrawRectangle(new Pen(Color.Silver), 0, 0, bitmap.Width - 1, bitmap.Height - 1);

            //Å¤Çú
            // Iterate over every pixel
            //double distort = RNG.Next(5, 8) * (RNG.Next(10) == 1 ? 1 : -1);

            // Copy the image so that we're always using the original for source color
            //using (Bitmap copy = (Bitmap)bitmap.Clone())
            //{
            //    for (int y = 0; y < height; y++)
            //    {
            //        for (int x = 0; x < width; x++)
            //        {
            //            // Adds a simple wave
            //            int newX = (int)(x + (distort * Math.Sin(Math.PI * y / 84.0)));
            //            int newY = (int)(y + (distort * Math.Cos(Math.PI * x / 44.0)));
            //            if (newX < 0 || newX >= width) newX = 0;
            //            if (newY < 0 || newY >= height) newY = 0;
            //            bitmap.SetPixel(x, y, copy.GetPixel(newX, newY));
            //        }
            //    }
            //}

            g.Dispose();
            // Set the image.
            this.image = bitmap;
        }        
    }
}