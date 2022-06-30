using System;
using System.Collections.Generic;
using saab.Data;
using saab.Model;
using System.Linq;
using saab.Dto.Project;

namespace saab.Repository.DBMysql
{
    public class CentroDeCargaRepository : ICentroDeCargaRepository
    {
        private readonly SaabContext _context;

        public CentroDeCargaRepository(SaabContext context)
        {
            _context = context;
        }

        public ProjectRecord GetRecordGeneralProjectByProject(int idProject)
        {
            return (
                from cc in _context.CentrosDeCargas
                where cc.Id == idProject
                select new ProjectRecord
                {
                    Direccion =
                        $" Calle {cc.Calle},  Número exterior {cc.NumeroExterior}, Número interior {cc.NumeroInterior}, Colonia {cc.Colonia} , Código Postal {cc.CodigoPostal},  {cc.DelegacionOMunicipio}, {cc.EntidadFederativa}, {cc.Pais}  ",
                    TarifasCfe = cc.TarifaCfe,
                    DemandaContratada = cc.DemandaContratadaCfe,
                    TipoContrato = cc.TipoContrato,
                    Duracion = cc.DuracionContratoYears,
                    FechaFirma = cc.FechaFirmaContrato,
                    FechaInicioOperacion = cc.FechaInicioDeOperacion,
                    FactorAhorro = cc.FactorDeAhorro,
                    PenaTerminacionAnticipada = cc.PenaTerminacion,
                    Capacidad = cc.KwhinstaladosBat,
                    Instalador = cc.EpcInstalador,
                    Fabricante = cc.FabricanteBateria,
                    Controlador = cc.Controlador,
                    Capex = cc.Capex,
                    Opex = cc.Opex,
                    IngresoMensualEsperado = cc.IngresoAnualEsperado,
                }).FirstOrDefault();
        }

        public CentrosDeCarga GetRecordGeneralByRpu(string rpu)
        {
            return _context.CentrosDeCargas.FirstOrDefault(x => x.Rpu == rpu);
        }

        public decimal? GetCapacityInstalledByStatus(ulong statusCentroCarga)
        {
            return _context.CentrosDeCargas
                .Where(x => x.HabilitadoDeshabilitado == statusCentroCarga)
                .Sum(x => x.KwhinstaladosBat);
        }

        public List<ProjectClient> GetProjectsByStatusProjectAndStatusClient(sbyte statusClient,
            ulong statusCentroCarga)
        {
            return (from cc in _context.CentrosDeCargas
                    join c in _context.Clientes on cc.Idcliente equals c.Id
                    where c.HabilitadoDeshabilitado == statusClient
                    where cc.HabilitadoDeshabilitado == statusCentroCarga
                    select new ProjectClient
                    {
                        IdClient = c.Id,
                        IdCentroCarga = cc.Id,
                        Client = c.Alias,
                        Rpu = cc.Rpu,
                        PaymentDeadlineDays = cc.DiasLimitePago,
                        Rate = cc.TarifaCfe
                    }
                ).ToList();
        }

        public List<CentrosDeCarga> GetProjectsByStatusOptionalFilters(ulong statusCentroCarga, int? client = null, string rpu = null)
        {
            throw new NotImplementedException();
        }

        public List<CentrosDeCarga> GetProjectsByStatus(ulong statusCentroCarga)
        {
            return _context.CentrosDeCargas.Where(x => x.HabilitadoDeshabilitado == statusCentroCarga).ToList();
        }

        public List<CentrosDeCarga> GetProjectsByStatusOptionalFilters(ulong status, int? client,
            int? project, string rpu)
        {
            var query = _context.CentrosDeCargas.Where(x => x.HabilitadoDeshabilitado == status);
            if (client.HasValue) query = query.Where(x => x.Idcliente == client);
            if (project.HasValue) query = query.Where(x => x.Id == project);
            if (!string.IsNullOrEmpty(rpu)) query = query.Where(x => x.Rpu == rpu);
            return query.ToList();
        }

        public List<ProjectData> GetDataProjectsAll()
        {
            return (from cc in _context.CentrosDeCargas
                    join c in _context.Clientes on cc.Idcliente equals c.Id
                    select new ProjectData()
                    {
                        Proyecto = c.Alias,
                        Rpu = cc.Rpu,
                        Estatus = cc.HabilitadoDeshabilitado == 1 ? "Activo" : "Inactivo",
                        TipoContrato = cc.TipoContrato,
                        FechaInicio = cc.FechaInicioDeOperacion,
                        Duracion = cc.DuracionContratoYears,
                        CapacidadBateria =cc.KwhinstaladosBat,
                        DemandaContratada = cc.DemandaContratadaCfe,
                        Municipio = cc.DelegacionOMunicipio,
                        Estado = cc.EntidadFederativa,
                        TafifaCfe = cc.TarifaCfe,
                        Epc = cc.EpcInstalador,
                        FabricanteBateria = cc.FabricanteBateria,
                        Controlador = cc.Controlador,
                        PenaTerminacionAnticipada = cc.PenaTerminacion,
                        Capex = cc.Capex,
                        Opex = cc.Opex,
                        IngresosAnualesEsperados = cc.IngresoAnualEsperado,
                        FactorAhorro = cc.FactorDeAhorro
                    }
                ).ToList();
        }

        public List<CentrosDeCarga> GetAll()
        {
            return _context.CentrosDeCargas.ToList();
        }
    }
}