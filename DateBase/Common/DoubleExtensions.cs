namespace CorectMyQuran.DateBase.Common;

public static class DoubleExtensions
{
    const double _3 = 0.001;
    const double _4 = 0.0001;
    const double _5 = 0.00001;
    const double _6 = 0.000001;
    const double _7 = 0.0000001;

    public static bool Equals3DigitPrecision(this double left, double right)
    {
        return Math.Abs(left - right) < _3;
    }

    public static bool Equals4DigitPrecision(this double left, double right)
    {
        return Math.Abs(left - right) < _4;
    }
}