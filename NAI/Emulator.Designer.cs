namespace NAI
{
    partial class Emulator
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
            this.dgvCode = new System.Windows.Forms.DataGridView();
            this.dgvReg = new System.Windows.Forms.DataGridView();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.pnlLEDR_SW = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlLEDG_KEY = new System.Windows.Forms.Panel();
            this.lblLCD = new System.Windows.Forms.Label();
            this.pnlHEX = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReg)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvCode
            // 
            this.dgvCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCode.Location = new System.Drawing.Point(12, 75);
            this.dgvCode.Name = "dgvCode";
            this.dgvCode.RowTemplate.Height = 24;
            this.dgvCode.Size = new System.Drawing.Size(391, 666);
            this.dgvCode.TabIndex = 0;
            // 
            // dgvReg
            // 
            this.dgvReg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReg.Location = new System.Drawing.Point(420, 75);
            this.dgvReg.Name = "dgvReg";
            this.dgvReg.RowTemplate.Height = 24;
            this.dgvReg.Size = new System.Drawing.Size(95, 666);
            this.dgvReg.TabIndex = 1;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(12, 37);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(104, 32);
            this.btnRun.TabIndex = 2;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(122, 37);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(110, 32);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "Next Step";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(238, 37);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 32);
            this.btnPause.TabIndex = 4;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(319, 37);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(84, 32);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // pnlLEDR_SW
            // 
            this.pnlLEDR_SW.Location = new System.Drawing.Point(538, 75);
            this.pnlLEDR_SW.Name = "pnlLEDR_SW";
            this.pnlLEDR_SW.Size = new System.Drawing.Size(684, 159);
            this.pnlLEDR_SW.TabIndex = 6;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1262, 28);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // pnlLEDG_KEY
            // 
            this.pnlLEDG_KEY.Location = new System.Drawing.Point(538, 271);
            this.pnlLEDG_KEY.Name = "pnlLEDG_KEY";
            this.pnlLEDG_KEY.Size = new System.Drawing.Size(226, 100);
            this.pnlLEDG_KEY.TabIndex = 8;
            // 
            // lblLCD
            // 
            this.lblLCD.Location = new System.Drawing.Point(647, 423);
            this.lblLCD.Name = "lblLCD";
            this.lblLCD.Size = new System.Drawing.Size(430, 100);
            this.lblLCD.TabIndex = 9;
            this.lblLCD.Text = "16 x 2 LCD Display";
            // 
            // pnlHEX
            // 
            this.pnlHEX.Location = new System.Drawing.Point(795, 271);
            this.pnlHEX.Name = "pnlHEX";
            this.pnlHEX.Size = new System.Drawing.Size(427, 100);
            this.pnlHEX.TabIndex = 10;
            // 
            // Emulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 753);
            this.Controls.Add(this.pnlHEX);
            this.Controls.Add(this.lblLCD);
            this.Controls.Add(this.pnlLEDG_KEY);
            this.Controls.Add(this.pnlLEDR_SW);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.dgvReg);
            this.Controls.Add(this.dgvCode);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Emulator";
            this.Text = "Emulator";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReg)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCode;
        private System.Windows.Forms.DataGridView dgvReg;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Panel pnlLEDR_SW;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Panel pnlLEDG_KEY;
        private System.Windows.Forms.Label lblLCD;
        private System.Windows.Forms.Panel pnlHEX;
    }
}