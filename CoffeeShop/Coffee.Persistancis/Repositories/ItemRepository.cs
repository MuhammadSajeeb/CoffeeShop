using Coffee.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Persistancis.Repositories
{
    public class ItemRepository
    {
        MainRepository _MainRepository = new MainRepository();
        public decimal AlreadyExistItem(ItemsM _Items)
        {
            string query = "Select Count(*)from Items Where Name='" + _Items.Name + "' And Categoriesid='" + _Items.CategoriesId + "' ";
            return _MainRepository.ExecuteScalar(query, _MainRepository.ConnectionString());
        }
        public int Add(ItemsM _Items)
        {
            string query = "Insert Into Items(Name,Price,CategoriesId) Values ('" + _Items.Name + "','" + _Items.Price + "','" + _Items.CategoriesId + "')";
            return _MainRepository.ExecuteNonQuery(query, _MainRepository.ConnectionString());
        }
        public int Update(ItemsM _Items)
        {
            string query = "Update Items SET Name='" + _Items.Name + "',Price='" + _Items.Price + "' Where Id='" + _Items.Id + "' ";
            return _MainRepository.ExecuteNonQuery(query, _MainRepository.ConnectionString());
        }

        public int Delete(ItemsM _Items)
        {
            string query = ("Delete From Items Where Id='" + _Items.Id + "'");
            return _MainRepository.ExecuteNonQuery(query, _MainRepository.ConnectionString());
        }
        public List<Categories> GetAllCategories()
        {
            var _CategoryList = new List<Categories>();
            string query = ("Select *From Categories");
            var reader = _MainRepository.Reader(query, _MainRepository.ConnectionString());
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var _Category = new Categories();
                    _Category.Id = Convert.ToInt32(reader["Id"].ToString());
                    _Category.Name = reader["Name"].ToString();

                    _CategoryList.Add(_Category);
                }
            }
            reader.Close();

            return _CategoryList;
        }
        public List<ItemsM> GetAll(int id)
        {
            var _ItemsList = new List<ItemsM>();
            string query = ("Select *From Items where CategoriesId='" + id + "' ");
            var reader = _MainRepository.Reader(query, _MainRepository.ConnectionString());
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var _Items = new ItemsM();
                    _Items.Id = Convert.ToInt32(reader["Id"].ToString());
                    _Items.Name = reader["Name"].ToString();
                    _Items.Price = Convert.ToDecimal(reader["Price"].ToString());

                    _ItemsList.Add(_Items);
                }
            }
            reader.Close();

            return _ItemsList;
        }
    }
}
