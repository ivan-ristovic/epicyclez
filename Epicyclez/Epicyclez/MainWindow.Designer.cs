namespace Epicyclez
{
    partial class MainWindow
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
            if (disposing && (components != null)) {
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
            this.components = new System.ComponentModel.Container();
            this.tmrTick = new System.Windows.Forms.Timer(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnDraw = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnMask = new System.Windows.Forms.Button();
            this.ttHelp = new System.Windows.Forms.ToolTip(this.components);
            this.lblEpicycles = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrTick
            // 
            this.tmrTick.Enabled = true;
            this.tmrTick.Interval = 15;
            this.tmrTick.Tick += new System.EventHandler(this.TmrTick_Tick);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Red;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnExit.Location = new System.Drawing.Point(1061, 1);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(39, 36);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "❌";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(508, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(75, 20);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Epicycles";
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Move_MouseDown);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlTop.Controls.Add(this.btnExit);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Location = new System.Drawing.Point(0, -1);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1100, 36);
            this.pnlTop.TabIndex = 2;
            this.pnlTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Move_MouseDown);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(43, 41);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(25, 25);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "▶️";
            this.btnStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttHelp.SetToolTip(this.btnStart, "Start epicycles");
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(12, 41);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(25, 25);
            this.btnDraw.TabIndex = 4;
            this.btnDraw.Text = "🖋️";
            this.btnDraw.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttHelp.SetToolTip(this.btnDraw, "Draw custom shape");
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.BtnDraw_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(74, 41);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(25, 25);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "⬛";
            this.btnStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttHelp.SetToolTip(this.btnStop, "Pause epicycles");
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestart.Location = new System.Drawing.Point(105, 41);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(25, 25);
            this.btnRestart.TabIndex = 6;
            this.btnRestart.Text = "◀◀";
            this.ttHelp.SetToolTip(this.btnRestart, "Restart epicycles");
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.BtnRestart_Click);
            // 
            // btnMask
            // 
            this.btnMask.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMask.Location = new System.Drawing.Point(136, 41);
            this.btnMask.Name = "btnMask";
            this.btnMask.Size = new System.Drawing.Size(25, 25);
            this.btnMask.TabIndex = 7;
            this.btnMask.Text = "M";
            this.ttHelp.SetToolTip(this.btnMask, "Toggle mask mode - Show/Hide the data that is being approximated");
            this.btnMask.UseVisualStyleBackColor = true;
            this.btnMask.Click += new System.EventHandler(this.BtnMask_Click);
            // 
            // lblEpicycles
            // 
            this.lblEpicycles.BackColor = System.Drawing.Color.Transparent;
            this.lblEpicycles.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEpicycles.ForeColor = System.Drawing.Color.White;
            this.lblEpicycles.Location = new System.Drawing.Point(9, 87);
            this.lblEpicycles.Name = "lblEpicycles";
            this.lblEpicycles.Size = new System.Drawing.Size(152, 23);
            this.lblEpicycles.TabIndex = 8;
            this.lblEpicycles.Text = "Epicycles : 0";
            this.lblEpicycles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTime
            // 
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(9, 69);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(152, 23);
            this.lblTime.TabIndex = 9;
            this.lblTime.Text = "Time      : 0";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1098, 768);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblEpicycles);
            this.Controls.Add(this.btnMask);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.pnlTop);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainWindow";
            this.Text = "Epicyclez";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainWindow_Paint);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrTick;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Button btnMask;
        private System.Windows.Forms.ToolTip ttHelp;
        private System.Windows.Forms.Label lblEpicycles;
        private System.Windows.Forms.Label lblTime;
    }
}

