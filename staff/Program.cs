using System.Configuration;
using System.Collections.Specialized;
using System;
using System.Collections.Generic;
using StaffModelsLibrary;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;


namespace staff
{    
    class Program
    {    
        public static int empId;
        static void Main(string[] args)
        {

            var builder = new ConfigurationBuilder()
              .AddJsonFile($"appsettings.json", true, true);

            var config = builder.Build();
            var stoageConfig = config["StorageType"];
            Console.WriteLine($"Storage type is: {stoageConfig}");
            Console.WriteLine();

            Assembly executing = Assembly.GetExecutingAssembly();
            Type storageType = executing.GetType(stoageConfig);
            Istaffstorage storageObject = (Istaffstorage)Activator.CreateInstance(storageType);
            
            Staff staff = null;
            string continueOption;
            int userChoice;
            do
            {
                Console.WriteLine("\nSelect your Action \n1.Add a staff\n2.Display a staff\n3.Display all staffs\n4.Update a staff\n5.Delete a staff");
                userChoice = Convert.ToInt32(Console.ReadLine());
                object[] param1 = new object[1];
                switch (userChoice) 
                    {
                    case 1:
                        empId++;  
                        staff = MenuActinos.AddStaff(empId);
                        storageObject.Add(staff);
                        break;
                    case 2:
                        Console.WriteLine("Enter the empId ");
                        empId = Convert.ToInt32(Console.ReadLine());
                        param1[0] = empId;
                        staff = storageObject.GetStaff(empId);
                        StaffDisplay.Display(staff);
                        break;
                    case 3:
                        List<Staff> staffList = storageObject.GetAllStaffs();
                        int count = (int)staffList.Count;
                        Console.WriteLine($"list count {count}");
                        MenuActinos.DisplayAllStaffs(staffList);    
                        break;
                    case 4:
                        Console.WriteLine("Enter the empId ");
                        empId = Convert.ToInt32(Console.ReadLine());
                        staff = storageObject.GetStaff(empId);
                        storageObject.Delete(empId);
                        Staff updatedStaff = StaffUpdate.Update(staff);
                        storageObject.Upadate(updatedStaff);
                
                        break;  
                    case 5:
                        Console.WriteLine("Enter the empId ");
                        empId = Convert.ToInt32(Console.ReadLine());
                        param1[0] = empId;
                        storageObject.Delete(empId);
                        break;      
                    default:
                        Console.WriteLine("SELECT A VALID OPTION");
                        break;
                }
                Console.WriteLine("Do you want to continue in main menu?(y/n)");
                continueOption = Console.ReadLine();                            
            }while (continueOption == "y");
        }
    }     
}
