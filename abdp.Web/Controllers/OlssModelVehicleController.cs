using abdp.Service.IService;
using abdp.Service.Models;

using abdp.Web.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace abdp.Web.Controllers
{
    public class OlssModelVehicleController : Controller
    {
        private readonly IOlssModelVehicleService _service;

        public OlssModelVehicleController(
            IOlssModelVehicleService bllService
        )
        {
            _service = bllService;
        }

        // GET: OlssModelVehicle
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            try
            {
                #region SET FILTER
                Expression<Func<OlssModelVehicleServiceModel, bool>> bllFilter = null;

                if (param.sSearch != null)
                {
                    bllFilter = (
                        o => o.model_vehicle_name.Contains(param.sSearch)
                             ||
                             o.model_vehicle_desc.Contains(param.sSearch)
                             ||
                             o.brand_name.Contains(param.sSearch)
                    );
                }
                #endregion SET FILTER

                #region SET SORTING & ORDERING
                var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
                Expression<Func<OlssModelVehicleServiceModel, string>> bllOrdering = (
                    o => sortColumnIndex == 0 ? o.brand_name :
                         sortColumnIndex == 1 ? o.model_vehicle_name :
                         o.model_vehicle_desc
                );
                var sortDirection = Request["sSortDir_0"]; // ASC / DESC
                #endregion SET SORTING & ORDERING

                List<OlssModelVehicleServiceModel> lstData = _service.GetList(
                    bllFilter,
                    param.iDisplayLength,
                    param.iDisplayStart,
                    bllOrdering,
                    sortDirection
                );

                _service.DoSave();

                return Json(new
                    {
                        param.sEcho,
                        iTotalRecords = _service.TotalRows(),
                        iTotalDisplayRecords = _service.TotalRows(bllFilter),
                        aaData = lstData
                    },
                    JsonRequestBehavior.AllowGet
                );
            }
            catch (Exception ex)
            {
                return View("Error" + "/n" + ex.Message);
            }
        }
    }
}
