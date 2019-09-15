using Coffee.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Persistancis.Repositories
{
    public class UserRepository
    {
        MainRepository _MainRepository = new MainRepository();
        public decimal AlreadyExistName(Users _Users)
        {
            string query = "Select Count(*)from Users Where Person='" + _Users.Person + "' ";
            return _MainRepository.ExecuteScalar(query, _MainRepository.ConnectionString());
        }
        public int Add(Users _Users)
        {
            string query = "Insert Into Users(Person,Password,AdminBy) Values ('" + _Users.Person + "','" + _Users.Password + "','" + _Users.AdminBy + "')";
            return _MainRepository.ExecuteNonQuery(query, _MainRepository.ConnectionString());
        }
        public int Update(Users _Users)
        {
            string query = "Update Users SET Person='" + _Users.Person + "',Password='"+_Users.Password+"' where Id='" + _Users.Id + "' ";
            return _MainRepository.ExecuteNonQuery(query, _MainRepository.ConnectionString());
        }

        public int Delete(Users _Users)
        {
            string query = ("Delete From Users Where Id='" + _Users.Id + "' ");
            return _MainRepository.ExecuteNonQuery(query, _MainRepository.ConnectionString());
        }
        public List<Users> GetAllUsers()
        {
            var _UsersList = new List<Users>();
            string query = ("Select *From Users");
            var reader = _MainRepository.Reader(query, _MainRepository.ConnectionString());
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var _Users = new Users();
                    _Users.Id = Convert.ToInt32(reader["Id"].ToString());
                    _Users.Person = reader["Person"].ToString();
                    _Users.Password = reader["Password"].ToString();
                    _Users.AdminBy = reader["AdminBy"].ToString();

                    _UsersList.Add(_Users);
                }
            }
            reader.Close();

            return _UsersList;
        }
    }
}
