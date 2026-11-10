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
using System.Data.Entity;
namespace Тужилова_Чудо_Обувь
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1(User user)
        {
            InitializeComponent();
            FIOTB.Text = user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
            switch (user.UserRole)
            {
                case 1:
                    RoleTB.Text = "Клиент"; break;
                case 2:
                    RoleTB.Text = "Менеджер"; break;
                case 3:
                    RoleTB.Text = "Администратор"; break;
            }


            var currentServices = Тужилова41Entities.GetContext().Product.ToList();
            ServiceListView.ItemsSource = currentServices;
            ComboType.SelectedIndex = 0;
            UpdateServices();
            TotalRecords = TableList.Count;
        }

        private void UpdateServices()
        {
            var currentServices = Тужилова41Entities.GetContext().Product.ToList();

            if (ComboType.SelectedIndex == 0)
                currentServices = currentServices.Where(p => (p.ProductDiscountAmount >= 0 && p.ProductDiscountAmount <= 100)).ToList();
            if (ComboType.SelectedIndex == 1)
                currentServices = currentServices.Where(p => (p.ProductDiscountAmount >= 0 && p.ProductDiscountAmount < 9.99)).ToList();
            if (ComboType.SelectedIndex == 2)
                currentServices = currentServices.Where(p => (p.ProductDiscountAmount >= 10 && p.ProductDiscountAmount < 14.99)).ToList();
            if (ComboType.SelectedIndex == 3)
                currentServices = currentServices.Where(p => (p.ProductDiscountAmount >= 15 && p.ProductDiscountAmount < 99.99)).ToList();
            currentServices = currentServices.Where(p => p.ProductName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            if (RButtonDown.IsChecked.Value)
                currentServices = currentServices.OrderByDescending(p => p.ProductCost).ToList();
            if (RButtonUp.IsChecked.Value)
                currentServices = currentServices.OrderBy(p => p.ProductCost).ToList();

            ServiceListView.ItemsSource = currentServices;
            TableList = currentServices;
            ChangePage();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Page2());
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateServices();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
        }

        private void RButtonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }

        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }

        int CountRecords;
        int TotalRecords;
        List<Product> TableList;
        private void ChangePage()
        {
            CountRecords = TableList.Count;
            TBCount.Text = "кол-во " + CountRecords + " из " + TotalRecords.ToString();
        }

        private void FIOTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RoleTB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
