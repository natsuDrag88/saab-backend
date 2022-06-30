using System.Collections.Generic;
using saab.Dto.Project;
using saab.Model;
using saab.Util.Enum;

namespace saab.Repository
{
    public interface IClientRepository
    {
        public List<ClientLightWeight> GetListLightWeight(Status status);
    }
}