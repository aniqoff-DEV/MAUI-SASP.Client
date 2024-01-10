using SASP.Client.Pages;

namespace SASP.Client;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(IssuesPage), typeof(IssuesPage));
    }
}
