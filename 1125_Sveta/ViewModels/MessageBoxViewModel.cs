using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace _1125_Sveta.ViewModels;

public partial class MessageBoxViewModel: ViewModelBase
{
        [ObservableProperty]
        private string _message;

        private Action? _closeAction;

        public MessageBoxViewModel(string message)
        {
            Message = message;
        }

        public void SetClose(Action action)
        {
            _closeAction = action;
        }

        [RelayCommand]
        private void Close()
        {
            _closeAction?.Invoke();
        }
}