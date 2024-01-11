using SASP.Client.Dtos;
using SASP.Client.Models;
using SASP.Client.Pages;

namespace SASP.Client;

public partial class MainPage : ContentPage
{
    public MainPage()
	{
		InitializeComponent();
	}

    private async void ToIssuesPage_Clicked(object sender, EventArgs e)
    {
        var navigationParametr = new Dictionary<string, object>
        {
            {nameof(Issue), new Issue()},
            {nameof(IssueDto), new IssueDto() }
        };

        await Shell.Current.GoToAsync(nameof(IssuesPage), navigationParametr);
    }

    private async void ToSubsPage_Clicked(object sender, EventArgs e)
    {
        var navigationParametr = new Dictionary<string, object>
        {
            {nameof(Subscription), new Subscription()},
            {nameof(SubscriptionDto), new SubscriptionDto() }
        };

        await Shell.Current.GoToAsync(nameof(SubscriptionsPage), navigationParametr);
    }
}

