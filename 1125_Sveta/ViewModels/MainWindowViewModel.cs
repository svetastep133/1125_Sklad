using System;
using System.Collections.Generic;
using _1125_Sveta.Models;
using _1125_Sveta.Repository;
using _1125_Sveta.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace _1125_Sveta.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;
    private MainWindow _currentWindow;
    [ObservableProperty] List<Warehouse> _warehouses;
    [ObservableProperty] private Warehouse _selectedWarehouse;
    
    


    public MainWindowViewModel(IServiceProvider serviceProvider, WareHouseRepository repository)
    {
        _serviceProvider = serviceProvider;
        Warehouses = repository.GetWarehouses();
        
    }

    [RelayCommand]
    public void OpenSklad()
    {
        var vm = ActivatorUtilities.CreateInstance<StockViewModel>(
            _serviceProvider, 
            _selectedWarehouse);
       
        var win = _serviceProvider.GetRequiredService<StockWindow>();
        win.DataContext = vm;
        win.Show();
        vm.SetClose(win.Close);
        vm.SetWindow(win);
    }

    [RelayCommand]
    public void AddProduct()
    {
        var vm = ActivatorUtilities.CreateInstance<AddProductviewModel>(
            _serviceProvider);
        var win = _serviceProvider.GetRequiredService<AddProductWindow>();
        win.DataContext = vm;
        win.Show();
        
        vm.SetClose(win.Close);
    }

    public void SetWindow(MainWindow window)
    {
        _currentWindow = window;
    }

    [RelayCommand]
    public void OpenBuyer()
    {var vm = ActivatorUtilities.CreateInstance<AddBuyerViewModel>(
            _serviceProvider);
        var win = _serviceProvider.GetRequiredService<AddBuyerWindow>();
        win.DataContext = vm;
        win.Show();
        
        vm.SetClose(win.Close);
        
    }
    
}