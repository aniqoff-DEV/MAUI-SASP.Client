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

    private async void LoadImagesOnContent()
    {
        //TODO
        //var issues = await _issueDataService.GetAllAsync();
        //List<Image> images = new List<Image>();

        //foreach (var issue in issues)
        //{
        //    if(issue.Photo != null && issue.Photo != string.Empty)
        //    {
        //        Image image = new Image
        //        {
        //            Source = issue.Photo
        //        };

        //        images.Add(image);
        //    }
        //}

        //ImagesContentView = new StackLayout()
        //{
        //    Children = { images[0] }
        //};
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
}

