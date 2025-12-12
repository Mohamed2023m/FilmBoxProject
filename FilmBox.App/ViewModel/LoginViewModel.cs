using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmBox.App.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Storage;

namespace FilmBox.App.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly UserApiService _api; 
        private readonly NavigationManager _nav;




        public LoginViewModel(UserApiService api, NavigationManager nav)
        {

            _api = api; 
            
            _nav = nav;


        }
        [ObservableProperty]
        private string email;
        
        [ObservableProperty] 
        private string password;
        [ObservableProperty]
        private string loginMessage;

   
       [RelayCommand]
        public async Task LoginAsync()
        {
        var result = await _api.LoginAsync(Email, Password); 

        if (result != null && !string.IsNullOrEmpty(result.Token))
        { await SecureStorage.Default.SetAsync("jwt", result.Token);
            LoginMessage = "Login successful!";

            _nav.NavigateTo("/media", true); }
        else
        { LoginMessage = "Invalid email or password."; } 
    
    }
}

}



