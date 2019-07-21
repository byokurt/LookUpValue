using OsmanKURT.Business.Entities;
using OsmanKURT.ClientEntites;
using OsmanKURT.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsmanKURT.Data
{
    public class LookUpValueRepository : Repository<LookUpValue>, ILookUpValueRepository
    {
        public LookUpValueRepository(MainContext context) : base(context)
        {

        }

        public string GetValue(GetValueRequest request)
        {
            var response = Db.SetLookUpValue.Where(w => w.Name == request.Name && w.ApplicationName == request.ApplicationName && w.IsActive == true).Select(s => s.Value).SingleOrDefault();
            return response;
        }
    }
}
