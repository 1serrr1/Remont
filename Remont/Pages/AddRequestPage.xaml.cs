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
    /// Логика взаимодействия для AddRequestPage.xaml
    /// </summary>
    public partial class AddRequestPage : Page
    {
        public AddRequestPage()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            using (var db = new EquipmentRepairDBEntities1())
            {
                ComboBoxEquipment.ItemsSource = db.Equipment.ToList();
                ComboBoxFaultType.ItemsSource = db.FaultType.ToList();
                ComboBoxClient.ItemsSource = db.Client.ToList();
                ComboBoxStatus.ItemsSource = db.Status.ToList();
                ComboBoxTechnician.ItemsSource = db.Technician.ToList();
                
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxEquipment.SelectedValue == null || ComboBoxFaultType.SelectedValue == null ||
                ComboBoxClient.SelectedValue == null || ComboBoxStatus.SelectedValue == null ||
                string.IsNullOrEmpty(TextBoxProblemDescription.Text) || DatePickerRequestDate.SelectedDate == null)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            using (var db = new EquipmentRepairDBEntities1())
            {
                var newRequest = new Request
                {
                    RequestDate = DatePickerRequestDate.SelectedDate.Value,
                    EquipmentID = (int)ComboBoxEquipment.SelectedValue,
                    FaultTypeID = (int)ComboBoxFaultType.SelectedValue,
                    ClientID = (int)ComboBoxClient.SelectedValue,
                    StatusID = (int)ComboBoxStatus.SelectedValue,
                    ProblemDescription = TextBoxProblemDescription.Text,
                    TechnicianID = (int)ComboBoxTechnician.SelectedValue,
                };

                db.Request.Add(newRequest);
                db.SaveChanges();
                MessageBox.Show("Заявка успешно добавлена!");
                NavigationService?.GoBack();
            }
        }
    }
}
