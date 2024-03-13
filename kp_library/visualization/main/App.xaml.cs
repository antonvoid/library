using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Main_kp;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
public partial class App : Application
{
    public static string GetConnectionString()
    {
        return "Host=127.0.0.1;Port=5432;Database=kp;Username=user1;Password=1234;SSL Mode=Prefer;Timeout=10;";
    }
}

