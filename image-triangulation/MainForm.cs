using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

// TO DO
// * Пройтись по освобождению памяти
// * Проверить нейминг методов
// * Валидация вводимых с клавиатуры значений
// * Обработать попытку перезаписи открытого файла
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
            openPngFileDialog.Filter = "PNG file (*.png)|*.png";
            savePngFileDialog.Filter = "PNG file (*.png)|*.png";

            // Инициализируем comboBox'ы
            pPMakersComboBox.Items.AddRange(new string[] { "Выберите алгоритм", "SectorPPMaker1" });
            pPMakersComboBox.SelectedIndex = 0;
            triangulationsComboBox.Items.AddRange(new string[] { "Выберите алгоритм", "SimpleIterativeTriangulation" });
            triangulationsComboBox.SelectedIndex = 0;
            shadersComboBox.Items.AddRange(new string[] { "Выберите алгоритм", "VerticesAverageBrightnessShader" });
            shadersComboBox.SelectedIndex = 0;

            // Блокируем элементы формы с нереализованным функционалом
            openTButton.Enabled = false;
            saveInTButton.Enabled = false;

            // Выставляем элементы формы
            showHideImageGroupBox.Enabled = false;
            showHidePPointsGroupBox.Enabled = false;
            showHideGridGroupBox.Enabled = false;
            pPointsControlsGroupBox.Enabled = false;
            triangulationControlsGroupBox.Enabled = false;
            shadingControlsGroupBox.Enabled = false;
            label7.Visible = false;
            pPMakerThresholdTextBox.Visible = false;
            label8.Text = "";
            label9.Text = "";
            label10.Text = "";
            standartDeviationLabel.Text = "";
            saveInPngButton.Enabled = false;

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
                Size = pivotPointsPictureBox.Size,
                SizeMode = pivotPointsPictureBox.SizeMode,
                Location = new Point(0, 0),
                BackColor = Color.Transparent,
                Enabled = false
            };
            pivotPointsPictureBox.Controls.Add(triangulationGridPictureBox);
                        
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

        private void openPngButton_Click(object sender, EventArgs e)
        {
            if (openPngFileDialog.ShowDialog() == DialogResult.Cancel) return;

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
            showOriginalImage.Checked = true;
            label8.Text = "";
            label9.Text = "";
            label10.Text = "";
            standartDeviationLabel.Text = "";
            saveInPngButton.Enabled = false;
        }

        private void hideOriginalImage_CheckedChanged(object sender, EventArgs e)
        {
            originalImagePictureBox.Image = null;
        }

        private void showOriginalImage_CheckedChanged(object sender, EventArgs e)
        {
            originalImagePictureBox.Image = originalPictureBitmap;
        }

        private void hidePivotPoints_CheckedChanged(object sender, EventArgs e)
        {
            pivotPointsPictureBox.Image = null;
        }

        private void showPivotPoints_CheckedChanged(object sender, EventArgs e)
        {
            pivotPointsPictureBox.Image = pivotPointsBitmap;
        }

        private void hideGrid_CheckedChanged(object sender, EventArgs e)
        {
            triangulationGridPictureBox.Image = null;
        }

        private void showGrid_CheckedChanged(object sender, EventArgs e)
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
                    Stopwatch sw = Stopwatch.StartNew();
                    SectorPPMaker1.Run(originalPictureBitmap, pPMakerThreshold, pivotPointsList);
                    sw.Stop();
                    label8.Text = sw.ElapsedMilliseconds.ToString();
                    DrawOperations.PixelsToBitmap(pivotPointsList, pivotPointsBitmap);
                    pivotPointsPictureBox.Image = pivotPointsBitmap;

                    // Выставляем элементы формы
                    showHideImageGroupBox.Enabled = true;
                    showHidePPointsGroupBox.Enabled = true;
                    showHideGridGroupBox.Enabled = false;
                    triangulationControlsGroupBox.Enabled = true;
                    shadingControlsGroupBox.Enabled = false;
                    showPivotPoints.Checked = true;
                    label9.Text = "";
                    label10.Text = "";
                    standartDeviationLabel.Text = "";
                    saveInPngButton.Enabled = false;
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

                    Stopwatch sw = Stopwatch.StartNew();
                    SimpleIterativeTriangulation.MakeTriangulation(pivotPointsList, triangulationSectionsList, trianglesHashSet, originalPictureBitmap);
                    sw.Stop();
                    label9.Text = sw.ElapsedMilliseconds.ToString();
                    DrawOperations.SectionsToBitmap(triangulationSectionsList, triangulationGridBitmap);
                    triangulationGridPictureBox.Image = triangulationGridBitmap;

                    // Выставляем элементы формы
                    showHideImageGroupBox.Enabled = true;
                    showHidePPointsGroupBox.Enabled = true;
                    showHideGridGroupBox.Enabled = true;
                    pPointsControlsGroupBox.Enabled = true;
                    shadingControlsGroupBox.Enabled = true;
                    showGrid.Checked = true;
                    label10.Text = "";
                    standartDeviationLabel.Text = "";
                    saveInPngButton.Enabled = false;
                    return;
            }            
        }

        private void runShaderButton_Click(object sender, EventArgs e)
        {
            switch (shadersComboBox.SelectedIndex)
            {
                case 0:
                    return;
                case 1:
                    resetShading();

                    Stopwatch sw = Stopwatch.StartNew();
                    VerticesAverageBrightnessShader.Run(rebuiltPictureBitmap, trianglesHashSet);
                    sw.Stop();
                    label10.Text = sw.ElapsedMilliseconds.ToString();                    
                    rebuiltImagePictureBox.Image = rebuiltPictureBitmap;

                    standartDeviationLabel.Text = StandartDeviation.Run(originalPictureBitmap, rebuiltPictureBitmap).ToString();

                    // Выставляем элементы формы
                    showHideImageGroupBox.Enabled = true;
                    showHidePPointsGroupBox.Enabled = true;
                    showHideGridGroupBox.Enabled = true;
                    pPointsControlsGroupBox.Enabled = true;
                    triangulationControlsGroupBox.Enabled = true;
                    saveInPngButton.Enabled = true;
                    return;
            }
        }

        private void pPMakersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (pPMakersComboBox.SelectedIndex)
            {
                case 0:
                    label7.Visible = false;
                    pPMakerThresholdTextBox.Visible = false;
                    thresholdLimitsLabel.Visible = false;
                    return;
                case 1:
                    label7.Visible = true;
                    pPMakerThresholdTextBox.Visible = true;
                    thresholdLimitsLabel.Visible = true;
                    return;
            }
        }

        private void SaveInPngButton_Click(object sender, EventArgs e)
        {
            if (savePngFileDialog.ShowDialog() == DialogResult.Cancel) return;

            rebuiltPictureBitmap.Save(savePngFileDialog.FileName);
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
            trianglesHashSet.Clear();
            if (triangulationGridBitmap != null) triangulationGridBitmap.Dispose();
            triangulationGridBitmap = new Bitmap(triangulationGridPictureBox.Width, triangulationGridPictureBox.Height);
            triangulationGridPictureBox.Image = null;
        }

        private void resetShading()
        {
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
