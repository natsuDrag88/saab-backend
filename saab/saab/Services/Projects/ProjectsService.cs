using System;
using System.Collections.Generic;
using saab.Dto;
using saab.Dto.Project;
using saab.Model;
using saab.Repository;
using saab.Util.Enum;

namespace saab.Services.Projects
{
    public class ProjectsService : IProjectsService
    {
        private readonly ICentroDeCargaRepository _centroDeCargaRepository;

        public ProjectsService(ICentroDeCargaRepository centroDeCargaRepository)
        {
            _centroDeCargaRepository = centroDeCargaRepository;
        }

        public ProjectRecord GetRecordGeneralByProject(int idProject)
        {
            return _centroDeCargaRepository.GetRecordGeneralProjectByProject(idProject);
        }

        public TotalsProject GetTotalProjectsActives(Status status)
        {
            var statusCentroCarga = (status == Status.activo) ? ulong.Parse("1") : ulong.Parse("0");
            var total = _centroDeCargaRepository.GetProjectsByStatusOptionalFilters(statusCentroCarga).Count;
            return new TotalsProject
            {
                unidad = "proyectos",
                total = new decimal?(total)
            };
        }

        public TotalsProject GetInstalledCapacityProjects(Status status)
        {
            var statusCentroCarga = (status == Status.activo) ? ulong.Parse("1") : ulong.Parse("0");
            var total = _centroDeCargaRepository.GetCapacityInstalledByStatus(statusCentroCarga);
            return new TotalsProject
            {
                unidad = "Capacidad instalada",
                total = total ?? 0
            };
        }

        public List<ProjectData> GetAllProjects()
        {
            return _centroDeCargaRepository.GetDataProjectsAll();
        }

        public List<CentrosDeCarga> GetAllContent()
        {
            throw new NotImplementedException();
        }

        public List<ProjectClient> GetDataProjectsClientEnabled()
        {
            return _centroDeCargaRepository.GetProjectsByStatusProjectAndStatusClient(1, 1);
        }

        public List<CentrosDeCarga> GetProjectsByStatusOptionalFilters(ulong statusCentroCarga, int? client = null,
            int? project = null, string rpu = null)
        {
            return _centroDeCargaRepository.GetProjectsByStatusOptionalFilters(status: statusCentroCarga,
                client: client, rpu: rpu, project: project);
        }
    }
}