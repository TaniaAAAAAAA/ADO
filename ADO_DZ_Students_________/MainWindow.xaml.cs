using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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

namespace ADO_DZ_Students_________
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
        internal ObservableCollection<Student> students_list = new ObservableCollection<Student>();


        public MainWindow()
        {
            InitializeComponent();

            lb_Students.ItemsSource = students_list;
            SelectFromDB();

        }

        public void SelectFromDB()
        {
            students_list.Clear();
            string sql = "SELECT * FROM Student";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(sql, connection);
                sqlCommand.CommandText = "SELECT * FROM Student";
                sqlCommand.Connection = connection;

                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        students_list.Add(new Student
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Surname = (string)reader["Surname"],
                            IdGroup = reader["IdGroup"] as Nullable<int>


                        });

                    }
                }


              

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) //Add
        {

            Window1 w1 = new Window1();
            w1.btnUpdate.IsEnabled = false;
            w1.Show();
            SelectFromDB();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //Delete
        {
            if (lb_Students.SelectedIndex == -1)
                return;
            DeleteStudent();
        }

        private void DeleteStudent()
        {
            int index = lb_Students.SelectedIndex;
            int ID = students_list[index].Id;
           
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string cmdText = $@"Delete from Student where Id={ID}";
                SqlCommand command = new SqlCommand(cmdText, conn);

                conn.Open();

                int rows = command.ExecuteNonQuery();
                if(rows>0)
                {
                    students_list.Remove(students_list[index]); 
                }

            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //Update
        {
            if (lb_Students.SelectedIndex == -1)
                return;
            Window1 w = new Window1(lb_Students, students_list);
           
            w.Show();
        
        }

      

    }
}
