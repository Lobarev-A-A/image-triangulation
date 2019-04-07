using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace image_triangulation
{
    public partial class MainForm : Form
    {
        const String DEFAULT_THRESHOLD = "0.035";

        Bitmap originalPictureLayer;
        Bitmap pivotPointsLayer;
        Bitmap triangulationGridLayer;

        List<Point> pivotPointsList = new List<Point>();
        List<Section> outputTriangulation = new List<Section>();

        PictureBox PivotPointsPictureBox;
        PictureBox TriangulationGridPictureBox;

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
            PivotPointsPictureBox = new PictureBox
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
            OriginalImagePictureBox.Controls.Add(PivotPointsPictureBox);

            // подписываем PivotPointsPictureBox на MouseClick
            PivotPointsPictureBox.MouseClick += PivotPointsPictureBox_MouseClick1;

            // создаём PictureBox для слоя с триангуляционной сеткой
            TriangulationGridPictureBox = new PictureBox
            {
                Size = PivotPointsPictureBox.Size,
                MaximumSize = PivotPointsPictureBox.MaximumSize,
                MinimumSize = PivotPointsPictureBox.MinimumSize,
                SizeMode = PivotPointsPictureBox.SizeMode,
                Location = new Point(0, 0),
                BackColor = Color.Transparent,
                Enabled = false
            };

            // добавляем TriangulationGridPictureBox в качестве дочернего для PivotPointsPictureBox
            PivotPointsPictureBox.Controls.Add(TriangulationGridPictureBox);

            // устанавливаем значение по умолчанию для порога яркости при поиске опорных точек
            PPMakerThreshold.Text = DEFAULT_THRESHOLD;
        }

        private void PivotPointsPictureBox_MouseClick1(object sender, MouseEventArgs e)
        {
            // добавляем точку в список опорных точек по координатам клика относительно PivotPointsPictureBox
            pivotPointsList.Add(e.Location);

            // устанавливаем соответствующий пиксель в слое опорных точек
            pivotPointsLayer.SetPixel(pivotPointsList.Last().X, pivotPointsList.Last().Y, Color.Red);

            // обновляем изображение в PivotPointsPictureBox
            PivotPointsPictureBox.Image = pivotPointsLayer;
        }

        private void OpenImageButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            // очищаем PivotPointsPictureBox
            PivotPointsPictureBox.Image = null;

            // очищаем список опорных точек
            pivotPointsList.Clear();

            // очищаем слой с сеткой
            triangulationGridLayer = null;

            // очищаем TriangulationGridPictureBox
            TriangulationGridPictureBox.Image = null;

            // ощищаем список отрезков триангуляции
            outputTriangulation.Clear();

            // создаём Bitmap слой для исходной картинки используя путь до файла из openFileDialog
            originalPictureLayer = new Bitmap(openFileDialog1.FileName);

            // отображаем слой с исходным изображением в OriginalImagePictureBox
            OriginalImagePictureBox.Image = originalPictureLayer;

            // создаём Bitmap слой для опорных точек
            pivotPointsLayer = new Bitmap(PivotPointsPictureBox.Width, PivotPointsPictureBox.Height);

            // активируем PivotPointsPictureBox
            PivotPointsPictureBox.Enabled = true;

            // деактивируем кнопку сброса триангуляции
            //ResetTriangulation.Enabled = false;

            // деактивируем GridControls
            GridControls.Enabled = false;

            pivotPointsList.Add(new Point(0, 0));
            pivotPointsList.Add(new Point(511, 0));
            pivotPointsList.Add(new Point(511, 511));
            pivotPointsList.Add(new Point(0, 511));
        }

        private void PictureLayerOff_CheckedChanged(object sender, EventArgs e)
        {
            // очищаем OrignalImagePictureBox
            OriginalImagePictureBox.Image = null;
        }

        private void PictureLayerOn_CheckedChanged(object sender, EventArgs e)
        {
            // отоброжаем исходное изображение в OriginalImagePictureBox
            OriginalImagePictureBox.Image = originalPictureLayer;
        }

        private void PointsLayerOff_CheckedChanged(object sender, EventArgs e)
        {
            PivotPointsPictureBox.Enabled = false;
            PivotPointsPictureBox.Image = null;
        }

        private void PointsLayerOn_CheckedChanged(object sender, EventArgs e)
        {
            PivotPointsPictureBox.Enabled = true;
            PivotPointsPictureBox.Image = pivotPointsLayer;
        }

        private void RunTriangulation_Click(object sender, EventArgs e)
        {
            outputTriangulation.Clear();
            triangulationGridLayer = new Bitmap(TriangulationGridPictureBox.Width, TriangulationGridPictureBox.Height);
            Graphics gridCanvas = Graphics.FromImage(triangulationGridLayer);

            SimpleIterativeTriangulation.MakeTriangulation(pivotPointsList, outputTriangulation);

            DrawOperations.LinesToGraphics(outputTriangulation, gridCanvas);
            TriangulationGridPictureBox.Image = triangulationGridLayer;

            // активируем кнопку сброса триангуляции
            ResetTriangulation.Enabled = true;

            // активируем GridControls
            GridControls.Enabled = true;
        }

        private void GridLayerOff_CheckedChanged(object sender, EventArgs e)
        {
            TriangulationGridPictureBox.Image = null;
        }

        private void GridLayerOn_CheckedChanged(object sender, EventArgs e)
        {
            TriangulationGridPictureBox.Image = triangulationGridLayer;
        }

        private void ResetTriangulation_Click(object sender, EventArgs e)
        {
            // очищаем PivotPointsPictureBox
            PivotPointsPictureBox.Image = null;

            // очищаем список опорных точек
            pivotPointsList.Clear();

            // очищаем слой с сеткой
            triangulationGridLayer = null;

            // очищаем слой с опорными точками
            pivotPointsLayer = new Bitmap(PivotPointsPictureBox.Width, PivotPointsPictureBox.Height);

            // очищаем TriangulationGridPictureBox
            TriangulationGridPictureBox.Image = null;

            // ощищаем список отрезков триангуляции
            outputTriangulation.Clear();

            // деактивируем кнопку сброса триангуляции
            //ResetTriangulation.Enabled = false;

            // деактивируем GridControls
            GridControls.Enabled = false;
        }

        private void MakePivotPointsButton_Click(object sender, EventArgs e)
        {
            // очищаем bitmap с опорными точками
            pivotPointsLayer = new Bitmap(PivotPointsPictureBox.Width, PivotPointsPictureBox.Height);

            // очищаем PB для опорных точек
            PivotPointsPictureBox.Image = null;

            // парсинг значения порога
            float threshold = float.Parse(PPMakerThreshold.Text, System.Globalization.CultureInfo.InvariantCulture);

            // поиск опорных точек
            PPMaker1.Run(originalPictureLayer, threshold, pivotPointsList);

            // отображаем опорные точки в bitmap
            DrawOperations.PointsToBitmap(pivotPointsList, pivotPointsLayer);

            // отображаем bitmap в PB
            PivotPointsPictureBox.Image = pivotPointsLayer;
        }
    }
}
