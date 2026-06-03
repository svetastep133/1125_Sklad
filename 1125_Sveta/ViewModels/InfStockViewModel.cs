using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using _1125_Sveta.Models;
using _1125_Sveta.Repository;
using _1125_Sveta.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace _1125_Sveta.ViewModels;

public partial class InfStockViewModel: ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;
   
    private readonly InfRepository _infRepository;
    private string _searchText;
    private string _searchText1;
    private List<Product> _allBuyer;
    private List<Product> _allSuppliers;
   
    [ObservableProperty] Stock _stock;
    [ObservableProperty] private Stock _selectedStock;
    [ObservableProperty] private Product? _selectedProduct;
    [ObservableProperty] private Warehouse _selectedWarehouse;
    
   [ObservableProperty] private ObservableCollection<Product> _buyers;
   [ObservableProperty] private ObservableCollection<Product> _suppliers;

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
    
   public string SearchText1
   {
       get => _searchText1;
       set
       {
           if (value == _searchText1) return;
           _searchText1 = value;
           OnPropertyChanged();
           Search1();
       }
   }
    private Action _closeAction;
   
    public InfStockViewModel(IServiceProvider serviceProvider, Stock stock,InfRepository infRepository )
    {
        _serviceProvider = serviceProvider;
        _infRepository = infRepository;
        SelectedStock = stock;
        
      _allBuyer=_infRepository.GetInfQua(stock.ProductId);
      Buyers = new ObservableCollection<Product>(_allBuyer);
      
      _allSuppliers=_infRepository.GetInfInc(stock.ProductId);
      Suppliers = new ObservableCollection<Product>(_allSuppliers);

    }
 

    public void SetClose(Action action)
    {
        _closeAction = action;
    }
    [RelayCommand]
    public void Back()
    {
        _closeAction?.Invoke();
    }
    
    private void Search()
    {
        Buyers= new ObservableCollection<Product>(_allBuyer.Where(s => s.BuyerName.ToLower().Contains(SearchText.ToLower())));
    }

    private void Search1()
    {
        Suppliers = new ObservableCollection<Product>(_allSuppliers.Where(s => s.SupplierName.ToLower().Contains(SearchText1.ToLower())));
    }
}