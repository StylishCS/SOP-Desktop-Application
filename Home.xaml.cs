using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace SOP_Login_Page
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        LinkedList<Tuple<Product, int>> cartData = new LinkedList<Tuple<Product, int>>();
        private const string apiUrl = "https://salepoint.onrender.com/products/";
        public User user { get; set; }
        public Home(User user)
        {
            InitializeComponent();
            this.user = user;
            name.Text = user.Name;
            LoadProductCardsAsync();
        }
        private async Task LoadProductCardsAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        List<Product> products = JsonConvert.DeserializeObject<List<Product>>(responseBody);

                        foreach (Product product in products)
                        {
                            RepeatStackPanel(product);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to fetch data from the API.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void RepeatStackPanel(Product product)
        {
            Button edit = new Button();
            StackPanel innerStackPanel = new StackPanel();
            innerStackPanel.Orientation = Orientation.Horizontal;


            innerStackPanel.Children.Add(new Label()
            {
                Content = product.Id,
                Width = 140,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                FontSize = 20

            });

            innerStackPanel.Children.Add(new Label()
            {
                Content = product.Name,
                Width = 270,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                FontSize = 20

            });
            innerStackPanel.Children.Add(new Label()
            {
                Content = product.Category,
                Width = 180,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                FontSize = 20
            });


            innerStackPanel.Children.Add(new Label()
            {
                Content = product.SellPrice,
                Width = 190,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                FontSize = 20
            });

            innerStackPanel.Children.Add(new Label()
            {
                Content = product.NetPrice,
                Width = 190,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                FontSize = 20
            });

            innerStackPanel.Children.Add(new Label()
            {
                Content = product.Stock,
                Width = 190,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                FontSize = 20
            });



            Button Buy = new Button();
            Buy.Content = "Buy";
            Buy.Width = 100;

            Buy.Click += (sender, e) => AddToCart(product);


            innerStackPanel.Children.Add(Buy);


            Button Delete = new Button();
            Delete.Content = "Delete";
            Delete.Width = 80;

            Delete.Click += (sender, e) => DeleteProduct(product);

            innerStackPanel.Children.Add(Delete);


            Button Edit = new Button();
            Edit.Content = "Edit";
            Edit.Width = 80;

            Edit.Click += (sender, e) => EditProduct(product);

            innerStackPanel.Children.Add(Edit);

            ss.Children.Add(innerStackPanel);
        }

        private void AddToCart(Product product)
        {
            if(product.Stock >= 1)
            {
                cartData.AddLast(Tuple.Create(product, 1));
            }
            else
            {
                MessageBox.Show("No stock of this product!");
            }
        }

        async private void DeleteProduct(Product product)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to proceed?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.DeleteAsync("https://salepoint.onrender.com/products/deleteProduct/" + product.Id);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Product Deleted Succesfully!");
                            Window page = new Home(user);
                            page.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Something Went Wrong");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            
        }

        private void EditProduct(Product product)
        {

        }

        private async void NavAddProd(object sender, RoutedEventArgs e)
        {
            Window page = new AddProduct(user);
            page.Show();
            this.Close();
        }
        private async void NavProds(object sender, RoutedEventArgs e)
        {
            Window page = new Home(user);
            page.Show();
            this.Close();
        }
        private async void NavCart(object sender, RoutedEventArgs e)
        {
            /*Window page = new Cart(user, cartData);
            page.Show();
            this.Close();*/
        }

        private async void NavSearch(object sender, RoutedEventArgs e)
        {
            /*Window page = new SearchPage(user);
            page.Show();
            this.Close();*/
        }

        private async void logout(object sender, RoutedEventArgs e)
        {
            Window page = new MainWindow();
            page.Show();
            this.Close();
        }
    }
}
