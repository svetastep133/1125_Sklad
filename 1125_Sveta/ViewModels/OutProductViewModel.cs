using System;
using System.Collections.Generic;
using _1125_Sveta.Models;
using _1125_Sveta.Repository;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace _1125_Sveta.ViewModels;

public partial class OutProductViewModel : ViewModelBase
{
    private readonly StockRepository _stockRepository;
    private readonly ProductsRepository _productsRepository;
    private readonly CategoryRepository _categoryRepository;
    private readonly OutProductRepository _outProductRepository;
    private readonly WareHouseRepository _warehouseRepository;
    private readonly SuppliersRepository _supplierRepository;
    private readonly Warehouse _currentWarehouse;


    private Product _product;
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
    private string _outgoingDocNumber;

    [ObservableProperty] 
    private string _outgoingItemQuantity;
    [ObservableProperty]
    private string _outgoingItemCost;
    
  
    [ObservableProperty]
    private string _stockReserved;

    public OutProductViewModel(StockRepository stockRepository,
        ProductsRepository productsRepository, CategoryRepository categoryRepository,
        OutProductRepository outProductRepository,
        WareHouseRepository warehouseRepository, SuppliersRepository supplierRepository, Warehouse currentWarehouse)
    {
        _stockRepository = stockRepository;
        _productsRepository = productsRepository;
        _categoryRepository = categoryRepository;
        _outProductRepository = outProductRepository;
        _warehouseRepository = warehouseRepository;
        _supplierRepository = supplierRepository;
        SelectedWarehouse = currentWarehouse;
        Categories = _categoryRepository.GetCategories();
        Warehouses= _warehouseRepository.GetWarehouses();
        Suppliers= _supplierRepository.GetSuppliers();
        Products = _productsRepository.GetProducts();
    }
    
    private Action _closeAction;
    
    public void SetClose(Action action)
    {
        _closeAction = action;
    }
    
    
    [RelayCommand]
    public void SaveOutProduct()
    {
        Outgoing outgoing = new();
        outgoing.SupplierId = SelectedSupplier.Id;
        outgoing.WarehouseId = SelectedWarehouse.Id;
        outgoing.DocNumber = OutgoingDocNumber;
        outgoing.Date = DateTime.Now;
         
        OutgoingItem outgoingItem = new();
        outgoingItem.Quantity = int.Parse(OutgoingItemQuantity);
        outgoingItem.Cost = int.Parse(OutgoingItemCost);
         
        Stock stock = new Stock();
        stock.LastUpdated = DateTime.Now;
         
        _outProductRepository.SaveProduct(SelectedProduct, outgoing, outgoingItem, stock, SelectedWarehouse, SelectedSupplier);
        _closeAction?.Invoke();  
    }
}