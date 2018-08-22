namespace DespairBox.UI
{
    partial class BGMLoopViewer
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
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SaveLOOPButton = new System.Windows.Forms.Button();
            this.LoopPointValue = new System.Windows.Forms.NumericUpDown();
            this.LoopingPointLable = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LoopStartPositionValue = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.LoopPointValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoopStartPositionValue)).BeginInit();
            this.SuspendLayout();
            // 
            // SaveLOOPButton
            // 
            this.SaveLOOPButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.SaveLOOPButton.Location = new System.Drawing.Point(349, 22);
            this.SaveLOOPButton.Name = "SaveLOOPButton";
            this.SaveLOOPButton.Size = new System.Drawing.Size(75, 23);
            this.SaveLOOPButton.TabIndex = 0;
            this.SaveLOOPButton.Text = "Save";
            this.SaveLOOPButton.UseVisualStyleBackColor = true;
            this.SaveLOOPButton.Click += new System.EventHandler(this.SaveLOOPButton_Click);
            // 
            // LoopPointValue
            // 
            this.LoopPointValue.Location = new System.Drawing.Point(15, 24);
            this.LoopPointValue.Maximum = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            0});
            this.LoopPointValue.Name = "LoopPointValue";
            this.LoopPointValue.Size = new System.Drawing.Size(120, 20);
            this.LoopPointValue.TabIndex = 2;
            this.LoopPointValue.ValueChanged += new System.EventHandler(this.LoopPointValue_ValueChanged);
            // 
            // LoopingPointLable
            // 
            this.LoopingPointLable.AutoSize = true;
            this.LoopingPointLable.Location = new System.Drawing.Point(12, 8);
            this.LoopingPointLable.Name = "LoopingPointLable";
            this.LoopingPointLable.Size = new System.Drawing.Size(75, 13);
            this.LoopingPointLable.TabIndex = 3;
            this.LoopingPointLable.Text = "Looping Point:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(174, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Looping Start Point:";
            // 
            // LoopStartPositionValue
            // 
            this.LoopStartPositionValue.Location = new System.Drawing.Point(175, 23);
            this.LoopStartPositionValue.Maximum = new decimal(new int[] {
            -1486618625,
            232830643,
            0,
            0});
            this.LoopStartPositionValue.Name = "LoopStartPositionValue";
            this.LoopStartPositionValue.Size = new System.Drawing.Size(120, 20);
            this.LoopStartPositionValue.TabIndex = 4;
            this.LoopStartPositionValue.ValueChanged += new System.EventHandler(this.LoopStartPositionValue_ValueChanged);
            // 
            // BGMLoopViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 56);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LoopStartPositionValue);
            this.Controls.Add(this.LoopingPointLable);
            this.Controls.Add(this.LoopPointValue);
            this.Controls.Add(this.SaveLOOPButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "BGMLoopViewer";
            this.Text = ".loop Editor";
            this.Load += new System.EventHandler(this.BGMLoopViewer_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.LoopPointValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoopStartPositionValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveLOOPButton;
        private System.Windows.Forms.NumericUpDown LoopPointValue;
        private System.Windows.Forms.Label LoopingPointLable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown LoopStartPositionValue;
    }
}