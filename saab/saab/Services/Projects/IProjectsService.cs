using System.Collections.Generic;
using saab.Dto;
using saab.Dto.Project;
using saab.Model;
using saab.Util.Enum;

namespace saab.Services.Projects
{
    public interface IProjectsService
    {
        /// <summary>
        ///Service return information general by project  
        /// </summary>
        /// <param name="idProject"></param>
        /// <returns></returns>
        ProjectRecord GetRecordGeneralByProject(int idProject);
        /// <summary>
        /// Service return total projects in status active
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        TotalsProject GetTotalProjectsActives(Status status);

        /// <summary>
        /// Service return total in KWh installed capacity  in all projects 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        TotalsProject GetInstalledCapacityProjects(Status status);

        /// <summary>
        /// Services return projects actives in projects and clients
        /// </summary>
        /// <returns></returns>
        List<ProjectClient> GetDataProjectsClientEnabled();

        /// <summary>
        /// Services return projects filter by status and optional RPU and Client
        /// </summary>
        /// <param name="statusCentroCarga"></param>
        /// <param name="client"></param>
        /// <param name="project"></param>
        /// <param name="rpu"></param>
        /// <returns></returns>
        List<CentrosDeCarga> GetProjectsByStatusOptionalFilters(ulong statusCentroCarga, int? client = null, int? project = null,
            string rpu = null);

        List<ProjectData> GetAllProjects();
    }
}