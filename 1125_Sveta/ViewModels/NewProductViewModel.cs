using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using _1125_Sveta.Models;
using _1125_Sveta.Repository;
using _1125_Sveta.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace _1125_Sveta.ViewModels;

public partial class NewProductViewModel:ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly StockRepository _stockRepository;
    private readonly ProductsRepository _productsRepository;
    private readonly CategoryRepository _categoryRepository;
    private readonly NewProductRepository _newProductRepository;
    private readonly WareHouseRepository _warehouseRepository;
    private readonly SuppliersRepository _supplierRepository;
    private readonly Warehouse _currentWarehouse;


    private Product _product;
    
    
    private Action _closeAction;
    [ObservableProperty]
    private Category _selectedCategory;
    [ObservableProperty]
    private List<Category> _categories;
    
    [ObservableProperty]
    private Supplier _selectedSupplier;
    [ObservableProperty]
    private List<Supplier> _suppliers;

    [ObservableProperty]
    private Warehouse _selectedWarehouse;
    [ObservableProperty]
    private List<Warehouse> _warehouses;
    
    [ObservableProperty]
   private Product _selectedProduct;
   [ObservableProperty] 
   private List<Product> _products;
    
    [ObservableProperty]
    private string _incomingDocNumber;

    [ObservableProperty] 
    private string _incomingItemQuantity;
    [ObservableProperty]
    private string _incomingItemCost;
    
  
    public NewProductViewModel(IServiceProvider serviceProvider,StockRepository stockRepository,
        ProductsRepository productsRepository,CategoryRepository categoryRepository, NewProductRepository newProductRepository, 
        WareHouseRepository warehouseRepository, SuppliersRepository supplierRepository, Warehouse currentWarehouse)
    {
        _serviceProvider = serviceProvider;
        _stockRepository = stockRepository;
        _productsRepository = productsRepository;
        _categoryRepository = categoryRepository;
        _newProductRepository = newProductRepository;
        _warehouseRepository = warehouseRepository;
        _supplierRepository = supplierRepository;
        SelectedWarehouse = currentWarehouse;
        Categories = _categoryRepository.GetCategories();
        Warehouses= _warehouseRepository.GetWarehouses();
        Suppliers= _supplierRepository.GetSuppliers();
        Products = _productsRepository.GetProducts();
    }
    public void SetClose(Action action)
    {
        _closeAction = action;
    }
    [RelayCommand]
     public void SaveProduct()
     {
         Incoming incoming = new();
         incoming.SupplierId = SelectedSupplier.Id;
         incoming.WarehouseId = SelectedWarehouse.Id;
         incoming.DocNumber = IncomingDocNumber;
         incoming.Date = DateTime.Now;
         
         IncomingItem incomingItem = new();
         incomingItem.Quantity = int.Parse(IncomingItemQuantity);
         incomingItem.Cost = int.Parse(IncomingItemCost);
         
         Stock stock = new Stock();
         stock.LastUpdated = DateTime.Now;
         
         _newProductRepository.SaveProduct(SelectedProduct, incoming, incomingItem, stock, SelectedWarehouse, SelectedSupplier);
         _closeAction?.Invoke();
         
         MessageBoxWindow messageBox = new MessageBoxWindow(new MessageBoxViewModel("Товар успешно добавлен"));
         
         messageBox.Show();
     }
    
}