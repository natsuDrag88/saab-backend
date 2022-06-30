using System.Collections.Generic;
using saab.Dto.Project;
using saab.Model;
using saab.Repository;
using saab.Repository.DBMysql;
using saab.Util.Enum;

namespace saab.Services.Clients
{
    public class ClientsService : IClientsService
    {
        private readonly IClientRepository _clientRepository;

        public ClientsService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public List<ClientLightWeight> GetListLightWeight(Status status)
        {
            return _clientRepository.GetListLightWeight(status);
        }
    }
}