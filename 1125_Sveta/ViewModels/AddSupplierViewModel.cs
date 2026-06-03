using System;
using System.Collections.Generic;
using _1125_Sveta.Models;
using _1125_Sveta.Repository;
using _1125_Sveta.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace _1125_Sveta.ViewModels;

public partial class AddSupplierViewModel: ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly SuppliersRepository _suppliersRepository;

    [ObservableProperty] public List<Supplier> _suppliers;
    [ObservableProperty] public string _supplierName;
    [ObservableProperty] public string _supplierEmail;
    private Action _closeAction;
    private Supplier _editSupplier;

    public AddSupplierViewModel(IServiceProvider serviceProvider, SuppliersRepository  suppliersRepository, Supplier editSupplier)
    {
        _serviceProvider = serviceProvider;
        _suppliersRepository = suppliersRepository;
        _editSupplier = editSupplier;

        if (_editSupplier.Id != 0)
        {
            
            
        }
    }
    public void SetClose(Action action)
    {
        _closeAction = action;
    }

    [RelayCommand]
    public void AddSupplier()
    {
       
        _editSupplier.Name = SupplierName;
        _editSupplier.Email = SupplierEmail;
        if (_editSupplier.Id == 0)
        {
            _suppliersRepository.AddSupplier(_editSupplier);
                    MessageBoxWindow messageBox = new MessageBoxWindow(new MessageBoxViewModel("Поставщик добавлен")); 
                    messageBox.Show();
        }
        else
        {
            
        }
       
        _closeAction?.Invoke();  

    }
    
}