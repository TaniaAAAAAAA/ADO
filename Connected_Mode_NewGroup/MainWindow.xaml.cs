using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Connected_Mode_NewGroup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtLogin.IsEnabled = false;
            txtPassword.IsEnabled = false;
            txtAddressServer.IsEnabled = false;

            lbTables.ItemsSource = listObjectTables;
            cbDataBase.ItemsSource = listObject;
            lb_DataTable.ItemsSource = data_list;

            cbDataBase.IsEnabled=false;
       
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)//User Authentication
        {
          

        }

       private string connectionString = "";
        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e) 
        {

            txtLogin.IsEnabled = true;
            txtPassword.IsEnabled = true;
            txtAddressServer.IsEnabled = true;

            FlagIntegratedSecurity = false;
           
        }
        bool FlagIntegratedSecurity = true;
        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            FlagIntegratedSecurity = true;

          
        }

        ObservableCollection<object> listObject = new ObservableCollection<object>();
        ObservableCollection<object> listObjectTables = new ObservableCollection<object>();

        string InitialCatalog = "master";

        private void CreateConnectStr()
        {
            if (FlagIntegratedSecurity == true)
            {
                connectionString = $@"Data Source=(localdb)\MSSQLLocalDB;
                                        Initial catalog={InitialCatalog};
                                        Integrated Security=true;";
            }
            else
            {
                connectionString = $@"Data Source={txtAddressServer.Text};Initial Catalog={InitialCatalog};
                                        Integrated Security=false; User Id={txtLogin.Text}; Password={txtPassword.Text}";

            }
        }
        private void Button_Click(object sender, RoutedEventArgs e) //Connecting
        {
            cbDataBase.IsEnabled = true;
            CreateConnectStr();
           
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();


                    SqlCommand command = new SqlCommand();
                    command.CommandText = "select * from sys.databases";
                    command.Connection = connection;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                           
                           listObject.Add(reader[0]);

                       
                        }
                    }
                    reader.Close();

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
          
        }

        private void cbDataBase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listObjectTables.Clear();
             InitialCatalog = cbDataBase.SelectedItem.ToString();
            CreateConnectStr();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();


                    SqlCommand command = new SqlCommand();
                    command.CommandText = "SELECT name FROM sys.tables";
                    command.Connection = connection;

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            listObjectTables.Add(reader[0]);

                           
                        }
                    }
                    reader.Close();

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
         
        }

            ObservableCollection<object> data_list = new ObservableCollection<object>();
   
        private void lbTables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            data_list.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.ConnectionString = connectionString;
                try
                {
                    connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = $"SELECT * FROM {lbTables.SelectedItem}";
                command.Connection = connection;

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                        data_list.Add(reader[i]);
                        }
                         data_list.Add("_______________________________");

                    }
                }
                reader.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }



            }
        }
    }
}
