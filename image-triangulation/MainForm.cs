using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

// TO DO
// * Оптимизация памяти. Минимизировать new.
// * Переписать нахер весь MainForm
// * Валидация вводимых с клавиатуры значений
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

        float pPMakerThreshold;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            openPngFileDialog.Filter = "PNG files (*.png)|*.png";

            // Инициализируем comboBox'ы
            pPMakersComboBox.Items.AddRange(new string[] { "Выберите алгоритм", "SectorPPMaker1" });
            pPMakersComboBox.SelectedIndex = 0;
            triangulationsComboBox.Items.AddRange(new string[] { "Выберите алгоритм", "SimpleIterativeTriangulation" });
            triangulationsComboBox.SelectedIndex = 0;
            shadersComboBox.Items.AddRange(new string[] { "Выберите алгоритм", "VerticesAverageBrightnessShader" });
            shadersComboBox.SelectedIndex = 0;

            // Блокируем элементы формы с нереализованным функционалом
            openTButton.Enabled = false;
            saveInPngButton.Enabled = false;
            saveInTButton.Enabled = false;

            // Выставляем элементы формы
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

            resetPivotPoints();
            resetTriangulation();
            resetShading();
            setNewSourcePng();

            // Выставляем элементы формы
            showHidePPointsGroupBox.Enabled = false;
            showHideGridGroupBox.Enabled = false;
            triangulationControlsGroupBox.Enabled = false;
            shadingControlsGroupBox.Enabled = false;
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

        private void runPPMakerButton_Click(object sender, EventArgs e)
        {
            switch (pPMakersComboBox.SelectedIndex)
            {
                case 0:
                    return;
                case 1:
                    resetShading();
                    resetTriangulation();
                    resetPivotPoints();

                    pPMakerThreshold = float.Parse(pPMakerThresholdTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);
                    SectorPPMaker1.Run(originalPictureBitmap, pPMakerThreshold, pivotPointsList);
                    DrawOperations.PointsToBitmap(pivotPointsList, pivotPointsBitmap);
                    pivotPointsPictureBox.Image = pivotPointsBitmap;

                    // Выставляем элементы формы
                    showHideImageGroupBox.Enabled = true;
                    showHidePPointsGroupBox.Enabled = true;
                    showHideGridGroupBox.Enabled = false;
                    triangulationControlsGroupBox.Enabled = true;
                    shadingControlsGroupBox.Enabled = false;
                    pivotPointsShow.Checked = true;
                    return;
            }
        }

        private void RunTriangulation_Click(object sender, EventArgs e)
        {
            switch (triangulationsComboBox.SelectedIndex)
            {
                case 0:
                    return;
                case 1:
                    resetShading();
                    resetTriangulation();


                    return;
            }






            Graphics gridCanvas = Graphics.FromImage(triangulationGridBitmap);

            SimpleIterativeTriangulation.MakeTriangulation(pivotPointsList, triangulationSectionsList, trianglesHashSet, originalPictureBitmap);

            DrawOperations.LinesToGraphics(triangulationSectionsList, gridCanvas);
            triangulationGridPictureBox.Image = triangulationGridBitmap;






            rebuiltPictureBitmap = new Bitmap(rebuiltImagePictureBox.Width, rebuiltImagePictureBox.Height);
            VerticesAverageBrightnessShader.Run(rebuiltPictureBitmap, trianglesHashSet);
            rebuiltImagePictureBox.Image = rebuiltPictureBitmap;
        }

        private void resetPivotPoints()
        {
            pivotPointsList.Clear();
            if (pivotPointsBitmap != null) pivotPointsBitmap.Dispose();
            pivotPointsBitmap = new Bitmap(pivotPointsPictureBox.Width, pivotPointsPictureBox.Height);
            pivotPointsPictureBox.Image = null;
        }

        private void resetTriangulation()
        {
            triangulationSectionsList.Clear();
            if (triangulationGridBitmap != null) triangulationGridBitmap.Dispose();
            triangulationGridBitmap = new Bitmap(triangulationGridPictureBox.Width, triangulationGridPictureBox.Height);
            triangulationGridPictureBox.Image = null;
        }

        private void resetShading()
        {
            trianglesHashSet.Clear();
            if (rebuiltPictureBitmap != null) rebuiltPictureBitmap.Dispose();
            rebuiltPictureBitmap = new Bitmap(rebuiltImagePictureBox.Width, rebuiltImagePictureBox.Height);
            rebuiltImagePictureBox.Image = null;
        }

        private void setNewSourcePng()
        {
            if (originalPictureBitmap != null) originalPictureBitmap.Dispose();
            originalPictureBitmap = new Bitmap(openPngFileDialog.FileName);
            originalImagePictureBox.Image = originalPictureBitmap;
        }
    }
}
