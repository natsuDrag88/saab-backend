using System;
using System.Collections.Generic;
using saab.Data;
using saab.Dto.Alerts;
using saab.Repository;
using saab.Util.Enum;
using saab.Util.Project;

namespace saab.Services.Alerts
{
    public interface IDetailAlertService
    {
        AlertDetail GetAlertDetail(string period);
        List<AlertGeneralModel> GetListAlertGeneral(string period);
    }
}