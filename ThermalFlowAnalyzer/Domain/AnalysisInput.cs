using System.ComponentModel.DataAnnotations;

namespace ThermalFlowAnalyzer.Domain
{
    public class AnalysisInput
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название проекта")]
        [StringLength(100, ErrorMessage = "Название слишком длинное")]
        public string ProjectTitle { get; set; }

        [Range(0.5, 20, ErrorMessage = "Высота слоя должна быть от 0,5 до 20 м")]
        public double LayerHeight { get; set; }

        [Range(-50, 1500, ErrorMessage = "Температура материала вне допустимого диапазона")]
        public double SolidTempStart { get; set; }

        [Range(-50, 1500, ErrorMessage = "Температура газа вне допустимого диапазона")]
        public double GasTempStart { get; set; }

        [Range(0.1, 2, ErrorMessage = "Скорость газа должна быть от 0,1 до 2 м/с")]
        public double GasSpeed { get; set; }

        [Range(0.5, 2, ErrorMessage = "Теплоёмкость газа должна быть от 0,5 до 2 кДж/(м³·К)")]
        public double GasHeatCapacity { get; set; }

        [Range(0.1, 5, ErrorMessage = "Расход материала должен быть от 0,1 до 5 кг/с")]
        public double SolidFlow { get; set; }

        [Range(0.5, 2, ErrorMessage = "Теплоёмкость материала должна быть от 0,5 до 2 кДж/(кг·К)")]
        public double SolidHeatCapacity { get; set; }

        [Range(500, 5000, ErrorMessage = "αV должен быть от 500 до 5000 Вт/(м³·К)")]
        public double HeatTransferFactor { get; set; }

        [Range(0.5, 5, ErrorMessage = "Диаметр колонны должен быть от 0,5 до 5 м")]
        public double ColumnDiameter { get; set; }
    }
}
