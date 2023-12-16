using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOP_Login_Page
{
    public class Product
    {
        private string id { get; set; }
        private string name { get; set; }
        private string category { get; set; }
        private decimal sellPrice { get; set; }
        private decimal netPrice { get; set; }
        private int stock { get; set; }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Category
        {
            get { return category; }
            set { category = value; }
        }
        public decimal SellPrice
        {
            get { return sellPrice; }
            set { sellPrice = value; }
        }
        public decimal NetPrice
        {
            get { return netPrice; }
            set { netPrice = value; }
        }
        public int Stock
        {
            get { return stock; }
            set { stock = value; }
        }

    }
}
