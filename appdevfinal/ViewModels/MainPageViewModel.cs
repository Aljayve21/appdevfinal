using appdevfinal.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace appdevfinal.ViewModels
{
    public class MainPageViewModel : BindableObject
    {
        public Command NavigateToSignupCommand { get; }

        public Command LoginCommand { get; }

        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private async Task ExecuteLoginCommand()
        {
            try
            {
                var dbConnection = new DBConnection();
                var isAuthenticated = await dbConnection.AuthenticateUser(Username, Password);

                if (isAuthenticated)
                {
                    Console.WriteLine("Login Successfully!");

                    await Shell.Current.GoToAsync("//admin/adminDashboard");
                }
                else
                {

                    Console.WriteLine("Sorry! The username or password is invalid. Please try again");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");
            }

            
        }

        

        public MainPageViewModel()
        {
            NavigateToSignupCommand = new Command(NavigateToSignup);
            LoginCommand = new Command(async () => await ExecuteLoginCommand());

        }



        private async void NavigateToSignup()
        {
            var signupPage = new appdevfinal.Page.SignupPage();
            await Shell.Current.Navigation.PushAsync(signupPage);
        }


    }
}
