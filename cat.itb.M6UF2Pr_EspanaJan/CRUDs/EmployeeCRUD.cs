using NHibernate;
using Npgsql;
using cat.itb.M6UF2Pr_EspanaJan.models;
using cat.itb.M6UF2Pr_EspanaJan.Connection;

namespace cat.itb.M6UF2Pr_EspanaJan.CRUDs
{
    public class EmployeeCRUD
    {
        public List<Employee> SelectAllEmployee()
        {
            using (var session = SessionFactoryCloud.Open())
            {
                return session.Query<Employee>().ToList();
            }
        }
        public Employee SelectEmployeeById(int id)
        {
            Employee employee;
            using (var session = SessionFactoryCloud.Open())
            {
                employee = session.Get<Employee>(id);
                session.Close();
            }
            return employee;
        }
        public void InsertEmployee(Employee employee)
        {
            using (var session = SessionFactoryCloud.Open())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(employee);
                    transaction.Commit();
                    Console.WriteLine("Employee inserted");
                    session.Close();
                }
            }
        }
        public void UpdateEmployee(Employee employee)
        {
            using (var session = SessionFactoryCloud.Open())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(employee);
                        transaction.Commit();
                        Console.WriteLine("Employee updated");
                    }
                    catch (Exception e)
                    {
                        if (!transaction.WasCommitted)
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }
        }
        public void DeleteEmployee(Employee employee)
        {
            using (var session = SessionFactoryCloud.Open())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(employee);
                    transaction.Commit();
                    Console.WriteLine("Employee deleted");
                    session.Close();
                }
            }
        }
        public void InsertADO(List<Employee> employees)
        {
            string sql = "INSERT INTO EMPLOYEE (SURNAME, JOB, MANAGERNO, STARTDATE, SALARY, COMMISSION, DEPTNO) VALUES (@SURNAME, @JOB, @MANAGERNO, @STARTDATE, @SALARY, @COMMISSION, @DEPTNO)";
            using (NpgsqlConnection connection = new CloudConnection().GetConnection())
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    foreach (Employee employee in employees)
                    {
                        command.Parameters.AddWithValue("SURNAME", employee.Surname);
                        command.Parameters.AddWithValue("JOB", employee.Job);
                        command.Parameters.AddWithValue("MANAGERNO", employee.ManagerNo);
                        command.Parameters.AddWithValue("STARTDATE", employee.StartDate);
                        command.Parameters.AddWithValue("SALARY", employee.Salary);
                        command.Parameters.AddWithValue("COMMISSION", employee.Commission == null ? 0 : employee.Commission);
                        command.Parameters.AddWithValue("DEPTNO", employee.DeptNo);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    Console.WriteLine("Employees inserted correctly.");
                }
            }
        }
        public Employee SelectByNameADO(string name)
        {
            Employee employee = new Employee();
            string sql = "SELECT * FROM EMPLOYEE WHERE SURNAME = @SURNAME";
            using (NpgsqlConnection connection = new CloudConnection().GetConnection())
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("SURNAME", name.ToUpper());
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employee.Id = reader.GetInt32(0);
                            employee.Surname = reader.GetString(1);
                            employee.Job = reader.GetString(2);
                            employee.ManagerNo = reader.GetInt32(3);
                            employee.StartDate = reader.GetDateTime(4);
                            employee.Salary = reader.GetDouble(5);
                            employee.Commission = reader.GetDouble(6);
                            employee.DeptNo = reader.GetInt32(7);
                        }
                    }
                }
            }
            return employee;
        }
        public void DeleteADO(Employee employee)
        {
            string sql = "DELETE FROM EMPLOYEE WHERE ID = @ID";
            using (NpgsqlConnection connection = new CloudConnection().GetConnection())
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("ID", employee.Id);
                    if (command.ExecuteNonQuery() != 0)
                    {
                        Console.WriteLine($"Employee {employee.Surname} deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Error deleting {employee.Surname}.");
                    }
                }
            }
        }
        public Employee SelectByName(string name)
        {
            using (var session = SessionFactoryCloud.Open())
            {
                return session.Query<Employee>().Where(e => e.Surname == name).FirstOrDefault();
            }
        }
    }
}
