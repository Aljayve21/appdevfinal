using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using appdevfinal.Classes;
using appdevfinal;

public class SignupViewModel : BindableObject
{
    private string _fullname;
    private string _username;
    private string _password;
    private string _selectedRole;

    public string Fullname
    {
        get => _fullname;
        set
        {
            _fullname = value;
            OnPropertyChanged(nameof(Fullname));
        }
    }

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

    public string SelectedRole
    {
        get => _selectedRole;
        set
        {
            _selectedRole = value;
            OnPropertyChanged(nameof(SelectedRole));
        }
    }


    private async Task Signup(string password)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(Fullname) || string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                Console.WriteLine("Please fill in all required fields.");
                return;
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);

            var admin = new Admins
            {
                Fullname = Fullname,
                Username = Username,
                PasswordHash = hashedPassword,
                Role = SelectedRole
            };
            

            await InsertAdminAsync(admin);

            await App.Current.MainPage.Navigation.PushAsync(new MainPage());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during signup: {ex.Message}");
        }

      }

    private async Task InsertAdminAsync(Admins admin)
    {
        try
        {
            var dbConnection = new DBConnection();
            var database = dbConnection.GetDatabase();
            var adminsCollection = database.GetCollection<Admins>("admins");

            await adminsCollection.InsertOneAsync(admin);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during admin insertion: {ex.Message}");
        }

    }

    public Microsoft.Maui.Controls.Command<string> SignupCommand => new Microsoft.Maui.Controls.Command<string>(async (password) => await Signup(password));
}
