using ThermalFlowAnalyzer.Domain;
using System.Collections.Generic;

namespace ThermalFlowAnalyzer.Logic
{
    public interface ICounterflowSolver
    {
        List<AnalysisPoint> Solve(AnalysisInput input);
    }
}
