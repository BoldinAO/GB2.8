using APICompany.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace APICompany.Controllers
{
    public class Sql
    {
        static string _connString;
        static SqlConnection _sqlCOnn;
        static string _sql;
        static SqlDataReader _reader;
        static ObservableCollection<Department> _departmentsList;
        static ObservableCollection<Worker> _workersList;

        static Sql()
        {
            _connString = @"Data Source=(LocalDB)\MSSQLLocalDB;
                        Initial Catalog=D:\DB\COMPANY\COMPANYDB.MDF;
                        Integrated Security=True;
                        Connect Timeout=30;
                        Encrypt=False;
                        TrustServerCertificate=False;
                        ApplicationIntent=ReadWrite;
                        MultiSubnetFailover=False";

            _sqlCOnn = new SqlConnection(_connString);

            _departmentsList = new ObservableCollection<Department>();
            _workersList = new ObservableCollection<Worker>();
        }

        public static void WriteData(object DTO)
        {
            if (DTO is Worker)
                _sql = $@"INSERT INTO Worker (Id, Name, SurName, SecondName, DepartName) VALUES ({(DTO as Worker).Id}, '{(DTO as Worker).Name}', " +
                            $"'{(DTO as Worker).SurName}', '{(DTO as Worker).SecondName}', '{(DTO as Worker).Department}')";
            if (DTO is Department)
                _sql = $@"INSERT INTO Department (Id, DepartName) VALUES ({(DTO as Department).Id}, '{(DTO as Department).DepartName}')";

            using (SqlConnection sqlConnection = new SqlConnection(_connString))
            {
                sqlConnection.Open();
                var command = new SqlCommand(_sql, sqlConnection);
                command.ExecuteNonQuery();
            }
        }

        public static IEnumerable<Department> GetDepartments()
        {
            _sql = $@"SELECT * FROM Department";

            using (SqlConnection sqlConnection = new SqlConnection(_connString))
            {
                sqlConnection.Open();
                var command = new SqlCommand(_sql, sqlConnection);
                _reader = command.ExecuteReader();
                while (_reader.Read())
                {
                    yield return new Department() { Id = (int)_reader["Id"], DepartName = _reader["DepartName"].ToString() };
                }
            }
        }

        public static IEnumerable<Worker> GetWorkers()
        {
            _sql = $@"SELECT * FROM Worker";

            using (SqlConnection sqlConnection = new SqlConnection(_connString))
            {
                sqlConnection.Open();
                var command = new SqlCommand(_sql, sqlConnection);
                _reader = command.ExecuteReader();
                while (_reader.Read())
                {
                    yield return new Worker() { Id = (int)_reader["Id"],
                        Name = _reader["Name"].ToString(), SecondName = _reader["SecondName"].ToString(),
                        SurName = _reader["SurName"].ToString(), Department = _reader["DepartName"].ToString() };
                }
            }
        }

        public static void DelData(object DTO)
        {
            if (DTO is Worker)
                _sql = $@"DELETE Worker WHERE Id = {(DTO as Worker).Id}";
            if (DTO is Department)
                _sql = $@"DELETE Department WHERE Id = {(DTO as Department).Id}";

            using (SqlConnection sqlConnection = new SqlConnection(_connString))
            {
                sqlConnection.Open();
                var command = new SqlCommand(_sql, sqlConnection);
                command.ExecuteNonQuery();
            }
        }

        public static void ChangeData(object DTO)
        {
            if (DTO is Worker)
                _sql = $@"UPDATE Worker SET Name = '{(DTO as Worker).Name}', " +
                            $"SurName = '{(DTO as Worker).SurName}', SecondName = '{(DTO as Worker).SecondName}', " +
                            $"DepartName = '{(DTO as Worker).Department}' WHERE Id = {(DTO as Worker).Id}";
            if (DTO is Department)
                _sql = $@"UPDATE Department SET DepartName = '{(DTO as Department).DepartName}' WHERE Id = {(DTO as Department).Id}";

            using (SqlConnection sqlConnection = new SqlConnection(_connString))
            {
                sqlConnection.Open();
                var command = new SqlCommand(_sql, sqlConnection);
                command.ExecuteNonQuery();
            }
        }
    }
}