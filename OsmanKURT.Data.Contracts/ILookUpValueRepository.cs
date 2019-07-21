using OsmanKURT.Business.Entities;
using OsmanKURT.ClientEntites;
using System;

namespace OsmanKURT.Data.Contracts
{
    public interface ILookUpValueRepository : IRepository<LookUpValue>
    {
        string GetValue(GetValueRequest request);
    }
}
