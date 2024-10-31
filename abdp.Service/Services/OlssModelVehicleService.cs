using abdp.Service.IService;
using abdp.Service.Models;

using abdp.Data.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace abdp.Service.Service
{
    public class OlssModelVehicleService : IOlssModelVehicleService
    {
        private IQueryable<OlssModelVehicleServiceModel> Query
        {
            get
            {
                abdpEntities dbContext = new abdpEntities();

                #region 1
                /*
                // USING VIRTUAL OBJECT IN ENTITIES (SEE ENTITIES - ACTUALLY REMARKS ALL VIRTUAL OBJECT) 
                var qry1 = dbContext.tm_olss_model_vehicle.Select(
                    o => new BllOlssModelVehicleModel
                    {
                        tm_olss_model_vehicle_id = o.tm_olss_model_vehicle_id,
                        tm_olss_brand_id = o.tm_olss_brand_id,
                        model_vehicle_name = o.model_vehicle_name,
                        model_vehicle_desc = o.model_vehicle_desc,
                        brand_name = o.tm_olss_brand.brand_name
                    }
                );

                return qry1;
                */
                #endregion 1

                #region 2
                /*
                // USING LINQ
                var qry2 = dbContext.tm_olss_model_vehicle
                    .Where(
                        a => !dbContext.tm_olss_brand.Any(x1 => a.tm_olss_brand_id == x1.tm_olss_brand_id_prev)
                             &&
                             !dbContext.tm_olss_model_vehicle.Any(x2 => a.tm_olss_model_vehicle_id == x2.tm_olss_model_vehicle_id_prev)
                    )
                    .Select(a => new BllOlssModelVehicleModel
                    {
                        tm_olss_model_vehicle_id = a.tm_olss_model_vehicle_id,
                        tm_olss_brand_id = a.tm_olss_brand_id,
                        model_vehicle_name = a.model_vehicle_name,
                        model_vehicle_desc = a.model_vehicle_desc,
                        brand_name =
                            dbContext.tm_olss_brand
                            .Where(
                                b => b.tm_olss_brand_id == a.tm_olss_brand_id
                            )
                            .Select(b => b.brand_name)
                            .FirstOrDefault()
                    }
                );

                return qry2;
                */
                #endregion 2

                #region 3
                // USING NATIVE SQL - DEFINE THE SQL QUERY
                string sqlQuery = @"
                    select
                    a.tm_olss_model_vehicle_id,
                    a.tm_olss_brand_id,
                    a.model_vehicle_name,
                    a.model_vehicle_desc,
                    b.brand_name
                    
                    from tm_olss_model_vehicle a
                    
                    inner join tm_olss_brand b
                    on a.tm_olss_brand_id = b.tm_olss_brand_id
                    
                    where not exists
                    (
                        select 1
                        from abdp.dbo.tm_olss_brand x1
                        where a.tm_olss_brand_id = x1.tm_olss_brand_id_prev
                    )
                    and not exists
                    (
                        select 1
                        from tm_olss_model_vehicle x2
                        where a.tm_olss_model_vehicle_id = x2.tm_olss_model_vehicle_id_prev
                    )
                    ";

                // EXECUTE THE QUERY AND MAP TO THE MODEL
                var qry3 = dbContext.Database.SqlQuery<OlssModelVehicleServiceModel>(sqlQuery).AsQueryable();

                return qry3;
                #endregion 3
            }
        }

        public List<OlssModelVehicleServiceModel> GetList(
            Expression<Func<OlssModelVehicleServiceModel, bool>> where,
            int take,
            int skip,
            Expression<Func<OlssModelVehicleServiceModel, string>> sort,
            string sortDirection
        )
        {
            if (sortDirection.Equals("asc"))
            {
                if (where == null)
                    return Query.OrderBy(sort).Skip(skip).Take(take).ToList();
                else
                    return Query.Where(where).OrderBy(sort).Skip(skip).Take(take).ToList();
            }
            else
            {
                if (where == null)
                    return Query.OrderByDescending(sort).Skip(skip).Take(take).ToList();
                else
                    return Query.Where(where).OrderByDescending(sort).Skip(skip).Take(take).ToList();
            }
        }

        public int TotalRows()
        {
            return Query.Count();
        }

        public int TotalRows(Expression<Func<OlssModelVehicleServiceModel, bool>> where)
        {
            if (where == null)
                return TotalRows();

            return Query.Where(where).Count();
        }

        public int DoSave()
        {
            abdpEntities dbContext = new abdpEntities();
            var tran = dbContext.Database.BeginTransaction();

            try
            {
                dbContext.tm_olss_brand.Add(new tm_olss_brand
                {
                    brand_name = "xxx",
                    brand_desc = "xxx"
                });

                tm_olss_brand item1 = new tm_olss_brand();
                item1.brand_name = "yyy";
                item1.brand_desc = "yyy";
                dbContext.tm_olss_brand.Add(item1);

                tm_olss_brand item2 = new tm_olss_brand();
                item2.brand_name = "zzz";
                item2.brand_desc = "zzz";
                dbContext.tm_olss_brand.Add(item2);

                dbContext.SaveChanges();

                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
            }

            return 1;
        }
    }
}
