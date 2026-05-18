using System;
using System.Collections.Generic;
using System.Windows.Input;
using _1125_Sveta.Models;
using _1125_Sveta.Repository;
using _1125_Sveta.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace _1125_Sveta.ViewModels;

public partial class AddProductviewModel: ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ProductsRepository _productsRepository;
    private readonly CategoryRepository _categoryRepository;
    [ObservableProperty] List<Category> _categories;
     private Action _closeAction;
    
    [ObservableProperty]
    private string _productName;
    [ObservableProperty]
    private string _productWeight;
    
    [ObservableProperty]
    private Category _selectedCategory;

    public AddProductviewModel(IServiceProvider  serviceProvider, ProductsRepository productsRepository,CategoryRepository categoryRepository)
    {
        _serviceProvider = serviceProvider;
        _productsRepository = productsRepository;
        _categoryRepository = categoryRepository;
        Categories = _categoryRepository.GetCategories();
    }

    public void SetClose(Action action)
    {
        _closeAction = action;
    }
    
    [RelayCommand]
    public void AddPRoduct()
    {
        if(SelectedCategory==null)
            return;
        
        Product product = new Product();
        product.Name = ProductName;
        product.Weight = decimal.Parse(ProductWeight);
        product.CategoryId = SelectedCategory.Id;
        
        _productsRepository.AddProduct(product);
        
        _closeAction?.Invoke();  
        
    }
}