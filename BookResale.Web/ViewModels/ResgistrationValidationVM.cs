using FluentValidation;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace BookResale.Web.ViewModels
{
    public class ResgistrationValidationVM : AbstractValidator<RegistrationVM>
    {
        private readonly HttpClient _httpClient;
        public ResgistrationValidationVM(HttpClient _httpClient)
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress()
                .MustAsync(async (value, CancellationToken) => await UniqueEmail(value))
                .When(_ => !string.IsNullOrEmpty(_.Email) && Regex.IsMatch(_.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase), ApplyConditionTo.CurrentValidator)
                .WithMessage("Email Already Exists.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Empty Password.")
                .MinimumLength(6).WithMessage("Short Password Use At Ceast 6 Characters.")
                .Matches(@"[A-Z]+").WithMessage("Use Uppercase Letters In Your Password.")
                .Matches(@"[a-z]+").WithMessage("Use Lowercase Letters In Your Password.")
                .Matches(@"[0-9]+").WithMessage("Use Numbers In Your Password.")
                .Matches(@"[\@\!\?\*\.]+").WithMessage("Use Signs [@, !, ?, *, .] In Your Password.");
            RuleFor(x => x.ConfirmPassword).Equal(_ => _.Password).WithMessage("Passwords does not match.");
            this._httpClient = _httpClient;
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<RegistrationVM>.CreateWithOptions((RegistrationVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };

        private async Task<bool> UniqueEmail(string email)
        {
            try
            {
                string url = $"/api/User/unique-user-email?email={email}";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return Convert.ToBoolean(content);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
