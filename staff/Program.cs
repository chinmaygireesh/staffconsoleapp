using System;
using System.Collections.Generic;
using StaffModelsLibrary;

namespace staff
{    
    class Program
    {    
        public static int empId;
        static void Main(string[] args)
        {   
            Staff staff = null;
            List<Staff> staffList = new List<Staff>();
            string continueOption;
            int userChoice;   
            do
            {
                Console.WriteLine("\nSelect your Action \n1.Add a staff\n2.Display a staff\n3.Display all staffs\n4.Update a staff\n5.Delete a staff");
                userChoice = Convert.ToInt32(Console.ReadLine());
                switch(userChoice) 
                    {
                    case 1:    
                        empId++;  
                        staff = MenuActinos.AddStaff(empId);
                        staffList.Add(staff);
                        break;
                    case 2:
                        MenuActinos.DisplayAstaff(staffList);
                        break;
                    case 3:
                        MenuActinos.DisplayAllStaffs(staffList);    
                        break;
                    case 4:
                        MenuActinos.UpdateAStaff(staffList);
                        break;  
                    case 5:
                        MenuActinos.DeleteAStaff(staffList);
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
