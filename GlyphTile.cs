using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OXCFontConverter
{
    public class GlyphTile
    {
        public static Color[] Palette { get; set; } = new Color[]
        {
            Color.FromArgb(0, 0, 0, 0),         // 0:背景:透明色
            Color.FromArgb(255, 255, 255, 255), // 1:主字
            Color.FromArgb(255, 207, 207, 207), // 2:
            Color.FromArgb(255, 159, 159, 159), // 3:
            Color.FromArgb(255, 111, 111, 111), // 4:描邊
            Color.FromArgb(255, 63, 63, 63),    // 5:
        };

        public SizeType Name { get; set; }
        public Square Size { get; set; }
        public Font Font { get; set; }
        public string DefaultFontName { get; set; }
        public int OffsetX { get; set; }        // 微調左邊界
        public int OffsetY { get; set; }        // 微調上邊界
    }

    public enum SizeType
    {
        FontBig,
        FontSmall,
        FontGeoBig,
        FontGeoSmall,
    }

    public class Square
    {
        public byte Width { get; set; }
        public byte Height { get; set; }
        public byte FontPixel { get; set; }
    }
}
