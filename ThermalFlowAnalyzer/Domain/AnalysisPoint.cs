namespace ThermalFlowAnalyzer.Domain
{
    public class AnalysisPoint
    {
        public int Id { get; set; }
        public int AnalysisInputId { get; set; }

        public double Height { get; set; }              // y
        public double RelativeHeight { get; set; }      // Y

        public double Exp1 { get; set; }                // 1 - exp(...)
        public double Exp2 { get; set; }                // 1 - m*exp(...)

        public double ThetaSolid { get; set; }          // θ1
        public double ThetaGas { get; set; }            // θ2

        public double SolidTemp { get; set; }           // Tмат
        public double GasTemp { get; set; }             // Tгаз
        public double DeltaTemp { get; set; }           // ΔT = Tмат - Tгаз
    }
}
