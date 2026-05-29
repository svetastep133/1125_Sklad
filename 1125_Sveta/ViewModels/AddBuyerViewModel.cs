using System;
using System.Collections.Generic;
using _1125_Sveta.Models;
using _1125_Sveta.Repository;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace _1125_Sveta.ViewModels;

public partial class AddBuyerViewModel:ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly BuyerRepository _buyerRepository;
    [ObservableProperty] public List<Buyer> _buyers;
    [ObservableProperty] public string _buyerName;
    private Action _closeAction;

    public AddBuyerViewModel(IServiceProvider serviceProvider, BuyerRepository  buyerRepository)
    {
        _serviceProvider = serviceProvider;
        _buyerRepository = buyerRepository;
    }
    public void SetClose(Action action)
    {
        _closeAction = action;
    }

    [RelayCommand]
    public void AddBuyer()
    {
        Buyer buyer=new  Buyer();
        buyer.Name = BuyerName;
        _buyerRepository.AddBuyer(buyer);
        _closeAction?.Invoke();  

    }
    
}