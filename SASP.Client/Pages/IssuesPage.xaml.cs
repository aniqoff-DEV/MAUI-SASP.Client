using SASP.Client.DataServices;
using SASP.Client.Models;

namespace SASP.Client.Pages;

public partial class IssuesPage : ContentPage
{
    private readonly IRestDataService<Issue, IssueDto> _issueDataService;

    private string selectedButtonText;

    private Button lastButton;

    public IssuesPage(IRestDataService<Issue, IssueDto> issueDataService)
	{
		InitializeComponent();

        _issueDataService = issueDataService;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        IssuesDataView.ItemsSource = await _issueDataService.GetAllAsync();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if(lastButton != null) 
        {
            lastButton.BackgroundColor = Color.FromArgb("#512BD4");
        }
        
        lastButton = (Button)sender;
        selectedButtonText = ((Button)sender).Text;

        ((Button)sender).BackgroundColor = Colors.Blue;

        IssuesSource();
    }

    private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        IssuesSource();
    }

    private async void IssuesSource()
    {
        var currentIssues = await _issueDataService.GetAllAsync();

        if(selectedButtonText != null)
        {
            currentIssues = currentIssues.Where(i => i.TypeIssue.Contains(selectedButtonText)).ToList();
        }

        if(SearchEntry.Text != null)
        {
            currentIssues = currentIssues.Where(i =>
                i.Title.ToLower().Contains(SearchEntry.Text.ToLower()) ||
                i.Catalog.ToLower().Contains(SearchEntry.Text.ToLower()))
                .ToList();
        }

        IssuesDataView.ItemsSource = currentIssues.OrderBy(i => i.Title).ToList();
    }
}