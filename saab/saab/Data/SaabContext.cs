using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using saab.Model;

#nullable disable

namespace saab.Data
{
    public partial class SaabContext : DbContext
    {
        public SaabContext()
        {
        }

        public SaabContext(DbContextOptions<SaabContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CanalesEtbconsumoCfe> CanalesEtbconsumoCves { get; set; }
        public virtual DbSet<CanalesEtbpanelesSolare> CanalesEtbpanelesSolares { get; set; }
        public virtual DbSet<CanalesEtbsinBaterium> CanalesEtbsinBateria { get; set; }
        public virtual DbSet<CargaDescargaBaterium> CargaDescargaBateria { get; set; }
        public virtual DbSet<CentrosDeCarga> CentrosDeCargas { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<ComparativaHistorico> ComparativaHistoricos { get; set; }
        public virtual DbSet<CuadroTarifario> CuadroTarifarios { get; set; }
        public virtual DbSet<DesglosePagoHistorico> DesglosePagoHistoricos { get; set; }
        public virtual DbSet<DiasFeriado> DiasFeriados { get; set; }
        public virtual DbSet<FactoresDeCarga> FactoresDeCargas { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }
        public virtual DbSet<MedicionesEtb> MedicionesBesses { get; set; }
        public virtual DbSet<MedicionesEtb> MedicionesEtbs { get; set; }
        public virtual DbSet<MedicionesOperati> MedicionesOperatis { get; set; }
        public virtual DbSet<Medidore> Medidores { get; set; }
        public virtual DbSet<ParametrosHistorico> ParametrosHistoricos { get; set; }
        public virtual DbSet<PeriodosBipCfe> PeriodosBipCves { get; set; }
        public virtual DbSet<PeriodosCfe> PeriodosCves { get; set; }
        public virtual DbSet<ProporcionConsumoBloqueHorario> ProporcionConsumoBloqueHorarios { get; set; }
        public virtual DbSet<TarifaMediaNacional> TarifaMediaNacionals { get; set; }
        public virtual DbSet<TemporadasCfe> TemporadasCves { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<XmlCfe> XmlCves { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            modelBuilder.Entity<CanalesEtbconsumoCfe>(entity =>
            {
                entity.HasKey(e => new { e.Idmedidor, e.FechaUnix })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("CanalesETBConsumoCFE");

                entity.UseCollation("utf8_spanish_ci");

                entity.Property(e => e.Idmedidor)
                    .HasColumnType("int(11)")
                    .HasColumnName("IDMedidor");

                entity.Property(e => e.FechaUnix).HasColumnType("int(11)");

                entity.Property(e => e.FechaNormal).HasColumnType("timestamp");

                entity.Property(e => e.MeteredDemand)
                    .HasPrecision(18, 6)
                    .HasColumnName("metered_demand");

                entity.Property(e => e.Offset)
                    .HasColumnType("int(11)")
                    .HasColumnName("offset");
            });

            modelBuilder.Entity<CanalesEtbpanelesSolare>(entity =>
            {
                entity.HasKey(e => new { e.Idmedidor, e.FechaUnix })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("CanalesETBPanelesSolares");

                entity.UseCollation("utf8_spanish_ci");

                entity.Property(e => e.Idmedidor)
                    .HasColumnType("int(11)")
                    .HasColumnName("IDMedidor");

                entity.Property(e => e.FechaUnix).HasColumnType("int(11)");

                entity.Property(e => e.FechaNormal).HasColumnType("timestamp");

                entity.Property(e => e.MeterPvPower)
                    .HasPrecision(18, 6)
                    .HasColumnName("meter_pv_power");

                entity.Property(e => e.Offset)
                    .HasColumnType("int(11)")
                    .HasColumnName("offset");
            });

            modelBuilder.Entity<CanalesEtbsinBaterium>(entity =>
            {
                entity.HasKey(e => new { e.IdcentroDeCarga, e.FechaUnix })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("CanalesETBSinBateria");

                entity.UseCollation("utf8_spanish_ci");

                entity.Property(e => e.IdcentroDeCarga)
                    .HasColumnType("int(11)")
                    .HasColumnName("IDCentroDeCarga");

                entity.Property(e => e.FechaUnix).HasColumnType("int(11)");

                entity.Property(e => e.FechaNormal).HasColumnType("timestamp");

                entity.Property(e => e.MeterSiteDemand)
                    .HasPrecision(18, 6)
                    .HasColumnName("meter_site_demand");

                entity.Property(e => e.MeterSiteNetPvDemand)
                    .HasPrecision(18, 6)
                    .HasColumnName("meter_site_net_pv_demand");

                entity.Property(e => e.Offset).HasColumnType("int(11)");
            });

            modelBuilder.Entity<CargaDescargaBaterium>(entity =>
            {
                entity.HasKey(e => new { e.Idmedidor, e.FechaUnix })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.UseCollation("utf8_spanish_ci");

                entity.Property(e => e.Idmedidor)
                    .HasColumnType("int(11)")
                    .HasColumnName("IDMedidor");

                entity.Property(e => e.FechaUnix).HasColumnType("int(11)");

                entity.Property(e => e.BatterySoc)
                    .HasPrecision(18, 6)
                    .HasColumnName("batterySoc");

                entity.Property(e => e.EssPower)
                    .HasPrecision(18, 6)
                    .HasColumnName("ess_power");

                entity.Property(e => e.FechaNormal).HasColumnType("timestamp");

                entity.Property(e => e.Offset).HasColumnType("int(11)");
            });

            modelBuilder.Entity<CentrosDeCarga>(entity =>
            {
                entity.ToTable("CentrosDeCarga");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.Idcliente, "FK_Cliente_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.AhorroMinimoMensual).HasPrecision(11, 6);

                entity.Property(e => e.Apikey)
                    .HasMaxLength(100)
                    .HasColumnName("apikey");

                entity.Property(e => e.Bfp)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("%BFP");

                entity.Property(e => e.Bfp1)
                    .HasPrecision(11, 6)
                    .HasColumnName("BFP");

                entity.Property(e => e.Calle)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.Capex).HasPrecision(11, 6);

                entity.Property(e => e.CodigoPostal)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Codigo Postal");

                entity.Property(e => e.Colonia)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Controlador).HasMaxLength(25);

                entity.Property(e => e.Dap)
                    .HasColumnType("bit(1)")
                    .HasColumnName("DAP");

                entity.Property(e => e.Dap1)
                    .HasPrecision(11, 6)
                    .HasColumnName("%DAP");

                entity.Property(e => e.DelegacionOMunicipio)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("Delegacion o Municipio");

                entity.Property(e => e.DemandaContratadaCfe)
                    .HasPrecision(11, 6)
                    .HasColumnName("DemandaContratadaCFE");

                entity.Property(e => e.DiasLimitePago).HasColumnType("int(11)");

                entity.Property(e => e.DivisionTarifaria)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("Division Tarifaria");

                entity.Property(e => e.DuracionContratoYears).HasColumnType("int(11)");

                entity.Property(e => e.EntidadFederativa)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("Entidad Federativa");

                entity.Property(e => e.EntreCalle)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("Entre Calle");

                entity.Property(e => e.EpcInstalador)
                    .HasMaxLength(30)
                    .HasColumnName("EPC_Instalador");

                entity.Property(e => e.FabricanteBateria).HasMaxLength(35);

                entity.Property(e => e.FactorDeAhorro)
                    .HasPrecision(11, 6)
                    .HasColumnName("Factor De Ahorro");

                entity.Property(e => e.FactorDeCarga)
                    .HasPrecision(11, 6)
                    .HasColumnName("Factor De Carga");

                entity.Property(e => e.FactorEneReactiva).HasPrecision(11, 6);

                entity.Property(e => e.FactorKavar)
                    .HasPrecision(11, 6)
                    .HasColumnName("FactorKAVAR");

                entity.Property(e => e.FactorPerdidasBt)
                    .HasPrecision(11, 6)
                    .HasColumnName("FactorPerdidasBT");

                entity.Property(e => e.FechaFirmaContrato).HasColumnType("date");

                entity.Property(e => e.FechaInicioDeOperacion)
                    .HasColumnType("date")
                    .HasColumnName("Fecha Inicio De Operacion");

                entity.Property(e => e.GatewayId)
                    .HasMaxLength(100)
                    .HasColumnName("gatewayId");

                entity.Property(e => e.GeneracionInSitu).HasColumnType("tinyint(4)");

                entity.Property(e => e.HabilitadoDeshabilitado)
                    .HasColumnType("bit(1)")
                    .HasColumnName("Habilitado_Deshabilitado");

                entity.Property(e => e.Idcliente)
                    .HasColumnType("int(11)")
                    .HasColumnName("IDCliente");

                entity.Property(e => e.IngresoAnualEsperado).HasPrecision(11, 6);

                entity.Property(e => e.KwhinstaladosBat)
                    .HasPrecision(11, 6)
                    .HasColumnName("KWHInstalados_Bat");

                entity.Property(e => e.Latitud).HasPrecision(11, 6);

                entity.Property(e => e.Locacion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Longitud).HasPrecision(11, 6);

                entity.Property(e => e.MedicionEnBajaTension)
                    .HasColumnType("bit(1)")
                    .HasColumnName("Medicion En Baja Tension");

                entity.Property(e => e.MedicionEnBajaTension1)
                    .HasPrecision(11, 6)
                    .HasColumnName("%Medicion En Baja Tension");

                entity.Property(e => e.NumeroExterior)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Numero Exterior");

                entity.Property(e => e.NumeroInterior)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Numero Interior");

                entity.Property(e => e.Opex).HasPrecision(11, 6);

                entity.Property(e => e.Pais)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PenaTerminacion).HasPrecision(11, 6);

                entity.Property(e => e.Rmu)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("RMU");

                entity.Property(e => e.Rpu)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("RPU");

                entity.Property(e => e.Sistema)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.TarifaCfe)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Tarifa CFE");

                entity.Property(e => e.TarifaDeExportaciones)
                    .HasColumnType("bit(1)")
                    .HasColumnName("Tarifa De Exportaciones");

                entity.Property(e => e.TarifaDeExportaciones1)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("%Tarifa De Exportaciones");

                entity.Property(e => e.TarifaDePpa)
                    .HasColumnType("bit(1)")
                    .HasColumnName("Tarifa De PPA");

                entity.Property(e => e.TarifaDePpa1)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("%Tarifa De PPA");

                entity.Property(e => e.TarifaSaas)
                    .HasColumnType("bit(1)")
                    .HasColumnName("Tarifa SAAS");

                entity.Property(e => e.TarifaSaas1)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("%Tarifa SAAS");

                entity.Property(e => e.TipoContrato).HasMaxLength(30);

                entity.Property(e => e.YCalle)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("Y Calle");

                entity.Property(e => e.ZonaDeCarga)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("Zona De Carga");

                entity.Property(e => e.ZonaHoraria)
                    .HasMaxLength(100)
                    .HasColumnName("Zona_Horaria");

                entity.HasOne(d => d.IdclienteNavigation)
                    .WithMany(p => p.CentrosDeCargas)
                    .HasForeignKey(d => d.Idcliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cliente");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.Alias)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Calle)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.Cantidad).HasColumnType("int(11)");

                entity.Property(e => e.ClaveProducto).HasMaxLength(20);

                entity.Property(e => e.ClaveUnidad).HasMaxLength(10);

                entity.Property(e => e.Cliente1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("Cliente");

                entity.Property(e => e.CodigoPostal)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Codigo Postal");

                entity.Property(e => e.Colonia)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.DelegacionOMunicipio)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("Delegacion o Municipio");

                entity.Property(e => e.Descripcion).HasMaxLength(150);

                entity.Property(e => e.EntidadFederativa)
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnName("Entidad Federativa");

                entity.Property(e => e.FomatoPago).HasColumnType("int(11)");

                entity.Property(e => e.HabilitadoDeshabilitado)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("Habilitado_Deshabilitado");

                entity.Property(e => e.MetodoPago).HasMaxLength(10);

                entity.Property(e => e.NombreORazonSocial)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("Nombre o Razon Social");

                entity.Property(e => e.NumeroExterior)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Numero Exterior");

                entity.Property(e => e.NumeroInterior)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Numero Interior");

                entity.Property(e => e.País)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Rfc)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("RFC");

                entity.Property(e => e.UsoCfdi)
                    .HasMaxLength(10)
                    .HasColumnName("UsoCFDI");
            });

            modelBuilder.Entity<ComparativaHistorico>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.CentroDeCarga, e.Mes, e.AlgoritmoJerarquia })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

                entity.ToTable("Comparativa_Historico");

                entity.UseCollation("utf8_spanish_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.CentroDeCarga).HasColumnType("int(50)");

                entity.Property(e => e.Mes).HasMaxLength(15);

                entity.Property(e => e.AlgoritmoJerarquia).HasColumnType("int(11)");

                entity.Property(e => e.AhorroBruto).HasPrecision(18, 2);

                entity.Property(e => e.Cargo).HasMaxLength(100);

                entity.Property(e => e.CargoConSaas).HasPrecision(18, 2);

                entity.Property(e => e.CargoSinSaaS).HasPrecision(18, 2);

                entity.Property(e => e.FechaActualizacion).HasColumnType("date");

                entity.Property(e => e.MesTarifa).HasMaxLength(15);

                entity.Property(e => e.Tarifa).HasPrecision(18, 4);
            });

            modelBuilder.Entity<CuadroTarifario>(entity =>
            {
                entity.HasKey(e => new { e.Tarifa, e.Segmento, e.Division, e.IntHorario, e.Periodo })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0, 0 });

                entity.ToTable("Cuadro_Tarifario");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Tarifa).HasMaxLength(50);

                entity.Property(e => e.Segmento).HasMaxLength(50);

                entity.Property(e => e.Division).HasMaxLength(50);

                entity.Property(e => e.IntHorario)
                    .HasMaxLength(50)
                    .HasColumnName("Int_Horario");

                entity.Property(e => e.Periodo).HasMaxLength(50);

                entity.Property(e => e.CargosTrifarios)
                    .HasPrecision(18, 10)
                    .HasColumnName("Cargos Trifarios");

                entity.Property(e => e.Concepto).HasMaxLength(50);

                entity.Property(e => e.Unidades).HasMaxLength(50);
            });

            modelBuilder.Entity<DesglosePagoHistorico>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.CentroDeCarga, e.Mes, e.AlgoritmoJerarquia })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

                entity.ToTable("DesglosePago_Historico");

                entity.UseCollation("utf8_spanish_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.CentroDeCarga).HasColumnType("int(50)");

                entity.Property(e => e.Mes).HasMaxLength(15);

                entity.Property(e => e.AlgoritmoJerarquia).HasColumnType("int(11)");

                entity.Property(e => e.AhorroBruto).HasPrecision(18, 2);

                entity.Property(e => e.AhorroNeto).HasPrecision(18, 2);

                entity.Property(e => e.AjusteMesAnterior).HasPrecision(18, 2);

                entity.Property(e => e.FechaActualizacion).HasColumnType("date");

                entity.Property(e => e.Iva)
                    .HasPrecision(18, 2)
                    .HasColumnName("IVA");

                entity.Property(e => e.PagoFrv)
                    .HasPrecision(18, 2)
                    .HasColumnName("PagoFRV");

                entity.Property(e => e.SubTotal).HasPrecision(18, 2);

                entity.Property(e => e.Total).HasPrecision(18, 2);
            });

            modelBuilder.Entity<DiasFeriado>(entity =>
            {
                entity.ToTable("Dias_Feriados");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.Detalle).HasMaxLength(100);

                entity.Property(e => e.Fecha).HasColumnType("date");
            });

            modelBuilder.Entity<FactoresDeCarga>(entity =>
            {
                entity.HasKey(e => new { e.Periodo, e.GrupoTarifario })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("Factores_De_Carga");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Periodo).HasMaxLength(50);

                entity.Property(e => e.GrupoTarifario)
                    .HasMaxLength(50)
                    .HasColumnName("Grupo Tarifario");

                entity.Property(e => e.FactorDeCarga)
                    .HasPrecision(18, 2)
                    .HasColumnName("Factor de Carga");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.IdDeFactura)
                    .HasName("PRIMARY");

                entity.UseCollation("utf8_spanish_ci");

                entity.Property(e => e.IdDeFactura)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id_de_factura");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.AhorroBruto).HasPrecision(18, 2);

                entity.Property(e => e.AhorroNeto).HasPrecision(18, 2);

                entity.Property(e => e.AjusteMesAnterior).HasPrecision(18, 2);

                entity.Property(e => e.AlgoritmoJerarquia)
                    .HasColumnType("int(11)")
                    .HasColumnName("algoritmo_jerarquia");

                entity.Property(e => e.CargoConSaas).HasPrecision(18, 2);

                entity.Property(e => e.CargoSinSaas).HasPrecision(18, 2);

                entity.Property(e => e.FacturaConsolidada).HasColumnName("facturaConsolidada");

                entity.Property(e => e.FacturaPdf).HasColumnName("factura_pdf");

                entity.Property(e => e.FacturaXml)
                    .HasColumnType("blob")
                    .HasColumnName("factura_xml");

                entity.Property(e => e.FechaDeCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_de_creacion");

                entity.Property(e => e.Filename)
                    .HasMaxLength(100)
                    .HasColumnName("filename");

                entity.Property(e => e.Folio)
                    .HasMaxLength(45)
                    .HasColumnName("folio");

                entity.Property(e => e.IdCentroDeCarga)
                    .HasMaxLength(45)
                    .HasColumnName("id_centro_de_carga");

                entity.Property(e => e.Iva).HasPrecision(18, 2);

                entity.Property(e => e.MontoTotal).HasPrecision(18, 2);

                entity.Property(e => e.PagoAfrv)
                    .HasPrecision(18, 2)
                    .HasColumnName("PagoAFrv");

                entity.Property(e => e.Periodo)
                    .HasMaxLength(100)
                    .HasColumnName("periodo");

                entity.Property(e => e.Serie)
                    .HasMaxLength(45)
                    .HasColumnName("serie");

                entity.Property(e => e.Subtotal)
                    .HasPrecision(18, 2)
                    .HasColumnName("subtotal");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(45)
                    .HasColumnName("uuid");
            });

            modelBuilder.Entity<MedicionesBess>(entity =>
            {
                entity.HasKey(e => new { e.Fecha, e.Idmedidor, e.Offset })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("Mediciones_BESS");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Fecha)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Idmedidor)
                    .HasColumnType("int(11)")
                    .HasColumnName("IDMedidor");

                entity.Property(e => e.Offset).HasColumnType("int(11)");

                entity.Property(e => e.KVarhQ1)
                    .HasPrecision(18, 6)
                    .HasColumnName("kVARh Q1");

                entity.Property(e => e.KVarhQ2)
                    .HasPrecision(18, 6)
                    .HasColumnName("kVARh Q2");

                entity.Property(e => e.KVarhQ3)
                    .HasPrecision(18, 6)
                    .HasColumnName("kVARh Q3");

                entity.Property(e => e.KVarhQ4)
                    .HasPrecision(18, 6)
                    .HasColumnName("kVARh Q4");

                entity.Property(e => e.KWhE)
                    .HasPrecision(18, 6)
                    .HasColumnName("kWh E");

                entity.Property(e => e.KWhR)
                    .HasPrecision(18, 6)
                    .HasColumnName("kWh R");
            });

            modelBuilder.Entity<MedicionesEtb>(entity =>
            {
                entity.HasKey(e => new { e.Fecha, e.Idmedidor, e.Offset })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("Mediciones_ETB");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Fecha)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Idmedidor)
                    .HasColumnType("int(11)")
                    .HasColumnName("IDMedidor");

                entity.Property(e => e.Offset).HasColumnType("int(11)");

                entity.Property(e => e.KVarhQ1)
                    .HasPrecision(18, 6)
                    .HasColumnName("kVARh Q1");

                entity.Property(e => e.KVarhQ2)
                    .HasPrecision(18, 6)
                    .HasColumnName("kVARh Q2");

                entity.Property(e => e.KVarhQ3)
                    .HasPrecision(18, 6)
                    .HasColumnName("kVARh Q3");

                entity.Property(e => e.KVarhQ4)
                    .HasPrecision(18, 6)
                    .HasColumnName("kVARh Q4");

                entity.Property(e => e.KWhE)
                    .HasPrecision(18, 6)
                    .HasColumnName("kWh E");

                entity.Property(e => e.KWhR)
                    .HasPrecision(18, 6)
                    .HasColumnName("kWh R");
            });

            modelBuilder.Entity<MedicionesOperati>(entity =>
            {
                entity.HasKey(e => new { e.Fecha, e.Idmedidor, e.Offset })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("Mediciones_Operati");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Fecha)
                    .HasColumnType("timestamp");

                entity.Property(e => e.Idmedidor)
                    .HasColumnType("int(11)")
                    .HasColumnName("IDMedidor");

                entity.Property(e => e.Offset).HasColumnType("int(11)");

                entity.Property(e => e.KVarhQ1)
                    .HasPrecision(18, 6)
                    .HasColumnName("kVARh Q1");

                entity.Property(e => e.KVarhQ2)
                    .HasPrecision(18, 6)
                    .HasColumnName("kVARh Q2");

                entity.Property(e => e.KVarhQ3)
                    .HasPrecision(18, 6)
                    .HasColumnName("kVARh Q3");

                entity.Property(e => e.KVarhQ4)
                    .HasPrecision(18, 6)
                    .HasColumnName("kVARh Q4");

                entity.Property(e => e.KWhE)
                    .HasPrecision(18, 6)
                    .HasColumnName("kWh E");

                entity.Property(e => e.KWhR)
                    .HasPrecision(18, 6)
                    .HasColumnName("kWh R");
            });

            modelBuilder.Entity<Medidore>(entity =>
            {
                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.CentroDeCarga, "FK_CentroDeCarga_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.CentroDeCarga)
                    .HasColumnType("int(11)")
                    .HasColumnName("Centro De Carga");

                entity.Property(e => e.Channel)
                    .HasMaxLength(100)
                    .HasColumnName("channel");

                entity.Property(e => e.FuenteDeInformacion)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Fuente De Informacion");

                entity.Property(e => e.HabilitadoDeshabilitado)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("Habilitado_Deshabilitado");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Provedor)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RelacionDeTransformacion)
                    .HasMaxLength(100)
                    .HasColumnName("relacion_de_transformacion");

                entity.Property(e => e.Timezone).HasMaxLength(100);

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.UdisDeviceId)
                    .HasMaxLength(20)
                    .HasColumnName("UDIS_DEVICE_ID");

                entity.Property(e => e.UdisMeterId)
                    .HasColumnType("int(11)")
                    .HasColumnName("UDIS_METER_ID");

                entity.HasOne(d => d.CentroDeCargaNavigation)
                    .WithMany(p => p.Medidores)
                    .HasForeignKey(d => d.CentroDeCarga)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CentroDeCarga");
            });

            modelBuilder.Entity<ParametrosHistorico>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.CentroDeCarga, e.AlgoritmoJerarquia, e.Mes })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

                entity.ToTable("Parametros_Historico");

                entity.UseCollation("utf8_spanish_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.CentroDeCarga).HasColumnType("int(50)");

                entity.Property(e => e.AlgoritmoJerarquia).HasColumnType("int(11)");

                entity.Property(e => e.Mes).HasMaxLength(15);

                entity.Property(e => e.CantidadEvitada).HasPrecision(18, 1);

                entity.Property(e => e.Concepto).HasMaxLength(45);

                entity.Property(e => e.ConsumoConSaaS).HasPrecision(18, 1);

                entity.Property(e => e.ConsumoSinSaaS).HasPrecision(18, 1);

                entity.Property(e => e.FechaActualizacion).HasColumnType("date");

                entity.Property(e => e.MesTarifa).HasMaxLength(15);
            });

            modelBuilder.Entity<PeriodosBipCfe>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PeriodosBIP_CFE");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Anio)
                    .HasColumnType("int(4)")
                    .HasColumnName("ANIO");

                entity.Property(e => e.Dia)
                    .HasColumnType("int(2)")
                    .HasColumnName("DIA");

                entity.Property(e => e.DiaSemana)
                    .HasColumnType("int(2)")
                    .HasColumnName("DIA_SEMANA");

                entity.Property(e => e.Estacion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("ESTACION");

                entity.Property(e => e.Fecha)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("FECHA")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Hora)
                    .HasColumnType("int(2)")
                    .HasColumnName("HORA");

                entity.Property(e => e.Mes)
                    .HasColumnType("int(2)")
                    .HasColumnName("MES");

                entity.Property(e => e.Min)
                    .HasColumnType("int(2)")
                    .HasColumnName("MIN");

                entity.Property(e => e.PeriodoCfe)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("PERIODO_CFE");

                entity.Property(e => e.Sistema)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("SISTEMA");

                entity.Property(e => e.TarifaCfe)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("TARIFA_CFE");
            });

            modelBuilder.Entity<PeriodosCfe>(entity =>
            {
                entity.HasKey(e => e.IdPeriodosCfe)
                    .HasName("PRIMARY");

                entity.ToTable("Periodos_CFE");

                entity.HasComment("Tabla para determinar que tipo de periodo (Base, Intermedio, Punta) se le asigna a un cincominutal ")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.IdPeriodosCfe)
                    .HasColumnType("int(11)")
                    .HasColumnName("idPeriodos_CFE");

                entity.Property(e => e.Año)
                    .HasColumnType("datetime")
                    .HasComment("Fecha a la que corresponde el registro de los periodos CFE ");

                entity.Property(e => e.DiaDeLaSemana).HasMaxLength(45);

                entity.Property(e => e.Estacion).HasMaxLength(15);

                entity.Property(e => e.Horario).HasMaxLength(50);

                entity.Property(e => e.Sistema)
                    .HasMaxLength(3)
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Tarifa)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Tipo).HasMaxLength(45);
            });

            modelBuilder.Entity<ProporcionConsumoBloqueHorario>(entity =>
            {
                entity.HasKey(e => new { e.Tarifa, e.Sistema, e.IntHorario, e.Periodo })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

                entity.ToTable("Proporcion_Consumo_Bloque_Horario");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Tarifa)
                    .HasMaxLength(50)
                    .HasColumnName("tarifa");

                entity.Property(e => e.Sistema)
                    .HasMaxLength(3)
                    .HasColumnName("sistema");

                entity.Property(e => e.IntHorario)
                    .HasMaxLength(2)
                    .HasColumnName("int_horario");

                entity.Property(e => e.Periodo)
                    .HasMaxLength(50)
                    .HasColumnName("periodo");

                entity.Property(e => e.Consumo)
                    .HasPrecision(11, 6)
                    .HasColumnName("consumo");
            });

            modelBuilder.Entity<TarifaMediaNacional>(entity =>
            {
                entity.HasKey(e => new { e.Tarifa, e.Periodo })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("Tarifa_Media_Nacional");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Tarifa)
                    .HasMaxLength(50)
                    .HasColumnName("tarifa");

                entity.Property(e => e.Periodo)
                    .HasMaxLength(50)
                    .HasColumnName("periodo");

                entity.Property(e => e.TarifaMedia)
                    .HasPrecision(18, 2)
                    .HasColumnName("tarifa_media");
            });

            modelBuilder.Entity<TemporadasCfe>(entity =>
            {
                entity.ToTable("Temporadas_CFE");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.Año)
                    .HasMaxLength(45)
                    .HasComment("Version de la temporada, cada registro aplica para un año en especifico el cual determina esta columna AÑO ");

                entity.Property(e => e.Periodo).HasMaxLength(50);

                entity.Property(e => e.Sistema)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.Tarifa)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Temporada)
                    .IsRequired()
                    .HasMaxLength(11);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PRIMARY");

                entity.UseCollation("utf8_spanish_ci");

                entity.Property(e => e.IdUsuario)
                    .HasColumnType("bigint(20) unsigned")
                    .HasColumnName("id_usuario");

                entity.Property(e => e.Calle)
                    .HasMaxLength(50)
                    .HasColumnName("calle");

                entity.Property(e => e.CentroCarga)
                    .HasMaxLength(100)
                    .HasColumnName("centro_carga");

                entity.Property(e => e.CodigoPostal)
                    .HasMaxLength(50)
                    .HasColumnName("codigo_postal");

                entity.Property(e => e.Colonia)
                    .HasMaxLength(50)
                    .HasColumnName("colonia");

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(50)
                    .HasColumnName("contrasena");

                entity.Property(e => e.CreadoEl)
                    .HasColumnType("datetime")
                    .HasColumnName("creado_el");

                entity.Property(e => e.CuentaUsuario)
                    .HasMaxLength(100)
                    .HasColumnName("cuenta_usuario");

                entity.Property(e => e.Delegacion)
                    .HasMaxLength(50)
                    .HasColumnName("delegacion");

                entity.Property(e => e.EmpresaLogo).HasColumnName("empresaLogo");

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .HasColumnName("estado");

                entity.Property(e => e.NombreEmpresa)
                    .HasMaxLength(100)
                    .HasColumnName("nombre_empresa");

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(100)
                    .HasColumnName("nombre_usuario");

                entity.Property(e => e.Rfc)
                    .HasMaxLength(50)
                    .HasColumnName("rfc");

                entity.Property(e => e.TipoUsuario)
                    .HasMaxLength(100)
                    .HasColumnName("tipo_usuario");
            });

            modelBuilder.Entity<XmlCfe>(entity =>
            {
                entity.HasKey(e => new { e.Rpu, e.Periodo })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("XML_CFE");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Rpu)
                    .HasMaxLength(50)
                    .HasColumnName("RPU");

                entity.Property(e => e.Periodo).HasMaxLength(15);

                entity.Property(e => e.CargoCapacidad).HasPrecision(18, 2);

                entity.Property(e => e.CargoCenace)
                    .HasPrecision(18, 2)
                    .HasColumnName("CargoCENACE");

                entity.Property(e => e.CargoDistribucion).HasPrecision(18, 2);

                entity.Property(e => e.CargoGeneracionB).HasPrecision(18, 2);

                entity.Property(e => e.CargoGeneracionI).HasPrecision(18, 2);

                entity.Property(e => e.CargoGeneracionP).HasPrecision(18, 2);

                entity.Property(e => e.CargoGeneracionSp)
                    .HasPrecision(18, 2)
                    .HasColumnName("CargoGeneracionSP");

                entity.Property(e => e.CargoScnMem)
                    .HasPrecision(18, 2)
                    .HasColumnName("CargoSCnMEM");

                entity.Property(e => e.CargoSuministro).HasPrecision(18, 2);

                entity.Property(e => e.CargoTransmision).HasPrecision(18, 2);

                entity.Property(e => e.FactorPotencia)
                    .HasPrecision(18, 2)
                    .HasColumnName("Factor_Potencia");

                entity.Property(e => e.Importe2Bt)
                    .HasPrecision(18, 2)
                    .HasColumnName("Importe2%BT");

                entity.Property(e => e.ImporteBonifFactorPotencia)
                    .HasPrecision(18, 2)
                    .HasColumnName("ImporteBonif_FactorPotencia");

                entity.Property(e => e.ImporteCargoFijo).HasPrecision(18, 2);

                entity.Property(e => e.ImporteDap)
                    .HasPrecision(18, 2)
                    .HasColumnName("Importe_DAP");

                entity.Property(e => e.ImporteEnergia).HasPrecision(18, 2);

                entity.Property(e => e.Iva)
                    .HasPrecision(18, 2)
                    .HasColumnName("IVA");

                entity.Property(e => e.KVarh)
                    .HasPrecision(18)
                    .HasColumnName("kVArh");

                entity.Property(e => e.KWBaseDemanda)
                    .HasPrecision(18)
                    .HasColumnName("kW_Base Demanda");

                entity.Property(e => e.KWIntermediaDemanda)
                    .HasPrecision(18)
                    .HasColumnName("kW_Intermedia Demanda");

                entity.Property(e => e.KWMaxDemanda)
                    .HasPrecision(18)
                    .HasColumnName("kW_Max Demanda");

                entity.Property(e => e.KWPuntaDemanda)
                    .HasPrecision(18)
                    .HasColumnName("kW_Punta Demanda");

                entity.Property(e => e.KWSemipuntaDemanda)
                    .HasPrecision(18)
                    .HasColumnName("kW_Semipunta Demanda");

                entity.Property(e => e.KWhBaseConsumo)
                    .HasPrecision(18)
                    .HasColumnName("kWh_Base Consumo");

                entity.Property(e => e.KWhIntermediaConsumo)
                    .HasPrecision(18)
                    .HasColumnName("kWh_Intermedia Consumo");

                entity.Property(e => e.KWhPuntaConsumo)
                    .HasPrecision(18)
                    .HasColumnName(" kWh_Punta Consumo");

                entity.Property(e => e.KWhSemipuntaConsumo)
                    .HasPrecision(18)
                    .HasColumnName(" kWh_Semipunta Consumo");

                entity.Property(e => e.KwhTotal)
                    .HasPrecision(18)
                    .HasColumnName("KWh_Total");

                entity.Property(e => e.Rfc)
                    .HasMaxLength(50)
                    .HasColumnName("RFC");

                entity.Property(e => e.Subtotal).HasPrecision(18, 2);

                entity.Property(e => e.TotalFacturado).HasPrecision(18, 2);

                entity.Property(e => e.UuidCfe)
                    .HasMaxLength(100)
                    .HasColumnName("UUID_CFE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
