using System;
using System.Collections.Generic;

#nullable disable

namespace saab.Model
{
    public partial class Cliente
    {
        public Cliente()
        {
            CentrosDeCargas = new HashSet<CentrosDeCarga>();
        }

        public int Id { get; set; }
        public string Cliente1 { get; set; }
        public string NombreORazonSocial { get; set; }
        public string Alias { get; set; }
        public string Rfc { get; set; }
        public string Calle { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public string Colonia { get; set; }
        public string DelegacionOMunicipio { get; set; }
        public string EntidadFederativa { get; set; }
        public string País { get; set; }
        public string CodigoPostal { get; set; }
        public sbyte HabilitadoDeshabilitado { get; set; }
        public string MetodoPago { get; set; }
        public int? FomatoPago { get; set; }
        public string UsoCfdi { get; set; }
        public int? Cantidad { get; set; }
        public string ClaveUnidad { get; set; }
        public string ClaveProducto { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<CentrosDeCarga> CentrosDeCargas { get; set; }
    }
}
