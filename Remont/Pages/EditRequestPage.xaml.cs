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
    /// Логика взаимодействия для EditRequestPage.xaml
    /// </summary>
    public partial class EditRequestPage : Page
    {
        private readonly int _requestId;

        public EditRequestPage(int requestId)
        {
            InitializeComponent();
            _requestId = requestId;
            LoadData();
        }

        private void LoadData()
        {
            using (var db = new EquipmentRepairDBEntities1())
            {
                
                var request = db.Request.Find(_requestId);
                TextBoxProblemDescription.Text = request.ProblemDescription;
                ComboBoxStatus.ItemsSource = db.Status.ToList();
                ComboBoxStatus.SelectedValue = request.StatusID;
                ComboBoxTechnician.ItemsSource = db.Technician.ToList();
                ComboBoxTechnician.SelectedValue = request.TechnicianID;
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new EquipmentRepairDBEntities1())
            {
                var request = db.Request.Find(_requestId);
                request.ProblemDescription = TextBoxProblemDescription.Text;
                request.StatusID = (int)ComboBoxStatus.SelectedValue;
                request.TechnicianID = (int)ComboBoxTechnician.SelectedValue;

                db.SaveChanges();
                MessageBox.Show("Заявка успешно обновлена!");
                NavigationService?.GoBack();
            }
        }
    }
}
