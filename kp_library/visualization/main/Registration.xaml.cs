using Db_lib.Model;
using Db_lib;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Main_kp;

/// <summary>
/// Логика взаимодействия для Registration.xaml
/// </summary>
public partial class Registration : Window
{
    public Registration()
    {
        InitializeComponent();
        Closing += Registration_Closing;
    }

    private void Registration_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (MainWindow.Instance != null)
        {
            MainWindow.Instance.Close();
        }
    }

    private void addReader_Button_Click(object sender, RoutedEventArgs e)
    {
        using (var context = new KpContext())
        {
            try
            {
                string[] text_fields = { firstNameField_TextBox.Text.ToString(),
                                            lastNameField_TextBox.Text.ToString(),
                                            middleNameField_TextBox.Text.ToString(),
                                            addressField_TextBox.Text.ToString(),
                                            phoneNumberField_TextBox.Text.ToString() };
                if (text_fields.Any(Checker.is_empty))
                    throw new Exception("Одно или несколько полей пустые");
                else
                {
                    string fullName = text_fields[1] + " " 
                        + text_fields[0] + " " + text_fields[2];

                    if (!Checker.textField_is_valid(fullName))
                        throw new Exception($"Неверный формат имени/фамилии/отчества");

                    if (!Checker.phoneNumber_is_valid(text_fields[4]))
                        throw new Exception("Неверный формат номера телефона");

                    var reader = new Visitor
                    {
                        IssuanceId = 1,
                        FirstName = text_fields[0],
                        LastName = text_fields[1],
                        MiddleName = text_fields[2],
                        Addres = text_fields[3],
                        VisitorPhoneNumber = Checker.phoneNumber_to_format(text_fields[4])
                    };

                    context.Visitors.Add(reader);
                    context.SaveChanges();

                    MessageBox.Show($"Читатель {fullName} был успешно добавлен!");
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
    }

    private void buttonTo_MainWindow_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        this.Hide();
    }

    private void buttonTo_Issuance_Click(object sender, RoutedEventArgs e)
    {
        Issuance issuance = new Issuance();
        issuance.Show();
        this.Hide();
    }
}
