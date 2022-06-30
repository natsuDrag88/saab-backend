using System;
using System.Collections.Generic;

#nullable disable

namespace saab.Model
{
    public partial class Usuario
    {
        public ulong IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string TipoUsuario { get; set; }
        public string NombreEmpresa { get; set; }
        public string CentroCarga { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public string Delegacion { get; set; }
        public string Estado { get; set; }
        public string CodigoPostal { get; set; }
        public string Rfc { get; set; }
        public string Contrasena { get; set; }
        public string CuentaUsuario { get; set; }
        public DateTime? CreadoEl { get; set; }
        public string EmpresaLogo { get; set; }
    }
}
