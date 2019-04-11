﻿namespace image_triangulation
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
            this.originalImageHide = new System.Windows.Forms.RadioButton();
            this.originalImageShow = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pivotPointsHide = new System.Windows.Forms.RadioButton();
            this.pivotPointsShow = new System.Windows.Forms.RadioButton();
            this.showHidePPointsGroupBox = new System.Windows.Forms.GroupBox();
            this.gridHide = new System.Windows.Forms.RadioButton();
            this.gridShow = new System.Windows.Forms.RadioButton();
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
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.triangulationControlsGroupBox = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.shadingControlsGroupBox = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.openTButton = new System.Windows.Forms.Button();
            this.saveInPngButton = new System.Windows.Forms.Button();
            this.saveInTButton = new System.Windows.Forms.Button();
            this.openTFileDialog = new System.Windows.Forms.OpenFileDialog();
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
            this.openPngButton.Click += new System.EventHandler(this.OpenImageButton_Click);
            // 
            // openPngFileDialog
            // 
            this.openPngFileDialog.FileName = "openFileDialog1";
            // 
            // showHideImageGroupBox
            // 
            this.showHideImageGroupBox.Controls.Add(this.originalImageHide);
            this.showHideImageGroupBox.Controls.Add(this.originalImageShow);
            this.showHideImageGroupBox.Location = new System.Drawing.Point(7, 578);
            this.showHideImageGroupBox.Name = "showHideImageGroupBox";
            this.showHideImageGroupBox.Size = new System.Drawing.Size(249, 34);
            this.showHideImageGroupBox.TabIndex = 14;
            this.showHideImageGroupBox.TabStop = false;
            this.showHideImageGroupBox.Text = "Изображение";
            // 
            // originalImageHide
            // 
            this.originalImageHide.AutoSize = true;
            this.originalImageHide.Location = new System.Drawing.Point(209, 12);
            this.originalImageHide.Name = "originalImageHide";
            this.originalImageHide.Size = new System.Drawing.Size(17, 16);
            this.originalImageHide.TabIndex = 1;
            this.originalImageHide.TabStop = true;
            this.originalImageHide.UseVisualStyleBackColor = true;
            this.originalImageHide.CheckedChanged += new System.EventHandler(this.PictureLayerOff_CheckedChanged);
            // 
            // originalImageShow
            // 
            this.originalImageShow.AutoSize = true;
            this.originalImageShow.Checked = true;
            this.originalImageShow.Location = new System.Drawing.Point(141, 12);
            this.originalImageShow.Name = "originalImageShow";
            this.originalImageShow.Size = new System.Drawing.Size(17, 16);
            this.originalImageShow.TabIndex = 0;
            this.originalImageShow.TabStop = true;
            this.originalImageShow.UseVisualStyleBackColor = true;
            this.originalImageShow.CheckedChanged += new System.EventHandler(this.PictureLayerOn_CheckedChanged);
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
            // pivotPointsHide
            // 
            this.pivotPointsHide.AutoSize = true;
            this.pivotPointsHide.Location = new System.Drawing.Point(209, 12);
            this.pivotPointsHide.Name = "pivotPointsHide";
            this.pivotPointsHide.Size = new System.Drawing.Size(17, 16);
            this.pivotPointsHide.TabIndex = 1;
            this.pivotPointsHide.TabStop = true;
            this.pivotPointsHide.UseVisualStyleBackColor = true;
            this.pivotPointsHide.CheckedChanged += new System.EventHandler(this.PointsLayerOff_CheckedChanged);
            // 
            // pivotPointsShow
            // 
            this.pivotPointsShow.AutoSize = true;
            this.pivotPointsShow.Checked = true;
            this.pivotPointsShow.Location = new System.Drawing.Point(141, 12);
            this.pivotPointsShow.Name = "pivotPointsShow";
            this.pivotPointsShow.Size = new System.Drawing.Size(17, 16);
            this.pivotPointsShow.TabIndex = 0;
            this.pivotPointsShow.TabStop = true;
            this.pivotPointsShow.UseVisualStyleBackColor = true;
            this.pivotPointsShow.CheckedChanged += new System.EventHandler(this.PointsLayerOn_CheckedChanged);
            // 
            // showHidePPointsGroupBox
            // 
            this.showHidePPointsGroupBox.Controls.Add(this.pivotPointsHide);
            this.showHidePPointsGroupBox.Controls.Add(this.pivotPointsShow);
            this.showHidePPointsGroupBox.Location = new System.Drawing.Point(7, 624);
            this.showHidePPointsGroupBox.Name = "showHidePPointsGroupBox";
            this.showHidePPointsGroupBox.Size = new System.Drawing.Size(249, 34);
            this.showHidePPointsGroupBox.TabIndex = 15;
            this.showHidePPointsGroupBox.TabStop = false;
            this.showHidePPointsGroupBox.Text = "Опорные точки";
            // 
            // gridHide
            // 
            this.gridHide.AutoSize = true;
            this.gridHide.Location = new System.Drawing.Point(209, 12);
            this.gridHide.Name = "gridHide";
            this.gridHide.Size = new System.Drawing.Size(17, 16);
            this.gridHide.TabIndex = 1;
            this.gridHide.TabStop = true;
            this.gridHide.UseVisualStyleBackColor = true;
            this.gridHide.CheckedChanged += new System.EventHandler(this.GridLayerOff_CheckedChanged);
            // 
            // gridShow
            // 
            this.gridShow.AutoSize = true;
            this.gridShow.Checked = true;
            this.gridShow.Location = new System.Drawing.Point(141, 12);
            this.gridShow.Name = "gridShow";
            this.gridShow.Size = new System.Drawing.Size(17, 16);
            this.gridShow.TabIndex = 0;
            this.gridShow.TabStop = true;
            this.gridShow.UseVisualStyleBackColor = true;
            this.gridShow.CheckedChanged += new System.EventHandler(this.GridLayerOn_CheckedChanged);
            // 
            // showHideGridGroupBox
            // 
            this.showHideGridGroupBox.Controls.Add(this.gridHide);
            this.showHideGridGroupBox.Controls.Add(this.gridShow);
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
            this.runPPMakerButton.Click += new System.EventHandler(this.runPPMakerButton_Click);
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
            // 
            // triangulationsComboBox
            // 
            this.triangulationsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.triangulationsComboBox.FormattingEnabled = true;
            this.triangulationsComboBox.Location = new System.Drawing.Point(6, 21);
            this.triangulationsComboBox.Name = "triangulationsComboBox";
            this.triangulationsComboBox.Size = new System.Drawing.Size(143, 24);
            this.triangulationsComboBox.TabIndex = 23;
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
            // 
            // pPointsControlsGroupBox
            // 
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 52);
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
            this.label3.Size = new System.Drawing.Size(169, 17);
            this.label3.TabIndex = 23;
            this.label3.Text = "Время выполнения, сек:";
            // 
            // triangulationControlsGroupBox
            // 
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(169, 17);
            this.label4.TabIndex = 24;
            this.label4.Text = "Время выполнения, сек:";
            // 
            // shadingControlsGroupBox
            // 
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(169, 17);
            this.label6.TabIndex = 27;
            this.label6.Text = "Время выполнения, сек:";
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
            this.openTButton.Enabled = false;
            this.openTButton.Location = new System.Drawing.Point(140, 5);
            this.openTButton.Name = "openTButton";
            this.openTButton.Size = new System.Drawing.Size(130, 30);
            this.openTButton.TabIndex = 29;
            this.openTButton.Text = "Открыть .t";
            this.openTButton.UseVisualStyleBackColor = true;
            // 
            // saveInPngButton
            // 
            this.saveInPngButton.Enabled = false;
            this.saveInPngButton.Location = new System.Drawing.Point(275, 5);
            this.saveInPngButton.Name = "saveInPngButton";
            this.saveInPngButton.Size = new System.Drawing.Size(130, 30);
            this.saveInPngButton.TabIndex = 30;
            this.saveInPngButton.Text = "Сохранить в .png";
            this.saveInPngButton.UseVisualStyleBackColor = true;
            // 
            // saveInTButton
            // 
            this.saveInTButton.Enabled = false;
            this.saveInTButton.Location = new System.Drawing.Point(410, 5);
            this.saveInTButton.Name = "saveInTButton";
            this.saveInTButton.Size = new System.Drawing.Size(130, 30);
            this.saveInTButton.TabIndex = 31;
            this.saveInTButton.Text = "Сохранить в .t";
            this.saveInTButton.UseVisualStyleBackColor = true;
            // 
            // openTFileDialog
            // 
            this.openTFileDialog.FileName = "openFileDialog1";
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
        private System.Windows.Forms.RadioButton originalImageHide;
        private System.Windows.Forms.RadioButton originalImageShow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton pivotPointsHide;
        private System.Windows.Forms.RadioButton pivotPointsShow;
        private System.Windows.Forms.GroupBox showHidePPointsGroupBox;
        private System.Windows.Forms.RadioButton gridHide;
        private System.Windows.Forms.RadioButton gridShow;
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
    }
}

