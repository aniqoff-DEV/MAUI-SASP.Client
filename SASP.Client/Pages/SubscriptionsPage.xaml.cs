using SASP.Client.DataServices;
using SASP.Client.Dtos;
using SASP.Client.Models;
using System.Xml;

namespace SASP.Client.Pages;

public partial class SubscriptionsPage : ContentPage
{
    private readonly IRestDataService<Subscription, SubscriptionDto> _subscriptonDataService;
    private readonly IRestDataService<Issue, IssueDto> _issueDataService;
    private readonly IRestDataService<User, UserDto> _userDataService;

    public SubscriptionsPage(
        IRestDataService<Subscription, SubscriptionDto> subscriptonDataService, 
        IRestDataService<Issue, IssueDto> issueDataService,
        IRestDataService<User, UserDto> userDataService)
	{
		InitializeComponent();

        _subscriptonDataService = subscriptonDataService;
        _issueDataService = issueDataService;
        _userDataService = userDataService;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        SubscriptionsDataView.ItemsSource = await _subscriptonDataService.GetAllAsync();

    }

    private async void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        var currentSubs = await _subscriptonDataService.GetAllAsync();

        if (SearchEntry.Text != null)
        {
            currentSubs = currentSubs.Where(s =>
                s.User.ToLower().Contains(SearchEntry.Text.ToLower()) ||
                s.Issue.ToLower().Contains(SearchEntry.Text.ToLower()) ||
                s.StartSub.ToString().Contains(SearchEntry.Text) ||
                s.EndSub.ToString().Contains(SearchEntry.Text))
                .ToList();
        }

        SubscriptionsDataView.ItemsSource = currentSubs.OrderBy(i => i.SubscriptionId).ToList();
    }

    private void CreateSub_Clicked(object sender, EventArgs e)
    {
        MauiPopup.PopupAction.DisplayPopup(new CreateSubPopupPage(_subscriptonDataService, _issueDataService, _userDataService));
    }
}