using MLOBuddy.ViewModel;
namespace MLOBuddy;

public partial class AddOrEditClient : ContentPage
{
	public AddOrEditClient(AddOrEditClientViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}