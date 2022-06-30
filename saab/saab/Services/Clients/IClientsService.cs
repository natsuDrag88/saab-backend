using System.Collections.Generic;
using saab.Dto.Project;
using saab.Model;
using saab.Util.Enum;

namespace saab.Services.Clients
{
    public interface IClientsService
    {
        public List<ClientLightWeight> GetListLightWeight(Status status);
    }
}