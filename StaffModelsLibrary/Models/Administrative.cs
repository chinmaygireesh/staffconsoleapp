using System;
namespace StaffModelsLibrary
{
    
    public class Administrative: Staff
    {         
        public string AdminNo {get;set;}
        public string AdmDprt {get;set;}

        public Administrative(string name,int empId,int salary,string admNo,string admDprt):base(name,empId,salary)
        {
            StaffType = TypesOfStaffs.Administrative;
            AdminNo = admNo;
            AdmDprt = admDprt;
        }
        public Administrative() { }
    }
}