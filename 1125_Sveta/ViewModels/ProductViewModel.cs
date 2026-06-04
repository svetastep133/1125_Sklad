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

public partial class ProductViewModel : ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ProductsRepository _productsRepository;
    [ObservableProperty] List<Product> _products;
    private Action _closeAction;
    [ObservableProperty] private Product? _selectedProduct;
    private List<Product> _allProducts;

    
    
    
    public ProductViewModel(IServiceProvider serviceProvider, ProductsRepository productsRepository)
    {
        _serviceProvider = serviceProvider;
        _productsRepository = productsRepository;
        _allProducts = _productsRepository.GetProducts();
        _products = new List<Product>(_allProducts);
    }

    public void SetClose(Action action)
    {
        _closeAction = action;
    }

    private string _searchText;

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
        Products = new List<Product>(_allProducts.Where(s => s.Name.ToLower().Contains(SearchText.ToLower())));
    }

    
    [RelayCommand]
    public void Edit()
    {
        if (SelectedProduct == null) 
            return;
        
        var vm = ActivatorUtilities.CreateInstance<AddProductViewModel>(
            _serviceProvider,SelectedProduct);
        var win = _serviceProvider.GetRequiredService<AddProductWindow>();
        win.DataContext = vm;
        win.Show();
        win.Closed += (sender, args) =>
        {
            Products = _productsRepository.GetProducts();
        };
        
        vm.SetClose(win.Close);
    }

    [RelayCommand]
    public void Delete()
    {
        if (SelectedProduct == null)
            return;

        if (_productsRepository.DeleteProduct(SelectedProduct.Id))
        {
            _allProducts = _productsRepository.GetProducts();
            Products = new List<Product>(_allProducts);
            MessageBoxWindow messageBox = new MessageBoxWindow(new MessageBoxViewModel("Товар удален"));

            messageBox.Show();
        }
    }
    [RelayCommand]
    public void Back()
    {
        _closeAction?.Invoke();
    }
    [RelayCommand]
    public void AddProduct()
    {
        var vm = ActivatorUtilities.CreateInstance<AddProductViewModel>(
            _serviceProvider, new Product());
        var win = _serviceProvider.GetRequiredService<AddProductWindow>();
        win.DataContext = vm;
        win.Show();
        win.Closed += (sender, args) =>
        {
            Products = _productsRepository.GetProducts();
        };
        
        vm.SetClose(win.Close);
    }
}