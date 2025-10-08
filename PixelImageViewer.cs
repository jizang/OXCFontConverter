using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace OXCFontConverter
{
    public class PixelImageViewer : Control
    {
        public Image Image { get; set; }
        public Color BackgroundColor { get; set; } = Color.White;
        public bool ShowPixelInfo { get; set; } = true;
        public bool ShowGrid { get; set; } = false;
        public int PixelGridWidth { get; set; } = 16;
        public int PixelGridHeight { get; set; } = 16;
        public float ZoomFactor { get; set; } = 2f;
        public float MinZoom { get; set; } = 1f;
        public float MaxZoom { get; set; } = 8f;
        private string pixelInfo { get; set; } = "";

        public PixelImageViewer()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            MouseMove += PixelImageViewer_MouseMove;
            this.MouseWheel += PixelImageViewer_MouseWheel;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.Clear(BackgroundColor);
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;

            if (Image != null)
            {
                int drawWidth = (int)(Image.Width * ZoomFactor);
                int drawHeight = (int)(Image.Height * ZoomFactor);
                e.Graphics.DrawImage(Image, new Rectangle(0, 0, drawWidth, drawHeight));
            }

            if (ShowPixelInfo && !string.IsNullOrEmpty(pixelInfo))
            {
                using (Font font = new Font("Consolas", 10))
                using (Brush brush = new SolidBrush(Color.Yellow))
                {
                    e.Graphics.DrawString(pixelInfo, font, brush, new PointF(5, 5));
                }
            }

            if (ShowGrid && PixelGridWidth > 0 && PixelGridHeight > 0)
            {
                DrawPixelGrid(e.Graphics);
            }
        }

        private void PixelImageViewer_MouseMove(object sender, MouseEventArgs e)
        {
            if (Image is Bitmap bmp)
            {
                int x = e.X;
                int y = e.Y;

                if (x < bmp.Width && y < bmp.Height)
                {
                    BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);
                    int stride = data.Stride;
                    IntPtr ptr = data.Scan0;
                    byte[] raw = new byte[stride * bmp.Height];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, raw, 0, raw.Length);
                    bmp.UnlockBits(data);

                    int index = raw[y * stride + x];
                    //Color color = bmp.Palette.Entries[index];
                    //pixelInfo = $"({x},{y}) Index:{index} RGB:{color.R},{color.G},{color.B}";
                    Invalidate();
                }
                else
                {
                    pixelInfo = "";
                    Invalidate();
                }
            }
            else
            {
                pixelInfo = "";
                Invalidate();
            }
        }

        private void PixelImageViewer_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                ZoomFactor = Math.Min(MaxZoom, ZoomFactor * 2f);
            else
                ZoomFactor = Math.Max(MinZoom, ZoomFactor / 2f);

            Invalidate();
        }

        public void LoadImage(Bitmap bitmap)
        {
            Image = bitmap;
            Invalidate();
        }

        public void LoadImage(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    ms.Position = 0;
                    Image = Image.FromStream(ms);
                    Invalidate();
                }
            }
        }

        private void DrawPixelGrid(Graphics g)
        {
            var actualGridWidth = PixelGridWidth * ZoomFactor;
            var actualGridHeight = PixelGridHeight * ZoomFactor;

            int gridW = (int)(Width / actualGridWidth);
            int gridH = (int)(Height / actualGridWidth);

            float lineWidth = 0.1f;

            using (Pen pen = new Pen(Color.LightGray, lineWidth))
            {
                for (int x = 0; x <= gridW; x++)
                {
                    float px = x * actualGridWidth;
                    g.DrawLine(pen, px, 0, px, Height);
                }

                for (int y = 0; y <= gridH; y++)
                {
                    float py = y * actualGridHeight;
                    g.DrawLine(pen, 0, py, Width, py);
                }
            }
        }
    }
}
