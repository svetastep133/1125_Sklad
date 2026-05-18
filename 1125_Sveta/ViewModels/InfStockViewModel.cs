using System;
using System.Collections.Generic;
using _1125_Sveta.Models;
using _1125_Sveta.Repository;
using CommunityToolkit.Mvvm.ComponentModel;

namespace _1125_Sveta.ViewModels;

public partial class InfStockViewModel: ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;
    
    [ObservableProperty] Stock _stock;
    [ObservableProperty] private Stock _selectedStock;
    

    public InfStockViewModel(IServiceProvider serviceProvider, Stock stock )
    {
        _serviceProvider = serviceProvider;
        SelectedStock = stock;

    }
    
    
    
}