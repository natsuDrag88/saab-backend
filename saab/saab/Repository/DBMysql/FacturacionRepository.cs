using System;
using saab.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using saab.Dto.Billing;
using saab.Dto.Saving;
using saab.Model;

namespace saab.Repository.DBMysql
{
    public class FacturacionRepository : IFacturacionRepository
    {
        private readonly SaabContext _context;

        public FacturacionRepository(SaabContext context)
        {
            _context = context;
        }

        public List<InformationHistorical> GetHistoricalMonths(string centroCarga, IEnumerable<string> periods)
        {
            return (from x in _context.Facturas
                where x.IdCentroDeCarga == centroCarga
                where (periods.Contains(x.Periodo))
                select new InformationHistorical()
                {
                    Periodo = x.Periodo,
                    AhorroBruto = x.AhorroBruto,
                    AhorroNeto = x.AhorroNeto,
                    PagoFrv = x.PagoAfrv,
                    CargoSinSaas = x.CargoSinSaas,
                    CargoConSaas = x.CargoConSaas
                }).ToList();
        }

        public List<Invoice> GetDataInvoiceById(List<string> idInvoice)
        {
            return (from x in _context.Facturas
                    where idInvoice.Contains(x.IdDeFactura.ToString())
                    select new Invoice()
                    {
                        FacturaPdf = x.FacturaPdf,
                        FacturaXml = x.FacturaXml,
                        Periodo = x.Periodo
                    }
                ).ToList();
        }

        public decimal? GetTotalBillingByProjectAndPeriod(string centroCarga, string period)
        {
            return _context.Facturas.Where(x => x.IdCentroDeCarga == centroCarga).Where(x => x.Activo == true)
                .Where(x => x.Periodo.Contains(period)).Sum(x => x.MontoTotal);
        }

        public DateTime? GetDateOfIssueTotalBillingByProjectAndPeriod(string centroCarga, string period)
        {
            return _context.Facturas.Where(x => x.IdCentroDeCarga == centroCarga).Where(x => x.Periodo.Contains(period))
                .FirstOrDefault(x => x.Activo == true)
                ?.FechaDeCreacion;
        }

        public List<HistoricalBilling> GetHistoricalBillingsByClienteAndPeriod(int client, List<string> periodsDb)
        {
            return (from f in _context.Facturas
                join cc in _context.CentrosDeCargas
                    on f.IdCentroDeCarga equals cc.Id.ToString()
                join c in _context.Clientes
                    on cc.Idcliente equals c.Id
                where (periodsDb.Contains(f.Periodo))
                where c.HabilitadoDeshabilitado == 1
                where cc.HabilitadoDeshabilitado == 1
                where c.Id == client
                orderby c.Id, cc.Rpu
                select new HistoricalBilling
                {
                    Id = f.IdDeFactura,
                    Anio = Convert.ToInt16(f.Periodo.Substring(f.Periodo.Length - 4, 4)),
                    Mes = f.Periodo.Substring(0, f.Periodo.Length - 5),
                    Client = c.Alias,
                    Rpu = cc.Rpu,
                    Fecha = DateTime.Now,
                    Folio = f.Folio,
                    FolioContable = $"{f.Serie}-{f.Folio}",
                    FolioFiscal = f.Uuid,
                    Monto = f.MontoTotal ?? 0,
                    AjusteMesAnterior = f.AjusteMesAnterior ?? 0,
                    Estado = f.Activo ?? false ? "Habilitado" : "Deshabilitado",
                }).ToList();
        }


        public Average GetDataAverage(string year, bool typeOperation)
        {
            return (from a in (from f in _context.Facturas
                    where f.Periodo != null && f.Periodo.Contains(year)
                    group f by f.Periodo
                    into grp
                    select new
                    {
                        Period = year,
                        Addition = typeOperation
                            ? grp.Sum(x => x.AhorroBruto / x.CargoSinSaas)
                            : grp.Sum(x => x.AhorroNeto / x.CargoSinSaas)
                    }).ToList()
                group a by a.Period
                into grpAvg
                select new Average()
                {
                    ResultAverage = grpAvg.Average(x => x.Addition)
                }).FirstOrDefault();
        }

        public List<Periods> GetDListPeriodsByYear(string year)
        {
            return (from f in _context.Facturas
                orderby f.IdDeFactura
                where f.Periodo.Contains(year)
                group f by f.Periodo
                into grp
                select new Periods
                {
                    Periodo = grp.Key
                }).ToList();
        }

        public BillingTotal GetBillingTotal(string period)
        {
            return (from f in _context.Facturas
                where f.Periodo == period
                group f by f.Periodo
                into grp
                select new BillingTotal()
                {
                    TotalFacturado = grp.Sum(x => x.Subtotal) ?? 0,
                    TotalAjusteMesAnterior = grp.Sum(x => x.AjusteMesAnterior) ?? 0,
                    TotalFacturadoCfe = grp.Sum(x => x.CargoConSaas) ?? 0,
                }).FirstOrDefault();
        }

        public Task<bool> GetDataAlertWithoutInvoiceIssuance(string periodString, int idCentroCarga)
        {
            var listDataBilling = _context.Facturas
                .Where(x => x.IdCentroDeCarga == idCentroCarga.ToString())
                .Where(x => x.Periodo.Contains(periodString))
                .Any(x => x.Activo == true);

            return Task.FromResult(listDataBilling);
        }

        public List<HistoricalBilling> GetHistoricalBillingsByRpuAndPeriod(string[] listRpu, List<string> periods)
        {
            return (from f in _context.Facturas
                join cc in _context.CentrosDeCargas
                    on f.IdCentroDeCarga equals cc.Id.ToString()
                join c in _context.Clientes
                    on cc.Idcliente equals c.Id
                where (periods.Contains(f.Periodo))
                where c.HabilitadoDeshabilitado == 1
                where cc.HabilitadoDeshabilitado == 1
                where listRpu.Contains(cc.Rpu)
                orderby c.Id, cc.Rpu
                select new HistoricalBilling
                {
                    Id = f.IdDeFactura,
                    Anio = Convert.ToInt16(f.Periodo.Substring(f.Periodo.Length - 4, 4)),
                    Mes = f.Periodo.Substring(0, f.Periodo.Length - 5),
                    Client = c.Alias,
                    Rpu = cc.Rpu,
                    Fecha = DateTime.Now,
                    Folio = f.Folio,
                    FolioContable = $"{f.Serie}-{f.Folio}",
                    FolioFiscal = f.Uuid,
                    Monto = f.MontoTotal ?? 0,
                    AjusteMesAnterior = f.AjusteMesAnterior ?? 0,
                    Estado = f.Activo ?? false ? "Habilitado" : "Deshabilitado",
                }).ToList();
        }
    }
}