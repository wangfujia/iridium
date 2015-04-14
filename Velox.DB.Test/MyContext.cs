﻿using System.ComponentModel.Design;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.Remoting;
using Velox.DB.Sql.MySql;
using Velox.DB.Sql.Sqlite;
using Velox.DB.Sql.SqlServer;

namespace Velox.DB.Test
{
    public class MyContext : Vx.Context
    {
        public IDataSet<Order> Orders { get; set; }
        public IDataSet<Customer> Customers { get; set; }
        public IDataSet<OrderItem> OrderItems { get; set; }
        public IDataSet<SalesPerson> SalesPeople { get; set; }
        public IDataSet<Product> Products;
        public IDataSet<PaymentMethod> PaymentMethods { get; set; }
        public IDataSet<CustomerPaymentMethodLink> CustomerPaymentMethodLinks { get; set; }

        public MyContext(IDataProvider dataProvider) : base(dataProvider)
        {
        }

        public void PurgeAll()
        {
            Customers.Purge();
            OrderItems.Purge();
            Orders.Purge();
            OrderItems.Purge();
            SalesPeople.Purge();
            Products.Purge();
            PaymentMethods.Purge();
            CustomerPaymentMethodLinks.Purge();
        }

        public void CreateAllTables()
        {
            CreateTable<Product>();
            CreateTable<Order>();
            CreateTable<Customer>();
            CreateTable<OrderItem>();
            CreateTable<SalesPerson>();
            CreateTable<PaymentMethod>();
            CreateTable<CustomerPaymentMethodLink>();
        }

        private static MyContext _instance;

        public static MyContext Instance
        {
//            get { return _instance ?? (_instance = new MemoryStorage()); }
            get { return _instance ?? (_instance = new SqlServerStorage()); }
//            get { return _instance ?? (_instance = new MySqlStorage()); }
//            get { return _instance ?? (_instance = new SqliteStorage()); }
        }
    }

    public class MySqlStorage : MyContext
    {
        public MySqlStorage() : base(new MySqlDataProvider("Server=192.168.1.2;Database=velox;UID=velox;PWD=velox")) { }


        
    }

    public class SqlServerStorage : MyContext
    {
        public SqlServerStorage() : base(new SqlServerDataProvider("Server=MINI;Database=velox;UID=velox;PWD=velox")) { }
        //public SqlServerStorage() : base(new SqlServerDataProvider("Server=tcp:jp7vdmbisx.database.windows.net,1433;Database=velox;User ID=velox@jp7vdmbisx;Password=Cessna182;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;")) { }
    }

    public class SqliteStorage : MyContext
    {
        public SqliteStorage() : base(new SqliteDataProvider("velox.sqlite", true)) { }
    }

    public class MemoryStorage : MyContext
    {
        public MemoryStorage() : base(new MemoryDataProvider()) { }
    }
}
