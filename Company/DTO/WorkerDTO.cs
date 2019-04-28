namespace Company.DTO
{
    class WorkerDTO : MyPropertyChanged
    {
        private string _secondName;
        private string _name;
        private string _surName;
        private string _department;
        //Id департамента
        public int Id { get; set; }
        //Имя сотрудника
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged("Name"); } }
        //Фамилия сотрудника
        public string SurName { get { return _surName; } set { _surName = value; OnPropertyChanged("SurName"); } }
        //Отчество сотрудника
        public string SecondName { get { return _secondName; } set { _secondName = value; OnPropertyChanged("SecondName"); } }
        //Департамент сотрудника
        public string Department { get { return _department; } set { _department = value; OnPropertyChanged("Department"); } }
    }
}
