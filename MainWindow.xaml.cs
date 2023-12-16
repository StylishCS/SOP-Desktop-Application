using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Markup;
using System.Text.Json;

namespace SOP_Login_Page
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

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        async private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUser.Text;
            string password = txtPassword.Password;
            string apiUrl = "https://salepoint.onrender.com/users/login";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var formData = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("email", username),
                        new KeyValuePair<string, string>("password", password)
                    });

                    HttpResponseMessage response = await client.PostAsync(apiUrl, formData);
                    var res = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        PostResponse[] data = JsonSerializer.Deserialize<PostResponse[]>(res);
                        User user = new User(data[0].id, data[0].name);
                        Window page = new Home(user);
                        page.Show();
                        this.Close();
                    }
                    else
                    {
                        PostResponse data = JsonSerializer.Deserialize<PostResponse>(res);
                        MessageBox.Show(data.msg);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
    }
}
