using System;

namespace saab.Dto.Billing
{
    public class BillsCfe
    {
        public DateTime Period { get; set; }
        public decimal? EnergyCostBaseGenerationCharge { get; set; }
        public decimal? EnergyCostIntermediateGenerationCharge { get; set; }
        public decimal? EnergyCostPeakGenerationCharge { get; set; }
        public decimal? DemandConsumptionBase { get; set; }
        public decimal? DemandIntermediateConsumption { get; set; }
        public decimal? DemandPeakConsumption { get; set; }
        public decimal? RegulatedCostSupplyCharge { get; set; }
        public decimal? RegulatedCostDistributionCharge { get; set; }
        public decimal? RegulatedCostTransmissionCharge { get; set; }
        public decimal? RegulatedCostCenacePosition { get; set; }
        public decimal? EnergyConsumptionConsumptionBase { get; set; }
        public decimal? EnergyConsumptionIntermediateConsumption { get; set; }
        public decimal? EnergyConsumptionPeakConsumption { get; set; }
        public decimal? EnergyCostCapacityCharge { get; set; }
        public decimal? EnergyCostScnmCharge { get; set; }
        public decimal? TotalBilling { get; set; }
    }
}