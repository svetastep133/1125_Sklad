using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Windows.Input;
using _1125_Sveta.Models;
using _1125_Sveta.Repository;
using _1125_Sveta.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace _1125_Sveta.ViewModels;

public partial class AddProductViewModel: ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ProductsRepository _productsRepository;
    private readonly CategoryRepository _categoryRepository;
    [ObservableProperty] List<Category> _categories;
    [ObservableProperty] List<Product> _product;
     private Action _closeAction;
     public ObservableCollection<Product> Products { get; set; }
     private Product _editProduct;
    
    [ObservableProperty]
    private string _productName;
    [ObservableProperty]
    private Decimal _productWeight;
    
    [ObservableProperty]
    private Category _selectedCategory;

    public AddProductViewModel(IServiceProvider  serviceProvider, ProductsRepository productsRepository,CategoryRepository categoryRepository, Product editProduct)
    {
        _serviceProvider = serviceProvider;
        _productsRepository = productsRepository;
        _categoryRepository = categoryRepository;
        _editProduct = editProduct;
        Categories = _categoryRepository.GetCategories();
        if (_editProduct.Id != 0)
        {
            ProductName = _editProduct.Name;
            ProductWeight = _editProduct.Weight;
            SelectedCategory = Categories.FirstOrDefault(c => c.Id == _editProduct.CategoryId) ;
        }
    }

    public void SetClose(Action action)
    {
        _closeAction = action;
    }
    
 

    [RelayCommand]
    public void AddPRoduct()
    {
        if (SelectedCategory == null)
            return;
        _editProduct.Name = ProductName;
        _editProduct.Weight = ProductWeight;
        _editProduct.CategoryId = SelectedCategory.Id;
        if (_editProduct.Id == 0)
        {
            _productsRepository.AddProduct(_editProduct);
              MessageBoxWindow messageBox = new MessageBoxWindow(new MessageBoxViewModel("Товар успешно создан"));
                    messageBox.Show();
        }
        else 
        {
            _productsRepository.Update(_editProduct);
            MessageBoxWindow messageBox = new MessageBoxWindow(new MessageBoxViewModel("Товар успешно обновлен"));
            messageBox.Show();
            
        }
        
    _closeAction?.Invoke();  
        
    }
}