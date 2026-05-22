using System;
using System.Collections.Generic;
using _1125_Sveta.Models;
using _1125_Sveta.Repository;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace _1125_Sveta.ViewModels;

public partial class InfStockViewModel: ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly InfRepository _infRepository;
    

    [ObservableProperty] Stock _stock;
    [ObservableProperty] private Stock _selectedStock;
    [ObservableProperty] private Product? _selectedProduct;
    [ObservableProperty] List<Product?> _products;

    public InfStockViewModel(IServiceProvider serviceProvider, Stock stock)
    {
        _serviceProvider = serviceProvider;
        SelectedStock = stock;
        _infRepository = _serviceProvider.GetService<InfRepository>();
        Products = _infRepository.GetInfInc(stock.ProductId);

    }
    
    
    
}