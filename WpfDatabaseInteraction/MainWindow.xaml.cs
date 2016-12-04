using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

public static class dataops {
    public static void loadDataTable(DataGrid dataGrid1)
    {
        using (SqlConnection sc = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=wpfdatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
        {
            sc.Open();
            string sql = "SELECT * FROM users";
            SqlCommand com = new SqlCommand(sql, sc);

                 
            using (SqlDataAdapter adapter = new SqlDataAdapter(com))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGrid1.ItemsSource = dt.DefaultView;
            }
        }
    }
}

namespace WpfDatabaseInteraction
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataops.loadDataTable(dataGrid);
        }
    }
}
