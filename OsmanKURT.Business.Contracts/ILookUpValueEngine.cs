using OsmanKURT.ClientEntites;
using OsmanKURT.Common;
using System;
using System.Globalization;

namespace OsmanKURT.Business.Contracts
{
    public interface ILookUpValueEngine
    {
        string GetValue(GetValueRequest request);
        bool SetValue(SetValueRequest request);
    }
}
