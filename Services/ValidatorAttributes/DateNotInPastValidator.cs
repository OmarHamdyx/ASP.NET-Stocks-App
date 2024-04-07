using System.ComponentModel.DataAnnotations;


namespace Application.ValidatorAttributes
{
	public class DateNotInPastValidator : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value != null && DateTime.TryParse(value.ToString(), out DateTime date))
			{
				if (date < new DateTime(2000, 1, 1))
				{
					return new ValidationResult(ErrorMessage);
				}
				return ValidationResult.Success;
			}
			return null;
		}
	}
}
