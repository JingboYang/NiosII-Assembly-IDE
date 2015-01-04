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
            this.dgvReg = new System.Windows.Forms.DataGridView();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.pnlLEDR_SW = new System.Windows.Forms.Panel();
            this.pnlLEDG_KEY = new System.Windows.Forms.Panel();
            this.lblLCD = new System.Windows.Forms.Label();
            this.pnlHEX = new System.Windows.Forms.Panel();
            this.dgvCode = new System.Windows.Forms.DataGridView();
            this.dgvCompiler = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompiler)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvReg
            // 
            this.dgvReg.AllowUserToAddRows = false;
            this.dgvReg.AllowUserToDeleteRows = false;
            this.dgvReg.AllowUserToResizeRows = false;
            this.dgvReg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvReg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvReg.Location = new System.Drawing.Point(484, 50);
            this.dgvReg.Name = "dgvReg";
            this.dgvReg.RowHeadersVisible = false;
            this.dgvReg.RowTemplate.Height = 24;
            this.dgvReg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvReg.Size = new System.Drawing.Size(148, 894);
            this.dgvReg.TabIndex = 1;
            this.dgvReg.VirtualMode = true;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(199, 12);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(104, 32);
            this.btnRun.TabIndex = 2;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(329, 12);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(110, 32);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "Next Step";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(467, 12);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 32);
            this.btnPause.TabIndex = 4;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(584, 12);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(84, 32);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // pnlLEDR_SW
            // 
            this.pnlLEDR_SW.Location = new System.Drawing.Point(638, 50);
            this.pnlLEDR_SW.Name = "pnlLEDR_SW";
            this.pnlLEDR_SW.Size = new System.Drawing.Size(655, 159);
            this.pnlLEDR_SW.TabIndex = 6;
            // 
            // pnlLEDG_KEY
            // 
            this.pnlLEDG_KEY.Location = new System.Drawing.Point(638, 246);
            this.pnlLEDG_KEY.Name = "pnlLEDG_KEY";
            this.pnlLEDG_KEY.Size = new System.Drawing.Size(250, 100);
            this.pnlLEDG_KEY.TabIndex = 8;
            // 
            // lblLCD
            // 
            this.lblLCD.Location = new System.Drawing.Point(718, 398);
            this.lblLCD.Name = "lblLCD";
            this.lblLCD.Size = new System.Drawing.Size(430, 100);
            this.lblLCD.TabIndex = 9;
            this.lblLCD.Text = "16 x 2 LCD Display";
            // 
            // pnlHEX
            // 
            this.pnlHEX.Location = new System.Drawing.Point(894, 246);
            this.pnlHEX.Name = "pnlHEX";
            this.pnlHEX.Size = new System.Drawing.Size(399, 100);
            this.pnlHEX.TabIndex = 10;
            // 
            // dgvCode
            // 
            this.dgvCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCode.Location = new System.Drawing.Point(12, 50);
            this.dgvCode.Name = "dgvCode";
            this.dgvCode.RowTemplate.Height = 24;
            this.dgvCode.Size = new System.Drawing.Size(466, 894);
            this.dgvCode.TabIndex = 0;
            this.dgvCode.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCode_CellContentClick);
            // 
            // dgvCompiler
            // 
            this.dgvCompiler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompiler.Location = new System.Drawing.Point(638, 433);
            this.dgvCompiler.Name = "dgvCompiler";
            this.dgvCompiler.RowTemplate.Height = 24;
            this.dgvCompiler.Size = new System.Drawing.Size(655, 311);
            this.dgvCompiler.TabIndex = 11;
            // 
            // Emulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1305, 953);
            this.Controls.Add(this.dgvCompiler);
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
            this.Name = "Emulator";
            this.Text = "Emulator";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Emulator_Closed);
            this.Load += new System.EventHandler(this.Emulator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompiler)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.DataGridView dgvReg;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Panel pnlLEDR_SW;
        private System.Windows.Forms.Panel pnlLEDG_KEY;
        private System.Windows.Forms.Label lblLCD;
        private System.Windows.Forms.Panel pnlHEX;
        private System.Windows.Forms.DataGridView dgvCode;
        private System.Windows.Forms.DataGridView dgvCompiler;
    }
}