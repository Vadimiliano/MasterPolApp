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

namespace MasterPolApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditPage.xaml
    /// </summary>
    public partial class AddOrEditPage : Page
    {

        public AddOrEditPage()
        {
            InitializeComponent();
            PartnerTypeComboBox.ItemsSource = Data.MasterPolEntities.GetContext().PartnerType.ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder error = new StringBuilder();

                if (string.IsNullOrEmpty(PartnerNameTextBox.Text))
                {
                    error.AppendLine("Введите наименование партнера");
                }

                if (string.IsNullOrEmpty(PartnerTypeComboBox.Text))
                {
                    error.AppendLine("Выберите тип партнера");
                }

                if (string.IsNullOrEmpty(RatingTextBox.Text))
                {
                    error.AppendLine("Введите рейтинг");
                }

                if (string.IsNullOrEmpty(AdressIndexTextBox.Text))
                {
                    error.AppendLine("Введите адрес");
                }

                if (string.IsNullOrEmpty(DirectorNameTextBox.Text))
                {
                    error.AppendLine("Введите ФИО директора");
                }

                if (string.IsNullOrEmpty(PhoneNumberTextBox.Text))
                {
                    error.AppendLine("Введите номер телефона");
                }

                if (string.IsNullOrEmpty(EmailTextBox.Text))
                {
                    error.AppendLine("Введите почту");
                }


                if(error.Length > 0)
                {
                    MessageBox.Show(error.ToString(), "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.MainFrame.Navigate(new Pages.PartnerListPage());
        }
    }
}
