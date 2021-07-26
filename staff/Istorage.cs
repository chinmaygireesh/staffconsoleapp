using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaffModelsLibrary;

namespace staff
{
    interface Istaffstorage
    {
        void Add(Staff staff);
        Staff GetStaff(int empId);

        List<Staff> GetAllStaffs();
        void Delete(int empId);
        void Upadate(Staff staff);
    }
}
