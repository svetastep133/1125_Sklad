using System;
using System.Collections.Generic;
using _1125_Sveta.Models;
using _1125_Sveta.Repository;
using _1125_Sveta.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace _1125_Sveta.ViewModels;

public partial class AddBuyerViewModel:ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly BuyerRepository _buyerRepository;
    private readonly Buyer _editBuyer;
    [ObservableProperty] public List<Buyer> _buyers;
    [ObservableProperty] public string _buyerName;
    [ObservableProperty] public string _buyerEmail;
    private Action _closeAction;

    public AddBuyerViewModel(IServiceProvider serviceProvider, BuyerRepository  buyerRepository,Buyer editBuyer)
    {
        _serviceProvider = serviceProvider;
        _buyerRepository = buyerRepository;
        _editBuyer = editBuyer;
        if (_editBuyer.Id !=0)
        {
            BuyerName= _editBuyer.Name;
            BuyerEmail = _editBuyer.Email;
        }
    }
    public void SetClose(Action action)
    {
        _closeAction = action;
    }

    [RelayCommand]
    public void AddBuyer()
    {
      
        _editBuyer.Name = BuyerName;
        _editBuyer.Email = BuyerEmail;
        if (_editBuyer.Id==0)
        {
            _buyerRepository.AddBuyer(_editBuyer);
            
        MessageBoxWindow messageBox = new MessageBoxWindow(new MessageBoxViewModel("Покупатель добавлен"));

        messageBox.Show();
        }
        else
        {
            _buyerRepository.Update(_editBuyer);
            
            MessageBoxWindow messageBox = new MessageBoxWindow(new MessageBoxViewModel("Покупатель обновлен"));

            messageBox.Show();
            
        }
          _closeAction?.Invoke();  
    }

    

    
    
}