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

    const string connstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=wpfdatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    public static void loadDataTable(DataGrid dataGrid1)
    {
        using (SqlConnection sc = new SqlConnection(connstring))
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


    public static int convertint(string intputstr)
    {
        int outputid; //this is all just to parse out the int.
        var temp = Int32.TryParse(intputstr, out outputid);
        return outputid;
    }


    public static void rundbstring(string inputstring)
    {
        SqlConnection sqlConnection1 = new SqlConnection(connstring);
        SqlCommand cmd = new SqlCommand();

        cmd.CommandText = String.Format(inputstring);
        cmd.CommandType = CommandType.Text;
        cmd.Connection = sqlConnection1;

        sqlConnection1.Open();
        cmd.ExecuteNonQuery();
        sqlConnection1.Close();
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

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            string addUserString = String.Format("INSERT INTO Users(id, username, password, email) values ({0},'{1}','{2}','{3}')", dataops.convertint(textBoxAddid.Text), textBoxAddName.Text, textBoxAddEmail.Text, textBoxAddPass.Text);
            dataops.rundbstring(addUserString);
            dataops.loadDataTable(dataGrid);
            textBoxAddid.Clear();
            textBoxAddName.Clear();
            textBoxAddPass.Clear();
            textBoxAddEmail.Clear();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(dataGrid.SelectedIndex);
            //Console.WriteLine(dataGrid.);
            DataRowView dataRow = (DataRowView)dataGrid.SelectedItem;
            string userID = dataRow.Row.ItemArray[0].ToString();
            Console.WriteLine(userID);

            string deleteUserString = String.Format("DELETE FROM Users WHERE id = {0}", dataops.convertint(userID));
            dataops.rundbstring(deleteUserString);
            dataops.loadDataTable(dataGrid);
        }
    }
}
