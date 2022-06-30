using System;
using System.Collections.Generic;

#nullable disable

namespace saab.Model
{
    public partial class CentrosDeCarga
    {
        public CentrosDeCarga()
        {
            Medidores = new HashSet<Medidore>();
        }

        public int Id { get; set; }
        public int Idcliente { get; set; }
        public string Rmu { get; set; }
        public string Rpu { get; set; }
        public string Calle { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public string Colonia { get; set; }
        public string DelegacionOMunicipio { get; set; }
        public string EntidadFederativa { get; set; }
        public string Pais { get; set; }
        public string CodigoPostal { get; set; }
        public string EntreCalle { get; set; }
        public string YCalle { get; set; }
        public string Locacion { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string ZonaDeCarga { get; set; }
        public string TarifaCfe { get; set; }
        public decimal? FactorDeCarga { get; set; }
        public string DivisionTarifaria { get; set; }
        public DateTime FechaInicioDeOperacion { get; set; }
        public decimal FactorDeAhorro { get; set; }
        public string Sistema { get; set; }
        public ulong Dap { get; set; }
        public decimal Dap1 { get; set; }
        public ulong MedicionEnBajaTension { get; set; }
        public decimal MedicionEnBajaTension1 { get; set; }
        public ulong TarifaDeExportaciones { get; set; }
        public string TarifaDeExportaciones1 { get; set; }
        public ulong TarifaSaas { get; set; }
        public string TarifaSaas1 { get; set; }
        public ulong TarifaDePpa { get; set; }
        public string TarifaDePpa1 { get; set; }
        public ulong? HabilitadoDeshabilitado { get; set; }
        public string ZonaHoraria { get; set; }
        public decimal? FactorPerdidasBt { get; set; }
        public decimal? FactorEneReactiva { get; set; }
        public sbyte? GeneracionInSitu { get; set; }
        public sbyte? Bfp { get; set; }
        public decimal? FactorKavar { get; set; }
        public decimal? Bfp1 { get; set; }
        public string Apikey { get; set; }
        public string GatewayId { get; set; }
        public decimal? KwhinstaladosBat { get; set; }
        public decimal? DemandaContratadaCfe { get; set; }
        public string TipoContrato { get; set; }
        public int? DuracionContratoYears { get; set; }
        public DateTime? FechaFirmaContrato { get; set; }
        public string EpcInstalador { get; set; }
        public string FabricanteBateria { get; set; }
        public string Controlador { get; set; }
        public decimal? PenaTerminacion { get; set; }
        public decimal? Capex { get; set; }
        public decimal? Opex { get; set; }
        public decimal? AhorroMinimoMensual { get; set; }
        public decimal? IngresoAnualEsperado { get; set; }
        public int? DiasLimitePago { get; set; }

        public virtual Cliente IdclienteNavigation { get; set; }
        public virtual ICollection<Medidore> Medidores { get; set; }
    }
}
