namespace DespairBox.UI
{
    partial class exePatcher
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
            this.ArchivePathPatchCheck = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ArchivePathPatchCheck
            // 
            this.ArchivePathPatchCheck.AutoSize = true;
            this.ArchivePathPatchCheck.Location = new System.Drawing.Point(12, 12);
            this.ArchivePathPatchCheck.Name = "ArchivePathPatchCheck";
            this.ArchivePathPatchCheck.Size = new System.Drawing.Size(171, 17);
            this.ArchivePathPatchCheck.TabIndex = 0;
            this.ArchivePathPatchCheck.Text = "Load files from /archive/ folder";
            this.ArchivePathPatchCheck.UseVisualStyleBackColor = true;
            this.ArchivePathPatchCheck.CheckedChanged += new System.EventHandler(this.ArchivePathPatchCheck_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(539, 346);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Patch";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // exePatcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 381);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ArchivePathPatchCheck);
            this.Name = "exePatcher";
            this.Text = "exePatcher";
            this.Load += new System.EventHandler(this.exePatcher_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ArchivePathPatchCheck;
        private System.Windows.Forms.Button button1;
    }
}