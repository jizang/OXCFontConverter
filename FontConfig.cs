using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OXCFontConverter
{
    public class FontConfig
    {
        /// <summary>
        /// 教育部4808個常用正體字
        /// </summary>
        public string Common4808HanT;
        public string Region;
        public GlyphTile[] GlyphTiles;
        /// <summary>
        /// 允許使用的字型
        /// </summary>
        public string[] AllowedFontFamily;
    }
}
