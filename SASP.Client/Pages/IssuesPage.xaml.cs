using SASP.Client.DataServices;
using SASP.Client.Models;

namespace SASP.Client.Pages;

public partial class IssuesPage : ContentPage
{
    private readonly IRestDataService<Issue, IssueDto> _issueDataService;
    private string SelectedText;
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

    private void Button_Clicked(object sender, EventArgs e)
    {
        SelectedText = ((Button)sender).Text;
    }

    private async void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        var currentIssues = await _issueDataService.GetAllAsync();

        currentIssues = currentIssues.Where(i =>
            i.Title.ToLower().Contains(SearchEntry.Text.ToLower()) ||
            i.Catalog.ToLower().Contains(SearchEntry.Text.ToLower()))
            .ToList();

        IssuesDataView.ItemsSource = currentIssues.OrderBy(i => i.Title).ToList();
    }
}