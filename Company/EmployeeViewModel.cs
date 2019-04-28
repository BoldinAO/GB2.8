using Company.DTO;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace Company
{
    class EmployeeViewModel
    {
        private readonly CollectionView workers;
        ObservableCollection<WorkerDTO> list;
        HttpClient client;
        const string url = @"http://localhost:54317/api/worker/";

        public EmployeeViewModel()
        {
            client = new HttpClient();
            list = new ObservableCollection<WorkerDTO>();
            workers = new CollectionView(list);
            LoadWorker();
        }

        public CollectionView Workers
        {
            get { return workers; }
        }

        public ObservableCollection<WorkerDTO> GetEmployeeList
        {
            get { return list; }
        }

        public object Current => throw new NotImplementedException();

        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="secondName">Отчество</param>
        /// <param name="surName">Фамилия</param>
        /// <param name="depart">Департамент</param>
        public void LoadWorker()
        {
            list.Clear();
            var Workers = JsonConvert.DeserializeObject<List<WorkerDTO>>(client.GetStringAsync(url).Result);
            foreach(var w in Workers)
                list.Add(w);
        }

        public void SaveWorker(string name, string secondName, string surName, string depart)
        {
            if(name != null && secondName != null && depart != null)
            {
                var worker = new WorkerDTO() { Name = name, Department = depart, SecondName = secondName, SurName = surName };
                var stringContent = new StringContent(JsonConvert.SerializeObject(worker), Encoding.UTF8, "application/json");
                client.PostAsync(url, stringContent).Wait();

                LoadWorker();
            }
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник</param>
        public void DelEmployee(object employee)
        {
            client.DeleteAsync(url + (employee as WorkerDTO).Id).Wait();

            LoadWorker();
        }

        /// <summary>
        /// Изменение данных пользователя
        /// </summary>
        /// <param name="departmentName">Наименование департамента</param>
        public void ChangeEmployeeData(object workr, string name = null, string secondName = null, string surName = null, object depart = null)
        {
            if(workr != null && depart != null)
            {
                WorkerDTO worker = (WorkerDTO)workr;
                worker.Name = name != "" ? name : worker.Name;
                worker.SecondName = secondName;
                worker.SurName = surName != "" ? surName : worker.SurName;
                DepartmentDTO department = (DepartmentDTO)depart;
                worker.Department = department.DepartName;
                var s = JsonConvert.SerializeObject(worker);
                var stringContent = new StringContent(JsonConvert.SerializeObject(worker), Encoding.UTF8, "application/json");
                client.PutAsync(url, stringContent).Wait();
            }
        }
    }
}
