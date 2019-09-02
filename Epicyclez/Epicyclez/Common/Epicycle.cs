using System;
using System.Numerics;

namespace Epicyclez.Common
{
    public sealed class Epicycle
    {
        public double Frequency { get; }
        public double Amplitude { get; }
        public double Phase => this.value.Phase;

        private readonly Complex value;


        public Epicycle(int freq, Complex c)
        {
            this.Frequency = freq;
            // Deliberately not using Complex.Magnitude for Amplitude due to poor precision level
            this.Amplitude = Math.Sqrt(c.Real * c.Real + c.Imaginary * c.Imaginary);  
            this.value = c;
        }
    }
}
