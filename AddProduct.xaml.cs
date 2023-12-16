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
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public User user { get; set; }
        public AddProduct(User user)
        {
            InitializeComponent();
            name.Text = user.Name;
            this.user = user;
        }
        private const string apiUrl = "https://salepoint.onrender.com/products/addProduct";

        private async void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string productName = productNameTextBox.Text;
                decimal productNetPrice = decimal.Parse(productnetPriceTextBox.Text);
                decimal productSellPrice = decimal.Parse(productsellPriceTextBox.Text);
                int productStock = int.Parse(productstockTextBox.Text);
                string productCategory = productcategoryTextBox.Text;

                var newProduct = new
                {
                    name = productName,
                    netPrice = productNetPrice,
                    sellPrice = productSellPrice,
                    stock = productStock,
                    category = productCategory
                };

                await SendProductData(newProduct);
                MessageBox.Show("Product added successfully!");
                productNameTextBox.Clear();
                productnetPriceTextBox.Clear();
                productsellPriceTextBox.Clear();
                productstockTextBox.Clear();
                productcategoryTextBox.Clear();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid data for prices and stock.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async Task SendProductData(object product)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonData = JsonConvert.SerializeObject(product);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to add product. Status code: {response.StatusCode}");
                }
            }
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
        private async void NavSearch(object sender, RoutedEventArgs e)
        {
            /*Window page = new SearchPage(user);
            page.Show();
            this.Close();*/
        }
    }
}
