using System.Collections.Generic;

namespace ThermalFlowAnalyzer.Domain
{
    public class AnalysisViewModel
    {
        public AnalysisInput Input { get; set; }
        public List<AnalysisPoint> Points { get; set; }
    }
}
