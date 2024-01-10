using Microsoft.Maui.Controls;
using SASP.Client.DataServices;
using SASP.Client.Models;

namespace SASP.Client;

public partial class MainPage : ContentPage
{
    private readonly IRestDataService<Issue, IssueDto> _issueDataService;

    public MainPage(IRestDataService<Issue, IssueDto> issueDataService)
	{
		InitializeComponent();
		_issueDataService = issueDataService;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        IssuesDataView.ItemsSource = await _issueDataService.GetAllAsync();
    }
}

