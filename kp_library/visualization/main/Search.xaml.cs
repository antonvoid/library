using Db_lib.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
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
    /// Логика взаимодействия для Search.xaml
    /// </summary>
public partial class Search : Window
{
    //private readonly KpContext context = new KpContext();

    public Search()
    {
        InitializeComponent();
        Closing += Search_Closing;
    }

    private void buttonTo_Home_Click(object sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        this.Hide();
    }

    private void Search_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (MainWindow.Instance != null)
        {
            MainWindow.Instance.Close();
        }
    }

    private async void SearchTextBox_TextChanged(object sender, RoutedEventArgs e)
    {
        using (var context = new KpContext())
        {
            try
            {
                string searchTerm = searchTextBox.Text.ToLower();
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    List<object> searchResults = await SearchAsync(searchTerm);

                    foreach (object entity in searchResults)
                    {
                        if (entity is Book book)
                        {
                            // Обработка сущности типа Book
                            // Например: book.Title, book.Author, и так далее

                            // Явная загрузка связанных сущностей
                            book = await context.Books
                                .Include(b => b.Series)
                                .Include(b => b.Country)
                                .Include(b => b.Place)
                                .Include(b => b.Publisher)
                                .Include(b => b.Genre)
                                .Where(b => b.BookId == book.BookId)
                                .FirstOrDefaultAsync();

                            resultsTextBlock.Text = "Результаты поиска:\n" +
                                $"Название книги: {book.Title}\n" +
                                $"Серия: {book.Series?.Title}\n" +
                                $"Страна: {book.Country?.CountryName}\n" +
                                $"Место: зал {book.Place?.Room}, ряд {book.Place?.Line}, полка {book.Place?.Shelf}, позиция {book.Place?.Position}\n" +
                                $"Издательство: {book.Publisher?.PublisherTitle} г. {book.Publisher?.City}\n" +
                                $"Жанр: {book.Genre?.GenreTitle}\n" +
                                $"Количество страниц: {book.NumberOfPages}";
                        }
                        else if (entity is Series series)
                        {
                            // Обработка сущности типа Series
                            // Например: series.Title, series.SeasonCount, и так далее
                            if (series.SeriesId == 1)
                                resultsTextBlock.Text = "Результаты поиска: у данной книги нет серии";
                            else
                            {
                                var booksInSeries = await context.Books
                                .Where(b => b.SeriesId == series.SeriesId)
                                .ToListAsync();
                                resultsTextBlock.Text = "Результаты поиска:\n" +
                                    $"Серия: {series.Title}\n" +
                                    "Книги, входящие в серию: ";
                                foreach (Book b in booksInSeries)
                                    resultsTextBlock.Text += b.Title + "; ";
                            }
                        }
                        else if (entity is Author author)
                        {
                            // Обработка сущности типа Author
                            // Например: author.FirstName, author.LastName, и так далее
                            var booksOfAuthor = await context.Books
                                .Where(b => b.AuthorId == author.AuthorId)
                                .ToListAsync();

                            string fullName;
                            resultsTextBlock.Text = "Результаты поиска:\n";
                            if (string.IsNullOrWhiteSpace(author.MiddleName))
                                fullName = author.FirstName + " " + author.LastName;
                            else
                                fullName = author.FirstName + " " + author.MiddleName + " " + author.LastName;

                            resultsTextBlock.Text += $"Полное имя: {fullName}\n" +
                                $"Страна: {author.Country}\n" +
                                $"Дата рождения: {author.DateOfBirth}\n" +
                                $"Дата смерти: {author.DateOfDeath}\n" +
                                $"Книги, написанные этим автором: ";
                            foreach (Book b in booksOfAuthor)
                                resultsTextBlock.Text += b.Title + "; ";
                        }
                    }

                    // Обновление TextBlock с результатами поиска
                    //resultsTextBlock.Text = "Результат поиска: " + string.Join(Environment.NewLine, searchResults);
                }
                else resultsTextBlock.Text = "Результат поиска: ";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
    }

    private async Task<List<object>> SearchAsync(string searchTerm)
    {
        using (var context = new KpContext())
        {
            var results1 = await context.Books
            .Where(e => EF.Functions.Like(e.Title.ToLower(), $"%{searchTerm}%"))
            .ToListAsync();

            var results2 = await context.Series
                .Where(e => EF.Functions.Like(e.Title.ToLower(), $"%{searchTerm}%"))
                .ToListAsync();

            var results3 = await context.Authors
                .Where(e =>
                EF.Functions.Like(e.LastName.ToLower(), $"%{searchTerm}%") ||
                EF.Functions.Like(e.FirstName.ToLower(), $"%{searchTerm}%") ||
                EF.Functions.Like(e.MiddleName.ToLower(), $"%{searchTerm}%"))
                .ToListAsync();

            var searchResults = new List<object>();
            searchResults.AddRange(results1);
            searchResults.AddRange(results2);
            searchResults.AddRange(results3);

            return searchResults;
        }
    }
}
