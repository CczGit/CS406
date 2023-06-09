﻿using Microsoft.Extensions.Logging;
using MLOBuddy.ViewModel;

namespace MLOBuddy;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainViewModel>();

		builder.Services.AddTransient<DetailsPage>();
		builder.Services.AddTransient<DetailsPageViewModel>();

		builder.Services.AddTransient<AddOrEditClient>();
        builder.Services.AddTransient<AddOrEditClientViewModel>();

		builder.Services.AddTransient<AddOrEditDebt>();
        builder.Services.AddTransient<AddOrEditDebtViewModel>();

		builder.Services.AddTransient<AddOrEditJob>();
		builder.Services.AddTransient<AddOrEditJobVIewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
