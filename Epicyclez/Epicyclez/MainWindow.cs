using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Epicyclez.Common;
using Newtonsoft.Json;

namespace Epicyclez
{
    public partial class MainWindow : Form
    {
        private int Count => this.csX.Count;
        private float time = 0;
        private readonly int skip = 8;

        private readonly IReadOnlyList<Epicycle> csX;
        private readonly IReadOnlyList<Epicycle> csY;
        private readonly List<PointF> path = new List<PointF>();


        public MainWindow()
        {
            this.InitializeComponent();

            // TODO
            var data = JsonConvert.DeserializeObject<List<List<double>>>(File.ReadAllText("data.json"))
                .Zip(Enumerable.Range(1, 10000), (f, s) => (f, s))
                .Where(t => t.s % skip == 0)
                .Select(t => t.f)
                .ToList();
            this.csX = FFT.DFT(data.Select(p => p.First())).OrderByDescending(c => c.Amplitude).ToList();
            this.csY = FFT.DFT(data.Select(p => p.Last())).OrderByDescending(c => c.Amplitude).ToList();

        }


        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            (float vxX, float vxY) = this.DrawEpicycles(g, this.Width / 2 + 100, 150, 0, this.csX);
            (float vyX, float vyY) = this.DrawEpicycles(g, 150, this.Height / 2 + 100, Math.PI / 2, this.csY);

            this.path.Insert(0, new PointF(vxX, vyY));

            using (var p = new Pen(Color.Gray)) {
                g.DrawLine(p, vxX, vxY, vxX, vyY);
                g.DrawLine(p, vyX, vyY, vxX, vyY);
            }
            if (this.path.Count > 1) {
                using (var p = new Pen(Color.Red, 2)) {
                    var path = new GraphicsPath();
                    path.AddLines(this.path.Select(t => new PointF(t.X, t.Y)).ToArray());
                    g.DrawPath(p, path);
                }
            }

            this.time += (float)(2 * Math.PI / this.Count);

            if (this.time > 2 * Math.PI) {
                this.time = 0;
                this.path.Clear();
            }
        }

        private void TmrTick_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private (float X, float Y) DrawEpicycles(Graphics g, float x, float y, double rotation, IReadOnlyList<Epicycle> cs)
        {
            using (var p = new Pen(Color.Black)) { 
                foreach (Epicycle c in cs) {
                    float px = x, py = y;
                    g.TranslateTransform(-(float)c.Amplitude, -(float)c.Amplitude);
                    x += (float)(c.Amplitude * Math.Cos(c.Frequency * this.time + c.Phase + rotation));
                    y += (float)(c.Amplitude * Math.Sin(c.Frequency * this.time + c.Phase + rotation));
                    g.DrawEllipse(p, px, py, (float)c.Amplitude * 2, (float)c.Amplitude * 2);
                    g.TranslateTransform((float)c.Amplitude, (float)c.Amplitude);
                    g.DrawLine(p, px , py , x , y );
                }
            }
            return (x, y);
        }

        private void TbStep_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
