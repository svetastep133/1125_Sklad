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

public partial class BuyerViewModel: ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly BuyerRepository _repository;
    [ObservableProperty] List<Buyer> _buyers;
    private List<Buyer> _allBuyers;
    [ObservableProperty] private Buyer? _selectedBuyers;

    public BuyerViewModel(IServiceProvider  serviceProvider, BuyerRepository  repository)
    {
        _serviceProvider = serviceProvider;
        _repository = repository;
        _allBuyers=_repository.GetBuyers();
        _buyers=new List<Buyer>(_allBuyers);
        
        
    }
    private string _searchText;
    private Action _closeAction;

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
        Buyers = new List<Buyer>(_allBuyers.Where(s => s.Name.ToLower().Contains(SearchText.ToLower())));
    }
    [RelayCommand]
    public void Edit()
    {
    }

    [RelayCommand]
    public void Delete()
    {
        if (SelectedBuyers == null)
            return;

        if (_repository.DeleteBuyer(SelectedBuyers.Id))
        {
            _allBuyers = _repository.GetBuyers();
            Buyers = new List<Buyer>(_allBuyers);
            MessageBoxWindow messageBox = new MessageBoxWindow(new MessageBoxViewModel("Покупатель удален"));

            messageBox.Show();
        }
        
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
    
    [RelayCommand]
    public void AddBuyer()
    {var vm = ActivatorUtilities.CreateInstance<AddBuyerViewModel>(
            _serviceProvider);
        var win = _serviceProvider.GetRequiredService<AddBuyerWindow>();
        win.DataContext = vm;
        win.Show();
        win.Closed += (sender, args) =>
        {
            Buyers = _repository.GetBuyers();
        };
        
        vm.SetClose(win.Close);
        
    }
}