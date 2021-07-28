using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffModelsLibrary
{   
    public class InMemory : IStorage
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
            int i=0;
            foreach (Staff staff in staffList)
            {
                if(updatedStaff.EmpId==staff.EmpId)
                {
                    break;
                }
                i++;
            }
            staffList[i] = updatedStaff;
            Console.WriteLine($"given index is {i}");
            // Console.WriteLine("----SUCCESSFULLY UPADATED----");
        }
        public List<Staff> GetAllStaffs()
        {
            return staffList;
        }
    }
}
