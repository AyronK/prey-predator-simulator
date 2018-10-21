namespace PreyPredatorSim.Brain.Extensions
{
    public static class MathExtensions
    {
        public static double Clamp(this double value, double min = double.MinValue, double max = double.MaxValue)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}