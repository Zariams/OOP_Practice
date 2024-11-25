using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OOP_Practice
{
    public partial class MainWindow : Window
    {
        private Hotel hotel;
        string[] parametreNamesRoom =
          {
                "ID",
                "Ціна оренди, $/день",
                "Тип"

            };
        string[] parametreNamesTenant =
        {
                "ID",
                "Ім'я",
                "Прізвище",
                "Дата народження"

        };
        string[] parametreNamesStaff =
        {
                "ID",
                "Ім'я",
                "Прізвище",
                "Професія",
                "Заробітна плата,\n$/день",
                "Востаннє отримав(ла)\nзаробітну плату "
        };
        string[] parametreNamesReservation =
        {
                "ID жильця",
                "ID кімнати",
                "Дата початку",
                "Дата кінця"

        };

        public MainWindow()
        {
            InitializeComponent();

        }
        private void SetGridAppearance(DataGrid dtgrdvw, string[] headerNames)
        {
            int max_col = headerNames.Length;
            dtgrdvw.AutoGenerateColumns = false;
            dtgrdvw.Columns.Clear();
            dtgrdvw.ItemsSource = null;
            dtgrdvw.IsReadOnly = true;
            dtgrdvw.CanUserSortColumns = false;
            for (int i = 0; i < max_col; i++)
            {
                var col = new DataGridTextColumn();
                col.Header = headerNames[i];
                col.Binding = new Binding(string.Format("[{0}]", i));
                col.Width = dtgrdvw.Width / max_col;
                dtgrdvw.Columns.Add(col);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetGridAppearance(Dtgrd_Room,parametreNamesRoom);
            SetGridAppearance(Dtgrd_Reservation, parametreNamesReservation);
            SetGridAppearance(Dtgrd_Staff, parametreNamesStaff);
            SetGridAppearance(Dtgrd_Tenant, parametreNamesTenant);
            LstBx_Room.ItemsSource = Enum.GetNames(typeof(RoomType));
            LstBx_Staff_Job.ItemsSource = Enum.GetNames(typeof(Job));
            AddListBoxScrollHandler(LstBx_Room);
            AddListBoxScrollHandler(LstBx_Staff_Job);
            SwitchCanvas(0);
        }

        private void SwitchCanvas(int i)
        {
            Canvas[] slides = {
                Canvas_Hotel,
                Canvas_Room,
                Canvas_Tenant,
                Canvas_Staff,
                Canvas_Reservation,
                Canvas_Clock
            };
            for (int j = 0; j < slides.Length; j++)
            {
                if (j == i)
                    slides[j].Visibility = Visibility.Visible;
                else 
                    slides[j].Visibility = Visibility.Hidden;
            }
        }


        private void HotelUpdateInformation()
        {
            if (hotel == null)
            {
                Canvas_Hotel_Extended.Visibility = Visibility.Hidden;
                MnuIt_Main.IsEnabled = false;
            }
            else
            {
                Canvas_Hotel_Extended.Visibility = Visibility.Visible;
                TxtBx_Hotel_Balance.Text = hotel.Account.Balance.ToString();
                TxtBx_Hotel_Reservations_Total.Text = hotel.Reservations.FindAll(x => !x.IsDeleted).Count.ToString();
                TxtBx_Hotel_Reservations_Active.Text = hotel.Reservations.FindAll(x => x.IsActiveToday&&!x.IsDeleted).Count.ToString();
                TxtBx_Hotel_Reservations_Deleted.Text = hotel.Reservations.FindAll(x => !x.IsActiveToday && !x.IsDeleted).Count.ToString();
                TxtBx_Hotel_Tenants.Text = hotel.Tenants.Count.ToString();
                TxtBx_Hotel_Rooms.Text = hotel.Rooms.Count.ToString();
                TxtBx_Hotel_Staff.Text = hotel.Staff.Count.ToString();
                Lbl_Hotel_Info.Content = $"Інформація про готель \"{hotel.Name}\"";
                Lbl_Hotel_Control.Content = $"Керування готелем \"{hotel.Name}\"";
                TxtBx_Hotel_Expected_Rent.Text = hotel.GetExpectedRentPay().ToString();
                TxtBx_Hotel_Expected_Salary.Text = hotel.GetExpectedTotalSalary().ToString();
                MnuIt_Main.IsEnabled = true;
            }
        }
        private void Canvas_Hotel_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            HotelUpdateInformation();
        }
        private void Btn_Hotel_Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = TxtBx_Hotel_Name.Text;
                string address = TxtBx_Hotel_Address.Text;
                hotel = new Hotel(name, address);
                HotelUpdateInformation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка!");
            }
        }

        private void Btn_Hotel_Balance_Withdraw_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double num = Double.Parse(TxtBx_Hotel_Balance_Change.Text) ;
                if (hotel.Account.Withdraw(num))
                {
                    MessageBox.Show("Гроші було успішно знято з рахунку!","Повідомлення");
                    HotelUpdateInformation();
                }
                else
                {
                    throw new Exception("Недостатньо грошей на рахунку");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Некоректне число", "Помилка!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка!");
            }
        }

        private void Btn_Hotel_Balance_Deposit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double num = Double.Parse(TxtBx_Hotel_Balance_Change.Text);
                hotel.Account.Deposit(num);
                HotelUpdateInformation();
                MessageBox.Show("Гроші було успішно покладено на рахунок!", "Повідомлення");
            }
            catch (FormatException)
            {
                MessageBox.Show("Некоректне число", "Помилка!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка!");
            }
        }

        private void Btn_Hotel_WithdrawRent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Reservation> cancelledReservations = hotel.WithdrawDailyFee();
               
                HotelUpdateInformation();

                if (cancelledReservations.Count > 0)
                {
                    MessageBox.Show($"Через недостаток грошей на рахунках клієнтів, було скасовано {cancelledReservations.Count} оренд(и)", "Повідомлення");
                }
                else
                {
                    MessageBox.Show("Орендна плата була успішно знята у повному обсязі!","Повідомлення");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка!");
            }
        }

        private void Btn_Hotel_PaySalary_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double currentBalance = hotel.Account.Balance;
                double expectedPay = hotel.GetExpectedTotalSalary();
                hotel.PayStaffSalaries();
                if (hotel.Account.Balance != currentBalance-expectedPay)
                {
                    MessageBox.Show($"Через недостаток грошей на рахунку готелю, заробітну плату було виплачено не в повному обсязі!", "Повідомлення");
                }
                else
                {
                    MessageBox.Show("Заробітну плату було успішно сплачено!", "Повідомлення");
                }
                HotelUpdateInformation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка!");
            }
        }
       
        
      
        private void RoomsUpdateInfo()
        {
            if (hotel == null)
            {
               Dtgrd_Room.ItemsSource = null;
            }
            else
            {
                List<Room> rooms = hotel.Rooms;
                rooms.Sort();
                int selectedIndex = Dtgrd_Room.SelectedIndex;
                object[][] output = new object[rooms.Count][];
                
                for (int i = 0; i < hotel.Rooms.Count; i++)
                {
                    output[i] = new object[parametreNamesRoom.Length];
                    Room r = rooms[i];
                    output[i][0] = r.ID;
                    output[i][1] = r.DailyCost;
                    output[i][2] = r.Type;
                }
                Dtgrd_Room.ItemsSource = output;
                Dtgrd_Room.SelectedIndex = selectedIndex;
            }
        }
        private void Canvas_Room_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            RoomsUpdateInfo();
        }
        private void Btn_Room_Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int rent = int.Parse(TxtBx_Room_RentCost.Text);
                if (LstBx_Room.SelectedIndex == -1)
                    throw new Exception("Спочатку необхідно обрати тип кімнати!");
                RoomType roomType = (RoomType)LstBx_Room.SelectedIndex;
                Room room = new Room(rent,roomType);
                hotel.RegisterNewRoom(room);
                RoomsUpdateInfo();
            }
            catch (FormatException)
            {
                MessageBox.Show("Некоректне число", "Помилка!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка!");
            }
        }

        private void Btn_Room_Copy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Dtgrd_Room.SelectedIndex == -1) throw new Exception("Спочатку оберіть кімнату у таблиці зверху!");
                Room room = hotel.Rooms[Dtgrd_Room.SelectedIndex];
                Room copy = (Room)room.Clone();
                hotel.RegisterNewRoom(copy);
                RoomsUpdateInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка!");
            }
        }
        private void TenantsUpdateInfo()
        {
            if (hotel == null)
            {
                Dtgrd_Tenant.ItemsSource = null;
            }
            else
            {
                int selectedIndex = Dtgrd_Tenant.SelectedIndex;
                object[][] output = new object[hotel.Tenants.Count][];
                for (int i = 0; i < hotel.Tenants.Count; i++)
                {
                    output[i] = new object[parametreNamesTenant.Length];
                    Person r = hotel.Tenants[i];
                    output[i][0] = r.ID;
                    output[i][1] = r.FirstName;
                    output[i][2] = r.LastName;
                    output[i][3] = r.BirthDate;
                }
                Dtgrd_Tenant.ItemsSource = output;
                if (selectedIndex == -1)
                {
                    TxtBx_Tenant_Balance.Text = "";

                }
                else
                {
                    Dtgrd_Tenant.SelectedIndex = selectedIndex; 
                    Tenant tenant = hotel.Tenants[selectedIndex];
                    TxtBx_Tenant_Balance.Text = tenant.Account.Balance.ToString();
                }
            }
        }
        private void Canvas_Tenant_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TenantsUpdateInfo();    
        } private void Dtgrd_Tenant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Dtgrd_Tenant.SelectedIndex == -1)
            {
                TxtBx_Tenant_Balance.Text = "";

            }
            else
            {
                Tenant tenant = hotel.Tenants[Dtgrd_Tenant.SelectedIndex];
                TxtBx_Tenant_Balance.Text = tenant.Account.Balance.ToString();
            }
        }

        private void Btn_Tenant_Withdraw_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Dtgrd_Tenant.SelectedIndex == -1)
                    throw new Exception("Спочатку оберіть жильця готелю зі списку вище!");
                Tenant tenant = hotel.Tenants[Dtgrd_Tenant.SelectedIndex];
                double num = Double.Parse(TxtBx_Tenant_Balance_Change.Text);
                if (tenant.Account.Withdraw(num))
                {
                    MessageBox.Show("Гроші було успішно знято з рахунку!", "Повідомлення");
                    TenantsUpdateInfo();
                }
                else
                {
                    throw new Exception("Недостатньо грошей на рахунку");
                }
             
            }
            catch (FormatException)
            {
                MessageBox.Show("Некоректне число", "Помилка!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка!");
            }
        }

        private void Btn_Tenant_Deposit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Dtgrd_Tenant.SelectedIndex == -1)
                    throw new Exception("Спочатку оберіть жильця готелю зі списку вище!");
                Tenant tenant = hotel.Tenants[Dtgrd_Tenant.SelectedIndex];
                double num = Double.Parse(TxtBx_Tenant_Balance_Change.Text);
                tenant.Account.Deposit(num);
                MessageBox.Show("Гроші було успішно покладено на рахунок!", "Повідомлення");
                TenantsUpdateInfo();
            }
            catch (FormatException)
            {
                MessageBox.Show("Некоректне число", "Помилка!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка!");
            }
        }

        private void Btn_Tenant_Register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string firstName = TxtBx_Tenant_FirstName.Text;
                string lastName = TxtBx_Tenant_LastName.Text;
                DateTime date = Dtpckr_Tenant_Birthdate.SelectedDate ?? Clock.Now;
                //Person tenant = new Tenant(firstName, lastName, date);
                hotel.RegisterTenant(firstName,lastName,date);
                MessageBox.Show("Нового жильця було успішно зареєстровано!", "Повідомлення");
                TenantsUpdateInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка!");
            }
        }
        private void StaffUpdateInfo()
        {
            if (hotel == null)
            {
                Dtgrd_Staff.ItemsSource = null;
            }
            else
            {
                int selectedIndex = Dtgrd_Staff.SelectedIndex;
                List<StaffMember> staffList = hotel.Staff.FindAll(x =>!x.IsFired);
                object[][] output = new object[staffList.Count][];
                for (int i = 0; i < staffList.Count; i++)
                {
                    output[i] = new object[parametreNamesStaff.Length];
                    StaffMember r = staffList[i];
                    output[i][0] = r.ID;
                    output[i][1] = r.FirstName;
                    output[i][2] = r.LastName;
                    output[i][3] = r.Job;
                    output[i][4] = r.DailyRate;
                    output[i][5] = r.LastSalaryPay;
                }
                Dtgrd_Staff.ItemsSource = output;
                if (selectedIndex == -1 || staffList.Count-1 < selectedIndex)
                {
                    TxtBx_Staff_Balance.Text = "";

                }
                else
                {

                    Dtgrd_Staff.SelectedIndex = selectedIndex;
                    StaffMember staff = staffList[selectedIndex];
                    TxtBx_Staff_Balance.Text = staff.Account.Balance.ToString();
                }
            }
        }
        private void Canvas_Staff_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            StaffUpdateInfo();
        }
        private void Dtgrd_Staff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            int selectedIndex = Dtgrd_Staff.SelectedIndex;
            if (selectedIndex == -1)
            {
                TxtBx_Staff_Balance.Text = "";

            }
            else
            {
                Dtgrd_Staff.SelectedIndex = selectedIndex;
                StaffMember staff = hotel.Staff[selectedIndex];
                TxtBx_Staff_Balance.Text = staff.Account.Balance.ToString();
            }
        }

        private void Btn_Staff_Register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (LstBx_Staff_Job.SelectedIndex == -1)
                    throw new Exception("Спочатку необхідно обрати професію зі списку!");
                string firstName = TxtBx_Staff_FirstName.Text;
                string lastName = TxtBx_Staff_LastName.Text;
                DateTime date = Dtpckr_Staff_Birthdate.SelectedDate ?? Clock.Now;
                double salary = Double.Parse(TxtBx_Staff_Salary.Text);
                Job job = (Job)LstBx_Staff_Job.SelectedIndex;
                //Person tenant = new Tenant(firstName, lastName, date);
                hotel.HireStaff(firstName, lastName, date, job, salary);
                StaffUpdateInfo();
            }
            catch (FormatException)
            {
                MessageBox.Show("Некоректне число", "Помилка!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка!");
            }

        }

        private void Btn_Staff_Withdraw_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (Dtgrd_Staff.SelectedIndex == -1)
                    throw new Exception("Спочатку оберіть робітника готелю зі списку вище!");
                StaffMember staff = hotel.Staff.FindAll(x => !x.IsFired)[Dtgrd_Staff.SelectedIndex];
                double num = Double.Parse(TxtBx_Staff_Balance_Change.Text);
                if (staff.Account.Withdraw(num))
                {
                    MessageBox.Show("Гроші було успішно знято з рахунку!", "Повідомлення");
                    StaffUpdateInfo();
                }
                else
                {
                    throw new Exception("Недостатньо грошей на рахунку");
                }

            }
            catch (FormatException)
            {
                MessageBox.Show("Некоректне число", "Помилка!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка!");
            }

        }

        private void Btn_Staff_Deposit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Dtgrd_Staff.SelectedIndex == -1)
                    throw new Exception("Спочатку оберіть робітника готелю зі списку вище!");
                StaffMember staff = hotel.Staff.FindAll(x => !x.IsFired)[Dtgrd_Staff.SelectedIndex];
                double num = Double.Parse(TxtBx_Staff_Balance_Change.Text);
                staff.Account.Deposit(num);
                MessageBox.Show("Гроші було успішно покладено на рахунок!", "Повідомлення");
                StaffUpdateInfo();
            }
            catch (FormatException)
            {
                MessageBox.Show("Некоректне число", "Помилка!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка!");
            }

        }
        private void Btn_Staff_Fire_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Dtgrd_Staff.SelectedIndex == -1)
                    throw new Exception("Спочатку оберіть робітника готелю зі списку вище!");
                List<StaffMember> staffList = hotel.Staff.FindAll(x => !x.IsFired);
                StaffMember staff = staffList[Dtgrd_Staff.SelectedIndex];
                hotel.FireStaff(staff.ID);
                MessageBox.Show("Робітника було успішно звільнено!", "Повідомлення");
                StaffUpdateInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка!");
            }
        }
        private void ReservationUpdateInfo() {
            if (hotel == null)
            {
                Dtgrd_Reservation.ItemsSource = null;
            }
            else
            {
                int selectedIndex = Dtgrd_Reservation.SelectedIndex;
                List<Reservation> reservation = hotel.Reservations.FindAll(x => !x.IsDeleted);
                object[][] output = new object[reservation.Count][];
                for (int i = 0; i < reservation.Count; i++)
                {
                    output[i] = new object[parametreNamesStaff.Length];
                    Reservation r = reservation[i];
                    output[i][0] = r.TenantID;
                    output[i][1] = r.RoomID;
                    output[i][2] = r.StartDate;
                    output[i][3] = r.EndDate;
                }
                Dtgrd_Reservation.ItemsSource = output;
                if (!(selectedIndex == -1)&& !(reservation.Count - 1 < selectedIndex))
                    Dtgrd_Reservation.SelectedIndex = selectedIndex;
                   
            }
        }
        private void Canvas_Reservation_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ReservationUpdateInfo();
        }
        private void Btn_Reservation_Register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int tenantId = int.Parse(TxtBx_Reservation_Tenant.Text);
                int roomId = int.Parse(TxtBx_Reservation_Room.Text);
                DateTime start = Dtpckr_Reservation_Start.SelectedDate?.AddHours(Clock.Now.Hour).AddMinutes(Clock.Now.Minute + 1) ?? Clock.Now;
                DateTime end = Dtpckr_Reservation_End.SelectedDate?.AddHours(Clock.Now.Hour).AddMinutes(Clock.Now.Minute+1) ?? Clock.Now;
                hotel.BookARoom(tenantId, roomId, start, end);
                MessageBox.Show("Кімнату було успішно заброньовано!","Повідомлення");
                ReservationUpdateInfo();
            }
            catch (FormatException)
            {
                MessageBox.Show("Некоректне число", "Помилка!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка!");
            }
        }
        private void Btn_Reservation_Cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int tenantId = int.Parse(TxtBx_Reservation_Tenant.Text);
                int roomId = int.Parse(TxtBx_Reservation_Room.Text);
                hotel.CancelRoomReservation(tenantId, roomId);
                MessageBox.Show("Оренду було успішно скасовано!", "Повідомлення");
                ReservationUpdateInfo();
            }
            catch (FormatException)
            {
                MessageBox.Show("Введіть коректні ID жильця та кімнати", "Помилка!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка!");
            }
        }
        private void ClockUpdateInfo()
        {
            TxtBx_Clock_CurrentTime.Text = Clock.Now.ToString();
        }
        private void Canvas_Clock_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ClockUpdateInfo();
        }
        private void Dtpckr_Clock_Current_DataContextChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DateTime date = Dtpckr_Clock_Current.SelectedDate?.AddHours(Clock.Now.Hour).AddMinutes(Clock.Now.Minute+1) ?? Clock.Now;
                if (date < Clock.Now)
                    throw new Exception("Оберіть дату у майбутньому");
                Clock.Offset += date - Clock.Now;
                ClockUpdateInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка!");
            }
        }
        private void MnuIt_Hotel_Click(object sender, RoutedEventArgs e)
        {
            SwitchCanvas(0);
        }

        private void MnuIt_Room_Click(object sender, RoutedEventArgs e)
        {
            SwitchCanvas(1);
        }

        private void MnuIt_Tenant_Click(object sender, RoutedEventArgs e)
        {
            SwitchCanvas(2);
        }

        private void MnuIt_Staff_Click(object sender, RoutedEventArgs e)
        {
            SwitchCanvas(3);
        }

        private void MnuIt_Reservation_Click(object sender, RoutedEventArgs e)
        {
            SwitchCanvas(4);
        }

        private void MnuIt_Time_Click(object sender, RoutedEventArgs e)
        {
            SwitchCanvas(5);
        }

        private void MnuIt_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private Visual GetDescendantByType(Visual element, Type type)
        {
            if (element == null)
            {
                return null;
            }
            if (element.GetType() == type)
            {
                return element;
            }
            Visual foundElement = null;
            if (element is FrameworkElement)
            {
                (element as FrameworkElement)?.ApplyTemplate();
            }
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
                foundElement = GetDescendantByType(visual, type);
                if (foundElement != null)
                {
                    break;
                }
            }
            return foundElement;
        }
        private void ScrollBarHandler(object sender, ListBox lstbx)
        {
            ScrollBar scroll = (ScrollBar)sender;
            if (lstbx != null && scroll.Value < lstbx.Items.Count)
                lstbx.SelectedIndex = (int)scroll.Value;
        }
        private void AddListBoxScrollHandler(ListBox lstbx)
        {
            Visual visual = GetDescendantByType(lstbx, typeof(ScrollBar));
            if (visual != null)
            {
                ScrollBar lbScrollBar = (ScrollBar)visual;
                lbScrollBar.ValueChanged += delegate (object sender, RoutedPropertyChangedEventArgs<double> args) { ScrollBarHandler(sender, lstbx); };
                lstbx.SelectedIndex = 0;
            }


        }
    }
}
