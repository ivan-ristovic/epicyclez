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
        public int Skip {
            get => this.skip;
            set {
                if (value > 1) {
                    this.skip = value;
                    this.Resample();
                }
            }
        }

        private bool isLoaded;
        private bool isPainting;
        private int skip;
        private float time;
        private List<(double X, double Y)> data;
        private List<Epicycle> csX;
        private List<Epicycle> csY;
        private readonly List<PointF> path = new List<PointF>();


        public Painter()
        {
            this.skip = 1;
            this.time = 0;
        }


        public void TryLoadData(string path)
        {
            this.data = JsonConvert.DeserializeObject<List<List<double>>>(File.ReadAllText(path)).Select(p => (p.First(), p.Last())).ToList();
            this.skip = 8;
            this.isLoaded = true;
            this.Resample();
        }

        public void SetData(IEnumerable<(double, double)> data)
        {
            this.data = data.ToList();
            this.skip = 0;
            this.isLoaded = true;
            this.Resample();
        }

        public void Resample()
        {
            if (!this.isLoaded)
                return;

            if (this.Skip > 1) {
                var csXnew = new List<double>();
                var csYnew = new List<double>();
                for (int i = 0; i < this.data.Count; i++) {
                    if (i % this.Skip == 0) {
                        csXnew.Add(this.data[i].X);
                        csYnew.Add(this.data[i].Y);
                    }
                }
                this.csX = FFT.DFT(csXnew).OrderByDescending(c => c.Amplitude).ToList();
                this.csY = FFT.DFT(csYnew).OrderByDescending(c => c.Amplitude).ToList();
            } else {
                this.csX = FFT.DFT(this.data.Select(p => p.X)).OrderByDescending(c => c.Amplitude).ToList();
                this.csY = FFT.DFT(this.data.Select(p => p.Y)).OrderByDescending(c => c.Amplitude).ToList();
            }

            this.Reset();
        }

        public void Paint(Graphics g, int w, int h)
        {
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
                    path.AddLines(this.path.Select(t => new PointF(t.X, t.Y)).ToArray());
                    g.DrawPath(p, path);
                }
            }

            if (this.isPainting) {
                this.time += (float)(2 * Math.PI / this.Count);
                if (this.time > 2 * Math.PI)
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
            this.time = 0;
            this.path.Clear();
        }


        private (float X, float Y) DrawEpicycles(Graphics g, float x, float y, double rotation, IReadOnlyList<Epicycle> cs)
        {
            using (var p = new Pen(Color.White)) {
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
