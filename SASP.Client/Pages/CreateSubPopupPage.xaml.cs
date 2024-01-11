using MauiPopup.Views;
using SASP.Client.DataServices;
using SASP.Client.Dtos;
using SASP.Client.Models;

namespace SASP.Client.Pages;

public partial class CreateSubPopupPage : BasePopupPage
{
    private readonly IRestDataService<Subscription, SubscriptionDto> _subscriptonDataService;
    private readonly IRestDataService<Issue, IssueDto> _issueDataService;
    private readonly IRestDataService<User, UserDto> _userDataService;

    public CreateSubPopupPage(
        IRestDataService<Subscription, SubscriptionDto> subscriptonDataService,
        IRestDataService<Issue, IssueDto> issueDataService,
        IRestDataService<User, UserDto> userDataService)
	{
		InitializeComponent();

        _subscriptonDataService = subscriptonDataService;
        _issueDataService = issueDataService;
        _userDataService = userDataService;

        OnAppearing();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var issues = await _issueDataService.GetAllAsync();
        IssuePicker.ItemsSource = issues;
    }

    private void Closed_Clicked(object sender, EventArgs e)
    {
        MauiPopup.PopupAction.ClosePopup();
    }

    private async void AccessCreateButton_Clicked(object sender, EventArgs e)
    {
        var userName = UserNameEnry.Text;
        var startSub = DateTime.Now;

        if(EndSubDatePicker.Date <= startSub)
        {
            await DisplayAlert("Ошибка", "Неправильная дата", "ОK");
        }
        else
        {
            var endSub = EndSubDatePicker.Date;

            IssueDto issue = (IssueDto)IssuePicker.SelectedItem;

            if(issue == null)
            {
                await DisplayAlert("Ошибка", "Не выбранно издание", "ОK");
            }
            else
            {
                var currentIssue = issue;

                var currentUser = await DefineUserByName(userName);

                if(currentUser == null)
                {
                    await DisplayAlert("Ошибка", "Пользователь не найден", "ОK");
                }
                else
                {
                    var price = PriceCalculation(currentIssue.Price, endSub, startSub);

                    var newSub = new Subscription
                    {
                        IssueId = currentIssue.IssueId,
                        UserId = currentUser.UserId,
                        StartSub = startSub,
                        EndSub = endSub,
                        Price = price
                    };

                    try
                    {
                        await _subscriptonDataService.AddAsync(newSub);

                        var result = await DisplayAlert("Успех", "Удалось зарегестрировать подписку \nСоздать еще одну подписку?", "Да", "Выйти");
                        if(result == false) await MauiPopup.PopupAction.ClosePopup();
                    }
                    catch
                    {
                        await DisplayAlert("Ошибка", "Неудалось зарегестрировать подписку", "ОK");
                    }
                }  
            }
        }
    }

    private async Task<UserDto> DefineUserByName(string userName)
    {
        var users = await _userDataService.GetAllAsync();
        var currentUser = users.Where(u => u.Name == userName).FirstOrDefault();

        return currentUser;
    }

    private decimal PriceCalculation(decimal priceNow, DateTime endSub, DateTime startSub)
    {
        decimal price = priceNow * (1 + 1 / (endSub.Day - startSub.Day)) * (decimal)1.01 + (decimal)1.18;
        return price;
    }

}