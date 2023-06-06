using BookResale.Admin.ViewModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Blazored.LocalStorage;
using System.Net;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components.Authorization;
using BookResale.Web.Shared.Providers;

namespace BookResale.Admin.Pages
{
    public class IndexBase : ComponentBase
    {
        
        [Inject]
        protected HttpClient? httpClient { get; set; }

        [Inject]
        protected IToastService? _toastService { get; set; }

        [Inject]
        protected NavigationManager? _navigationManager { get; set; }

        [Inject]
        protected ILocalStorageService? _localStorageService { get; set; }
        [Inject]
        protected AuthenticationStateProvider? _authStateProvider { get; set; }

        protected LoginVM loginModel = new LoginVM();
        protected MudForm form_login;
        protected string APIErrorMessagesLogin;
        protected LoginValidationVM loginValidatorModel = new LoginValidationVM();

        protected async Task LoginAsync()
        {
            await form_login.Validate();
            if (form_login.IsValid)
            {
                var jsonPayload = JsonSerializer.Serialize(loginModel);
                var requestContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("api/User/loginAdmin", requestContent);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errors = await response.Content.ReadFromJsonAsync<Dictionary<string, List<string>>>();
                    if (errors.Count > 0)
                    {
                        foreach (var item in errors)
                        {
                            foreach (var errorMessage in item.Value)
                            {
                                _toastService.ShowError($"{errorMessage}.");
                            }
                        }
                    }
                }
                else if (response.StatusCode == HttpStatusCode.OK)
                {
                    _navigationManager.NavigateTo("/Dashboard");
                    var tokenResponse = await response.Content.ReadFromJsonAsync<JwtTokenResponseVm>();
                    await _localStorageService.SetItemAsync<string>("jwt-access-token", tokenResponse.AccessToken);
                    (_authStateProvider as CustomAuthProvider).NotifyAuthState();
                    _toastService.ShowSuccess("logged-In.");
                }
                else
                {
                    _toastService.ShowError("Failed to login.");
                }
            }
        }
    }
}
