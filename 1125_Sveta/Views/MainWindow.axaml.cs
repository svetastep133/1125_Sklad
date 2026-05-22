using _1125_Sveta.ViewModels;
using Avalonia.Controls;

namespace _1125_Sveta.Views;

public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel vm)
    {
        DataContext = vm;
        InitializeComponent();
        vm.SetWindow(this);
    }
}