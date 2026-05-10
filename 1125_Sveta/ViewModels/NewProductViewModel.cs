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

    
    public NewProductViewModel(StockRepository stockRepository,ProductsRepository productsRepository,CategoryRepository categoryRepository)
    {
        _stockRepository = stockRepository;
        _productsRepository = productsRepository;
        _categoryRepository = categoryRepository;
        
    }
    
    [RelayCommand]
     public void SaveProduct()
     {
         
     }
    
}