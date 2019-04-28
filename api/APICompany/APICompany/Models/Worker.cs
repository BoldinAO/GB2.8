namespace APICompany.Models
{
    public class Worker
    {
        public int Id { get; set; }
        //Имя сотрудника
        public string Name { get; set; }
        //Фамилия сотрудника
        public string SurName { get; set; }
        //Отчество сотрудника
        public string SecondName { get; set; }
        //Департамент сотрудника
        public string Department { get; set; }
    }
}