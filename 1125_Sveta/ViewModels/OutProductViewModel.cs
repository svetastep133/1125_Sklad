using System;
using System.Collections.Generic;
using _1125_Sveta.Models;
using _1125_Sveta.Repository;
using _1125_Sveta.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace _1125_Sveta.ViewModels;

public partial class OutProductViewModel : ViewModelBase
{
    [ObservableProperty] private Stock _selectedStock;
    private readonly IServiceProvider _serviceProvider;
    private readonly ProductsRepository _productsRepository;
    private readonly StockRepository _stockRepository;
    private readonly CategoryRepository _categoryRepository;
    private readonly OutProductRepository _outProductRepository;
    private readonly WareHouseRepository _warehouseRepository;
    private readonly BuyerRepository _buyerRepository;
    private readonly Warehouse _currentWarehouse;


    [ObservableProperty] List<Stock> _stocks;
    
    private Product _product;
    
    [ObservableProperty]
    private Buyer _selectedBuyer;
    [ObservableProperty]
    private List<Buyer> _buyer;

    [ObservableProperty]
    private Warehouse _selectedWarehouse;
    [ObservableProperty]
    private List<Warehouse> _warehouses;
    
    [ObservableProperty]
    private Product _selectedProduct;
    [ObservableProperty] 
    private List<Product> _products;
    
    [ObservableProperty]
    private string _outgoingDocNumber;

    [ObservableProperty] 
    private string _outgoingItemQuantity;
    [ObservableProperty]
    private string _outgoingItemCost;


    
    

    public OutProductViewModel(IServiceProvider serviceProvider,Stock stock,
        ProductsRepository productsRepository, StockRepository stockRepository, OutProductRepository outProductRepository,
        WareHouseRepository warehouseRepository, BuyerRepository buyerRepository, Warehouse currentWarehouse)
    {
        SelectedStock = stock;
        _serviceProvider = serviceProvider;
        _productsRepository = productsRepository;
        _stockRepository = stockRepository;

        _outProductRepository = outProductRepository;
        _warehouseRepository = warehouseRepository;
        _buyerRepository = buyerRepository;
        SelectedWarehouse = currentWarehouse;
        Warehouses= _warehouseRepository.GetWarehouses();
        Buyer=_buyerRepository.GetBuyers();
        Products = _productsRepository.GetProducts();
        Stocks = _stockRepository.GetStocks(currentWarehouse);
    }
    
    private Action _closeAction;
    
    public void SetClose(Action action)
    {
        _closeAction = action;
    }

   

    [RelayCommand]
    public void SaveOutProduct()
    {
        try
        {
            Outgoing outgoing = new();
            outgoing.BuyerId = SelectedBuyer.Id;
            outgoing.WarehouseId = SelectedWarehouse.Id;
            outgoing.DocNumber = OutgoingDocNumber;
            outgoing.Date = DateTime.Now;

            OutgoingItem outgoingItem = new();
            outgoingItem.Quantity = int.Parse(OutgoingItemQuantity);
            outgoingItem.Cost = int.Parse(OutgoingItemCost);

            _outProductRepository.SaveProduct(outgoing, outgoingItem, SelectedStock);

            MessageBoxWindow messageBox = new MessageBoxWindow(new MessageBoxViewModel("Товар успешно отгружен"));
         
            messageBox.Show();

            _closeAction?.Invoke();
        }
        catch (Exception ex)
        {
            MessageBoxWindow messageBox = new MessageBoxWindow(new MessageBoxViewModel(ex.Message));
         
            messageBox.Show();
        }
    }
}