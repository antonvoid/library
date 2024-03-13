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
/// Логика взаимодействия для Issuance2.xaml
/// </summary>
public partial class Issuance2 : Window
{
    public Issuance2()
    {
        InitializeComponent();
        Closing += Issuance2_Closing;
    }

    private void buttonTo_MainWindow_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        this.Hide();
    }

    private void Issuance2_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (MainWindow.Instance != null)
        {
            MainWindow.Instance.Close();
        }
    }

    private void buttonShow_Info_Click(object sender, RoutedEventArgs e)
    {
        using (var context = new KpContext())
        {
            try
            {
                string[] text_fields = { Reader_Id_TextBox.Text.ToString(),
                                        BookName_TextBox.Text.ToString() };
                if (text_fields.All(Checker.is_empty))
                    throw new Exception("Поля пустые!");
                else
                {
                    if (!Checker.is_empty(text_fields[1]))
                    {
                        if (!Checker.textField_is_valid(text_fields[1]))
                            throw new Exception("Неверный формат названия книги!");

                        var book = context.Books.FirstOrDefault(x => x.Title == text_fields[1]);
                        if (book == null) throw new Exception("Такой книги в нашей библиотеке нет!");
                        else
                        {
                            if (!Checker.is_empty(text_fields[0]))
                            {
                                // Оба не пустые
                                if (!Checker.numField_is_valid(text_fields[0]))
                                    throw new Exception("Неверный формат читательского билета!");

                                var visitor = context.Visitors.Find(int.Parse(text_fields[0]));
                                if (visitor == null) throw new Exception("Такого номера читательского билета не существует!");
                                else
                                {
                                    Db_lib.Model.Issuance issuance = context.Issuances.FirstOrDefault(x => x.VisitorId == visitor.VisitorId);
                                    var issuanceInfo = context.Issuances
                                    .Where(issuance => issuance.VisitorId == visitor.VisitorId &&
                                                      issuance.Books.Any(b => b.Title == book.Title))
                                    .Select(issuance => new
                                    {
                                        VisitorName = $"{issuance.Visitor.LastName} {issuance.Visitor.FirstName} {issuance.Visitor.MiddleName}",
                                        BookTitle = book.Title,
                                        DateOfIssuance = issuance.DateOfIssuance,
                                        DateOfReturn = issuance.DateOfReturn
                                    })
                                    .FirstOrDefault();

                                    if (issuanceInfo != null)
                                    {
                                        Issuance_Info_TextBlock.Text = $"Имя читателя: {issuanceInfo.VisitorName}\n" +
                                            $"Название книги: {issuanceInfo.BookTitle}\n" +
                                            $"Дата выдачи: {issuanceInfo.DateOfIssuance}\n" +
                                            $"Дата возврата: {issuanceInfo.DateOfReturn}";
                                    }
                                    else throw new Exception("Такой выдачи не существует!");
                                }
                            }
                            else
                            {
                                // Книга не пустая, читательский билет пустой

                                var visitorIds = context.Issuances
                                    .Where(issuance => issuance.Books.Any(b => b.Title == book.Title))
                                    .Select(issuance => issuance.VisitorId)
                                    .ToList();

                                Issuance_Info_TextBlock.Text = "Читатели, бравшие эту книгу:";

                                foreach (var visitorId in visitorIds)
                                {
                                    var visitorNames = context.Visitors
                                        .Where(visitor => visitor.VisitorId == visitorId)
                                        .Select(visitor => $"{visitor.LastName} {visitor.FirstName} {visitor.MiddleName}")
                                        .ToList();

                                    foreach (var name in visitorNames)
                                        Issuance_Info_TextBlock.Text += $" {name};";
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!Checker.is_empty(text_fields[0]))
                        {
                            // Книга пустая, читательский билет не пустой
                            if (!Checker.numField_is_valid(text_fields[0]))
                                throw new Exception("Неверный формат читательского билета!");

                            var visitor = context.Visitors.Find(int.Parse(text_fields[0]));
                            if (visitor == null)
                                throw new Exception("Такого читательского билета нет!");
                            else 
                            {
                                var booksForVisitor = context.Issuances
                                .Where(issuance => issuance.VisitorId == visitor.VisitorId)
                                .SelectMany(issuance => issuance.Books)
                                .ToList();

                                if (booksForVisitor.Count == 0) throw new Exception("Выдачи, связанные с этим читателем не найдены!");
                                else
                                    Issuance_Info_TextBlock.Text += " ";
                                foreach (var book in booksForVisitor)
                                    Issuance_Info_TextBlock.Text += $"{book.Title}; ";
                            }
                        } 
                    }
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
}
