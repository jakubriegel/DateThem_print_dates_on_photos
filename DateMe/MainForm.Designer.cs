namespace DateThem
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ChooseFolderButton = new System.Windows.Forms.Button();
            this.PrintDatesButton = new System.Windows.Forms.Button();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.ChosenFolderLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ChooseFolderButton
            // 
            resources.ApplyResources(this.ChooseFolderButton, "ChooseFolderButton");
            this.ChooseFolderButton.Name = "ChooseFolderButton";
            this.ChooseFolderButton.UseVisualStyleBackColor = true;
            this.ChooseFolderButton.Click += new System.EventHandler(this.ChooseFolderButton_Click);
            // 
            // PrintDatesButton
            // 
            resources.ApplyResources(this.PrintDatesButton, "PrintDatesButton");
            this.PrintDatesButton.Name = "PrintDatesButton";
            this.PrintDatesButton.UseVisualStyleBackColor = true;
            this.PrintDatesButton.Click += new System.EventHandler(this.PrintDatesButton_Click);
            // 
            // ProgressBar
            // 
            resources.ApplyResources(this.ProgressBar, "ProgressBar");
            this.ProgressBar.Name = "ProgressBar";
            // 
            // ChosenFolderLabel
            // 
            resources.ApplyResources(this.ChosenFolderLabel, "ChosenFolderLabel");
            this.ChosenFolderLabel.Name = "ChosenFolderLabel";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ChosenFolderLabel);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.PrintDatesButton);
            this.Controls.Add(this.ChooseFolderButton);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ChooseFolderButton;
        private System.Windows.Forms.Button PrintDatesButton;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Label ChosenFolderLabel;
    }
}

