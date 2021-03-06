﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace ImageTriangulation.Applicaton
{
    public partial class MainForm : Form
    {
        #region Default_values
        const string DEFAULT_STRIPIG_FACTOR = "0.25";
        const string DEFAULT_CACHE_EXPAND_FACTOR = "0.001";
        const string DEFAULT_SECTOR_SIZE = "2";
        const string DEFAULT_THRESHOLD = "20";
        const string DEFAULT_NUBER_OF_RANDOM_PP = "100000";
        #endregion

        Bitmap sourceImageBitmap;
        Bitmap pivotPointsBitmap;
        Bitmap triangulationGridBitmap;
        Bitmap rebuiltImageBitmap;

        HashSet<Pixel> pivotPoints = new HashSet<Pixel>();
        List<Section> triangulationSectionsList = new List<Section>();
        HashSet<Triangle> trianglesHashSet = new HashSet<Triangle>();
        
        PictureBox pivotPointsPictureBox;
        PictureBox triangulationGridPictureBox;

        byte pPMakerThreshold;
        int sectorSize;
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
            label13.Text = "";
            label14.Text = "";
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
            pivotPointsPictureBox.MouseClick += PivotPointsPictureBox_MouseClick1;

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

            // Инициализируем comboBox'ы
            pPMakersComboBox.Items.AddRange(new string[] { "Выберите алгоритм", "SectorPPMaker1", "Комбинированный алгоритм", "Вручную", "Случайно" });
            pPMakersComboBox.SelectedIndex = 0;
            triangulationsComboBox.Items.AddRange(new string[] { "Выберите алгоритм", "SimpleIterativeTriangulation",
                                                                 "DCIterativeTriangulation", "StripIterativeTriangulation" });
            triangulationsComboBox.SelectedIndex = 0;
            shadersComboBox.Items.AddRange(new string[] { "Выберите алгоритм", "VerticesAverageBrightnessShader", "TriangleGradientShader" });
            shadersComboBox.SelectedIndex = 0;
        }        

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
            label13.Text = "";
            label14.Text = "";
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
            label13.Text = pivotPoints.Count.ToString();
            label14.Text = trianglesHashSet.Count.ToString();
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

                    pPMakerThreshold = byte.Parse(pPMakerThresholdTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);
                    sectorSize = int.Parse(sectorSizeTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);
                    sw = Stopwatch.StartNew();

                    ParallelSectorPPMaker ppm = new ParallelSectorPPMaker(sourceImageBitmap, pivotPoints, pPMakerThreshold, sectorSize);
                    ppm.Run();

                    //SectorPPMaker.Run(sourceImageBitmap, pivotPoints, pPMakerThreshold, sectorSize);

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
                    label13.Text = pivotPoints.Count.ToString();
                    label14.Text = "";
                    standartDeviationLabel.Text = "";
                    saveInPngButton.Enabled = false;
                    saveInTButton.Enabled = true;

                    // Код для вывода в файлы
                    //using (StreamWriter thresholdFile = new StreamWriter(@"D:\VSProjects\image-triangulation\image-triangulation\threshold.txt", true, System.Text.Encoding.Default))
                    //{
                    //    thresholdFile.WriteLine(pPMakerThreshold);
                    //}
                    //using (StreamWriter pointsdFile = new StreamWriter(@"D:\VSProjects\image-triangulation\image-triangulation\points.txt", true, System.Text.Encoding.Default))
                    //{
                    //    pointsdFile.WriteLine(pivotPoints.Count);
                    //}
                    return;
                case 2:
                    ResetShading();
                    ResetTriangulation();
                    ResetPivotPoints();

                    pPMakerThreshold = byte.Parse(pPMakerThresholdTextBox.Text, System.Globalization.CultureInfo.InvariantCulture);
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
                    triangulationControlsGroupBox.Enabled = true;
                    shadingControlsGroupBox.Enabled = true;
                    showPivotPoints.Checked = true;
                    showGrid.Checked = true;
                    label9.Text = "";
                    label10.Text = "";
                    label13.Text = pivotPoints.Count.ToString();
                    label14.Text = trianglesHashSet.Count.ToString();
                    standartDeviationLabel.Text = "";
                    saveInPngButton.Enabled = false;
                    saveInTButton.Enabled = true;

                    // Код для вывода в файлы
                    //using (StreamWriter thresholdFile = new StreamWriter(@"D:\VSProjects\image-triangulation\image-triangulation\threshold.txt", true, System.Text.Encoding.Default))
                    //{
                    //    thresholdFile.WriteLine(pPMakerThreshold);
                    //}
                    //using (StreamWriter pointsdFile = new StreamWriter(@"D:\VSProjects\image-triangulation\image-triangulation\points.txt", true, System.Text.Encoding.Default))
                    //{
                    //    pointsdFile.WriteLine(pivotPoints.Count);
                    //}
                    return;
                case 4:
                    ResetShading();
                    ResetTriangulation();
                    ResetPivotPoints();

                    int numberOfPivotPoints = int.Parse(textBoxNumberOfPivotPoints.Text, System.Globalization.CultureInfo.InvariantCulture);
                    sw = Stopwatch.StartNew();
                    RandomPPMaker.Run(sourceImageBitmap, pivotPoints, numberOfPivotPoints);
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
                    label13.Text = pivotPoints.Count.ToString();
                    label14.Text = "";
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
                    label14.Text = trianglesHashSet.Count.ToString();
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
                    label14.Text = trianglesHashSet.Count.ToString();
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
                    label14.Text = trianglesHashSet.Count.ToString();
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

                    sw = Stopwatch.StartNew();
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
                case 2:
                    ResetShading();

                    sw = Stopwatch.StartNew();

                    TriangleGradientShader shader = new TriangleGradientShader(rebuiltImageBitmap, trianglesHashSet);
                    shader.Run();

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

                    // Код для записи в файл
                    //using (StreamWriter SKOFile = new StreamWriter(@"D:\VSProjects\image-triangulation\image-triangulation\SKO.txt", true, System.Text.Encoding.Default))
                    //{
                    //    SKOFile.WriteLine(standartDeviationLabel.Text);
                    //}

                    return;
            }
        }

        private void PPMakersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (pPMakersComboBox.SelectedIndex)
            {
                case 0:
                    ResetPivotPoints();
                    label13.Text = "";
                    pivotPointsPictureBox.Enabled = false;
                    label7.Visible = false;
                    label15.Visible = false;
                    label16.Visible = false;
                    sectorSizeTextBox.Visible = false;
                    pPMakerThresholdTextBox.Visible = false;
                    thresholdLimitsLabel.Visible = false;
                    runPPMakerButton.Enabled = false;
                    triangulationControlsGroupBox.Enabled = false;
                    // RandomPPMaker
                    labelNumberOfPivotPoints.Visible = false;
                    textBoxNumberOfPivotPoints.Visible = false;
                    return;
                case 1:
                    ResetPivotPoints();
                    label13.Text = "";
                    pivotPointsPictureBox.Enabled = false;
                    label7.Visible = true;
                    label15.Visible = true;
                    label16.Visible = true;
                    sectorSizeTextBox.Visible = true;
                    sectorSizeTextBox.Text = DEFAULT_SECTOR_SIZE;
                    pPMakerThresholdTextBox.Visible = true;
                    pPMakerThresholdTextBox.Text = DEFAULT_THRESHOLD;
                    thresholdLimitsLabel.Visible = true;
                    runPPMakerButton.Enabled = true;
                    triangulationControlsGroupBox.Enabled = false;
                    // RandomPPMaker
                    labelNumberOfPivotPoints.Visible = false;
                    textBoxNumberOfPivotPoints.Visible = false;
                    return;
                case 2:
                    ResetPivotPoints();
                    label13.Text = "";
                    pivotPointsPictureBox.Enabled = false;
                    label7.Visible = true;
                    label15.Visible = false;
                    label16.Visible = false;
                    sectorSizeTextBox.Visible = false;
                    pPMakerThresholdTextBox.Visible = true;
                    pPMakerThresholdTextBox.Text = DEFAULT_THRESHOLD;
                    thresholdLimitsLabel.Visible = true;
                    runPPMakerButton.Enabled = true;
                    triangulationControlsGroupBox.Enabled = false;
                    // RandomPPMaker
                    labelNumberOfPivotPoints.Visible = false;
                    textBoxNumberOfPivotPoints.Visible = false;
                    return;
                case 3:
                    runPPMakerButton.Enabled = false;
                    ResetPivotPoints();
                    // Записываем стартовые точки триангуляции
                    Pixel[] initPixels = { new Pixel(0, 0, sourceImageBitmap.GetPixel(0, 0).R),
                                           new Pixel(sourceImageBitmap.Width - 1, 0, sourceImageBitmap.GetPixel(sourceImageBitmap.Width - 1, 0).R),
                                           new Pixel(sourceImageBitmap.Width - 1, sourceImageBitmap.Height - 1, sourceImageBitmap.GetPixel(sourceImageBitmap.Width - 1, sourceImageBitmap.Height - 1).R),
                                           new Pixel(0, sourceImageBitmap.Height - 1, sourceImageBitmap.GetPixel(0, sourceImageBitmap.Height - 1).R) };
                    for (byte i = 0; i < 4; ++i)
                    {
                        pivotPoints.Add(initPixels[i]);
                        pivotPointsBitmap.SetPixel(initPixels[i].X, initPixels[i].Y, Color.Red);
                    }
                    label13.Text = pivotPoints.Count.ToString();
                    pivotPointsPictureBox.Image = pivotPointsBitmap;
                    pivotPointsPictureBox.Enabled = true;
                    triangulationControlsGroupBox.Enabled = true;
                    label7.Visible = false;
                    label15.Visible = false;
                    label16.Visible = false;
                    sectorSizeTextBox.Visible = false;
                    pPMakerThresholdTextBox.Visible = false;
                    thresholdLimitsLabel.Visible = false;
                    // RandomPPMaker
                    labelNumberOfPivotPoints.Visible = false;
                    textBoxNumberOfPivotPoints.Visible = false;
                    return;
                case 4:
                    ResetPivotPoints();
                    label13.Text = "";
                    pivotPointsPictureBox.Enabled = false;
                    label7.Visible = false;
                    label15.Visible = false;
                    label16.Visible = false;
                    sectorSizeTextBox.Visible = false;
                    pPMakerThresholdTextBox.Visible = false;
                    thresholdLimitsLabel.Visible = false;
                    runPPMakerButton.Enabled = true;
                    triangulationControlsGroupBox.Enabled = false;
                    // RandomPPMaker
                    labelNumberOfPivotPoints.Visible = true;
                    textBoxNumberOfPivotPoints.Visible = true;
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
                    coefOfCacheExpandTextBox.Text = DEFAULT_CACHE_EXPAND_FACTOR;
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

                    stripingFactorTextBox.Text = DEFAULT_STRIPIG_FACTOR;
                    stripingFactorLabel.Visible = true;
                    stripingFactorTextBox.Visible = true;
                    stripingFactorRecomendLabel.Visible = true;
                    return;
            }
        }

        private void PivotPointsPictureBox_MouseClick1(object sender, MouseEventArgs e)
        {
            Pixel pixel = new Pixel(e.Location.X, e.Location.Y, sourceImageBitmap.GetPixel(e.Location.X, e.Location.Y).R);
            pivotPoints.Add(pixel);
            pivotPointsBitmap.SetPixel(pixel.X, pixel.Y, Color.Red);
            label13.Text = pivotPoints.Count.ToString();
            pivotPointsPictureBox.Image = pivotPointsBitmap;
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
