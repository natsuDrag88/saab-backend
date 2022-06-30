using System.Collections.Generic;
using System.Linq;
using saab.Data;
using saab.Dto.Alerts;
using saab.Dto.Billing;
using saab.Util.Project;

namespace saab.Repository.DBMysql
{
    public class XmlCfeRepository : IXmlCfeRepository
    {
        private readonly SaabContext _context;

        public XmlCfeRepository(SaabContext context)
        {
            _context = context;
        }

        public bool GetDataAlertNoInvoice(string period, string rpu)
        {
            var listDataBilling = (from x in _context.XmlCves
                where x.Rpu == rpu
                where x.Periodo == period
                select new AlertWithoutCfeInvoice
                {
                    Rpu = x.Rpu
                }).Any();

            return listDataBilling;
        }

        public List<BillItemizationOption> GetBillItemizationOptions()
        {
            return new List<BillItemizationOption>
            {
                new BillItemizationOption
                {
                    Id = 1,
                    Description = "Consumo de energia"
                },
                new BillItemizationOption
                {
                    Id = 2,
                    Description = "Demanda"
                },
                new BillItemizationOption
                {
                    Id = 3,
                    Description = "Costo Regulados"
                },
                new BillItemizationOption
                {
                    Id = 4,
                    Description = "Costo de Energía"
                }
            };
        }

        public List<BillsCfe> GetBillCfeByPeriodsAndRpu(List<string> listPeriods, List<string> listRpu)
        {
            return (from x in _context.XmlCves
                             where listRpu.Contains(x.Rpu)
                             where listPeriods.Contains(x.Periodo)
                             select new BillsCfe
                             {
                                 Period = DateUtil.ConvertPeriodToDate(x.Periodo),
                                 RegulatedCostSupplyCharge = x.CargoSuministro,
                                 RegulatedCostDistributionCharge = x.CargoDistribucion,
                                 RegulatedCostTransmissionCharge = x.CargoTransmision,
                                 RegulatedCostCenacePosition = x.CargoCenace,
                                 EnergyCostBaseGenerationCharge = x.CargoGeneracionB,
                                 EnergyCostIntermediateGenerationCharge = x.CargoGeneracionI,
                                 EnergyCostPeakGenerationCharge = x.CargoGeneracionP,
                                 DemandConsumptionBase = x.KWBaseDemanda,
                                 DemandIntermediateConsumption = x.KWIntermediaDemanda,
                                 DemandPeakConsumption = x.KWPuntaDemanda,
                                 EnergyConsumptionConsumptionBase = x.KWhBaseConsumo,
                                 EnergyConsumptionIntermediateConsumption = x.KWhIntermediaConsumo,
                                 EnergyConsumptionPeakConsumption = x.KWhPuntaConsumo,
                                 EnergyCostCapacityCharge = x.CargoCapacidad,
                                 EnergyCostScnmCharge = x.CargoScnMem,
                                 TotalBilling = x.TotalFacturado
                             }
                         ).ToList();
        }
        
    }
}