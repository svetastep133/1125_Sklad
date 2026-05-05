using System;
using System.Collections.Generic;
using _1125_Sveta.Models;
using _1125_Sveta.Repository;
using CommunityToolkit.Mvvm.ComponentModel;

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
        Warehouses.Insert(0, new Warehouse{Name = "Выберите склад"});
        SelectedWarehouse = Warehouses[0];

    }
    
}