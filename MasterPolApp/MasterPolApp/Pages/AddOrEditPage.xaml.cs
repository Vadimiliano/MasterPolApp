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
                        Where(d => d.ID == CurrentProduct.PartnerTypeID).FirstOrDefault();
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
                else
                {
                    var countParse = Int32.TryParse(RatingTextBox.Text, out var result);
                    if (!(countParse && result >= 0))
                    {
                        error.AppendLine("К-во - целое и положительное");
                    }
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
                else
                {
                    var countParse = Int32.TryParse(AdressHouseNumberTextBox.Text, out var result);
                    if (!(countParse && result >= 0))
                    {
                        error.AppendLine("Номер дома - целое и положительное");
                    }
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



                var address = new Data.Adress();

                var searchStreet = (from item in Data.MasterPolEntities.GetContext().Street
                                    where item.StreetName == AdressStreetTextBox.Text
                                    select item).FirstOrDefault();

                if (searchStreet != null)
                {
                    address.StreetID = searchStreet.ID;
                }
                else
                {
                    Data.Street nameStreet = new Data.Street()
                    {
                        StreetName = AdressStreetTextBox.Text
                    };
                    Data.MasterPolEntities.GetContext().Street.Add(nameStreet);
                    Data.MasterPolEntities.GetContext().SaveChanges();
                    address.StreetID = nameStreet.ID;
                }

                var searchCity = (from item in Data.MasterPolEntities.GetContext().City
                                  where item.City1 == AdressCityTextBox.Text
                                  select item).FirstOrDefault();

                if (searchCity != null)
                {
                    address.CityID = searchCity.ID;
                }
                else
                {
                    Data.City nameCity = new Data.City()
                    {
                        City1 = AdressCityTextBox.Text
                    };
                    Data.MasterPolEntities.GetContext().City.Add(nameCity);
                    Data.MasterPolEntities.GetContext().SaveChanges();
                    address.CityID = nameCity.ID;
                }

                var searchArea = (from item in Data.MasterPolEntities.GetContext().Area
                                  where item.AreaName == AdressAreaTextBox.Text
                                  select item).FirstOrDefault();

                if (searchArea != null)
                {
                    address.AreaID = searchArea.ID;
                }
                else
                {
                    Data.Area nameArea = new Data.Area()
                    {
                        AreaName = AdressAreaTextBox.Text
                    };
                    Data.MasterPolEntities.GetContext().Area.Add(nameArea);
                    Data.MasterPolEntities.GetContext().SaveChanges();
                    address.AreaID = nameArea.ID;
                }

                var searchIndex = (from item in Data.MasterPolEntities.GetContext().Index
                                   where item.IndexNumber == AdressIndexTextBox.Text
                                   select item).FirstOrDefault();

                if (searchIndex != null)
                {
                    address.IndexID = searchIndex.ID;
                }
                else
                {
                    Data.Index numberIndex = new Data.Index()
                    {
                        IndexNumber = AdressIndexTextBox.Text
                    };
                    Data.MasterPolEntities.GetContext().Index.Add(numberIndex);
                    Data.MasterPolEntities.GetContext().SaveChanges();
                    address.IndexID = numberIndex.ID;
                }

                var searchName = (from item in Data.MasterPolEntities.GetContext().Directors
                                  where item.DirectorName == DirectorNameTextBox.Text
                                  select item).FirstOrDefault();

                if (searchName != null)
                {
                    CurrentProduct.DirectorID = searchName.ID;
                }
                else
                {
                    Data.Directors directorName = new Data.Directors()
                    {
                        DirectorName = DirectorNameTextBox.Text
                    };
                    Data.MasterPolEntities.GetContext().Directors.Add(directorName);
                    Data.MasterPolEntities.GetContext().SaveChanges();
                    CurrentProduct.DirectorID = directorName.ID;
                }
                Data.MasterPolEntities.GetContext().Adress.Add(address);
                Data.MasterPolEntities.GetContext().SaveChanges();

                CurrentProduct.PartnerEmail = EmailTextBox.Text;
                CurrentProduct.PartnerPhone = PhoneNumberTextBox.Text;
                CurrentProduct.Rating = Convert.ToInt32(RatingTextBox.Text);
                var selectType = PartnerTypeComboBox.SelectedItem as Data.PartnerType;
                CurrentProduct.PartnerTypeID = selectType.ID;
                CurrentProduct.PartnerName = PartnerNameTextBox.Text;
                address.House = Convert.ToInt32(AdressHouseNumberTextBox.Text);
                CurrentProduct.AdressID = address.ID;
                if (FlagAddOrEdit == "add")
                {
                    Data.MasterPolEntities.GetContext().Adress.Add(address);
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
            catch (Exception ex)
            {
                // Создаем подробное сообщение об ошибке
                string errorMessage = $"Произошла ошибка:\n\n{ex.Message}\n\nПодробности:\n{ex.ToString()}";

                // Показываем сообщение в MessageBox
                MessageBox.Show(errorMessage, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.MainFrame.Navigate(new Pages.PartnerListPage());
        }
    }
}
