using System;
using System.Collections.Generic;

#nullable disable

namespace saab.Model
{
    public partial class Medidore
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string FuenteDeInformacion { get; set; }
        public int CentroDeCarga { get; set; }
        public string Provedor { get; set; }
        public sbyte HabilitadoDeshabilitado { get; set; }
        public string Timezone { get; set; }
        public int? UdisMeterId { get; set; }
        public string UdisDeviceId { get; set; }
        public string Channel { get; set; }
        public string RelacionDeTransformacion { get; set; }

        public virtual CentrosDeCarga CentroDeCargaNavigation { get; set; }
    }
}
