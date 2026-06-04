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

public partial class SupplierViewModel: ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly SuppliersRepository _suppliersRepository;
    [ObservableProperty] List<Supplier> _suppliers;
    private Action _closeAction;
    [ObservableProperty] private Supplier? _selectedSupplier;
    private List<Supplier> _allSuppliers;

    
    
    
    public SupplierViewModel(IServiceProvider serviceProvider, SuppliersRepository suppliersRepository)
    {
        _serviceProvider = serviceProvider;
        _suppliersRepository = suppliersRepository;

        _allSuppliers = _suppliersRepository.GetSuppliers();
        _suppliers = new List<Supplier>(_allSuppliers);
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
        Suppliers = new List<Supplier>(_allSuppliers.Where(s => s.Name.ToLower().Contains(SearchText.ToLower())));
    }

    [RelayCommand]
    public void Edit()
    {
        if (SelectedSupplier == null) 
            return;
        
        var vm = ActivatorUtilities.CreateInstance<AddSupplierViewModel>(
            _serviceProvider,SelectedSupplier);
        var win = _serviceProvider.GetRequiredService<AddSupplierWindow>();
        win.DataContext = vm;
        win.Show();
        win.Closed += (sender, args) =>
        {
            Suppliers = _suppliersRepository.GetSuppliers();
        };
        
        vm.SetClose(win.Close);
    }

    [RelayCommand]
    public void Delete()
    {
        if (SelectedSupplier == null)
            return;

        if (_suppliersRepository.DeleteSupplier(SelectedSupplier.Id))
        {
            _allSuppliers = _suppliersRepository.GetSuppliers();
            Suppliers = new List<Supplier>(_allSuppliers);
            MessageBoxWindow messageBox = new MessageBoxWindow(new MessageBoxViewModel("Поставщик удален"));

            messageBox.Show();
        }
    }

    [RelayCommand]
    public void Back()
    {
        _closeAction?.Invoke();
    }
    [RelayCommand]
    public void AddSupplier()
    {
        
        var vm = ActivatorUtilities.CreateInstance<AddSupplierViewModel>(
            _serviceProvider, new Supplier());
        var win = _serviceProvider.GetRequiredService<AddSupplierWindow>();
        win.DataContext = vm;
        win.Show();
        win.Closed += (sender, args) =>
        {
            Suppliers = _suppliersRepository.GetSuppliers();
        };
        
        vm.SetClose(win.Close);
        
    }

    
}