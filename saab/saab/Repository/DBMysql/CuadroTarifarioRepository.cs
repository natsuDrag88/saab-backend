using System;
using System.Collections.Generic;
using System.Linq;
using saab.Data;
using saab.Dto;
using saab.Util.Project;

namespace saab.Repository.DBMysql
{
    public class CuadroTarifarioRepository : ICuadroTarifarioRepository
    {
        private readonly SaabContext _context;

        public CuadroTarifarioRepository(SaabContext context)
        {
            _context = context;
        }

        public bool GetAlertFareDelayCfe(string periodString)
        {
            return _context.CuadroTarifarios.Any(x => x.Periodo == periodString);
        }

        public List<ElectricRateDto> GetElectricRates(List<string> listPeriods, string rate,
            string division)
        {
            return (from t in _context.CuadroTarifarios
                where t.Division == division
                where t.Tarifa == rate
                where listPeriods.Contains(t.Periodo)
                select new ElectricRateDto
                {
                    Period = DateUtil.ConvertPeriodDbToDate(t.Periodo),
                    Segment = t.Segmento,
                    Units = t.Unidades,
                    Concept = t.Concepto,
                    Division = t.Division,
                    TimeInterval = t.IntHorario,
                    FeeCharges = t.CargosTrifarios,
                }).ToList();
        }

        public List<string> GetDivision() =>
            _context.CuadroTarifarios
                .GroupBy(x => x.Division)
                .Select(x => (x.Key)).ToList();


        public List<GenericObject<string, string>> GetRates()
            => new List<GenericObject<string, string>>
            {
                new GenericObject<string, string>()
                {
                    Id = "DIT",
                    Description = "Demanda industrial en transmisión"
                },
                new GenericObject<string, string>()
                {
                    Id = "DIST",
                    Description = "Demanda industrial en subtransmisión"
                },
                new GenericObject<string, string>()
                {
                    Id = "GDMTH",
                    Description = "Gran demanda en media tensión horaria"
                },
                new GenericObject<string, string>()
                {
                    Id = "GDMTO",
                    Description = "Gran demanda en media tensión ordinaria"
                },
                new GenericObject<string, string>()
                {
                    Id = "APMT",
                    Description = "Alumbrado público en media tensión"
                },
                new GenericObject<string, string>()
                {
                    Id = "APBT",
                    Description = "Alumbrado público en baja tensión"
                },
                new GenericObject<string, string>()
                {
                    Id = "RAMT",
                    Description = "Riego agrícola en media tensión"
                },
                new GenericObject<string, string>()
                {
                    Id = "RABT",
                    Description = "Riego agrícola en baja tensión"
                }
            };
    }
}