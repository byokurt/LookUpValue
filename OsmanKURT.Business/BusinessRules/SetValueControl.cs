using OsmanKURT.ClientEntites;
using OsmanKURT.Common;
using FluentValidation;

namespace OsmanKURT.Business
{
    public class SetValueControl : CustomAbstractValidator<SetValueRequest>
    {
        public SetValueControl()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name boş geçilemez").NotNull().WithMessage("Name boş geçilemez");
            RuleFor(x => x.Value).NotEmpty().WithMessage("Value boş geçilemez").NotNull().WithMessage("Value boş geçilemez");
            RuleFor(x => x.ApplicationName).NotEmpty().WithMessage("ApplicationName boş geçilemez").NotNull().WithMessage("ApplicationName boş geçilemez");
        }
    }
}
