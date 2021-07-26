using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaffModelsLibrary;



namespace staff
{
    
    
    class InMemory : Istaffstorage
    {
        public List<Staff> staffList = new List<Staff>();

        public void Add(Staff staff)
        {
            staffList.Add(staff);
            Console.WriteLine("Added");
        }

        public void Delete(int empId)
        {
            staffList.Remove(GetStaff(empId));          
        }

        public Staff GetStaff(int empId)
        {
            Staff selectedStaff = null;
            var myLinqQuery = from staff in staffList
                              where staff.EmpId == empId
                              select staff;
            foreach (Staff staff in myLinqQuery)
            {
                return staff;
            }
            return selectedStaff;
        }

        public void Upadate(Staff updatedStaff)
        {
            staffList.Add(updatedStaff);
            Console.WriteLine("----SUCCESSFULLY UPADATED----");
        }

        public List<Staff> GetAllStaffs()
        {
            return staffList;
        }
    }
}
