using System.Linq;
using System.Threading.Tasks;
using KooliProjekt.WpfClient;
using Moq;
using Xunit;

namespace KooliProjekt.WpfClient.UnitTests
{
    public class MainWindowViewModelTests
    {
        [Fact]
        public void NewCommand_AddsNewCustomer_AndSelectsIt()
        {
            var vm = new MainWindowViewModel();
            int initialCount = vm.Customers.Count;
            vm.NewCommand.Execute(null);
            Assert.Equal(initialCount + 1, vm.Customers.Count);
            Assert.NotNull(vm.SelectedCustomer);
            Assert.Equal(vm.Customers.Last(), vm.SelectedCustomer);
        }

        [Fact]
        public void SelectedCustomer_PropertyChanged_Fires()
        {
            var vm = new MainWindowViewModel();
            bool fired = false;
            vm.PropertyChanged += (s, e) => { if (e.PropertyName == "SelectedCustomer") fired = true; };
            vm.NewCommand.Execute(null);
            vm.SelectedCustomer = vm.Customers.First();
            Assert.True(fired);
        }

        // NB! SaveCommand ja DeleteCommand testid eeldavad, et HttpClient on asendatud mockiga või refaktoreeritud DI jaoks.
        // Kui soovid, et Save/Delete testid töötaksid ilma päris API-ta, tuleb ViewModel refaktoreerida, et HttpClient oleks injekteeritav.
    }
} 