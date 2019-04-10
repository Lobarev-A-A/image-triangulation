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
            this.originalImagePictureBox = new System.Windows.Forms.PictureBox();
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
            this.MakePivotPointsButton = new System.Windows.Forms.Button();
            this.PPMakerThreshold = new System.Windows.Forms.TextBox();
            this.rebuiltImagePictureBox = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.originalImagePictureBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.GridControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rebuiltImagePictureBox)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // originalImagePictureBox
            // 
            this.originalImagePictureBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.originalImagePictureBox.Location = new System.Drawing.Point(11, 37);
            this.originalImagePictureBox.MaximumSize = new System.Drawing.Size(512, 512);
            this.originalImagePictureBox.MinimumSize = new System.Drawing.Size(512, 512);
            this.originalImagePictureBox.Name = "originalImagePictureBox";
            this.originalImagePictureBox.Size = new System.Drawing.Size(512, 512);
            this.originalImagePictureBox.TabIndex = 0;
            this.originalImagePictureBox.TabStop = false;
            // 
            // OpenImageButton
            // 
            this.OpenImageButton.Location = new System.Drawing.Point(12, 1);
            this.OpenImageButton.Name = "OpenImageButton";
            this.OpenImageButton.Size = new System.Drawing.Size(150, 30);
            this.OpenImageButton.TabIndex = 12;
            this.OpenImageButton.Text = "Открыть .png";
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
            this.GridControls.Location = new System.Drawing.Point(12, 666);
            this.GridControls.Name = "GridControls";
            this.GridControls.Size = new System.Drawing.Size(242, 34);
            this.GridControls.TabIndex = 16;
            this.GridControls.TabStop = false;
            this.GridControls.Text = "Сетка";
            // 
            // RunTriangulation
            // 
            this.RunTriangulation.Location = new System.Drawing.Point(155, 21);
            this.RunTriangulation.Name = "RunTriangulation";
            this.RunTriangulation.Size = new System.Drawing.Size(95, 24);
            this.RunTriangulation.TabIndex = 17;
            this.RunTriangulation.Text = "Выполнить";
            this.RunTriangulation.UseVisualStyleBackColor = true;
            this.RunTriangulation.Click += new System.EventHandler(this.RunTriangulation_Click);
            // 
            // MakePivotPointsButton
            // 
            this.MakePivotPointsButton.Location = new System.Drawing.Point(155, 21);
            this.MakePivotPointsButton.Name = "MakePivotPointsButton";
            this.MakePivotPointsButton.Size = new System.Drawing.Size(95, 24);
            this.MakePivotPointsButton.TabIndex = 19;
            this.MakePivotPointsButton.Text = "Выполнить";
            this.MakePivotPointsButton.UseVisualStyleBackColor = true;
            this.MakePivotPointsButton.Click += new System.EventHandler(this.MakePivotPointsButton_Click);
            // 
            // PPMakerThreshold
            // 
            this.PPMakerThreshold.Location = new System.Drawing.Point(6, 51);
            this.PPMakerThreshold.Name = "PPMakerThreshold";
            this.PPMakerThreshold.Size = new System.Drawing.Size(100, 22);
            this.PPMakerThreshold.TabIndex = 20;
            // 
            // rebuiltImagePictureBox
            // 
            this.rebuiltImagePictureBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rebuiltImagePictureBox.Location = new System.Drawing.Point(535, 37);
            this.rebuiltImagePictureBox.MaximumSize = new System.Drawing.Size(512, 512);
            this.rebuiltImagePictureBox.MinimumSize = new System.Drawing.Size(512, 512);
            this.rebuiltImagePictureBox.Name = "rebuiltImagePictureBox";
            this.rebuiltImagePictureBox.Size = new System.Drawing.Size(512, 512);
            this.rebuiltImagePictureBox.TabIndex = 21;
            this.rebuiltImagePictureBox.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 21);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(143, 24);
            this.comboBox1.TabIndex = 22;
            this.comboBox1.Text = "Алгоритм";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(6, 21);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(143, 24);
            this.comboBox2.TabIndex = 23;
            this.comboBox2.Text = "Алгоритм";
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(6, 21);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(143, 24);
            this.comboBox3.TabIndex = 24;
            this.comboBox3.Text = "Алгоритм";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(155, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 24);
            this.button1.TabIndex = 25;
            this.button1.Text = "Выполнить";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.comboBox1);
            this.groupBox3.Controls.Add(this.MakePivotPointsButton);
            this.groupBox3.Controls.Add(this.PPMakerThreshold);
            this.groupBox3.Location = new System.Drawing.Point(267, 555);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(256, 145);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Опорные точки";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.RunTriangulation);
            this.groupBox4.Controls.Add(this.comboBox2);
            this.groupBox4.Location = new System.Drawing.Point(529, 555);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(256, 145);
            this.groupBox4.TabIndex = 27;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Триангуляция";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.comboBox3);
            this.groupBox5.Controls.Add(this.button1);
            this.groupBox5.Location = new System.Drawing.Point(791, 555);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(256, 145);
            this.groupBox5.TabIndex = 28;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Закраска";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(163, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(136, 30);
            this.button2.TabIndex = 29;
            this.button2.Text = "Открыть .t";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(304, 1);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(132, 30);
            this.button3.TabIndex = 30;
            this.button3.Text = "Сохранить в .png";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(442, 1);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(147, 30);
            this.button4.TabIndex = 31;
            this.button4.Text = "Сохранить в .t";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 17);
            this.label3.TabIndex = 23;
            this.label3.Text = "Время выполнения, сек:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(169, 17);
            this.label4.TabIndex = 24;
            this.label4.Text = "Время выполнения, сек:";
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(169, 17);
            this.label6.TabIndex = 27;
            this.label6.Text = "Время выполнения, сек:";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1061, 710);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.OpenImageButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.rebuiltImagePictureBox);
            this.Controls.Add(this.GridControls);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.originalImagePictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(1079, 757);
            this.MinimumSize = new System.Drawing.Size(1079, 757);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.originalImagePictureBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.GridControls.ResumeLayout(false);
            this.GridControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rebuiltImagePictureBox)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox originalImagePictureBox;
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
        private System.Windows.Forms.Button MakePivotPointsButton;
        private System.Windows.Forms.TextBox PPMakerThreshold;
        private System.Windows.Forms.PictureBox rebuiltImagePictureBox;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}

