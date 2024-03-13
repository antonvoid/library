using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAnimatedGif;


namespace Main_kp;
/// <summary>
/// Логика взаимодействия для MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public static MainWindow? Instance { get; private set; }
    public MainWindow()
    {
        InitializeComponent();
        Instance = this;
    }

    private void buttonTo_registration_Click(object sender, RoutedEventArgs e)
    {
        Registration registration = new Registration();
        registration.Show();
        this.Hide();
    }

    private void buttonOff_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Закрыть приложение?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            Application.Current.Shutdown();
        }
    }

    private void buttonTo_Search_Click(object sender, RoutedEventArgs e)
    {
        Search search = new Search();
        search.Show();
        this.Hide();
    }

    private void buttonTo_Issuance_Click(object sender, RoutedEventArgs e)
    {
        ChooseIssuance chooseIssuance = new ChooseIssuance();
        chooseIssuance.Show();
        this.Hide();
    }
}
