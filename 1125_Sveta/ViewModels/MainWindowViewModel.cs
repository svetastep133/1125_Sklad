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
    public void Product()
    {var vm = ActivatorUtilities.CreateInstance<ProductViewModel>(
            _serviceProvider);
        var win = _serviceProvider.GetRequiredService<ProductWindow>();
        win.DataContext = vm;
        win.Show();
        
        vm.SetClose(win.Close);
        
    }
    [RelayCommand]
    public void Buyer()
    {var vm = ActivatorUtilities.CreateInstance<BuyerViewModel>(
            _serviceProvider);
        var win = _serviceProvider.GetRequiredService<BuyerWindow>();
        win.DataContext = vm;
        win.Show();
        
        vm.SetClose(win.Close);
        
    }
    [RelayCommand]
    public void Supplier()
    {var vm = ActivatorUtilities.CreateInstance<SupplierViewModel>(
            _serviceProvider);
        var win = _serviceProvider.GetRequiredService<SupplierWindow>();
        win.DataContext = vm;
        win.Show();
        
        vm.SetClose(win.Close);
        
    }
    
}