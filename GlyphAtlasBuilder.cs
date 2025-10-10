using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace OXCFontConverter
{
    public class GlyphAtlasBuilder
    {
        private int AtlasCols { get { return maxGlyphsPerRow; } set { maxGlyphsPerRow = value; } }
        private Color[] Palette { get; set; } = GlyphTile.Palette;
        private SizeType sizeType { get; set; }
        private Font resourceFont { get; set; }
        private int imgWidth { get; set; }
        private int imgHeight { get; set; }
        private int maxGlyphsPerRow { get; set; }
        private int offsetX { get; set; } = -1; // 微調左邊界
        private int offsetY { get; set; } = 0; // 微調上邊界

        public GlyphAtlasBuilder(GlyphTile glyphTile) : this(glyphTile, 20)
        {
        }

        public GlyphAtlasBuilder(GlyphTile glyphTile, int maxPerRow)
        {
            imgWidth = glyphTile.Size.Width;
            imgHeight = glyphTile.Size.Height;
            resourceFont = glyphTile.Font;
            sizeType = glyphTile.Name;
            offsetX = glyphTile.OffsetX;
            offsetY = glyphTile.OffsetY;
            maxGlyphsPerRow = maxPerRow;
        }

        public Bitmap Build(string text)
        {
            int cols = AtlasCols;
            int rows = (int)Math.Ceiling(text.Length / (float)cols);
            if (rows < 1) rows = 1; //基本一行
            Bitmap atlas = new Bitmap(cols * imgWidth, rows * imgHeight, PixelFormat.Format8bppIndexed);

            // 設定調色盤
            ColorPalette palette = atlas.Palette;
            for (int i = 0; i < Palette.Length; i++)
                palette.Entries[i] = Palette[i];
            for (int i = Palette.Length; i < 256; i++)
                palette.Entries[i] = Palette[0]; //強制透明色
            atlas.Palette = palette;

            BitmapData atlasData = atlas.LockBits(new Rectangle(0, 0, atlas.Width, atlas.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            int stride = atlasData.Stride;
            byte[] atlasRaw = new byte[stride * atlas.Height];

            var glyphMap = new Dictionary<char, Rectangle>();

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                Bitmap glyphBmp = RenderGlyph(c);
                Bitmap indexed = ConvertToIndexed(glyphBmp);

                switch (sizeType)
                {
                    case SizeType.FontBig:
                    case SizeType.FontSmall:
                        indexed = AddOutline(ConvertToIndexed(indexed));
                        break;
                }

                BitmapData glyphData = indexed.LockBits(new Rectangle(0, 0, imgWidth, imgHeight), ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
                byte[] glyphRaw = new byte[imgHeight * glyphData.Stride];
                System.Runtime.InteropServices.Marshal.Copy(glyphData.Scan0, glyphRaw, 0, glyphRaw.Length);

                int gx = (i % cols) * imgWidth;
                int gy = (i / cols) * imgHeight;

                for (int y = 0; y < imgHeight; y++)
                {
                    for (int x = 0; x < imgWidth; x++)
                    {
                        byte val = glyphRaw[y * glyphData.Stride + x];
                        atlasRaw[(gy + y) * stride + (gx + x)] = val;
                    }
                }

                glyphMap[c] = new Rectangle(gx, gy, imgWidth, imgHeight);
                indexed.UnlockBits(glyphData);
            }

            System.Runtime.InteropServices.Marshal.Copy(atlasRaw, 0, atlasData.Scan0, atlasRaw.Length);
            atlas.UnlockBits(atlasData);

            return atlas;
        }

        public void BuildAndSave(string text, string outputImagePath)
        {
            Build(text)?.Save(outputImagePath, ImageFormat.Png);
        }

        /// <summary>
        /// 將字渲染成黑底白字
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private Bitmap RenderGlyph(char c)
        {
            Bitmap bmp = new Bitmap(imgWidth, imgHeight);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                //要先經過 MeasureString，才能畫到新的位置上。邊界需要微調的數值會受到第一個字的影響
                SizeF size = g.MeasureString(c.ToString(), resourceFont);
                int actualWidth = (int)Math.Ceiling(size.Width);
                int actualHeight = (int)Math.Ceiling(size.Height);

                Bitmap cropped = new Bitmap(imgWidth, imgHeight);

                using (Graphics g2 = Graphics.FromImage(cropped))
                {
                    g2.Clear(Color.Black);
                    g2.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;

                    //要先經過 MeasureString，才能畫到新的位置上。邊界需要微調的數值似乎會受到字型的影響
                    g2.MeasureString(c.ToString(), resourceFont);
                    g2.DrawString(c.ToString(), resourceFont, Brushes.White, new PointF(offsetX, offsetY));
                }

                g.DrawImage(cropped, new Point(0, 0)); // 貼齊左上角
            }
            return bmp;
        }

        private Bitmap ConvertToIndexed(Bitmap source)
        {
            Bitmap indexed = new Bitmap(source.Width, source.Height, PixelFormat.Format8bppIndexed);
            ColorPalette palette = indexed.Palette;
            for (int i = 0; i < Palette.Length; i++)
                palette.Entries[i] = Palette[i];
            for (int i = Palette.Length; i < 256; i++)
                palette.Entries[i] = Color.Black;
            indexed.Palette = palette;

            BitmapData srcData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData dstData = indexed.LockBits(new Rectangle(0, 0, indexed.Width, indexed.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            unsafe
            {
                byte* srcPtr = (byte*)srcData.Scan0;
                byte* dstPtr = (byte*)dstData.Scan0;

                for (int y = 0; y < source.Height; y++)
                {
                    byte* srcRow = srcPtr + y * srcData.Stride;
                    byte* dstRow = dstPtr + y * dstData.Stride;

                    for (int x = 0; x < source.Width; x++)
                    {
                        byte b = srcRow[x * 4];
                        byte g = srcRow[x * 4 + 1];
                        byte r = srcRow[x * 4 + 2];
                        byte gray = (byte)(0.3 * r + 0.59 * g + 0.11 * b);

                        // 灰階 → palette 映射（簡化：黑→0，白→1）
                        dstRow[x] = gray < 128 ? (byte)0 : (byte)1;
                    }
                }
            }

            source.UnlockBits(srcData);
            indexed.UnlockBits(dstData);
            return indexed;
        }

        private Bitmap AddOutline(Bitmap source, byte mainIndex = 1, byte outlineIndex = 4)
        {
            int w = source.Width;
            int h = source.Height;

            Bitmap outlined = new Bitmap(w, h, PixelFormat.Format8bppIndexed);
            ColorPalette palette = outlined.Palette;
            for (int i = 0; i < Palette.Length; i++)
                palette.Entries[i] = Palette[i];
            for (int i = Palette.Length; i < 256; i++)
                palette.Entries[i] = Color.Black;
            outlined.Palette = palette;

            BitmapData srcData = source.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
            BitmapData dstData = outlined.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            byte[] src = new byte[srcData.Stride * h];
            byte[] dst = new byte[dstData.Stride * h];
            System.Runtime.InteropServices.Marshal.Copy(srcData.Scan0, src, 0, src.Length);

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    int idx = y * srcData.Stride + x;
                    if (src[idx] == mainIndex)
                    {
                        dst[idx] = mainIndex;

                        // 對周圍 8 個方向加描邊
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            for (int dx = -1; dx <= 1; dx++)
                            {
                                int nx = x + dx;
                                int ny = y + dy;
                                int nidx = ny * srcData.Stride + nx;

                                if (nx >= 0 && nx < w && ny >= 0 && ny < h && src[nidx] == 0 && dst[nidx] == 0)
                                {
                                    dst[nidx] = outlineIndex;
                                }
                            }
                        }
                    }
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(dst, 0, dstData.Scan0, dst.Length);
            source.UnlockBits(srcData);
            outlined.UnlockBits(dstData);

            return outlined;
        }
    }
}
