using Coffee.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace Coffee.Persistancis.Repositories
{
    public class ItemSalesRepository
    {
        MainRepository _MainRepository = new MainRepository();
        public decimal AlreadyExistData()
        {
            string query = "Select Count(*)from AccountSales Where Date Between'" + DateTime.Now.ToShortDateString() + "' And '" + DateTime.Now.ToShortDateString() + "'  ";
            return _MainRepository.ExecuteScalar(query, _MainRepository.ConnectionString());
        }
        public AccountSales GetLastCode()
        {
            AccountSales _AccountSales = null;

            string query = "Select top 1 Serial from AccountSales Where Date Between'" + DateTime.Now.ToShortDateString() + "' And '" + DateTime.Now.ToShortDateString() + "' order by Serial desc";
            var reader = _MainRepository.Reader(query, _MainRepository.ConnectionString());
            if (reader.HasRows)
            {
                reader.Read();
                _AccountSales = new AccountSales();
                _AccountSales.Serial = (reader["Serial"].ToString());
            }
            reader.Close();

            return _AccountSales;
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
        public List<ItemsM> GetAllItemsByCategories(int id)
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

                    _ItemsList.Add(_Items);
                }
            }
            reader.Close();

            return _ItemsList;
        }

        public ItemsM GetPriceByItem(int Id)
        {
            ItemsM _Items = null;

            string query = ("Select *From Items where Id='" + Id + "' ");
            var reader = _MainRepository.Reader(query, _MainRepository.ConnectionString());
            if (reader.HasRows)
            {
                reader.Read();
                _Items = new ItemsM();
                _Items.Price = Convert.ToDecimal(reader["Price"].ToString());
            }
            reader.Close();

            return _Items;
        }
        public int Add(ItemSales _ItemSales)
        {
            string query = "Insert Into ItemSales(Name,Qty,Per_Price,Sub_Price,Serial,Date) Values ('" + _ItemSales.Name + "','" + _ItemSales.Qty + "','" + _ItemSales.Per_Price + "','" + _ItemSales.Sub_Price + "','" + _ItemSales.Serial + "','" + DateTime.Now.ToShortDateString() + "')";
            return _MainRepository.ExecuteNonQuery(query, _MainRepository.ConnectionString());
        }
        public int Update(ItemSales _ItemSales)
        {
            string query = "Update ItemSales SET Qty='" + _ItemSales.Qty + "',Sub_Price='" + _ItemSales.Sub_Price + "' where Id='" + _ItemSales.Id + "' ";
            return _MainRepository.ExecuteNonQuery(query, _MainRepository.ConnectionString());
        }
        public int Delete(int id)
        {
            string query = ("Delete From ItemSales Where Id='" + id + "' ");
            return _MainRepository.ExecuteNonQuery(query, _MainRepository.ConnectionString());
        }
        public List<ItemSales> GetAllSaleItems(string serial)
        {
            var _ItemSalesList = new List<ItemSales>();
            string query = ("Select *from ItemSales where Serial='" + serial + "' And Date='" + DateTime.Now.ToShortDateString() + "' ");
            var reader = _MainRepository.Reader(query, _MainRepository.ConnectionString());
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var _ItemSales = new ItemSales();
                    _ItemSales.Id = Convert.ToInt32(reader["Id"].ToString());
                    _ItemSales.Name = reader["Name"].ToString();
                    _ItemSales.Qty = Convert.ToInt32(reader["Qty"].ToString());
                    _ItemSales.Per_Price = Convert.ToDecimal(reader["Per_Price"].ToString());
                    _ItemSales.Sub_Price = Convert.ToDecimal(reader["Sub_Price"].ToString());

                    _ItemSalesList.Add(_ItemSales);
                }
            }
            reader.Close();

            return _ItemSalesList;
        }
        public List<ItemSales> GetAllReport(string serial)
        {
            var _ItemSalesList = new List<ItemSales>();
            string query = ("Select *from ItemSales where Serial='" + serial + "' And Date='" + DateTime.Now.ToShortDateString() + "' ");
            var reader = _MainRepository.Reader(query, _MainRepository.ConnectionString());
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var _ItemSales = new ItemSales();
                    _ItemSales.Name = reader["Name"].ToString();
                    _ItemSales.Qty = Convert.ToInt32(reader["Qty"].ToString());
                    _ItemSales.Per_Price = Convert.ToDecimal(reader["Per_Price"].ToString());
                    _ItemSales.Sub_Price = Convert.ToDecimal(reader["Sub_Price"].ToString());

                    _ItemSalesList.Add(_ItemSales);
                }
            }
            reader.Close();

            return _ItemSalesList;
        }
        public decimal SumOrdere(string serial)
        {
            string query = "select SUM(Sub_Price) From ItemSales where Serial='" + serial + "' and Date='" + DateTime.Now.ToShortDateString() + "'";
            return _MainRepository.ExecuteScalar(query, _MainRepository.ConnectionString());
        }
        public int AddAccountSale(AccountSales _AccountSales)
        {
            string query = "Insert Into AccountSales(Serial,CashierName,CustomerName,GrandTotal,Discount,PaidAmount,ChangesAmount,Date) Values ('" + _AccountSales.Serial + "','" + _AccountSales.CashierName + "','" + _AccountSales.CustomerName + "','" + _AccountSales.GrandTotal + "','" + _AccountSales.Discount + "','"+ _AccountSales.PaidAmount+ "','"+ _AccountSales.ChangesAmount+ "','" + DateTime.Now.ToShortDateString() + "')";
            return _MainRepository.ExecuteNonQuery(query, _MainRepository.ConnectionString());
        }
        
    }
}
