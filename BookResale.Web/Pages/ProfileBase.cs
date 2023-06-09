using Blazored.Toast.Services;
using BookResale.Models.Dtos;
using BookResale.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
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
        public async Task UpdateUserShippingAddress()
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

        public async Task UpdateUserInfo()
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

        public async Task UpdatePassword()
        {
            updatePassword.userId = userId;
            if(updatePassword == null || !checkObjectNotEmpty(updatePassword) || string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                toastService.ShowWarning("Empty Input");
            }
            else if (updatePassword.newPassword != confirmPassword)
            {
                toastService.ShowError("Passwords not matching");
            }
            else if (!IsPasswordStrong(updatePassword.newPassword) )
            {
                toastService.ShowWarning("Week New Password.");
            }
            else
            {
                var oldPwdDto = new UpdatePasswordDto
                {
                    userId = userId,
                    newPassword = oldPassword,
                };
                var oldPasswordVerification = await userService.PasswordVerification(oldPwdDto);
                if (!oldPasswordVerification)
                {
                    Console.WriteLine($"old: {oldPassword} \n new : {updatePassword.newPassword}");
                    Console.WriteLine($"pwdv{oldPasswordVerification}");
                    toastService.ShowWarning("Incorrect Old Password");
                }
                else if(oldPasswordVerification)
                {
                    await userService.UpdatePassword(updatePassword);
                    navigationManager.NavigateTo("/Profile", forceLoad: true);
                    await Task.Delay(5000);
                    toastService.ShowSuccess("Password Changed successfully");
                }
            }
        }

        public async Task UpdatePasswordOnKeyPress(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                await UpdatePassword();
            }
        }

        public async Task UpdateAddressOnKeyPress(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                await UpdateUserShippingAddress();
            }
        }

        public async Task AddUserShippingAddressOnKeyPress(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                await AddUserShippingAddress();
            }
        }

        public async Task UpdateUserInfoOnKeyPress(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                await UpdateUserInfo();
            }
        }
        public bool IsPasswordStrong(string password)
        {
            bool hasUppercase = false;
            bool hasLowercase = false;
            bool hasDigit = false;
            bool hasSpecialChar = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                {
                    hasUppercase = true;
                }
                else if (char.IsLower(c))
                {
                    hasLowercase = true;
                }
                else if (char.IsDigit(c))
                {
                    hasDigit = true;
                }
                else if (!char.IsLetterOrDigit(c))
                {
                    hasSpecialChar = true;
                }
            }

            return hasUppercase && hasLowercase && hasDigit && hasSpecialChar;
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
