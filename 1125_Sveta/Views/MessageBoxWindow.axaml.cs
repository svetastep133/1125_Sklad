using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace _1125_Sveta.Views;

public partial class MessageBoxWindow : Window
{
    public MessageBoxWindow(ViewModels.MessageBoxViewModel viewModel)
    {
        InitializeComponent();
        
        viewModel.SetClose(Close);
        DataContext = viewModel;
    }
}