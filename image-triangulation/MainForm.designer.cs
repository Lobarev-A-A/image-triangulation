namespace image_triangulation
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
            this.OriginalImagePictureBox = new System.Windows.Forms.PictureBox();
            this.OpenImageButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PictureLayerOff = new System.Windows.Forms.RadioButton();
            this.PictureLayerOn = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PointsLayerOff = new System.Windows.Forms.RadioButton();
            this.PointsLayerOn = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.GridLayerOff = new System.Windows.Forms.RadioButton();
            this.GridLayerOn = new System.Windows.Forms.RadioButton();
            this.GridControls = new System.Windows.Forms.GroupBox();
            this.RunTriangulation = new System.Windows.Forms.Button();
            this.ResetTriangulation = new System.Windows.Forms.Button();
            this.MakePivotPointsButton = new System.Windows.Forms.Button();
            this.PPMakerThreshold = new System.Windows.Forms.TextBox();
            this.RebuiltImagePictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.OriginalImagePictureBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.GridControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RebuiltImagePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OriginalImagePictureBox
            // 
            this.OriginalImagePictureBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.OriginalImagePictureBox.Location = new System.Drawing.Point(12, 12);
            this.OriginalImagePictureBox.MaximumSize = new System.Drawing.Size(512, 512);
            this.OriginalImagePictureBox.MinimumSize = new System.Drawing.Size(512, 512);
            this.OriginalImagePictureBox.Name = "OriginalImagePictureBox";
            this.OriginalImagePictureBox.Size = new System.Drawing.Size(512, 512);
            this.OriginalImagePictureBox.TabIndex = 0;
            this.OriginalImagePictureBox.TabStop = false;
            // 
            // OpenImageButton
            // 
            this.OpenImageButton.Location = new System.Drawing.Point(12, 530);
            this.OpenImageButton.Name = "OpenImageButton";
            this.OpenImageButton.Size = new System.Drawing.Size(136, 30);
            this.OpenImageButton.TabIndex = 12;
            this.OpenImageButton.Text = "Открыть";
            this.OpenImageButton.UseVisualStyleBackColor = true;
            this.OpenImageButton.Click += new System.EventHandler(this.OpenImageButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PictureLayerOff);
            this.groupBox1.Controls.Add(this.PictureLayerOn);
            this.groupBox1.Location = new System.Drawing.Point(12, 586);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 34);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Изображение";
            // 
            // PictureLayerOff
            // 
            this.PictureLayerOff.AutoSize = true;
            this.PictureLayerOff.Location = new System.Drawing.Point(201, 12);
            this.PictureLayerOff.Name = "PictureLayerOff";
            this.PictureLayerOff.Size = new System.Drawing.Size(17, 16);
            this.PictureLayerOff.TabIndex = 1;
            this.PictureLayerOff.TabStop = true;
            this.PictureLayerOff.UseVisualStyleBackColor = true;
            this.PictureLayerOff.CheckedChanged += new System.EventHandler(this.PictureLayerOff_CheckedChanged);
            // 
            // PictureLayerOn
            // 
            this.PictureLayerOn.AutoSize = true;
            this.PictureLayerOn.Checked = true;
            this.PictureLayerOn.Location = new System.Drawing.Point(133, 12);
            this.PictureLayerOn.Name = "PictureLayerOn";
            this.PictureLayerOn.Size = new System.Drawing.Size(17, 16);
            this.PictureLayerOn.TabIndex = 0;
            this.PictureLayerOn.TabStop = true;
            this.PictureLayerOn.UseVisualStyleBackColor = true;
            this.PictureLayerOn.CheckedChanged += new System.EventHandler(this.PictureLayerOn_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(120, 571);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Показать";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 571);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "Скрыть";
            // 
            // PointsLayerOff
            // 
            this.PointsLayerOff.AutoSize = true;
            this.PointsLayerOff.Location = new System.Drawing.Point(201, 12);
            this.PointsLayerOff.Name = "PointsLayerOff";
            this.PointsLayerOff.Size = new System.Drawing.Size(17, 16);
            this.PointsLayerOff.TabIndex = 1;
            this.PointsLayerOff.TabStop = true;
            this.PointsLayerOff.UseVisualStyleBackColor = true;
            this.PointsLayerOff.CheckedChanged += new System.EventHandler(this.PointsLayerOff_CheckedChanged);
            // 
            // PointsLayerOn
            // 
            this.PointsLayerOn.AutoSize = true;
            this.PointsLayerOn.Checked = true;
            this.PointsLayerOn.Location = new System.Drawing.Point(133, 12);
            this.PointsLayerOn.Name = "PointsLayerOn";
            this.PointsLayerOn.Size = new System.Drawing.Size(17, 16);
            this.PointsLayerOn.TabIndex = 0;
            this.PointsLayerOn.TabStop = true;
            this.PointsLayerOn.UseVisualStyleBackColor = true;
            this.PointsLayerOn.CheckedChanged += new System.EventHandler(this.PointsLayerOn_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.PointsLayerOff);
            this.groupBox2.Controls.Add(this.PointsLayerOn);
            this.groupBox2.Location = new System.Drawing.Point(12, 626);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(242, 34);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Опорные точки";
            // 
            // GridLayerOff
            // 
            this.GridLayerOff.AutoSize = true;
            this.GridLayerOff.Location = new System.Drawing.Point(201, 12);
            this.GridLayerOff.Name = "GridLayerOff";
            this.GridLayerOff.Size = new System.Drawing.Size(17, 16);
            this.GridLayerOff.TabIndex = 1;
            this.GridLayerOff.TabStop = true;
            this.GridLayerOff.UseVisualStyleBackColor = true;
            this.GridLayerOff.CheckedChanged += new System.EventHandler(this.GridLayerOff_CheckedChanged);
            // 
            // GridLayerOn
            // 
            this.GridLayerOn.AutoSize = true;
            this.GridLayerOn.Checked = true;
            this.GridLayerOn.Location = new System.Drawing.Point(133, 12);
            this.GridLayerOn.Name = "GridLayerOn";
            this.GridLayerOn.Size = new System.Drawing.Size(17, 16);
            this.GridLayerOn.TabIndex = 0;
            this.GridLayerOn.TabStop = true;
            this.GridLayerOn.UseVisualStyleBackColor = true;
            this.GridLayerOn.CheckedChanged += new System.EventHandler(this.GridLayerOn_CheckedChanged);
            // 
            // GridControls
            // 
            this.GridControls.Controls.Add(this.GridLayerOff);
            this.GridControls.Controls.Add(this.GridLayerOn);
            this.GridControls.Enabled = false;
            this.GridControls.Location = new System.Drawing.Point(12, 666);
            this.GridControls.Name = "GridControls";
            this.GridControls.Size = new System.Drawing.Size(242, 34);
            this.GridControls.TabIndex = 16;
            this.GridControls.TabStop = false;
            this.GridControls.Text = "Сетка";
            // 
            // RunTriangulation
            // 
            this.RunTriangulation.Location = new System.Drawing.Point(388, 566);
            this.RunTriangulation.Name = "RunTriangulation";
            this.RunTriangulation.Size = new System.Drawing.Size(136, 30);
            this.RunTriangulation.TabIndex = 17;
            this.RunTriangulation.Text = "Триангуляция";
            this.RunTriangulation.UseVisualStyleBackColor = true;
            this.RunTriangulation.Click += new System.EventHandler(this.RunTriangulation_Click);
            // 
            // ResetTriangulation
            // 
            this.ResetTriangulation.Location = new System.Drawing.Point(388, 602);
            this.ResetTriangulation.Name = "ResetTriangulation";
            this.ResetTriangulation.Size = new System.Drawing.Size(136, 30);
            this.ResetTriangulation.TabIndex = 18;
            this.ResetTriangulation.Text = "Сбросить";
            this.ResetTriangulation.UseVisualStyleBackColor = true;
            this.ResetTriangulation.Click += new System.EventHandler(this.ResetTriangulation_Click);
            // 
            // MakePivotPointsButton
            // 
            this.MakePivotPointsButton.Location = new System.Drawing.Point(388, 530);
            this.MakePivotPointsButton.Name = "MakePivotPointsButton";
            this.MakePivotPointsButton.Size = new System.Drawing.Size(136, 30);
            this.MakePivotPointsButton.TabIndex = 19;
            this.MakePivotPointsButton.Text = "Опорные точки";
            this.MakePivotPointsButton.UseVisualStyleBackColor = true;
            this.MakePivotPointsButton.Click += new System.EventHandler(this.MakePivotPointsButton_Click);
            // 
            // PPMakerThreshold
            // 
            this.PPMakerThreshold.Location = new System.Drawing.Point(282, 534);
            this.PPMakerThreshold.Name = "PPMakerThreshold";
            this.PPMakerThreshold.Size = new System.Drawing.Size(100, 22);
            this.PPMakerThreshold.TabIndex = 20;
            // 
            // RebuiltImagePictureBox
            // 
            this.RebuiltImagePictureBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.RebuiltImagePictureBox.Location = new System.Drawing.Point(537, 12);
            this.RebuiltImagePictureBox.MaximumSize = new System.Drawing.Size(512, 512);
            this.RebuiltImagePictureBox.MinimumSize = new System.Drawing.Size(512, 512);
            this.RebuiltImagePictureBox.Name = "RebuiltImagePictureBox";
            this.RebuiltImagePictureBox.Size = new System.Drawing.Size(512, 512);
            this.RebuiltImagePictureBox.TabIndex = 21;
            this.RebuiltImagePictureBox.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1061, 710);
            this.Controls.Add(this.RebuiltImagePictureBox);
            this.Controls.Add(this.PPMakerThreshold);
            this.Controls.Add(this.MakePivotPointsButton);
            this.Controls.Add(this.ResetTriangulation);
            this.Controls.Add(this.RunTriangulation);
            this.Controls.Add(this.GridControls);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.OpenImageButton);
            this.Controls.Add(this.OriginalImagePictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OriginalImagePictureBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.GridControls.ResumeLayout(false);
            this.GridControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RebuiltImagePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox OriginalImagePictureBox;
        private System.Windows.Forms.Button OpenImageButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton PictureLayerOff;
        private System.Windows.Forms.RadioButton PictureLayerOn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton PointsLayerOff;
        private System.Windows.Forms.RadioButton PointsLayerOn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton GridLayerOff;
        private System.Windows.Forms.RadioButton GridLayerOn;
        private System.Windows.Forms.GroupBox GridControls;
        private System.Windows.Forms.Button RunTriangulation;
        private System.Windows.Forms.Button ResetTriangulation;
        private System.Windows.Forms.Button MakePivotPointsButton;
        private System.Windows.Forms.TextBox PPMakerThreshold;
        private System.Windows.Forms.PictureBox RebuiltImagePictureBox;
    }
}

