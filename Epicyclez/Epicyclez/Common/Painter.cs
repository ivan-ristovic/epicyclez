using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Epicyclez.Common
{
    public sealed class Painter
    {
        public int Count => this.csX.Count == this.csY.Count ? this.csX.Count : 0;
        public bool MaskMode { get; set; }
        public float Time { get; private set; }

        private bool isLoaded;
        private bool isPainting;
        private IReadOnlyList<(double X, double Y)> data;
        private IReadOnlyList<Epicycle> csX;
        private IReadOnlyList<Epicycle> csY;
        private readonly List<PointF> path = new List<PointF>();
        

        public void TryLoadData(string path)
        {
            this.data = JsonConvert.DeserializeObject<List<List<double>>>(File.ReadAllText(path)).Select(p => (p.First(), p.Last())).ToList();
            this.isLoaded = true;
            this.Resample();
        }

        public void SetData(IEnumerable<(double, double)> data)
        {
            this.data = data.ToList();
            this.isLoaded = true;
            this.Resample();
        }

        public void Resample()
        {
            if (!this.isLoaded)
                return;

            this.csX = FFT.DFT(this.data.Select(p => p.X));
            this.csY = FFT.DFT(this.data.Select(p => p.Y));

            this.Reset();
        }

        public void Paint(Graphics g, int w, int h)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            float offx = w / 2;
            float offy = h / 2 + 100;
            if (this.MaskMode)
                this.DrawData(g, offx, offy);

            (float vxX, float vxY) = this.DrawEpicycles(g, w / 2, 200, 0, this.csX);
            (float vyX, float vyY) = this.DrawEpicycles(g, 150, h / 2 + 100, Math.PI / 2, this.csY);

            this.path.Insert(0, new PointF(vxX, vyY));

            using (var p = new Pen(Color.White)) {
                g.DrawLine(p, vxX, vxY, vxX, vyY);
                g.DrawLine(p, vyX, vyY, vxX, vyY);
            }
            if (this.path.Count > 1) {
                using (var p = new Pen(Color.Red, 2)) {
                    var path = new GraphicsPath();
                    path.AddCurve(this.path.Select(t => new PointF(t.X, t.Y)).ToArray());
                    g.DrawPath(p, path);
                }
            }

            if (this.isPainting) {
                this.Time += (float)(2 * Math.PI / this.Count);
                if (this.Time > 2 * Math.PI)
                    this.Restart();
            }
        }

        public void Start()
            => this.isPainting = true;

        public void Stop()
            => this.isPainting = false;

        public void Reset()
        {
            this.Stop();
            this.Restart();
        }

        public void Restart()
        {
            this.Time = 0;
            this.path.Clear();
        }


        private (float X, float Y) DrawEpicycles(Graphics g, float x, float y, double rotation, IReadOnlyList<Epicycle> cs)
        {
            using (var p = new Pen(Color.White)) {
                foreach (Epicycle c in cs) {
                    float px = x, py = y;
                    g.TranslateTransform(-(float)c.Amplitude, -(float)c.Amplitude);
                    x += (float)(c.Amplitude * Math.Cos(c.Frequency * this.Time + c.Phase + rotation));
                    y += (float)(c.Amplitude * Math.Sin(c.Frequency * this.Time + c.Phase + rotation));
                    g.DrawEllipse(p, px, py, (float)c.Amplitude * 2, (float)c.Amplitude * 2);
                    g.TranslateTransform((float)c.Amplitude, (float)c.Amplitude);
                    g.DrawLine(p, px, py, x, y);
                }
            }
            return (x, y);
        }

        private void DrawData(Graphics g, float offx, float offy)
        {
            using (var pen = new Pen(Color.White))
                g.DrawLines(pen, this.data.Select(p => new PointF((float)p.X + offx, (float)p.Y + offy)).ToArray());
        }
    }
}
