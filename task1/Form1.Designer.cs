namespace Individual_task_1
{
    partial class Form1
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
            this.rand_button = new System.Windows.Forms.Button();
            this.show_union_checkBox = new System.Windows.Forms.CheckBox();
            this.canvas_pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.canvas_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // rand_button
            // 
            this.rand_button.Location = new System.Drawing.Point(10, 11);
            this.rand_button.Margin = new System.Windows.Forms.Padding(2);
            this.rand_button.Name = "rand_button";
            this.rand_button.Size = new System.Drawing.Size(56, 19);
            this.rand_button.TabIndex = 0;
            this.rand_button.Text = "random";
            this.rand_button.UseVisualStyleBackColor = true;
            this.rand_button.Click += new System.EventHandler(this.rand_button_Click);
            // 
            // show_union_checkBox
            // 
            this.show_union_checkBox.AutoSize = true;
            this.show_union_checkBox.Location = new System.Drawing.Point(71, 11);
            this.show_union_checkBox.Margin = new System.Windows.Forms.Padding(2);
            this.show_union_checkBox.Name = "show_union_checkBox";
            this.show_union_checkBox.Size = new System.Drawing.Size(143, 17);
            this.show_union_checkBox.TabIndex = 1;
            this.show_union_checkBox.Text = "показать объединение";
            this.show_union_checkBox.UseVisualStyleBackColor = true;
            this.show_union_checkBox.CheckedChanged += new System.EventHandler(this.show_union_checkBox_CheckedChanged);
            // 
            // canvas_pictureBox
            // 
            this.canvas_pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.canvas_pictureBox.BackColor = System.Drawing.Color.White;
            this.canvas_pictureBox.Location = new System.Drawing.Point(10, 35);
            this.canvas_pictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.canvas_pictureBox.Name = "canvas_pictureBox";
            this.canvas_pictureBox.Size = new System.Drawing.Size(1265, 916);
            this.canvas_pictureBox.TabIndex = 2;
            this.canvas_pictureBox.TabStop = false;
            this.canvas_pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_pictureBox_Paint);
            this.canvas_pictureBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.canvas_pictureBox_MouseDoubleClick);
            this.canvas_pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_pictureBox_MouseDown);
            this.canvas_pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_pictureBox_MouseMove);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 961);
            this.Controls.Add(this.canvas_pictureBox);
            this.Controls.Add(this.show_union_checkBox);
            this.Controls.Add(this.rand_button);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Объединение выпуклых полигонов";
            ((System.ComponentModel.ISupportInitialize)(this.canvas_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button rand_button;
        private System.Windows.Forms.CheckBox show_union_checkBox;
        private System.Windows.Forms.PictureBox canvas_pictureBox;
    }
}

