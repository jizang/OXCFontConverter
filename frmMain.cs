using Newtonsoft.Json;
using OXCFontConverter.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OXCFontConverter
{
    public partial class frmMain : Form
    {
        private GlyphTile glyphTile;
        private PixelImageViewer viewer;
        private FontConfig fontConfig;
        private bool isFontTypeChanging { get; set; } = false;

        private string selectedFont { get; set; } = string.Empty;

        public frmMain()
        {
            InitializeComponent();

            //加入 PixelImageViewer
            viewer = new PixelImageViewer
            {
                Location = new Point(0, 0),
                Size = new Size(128, 128),
                BackgroundColor = Color.Black,
                ShowPixelInfo = true,
                ShowGrid = true,
            };
            splitContainer1.Panel2.Controls.Add(viewer);
            viewer.Dock = DockStyle.Fill;

            var configFile = @"FontConfig.Json";
            if (File.Exists(configFile))
            {
                fontConfig = JsonConvert.DeserializeObject<FontConfig>(File.ReadAllText(configFile));

                foreach (var glyphTile in fontConfig.GlyphTiles)
                {
                    glyphTile.Font = new Font(glyphTile.DefaultFontName, glyphTile.Size.FontPixel, FontStyle.Regular, GraphicsUnit.Pixel);
                }

                cmbType.DataSource = fontConfig.GlyphTiles;
                cmbType.DisplayMember = "Name";
                cmbType.ValueMember = "Size";
                cmbType.SelectedIndex = 0;
            }
            else
                fontConfig = null;

            lbTargetPngFileName.Text = $"{glyphTile.Name}_{fontConfig?.Region}.png";
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            txtWords.Text = fontConfig == null ? "TEST" : fontConfig.Common4808HanT;
            FontSelector();
        }

        public void FontSelector()
        {
            int selectedIndex = -1;
            string[] allowedFontFamily = fontConfig?.AllowedFontFamily;

            // 取得所有系統字型
            foreach (FontFamily font in FontFamily.Families)
            {
                if (allowedFontFamily != null && !allowedFontFamily.Contains(font.Name)) continue;

                cmbFont.Items.Add(font.Name);

                if (font.Name == fontConfig?.GlyphTiles[0]?.DefaultFontName) //A default 12x12 pixel font
                    selectedIndex = cmbFont.Items.Count - 1;
            }

            cmbFont.SelectedIndex = selectedIndex;
        }

        private void cmbFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedFont = cmbFont.Text;

            if (string.IsNullOrEmpty(selectedFont)) return;

            ChangeFont();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (glyphTile == null) return;

            var outputFileName = lbTargetPngFileName.Text;

            Cursor = Cursors.WaitCursor;
            EnableUI(false);

            var builder = new GlyphAtlasBuilder(glyphTile);
            builder.BuildAndSave(txtWords.Text, outputFileName);

            if (File.Exists(outputFileName))
            {
                ViewerPropertyChange();
                viewer.LoadImage(outputFileName);
            }

            Cursor = Cursors.Default;
            EnableUI(true);
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbType.SelectedIndex < 0) return;

            glyphTile = cmbType.SelectedItem as GlyphTile;

            if (glyphTile == null || glyphTile.Size == null || glyphTile.Font == null) return;

            isFontTypeChanging = true;
            lbTargetPngFileName.Text = $"{glyphTile.Name}_{fontConfig.Region}.png";
            selectedFont = glyphTile.DefaultFontName;
            numOffsetX.Value = glyphTile.OffsetX;
            numOffsetY.Value = glyphTile.OffsetY;
            cmbFont.Text = selectedFont;
            isFontTypeChanging = false;

            ChangeFont();
        }

        private void ChangeFont()
        {
            if (isFontTypeChanging) return;
            if (string.IsNullOrEmpty(selectedFont)) return;
            if (glyphTile == null || glyphTile.Size == null) return;

            Cursor = Cursors.WaitCursor;

            txtWords.Font = new Font(selectedFont, glyphTile.Size.FontPixel + 4, FontStyle.Regular, GraphicsUnit.Pixel);

            glyphTile.Font = new Font(selectedFont, glyphTile.Size.FontPixel, FontStyle.Regular, GraphicsUnit.Pixel);

            ViewerPropertyChange();

            var builder = new GlyphAtlasBuilder(glyphTile);
            var tmpBitMap = builder.Build(txtWords.Text);

            viewer.LoadImage(tmpBitMap);

            Cursor = Cursors.Default;
        }

        private void ViewerPropertyChange()
        {
            switch (glyphTile.Name)
            {
                case SizeType.FontBig:
                    viewer.ZoomFactor = 2.0f;
                    break;

                case SizeType.FontSmall:
                case SizeType.FontGeoBig:
                case SizeType.FontGeoSmall:
                    viewer.ZoomFactor = 4.0f;
                    break;

                default:
                    viewer.ZoomFactor = 1.0f;
                    break;
            }

            viewer.PixelGridWidth = glyphTile.Size.Width;
            viewer.PixelGridHeight = glyphTile.Size.Height;
        }

        private void numOffset_ValueChanged(object sender, EventArgs e)
        {
            if (isFontTypeChanging) return;
            if (glyphTile == null) return;

            numOffsetX.Enabled = false;
            numOffsetY.Enabled = false;

            glyphTile.OffsetX = (int)numOffsetX.Value;
            glyphTile.OffsetY = (int)numOffsetY.Value;

            ChangeFont();

            numOffsetX.Enabled = true;
            numOffsetY.Enabled = true;
        }

        private void EnableUI(bool enable)
        {
            cmbType.Enabled = enable;
            cmbFont.Enabled = enable;
            numOffsetX.Enabled = enable;
            numOffsetY.Enabled = enable;
            txtWords.Enabled = enable;
            btnConvert.Enabled = enable;
        }
    }
}
