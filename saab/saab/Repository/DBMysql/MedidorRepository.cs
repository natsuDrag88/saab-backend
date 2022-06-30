using System;
using System.Collections.Generic;
using System.Linq;
using saab.Data;
using saab.Dto.Meter;
using saab.Model;

namespace saab.Repository.DBMysql
{
    public class MedidorRepository : IMedidorRepository
    {
        private readonly SaabContext _context;

        public MedidorRepository(SaabContext context)
        {
            _context = context;
        }

        public List<DataMeter> GetDataMeter(string centroCarga)
        {
            return (from m in _context.Medidores
                where m.CentroDeCarga == Convert.ToInt32(centroCarga)
                select new DataMeter()
                {
                    Id = m.Id,
                    Nombre = m.Nombre,
                    Provedor = m.Provedor,
                    FuenteInformacion = m.FuenteDeInformacion,
                    Habilitado = m.HabilitadoDeshabilitado == 1 ? "Activo" : "Inactivo"
                }).ToList();
        }

        public Medidore GetNameProvider(string idProvider)
        {
            return _context.Medidores.FirstOrDefault(m => m.Id == int.Parse(idProvider));
        }

        public RateMeter GetRateCfe(string idProvider)
        {
            return (from m in _context.Medidores
                join cc in _context.CentrosDeCargas on m.CentroDeCarga equals cc.Id 
                where m.Id == int.Parse(idProvider)
                select new RateMeter
                {
                    TarifaCfe = cc.TarifaCfe
                }).FirstOrDefault();
        }
    }
}