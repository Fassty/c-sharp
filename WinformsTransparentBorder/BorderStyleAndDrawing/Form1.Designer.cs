namespace BorderStyleAndDrawing
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.MinimizeButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Controls.Add(this.MinimizeButton);
            this.panel1.Controls.Add(this.CloseButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(454, 42);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // MinimizeButton
            // 
            this.MinimizeButton.BackColor = System.Drawing.Color.Teal;
            this.MinimizeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.MinimizeButton.FlatAppearance.BorderSize = 0;
            this.MinimizeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Turquoise;
            this.MinimizeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkCyan;
            this.MinimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinimizeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.MinimizeButton.Location = new System.Drawing.Point(370, 10);
            this.MinimizeButton.Name = "MinimizeButton";
            this.MinimizeButton.Size = new System.Drawing.Size(42, 32);
            this.MinimizeButton.TabIndex = 2;
            this.MinimizeButton.Text = "◻";
            this.MinimizeButton.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.MinimizeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.MinimizeButton.UseVisualStyleBackColor = false;
            this.MinimizeButton.Click += new System.EventHandler(this.MinimizeButton_Click_1);
            this.MinimizeButton.MouseLeave += new System.EventHandler(this.MinimizeButton_MouseLeave);
            this.MinimizeButton.MouseHover += new System.EventHandler(this.MinimizeButton_MouseHover);
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.Maroon;
            this.CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.CloseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkRed;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CloseButton.Location = new System.Drawing.Point(412, 10);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(42, 32);
            this.CloseButton.TabIndex = 0;
            this.CloseButton.Text = "X";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            this.CloseButton.MouseLeave += new System.EventHandler(this.CloseButton_MouseLeave);
            this.CloseButton.MouseHover += new System.EventHandler(this.CloseButton_MouseHover);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "My awesome app";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 697);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button MinimizeButton;
    }
}

