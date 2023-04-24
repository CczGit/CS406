using MLOBuddy.ViewModel;
namespace MLOBuddy;


public partial class DetailsPage : ContentPage
{
	public DetailsPage(DetailsPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}