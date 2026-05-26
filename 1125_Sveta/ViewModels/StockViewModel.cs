using System;
using System.Collections.Generic;
using System.Linq;
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
    private StockRepository _stockRepository;
    private Warehouse _house;
    [ObservableProperty] List<Stock> _stocks;
    [ObservableProperty] private Warehouse _selectedWarehouse;
    [ObservableProperty] private Stock? _selectedStock;
    [ObservableProperty] private Product? _selectedProduct;

    private StockWindow _stockWindow;
    private List<Stock> _allStocks;
    public string SearchText
    {
        get => _searchText;
        set
        {
            if (value == _searchText) return;
            _searchText = value;
            OnPropertyChanged();
            Search();
        }
    }

    private void Search()
    {
       Stocks=new List<Stock>( _allStocks.Where(s => s.ProductsName.ToLower().Contains(SearchText.ToLower())));
    }

    private Action _closeAction;
    private string _searchText;


    public StockViewModel(IServiceProvider serviceProvider, StockRepository stockRepository, Warehouse house,WareHouseRepository wareHouseRepository)
    {
        _serviceProvider = serviceProvider;
        _allStocks = stockRepository.GetStocks(house);
        _stocks=new List<Stock>(_allStocks);
        _stockRepository = stockRepository;
        _house = house;
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
        win.Closed += (sender, args) =>
        {
            Stocks = _stockRepository.GetStocks(_house);
        };
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
        vm.SetClose(win.Close);
        
        win.Closed += InfStockWindowClosed;
        
        _stockWindow.Hide();
    }

    private void InfStockWindowClosed(object? sender, EventArgs e)
    {
        _stockWindow.Show();
    }

    [RelayCommand]
    public void Back()
    {
        var vm = ActivatorUtilities.CreateInstance<MainWindowViewModel>(
            _serviceProvider);
       
        var win = _serviceProvider.GetRequiredService<MainWindow>();
        win.DataContext = vm;
        win.Show();
        _closeAction.Invoke();
    }

    [RelayCommand]
    public void OutProduct()
    {
        var vm = ActivatorUtilities.CreateInstance<OutProductViewModel>(
            _serviceProvider, SelectedWarehouse);
       
        var win = _serviceProvider.GetRequiredService<OutProductWindow>();
        
        win.DataContext = vm;
        win.Show();
        vm.SetClose(win.Close);
       _closeAction?.Invoke();
    }

    public void SetClose(Action closeAction)
    {
        _closeAction = closeAction;
    }

    [RelayCommand]
    public void DeleteStock()
    {
        if (SelectedStock == null)
            return;

        if (_stockRepository.DeleteStock(SelectedStock.WarehouseId, SelectedStock.ProductId))
        {
            _allStocks = _stockRepository.GetStocks(_house);
            Stocks = new  List<Stock>(_allStocks);
        }


    }


    public void SetWindow(StockWindow win)
    {
        _stockWindow = win;
    }
}