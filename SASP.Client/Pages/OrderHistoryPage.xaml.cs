using SASP.Client.DataServices;
using SASP.Client.Dtos;
using SASP.Client.Models;

namespace SASP.Client.Pages;

public partial class OrderHistoryPage : ContentPage
{
    private readonly IOrderRestDataService _orderDataService;

    public OrderHistoryPage(IOrderRestDataService orderDataService)
	{
		InitializeComponent();

        _orderDataService = orderDataService;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        OrderView.ItemsSource = await _orderDataService.GetAllAsync();
    }

    private async void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        var currentOrders = await _orderDataService.GetAllAsync();

        if (SearchEntry.Text != null)
        {
            currentOrders = currentOrders.Where(s =>
                s.User.ToLower().Contains(SearchEntry.Text.ToLower()) ||
                s.Issue.ToLower().Contains(SearchEntry.Text.ToLower()) ||
                s.City.ToLower().ToString().Contains(SearchEntry.Text.ToLower()) ||
                s.Country.ToLower().ToString().Contains(SearchEntry.Text.ToLower()) ||
                s.Status.ToLower().ToString().Contains(SearchEntry.Text.ToLower()))
                .ToList();
        }

        OrderView.ItemsSource = currentOrders.OrderBy(i => i.OrderId).ToList();
    }

    private async void OrderView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            var currentOrderDto = (OrderDto)OrderView.SelectedItem;

            var order = await _orderDataService.GetRowAsync(currentOrderDto.OrderId);

            order.Status = await CallPopup();

            await _orderDataService.UpdateAsync(order);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошбика", $"Что-то пошло не так \n {ex}", "ОK");
        }
    }

    private async Task<string> CallPopup()
    {
        var statusList = Enum.GetValues(typeof(OrderStatusEnum)).Cast<OrderStatusEnum>().ToList();
        var statusArray = statusList.ToArray();

        var action = await DisplayActionSheet("Выбрать стаутс", "Отмена", null,
                statusArray[0].ToString(), statusArray[1].ToString(), statusArray[2].ToString());

        return action;
    }

    private enum OrderStatusEnum
    {
        InProcessing = 1,
        OnMyWay = 2,
        Delivered = 3,
    }

}