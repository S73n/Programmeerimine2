using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
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

namespace KooliProjekt.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient _http = new HttpClient();
        private Customer _selectedCustomer;

        public MainWindow()
        {
            InitializeComponent();
            _http.BaseAddress = new System.Uri("https://localhost:5001/"); // Muuda vastavalt oma API aadressile
            LoadCustomers();
        }

        private async void LoadCustomers()
        {
            var customers = await _http.GetFromJsonAsync<List<Customer>>("api/customers");
            CustomerList.ItemsSource = customers;
        }

        private void CustomerList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selectedCustomer = (Customer)CustomerList.SelectedItem;
            if (_selectedCustomer != null)
            {
                NameBox.Text = _selectedCustomer.Name;
                EmailBox.Text = _selectedCustomer.Email;
                PhoneBox.Text = _selectedCustomer.Phone;
                AddressBox.Text = _selectedCustomer.Address;
            }
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedCustomer == null || _selectedCustomer.Id == 0)
            {
                // Uus
                var customer = new Customer
                {
                    Name = NameBox.Text,
                    Email = EmailBox.Text,
                    Phone = PhoneBox.Text,
                    Address = AddressBox.Text
                };
                await _http.PostAsJsonAsync("api/customers", customer);
            }
            else
            {
                // Muuda olemasolevat
                _selectedCustomer.Name = NameBox.Text;
                _selectedCustomer.Email = EmailBox.Text;
                _selectedCustomer.Phone = PhoneBox.Text;
                _selectedCustomer.Address = AddressBox.Text;
                await _http.PutAsJsonAsync($"api/customers/{_selectedCustomer.Id}", _selectedCustomer);
            }
            LoadCustomers();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            _selectedCustomer = null;
            NameBox.Text = "";
            EmailBox.Text = "";
            PhoneBox.Text = "";
            AddressBox.Text = "";
            CustomerList.SelectedItem = null;
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedCustomer != null && _selectedCustomer.Id != 0)
            {
                await _http.DeleteAsync($"api/customers/{_selectedCustomer.Id}");
                LoadCustomers();
            }
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}