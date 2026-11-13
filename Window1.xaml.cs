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
using System.Windows.Shapes;

namespace Тужилова_Чудо_Обувь
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        List<OrderProduct> selectedOrderProducts = new List<OrderProduct>();
        List<Product> selectedProducts = new List<Product>();
        private Order currentOrder = new Order();
        private OrderProduct currentProduct = new OrderProduct();
        public Window1(List<OrderProduct> selectedOrderProducts, List<Product> selectedProducts, string FIO)
        {
            InitializeComponent();
            var currentPickups = Тужилова41Entities.GetContext().PickupPoint.ToList();
            PickupsCombo.ItemsSource = currentPickups;
            ClientTB.Text = FIO;
            TBOrderID.Text = selectedOrderProducts.First().OrderID.ToString();
            ServiceListView.ItemsSource = selectedOrderProducts;
            foreach(Product P in selectedProducts)
            {
                P.ProductQuantityInStock = 1;
                foreach(OrderProduct q in selectedOrderProducts)
                {
                    if (P.ProductArticleNumber == q.ProductArticleNumber)
                        P.ProductQuantityInStock = q.Amount;
                }
            }

            this.selectedOrderProducts = selectedOrderProducts;
            this.selectedProducts = selectedProducts;
            OrderDP.Text = DateTime.Now.ToString();
            
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
