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
        public IReadOnlyList<(double, double)> DrawPoints => this.drawPoints.Select(p => ((double)(p.X - 100), (double)(p.Y - 150))).ToList().AsReadOnly();

        private List<CursorLocationPoint> drawPoints = new List<CursorLocationPoint>();


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
            this.drawPoints = new List<CursorLocationPoint>();
            this.tmrDraw.Enabled = true;
        }

        private void DrawingForm_MouseUp(object sender, MouseEventArgs e) => this.tmrDraw.Enabled = false;

        private void TmrDraw_Tick(object sender, EventArgs e)
        {
            CursorLocationPoint p = this.GetCursorPosition();
            if (InBounds(p))
                this.drawPoints.Add(p);

            bool InBounds(CursorLocationPoint cl)
                => cl.X >= 0 && cl.X <= this.Width && cl.Y >= 0 && cl.Y <= this.Height;
        }

        private void TmrTick_Tick(object sender, EventArgs e) => this.Invalidate();

        private void DrawingForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (this.drawPoints.Count > 1) {
                using (var p = new Pen(Color.Red, 2)) {
                    var path = new GraphicsPath();
                    path.AddLines(this.drawPoints.Select(pos => (Point)pos).ToArray());
                    e.Graphics.DrawPath(p, path);
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e) => this.Close();


        #region Mouse position helper
        [StructLayout(LayoutKind.Sequential)]
        public struct CursorLocationPoint
        {
            public static explicit operator Point(CursorLocationPoint p) => new Point(p.X, p.Y);

            public int X;
            public int Y;
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out CursorLocationPoint lpPoint);

        public CursorLocationPoint GetCursorPosition()
        {
            GetCursorPos(out CursorLocationPoint lpPoint);
            lpPoint.X -= this.Location.X;
            lpPoint.Y -= this.Location.Y;
            return lpPoint;
        }
        #endregion
    }
}
