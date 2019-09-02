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
            this.tbStep = new System.Windows.Forms.TrackBar();
            this.lblStep = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbStep)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrTick
            // 
            this.tmrTick.Enabled = true;
            this.tmrTick.Interval = 15;
            this.tmrTick.Tick += new System.EventHandler(this.TmrTick_Tick);
            // 
            // tbStep
            // 
            this.tbStep.Location = new System.Drawing.Point(68, 13);
            this.tbStep.Maximum = 100;
            this.tbStep.Minimum = 1;
            this.tbStep.Name = "tbStep";
            this.tbStep.Size = new System.Drawing.Size(704, 45);
            this.tbStep.TabIndex = 0;
            this.tbStep.Value = 100;
            this.tbStep.ValueChanged += new System.EventHandler(this.TbStep_ValueChanged);
            // 
            // lblStep
            // 
            this.lblStep.AutoSize = true;
            this.lblStep.Location = new System.Drawing.Point(12, 17);
            this.lblStep.Name = "lblStep";
            this.lblStep.Size = new System.Drawing.Size(50, 13);
            this.lblStep.TabIndex = 1;
            this.lblStep.Text = "Precision";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.lblStep);
            this.Controls.Add(this.tbStep);
            this.DoubleBuffered = true;
            this.Name = "MainWindow";
            this.Text = "Epicyclez";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainWindow_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.tbStep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrTick;
        private System.Windows.Forms.TrackBar tbStep;
        private System.Windows.Forms.Label lblStep;
    }
}

