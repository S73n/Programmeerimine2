using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using KooliProjekt.WpfClient.Api;

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

        public Action<string> OnError { get; set; }

        public MainWindowViewModel()
        {
            NewCommand = new RelayCommand(_ => AddNewCustomer());
            SaveCommand = new RelayCommand(async _ => await SaveCustomerAsync(), _ => SelectedCustomer != null);
            DeleteCommand = new RelayCommand(async _ => await DeleteCustomerAsync(), _ => SelectedCustomer != null && SelectedCustomer.Id != 0);
            LoadCustomers();
        }

        private async void LoadCustomers()
        {
            var result = await ListCustomersAsync();
            Customers.Clear();
            if (result.HasError)
            {
                OnError?.Invoke(result.Error);
                return;
            }
            foreach (var c in result.Value)
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
            Result<Customer> result;
            if (SelectedCustomer.Id == 0)
            {
                result = await CreateCustomerAsync(SelectedCustomer);
                if (!result.HasError && result.Value != null)
                {
                    SelectedCustomer.Id = result.Value.Id;
                }
            }
            else
            {
                result = await UpdateCustomerAsync(SelectedCustomer);
            }
            if (result.HasError)
                OnError?.Invoke(result.Error);
            LoadCustomers();
        }

        private async Task DeleteCustomerAsync()
        {
            if (SelectedCustomer == null || SelectedCustomer.Id == 0) return;
            var result = await DeleteCustomerByIdAsync(SelectedCustomer.Id);
            if (result.HasError)
                OnError?.Invoke(result.Error);
            Customers.Remove(SelectedCustomer);
            SelectedCustomer = null;
            LoadCustomers();
        }

        // API kliendi meetodid Result/Result<T> tüüpi
        public async Task<Result<Customer[]>> ListCustomersAsync()
        {
            try
            {
                var arr = await _http.GetFromJsonAsync<Customer[]>("api/customers");
                return new Result<Customer[]> { Value = arr };
            }
            catch (Exception ex)
            {
                return new Result<Customer[]> { Error = ex.Message };
            }
        }

        public async Task<Result<Customer>> CreateCustomerAsync(Customer customer)
        {
            try
            {
                var resp = await _http.PostAsJsonAsync("api/customers", customer);
                resp.EnsureSuccessStatusCode();
                var created = await resp.Content.ReadFromJsonAsync<Customer>();
                return new Result<Customer> { Value = created };
            }
            catch (Exception ex)
            {
                return new Result<Customer> { Error = ex.Message };
            }
        }

        public async Task<Result<Customer>> UpdateCustomerAsync(Customer customer)
        {
            try
            {
                var resp = await _http.PutAsJsonAsync($"api/customers/{customer.Id}", customer);
                resp.EnsureSuccessStatusCode();
                return new Result<Customer> { Value = customer };
            }
            catch (Exception ex)
            {
                return new Result<Customer> { Error = ex.Message };
            }
        }

        public async Task<Result> DeleteCustomerByIdAsync(int id)
        {
            try
            {
                var resp = await _http.DeleteAsync($"api/customers/{id}");
                resp.EnsureSuccessStatusCode();
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result { Error = ex.Message };
            }
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