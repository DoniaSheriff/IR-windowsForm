namespace IR_milestone
{
    partial class Form2
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
            this.InvertedIndexButton = new System.Windows.Forms.Button();
            this.SoundexButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InvertedIndexButton
            // 
            this.InvertedIndexButton.Enabled = false;
            this.InvertedIndexButton.Location = new System.Drawing.Point(12, 12);
            this.InvertedIndexButton.Name = "InvertedIndexButton";
            this.InvertedIndexButton.Size = new System.Drawing.Size(159, 49);
            this.InvertedIndexButton.TabIndex = 0;
            this.InvertedIndexButton.Text = "InvertedIndex";
            this.InvertedIndexButton.UseVisualStyleBackColor = true;
            this.InvertedIndexButton.Click += new System.EventHandler(this.InvertedIndexButton_Click);
            // 
            // SoundexButton
            // 
            this.SoundexButton.Location = new System.Drawing.Point(12, 99);
            this.SoundexButton.Name = "SoundexButton";
            this.SoundexButton.Size = new System.Drawing.Size(148, 55);
            this.SoundexButton.TabIndex = 1;
            this.SoundexButton.Text = "Soundex";
            this.SoundexButton.UseVisualStyleBackColor = true;
            this.SoundexButton.Click += new System.EventHandler(this.SoundexButton_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SoundexButton);
            this.Controls.Add(this.InvertedIndexButton);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button InvertedIndexButton;
        private System.Windows.Forms.Button SoundexButton;
    }
}