using Models;
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

namespace Hotel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IList<ComboItem> AvailableRooms = new List<ComboItem>();
        HttpHelper HttpHelper = new HttpHelper();
        private string Action = null;
        private string SelectedOccupiedRoom = null;
        private string SelectedVacantRoom = null;
        private string SelectedRepairRoom = null;

        public MainWindow()
        {
            InitializeComponent();
            HideAllPanels();
            GetAvailableRooms();
        }

        private void HideAllPanels()
        {
            //if (AllRoomsPanel != null)  AllRoomsPanel.Visibility = Visibility.Hidden;
            if (RepairCompletePanel != null) RepairCompletePanel.Visibility = Visibility.Hidden;
            if (RepairPanel != null) RepairPanel.Visibility = Visibility.Hidden;
            if (CleanPanel != null) CleanPanel.Visibility = Visibility.Hidden;
            if (AssignPanel != null) AssignPanel.Visibility = Visibility.Hidden;
            if (CheckoutPanel != null) CheckoutPanel.Visibility = Visibility.Hidden;
            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HideAllPanels();
            if (labelMessages != null) labelMessages.Content = string.Empty;
            ComboBox cmb = sender as ComboBox;
            ComboBoxItem cbi = (ComboBoxItem)cmb.SelectedItem;
            Action = cbi.Content.ToString();
            switch (Action)
            {
                case "Request Room Assignment":
                    AssignPanel.Visibility = Visibility.Visible;
                    break;
                case "Checkout Room":
                    GetOccupiedRooms();
                    CheckoutPanel.Visibility = Visibility.Visible;
                    break;
                case "Mark Room Cleaned":
                    GetVacantRooms();
                    CleanPanel.Visibility = Visibility.Visible;
                    break;
                case "Mark Room for Repair":
                    GetVacantRooms();
                    RepairPanel.Visibility = Visibility.Visible;
                    break;
                case "List All Available Rooms":
                    GetAvailableRooms();
                    //AllRoomsPanel.Visibility = Visibility.Visible;
                    break;
                case "Mark Repair Complated":
                    GetRepairingRooms();
                    RepairCompletePanel.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void GetAvailableRooms()
        {
            if(listBoxAllRooms!=null) listBoxAllRooms.ItemsSource = HttpHelper.GetAvailableRooms();
        }

        private void GetOccupiedRooms(bool showMessages = true)
        {
            SelectedOccupiedRoom = null;
            occupiedRoomsCombo.ItemsSource = null;
            var occupiedRooms = HttpHelper.GetOccupiedRooms();
            if (occupiedRooms == null || occupiedRooms.Count == 0)
            {
                if(showMessages) labelMessages.Content = "No ocupied rooms at the moment!";
                return;
            }
            occupiedRoomsCombo.ItemsSource = occupiedRooms;
        }

        private void GetVacantRooms(bool showMessages = true)
        {
            SelectedVacantRoom = null;
            vacantRoomsCombo.ItemsSource = null;
            vacantRoomsCombo1.ItemsSource = null;
            var vacantRooms = HttpHelper.GetVacantRooms();
            if (vacantRooms == null || vacantRooms.Count == 0)
            {
                if (showMessages) labelMessages.Content = "No vacant rooms at the moment!";
                return;
            }
            vacantRoomsCombo.ItemsSource = vacantRooms;
            vacantRoomsCombo1.ItemsSource = vacantRooms;
        }

        private void GetRepairingRooms(bool showMessages = true)
        {
            SelectedRepairRoom = null;
            repairingRoomsCombo.ItemsSource = null;
            var repairingRooms = HttpHelper.GetRepairingRooms();
            if (repairingRooms == null || repairingRooms.Count == 0)
            {
                if (showMessages) labelMessages.Content = "No repairing rooms at the moment!";
                return;
            }
            repairingRoomsCombo.ItemsSource = repairingRooms;
        }

        private void occupiedRoomsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (occupiedRoomsCombo.SelectedItem != null)
            {
                SelectedOccupiedRoom = occupiedRoomsCombo.SelectedValue.ToString();
            }
        }

        private void VacantRoomsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vacantRoomsCombo.SelectedItem != null)
            {
                SelectedVacantRoom = vacantRoomsCombo.SelectedValue.ToString();
            }
        }

        private void VacantRoomsCombo1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vacantRoomsCombo1.SelectedItem != null)
            {
                SelectedVacantRoom = vacantRoomsCombo1.SelectedValue.ToString();
            }
        }

        private void repairingRoomsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (repairingRoomsCombo.SelectedItem != null)
            {
                SelectedRepairRoom = repairingRoomsCombo.SelectedValue.ToString();
            }
        }

        private void AssignRoom(object sender, RoutedEventArgs e)
        {
            string room = HttpHelper.AssignRoom();
            if (!string.IsNullOrEmpty(room)) labelMessages.Content = "Room Assigned: " + room;
            else labelMessages.Content = "No rooms available at the moment!";
            GetAvailableRooms();
        }

        private void CheckoutRoom(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SelectedOccupiedRoom))
            {
                labelMessages.Content = "Please select an occupied room to checkout";
                return;
            }
            bool success = HttpHelper.CheckoutRoom(SelectedOccupiedRoom);
            labelMessages.Content = "Room: "+SelectedOccupiedRoom + (success ?  " checkout succesful" : " already checked out!");
            GetOccupiedRooms(false);
        }

        private void MarkRoomCleaned(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SelectedVacantRoom))
            {
                labelMessages.Content = "Please select a vacant room to mark as cleaned";
                return;
            }
            bool success = HttpHelper.MarkRoomCleaned(SelectedVacantRoom);
            labelMessages.Content = "Room: " + SelectedVacantRoom + (success ? " is available for guests now" : " is already cleaned and available!");
            GetVacantRooms(false);
            GetAvailableRooms();
        }

        private void MarkRoomForRepair(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SelectedVacantRoom))
            {
                labelMessages.Content = "Please select a vacant room to mark for repair";
                return;
            }
            bool success = HttpHelper.MarkRoomForRepair(SelectedVacantRoom);
            labelMessages.Content = "Room: " + SelectedVacantRoom + (success ? " is marked for repair" : " is not available for repairing at the moment!");
            GetVacantRooms(false);
            GetAvailableRooms();
        }

        private void MarkRepairCompleted(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SelectedRepairRoom))
            {
                labelMessages.Content = "Please select a room";
                return;
            }
            bool success = HttpHelper.MarkRepairCompleted(SelectedRepairRoom);
            labelMessages.Content = "Room: " + SelectedRepairRoom + (success ? " is vacant now" : " is not not being repaired at the moment!");
            GetRepairingRooms(false);
            GetAvailableRooms();
        }

    }


}
