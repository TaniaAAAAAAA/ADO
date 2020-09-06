using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

namespace ADO_DZ_Students_________
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
         string connectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;

            List<int?> list_IdGroup = new List<int?>();
        public Window1()
        {
            InitializeComponent();

            ReadGropsFromDB();
        }

        private void ReadGropsFromDB()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.ConnectionString = connectionString;
                connection.Open();


                string sql = "SELECT IdGroup FROM Student";
                SqlCommand sqlCommand = new SqlCommand(sql, connection);

                var reader = sqlCommand.BeginExecuteReader(CallBack, null);
                //if (reader.HasRows)
                //{
                //    while (reader.Read())
                //    {
                //        if (!list_IdGroup.Exists(x => x == reader["IdGroup"] as Nullable<int>))
                //            list_IdGroup.Add(reader["IdGroup"] as Nullable<int>);

                //    }
                //}
            //    reader.Close();


                cb_IdGroup.ItemsSource = list_IdGroup;

            }
        }

        private void CallBack(IAsyncResult ar)
        {
            AsyncResult res = (AsyncResult)ar;
            var result = (SqlCommand)res.AsyncDelegate;
            var reader = result.EndExecuteReader(ar);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (!list_IdGroup.Exists(x => x == reader["IdGroup"] as Nullable<int>))
                        list_IdGroup.Add(reader["IdGroup"] as Nullable<int>);

                }
            }
            reader.Close();
        }

        public Window1(ListBox lb, ObservableCollection<Student> students)
        {
            InitializeComponent();
            ReadGropsFromDB();
            index = lb.SelectedIndex;
             ID = students[index].Id;

            txtName.Text = students[index].Name;
            txtSurname.Text = students[index].Surname;
            cb_IdGroup.SelectedItem = students[index].IdGroup;
            btnAdd.IsEnabled = false;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Student newStudent = new Student()
            {
                Name = txtName.Text,
                Surname = txtSurname.Text,
                IdGroup = Convert.ToInt32(cb_IdGroup.Text)
            };
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString))
            {
                connection.Open();

                AddStudent(newStudent, connection);
            }
            this.Close();
        }




        private void AddStudent(Student student, SqlConnection conn)
        {
            string cmdText = @"insert Student values (@name, @surname, @idGroup)";
            SqlCommand command = new SqlCommand(cmdText, conn);

        
            command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = student.Name;

            command.Parameters.Add("@surname", System.Data.SqlDbType.NVarChar).Value = student.Surname;
            command.Parameters.Add("@idGroup", System.Data.SqlDbType.Int).Value = student.IdGroup;
           /* int rows = */
            command.BeginExecuteNonQuery();

            //if (rows > 0)
            //{
            //    
            //}
        }

       
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           UpdateStudent();
            this.Close();
        }

        int index ;
        int ID ;
        public void UpdateStudent()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string cmdText = $@"UPDATE Student SET Name=@name, Surname=@surname, IdGroup=@idGroup WHERE Id={ID}";


                SqlCommand command = new SqlCommand(cmdText, conn);

                command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = txtName.Text;

                command.Parameters.Add("@surname", System.Data.SqlDbType.NVarChar).Value = txtSurname.Text;
                command.Parameters.Add("@idGroup", System.Data.SqlDbType.Int).Value =cb_IdGroup.SelectedItem;


                conn.Open();

                /*int rows = */
                command.BeginExecuteNonQuery();
            }
        }


    }
}
