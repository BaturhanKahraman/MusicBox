using FluentValidation;
using MusicBox.Model.Concrete;

namespace MusicBox.Business.Utility.ValidationRules
{
    public class SearchModelValidator:AbstractValidator<SearchModel>
    {
        public SearchModelValidator()
        {
            RuleFor(x => x.Term)
                .NotNull().WithMessage("Term boş geçilemez")
                .NotEmpty().WithMessage("Term boş geçilemez");
            RuleFor(x=>x.Country)
                .NotNull().WithMessage("Country boş geçilemez")
                .NotEmpty().WithMessage("Country boş geçilemez");
        }
    }
}