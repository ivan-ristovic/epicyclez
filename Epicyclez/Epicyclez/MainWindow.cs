﻿using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Epicyclez.Common;

namespace Epicyclez
{
    public partial class MainWindow : Form
    {
        #region Window moving support
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Move_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion

        private readonly Painter p = new Painter();


        public MainWindow()
        {
            this.InitializeComponent();

            this.p.TryLoadData("data.json");
        }


        private void MainWindow_Paint(object sender, PaintEventArgs e) => this.p.Paint(e.Graphics, this.Width, this.Height);

        private void TmrTick_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
            this.lblEpicycles.Text = $"Epicycles : {this.p.Count}";
            this.lblTime.Text = $"Time      : {this.p.Time:n5}";
        }

        private void BtnExit_Click(object sender, EventArgs e) => this.Close();

        private void BtnStart_Click(object sender, EventArgs e) => this.p.Start();

        private void BtnStop_Click(object sender, EventArgs e) => this.p.Stop();

        private void BtnDraw_Click(object sender, EventArgs e)
        {
            this.p.Reset();
            using (var form = new DrawingForm()) {
                form.ShowDialog();
                if (form.IsConfirmed) {
                    this.p.SetData(form.DrawPoints);
                    this.trbPrecision.Value = this.trbPrecision.Maximum;
                }
            }
        }

        private void BtnRestart_Click(object sender, EventArgs e) => this.p.Restart();

        private void BtnMask_Click(object sender, EventArgs e) => this.p.MaskMode = !this.p.MaskMode;

        private void TrbInterval_ValueChanged(object sender, EventArgs e)
        {
            int value = (sender as TrackBar).Value;
            this.tmrTick.Interval = value;
            this.lblSpeed.Text = $"Speed     : {1000 / value}";
        }

        private void TrbPrecision_ValueChanged(object sender, EventArgs e) 
        {
            this.p.Count = this.p.PointCount * (sender as TrackBar).Value / 100;
        }
    }
}
