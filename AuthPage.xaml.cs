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

namespace Тужилова_Чудо_Обувь
{
    /// <summary>
    /// Логика взаимодействия для Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
            GenerateCaptcha();
        }
        private void GuestView_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void AuthView_Click(object sender, RoutedEventArgs e)
        {
            if (CaptchaPanel.Visibility == Visibility.Visible)
            {
                if (string.IsNullOrWhiteSpace(TBCInput.Text) || TBCInput.Text.Trim().ToUpper() != _currentCaptchaCode)
                {
                    MessageBox.Show("Неверно введенная капча");
                    AuthView.IsEnabled = false;
                    await Task.Delay(10000);
                    AuthView.IsEnabled = true;
                    GenerateCaptcha();
                    TBCInput.Text = "";
                    return;
                }

            }

            string login = TBoxLogin.Text.Trim();
            string password = TBoxPassword.Text;
            if (login == "" || password == "")
            {
                MessageBox.Show("Есть пустые поля");
                return;
            }
            User user = Тужилова41Entities.GetContext().User.ToList().Find(p => p.UserLogin == login && p.UserPassword == password);
            if (user != null)
            {
                CaptchaPanel.Visibility = Visibility.Collapsed;
                Manager.MainFrame.Navigate(new Page1(user));
                TBoxLogin.Text = "";
                TBoxPassword.Text = "";
            }
            else
            {
                CaptchaPanel.Visibility = Visibility.Visible;
                MessageBox.Show("Введены неверные данные");
                GenerateCaptcha();
                TBCInput.Text = "";
            }
        }

        private string _currentCaptchaCode = "";

        private void GenerateCaptcha()
        {
            string chars = "ABCDEFGHJKLMNPQSTUVWXY23456789";
            Random random = new Random();

            string code = "";
            for(int i=0;i<4;i++)
            {
                code += chars[random.Next(chars.Length)];
            }
            _currentCaptchaCode = code;
            captchaOneWord.Text = code[0].ToString();
            captchaTwoWord.Text = code[1].ToString();
            captchaThreeWord.Text = code[2].ToString();
            captchaFourWord.Text = code[3].ToString();
        }

        private void TBoxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TBoxPassword_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RefreshC_Click(object sender, RoutedEventArgs e)
        {
            GenerateCaptcha();
            TBCInput.Text = "";
        }
    }
}
