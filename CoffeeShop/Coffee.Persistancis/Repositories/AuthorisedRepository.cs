using Coffee.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Persistancis.Repositories
{
    public class AuthorisedRepository
    {
        MainRepository _MainRepository = new MainRepository();

        public decimal AdminLogin(Admin _Admin)
        {
            string query = "Select Count(*)from Admin Where Name='" + _Admin.Name + "' And Password='"+_Admin.Password+"' ";
            return _MainRepository.ExecuteScalar(query, _MainRepository.ConnectionString());
        }

        public decimal AuthorisedLogin(Authorizations _Authorization)
        {
            string query = "Select Count(*)from Users Where Person='" + _Authorization.Person + "' And Password='" + _Authorization.Password + "' ";
            return _MainRepository.ExecuteScalar(query, _MainRepository.ConnectionString());
        }
    }
}
