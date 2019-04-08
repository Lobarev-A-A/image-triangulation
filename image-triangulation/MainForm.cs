using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

// TO DO
// * Оптимизация памяти. Минимизировать new.
// * Вынести добавление стартовых точек триангуляции в триангуляцию.
// * Переписать нахер весь MainForm
namespace image_triangulation
{
    public partial class MainForm : Form
    {
        const String DEFAULT_THRESHOLD = "0.1";

        Bitmap originalPictureBitmap;
        Bitmap pivotPointsBitmap;
        Bitmap triangulationGridBitmap;
        Bitmap rebuiltPictureBitmap;

        List<Point> pivotPointsList = new List<Point>();
        List<Section> triangulationSectionsList = new List<Section>();
        HashSet<Triangle> trianglesHashSet = new HashSet<Triangle>();

        PictureBox pivotPointsPictureBox;
        PictureBox triangulationGridPictureBox;

        Pen redPen = new Pen(Color.Red);

        public MainForm()
        {
            InitializeComponent();

            openFileDialog1.Filter = "PNG files (*.png)|*.png";

            this.MinimumSize = Size;
            this.MaximumSize = Size;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // создаём PictureBox для слоя с опорными точками
            pivotPointsPictureBox = new PictureBox
            {
                Size = OriginalImagePictureBox.Size,
                MaximumSize = OriginalImagePictureBox.MaximumSize,
                MinimumSize = OriginalImagePictureBox.MinimumSize,
                SizeMode = OriginalImagePictureBox.SizeMode,
                Location = new Point(0, 0),
                BackColor = Color.Transparent,
                Enabled = false
            };

            // добавляем PictureBox для опорных точек в качестве дочернего для OriginalImagePictureBox
            OriginalImagePictureBox.Controls.Add(pivotPointsPictureBox);

            // подписываем PivotPointsPictureBox на MouseClick
            pivotPointsPictureBox.MouseClick += PivotPointsPictureBox_MouseClick1;

            // создаём PictureBox для слоя с триангуляционной сеткой
            triangulationGridPictureBox = new PictureBox
            {
                Size = pivotPointsPictureBox.Size,
                MaximumSize = pivotPointsPictureBox.MaximumSize,
                MinimumSize = pivotPointsPictureBox.MinimumSize,
                SizeMode = pivotPointsPictureBox.SizeMode,
                Location = new Point(0, 0),
                BackColor = Color.Transparent,
                Enabled = false
            };

            // добавляем TriangulationGridPictureBox в качестве дочернего для PivotPointsPictureBox
            pivotPointsPictureBox.Controls.Add(triangulationGridPictureBox);

            // устанавливаем значение по умолчанию для порога яркости при поиске опорных точек
            PPMakerThreshold.Text = DEFAULT_THRESHOLD;
        }

        private void PivotPointsPictureBox_MouseClick1(object sender, MouseEventArgs e)
        {
            // добавляем точку в список опорных точек по координатам клика относительно PivotPointsPictureBox
            pivotPointsList.Add(e.Location);

            // устанавливаем соответствующий пиксель в слое опорных точек
            pivotPointsBitmap.SetPixel(pivotPointsList.Last().X, pivotPointsList.Last().Y, Color.Red);

            // обновляем изображение в PivotPointsPictureBox
            pivotPointsPictureBox.Image = pivotPointsBitmap;
        }

        private void OpenImageButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            // сбрасываем опорные точки
            pivotPointsList.Clear();
            pivotPointsPictureBox.Image = null;
            pivotPointsBitmap = new Bitmap(pivotPointsPictureBox.Width, pivotPointsPictureBox.Height);

            // сбрасываем триангуляцию
            triangulationSectionsList.Clear();
            triangulationGridBitmap = null;
            triangulationGridPictureBox.Image = null;

            // сбрасываем восстановленное изображение
            RebuiltImagePictureBox.Image = null;
            trianglesHashSet.Clear();

            // обновляем исходное изображение
            originalPictureBitmap = new Bitmap(openFileDialog1.FileName);
            OriginalImagePictureBox.Image = originalPictureBitmap;

            // активируем PivotPointsPictureBox
            pivotPointsPictureBox.Enabled = true;

            pivotPointsList.Add(new Point(0, 0));
            pivotPointsList.Add(new Point(511, 0));
            pivotPointsList.Add(new Point(511, 511));
            pivotPointsList.Add(new Point(0, 511));
        }

        private void PictureLayerOff_CheckedChanged(object sender, EventArgs e)
        {
            OriginalImagePictureBox.Image = null;
        }

        private void PictureLayerOn_CheckedChanged(object sender, EventArgs e)
        {
            OriginalImagePictureBox.Image = originalPictureBitmap;
        }

        private void PointsLayerOff_CheckedChanged(object sender, EventArgs e)
        {
            pivotPointsPictureBox.Enabled = false;
            pivotPointsPictureBox.Image = null;
        }

        private void PointsLayerOn_CheckedChanged(object sender, EventArgs e)
        {
            pivotPointsPictureBox.Enabled = true;
            pivotPointsPictureBox.Image = pivotPointsBitmap;
        }

        private void RunTriangulation_Click(object sender, EventArgs e)
        {
            triangulationSectionsList.Clear();
            triangulationGridBitmap = new Bitmap(triangulationGridPictureBox.Width, triangulationGridPictureBox.Height);
            Graphics gridCanvas = Graphics.FromImage(triangulationGridBitmap);

            SimpleIterativeTriangulation.MakeTriangulation(pivotPointsList, triangulationSectionsList, trianglesHashSet);

            DrawOperations.LinesToGraphics(triangulationSectionsList, gridCanvas);
            triangulationGridPictureBox.Image = triangulationGridBitmap;
        }

        private void GridLayerOff_CheckedChanged(object sender, EventArgs e)
        {
            triangulationGridPictureBox.Image = null;
        }

        private void GridLayerOn_CheckedChanged(object sender, EventArgs e)
        {
            triangulationGridPictureBox.Image = triangulationGridBitmap;
        }

        private void ResetTriangulation_Click(object sender, EventArgs e)
        {
        }

        private void MakePivotPointsButton_Click(object sender, EventArgs e)
        {
            // очищаем bitmap с опорными точками
            pivotPointsBitmap = new Bitmap(pivotPointsPictureBox.Width, pivotPointsPictureBox.Height);

            // очищаем PB для опорных точек
            pivotPointsPictureBox.Image = null;

            // парсинг значения порога
            float threshold = float.Parse(PPMakerThreshold.Text, System.Globalization.CultureInfo.InvariantCulture);

            // поиск опорных точек
            PPMaker1.Run(originalPictureBitmap, threshold, pivotPointsList);

            // отображаем опорные точки в bitmap
            DrawOperations.PointsToBitmap(pivotPointsList, pivotPointsBitmap);

            // отображаем bitmap в PB
            pivotPointsPictureBox.Image = pivotPointsBitmap;
        }
    }
}
