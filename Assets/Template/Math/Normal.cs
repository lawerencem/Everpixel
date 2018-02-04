using System;

namespace Assets.Template.Math
{
    public class Normal
    {
        private double _mean;
        private double _stdDev;
        private static Random _random = new Random(Guid.NewGuid().GetHashCode());

        public double Mean { get { return this._mean; } }
        public double StdDev { get { return this._stdDev; } }

        public Normal(double mean, double stdDev)
        {
            this._mean = mean;
            this._stdDev = stdDev;
        }

        // <copyright file="Normal.cs" company="Math.NET">
        // Math.NET Numerics, part of the Math.NET Project
        // http://numerics.mathdotnet.com
        // http://github.com/mathnet/mathnet-numerics
        //
        // Copyright (c) 2009-2015 Math.NET
        //
        // Permission is hereby granted, free of charge, to any person
        // obtaining a copy of this software and associated documentation
        // files (the "Software"), to deal in the Software without
        // restriction, including without limitation the rights to use,
        // copy, modify, merge, publish, distribute, sublicense, and/or sell
        // copies of the Software, and to permit persons to whom the
        // Software is furnished to do so, subject to the following
        // conditions:
        //
        // The above copyright notice and this permission notice shall be
        // included in all copies or substantial portions of the Software.
        //
        // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
        // EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
        // OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
        // NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
        // HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
        // WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
        // FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
        // OTHER DEALINGS IN THE SOFTWARE.
        // </copyright

        public double Sample()
        {
            return (SampleUnchecked(_random, this._mean, this._stdDev));
        }

        internal static double SampleUnchecked(System.Random rnd, double mean, double stddev)
        {
            double x, y;
            while (!PolarTransform(rnd.NextDouble(), rnd.NextDouble(), out x, out y))
            {
            }

            return mean + (stddev * x);
        }

        static bool PolarTransform(double a, double b, out double x, out double y)
        {
            var v1 = (2.0 * a) - 1.0;
            var v2 = (2.0 * b) - 1.0;
            var r = (v1 * v1) + (v2 * v2);
            if (r >= 1.0 || r == 0.0)
            {
                x = 0;
                y = 0;
                return false;
            }

            var fac = System.Math.Sqrt(-2.0 * System.Math.Log(r) / r);
            x = v1 * fac;
            y = v2 * fac;
            return true;
        }
    }
}
