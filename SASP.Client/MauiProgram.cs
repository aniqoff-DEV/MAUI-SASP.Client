using SASP.Client.DataServices;
using SASP.Client.Dtos;
using SASP.Client.Models;
using SASP.Client.Pages;

namespace SASP.Client;

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

        builder.Services.AddHttpClient<IRestDataService<Issue, IssueDto>, IssueDataService>();
        builder.Services.AddHttpClient<IRestDataService<Subscription, SubscriptionDto>, SubscriptionDataService>();
        builder.Services.AddHttpClient<IRestDataService<User, UserDto>, UserDataService>();
        builder.Services.AddHttpClient<IOrderRestDataService, OrderDataService>();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddScoped<IssuesPage>();
        builder.Services.AddScoped<SubscriptionsPage>();
        builder.Services.AddTransient<OrderHistoryPage>();

        return builder.Build();
	}
}
