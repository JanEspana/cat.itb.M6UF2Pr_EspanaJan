using NHibernate;
using Npgsql;
using NHibernate.Cfg;
using System;
using cat.itb.M6UF2Pr_EspanaJan.models;
using cat.itb.M6UF2Pr_EspanaJan.CRUDs;

namespace cat.itb.M6UF2Pr_EspanaJan
{
    public class Program
    {
        public static void Main()
        {

            GeneralCRUD generalCRUD = new GeneralCRUD();
            int option = 0;
            Console.WriteLine("Choose an option:");
            Console.WriteLine("0. Reset database");
            Console.WriteLine("1. Insert 4 employees");
            Console.WriteLine("2. Update stock of 4 products");
            Console.WriteLine("3. Select orders of supplier 6");
            Console.WriteLine("4. Select suppliers with amount higher than 8000");
            Console.WriteLine("5. Delete employee with surname SMITH");
            Console.WriteLine("6. Insert 2 products and 2 suppliers");
            Console.WriteLine("7. Update credit of suppliers in BURLINGAME");
            Console.WriteLine("8. Select all products");
            Console.WriteLine("9. Select suppliers of employee ARROYO");
            Console.WriteLine("10. Select orders with cost higher than 10000 and amount higher than 100");
            Console.WriteLine("11. Select products with price lower than 30");
            Console.WriteLine("12. Select supplier with lowest amount");
            
            option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 0:
                    generalCRUD.RunScript();
                    break;
                case 1:
                    Ex1();
                    break;
                case 2:
                    Ex2();
                    break;
                case 3:
                    Ex3();
                    break;
                case 4:
                    Ex4();
                    break;
                case 5:
                    Ex5();
                    break;
                case 6:
                    Ex6();
                    break;
                case 7:
                    Ex7();
                    break;
                case 8:
                    Ex8();
                    break;
                case 9:
                    Ex9();
                    break;
                case 10:
                    Ex10();
                    break;
                case 11:
                    Ex11();
                    break;
                case 12:
                    Ex12();
                    break;
                default:
                    Main();
                    break;
            }
        }
        public static void Ex1()
        {
            List<Employee> employees = new List<Employee>();
            EmployeeCRUD employeeCRUD = new EmployeeCRUD();
            Employee emp1 = new Employee();
            emp1.Surname = "SMITH";
            emp1.Job = "DIRECTOR";
            emp1.ManagerNo = 9;
            emp1.StartDate = new DateTime(1988, 12, 12);
            emp1.Salary = 118000;
            emp1.Commission = 52000;
            emp1.DeptNo = 10;

            Employee emp2 = new Employee();
            emp2.Surname = "JOHNSON";
            emp2.Job = "VENEDOR";
            emp2.ManagerNo = 4;
            emp2.StartDate = new DateTime(1992, 02, 25);
            emp2.Salary = 125000;
            emp2.Commission = 30000;
            emp2.DeptNo = 30;

            Employee emp3 = new Employee();
            emp3.Surname = "HAMILTON";
            emp3.Job = "ANALISTA";
            emp3.ManagerNo = 7;
            emp3.StartDate = new DateTime(1989, 03, 18);
            emp3.Salary = 172000;
            emp3.Commission = null;
            emp3.DeptNo = 10;

            Employee emp4 = new Employee();
            emp4.Surname = "JACKSON";
            emp4.Job = "ANALISTA";
            emp4.ManagerNo = 7;
            emp4.StartDate = new DateTime(2001, 10, 25);
            emp4.Salary = 192000;
            emp4.Commission = null;
            emp4.DeptNo = 10;

            employees.Add(emp1);
            employees.Add(emp2);
            employees.Add(emp3);
            employees.Add(emp4);

            employeeCRUD.InsertADO(employees);
        }
        public static void Ex2()
        {
            ProductCRUD productCRUD = new ProductCRUD();

            Product product1 = productCRUD.SelectByCodeADO(100890);
            Product product2 = productCRUD.SelectByCodeADO(200376);
            Product product3 = productCRUD.SelectByCodeADO(200380);
            Product product4 = productCRUD.SelectByCodeADO(100861);

            product1.CurrentStock = 8;
            product2.CurrentStock = 7;
            product3.CurrentStock = 9;
            product4.CurrentStock = 12;

            List<Product> products = new List<Product> { product1, product2, product3, product4 };
            productCRUD.UpdateADO(products);
        }
        public static void Ex3()
        {
            OrderCRUD orderCRUD = new OrderCRUD();
            List<Orderp> orders = orderCRUD.SelectOrdersSupplierADO(6);

            double amount = 0;
            double cost = 0;

            foreach (Orderp order in orders)
            {
                amount += order.Amount;
                cost += order.Cost;
            }
            Console.WriteLine($"Amount: {amount}");
            Console.WriteLine($"Cost: {cost}");
        }
        public static void Ex4()
        {
            SupplierCRUD supplierCRUD = new SupplierCRUD();
            List<Supplier> suppliers = supplierCRUD.SelectHigherThan(8000);
            foreach (Supplier supplier in suppliers)
            {
                Console.WriteLine(supplier);
            }
        }
        public static void Ex5()
        {
            EmployeeCRUD employeeCRUD = new EmployeeCRUD();
            Employee employee = employeeCRUD.SelectByNameADO("SMITH");

            employeeCRUD.DeleteADO(employee);

        }
        public static void Ex6()
        {
            EmployeeCRUD employeeCRUD = new EmployeeCRUD();
            ProductCRUD productCRUD = new ProductCRUD();
            SupplierCRUD supplierCRUD = new SupplierCRUD();

            Product product1 = new Product();
            product1.Code = 100000;
            product1.Description = "Producte1";
            product1.CurrentStock = 10;
            product1.MinStock = 5;
            product1.Price = 10;
            product1.Employee = employeeCRUD.SelectEmployeeById(1);

            Product product2 = new Product();
            product2.Code = 100001;
            product2.Description = "Producte2";
            product2.CurrentStock = 15;
            product2.MinStock = 5;
            product2.Price = 15;
            product1.Employee = employeeCRUD.SelectEmployeeById(2);

            productCRUD.InsertProduct(product1);
            productCRUD.InsertProduct(product2);

            Supplier supplier1 = new Supplier();
            supplier1.Name = "Supplier1";
            supplier1.Address = "Carrer1";
            supplier1.City = "City1";
            supplier1.StCode = "ST";
            supplier1.ZipCode = "08080";
            supplier1.Area = 100;
            supplier1.Phone = "123456789";
            supplier1.Amount = 10000;
            supplier1.Credit = 5000;
            supplier1.Remark = "Remark1";
            supplier1.Product = product1;

            Supplier supplier2 = new Supplier();
            supplier2.Name = "Supplier2";
            supplier2.Address = "Carrer2";
            supplier2.City = "City2";
            supplier2.StCode = "ST";
            supplier2.ZipCode = "08080";
            supplier2.Area = 100;
            supplier2.Phone = "123456789";
            supplier2.Amount = 10000;
            supplier2.Credit = 5000;
            supplier2.Remark = "Remark2";
            supplier2.Product = product2;

            supplierCRUD.InsertSupplier(supplier1);
            supplierCRUD.InsertSupplier(supplier2);
        }
        public static void Ex7()
        {
            ProductCRUD productCRUD = new ProductCRUD();
            SupplierCRUD supplierCRUD = new SupplierCRUD();
            List<Supplier> suppliers = supplierCRUD.SelectByCity("BURLINGAME");
            foreach (Supplier supplier in suppliers)
            {
                supplier.Credit = 10000;
                supplier.Product = productCRUD.SelectProductById(1);
                supplierCRUD.UpdateSupplier(supplier);
            }
        }
        public static void Ex8()
        {
            ProductCRUD productCRUD = new ProductCRUD();
            List<Product> products = productCRUD.SelectAllProducts();
            foreach (Product product in products)
            {
                Console.WriteLine(product);
            }
        }
        public static void Ex9()
        {
            EmployeeCRUD employeeCRUD = new EmployeeCRUD();
            Employee employee = employeeCRUD.SelectByName("ARROYO");

            foreach (Product product in employee.Products)
            {
                Console.WriteLine(product.Supplier.Name);
            }
        }
        public static void Ex10()
        {
            Console.WriteLine("Does not work");
            //OrderCRUD orderpCRUD = new OrderCRUD();
            //var orders = orderpCRUD.SelectByCostHigherThan(1000, 100);
            //foreach (var order in orders)
            //{
            //    Console.WriteLine(order);
            //}
        }

        public static void Ex11()
        {
            ProductCRUD productCRUD = new ProductCRUD();
            List<Product> products = productCRUD.SelectByPriceLowThan(30);
            foreach (Product product in products)
            {
                Console.WriteLine(product.Code);
                Console.WriteLine(product.Description);
            }
        }
        public static void Ex12()
        {
            SupplierCRUD supplierCRUD = new SupplierCRUD();
            Supplier supplier = supplierCRUD.SelectLowestAmount();
            Console.WriteLine(supplier);
        }
    }
}