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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;


namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private SqlConnection sqlConnection = null;
        private SqlCommandBuilder sqlBuilder = null;
        private SqlDataAdapter sqlDataAdapter = null;
    
       
        public MainWindow()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\g1hxs\source\repos\WpfApp1\WpfApp1\Database1.mdf;Integrated Security=True");

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          

            try
            {
                SqlCommand show_table = new SqlCommand("SELECT * FROM clients1", con);
                con.Open();             
                sqlDataAdapter = new SqlDataAdapter("SELECT * FROM clients1", con);
                sqlBuilder = new SqlCommandBuilder(sqlDataAdapter);
                DataTable dt = new DataTable("Clients");
                sqlDataAdapter.Fill(dt);
                dateView.ItemsSource = dt.DefaultView;
                sqlDataAdapter.Update(dt);
                con.Close();
            }
            catch(Exception ex)
            {           
              MessageBox.Show(ex.Message, "Ошибка введения данных!", MessageBoxButton.OK);          
             }
        }

        public void isEmpty()
        {
            string[] base_id = { name_client.Text, surname_client.Text, patrm_client.Text, phone_client.Text, email_client.Text, date_client.Text, worker_client.Text, chartr.Text, service_cl.Text, result_service.Text, cost_service.Text };
  
            foreach(string id in base_id)
            {
                if (id.Equals(""))
                {
                    MessageBox.Show("Введите данные!");
                    break;
                }
                else
                {
                    MessageBox.Show("Данные обновлены!");
                    break;
                }
            }
        }
        private void insert_clients_Click(object sender, RoutedEventArgs e)
        {
            isEmpty();
            SqlCommand cmd = new SqlCommand("INSERT INTO clients1 VALUES (@name, @surname, @patronymic, @phone, @email, @date, @worker, @character, @service, @result,@final_cost)");
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@name", name_client.Text);
            cmd.Parameters.AddWithValue("@surname", surname_client.Text);
            cmd.Parameters.AddWithValue("@patronymic", patrm_client.Text);
            cmd.Parameters.AddWithValue("@phone", phone_client.Text);
            cmd.Parameters.AddWithValue("@email", email_client.Text);
            cmd.Parameters.AddWithValue("@date", date_client.Text);
            cmd.Parameters.AddWithValue("@worker", worker_client.Text);
            cmd.Parameters.AddWithValue("@character", chartr.Text);
            cmd.Parameters.AddWithValue("@service", service_cl.Text);
            cmd.Parameters.AddWithValue("@result", result_service.Text);
            cmd.Parameters.AddWithValue("@final_cost", cost_service.Text);
            SqlCommand show_table = new SqlCommand("SELECT * FROM clients1", con);
            con.Open();
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void remove_client_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM clients1 WHERE ID = " + id_client.Text + "", con);
                cmd.CommandType = CommandType.Text;
                SqlCommand show_table = new SqlCommand("SELECT * FROM clients1", con);
                con.Open();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Клиент удалён");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Чтобы удалить клиента, введите ID", MessageBoxButton.OK);
            }
        }
    }
}
