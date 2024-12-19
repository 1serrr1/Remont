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

namespace Remont.Pages
{
    /// <summary>
    /// Логика взаимодействия для RequestListPage.xaml
    /// </summary>
    public partial class RequestListPage : Page
    {
        public RequestListPage()
        {
            InitializeComponent();
            LoadRequests();
        }

        private void LoadRequests()
        {
            using (var db = new EquipmentRepairDBEntities1())
            {
                DataGridRequests.ItemsSource = db.Request.Include("Status").ToList();
                DataGridRequests.ItemsSource = db.Request.Include("Technician").ToList();
            }
        }

        private void DataGridRequests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridRequests.SelectedItem is Request selectedRequest)
            {
                NavigationService?.Navigate(new EditRequestPage(selectedRequest.RequestID));
            }
        }
    }
}
