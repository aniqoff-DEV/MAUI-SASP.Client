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
}