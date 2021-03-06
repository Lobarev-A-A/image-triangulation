﻿namespace ImageTriangulation.Applicaton
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.originalImagePictureBox = new System.Windows.Forms.PictureBox();
            this.openPngButton = new System.Windows.Forms.Button();
            this.openPngFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.showHideImageGroupBox = new System.Windows.Forms.GroupBox();
            this.hideOriginalImage = new System.Windows.Forms.RadioButton();
            this.showOriginalImage = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.hidePivotPoints = new System.Windows.Forms.RadioButton();
            this.showPivotPoints = new System.Windows.Forms.RadioButton();
            this.showHidePPointsGroupBox = new System.Windows.Forms.GroupBox();
            this.hideGrid = new System.Windows.Forms.RadioButton();
            this.showGrid = new System.Windows.Forms.RadioButton();
            this.showHideGridGroupBox = new System.Windows.Forms.GroupBox();
            this.runTriangulationButton = new System.Windows.Forms.Button();
            this.runPPMakerButton = new System.Windows.Forms.Button();
            this.pPMakerThresholdTextBox = new System.Windows.Forms.TextBox();
            this.rebuiltImagePictureBox = new System.Windows.Forms.PictureBox();
            this.pPMakersComboBox = new System.Windows.Forms.ComboBox();
            this.triangulationsComboBox = new System.Windows.Forms.ComboBox();
            this.shadersComboBox = new System.Windows.Forms.ComboBox();
            this.runShaderButton = new System.Windows.Forms.Button();
            this.pPointsControlsGroupBox = new System.Windows.Forms.GroupBox();
            this.textBoxNumberOfPivotPoints = new System.Windows.Forms.TextBox();
            this.labelNumberOfPivotPoints = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.sectorSizeTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.thresholdLimitsLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.triangulationControlsGroupBox = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.stripingFactorRecomendLabel = new System.Windows.Forms.Label();
            this.stripingFactorTextBox = new System.Windows.Forms.TextBox();
            this.stripingFactorLabel = new System.Windows.Forms.Label();
            this.coefOfCacheExpandRecomendLabel = new System.Windows.Forms.Label();
            this.coefOfCacheExpandTextBox = new System.Windows.Forms.TextBox();
            this.coefOfCacheExpandLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.shadingControlsGroupBox = new System.Windows.Forms.GroupBox();
            this.standartDeviationLabel = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.openTButton = new System.Windows.Forms.Button();
            this.saveInPngButton = new System.Windows.Forms.Button();
            this.saveInTButton = new System.Windows.Forms.Button();
            this.openTFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.savePngFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveTFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.label13 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.originalImagePictureBox)).BeginInit();
            this.showHideImageGroupBox.SuspendLayout();
            this.showHidePPointsGroupBox.SuspendLayout();
            this.showHideGridGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rebuiltImagePictureBox)).BeginInit();
            this.pPointsControlsGroupBox.SuspendLayout();
            this.triangulationControlsGroupBox.SuspendLayout();
            this.shadingControlsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // originalImagePictureBox
            // 
            this.originalImagePictureBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.originalImagePictureBox.Location = new System.Drawing.Point(7, 40);
            this.originalImagePictureBox.Name = "originalImagePictureBox";
            this.originalImagePictureBox.Size = new System.Drawing.Size(512, 512);
            this.originalImagePictureBox.TabIndex = 0;
            this.originalImagePictureBox.TabStop = false;
            // 
            // openPngButton
            // 
            this.openPngButton.Location = new System.Drawing.Point(5, 5);
            this.openPngButton.Name = "openPngButton";
            this.openPngButton.Size = new System.Drawing.Size(130, 30);
            this.openPngButton.TabIndex = 12;
            this.openPngButton.Text = "Открыть .png";
            this.openPngButton.UseVisualStyleBackColor = true;
            this.openPngButton.Click += new System.EventHandler(this.OpenPngButton_Click);
            // 
            // showHideImageGroupBox
            // 
            this.showHideImageGroupBox.Controls.Add(this.hideOriginalImage);
            this.showHideImageGroupBox.Controls.Add(this.showOriginalImage);
            this.showHideImageGroupBox.Location = new System.Drawing.Point(7, 578);
            this.showHideImageGroupBox.Name = "showHideImageGroupBox";
            this.showHideImageGroupBox.Size = new System.Drawing.Size(249, 34);
            this.showHideImageGroupBox.TabIndex = 14;
            this.showHideImageGroupBox.TabStop = false;
            this.showHideImageGroupBox.Text = "Изображение";
            // 
            // hideOriginalImage
            // 
            this.hideOriginalImage.AutoSize = true;
            this.hideOriginalImage.Location = new System.Drawing.Point(209, 12);
            this.hideOriginalImage.Name = "hideOriginalImage";
            this.hideOriginalImage.Size = new System.Drawing.Size(17, 16);
            this.hideOriginalImage.TabIndex = 1;
            this.hideOriginalImage.TabStop = true;
            this.hideOriginalImage.UseVisualStyleBackColor = true;
            this.hideOriginalImage.CheckedChanged += new System.EventHandler(this.HideOriginalImage_CheckedChanged);
            // 
            // showOriginalImage
            // 
            this.showOriginalImage.AutoSize = true;
            this.showOriginalImage.Checked = true;
            this.showOriginalImage.Location = new System.Drawing.Point(141, 12);
            this.showOriginalImage.Name = "showOriginalImage";
            this.showOriginalImage.Size = new System.Drawing.Size(17, 16);
            this.showOriginalImage.TabIndex = 0;
            this.showOriginalImage.TabStop = true;
            this.showOriginalImage.UseVisualStyleBackColor = true;
            this.showOriginalImage.CheckedChanged += new System.EventHandler(this.ShowOriginalImage_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 566);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Показать";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 566);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "Скрыть";
            // 
            // hidePivotPoints
            // 
            this.hidePivotPoints.AutoSize = true;
            this.hidePivotPoints.Location = new System.Drawing.Point(209, 12);
            this.hidePivotPoints.Name = "hidePivotPoints";
            this.hidePivotPoints.Size = new System.Drawing.Size(17, 16);
            this.hidePivotPoints.TabIndex = 1;
            this.hidePivotPoints.TabStop = true;
            this.hidePivotPoints.UseVisualStyleBackColor = true;
            this.hidePivotPoints.CheckedChanged += new System.EventHandler(this.HidePivotPoints_CheckedChanged);
            // 
            // showPivotPoints
            // 
            this.showPivotPoints.AutoSize = true;
            this.showPivotPoints.Checked = true;
            this.showPivotPoints.Location = new System.Drawing.Point(141, 12);
            this.showPivotPoints.Name = "showPivotPoints";
            this.showPivotPoints.Size = new System.Drawing.Size(17, 16);
            this.showPivotPoints.TabIndex = 0;
            this.showPivotPoints.TabStop = true;
            this.showPivotPoints.UseVisualStyleBackColor = true;
            this.showPivotPoints.CheckedChanged += new System.EventHandler(this.ShowPivotPoints_CheckedChanged);
            // 
            // showHidePPointsGroupBox
            // 
            this.showHidePPointsGroupBox.Controls.Add(this.hidePivotPoints);
            this.showHidePPointsGroupBox.Controls.Add(this.showPivotPoints);
            this.showHidePPointsGroupBox.Location = new System.Drawing.Point(7, 624);
            this.showHidePPointsGroupBox.Name = "showHidePPointsGroupBox";
            this.showHidePPointsGroupBox.Size = new System.Drawing.Size(249, 34);
            this.showHidePPointsGroupBox.TabIndex = 15;
            this.showHidePPointsGroupBox.TabStop = false;
            this.showHidePPointsGroupBox.Text = "Опорные точки";
            // 
            // hideGrid
            // 
            this.hideGrid.AutoSize = true;
            this.hideGrid.Location = new System.Drawing.Point(209, 12);
            this.hideGrid.Name = "hideGrid";
            this.hideGrid.Size = new System.Drawing.Size(17, 16);
            this.hideGrid.TabIndex = 1;
            this.hideGrid.TabStop = true;
            this.hideGrid.UseVisualStyleBackColor = true;
            this.hideGrid.CheckedChanged += new System.EventHandler(this.HideGrid_CheckedChanged);
            // 
            // showGrid
            // 
            this.showGrid.AutoSize = true;
            this.showGrid.Checked = true;
            this.showGrid.Location = new System.Drawing.Point(141, 12);
            this.showGrid.Name = "showGrid";
            this.showGrid.Size = new System.Drawing.Size(17, 16);
            this.showGrid.TabIndex = 0;
            this.showGrid.TabStop = true;
            this.showGrid.UseVisualStyleBackColor = true;
            this.showGrid.CheckedChanged += new System.EventHandler(this.ShowGrid_CheckedChanged);
            // 
            // showHideGridGroupBox
            // 
            this.showHideGridGroupBox.Controls.Add(this.hideGrid);
            this.showHideGridGroupBox.Controls.Add(this.showGrid);
            this.showHideGridGroupBox.Location = new System.Drawing.Point(7, 670);
            this.showHideGridGroupBox.Name = "showHideGridGroupBox";
            this.showHideGridGroupBox.Size = new System.Drawing.Size(249, 34);
            this.showHideGridGroupBox.TabIndex = 16;
            this.showHideGridGroupBox.TabStop = false;
            this.showHideGridGroupBox.Text = "Сетка";
            // 
            // runTriangulationButton
            // 
            this.runTriangulationButton.Location = new System.Drawing.Point(154, 20);
            this.runTriangulationButton.Name = "runTriangulationButton";
            this.runTriangulationButton.Size = new System.Drawing.Size(95, 23);
            this.runTriangulationButton.TabIndex = 17;
            this.runTriangulationButton.Text = "Выполнить";
            this.runTriangulationButton.UseVisualStyleBackColor = true;
            this.runTriangulationButton.Click += new System.EventHandler(this.RunTriangulation_Click);
            // 
            // runPPMakerButton
            // 
            this.runPPMakerButton.Location = new System.Drawing.Point(154, 20);
            this.runPPMakerButton.Name = "runPPMakerButton";
            this.runPPMakerButton.Size = new System.Drawing.Size(95, 23);
            this.runPPMakerButton.TabIndex = 19;
            this.runPPMakerButton.Text = "Выполнить";
            this.runPPMakerButton.UseVisualStyleBackColor = true;
            this.runPPMakerButton.Click += new System.EventHandler(this.RunPPMakerButton_Click);
            // 
            // pPMakerThresholdTextBox
            // 
            this.pPMakerThresholdTextBox.Location = new System.Drawing.Point(63, 49);
            this.pPMakerThresholdTextBox.Name = "pPMakerThresholdTextBox";
            this.pPMakerThresholdTextBox.Size = new System.Drawing.Size(86, 22);
            this.pPMakerThresholdTextBox.TabIndex = 20;
            // 
            // rebuiltImagePictureBox
            // 
            this.rebuiltImagePictureBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rebuiltImagePictureBox.Location = new System.Drawing.Point(526, 40);
            this.rebuiltImagePictureBox.Name = "rebuiltImagePictureBox";
            this.rebuiltImagePictureBox.Size = new System.Drawing.Size(512, 512);
            this.rebuiltImagePictureBox.TabIndex = 21;
            this.rebuiltImagePictureBox.TabStop = false;
            // 
            // pPMakersComboBox
            // 
            this.pPMakersComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pPMakersComboBox.FormattingEnabled = true;
            this.pPMakersComboBox.Location = new System.Drawing.Point(6, 21);
            this.pPMakersComboBox.Name = "pPMakersComboBox";
            this.pPMakersComboBox.Size = new System.Drawing.Size(143, 24);
            this.pPMakersComboBox.TabIndex = 22;
            this.pPMakersComboBox.SelectedIndexChanged += new System.EventHandler(this.PPMakersComboBox_SelectedIndexChanged);
            // 
            // triangulationsComboBox
            // 
            this.triangulationsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.triangulationsComboBox.FormattingEnabled = true;
            this.triangulationsComboBox.Location = new System.Drawing.Point(6, 21);
            this.triangulationsComboBox.Name = "triangulationsComboBox";
            this.triangulationsComboBox.Size = new System.Drawing.Size(143, 24);
            this.triangulationsComboBox.TabIndex = 23;
            this.triangulationsComboBox.SelectedIndexChanged += new System.EventHandler(this.TriangulationsComboBox_SelectedIndexChanged);
            // 
            // shadersComboBox
            // 
            this.shadersComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shadersComboBox.FormattingEnabled = true;
            this.shadersComboBox.Location = new System.Drawing.Point(6, 21);
            this.shadersComboBox.Name = "shadersComboBox";
            this.shadersComboBox.Size = new System.Drawing.Size(143, 24);
            this.shadersComboBox.TabIndex = 24;
            // 
            // runShaderButton
            // 
            this.runShaderButton.Location = new System.Drawing.Point(154, 20);
            this.runShaderButton.Name = "runShaderButton";
            this.runShaderButton.Size = new System.Drawing.Size(95, 23);
            this.runShaderButton.TabIndex = 25;
            this.runShaderButton.Text = "Выполнить";
            this.runShaderButton.UseVisualStyleBackColor = true;
            this.runShaderButton.Click += new System.EventHandler(this.RunShaderButton_Click);
            // 
            // pPointsControlsGroupBox
            // 
            this.pPointsControlsGroupBox.Controls.Add(this.label13);
            this.pPointsControlsGroupBox.Controls.Add(this.textBoxNumberOfPivotPoints);
            this.pPointsControlsGroupBox.Controls.Add(this.labelNumberOfPivotPoints);
            this.pPointsControlsGroupBox.Controls.Add(this.label16);
            this.pPointsControlsGroupBox.Controls.Add(this.label15);
            this.pPointsControlsGroupBox.Controls.Add(this.sectorSizeTextBox);
            this.pPointsControlsGroupBox.Controls.Add(this.label11);
            this.pPointsControlsGroupBox.Controls.Add(this.thresholdLimitsLabel);
            this.pPointsControlsGroupBox.Controls.Add(this.label8);
            this.pPointsControlsGroupBox.Controls.Add(this.label7);
            this.pPointsControlsGroupBox.Controls.Add(this.label3);
            this.pPointsControlsGroupBox.Controls.Add(this.pPMakersComboBox);
            this.pPointsControlsGroupBox.Controls.Add(this.runPPMakerButton);
            this.pPointsControlsGroupBox.Controls.Add(this.pPMakerThresholdTextBox);
            this.pPointsControlsGroupBox.Location = new System.Drawing.Point(264, 555);
            this.pPointsControlsGroupBox.Name = "pPointsControlsGroupBox";
            this.pPointsControlsGroupBox.Size = new System.Drawing.Size(255, 150);
            this.pPointsControlsGroupBox.TabIndex = 26;
            this.pPointsControlsGroupBox.TabStop = false;
            this.pPointsControlsGroupBox.Text = "Опорные точки";
            // 
            // textBoxNumberOfPivotPoints
            // 
            this.textBoxNumberOfPivotPoints.Location = new System.Drawing.Point(63, 49);
            this.textBoxNumberOfPivotPoints.Name = "textBoxNumberOfPivotPoints";
            this.textBoxNumberOfPivotPoints.Size = new System.Drawing.Size(86, 22);
            this.textBoxNumberOfPivotPoints.TabIndex = 36;
            // 
            // labelNumberOfPivotPoints
            // 
            this.labelNumberOfPivotPoints.AutoSize = true;
            this.labelNumberOfPivotPoints.Location = new System.Drawing.Point(6, 54);
            this.labelNumberOfPivotPoints.Name = "labelNumberOfPivotPoints";
            this.labelNumberOfPivotPoints.Size = new System.Drawing.Size(52, 17);
            this.labelNumberOfPivotPoints.TabIndex = 35;
            this.labelNumberOfPivotPoints.Text = "Точек:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(154, 82);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(62, 17);
            this.label16.TabIndex = 34;
            this.label16.Text = "(1...100)";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(6, 73);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(95, 31);
            this.label15.TabIndex = 33;
            this.label15.Text = "Размерность сектора:";
            // 
            // sectorSizeTextBox
            // 
            this.sectorSizeTextBox.Location = new System.Drawing.Point(101, 77);
            this.sectorSizeTextBox.Name = "sectorSizeTextBox";
            this.sectorSizeTextBox.Size = new System.Drawing.Size(48, 22);
            this.sectorSizeTextBox.TabIndex = 30;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 105);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 17);
            this.label11.TabIndex = 27;
            this.label11.Text = "Точек:";
            // 
            // thresholdLimitsLabel
            // 
            this.thresholdLimitsLabel.AutoSize = true;
            this.thresholdLimitsLabel.Location = new System.Drawing.Point(154, 54);
            this.thresholdLimitsLabel.Name = "thresholdLimitsLabel";
            this.thresholdLimitsLabel.Size = new System.Drawing.Size(62, 17);
            this.thresholdLimitsLabel.TabIndex = 26;
            this.thresholdLimitsLabel.Text = "(0...255)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(145, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 17);
            this.label8.TabIndex = 25;
            this.label8.Text = "label8";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 17);
            this.label7.TabIndex = 24;
            this.label7.Text = "Порог:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 17);
            this.label3.TabIndex = 23;
            this.label3.Text = "Время выполнения, мс:";
            // 
            // triangulationControlsGroupBox
            // 
            this.triangulationControlsGroupBox.Controls.Add(this.label14);
            this.triangulationControlsGroupBox.Controls.Add(this.label12);
            this.triangulationControlsGroupBox.Controls.Add(this.stripingFactorRecomendLabel);
            this.triangulationControlsGroupBox.Controls.Add(this.stripingFactorTextBox);
            this.triangulationControlsGroupBox.Controls.Add(this.stripingFactorLabel);
            this.triangulationControlsGroupBox.Controls.Add(this.coefOfCacheExpandRecomendLabel);
            this.triangulationControlsGroupBox.Controls.Add(this.coefOfCacheExpandTextBox);
            this.triangulationControlsGroupBox.Controls.Add(this.coefOfCacheExpandLabel);
            this.triangulationControlsGroupBox.Controls.Add(this.label9);
            this.triangulationControlsGroupBox.Controls.Add(this.label4);
            this.triangulationControlsGroupBox.Controls.Add(this.runTriangulationButton);
            this.triangulationControlsGroupBox.Controls.Add(this.triangulationsComboBox);
            this.triangulationControlsGroupBox.Location = new System.Drawing.Point(524, 555);
            this.triangulationControlsGroupBox.Name = "triangulationControlsGroupBox";
            this.triangulationControlsGroupBox.Size = new System.Drawing.Size(255, 150);
            this.triangulationControlsGroupBox.TabIndex = 27;
            this.triangulationControlsGroupBox.TabStop = false;
            this.triangulationControlsGroupBox.Text = "Триангуляция";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(116, 105);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 17);
            this.label14.TabIndex = 29;
            this.label14.Text = "label14";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 105);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(110, 17);
            this.label12.TabIndex = 28;
            this.label12.Text = "Треугольников:";
            // 
            // stripingFactorRecomendLabel
            // 
            this.stripingFactorRecomendLabel.AutoSize = true;
            this.stripingFactorRecomendLabel.Location = new System.Drawing.Point(169, 75);
            this.stripingFactorRecomendLabel.Name = "stripingFactorRecomendLabel";
            this.stripingFactorRecomendLabel.Size = new System.Drawing.Size(107, 17);
            this.stripingFactorRecomendLabel.TabIndex = 32;
            this.stripingFactorRecomendLabel.Text = "рек. 0.15...0.19";
            // 
            // stripingFactorTextBox
            // 
            this.stripingFactorTextBox.Location = new System.Drawing.Point(90, 70);
            this.stripingFactorTextBox.Name = "stripingFactorTextBox";
            this.stripingFactorTextBox.Size = new System.Drawing.Size(71, 22);
            this.stripingFactorTextBox.TabIndex = 31;
            // 
            // stripingFactorLabel
            // 
            this.stripingFactorLabel.Location = new System.Drawing.Point(6, 52);
            this.stripingFactorLabel.Name = "stripingFactorLabel";
            this.stripingFactorLabel.Size = new System.Drawing.Size(102, 51);
            this.stripingFactorLabel.TabIndex = 30;
            this.stripingFactorLabel.Text = "Коэффициент разбиения на полосы:";
            // 
            // coefOfCacheExpandRecomendLabel
            // 
            this.coefOfCacheExpandRecomendLabel.AutoSize = true;
            this.coefOfCacheExpandRecomendLabel.Location = new System.Drawing.Point(199, 63);
            this.coefOfCacheExpandRecomendLabel.Name = "coefOfCacheExpandRecomendLabel";
            this.coefOfCacheExpandRecomendLabel.Size = new System.Drawing.Size(67, 17);
            this.coefOfCacheExpandRecomendLabel.TabIndex = 29;
            this.coefOfCacheExpandRecomendLabel.Text = "рек. 3...8";
            // 
            // coefOfCacheExpandTextBox
            // 
            this.coefOfCacheExpandTextBox.Location = new System.Drawing.Point(127, 58);
            this.coefOfCacheExpandTextBox.Name = "coefOfCacheExpandTextBox";
            this.coefOfCacheExpandTextBox.Size = new System.Drawing.Size(64, 22);
            this.coefOfCacheExpandTextBox.TabIndex = 28;
            // 
            // coefOfCacheExpandLabel
            // 
            this.coefOfCacheExpandLabel.Location = new System.Drawing.Point(6, 52);
            this.coefOfCacheExpandLabel.Name = "coefOfCacheExpandLabel";
            this.coefOfCacheExpandLabel.Size = new System.Drawing.Size(152, 34);
            this.coefOfCacheExpandLabel.TabIndex = 27;
            this.coefOfCacheExpandLabel.Text = "Коэффициент роста динамического кэша:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(145, 126);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 17);
            this.label9.TabIndex = 26;
            this.label9.Text = "label9";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(163, 17);
            this.label4.TabIndex = 24;
            this.label4.Text = "Время выполнения, мс:";
            // 
            // shadingControlsGroupBox
            // 
            this.shadingControlsGroupBox.Controls.Add(this.standartDeviationLabel);
            this.shadingControlsGroupBox.Controls.Add(this.label10);
            this.shadingControlsGroupBox.Controls.Add(this.label6);
            this.shadingControlsGroupBox.Controls.Add(this.label5);
            this.shadingControlsGroupBox.Controls.Add(this.shadersComboBox);
            this.shadingControlsGroupBox.Controls.Add(this.runShaderButton);
            this.shadingControlsGroupBox.Location = new System.Drawing.Point(784, 555);
            this.shadingControlsGroupBox.Name = "shadingControlsGroupBox";
            this.shadingControlsGroupBox.Size = new System.Drawing.Size(255, 150);
            this.shadingControlsGroupBox.TabIndex = 28;
            this.shadingControlsGroupBox.TabStop = false;
            this.shadingControlsGroupBox.Text = "Закраска";
            // 
            // standartDeviationLabel
            // 
            this.standartDeviationLabel.Location = new System.Drawing.Point(47, 100);
            this.standartDeviationLabel.Name = "standartDeviationLabel";
            this.standartDeviationLabel.Size = new System.Drawing.Size(71, 22);
            this.standartDeviationLabel.TabIndex = 33;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(145, 126);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 17);
            this.label10.TabIndex = 27;
            this.label10.Text = "label10";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(163, 17);
            this.label6.TabIndex = 27;
            this.label6.Text = "Время выполнения, мс:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 17);
            this.label5.TabIndex = 26;
            this.label5.Text = "СКО:";
            // 
            // openTButton
            // 
            this.openTButton.Location = new System.Drawing.Point(140, 5);
            this.openTButton.Name = "openTButton";
            this.openTButton.Size = new System.Drawing.Size(130, 30);
            this.openTButton.TabIndex = 29;
            this.openTButton.Text = "Открыть .t";
            this.openTButton.UseVisualStyleBackColor = true;
            this.openTButton.Click += new System.EventHandler(this.OpenTButton_Click);
            // 
            // saveInPngButton
            // 
            this.saveInPngButton.Location = new System.Drawing.Point(275, 5);
            this.saveInPngButton.Name = "saveInPngButton";
            this.saveInPngButton.Size = new System.Drawing.Size(130, 30);
            this.saveInPngButton.TabIndex = 30;
            this.saveInPngButton.Text = "Сохранить в .png";
            this.saveInPngButton.UseVisualStyleBackColor = true;
            this.saveInPngButton.Click += new System.EventHandler(this.SaveInPngButton_Click);
            // 
            // saveInTButton
            // 
            this.saveInTButton.Location = new System.Drawing.Point(410, 5);
            this.saveInTButton.Name = "saveInTButton";
            this.saveInTButton.Size = new System.Drawing.Size(130, 30);
            this.saveInTButton.TabIndex = 31;
            this.saveInTButton.Text = "Сохранить в .t";
            this.saveInTButton.UseVisualStyleBackColor = true;
            this.saveInTButton.Click += new System.EventHandler(this.SaveInTButton_Click);
            // 
            // openTFileDialog
            // 
            this.openTFileDialog.FileName = "openFileDialog1";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(58, 100);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 22);
            this.label13.TabIndex = 34;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1045, 710);
            this.Controls.Add(this.saveInTButton);
            this.Controls.Add(this.saveInPngButton);
            this.Controls.Add(this.openTButton);
            this.Controls.Add(this.shadingControlsGroupBox);
            this.Controls.Add(this.triangulationControlsGroupBox);
            this.Controls.Add(this.openPngButton);
            this.Controls.Add(this.pPointsControlsGroupBox);
            this.Controls.Add(this.rebuiltImagePictureBox);
            this.Controls.Add(this.showHideGridGroupBox);
            this.Controls.Add(this.showHidePPointsGroupBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.showHideImageGroupBox);
            this.Controls.Add(this.originalImagePictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.originalImagePictureBox)).EndInit();
            this.showHideImageGroupBox.ResumeLayout(false);
            this.showHideImageGroupBox.PerformLayout();
            this.showHidePPointsGroupBox.ResumeLayout(false);
            this.showHidePPointsGroupBox.PerformLayout();
            this.showHideGridGroupBox.ResumeLayout(false);
            this.showHideGridGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rebuiltImagePictureBox)).EndInit();
            this.pPointsControlsGroupBox.ResumeLayout(false);
            this.pPointsControlsGroupBox.PerformLayout();
            this.triangulationControlsGroupBox.ResumeLayout(false);
            this.triangulationControlsGroupBox.PerformLayout();
            this.shadingControlsGroupBox.ResumeLayout(false);
            this.shadingControlsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox originalImagePictureBox;
        private System.Windows.Forms.Button openPngButton;
        private System.Windows.Forms.OpenFileDialog openPngFileDialog;
        private System.Windows.Forms.GroupBox showHideImageGroupBox;
        private System.Windows.Forms.RadioButton hideOriginalImage;
        private System.Windows.Forms.RadioButton showOriginalImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton hidePivotPoints;
        private System.Windows.Forms.RadioButton showPivotPoints;
        private System.Windows.Forms.GroupBox showHidePPointsGroupBox;
        private System.Windows.Forms.RadioButton hideGrid;
        private System.Windows.Forms.RadioButton showGrid;
        private System.Windows.Forms.GroupBox showHideGridGroupBox;
        private System.Windows.Forms.Button runTriangulationButton;
        private System.Windows.Forms.Button runPPMakerButton;
        private System.Windows.Forms.TextBox pPMakerThresholdTextBox;
        private System.Windows.Forms.PictureBox rebuiltImagePictureBox;
        private System.Windows.Forms.ComboBox pPMakersComboBox;
        private System.Windows.Forms.ComboBox triangulationsComboBox;
        private System.Windows.Forms.ComboBox shadersComboBox;
        private System.Windows.Forms.Button runShaderButton;
        private System.Windows.Forms.GroupBox pPointsControlsGroupBox;
        private System.Windows.Forms.GroupBox triangulationControlsGroupBox;
        private System.Windows.Forms.GroupBox shadingControlsGroupBox;
        private System.Windows.Forms.Button openTButton;
        private System.Windows.Forms.Button saveInPngButton;
        private System.Windows.Forms.Button saveInTButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.OpenFileDialog openTFileDialog;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label thresholdLimitsLabel;
        private System.Windows.Forms.SaveFileDialog savePngFileDialog;
        private System.Windows.Forms.SaveFileDialog saveTFileDialog;
        private System.Windows.Forms.Label coefOfCacheExpandLabel;
        private System.Windows.Forms.TextBox coefOfCacheExpandTextBox;
        private System.Windows.Forms.Label coefOfCacheExpandRecomendLabel;
        private System.Windows.Forms.Label stripingFactorRecomendLabel;
        private System.Windows.Forms.TextBox stripingFactorTextBox;
        private System.Windows.Forms.Label stripingFactorLabel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox sectorSizeTextBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBoxNumberOfPivotPoints;
        private System.Windows.Forms.Label labelNumberOfPivotPoints;
        private System.Windows.Forms.TextBox standartDeviationLabel;
        private System.Windows.Forms.TextBox label13;
    }
}

