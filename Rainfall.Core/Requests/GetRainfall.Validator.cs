using FluentValidation;

namespace Rainfall.Core.Requests;

public class GetRainfallValidator : AbstractValidator<GetRainfall>
{
    public GetRainfallValidator()
    {
        RuleFor(x => x.StationId)
            .NotEmpty();

        RuleFor(x => x.Count)
            .Must(y => y >= 1 && y <= 100)
            .WithMessage($"'{{PropertyName}}' must be at least 1 and 100 max.");
    }
}
