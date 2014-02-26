namespace ChangeNamePic
{
    partial class FromChangePass
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnchangepath = new System.Windows.Forms.Button();
            this.txt_path = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bt_change = new System.Windows.Forms.Button();
            this.txt_text = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnchangepath
            // 
            this.btnchangepath.Location = new System.Drawing.Point(161, 9);
            this.btnchangepath.Name = "btnchangepath";
            this.btnchangepath.Size = new System.Drawing.Size(39, 23);
            this.btnchangepath.TabIndex = 0;
            this.btnchangepath.Text = "...";
            this.btnchangepath.UseVisualStyleBackColor = true;
            this.btnchangepath.Click += new System.EventHandler(this.btnchangepath_Click);
            // 
            // txt_path
            // 
            this.txt_path.Location = new System.Drawing.Point(12, 12);
            this.txt_path.Name = "txt_path";
            this.txt_path.ReadOnly = true;
            this.txt_path.Size = new System.Drawing.Size(143, 20);
            this.txt_path.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(12, 38);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(188, 97);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // bt_change
            // 
            this.bt_change.Location = new System.Drawing.Point(12, 167);
            this.bt_change.Name = "bt_change";
            this.bt_change.Size = new System.Drawing.Size(188, 23);
            this.bt_change.TabIndex = 3;
            this.bt_change.Text = "OK";
            this.bt_change.UseVisualStyleBackColor = true;
            this.bt_change.Click += new System.EventHandler(this.bt_change_Click);
            // 
            // txt_text
            // 
            this.txt_text.Location = new System.Drawing.Point(12, 141);
            this.txt_text.Name = "txt_text";
            this.txt_text.Size = new System.Drawing.Size(188, 20);
            this.txt_text.TabIndex = 4;
            // 
            // FromChangePass
            // 
            this.AcceptButton = this.bt_change;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(212, 199);
            this.Controls.Add(this.txt_text);
            this.Controls.Add(this.bt_change);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txt_path);
            this.Controls.Add(this.btnchangepath);
            this.Name = "FromChangePass";
            this.Text = "Change Pass";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnchangepath;
        private System.Windows.Forms.TextBox txt_path;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button bt_change;
        private System.Windows.Forms.TextBox txt_text;
    }
}

