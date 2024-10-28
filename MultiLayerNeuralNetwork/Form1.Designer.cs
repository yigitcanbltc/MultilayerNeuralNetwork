namespace MultiLayerNeuralNetwork
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            comboBox1 = new ComboBox();
            label5 = new Label();
            txtInputNeurons = new TextBox();
            txtOutputNeurons = new TextBox();
            txtHiddenLayers = new TextBox();
            txtLearningRate = new TextBox();
            button1 = new Button();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            dataGridView1 = new DataGridView();
            button2 = new Button();
            label6 = new Label();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 31);
            label1.Name = "label1";
            label1.Size = new Size(285, 20);
            label1.TabIndex = 0;
            label1.Text = "Giriş Katmanındaki Nöron Sayısını Giriniz :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 73);
            label2.Name = "label2";
            label2.Size = new Size(286, 20);
            label2.TabIndex = 1;
            label2.Text = "Çıkış Katmanındaki Nöron Sayısını Giriniz :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(455, 31);
            label3.Name = "label3";
            label3.Size = new Size(199, 20);
            label3.TabIndex = 2;
            label3.Text = "Gizli Katman Sayısını Giriniz :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(455, 70);
            label4.Name = "label4";
            label4.Size = new Size(197, 20);
            label4.TabIndex = 3;
            label4.Text = "Öğrenme Katsayısını Giriniz :";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Sigmoid Function" });
            comboBox1.Location = new Point(231, 117);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(184, 28);
            comboBox1.TabIndex = 4;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(11, 120);
            label5.Name = "label5";
            label5.Size = new Size(214, 20);
            label5.TabIndex = 5;
            label5.Text = "Aktivasyon Fonksiyonu Seçiniz :";
            // 
            // txtInputNeurons
            // 
            txtInputNeurons.Location = new Point(303, 28);
            txtInputNeurons.Name = "txtInputNeurons";
            txtInputNeurons.Size = new Size(112, 27);
            txtInputNeurons.TabIndex = 6;
            // 
            // txtOutputNeurons
            // 
            txtOutputNeurons.Location = new Point(303, 70);
            txtOutputNeurons.Name = "txtOutputNeurons";
            txtOutputNeurons.Size = new Size(112, 27);
            txtOutputNeurons.TabIndex = 7;
            // 
            // txtHiddenLayers
            // 
            txtHiddenLayers.Location = new Point(660, 28);
            txtHiddenLayers.Name = "txtHiddenLayers";
            txtHiddenLayers.Size = new Size(112, 27);
            txtHiddenLayers.TabIndex = 8;
            // 
            // txtLearningRate
            // 
            txtLearningRate.Location = new Point(658, 67);
            txtLearningRate.Name = "txtLearningRate";
            txtLearningRate.Size = new Size(114, 27);
            txtLearningRate.TabIndex = 9;
            // 
            // button1
            // 
            button1.Location = new Point(469, 116);
            button1.Name = "button1";
            button1.Size = new Size(280, 29);
            button1.TabIndex = 10;
            button1.Text = "Ağı Oluştur";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.ButtonFace;
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(776, 269);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(12, 185);
            panel1.Name = "panel1";
            panel1.Size = new Size(776, 269);
            panel1.TabIndex = 12;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 476);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(760, 188);
            dataGridView1.TabIndex = 13;
            // 
            // button2
            // 
            button2.Location = new Point(205, 684);
            button2.Name = "button2";
            button2.Size = new Size(363, 29);
            button2.TabIndex = 14;
            button2.Text = "Ağı Eğit";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(252, 790);
            label6.Name = "label6";
            label6.Size = new Size(112, 20);
            label6.TabIndex = 15;
            label6.Text = "Eğitim Sayısı : 0";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(391, 790);
            label7.Name = "label7";
            label7.Size = new Size(114, 20);
            label7.TabIndex = 16;
            label7.Text = "Toplam Hata : 0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 907);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(button2);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Controls.Add(button1);
            Controls.Add(txtLearningRate);
            Controls.Add(txtHiddenLayers);
            Controls.Add(txtOutputNeurons);
            Controls.Add(txtInputNeurons);
            Controls.Add(label5);
            Controls.Add(comboBox1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private ComboBox comboBox1;
        private Label label5;
        private TextBox txtInputNeurons;
        private TextBox txtOutputNeurons;
        private TextBox txtHiddenLayers;
        private TextBox txtLearningRate;
        private Button button1;
        private PictureBox pictureBox1;
        private Panel panel1;
        private DataGridView dataGridView1;
        private Button button2;
        private Label label6;
        private Label label7;
    }
}
