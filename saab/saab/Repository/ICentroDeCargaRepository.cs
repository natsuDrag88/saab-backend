using System.Collections.Generic;
using saab.Dto.Project;
using saab.Model;

namespace saab.Repository
{
    public interface ICentroDeCargaRepository
    {
        /// <summary>
        /// Get project by id return model  with info general
        /// </summary>
        /// <param name="idProject"></param>
        /// <returns></returns>
        public ProjectRecord GetRecordGeneralProjectByProject(int idProject);
        
        public CentrosDeCarga GetRecordGeneralByRpu(string rpu);

        public decimal? GetCapacityInstalledByStatus(ulong statusCentroCarga);

        public List<ProjectClient> GetProjectsByStatusProjectAndStatusClient(sbyte statusClient,
            ulong statusCentroCarga);

        public List<ProjectData> GetDataProjectsAll();

        public List<CentrosDeCarga> GetProjectsByStatusOptionalFilters(ulong status, int? client = null,
            int? project = null, string rpu = null);

        public List<CentrosDeCarga> GetAll();
    }
}