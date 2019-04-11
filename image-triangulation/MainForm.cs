using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

// TO DO
// * Оптимизация памяти. Минимизировать new.
// * Переписать нахер весь MainForm
namespace image_triangulation
{
    public partial class MainForm : Form
    {
        Bitmap originalPictureBitmap;
        Bitmap pivotPointsBitmap;
        Bitmap triangulationGridBitmap;
        Bitmap rebuiltPictureBitmap;

        List<Pixel> pivotPointsList = new List<Pixel>();
        List<Section> triangulationSectionsList = new List<Section>();
        HashSet<Triangle> trianglesHashSet = new HashSet<Triangle>();

        PictureBox pivotPointsPictureBox;
        PictureBox triangulationGridPictureBox;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            openPngFileDialog.Filter = "PNG files (*.png)|*.png";

            // Инициализируем comboBox'ы
            pPMakersComboBox.Items.AddRange(new string[] { "SectorPPMaker1" });
            triangulationsComboBox.Items.AddRange(new string[] { "GreedyTriangulation", "SimpleIterativeTriangulation" });
            shadersComboBox.Items.AddRange(new string[] { "VerticesAverageBrightnessShader" });

            // Блокируем элементы формы с нереализованным функционалом
            openTButton.Enabled = false;
            saveInPngButton.Enabled = false;
            saveInTButton.Enabled = false;

            // Блокируем элементы формы, которые не должны быть доступны сразу после загрузки формы
            showHideImageGroupBox.Enabled = false;
            showHidePPointsGroupBox.Enabled = false;
            showHideGridGroupBox.Enabled = false;
            pPointsControlsGroupBox.Enabled = false;
            triangulationControlsGroupBox.Enabled = false;
            shadingControlsGroupBox.Enabled = false;            
            
            // создаём PictureBox для слоя с опорными точками
            pivotPointsPictureBox = new PictureBox
            {
                Size = originalImagePictureBox.Size,
                SizeMode = originalImagePictureBox.SizeMode,
                Location = new Point(0, 0),
                BackColor = Color.Transparent,
                Enabled = false
            };
            originalImagePictureBox.Controls.Add(pivotPointsPictureBox);           

            // создаём PictureBox для слоя с триангуляционной сеткой
            triangulationGridPictureBox = new PictureBox
            {
                Size = originalImagePictureBox.Size,
                SizeMode = originalImagePictureBox.SizeMode,
                Location = new Point(0, 0),
                BackColor = Color.Transparent,
                Enabled = false
            };
            originalImagePictureBox.Controls.Add(triangulationGridPictureBox);
                        
            // (нужно для выбора точек вручную) подписываем PivotPointsPictureBox на MouseClick
            //pivotPointsPictureBox.MouseClick += PivotPointsPictureBox_MouseClick1;
        }

        // Код для выбора точек вручную
        //private void PivotPointsPictureBox_MouseClick1(object sender, MouseEventArgs e)
        //{
        //    pivotPointsList.Add(new Pixel(e.Location.X, e.Location.Y));
        //    pivotPointsBitmap.SetPixel(pivotPointsList.Last().X, pivotPointsList.Last().Y, Color.Red);
        //    pivotPointsPictureBox.Image = pivotPointsBitmap;
        //}

        private void OpenImageButton_Click(object sender, EventArgs e)
        {
            if (openPngFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            // сбрасываем опорные точки
            pivotPointsList.Clear();
            pivotPointsBitmap = new Bitmap(pivotPointsPictureBox.Width, pivotPointsPictureBox.Height);
            pivotPointsPictureBox.Image = null;

            // сбрасываем триангуляцию
            triangulationSectionsList.Clear();
            triangulationGridBitmap = new Bitmap(triangulationGridPictureBox.Width, triangulationGridPictureBox.Height);
            triangulationGridPictureBox.Image = null;

            // сбрасываем восстановленное изображение
            trianglesHashSet.Clear();
            rebuiltPictureBitmap = new Bitmap(rebuiltImagePictureBox.Width, rebuiltImagePictureBox.Height);
            rebuiltImagePictureBox.Image = null;

            // обновляем исходное изображение
            originalPictureBitmap = new Bitmap(openPngFileDialog.FileName);
            originalImagePictureBox.Image = originalPictureBitmap;

            // Разблокируем нужные элементы формы
            showHideImageGroupBox.Enabled = true;
            pPointsControlsGroupBox.Enabled = true;
            originalImageShow.Checked = true;
        }

        private void PictureLayerOff_CheckedChanged(object sender, EventArgs e)
        {
            originalImagePictureBox.Image = null;
        }

        private void PictureLayerOn_CheckedChanged(object sender, EventArgs e)
        {
            originalImagePictureBox.Image = originalPictureBitmap;
        }

        private void PointsLayerOff_CheckedChanged(object sender, EventArgs e)
        {
            pivotPointsPictureBox.Image = null;
        }

        private void PointsLayerOn_CheckedChanged(object sender, EventArgs e)
        {
            pivotPointsPictureBox.Image = pivotPointsBitmap;
        }

        private void GridLayerOff_CheckedChanged(object sender, EventArgs e)
        {
            triangulationGridPictureBox.Image = null;
        }

        private void GridLayerOn_CheckedChanged(object sender, EventArgs e)
        {
            triangulationGridPictureBox.Image = triangulationGridBitmap;
        }

        private void MakePivotPointsButton_Click(object sender, EventArgs e)
        {
            // очищаем bitmap с опорными точками
            pivotPointsBitmap = new Bitmap(pivotPointsPictureBox.Width, pivotPointsPictureBox.Height);

            // очищаем PB для опорных точек
            pivotPointsPictureBox.Image = null;

            // парсинг значения порога
            float threshold = float.Parse(pPMakerThresholdTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);

            // поиск опорных точек
            PPMaker1.Run(originalPictureBitmap, threshold, pivotPointsList);

            // отображаем опорные точки в bitmap
            DrawOperations.PointsToBitmap(pivotPointsList, pivotPointsBitmap);

            // отображаем bitmap в PB
            pivotPointsPictureBox.Image = pivotPointsBitmap;
        }

        private void RunTriangulation_Click(object sender, EventArgs e)
        {
            triangulationSectionsList.Clear();
            triangulationGridBitmap = new Bitmap(triangulationGridPictureBox.Width, triangulationGridPictureBox.Height);
            Graphics gridCanvas = Graphics.FromImage(triangulationGridBitmap);

            SimpleIterativeTriangulation.MakeTriangulation(pivotPointsList, triangulationSectionsList, trianglesHashSet, originalPictureBitmap);

            DrawOperations.LinesToGraphics(triangulationSectionsList, gridCanvas);
            triangulationGridPictureBox.Image = triangulationGridBitmap;

            rebuiltPictureBitmap = new Bitmap(rebuiltImagePictureBox.Width, rebuiltImagePictureBox.Height);
            VerticesAverageBrightnessShader.Run(rebuiltPictureBitmap, trianglesHashSet);
            rebuiltImagePictureBox.Image = rebuiltPictureBitmap;
        }
    }
}
