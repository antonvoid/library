using Db_lib;
using Db_lib.Model;
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
/// Логика взаимодействия для Issuance.xaml
/// </summary>
public partial class Issuance : Window
{
    public Issuance()
    {
        InitializeComponent();
        Closing += Issuance_Closing;
    }

    private void buttonMake_Issuance_Click(object sender, RoutedEventArgs e)
    {
        using (var context = new KpContext())
        {
            try
            {
                string[] text_fields = { Reader_Id_TextBox.Text.ToString(),
                                        BookName_TextBox.Text.ToString() };
                if (text_fields.Any(Checker.is_empty))
                    throw new Exception("Одно или несколько полей пустые!");
                else
                {
                    if (!Checker.textField_is_valid(text_fields[1]))
                        throw new Exception("Неверный формат названия книги!");

                    var visitor = context.Visitors.Find(int.Parse(text_fields[0]));
                    if (!Checker.numField_is_valid(text_fields[0]))
                        throw new Exception("Неверный формат читательского билета!");
                    else if (visitor == null)
                        throw new Exception("Такого читательского билета не существует. " +
                            "Зарегестрируйте читателя.");

                    var book = context.Books.FirstOrDefault(x => x.Title == text_fields[1]);
                    if (book != null)
                    {
                        var issuance = new Db_lib.Model.Issuance
                        {
                            VisitorId = visitor.VisitorId,
                            DateOfIssuance = DateOnly.FromDateTime(DateTime.Now),
                            DateOfReturn = DateOnly.FromDateTime(DateTime.Now.AddDays(30))
                        };

                        issuance.Books.Add(book);

                        context.Issuances.Add(issuance);
                        context.SaveChanges();

                        MessageBox.Show($"Выдача книги {book.Title} " +
                            $"читателю {visitor.LastName + " " + visitor.FirstName + 
                            " " + visitor.MiddleName}" +
                            $" была успешно зарегестрирована!", "Выдача успешна",
                            MessageBoxButton.OK);
                    }
                    else throw new Exception("Такой книги в нашей библиотеке нет!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
    }

    private void buttonTo_Home_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        this.Hide();
    }

    private void Issuance_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (MainWindow.Instance != null)
        {
            MainWindow.Instance.Close();
        }
    }
}
