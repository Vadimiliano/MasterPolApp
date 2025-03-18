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
    /// Логика взаимодействия для SellHistoryPage.xaml
    /// </summary>
    public partial class SellHistoryPage : Page
    {
        public SellHistoryPage(int partnerId)
        {
            InitializeComponent();
            LoadSalesHistory(partnerId);

        }

        private void LoadSalesHistory(int partnerId)
        {
            SalesDataGrid.ItemsSource = Data.MasterPolEntities.GetContext().PartnerProducts.
                Where(s => s.PartnerID == partnerId).ToList();

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.MainFrame.Navigate(new Pages.PartnerListPage());
        }
    }
}
