using Company.DTO;
using System.Windows;

namespace Company
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DepartmentViewModel depart = new DepartmentViewModel();
        EmployeeViewModel emoloyee = new EmployeeViewModel();

        public MainWindow()
        {
            InitializeComponent();
            departCB.DataContext = depart;
            listView.DataContext = emoloyee;
        }

        /// <summary>
        /// Добавление департамента
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            depart.SaveDepart(DepartNameTB.Text);
            DepartNameTB.Clear();
        }

        /// <summary>
        /// Удаление департамента
        /// </summary>
        private void DelDepartBtn_Click(object sender, RoutedEventArgs e)
        {
            depart.DelDepart(departCB.SelectedItem, emoloyee);
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        private void ChangeDataEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            emoloyee.DelEmployee(listView.SelectedItem);
        }

        /// <summary>
        /// Изменение наименования департамента
        /// </summary>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            depart.ChangeDepartmentName(departCB.SelectedItem, DepartNameTB.Text, emoloyee);
        }

        /// <summary>
        /// Изменить данные пользователя
        /// </summary>
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            emoloyee.ChangeEmployeeData(listView.SelectedItem, TbFirstName.Text, TbSecondName.Text, TbSurname.Text, departCB.SelectedItem);
        }

        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        private void BtAddWorker_Click(object sender, RoutedEventArgs e)
        {
            if(departCB.Text != "")
                emoloyee.SaveWorker(TbFirstName.Text, TbSecondName.Text, TbSurname.Text, (departCB.SelectedItem as DepartmentDTO).DepartName);
        }

        private void ListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(listView.SelectedItem != null)
            {
                TbFirstName.Text = (listView.SelectedItem as WorkerDTO).Name;
                TbSecondName.Text = (listView.SelectedItem as WorkerDTO).SecondName;
                TbSurname.Text = (listView.SelectedItem as WorkerDTO).SurName;
                departCB.Text = (listView.SelectedItem as WorkerDTO).Department;
            }
        }
    }
}