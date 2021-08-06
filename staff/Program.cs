
using System;
using System.Collections.Generic;
using StaffModelsLibrary;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace staff
{
    class Program
    {    
        public static int empId;
      
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", true, true);
            var config = builder.Build();
            var stoageConfig = config["StorageType"];
            var path = config["path"];
        
            Console.WriteLine($"Storage type is: {stoageConfig}");
            Console.WriteLine($"Storage path is: {path}");
          
            Type type = Type.GetType(stoageConfig); 
            IStorage storageObject = (IStorage)Activator.CreateInstance(type);
                           
            Staff staff = null;
            List<Staff> staffList;
            string continueOption;
            int userChoice;
            do
            {
                Console.WriteLine("\nSelect your Action \n1.Add a staff\n2.Display a staff\n3.Display all staffs\n4.Update a staff\n5.Delete a staff");
                userChoice = Convert.ToInt32(Console.ReadLine());
                switch (userChoice) 
                    {
                    case 1:
                        do
                        {
                            if (storageObject is DbStorage)
                            {
                                staff = MenuActinos.AddStaff(0);
                                storageObject.Add(staff);
                            }
                            else
                            {
                                staffList = storageObject.GetAllStaffs();
                                var list = from s in staffList
                                           orderby s.EmpId descending
                                           select s;
                                int empId = list.ElementAt(0).EmpId;
                                Console.WriteLine($"last empid was :{empId}");
                                empId++;
                                staff = MenuActinos.AddStaff(empId);
                                storageObject.Add(staff);
                            }
                            Console.WriteLine("Do you want to Adding staff?(y/n)");
                            continueOption = Console.ReadLine();
                        } while (continueOption == "y");
                        if (storageObject is DbStorage)
                        {
                            DbStorage db = (DbStorage)storageObject;
                            db.Bulkinsert();
                        }           
                        break;
                    case 2:
                        Console.WriteLine("Enter the empId ");
                        empId = Convert.ToInt32(Console.ReadLine());
                        staff = storageObject.GetStaff(empId);
                        StaffDisplay.Display(staff);
                        break;
                    case 3:
                        staffList = storageObject.GetAllStaffs();
                        int count = (int)staffList.Count;
                        Console.WriteLine($"list count {count}");
                        MenuActinos.DisplayAllStaffs(staffList);    
                        break;
                    case 4:
                        Console.WriteLine("Enter the empId ");
                        empId = Convert.ToInt32(Console.ReadLine());
                        staff = storageObject.GetStaff(empId);
                        Staff updatedStaff = StaffUpdate.Update(staff);
                        storageObject.Upadate(updatedStaff);
                        break;  
                    case 5:
                        Console.WriteLine("Enter the empId ");
                        empId = Convert.ToInt32(Console.ReadLine());
                        storageObject.Delete(empId);
                        break;      
                    default:
                        Console.WriteLine("SELECT A VALID OPTION");
                        break;
                }
                Console.WriteLine("Do you want to continue in main menu?(y/n)");
                continueOption = Console.ReadLine();                            
            }while (continueOption == "y");
            if((storageObject is ISerialize))
            {
                ISerialize serializeObj  = (ISerialize)storageObject;
                serializeObj.Serialize(path);
            }
        }
    }     
}
