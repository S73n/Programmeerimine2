using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KooliProjekt.WpfClient
{
    public class MainWindowViewModel : NotifyPropertyChangedBase
    {
        private readonly HttpClient _http = new HttpClient { BaseAddress = new System.Uri("https://localhost:5001/") };
        private Customer _selectedCustomer;
        public ObservableCollection<Customer> Customers { get; set; } = new ObservableCollection<Customer>();

        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand NewCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainWindowViewModel()
        {
            NewCommand = new RelayCommand(_ => AddNewCustomer());
            SaveCommand = new RelayCommand(async _ => await SaveCustomerAsync(), _ => SelectedCustomer != null);
            DeleteCommand = new RelayCommand(async _ => await DeleteCustomerAsync(), _ => SelectedCustomer != null && SelectedCustomer.Id != 0);
            LoadCustomers();
        }

        private async void LoadCustomers()
        {
             var customers = await _http.GetFromJsonAsync<Customer[]>("api/customers");
            Customers.Clear();
            foreach (var c in customers)
                Customers.Add(c);
        }

        private void AddNewCustomer()
        {
            var newCustomer = new Customer();
            Customers.Add(newCustomer);
            SelectedCustomer = newCustomer;
        }

        private async Task SaveCustomerAsync()
        {
            if (SelectedCustomer == null) return;
            if (SelectedCustomer.Id == 0)
            {
                var resp = await _http.PostAsJsonAsync("api/customers", SelectedCustomer);
                var created = await resp.Content.ReadFromJsonAsync<Customer>();
                if (created != null)
                {
                    SelectedCustomer.Id = created.Id;
                }
            }
            else
            {
                await _http.PutAsJsonAsync($"api/customers/{SelectedCustomer.Id}", SelectedCustomer);
            }
            LoadCustomers();
        }

        private async Task DeleteCustomerAsync()
        {
            if (SelectedCustomer == null || SelectedCustomer.Id == 0) return;
            await _http.DeleteAsync($"api/customers/{SelectedCustomer.Id}");
            Customers.Remove(SelectedCustomer);
            SelectedCustomer = null;
            LoadCustomers();
        }
    }

    // RelayCommand klass lihtsaks ICommand kasutamiseks
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
} 