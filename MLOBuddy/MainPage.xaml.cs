
using MLOBuddy.ViewModel;

namespace MLOBuddy;

public partial class MainPage : ContentPage
{

	public MainPage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	
}

