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
            this.Bigram = new System.Windows.Forms.Button();
            this.SpellCheckModule = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InvertedIndexButton
            // 
            this.InvertedIndexButton.Location = new System.Drawing.Point(30, 11);
            this.InvertedIndexButton.Margin = new System.Windows.Forms.Padding(2);
            this.InvertedIndexButton.Name = "InvertedIndexButton";
            this.InvertedIndexButton.Size = new System.Drawing.Size(119, 46);
            this.InvertedIndexButton.TabIndex = 0;
            this.InvertedIndexButton.Text = "InvertedIndex";
            this.InvertedIndexButton.UseVisualStyleBackColor = true;
            this.InvertedIndexButton.Click += new System.EventHandler(this.InvertedIndexButton_Click);
            // 
            // SoundexButton
            // 
            this.SoundexButton.Location = new System.Drawing.Point(30, 137);
            this.SoundexButton.Margin = new System.Windows.Forms.Padding(2);
            this.SoundexButton.Name = "SoundexButton";
            this.SoundexButton.Size = new System.Drawing.Size(119, 45);
            this.SoundexButton.TabIndex = 1;
            this.SoundexButton.Text = "Soundex";
            this.SoundexButton.UseVisualStyleBackColor = true;
            this.SoundexButton.Click += new System.EventHandler(this.SoundexButton_Click);
            // 
            // Bigram
            // 
            this.Bigram.Location = new System.Drawing.Point(30, 198);
            this.Bigram.Margin = new System.Windows.Forms.Padding(2);
            this.Bigram.Name = "Bigram";
            this.Bigram.Size = new System.Drawing.Size(119, 43);
            this.Bigram.TabIndex = 2;
            this.Bigram.Text = "Bigram";
            this.Bigram.UseVisualStyleBackColor = true;
            this.Bigram.Click += new System.EventHandler(this.Bigram_Click);
            // 
            // SpellCheckModule
            // 
            this.SpellCheckModule.Location = new System.Drawing.Point(30, 75);
            this.SpellCheckModule.Name = "SpellCheckModule";
            this.SpellCheckModule.Size = new System.Drawing.Size(119, 44);
            this.SpellCheckModule.TabIndex = 3;
            this.SpellCheckModule.Text = "Spell Check";
            this.SpellCheckModule.UseVisualStyleBackColor = true;
            this.SpellCheckModule.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(181, 259);
            this.Controls.Add(this.SpellCheckModule);
            this.Controls.Add(this.Bigram);
            this.Controls.Add(this.SoundexButton);
            this.Controls.Add(this.InvertedIndexButton);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button InvertedIndexButton;
        private System.Windows.Forms.Button SoundexButton;
        private System.Windows.Forms.Button Bigram;
        private System.Windows.Forms.Button SpellCheckModule;
    }
}