using SASP.Client.DataServices;
using SASP.Client.Dtos;
using SASP.Client.Models;

namespace SASP.Client.Pages;

public partial class SubscriptionsPage : ContentPage
{
    private readonly IRestDataService<Subscription, SubscriptionDto> _subscriptonDataService;

    public SubscriptionsPage(IRestDataService<Subscription, SubscriptionDto> subscriptonDataService)
	{
		InitializeComponent();

        _subscriptonDataService = subscriptonDataService;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        SubscriptionsDataView.ItemsSource = await _subscriptonDataService.GetAllAsync();
    }

    private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        //var currentIssues = await _issueDataService.GetAllAsync();

        //if (selectedButtonText != null)
        //{
        //    currentIssues = currentIssues.Where(i => i.TypeIssue.Contains(selectedButtonText)).ToList();
        //}

        //if (SearchEntry.Text != null)
        //{
        //    currentIssues = currentIssues.Where(i =>
        //        i.Title.ToLower().Contains(SearchEntry.Text.ToLower()) ||
        //        i.Catalog.ToLower().Contains(SearchEntry.Text.ToLower()))
        //        .ToList();
        //}

        //IssuesDataView.ItemsSource = currentIssues.OrderBy(i => i.Title).ToList();
    }
}