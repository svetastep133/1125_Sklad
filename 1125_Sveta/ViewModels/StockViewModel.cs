using System;
using System.Collections.Generic;
using _1125_Sveta.Models;
using _1125_Sveta.Repository;
using _1125_Sveta.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace _1125_Sveta.ViewModels;

public partial class StockViewModel : ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;
    [ObservableProperty] List<Stock> _stocks;
    [ObservableProperty] private Warehouse _selectedWarehouse;
    [ObservableProperty] private Stock? _selectedStock;

    public StockViewModel(IServiceProvider serviceProvider, StockRepository stockRepository, Warehouse house,WareHouseRepository wareHouseRepository)
    {
        _serviceProvider = serviceProvider;
        _stocks = stockRepository.GetStocks(house);
        SelectedWarehouse = house;
       
    }
    
    [RelayCommand]
    public void AddProduct()
    {
        var vm = ActivatorUtilities.CreateInstance<NewProductViewModel>(
            _serviceProvider, SelectedWarehouse);
       
        var win = _serviceProvider.GetRequiredService<NewProductWindow>();
        
        win.DataContext = vm;
        win.Show();
        vm.SetClose(win.Close);
    }

    [RelayCommand]
    public void Inf()
    {
        if (SelectedStock == null)
            return;
        var vm = ActivatorUtilities.CreateInstance<InfStockViewModel>(
            _serviceProvider, SelectedStock);
       
        var win = _serviceProvider.GetRequiredService<InfStockWindow>();
        win.DataContext = vm;
        win.Show();
        
    }
    
}