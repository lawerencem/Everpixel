﻿namespace Assets.Template.Math
{
    public class DistributionMath
    {
        // Z Score stuff
        private double a1 = 0.254829592;
        private double a2 = -0.284496736;
        private double a3 = 1.421413741;
        private double a4 = -1.453152027;
        private double a5 = 1.061405429;
        private double p = 0.3275911;

        private double GetPercentileFromZScore(double Z)
        {
            int sign = 1;
            if (Z < 0)
                sign = -1;
            double x = System.Math.Abs(Z) / System.Math.Sqrt(2.0);

            double t = 1.0 / (1.0 + p * x);
            double y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * System.Math.Exp(-x * x);

            return (0.5 * (1.0 + sign * y));
        }

        public double DetermineDoubleDistributionProbability(int meanToGet, int meanToCompare, int stdDev)
        {
            int median = (meanToGet + meanToCompare) / 2;
            double z = System.Math.Abs(meanToGet - median);
            z /= stdDev;
            var probability = GetPercentileFromZScore(z);
            probability = 2 * (probability * probability);

            if (meanToGet > meanToCompare)
                return probability;
            else
                return (1 - probability);
        }
    }
}
