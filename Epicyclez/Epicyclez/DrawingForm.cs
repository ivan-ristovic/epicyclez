using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Epicyclez
{
    public partial class DrawingForm : Form
    {
        public bool IsConfirmed { get; private set; }
        public IReadOnlyList<(double, double)> DrawPoints => this.drawPoints.Select(p => (p.X - 100, p.Y - 150)).ToList().AsReadOnly();

        private List<(double X, double Y)> drawPoints = new List<(double, double)>();


        public DrawingForm()
        {
            this.InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            this.IsConfirmed = true;
            this.Close();
        }

        private void DrawingForm_MouseDown(object sender, MouseEventArgs e)
        {
            this.drawPoints = new List<(double, double)>();
            this.tmrDraw.Enabled = true;
        }

        private void DrawingForm_MouseUp(object sender, MouseEventArgs e)
        {
            this.tmrDraw.Enabled = false;
        }

        private void TmrDraw_Tick(object sender, EventArgs e)
        {
            CursorLocationPoint p = GetCursorPosition();
            this.drawPoints.Add((p.X - this.Location.X, p.Y - this.Location.Y));
            Console.WriteLine(this.drawPoints.Last());
        }

        private void TmrTick_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void DrawingForm_Paint(object sender, PaintEventArgs e)
        {
            if (this.drawPoints.Count > 1) {
                using (var p = new Pen(Color.Red, 2)) {
                    var path = new GraphicsPath();
                    path.AddLines(this.drawPoints.Select(t => new PointF((float)t.X, (float)t.Y)).ToArray());
                    e.Graphics.DrawPath(p, path);
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #region Mouse position helper
        [StructLayout(LayoutKind.Sequential)]
        public struct CursorLocationPoint
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out CursorLocationPoint lpPoint);

        public static CursorLocationPoint GetCursorPosition()
        {
            GetCursorPos(out CursorLocationPoint lpPoint);
            return lpPoint;
        }
        #endregion
    }
}
