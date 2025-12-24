using ThermalFlowAnalyzer.Domain;

namespace ThermalFlowAnalyzer.Logic
{
    public class CounterflowSolver : ICounterflowSolver
    {
        public List<AnalysisPoint> Solve(AnalysisInput p)
        {
            var list = new List<AnalysisPoint>();

            // Площадь сечения
            double S = Math.PI * Math.Pow(p.ColumnDiameter / 2.0, 2.0);

            // Водяные эквиваленты (Вт/К)
            double Wm = p.SolidFlow * p.SolidHeatCapacity * 1000.0;
            double Wg = p.GasSpeed * S * p.GasHeatCapacity * 1000.0;

            double m = Wm / Wg;

            // ПОЛНАЯ относительная высота (КАК В МЕТОДИЧКЕ)
            double Y0 = (p.HeatTransferFactor * S * p.LayerHeight) / Wg;

            double denominator = 1.0 - m * Math.Exp(((m - 1.0) * Y0) / m);

            for (double y = 0; y <= p.LayerHeight + 1e-9; y += 0.5)
            {
                // ОТНОСИТЕЛЬНАЯ ВЫСОТА (КАК В EXCEL)
                double Y = (p.HeatTransferFactor * S * y) / Wg;

                double arg = ((m - 1.0) * Y) / m;
                arg = Math.Max(-50, Math.Min(50, arg)); // защита

                double expVal = Math.Exp(arg);

                double exp1 = 1.0 - expVal;
                double exp2 = 1.0 - m * expVal;

                double theta1 = exp1 / denominator;
                double theta2 = exp2 / denominator;

                double Ts =
                    p.SolidTempStart +
                    (p.GasTempStart - p.SolidTempStart) * theta1;

                double Tg =
                    p.SolidTempStart +
                    (p.GasTempStart - p.SolidTempStart) * theta2;

                list.Add(new AnalysisPoint
                {
                    Height = Math.Round(y, 1),
                    RelativeHeight = Math.Round(Y, 2),

                    Exp1 = Math.Round(exp1, 4),
                    Exp2 = Math.Round(exp2, 4),

                    ThetaSolid = Math.Round(theta1, 4),
                    ThetaGas = Math.Round(theta2, 4),

                    SolidTemp = Math.Round(Ts, 2),
                    GasTemp = Math.Round(Tg, 2),

                    // СТРОГО по методичке
                    DeltaTemp = Math.Round(Ts - Tg, 2)
                });
            }

            return list;
        }
    }
}
