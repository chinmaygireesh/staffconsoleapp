using System;
using System.Collections.Generic;
using StaffModelsLibrary;

namespace staff
{    
    class MenuActinos
    {   
        public static Staff  AddStaff(int empId)
        {
            Staff staff =null; 
            Console.WriteLine("----EMPLOYEE ENROLLING---");
            Console.WriteLine("Select your job type \n1.Teaching staff\n2.Admin. staff\n3.Supporting Staff");
            int userChoice = Convert.ToInt32(Console.ReadLine());
            staff = StaffRegister.Register(userChoice,empId);
            Console.WriteLine("Staff successfully added..");
            return staff;
        }

        public static void  DisplayAstaff(InMemory inMemory)
        {   
            Console.WriteLine("Enter the empId ");
            int empId = Convert.ToInt32(Console.ReadLine());
            try 
            {                
                StaffDisplay.Display(inMemory.GetStaff(empId));
            }
            catch(ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Exception caught: {0}", e);
            }
        }

        public static void DisplayAllStaffs(List<Staff> staffList)
        {
            Console.WriteLine("----EMPLOYEE DETAILS---");
            foreach (Staff staff in staffList)
            {
                StaffDisplay.Display(staff);
            }
        }
    }
}