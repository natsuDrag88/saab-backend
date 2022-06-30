using System;
using System.Collections.Generic;
using System.Linq;
using saab.Data;
using saab.Dto.Project;
using saab.Model;
using saab.Util.Enum;

namespace saab.Repository.DBMysql
{
    public class ClientRepository : IClientRepository

    {
        private readonly SaabContext _context;

        public ClientRepository(SaabContext context)
        {
            _context = context;
        }

        public List<ClientLightWeight> GetListLightWeight(Status status)
        {
            var bStatus = Convert.ToSByte(status == Status.activo);
            return _context.Clientes.Where(x => x.HabilitadoDeshabilitado == bStatus).Select(x => new ClientLightWeight
            {
                Id = x.Id,
                Cliente = x.Alias
            }).ToList();
        }
    }
}