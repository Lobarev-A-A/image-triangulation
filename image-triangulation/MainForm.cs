using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

// TO DO
// * Пройтись по использованию памяти (using (разобраться с неуправляемыми ресурсами))
// * Проверить нейминг методов
// * Валидация вводимых с клавиатуры значений
// * Обработать попытку перезаписи открытого файла
// * Решить, что использовать для отрисовки открываемого .t-файла
namespace image_triangulation
{
    public partial class MainForm : Form
    {
        Bitmap sourceImageBitmap;
        Bitmap pivotPointsBitmap;
        Bitmap triangulationGridBitmap;
        Bitmap rebuiltImageBitmap;

        HashSet<Pixel> pivotPoints = new HashSet<Pixel>();
        List<Section> triangulationSectionsList = new List<Section>();
        HashSet<Triangle> trianglesHashSet = new HashSet<Triangle>();
        
        PictureBox pivotPointsPictureBox;
        PictureBox triangulationGridPictureBox;

        float pPMakerThreshold;
        float coefOfCacheExpand;
        float stripingFactor;

        Stopwatch sw;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            openPngFileDialog.Filter = "PNG file (*.png)|*.png";
            savePngFileDialog.Filter = "PNG file (*.png)|*.png";
            saveTFileDialog.Filter = "T file (*.t)|*.t";
            openTFileDialog.Filter = "T file (*.t)|*.t";

            // Инициализируем comboBox'ы
            pPMakersComboBox.Items.AddRange(new string[] { "Выберите алгоритм", "SectorPPMaker1", "Комбинированный алгоритм" });
            pPMakersComboBox.SelectedIndex = 0;
            triangulationsComboBox.Items.AddRange(new string[] { "Выберите алгоритм", "SimpleIterativeTriangulation",
                                                                 "DCIterativeTriangulation", "StripIterativeTriangulation" });
            triangulationsComboBox.SelectedIndex = 0;
            shadersComboBox.Items.AddRange(new string[] { "Выберите алгоритм", "VerticesAverageBrightnessShader" });
            shadersComboBox.SelectedIndex = 0;

            // Выставляем элементы формы
            showHideImageGroupBox.Enabled = false;
            showHidePPointsGroupBox.Enabled = false;
            showHideGridGroupBox.Enabled = false;
            pPointsControlsGroupBox.Enabled = false;
            triangulationControlsGroupBox.Enabled = false;
            shadingControlsGroupBox.Enabled = false;
            label8.Text = "";
            label9.Text = "";
            label10.Text = "";
            standartDeviationLabel.Text = "";
            saveInPngButton.Enabled = false;
            saveInTButton.Enabled = false;

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

        private void OpenPngButton_Click(object sender, EventArgs e)
        {
            if (openPngFileDialog.ShowDialog() == DialogResult.Cancel) return;

            ResetPivotPoints();
            ResetTriangulation();
            ResetShading();
            SetNewSourcePng();

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
            saveInTButton.Enabled = false;
        }
        private void SaveInPngButton_Click(object sender, EventArgs e)
        {
            if (savePngFileDialog.ShowDialog() == DialogResult.Cancel) return;

            rebuiltImageBitmap.Save(savePngFileDialog.FileName);
        }

        private void OpenTButton_Click(object sender, EventArgs e)
        {
            if (openTFileDialog.ShowDialog() == DialogResult.Cancel) return;

            ResetPivotPoints();
            ResetTriangulation();
            ResetShading();

            TExtension.Open(pivotPoints, openTFileDialog.FileName);
            SimpleIterativeTriangulation.Run(pivotPoints, triangulationSectionsList, trianglesHashSet);
            VerticesAverageBrightnessShader.Run(rebuiltImageBitmap, trianglesHashSet);
            rebuiltImagePictureBox.Image = rebuiltImageBitmap;

            // Выставляем элементы формы
            showHideImageGroupBox.Enabled = false;
            showHidePPointsGroupBox.Enabled = false;
            showHideGridGroupBox.Enabled = false;
            pPointsControlsGroupBox.Enabled = false;
            triangulationControlsGroupBox.Enabled = false;
            shadingControlsGroupBox.Enabled = false;
            label8.Text = "";
            label9.Text = "";
            label10.Text = "";
            standartDeviationLabel.Text = "";
            saveInPngButton.Enabled = true;
            saveInTButton.Enabled = false;
            originalImagePictureBox.Image = null;
        }
        private void SaveInTButton_Click(object sender, EventArgs e)
        {
            if (saveTFileDialog.ShowDialog() == DialogResult.Cancel) return;

            TExtension.Save(pivotPoints, saveTFileDialog.FileName);
        }

        private void HideOriginalImage_CheckedChanged(object sender, EventArgs e)
        {
            originalImagePictureBox.Image = null;
        }
        private void ShowOriginalImage_CheckedChanged(object sender, EventArgs e)
        {
            originalImagePictureBox.Image = sourceImageBitmap;
        }

        private void HidePivotPoints_CheckedChanged(object sender, EventArgs e)
        {
            pivotPointsPictureBox.Image = null;
        }
        private void ShowPivotPoints_CheckedChanged(object sender, EventArgs e)
        {
            pivotPointsPictureBox.Image = pivotPointsBitmap;
        }

        private void HideGrid_CheckedChanged(object sender, EventArgs e)
        {
            triangulationGridPictureBox.Image = null;
        }
        private void ShowGrid_CheckedChanged(object sender, EventArgs e)
        {
            triangulationGridPictureBox.Image = triangulationGridBitmap;
        }

        private void RunPPMakerButton_Click(object sender, EventArgs e)
        {
            switch (pPMakersComboBox.SelectedIndex)
            {
                case 0:
                    return;
                case 1:
                    ResetShading();
                    ResetTriangulation();
                    ResetPivotPoints();

                    pPMakerThreshold = float.Parse(pPMakerThresholdTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);
                    sw = Stopwatch.StartNew();
                    SectorPPMaker1.Run(sourceImageBitmap, pPMakerThreshold, pivotPoints);
                    sw.Stop();
                    label8.Text = sw.ElapsedMilliseconds.ToString();
                    DrawOperations.PixelsToBitmap(pivotPoints, pivotPointsBitmap);
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
                    saveInTButton.Enabled = true;
                    return;
                case 2:
                    ResetShading();
                    ResetTriangulation();
                    ResetPivotPoints();

                    pPMakerThreshold = float.Parse(pPMakerThresholdTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);
                    sw = Stopwatch.StartNew();
                    SearchInTriangle.Run(sourceImageBitmap, pPMakerThreshold, pivotPoints, triangulationSectionsList, trianglesHashSet);
                    sw.Stop();
                    label8.Text = sw.ElapsedMilliseconds.ToString();

                    DrawOperations.PixelsToBitmap(pivotPoints, pivotPointsBitmap);
                    pivotPointsPictureBox.Image = pivotPointsBitmap;
                    DrawOperations.SectionsToBitmap(triangulationSectionsList, triangulationGridBitmap);
                    triangulationGridPictureBox.Image = triangulationGridBitmap;

                    // Выставляем элементы формы
                    showHideImageGroupBox.Enabled = true;
                    showHidePPointsGroupBox.Enabled = true;
                    showHideGridGroupBox.Enabled = true;
                    triangulationControlsGroupBox.Enabled = false;
                    shadingControlsGroupBox.Enabled = true;
                    showPivotPoints.Checked = true;
                    showGrid.Checked = true;
                    label9.Text = "";
                    label10.Text = "";
                    standartDeviationLabel.Text = "";
                    saveInPngButton.Enabled = false;
                    saveInTButton.Enabled = true;
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
                    ResetShading();
                    ResetTriangulation();

                    sw = Stopwatch.StartNew();
                    SimpleIterativeTriangulation.Run(pivotPoints, triangulationSectionsList, trianglesHashSet);
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
                    saveInTButton.Enabled = true;
                    return;
                case 2:
                    ResetShading();
                    ResetTriangulation();

                    coefOfCacheExpand = float.Parse(coefOfCacheExpandTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);
                    sw = Stopwatch.StartNew();
                    DCIterativeTriangulation.Run(pivotPoints, triangulationSectionsList, trianglesHashSet, coefOfCacheExpand);
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
                    saveInTButton.Enabled = true;
                    return;
                case 3:
                    ResetShading();
                    ResetTriangulation();

                    stripingFactor = float.Parse(stripingFactorTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);
                    sw = Stopwatch.StartNew();
                    StripIterativeTriangulation.Run(pivotPoints, triangulationSectionsList, trianglesHashSet, stripingFactor);
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
                    saveInTButton.Enabled = true;
                    return;
            }            
        }
        private void RunShaderButton_Click(object sender, EventArgs e)
        {
            switch (shadersComboBox.SelectedIndex)
            {
                case 0:
                    return;
                case 1:
                    ResetShading();

                    Stopwatch sw = Stopwatch.StartNew();
                    VerticesAverageBrightnessShader.Run(rebuiltImageBitmap, trianglesHashSet);
                    sw.Stop();
                    label10.Text = sw.ElapsedMilliseconds.ToString();                    
                    rebuiltImagePictureBox.Image = rebuiltImageBitmap;

                    standartDeviationLabel.Text = StandartDeviation.Run(sourceImageBitmap, rebuiltImageBitmap).ToString();

                    // Выставляем элементы формы
                    showHideImageGroupBox.Enabled = true;
                    showHidePPointsGroupBox.Enabled = true;
                    showHideGridGroupBox.Enabled = true;
                    pPointsControlsGroupBox.Enabled = true;
                    triangulationControlsGroupBox.Enabled = true;
                    saveInPngButton.Enabled = true;
                    saveInTButton.Enabled = true;
                    return;
            }
        }

        private void PPMakersComboBox_SelectedIndexChanged(object sender, EventArgs e)
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
                    pPMakerThresholdTextBox.Text = "0.1";
                    thresholdLimitsLabel.Visible = true;
                    return;
                case 2:
                    label7.Visible = true;
                    pPMakerThresholdTextBox.Visible = true;
                    pPMakerThresholdTextBox.Text = "0.1";
                    thresholdLimitsLabel.Visible = true;
                    return;
            }
        }
        private void TriangulationsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (triangulationsComboBox.SelectedIndex)
            {
                case 0:
                    coefOfCacheExpandLabel.Visible = false;
                    coefOfCacheExpandTextBox.Visible = false;
                    coefOfCacheExpandRecomendLabel.Visible = false;

                    stripingFactorLabel.Visible = false;
                    stripingFactorTextBox.Visible = false;
                    stripingFactorRecomendLabel.Visible = false;
                    return;
                case 1:
                    coefOfCacheExpandLabel.Visible = false;
                    coefOfCacheExpandTextBox.Visible = false;
                    coefOfCacheExpandRecomendLabel.Visible = false;

                    stripingFactorLabel.Visible = false;
                    stripingFactorTextBox.Visible = false;
                    stripingFactorRecomendLabel.Visible = false;
                    return;
                case 2:
                    coefOfCacheExpandTextBox.Text = "5";
                    coefOfCacheExpandLabel.Visible = true;
                    coefOfCacheExpandTextBox.Visible = true;
                    coefOfCacheExpandRecomendLabel.Visible = true;

                    stripingFactorLabel.Visible = false;
                    stripingFactorTextBox.Visible = false;
                    stripingFactorRecomendLabel.Visible = false;
                    return;
                case 3:
                    coefOfCacheExpandLabel.Visible = false;
                    coefOfCacheExpandTextBox.Visible = false;
                    coefOfCacheExpandRecomendLabel.Visible = false;

                    stripingFactorTextBox.Text = "0.17";
                    stripingFactorLabel.Visible = true;
                    stripingFactorTextBox.Visible = true;
                    stripingFactorRecomendLabel.Visible = true;
                    return;
            }
        }

        private void ResetPivotPoints()
        {
            pivotPoints.Clear();
            if (pivotPointsBitmap != null) pivotPointsBitmap.Dispose();
            pivotPointsBitmap = new Bitmap(pivotPointsPictureBox.Width, pivotPointsPictureBox.Height);
            pivotPointsPictureBox.Image = null;
        }
        private void ResetTriangulation()
        {
            triangulationSectionsList.Clear();
            trianglesHashSet.Clear();
            if (triangulationGridBitmap != null) triangulationGridBitmap.Dispose();
            triangulationGridBitmap = new Bitmap(triangulationGridPictureBox.Width, triangulationGridPictureBox.Height);
            triangulationGridPictureBox.Image = null;
        }
        private void ResetShading()
        {
            if (rebuiltImageBitmap != null) rebuiltImageBitmap.Dispose();
            rebuiltImageBitmap = new Bitmap(rebuiltImagePictureBox.Width, rebuiltImagePictureBox.Height);
            rebuiltImagePictureBox.Image = null;
        }
        private void SetNewSourcePng()
        {
            if (sourceImageBitmap != null) sourceImageBitmap.Dispose();
            sourceImageBitmap = new Bitmap(openPngFileDialog.FileName);
            originalImagePictureBox.Image = sourceImageBitmap;
        }
    }
}
