using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Epicyclez.Common
{
    public static class Fourier
    {
        public static IReadOnlyList<Epicycle> DiscreteTransform(IEnumerable<float> values, int? count = null)
        {
            var ys = values.ToList();
            if (count is null || count < 1)
                count = ys.Count;
            var dft = new List<Epicycle>(count.Value);

            for (int i = 0; i < count; i++) {
                double re = 0, im = 0;

                for (int n = 0; n < ys.Count; n++) {
                    double phi = 2 * Math.PI * i * n / ys.Count;
                    re += ys[n] * Math.Cos(phi);
                    im -= ys[n] * Math.Sin(phi);
                }

                dft.Add(new Epicycle(i, new Complex(re / ys.Count, im / ys.Count)));
            }

            return dft.OrderByDescending(c => c.Amplitude).ToList().AsReadOnly();
        }
    }
}
