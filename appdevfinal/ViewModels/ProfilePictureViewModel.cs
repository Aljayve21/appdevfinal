using appdevfinal.Classes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace appdevfinal.ViewModels
{
    public class ProfilePictureViewModel : BindableObject
    {
        private string _fullName;
        private string _username;

        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
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

        public async Task LoadUserDataAsync()
        {
            try
            {
                var dbConnection = new DBConnection();
                var database = dbConnection.GetDatabase();
                var adminsCollection = database.GetCollection<Admins>("admins");

                var loggedInUser = await adminsCollection.Find(u => u.Username == "LoggedInUsername").FirstOrDefaultAsync();

                if (loggedInUser != null)
                {
                    FullName = loggedInUser.Fullname;
                    Username = loggedInUser.Username;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading user data: {ex.Message}");
            }
        }

        
    }
}
