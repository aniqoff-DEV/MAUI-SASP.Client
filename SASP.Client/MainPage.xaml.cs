using SASP.Client.DataServices;
using SASP.Client.Dtos;
using SASP.Client.Models;
using SASP.Client.Pages;

namespace SASP.Client;

public partial class MainPage : ContentPage
{
    private readonly IRestDataService<Issue, IssueDto> _issueDataService;

    public MainPage(IRestDataService<Issue, IssueDto> issueDataService)
	{
		InitializeComponent();

        _issueDataService = issueDataService;
        LoadImagesOnContent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();


    }

    private async void LoadImagesOnContent()
    {
        var issues = await _issueDataService.GetAllAsync();

        ImagesIssueView.ItemsSource = issues.Where(i => i.Photo != string.Empty);
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
            {nameof(SubscriptionDto), new SubscriptionDto() },

            {nameof(Issue), new Issue()},
            {nameof(IssueDto), new IssueDto() },

            {nameof(User), new User()},
            {nameof(UserDto), new UserDto() },
        };

        await Shell.Current.GoToAsync(nameof(SubscriptionsPage), navigationParametr);
    }

    private async void ToOrderHistroyPage_Clicked(object sender, EventArgs e)
    {
        var navigationParametr = new Dictionary<string, object>
        {
            {nameof(Order), new Order()},
            {nameof(OrderDto), new OrderDto() }
        };

        await Shell.Current.GoToAsync(nameof(OrderHistoryPage), navigationParametr);
    }

    private async void ToReportsPage_Clicked(object sender, EventArgs e)
    {
        var navigationParametr = new Dictionary<string, object>
        {
            {nameof(Subscription), new Subscription()},
            {nameof(SubscriptionDto), new SubscriptionDto() },

            {nameof(Issue), new Issue()},
            {nameof(IssueDto), new IssueDto() },

            {nameof(User), new User()},
            {nameof(UserDto), new UserDto() },

            {nameof(Order), new Order()},
            {nameof(OrderDto), new OrderDto() }
        };

        await Shell.Current.GoToAsync(nameof(СonsolidationChartPage), navigationParametr);
    }
}

