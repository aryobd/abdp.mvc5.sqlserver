using abdp.BLL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace abdp.BLL.IServices
{
    public interface IOlssModelVehicleService
    {
        List<OlssModelVehicleServiceModel> GetList(
            Expression<Func<OlssModelVehicleServiceModel, bool>> where,
            int take,
            int skip,
            Expression<Func<OlssModelVehicleServiceModel, string>> sort,
            string sortDirection
        );

        int TotalRows();
        int TotalRows(Expression<Func<OlssModelVehicleServiceModel, bool>> where);

        int DoSave();
    }
}
