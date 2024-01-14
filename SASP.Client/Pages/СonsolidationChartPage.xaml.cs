using Microcharts;
using SASP.Client.DataServices;
using SASP.Client.Dtos;
using SASP.Client.Models;
using SkiaSharp;

namespace SASP.Client.Pages;

public partial class СonsolidationChartPage : ContentPage
{
    private readonly IOrderRestDataService _orderDataService;
    private readonly IRestDataService<Subscription, SubscriptionDto> _subscriptonDataService;
    private readonly IRestDataService<Issue, IssueDto> _issueDataService;
    private readonly IRestDataService<User, UserDto> _userDataService;
	public СonsolidationChartPage(
		IOrderRestDataService orderDataService, 
		IRestDataService<Subscription, SubscriptionDto> subscriptonDataService,
        IRestDataService<Issue, IssueDto> issueDataService,
        IRestDataService<User, UserDto> userDataService
        )
	{
        InitializeComponent();

		_issueDataService = issueDataService;
		_subscriptonDataService = subscriptonDataService;
		_orderDataService = orderDataService;
		_userDataService = userDataService;

		LoadingAllCharts();
    }

	private void LoadingAllCharts()
	{
        AllDataChart();
        PurchasedIssues();
        SharedCurrentIncome();
        ShowFunnel();
    }

    private async void AllDataChart()
	{
		var users = await _userDataService.GetAllAsync();
		var subs = await _subscriptonDataService.GetAllAsync();
		var orders = await _orderDataService.GetAllAsync();

		int numberUsers = users.Count;
		int numberSubs = subs.Count;
		int numberOrders = orders.Count;

		ChartEntry[] allEntries = new[]
		{
			new ChartEntry(numberUsers)
			{
				Label = "Пользователи",
				ValueLabel = $"{numberUsers}",
				Color = SKColor.Parse("#2c3e50")
			},
			new ChartEntry(numberSubs)
			{
				Label = "Действующие подписки",
				ValueLabel = $"{numberSubs}",
				Color = SKColor.Parse("#77d065")
			},
            new ChartEntry(numberOrders)
            {
                Label = "Обработанные заказы",
                ValueLabel = $"{numberOrders}",
                Color = SKColor.Parse("#77d065")
            },
        };

        AllDataLineChart.Chart = new LineChart
		{
			Entries = allEntries
        };
	}

    private async void PurchasedIssues()
    {
        var issues = await _issueDataService.GetAllAsync();
        var orders = await _orderDataService.GetAllAsync();

        int numberIssues = issues.Count;
        int numberOrders = orders.Count;

        ChartEntry[] issuesEntries = new[]
        {
            new ChartEntry(numberIssues)
            {
                Label = "Количество изданий",
                ValueLabel = $"{numberIssues}",
                Color = SKColor.Parse("#2c3e50")
            },
            new ChartEntry(numberOrders)
            {
                Label = "Купленные издания",
                ValueLabel = $"{numberOrders}",
                Color = SKColor.Parse("#77d065")
            }
        };

        PurchasedIssuesDataDonutChart.Chart = new DonutChart
        {
            Entries = issuesEntries
        };
    }

    private async void SharedCurrentIncome()
    {
        var subs = await _subscriptonDataService.GetAllAsync();
        var issues = await _issueDataService.GetAllAsync();

        decimal profitToIssues = 0;
        decimal totalCostWithIssues = 0;

        foreach (var sub in subs) profitToIssues += sub.Price;

        foreach (var issue in issues) totalCostWithIssues += issue.Price;

        ChartEntry[] purchasedIssuesEntries = new[]
        {
            new ChartEntry((float?)Decimal.Round(profitToIssues,2))
            {
                Label = "Оборот продаж",
                ValueLabel = $"{Decimal.Round(profitToIssues,2)}",
                Color = SKColor.Parse("#2c3e50")
            },
            new ChartEntry((float?)Decimal.Round(totalCostWithIssues,2))
            {
                Label = "Общий капитал изданий",
                ValueLabel = $"{Decimal.Round(totalCostWithIssues,2)}",
                Color = SKColor.Parse("#77d065")
            }
        };

        CurrentIncomeChart.Chart = new DonutChart
        {
            Entries = purchasedIssuesEntries
        };
    }

    private async void ShowFunnel()
    {
        var users = await _userDataService.GetAllAsync();
        var subs = await _subscriptonDataService.GetAllAsync();

        var usersWithSubs = subs.Select(s => s.User).Distinct().ToList();

        ChartEntry[] funnelEntries = new[]
        {
            new ChartEntry(users.Count)
            {
                Label = "Всего пользователей",
                ValueLabel = $"{users.Count}",
                Color = SKColor.Parse("#2c3e50")
            },
            new ChartEntry(usersWithSubs.Count)
            {
                Label = "Заинтересованные пользователя",
                ValueLabel = $"{usersWithSubs.Count}",
                Color = SKColor.Parse("#77d065")
            }
        };

        FunnelBarChart.Chart = new BarChart
        {
            Entries = funnelEntries
        };
    }
}