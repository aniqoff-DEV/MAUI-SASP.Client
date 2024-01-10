using SASP.Client.DataServices;
using SASP.Client.Models;

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

        builder.Services.AddSingleton<MainPage>();
        //builder.Services.AddTransient<ManagerToDoPage>();

        return builder.Build();
	}
}
