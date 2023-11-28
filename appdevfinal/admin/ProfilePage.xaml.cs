using appdevfinal.ViewModels;
namespace appdevfinal.admin;

public partial class ProfilePage : ContentPage
{
    private readonly ProfilePictureViewModel _viewModel;
	
    public ProfilePage()
	{
		InitializeComponent();

        BindingContext = new ProfilePictureViewModel();
	}

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
    

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadUserDataAsync();
        }
    
    
    
}