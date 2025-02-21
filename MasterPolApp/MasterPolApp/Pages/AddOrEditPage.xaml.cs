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

        public Data.Partners CurrentProduct = new Data.Partners();
        public string FlagAddOrEdit = "default";

        public AddOrEditPage(Data.Partners _partners)
        {
            InitializeComponent();

            if(_partners != null)
            {
                CurrentProduct = _partners;
                FlagAddOrEdit = "edit";
            }
            else
            {
                FlagAddOrEdit = "add";
            }

            DataContext = CurrentProduct;

            Init();
        }


        public void Init()
        {
            try
            {
                PartnerTypeComboBox.ItemsSource = Data.MasterPolEntities.GetContext().PartnerType.ToList();

                if (FlagAddOrEdit == "add")
                {
                    PartnerNameTextBox.Text = string.Empty;
                    PartnerTypeComboBox.SelectedItem = null;
                    RatingTextBox.Text = string.Empty;
                    AdressIndexTextBox.Text = string.Empty;
                    AdressAreaTextBox.Text = string.Empty;
                    AdressCityTextBox.Text = string.Empty;
                    AdressStreetTextBox.Text = string.Empty;
                    AdressHouseNumberTextBox.Text = string.Empty;
                    DirectorNameTextBox.Text = string.Empty;
                    PhoneNumberTextBox.Text = string.Empty;
                    EmailTextBox.Text = string.Empty;
                }
                else if (FlagAddOrEdit == "edit")
                {
                    PartnerNameTextBox.Text = CurrentProduct.PartnerName;
                    PartnerTypeComboBox.SelectedItem = Data.MasterPolEntities.GetContext().PartnerType.
                        Where(d => d.ID == CurrentProduct.ID).FirstOrDefault();
                    RatingTextBox.Text = CurrentProduct.Rating.ToString();
                    AdressIndexTextBox.Text = CurrentProduct.Adress.Index.IndexNumber.ToString();
                    AdressAreaTextBox.Text = CurrentProduct.Adress.Area.AreaName;
                    AdressCityTextBox.Text = CurrentProduct.Adress.City.City1;
                    AdressStreetTextBox.Text = CurrentProduct.Adress.Street.StreetName;
                    AdressHouseNumberTextBox.Text = CurrentProduct.Adress.House.ToString();
                    DirectorNameTextBox.Text = CurrentProduct.Directors.DirectorName;
                    PhoneNumberTextBox.Text = CurrentProduct.PartnerPhone.ToString();
                    EmailTextBox.Text = CurrentProduct.PartnerEmail;
                }

            }
            catch
            {

            }
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
                    error.AppendLine("Введите индекс");
                }
                if (string.IsNullOrEmpty(AdressAreaTextBox.Text))
                {
                    error.AppendLine("Введите область");
                }
                if (string.IsNullOrEmpty(AdressCityTextBox.Text))
                {
                    error.AppendLine("Введите город");
                } 
                if (string.IsNullOrEmpty(AdressStreetTextBox.Text))
                {
                    error.AppendLine("Введите улицу");
                }
                if (string.IsNullOrEmpty(AdressHouseNumberTextBox.Text))
                {
                    error.AppendLine("Введите номер дома");
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

                var searcDirector = (from item in Data.MasterPolEntities.GetContext().Directors
                                  where item.DirectorName == DirectorNameTextBox.Text
                                  select item).FirstOrDefault();

                if (searcDirector != null)
                {
                    CurrentProduct.ID = searcDirector.ID;
                }
                else
                {
                    Data.Directors DirectorName = new Data.Directors()
                    {
                        DirectorName = DirectorNameTextBox.Text
                    };
                    Data.MasterPolEntities.GetContext().Directors.Add(DirectorName);
                    Data.MasterPolEntities.GetContext().SaveChanges();
                    CurrentProduct.ID = DirectorName.ID;
                }

                
                
                var searchIndex = (from item in Data.MasterPolEntities.GetContext().Index
                                    where item.IndexNumber == AdressIndexTextBox.Text
                                    select item).FirstOrDefault();

                if (searchIndex != null)
                {
                    CurrentProduct.ID = searchIndex.ID;
                }
                else
                {
                    Data.Index IndexNumber = new Data.Index()
                    {
                        IndexNumber = AdressIndexTextBox.Text
                    };
                    Data.MasterPolEntities.GetContext().Index.Add(IndexNumber);
                    Data.MasterPolEntities.GetContext().SaveChanges();
                    CurrentProduct.ID = IndexNumber.ID;
                }

                var searhCity = (from item in Data.MasterPolEntities.GetContext().City
                                   where item.City1 == AdressCityTextBox.Text
                                   select item).FirstOrDefault();

                if (searhCity != null)
                {
                    CurrentProduct.ID = searhCity.ID;
                }
                else
                {
                    Data.City IndexNumber = new Data.City()
                    {
                        City1 = AdressCityTextBox.Text
                    };
                    Data.MasterPolEntities.GetContext().City.Add(IndexNumber);
                    Data.MasterPolEntities.GetContext().SaveChanges();
                    CurrentProduct.ID = IndexNumber.ID;
                }


                var searchStreet = (from item in Data.MasterPolEntities.GetContext().Street
                                   where item.StreetName == AdressStreetTextBox.Text
                                   select item).FirstOrDefault();

                if (searchStreet != null)
                {
                    CurrentProduct.ID = searchStreet.ID;
                }
                else
                {
                    Data.Street IndexNumber = new Data.Street()
                    {
                        StreetName = AdressStreetTextBox.Text
                    };
                    Data.MasterPolEntities.GetContext().Street.Add(IndexNumber);
                    Data.MasterPolEntities.GetContext().SaveChanges();
                    CurrentProduct.ID = IndexNumber.ID;
                }

                //var searchHouse = (from item in Data.MasterPolEntities.GetContext().Adress
                //                   where item.House.ToString() == AdressHouseNumberTextBox.Text
                //                   select item).FirstOrDefault();

                //if (searchHouse != null)
                //{
                //    CurrentProduct.ID = searchHouse.ID;
                //}
                //else
                //{
                //    Data.Adress IndexNumber = new Data.Adress()
                //    {
                //        House = AdressHouseNumberTextBox.Text;
                //    };
                //    Data.MasterPolEntities.GetContext().Index.Add(IndexNumber);
                //    Data.MasterPolEntities.GetContext().SaveChanges();
                //    CurrentProduct.ID = IndexNumber.ID;
                //}





                var searchArea = (from item in Data.MasterPolEntities.GetContext().Area
                                  where item.AreaName == AdressAreaTextBox.Text
                                  select item).FirstOrDefault();

                if (searchArea != null)
                {
                    CurrentProduct.ID = searchArea.ID;
                }
                else
                {
                    Data.Area supplierName = new Data.Area()
                    {
                        AreaName = AdressAreaTextBox.Text
                    };
                    Data.MasterPolEntities.GetContext().Area.Add(supplierName);
                    Data.MasterPolEntities.GetContext().SaveChanges();
                    CurrentProduct.ID = supplierName.ID;
                }







                CurrentProduct.PartnerName = PartnerNameTextBox.Text;
                CurrentProduct.Rating = Convert.ToInt32(RatingTextBox.Text);
                CurrentProduct.PartnerPhone = PhoneNumberTextBox.Text;
                var selectType = PartnerTypeComboBox.SelectedItem as Data.PartnerType;
                CurrentProduct.PartnerTypeID = selectType.ID;


                if (FlagAddOrEdit == "add")
                {
                    Data.MasterPolEntities.GetContext().Partners.Add(CurrentProduct);
                    Data.MasterPolEntities.GetContext().SaveChanges();
                    MessageBox.Show("Успешно добавлено!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (FlagAddOrEdit == "edit")
                {
                    Data.MasterPolEntities.GetContext().SaveChanges();
                    MessageBox.Show("Успешно сохранено!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);

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
