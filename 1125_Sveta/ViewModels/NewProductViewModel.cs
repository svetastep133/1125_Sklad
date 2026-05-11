using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using _1125_Sveta.Models;
using _1125_Sveta.Repository;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace _1125_Sveta.ViewModels;

public partial class NewProductViewModel:ViewModelBase
{
    private readonly StockRepository _stockRepository;
    private readonly ProductsRepository _productsRepository;
    private readonly CategoryRepository _categoryRepository;
    private readonly NewProductRepository _newProductRepository;
    
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
    private string _productName;
    [ObservableProperty]
    private string _productUnit;
    [ObservableProperty]
    private string _productWeight;
    
    [ObservableProperty]
    private string _incomingDocNumber;

    [ObservableProperty] 
    private string _incomingItemQuantity;
    [ObservableProperty]
    private string _incomingItemCost;
    
    [ObservableProperty]
    private string _stockQuantity;
    [ObservableProperty]
    private string _stockReserved;
    
    public NewProductViewModel(StockRepository stockRepository,ProductsRepository productsRepository,CategoryRepository categoryRepository, NewProductRepository newProductRepository)
    {
        _stockRepository = stockRepository;
        _productsRepository = productsRepository;
        _categoryRepository = categoryRepository;
        _newProductRepository = newProductRepository;
        Categories = _categoryRepository.GetCategories();
    }
    
    [RelayCommand]
     public void SaveProduct()
     {
         Product product = new Product();
         product.Name = ProductName;
         product.Unit = ProductUnit;
         product.Weight = decimal.Parse(ProductWeight);
         product.Category_id = SelectedCategory.Id;

         Incoming incoming = new();
         incoming.Supplier_id = SelectedSupplier.Id;
         incoming.Warehouse_id = SelectedWarehouse.Id;
         incoming.DocNumber = IncomingDocNumber;
         incoming.Date = DateTime.Now;
         
         IncomingItem incomingItem = new();
         incomingItem.Quantity = int.Parse(IncomingItemQuantity);
         incomingItem.Cost = int.Parse(IncomingItemCost);
         
         Stock stock = new Stock();
         stock.Quantity = int.Parse(StockQuantity);
         stock.LastUpdated = DateTime.Now;
         stock.Reserved = int.Parse(StockReserved);
         
         _newProductRepository.SaveProduct(product, SelectedCategory, incoming, incomingItem, stock, SelectedWarehouse, SelectedSupplier);
     }
    
}