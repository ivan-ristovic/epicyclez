using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Epicyclez.Common
{
    public static class FFT
    {
        public static IReadOnlyList<Epicycle> DFT(IEnumerable<double> values)
        {
            var ys = values.ToList();
            var dft = new List<Epicycle>(ys.Count);

            for (int i = 0; i < ys.Count; i++) {
                double re = 0, im = 0;

                for (int n = 0; n < ys.Count; n++) {
                    double phi = 2 * Math.PI * i * n / ys.Count;
                    re += ys[n] * Math.Cos(phi);
                    im -= ys[n] * Math.Sin(phi);
                }

                dft.Add(new Epicycle(i, new Complex(re / ys.Count, im / ys.Count)));
            }

            return dft.ToList().AsReadOnly();
        }
    }
}
