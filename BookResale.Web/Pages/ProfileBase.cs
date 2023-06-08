using Blazored.Toast.Services;
using BookResale.Models.Dtos;
using BookResale.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BookResale.Web.Pages
{
    public class ProfileBase : ComponentBase
    {
        [Inject]
        public IUserService? userService { get; set; }
        [Inject]
        public AuthenticationStateProvider? authenticationStateProvider { get; set; }
        [Inject]
        public IToastService toastService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        public UserDto? userDto { get; set; }
        public UserShippingAdressDto? userShippingAdress { get; set; }
        public UserShippingAdressDto? addUserShippingAddress = new UserShippingAdressDto();
        public UpdatePasswordDto? updatePassword = new UpdatePasswordDto();
        public bool IsLoggedIn { get; set; }
        public int userId { get; set; }
        public bool EditProfile = true;
        public bool EditShipping = true;
        public string? submitOnOffProfil = "none";
        public string? submitOnOffShipping = "none";
        public string? oldPassword;
        public string? confirmPassword;
        public string? ProfileActive = "liActive";
        public string? ShippingActive;
        public string? PasswordActive;
        public string? ProfileActionActive = "";
        public string? ShippingActionActive = "none";
        public string? PasswordActionActive = "none";

        public void SetProfileActive()
        {
            ProfileActive = "liActive";
            ShippingActive = "";
            PasswordActive = "";

            ProfileActionActive = "";
            ShippingActionActive = "none";
            PasswordActionActive = "none";
        }
        public void SetShippingActive()
        {
            ProfileActive = "";
            ShippingActionActive = "liActive";
            PasswordActive = "";

            ProfileActionActive = "none";
            ShippingActionActive = "";
            PasswordActionActive = "none";
        }
        public void SetPasswordActive()
        {
            ProfileActive = "";
            ShippingActive = "";
            PasswordActive = "liActive";

            ProfileActionActive = "none";
            ShippingActionActive = "none";
            PasswordActionActive = "";
        }

        public void enableOrDisableEditProfile()
        {
            EditProfile = !EditProfile;
            if(submitOnOffProfil == "none")
            {
                submitOnOffProfil = "block";
            }
            else
            {
                submitOnOffProfil = "none";
            }
        }
        public async void UpdateUserShippingAddress()
        {
            if(userShippingAdress == null || !checkObjectNotEmpty(userShippingAdress))
            {
                toastService.ShowWarning("Empty Input");
            }
            else
            {
                await userService.UpdateUserShippingAddress(userShippingAdress);
                navigationManager.NavigateTo("/Profile", forceLoad: true);
                await Task.Delay(5000);
                toastService.ShowSuccess("Shipping Address Updated");
            }
        }

        public async void UpdateUserInfo()
        {
            if(userDto == null || !checkObjectNotEmpty(userDto))
            {
                toastService.ShowWarning("Empty Input");
            }
            else
            {
                var updateUserInformationsDto = new UpdateUserInformationsDto
                {
                    userId = userDto.Id,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Email = userDto.Email,
                };
                await userService.UpdateUserInformations(updateUserInformationsDto);
                navigationManager.NavigateTo("/Profile", forceLoad: true);
                StateHasChanged();
                await Task.Delay(5000);
                toastService.ShowSuccess("Shipping Address Updated");
            }
        }

        public async void UpdatePassword()
        {
            updatePassword.userId = userId;
            if(updatePassword == null || !checkObjectNotEmpty(updatePassword))
            {
                toastService.ShowWarning("Empty Input");
            }
            else if(updatePassword.newPassword != confirmPassword)
            {
                toastService.ShowWarning("Passwords not matching");
            }
            else
            {
                var oldPwdDto = new UpdatePasswordDto
                {
                    userId = userId,
                    newPassword = oldPassword,
                };
                var oldPasswordVerification = await userService.PasswordVerification(oldPwdDto);
                if (oldPasswordVerification == false)
                {
                    Console.WriteLine($"old: {oldPassword} \n new : {updatePassword.newPassword}");
                    Console.WriteLine($"pwdv{oldPasswordVerification}");
                    toastService.ShowWarning("Incorrect Old Password");
                }
                else
                {
                    await userService.UpdatePassword(updatePassword);
                    navigationManager.NavigateTo("/Profile", forceLoad: true);
                    await Task.Delay(5000);
                    toastService.ShowSuccess("Password Changed successfully");
                }
            }
        }

        public void enableOrDisableEditShipping()
        {
            EditShipping = !EditShipping;
            if (submitOnOffShipping == "none")
            {
                submitOnOffShipping = "block";
            }
            else
            {
                submitOnOffShipping = "none";
            }
        }

        public async Task AddUserShippingAddress()
        {
            addUserShippingAddress.userId = userId;

            if (addUserShippingAddress == null || !checkObjectNotEmpty(addUserShippingAddress))
            {
                toastService.ShowWarning("Empty Input.");
                Console.WriteLine($"userId: {addUserShippingAddress.userId}\n address: {addUserShippingAddress.Address} \n city: {addUserShippingAddress.city} \n phone: {addUserShippingAddress.phoneNumber}");
            }
            else if (addUserShippingAddress.phoneNumber.Length != 10)
            {
                toastService.ShowWarning("Invalid phone number.");
            }
            else
            {
                await userService.AddUserShippingAddress(addUserShippingAddress);
                navigationManager.NavigateTo("/Profile", forceLoad: true);
                await Task.Delay(5000);
                toastService.ShowSuccess("Shipping Address Added");
            }
        }

        public bool checkObjectNotEmpty(object obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(obj);
                if (value == null || string.IsNullOrEmpty(value.ToString()))
                {
                    return false;
                }
            }

            return true;
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;

                IsLoggedIn = user.Identity?.IsAuthenticated ?? false;

                if (IsLoggedIn)
                {
                    var claims = user.Claims;
                    var user_id = int.Parse(claims.Where(_ => _.Type == "Sub").Select(_ => _.Value).FirstOrDefault());
                    if (user_id != 0)
                    {
                        userId = user_id;
                        userDto = await userService.GetUser(userId);
                        userShippingAdress = await userService.GetShippingInformations(userId);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
