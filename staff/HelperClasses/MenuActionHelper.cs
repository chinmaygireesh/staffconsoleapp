using System;
using System.Collections.Generic;
using StaffModelsLibrary;
namespace staff
{
    class MenuActinoHelper
    {
        public static int ShowStaffList(List<Staff> staffList)
        {
            int selectedUser;
            int i = 1;
            Console.WriteLine("Selecte the Staff");
            foreach (Staff staff in staffList)  
            {  
                Console.WriteLine($"{i}. Employe ID = {staff.EmpId}, name = {staff.Name} ");
                i++;
            }
            selectedUser = Convert.ToInt32(Console.ReadLine());
            return selectedUser-1;
        }           
    }
}    
