using OsmanKURT.ClientEntites;
using OsmanKURT.Common;
using FluentValidation;

namespace OsmanKURT.Business.BusinessRules
{
    public class AccountLoginControl : CustomAbstractValidator<LoginUserDto>
    {
        public AccountLoginControl()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName boş geçilemez").NotNull().WithMessage("UserName boş geçilemez");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password boş geçilemez").NotNull().WithMessage("Password boş geçilemez");
        }
    }
}
