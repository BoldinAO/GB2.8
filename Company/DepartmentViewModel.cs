using Company.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Company
{
    class DepartmentViewModel : MyPropertyChanged
    {
        private readonly CollectionView departments;
        private string department;
        ObservableCollection<DepartmentDTO> list;
        HttpClient client;
        const string url = @"http://localhost:54317/api/department/";

        public DepartmentViewModel()
        {
            client = new HttpClient();
            list = new ObservableCollection<DepartmentDTO>();
            departments = new CollectionView(list);
            LoadDeparts();
        }

        public string Department
        {
            get { return department; }
            set
            {
                if (department == value) return;
                department = value;
            }
        }

        public CollectionView Departments
        {
            get { return departments; }
        }

        /// <summary>
        /// Загрузка списка департаментов
        /// </summary>
        private void LoadDeparts()
        {
            list.Clear();
            var s = client.GetStringAsync(url).Result;
            var departments = JsonConvert.DeserializeObject<List<DepartmentDTO>>(s);
            foreach (var depart in departments)
                list.Add(depart);
        }

        /// <summary>
        /// Создание департамента
        /// </summary>
        /// <param name="name">Наименование департамента</param>
        public void SaveDepart(string name)
        {
            var depart = new DepartmentDTO() { DepartName = name };
            var content = new StringContent(JsonConvert.SerializeObject(depart), Encoding.UTF8, "application/json");
            client.PostAsync(url, content).Wait();

            LoadDeparts();
        }

        /// <summary>
        /// Удаление департамента из списка
        /// </summary>
        /// <param name="department">Департамент</param>
        public void DelDepart(object department, EmployeeViewModel employee)
        {
            var depart = (DepartmentDTO)department;
            var emp = new ObservableCollection<WorkerDTO>();

            foreach (var e in employee.GetEmployeeList)
            {
                emp.Add(e);
            }

            foreach (var em in emp)
            {
                if (em.Department == depart.DepartName)
                    for (var i = 0; i < employee.GetEmployeeList.Count; i++)
                    {
                        if (employee.GetEmployeeList[i].Id == em.Id)
                            employee.DelEmployee(employee.GetEmployeeList[i]);
                    }
            }

            client.DeleteAsync(url + depart.Id).Wait();

            LoadDeparts();
        }

        /// <summary>
        /// Изменение наименования департамента
        /// </summary>
        /// <param name="departmentName">Наименование департамента</param>
        public void ChangeDepartmentName(object depart, string departmentName, EmployeeViewModel employee)
        {
            if(depart != null)
            {
                var department = (DepartmentDTO)depart;
                foreach (var s in employee.GetEmployeeList)
                {
                    if (s.Department == department.DepartName)
                    {
                        s.Department = departmentName;
                        employee.ChangeEmployeeData(s, s.Name, s.SecondName, s.SurName, new DepartmentDTO() { Id = (depart as DepartmentDTO).Id, DepartName = departmentName });
                    }
                }

                department.DepartName = departmentName;
                var content = new StringContent(JsonConvert.SerializeObject(department), Encoding.UTF8, "application/json");
                client.PutAsync(url, content);
            }
        }
    }
}
