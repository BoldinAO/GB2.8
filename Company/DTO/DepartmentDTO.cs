namespace Company
{
    class DepartmentDTO : MyPropertyChanged
    {
        string departName;
        //Id департамента
        public int Id { get; set; }
        //Наименование департамента
        public string DepartName {
            get
            {
                return departName;
            }
            set
            {
                departName = value;
                OnPropertyChanged("DepartName");
            }
        }
    }
}