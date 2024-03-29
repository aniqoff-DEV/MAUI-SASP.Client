﻿using SASP.Client.Pages;

namespace SASP.Client;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(IssuesPage), typeof(IssuesPage));
        Routing.RegisterRoute(nameof(SubscriptionsPage), typeof(SubscriptionsPage));
        Routing.RegisterRoute(nameof(OrderHistoryPage), typeof(OrderHistoryPage));
        Routing.RegisterRoute(nameof(СonsolidationChartPage), typeof(СonsolidationChartPage));
    }
}
