namespace appdevfinal.Page;

public partial class SignupPage : ContentPage
{
	public SignupPage()
	{
		InitializeComponent();

		BindingContext = new SignupViewModel();

        RolePicker.ItemsSource = new List<string> { "admin" };
    }
}