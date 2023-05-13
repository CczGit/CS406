namespace MLOBuddy;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
		Routing.RegisterRoute(nameof(AddOrEditClient), typeof(AddOrEditClient));
		Routing.RegisterRoute(nameof(AddOrEditDebt), typeof(AddOrEditDebt));
		Routing.RegisterRoute(nameof(AddOrEditJob), typeof(AddOrEditJob));
	}
}
