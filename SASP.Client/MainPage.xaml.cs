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
        //await Navigation.PushAsync(new IssuesPage());

        var navigationParametr = new Dictionary<string, object>
        {
            {nameof(Issue), new Issue()},
            {nameof(IssueDto), new IssueDto() }
        };
        await Shell.Current.GoToAsync(nameof(IssuesPage), navigationParametr);
    }
}

