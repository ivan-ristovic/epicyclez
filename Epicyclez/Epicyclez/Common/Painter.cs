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
        public int Count { get; private set; }
        public int Skip {
            get => this.skip;
            set {
                if (value > 0) {
                    this.skip = value;
                    this.Resample();
                }
            }
        }

        private bool isLoaded;
        private int skip;
        private float time = 0;
        private List<Epicycle> csX;
        private List<Epicycle> csY;
        private readonly List<PointF> path = new List<PointF>();


        public Painter()
        {
            this.skip = 1;
        }


        public void TryLoadData(string path)
        {
            var data = JsonConvert.DeserializeObject<List<List<double>>>(File.ReadAllText("data.json")).ToList();
            this.Count = data.Count;
            this.csX = FFT.DFT(data.Select(p => p.First())).OrderByDescending(c => c.Amplitude).ToList();
            this.csY = FFT.DFT(data.Select(p => p.Last())).OrderByDescending(c => c.Amplitude).ToList();
            this.Resample();
            this.isLoaded = true;
        }

        public void Resample()
        {
            if (!this.isLoaded || this.Skip < 2)
                return;

            for (int i = 0; i < this.Count / this.Skip; i++) {
                this.csX.RemoveRange(i, this.Skip - 1);
                this.csY.RemoveRange(i, this.Skip - 1);
            }
        }

        public void Paint(Graphics g, int w, int h)
        {
            (float vxX, float vxY) = this.DrawEpicycles(g, w / 2 + 100, 150, 0, this.csX);
            (float vyX, float vyY) = this.DrawEpicycles(g, 150, h / 2 + 100, Math.PI / 2, this.csY);

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
                    g.DrawLine(p, px, py, x, y);
                }
            }
            return (x, y);
        }
    }
}
