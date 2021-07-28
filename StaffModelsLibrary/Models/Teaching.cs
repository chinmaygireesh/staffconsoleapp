using System;

namespace StaffModelsLibrary

{
    public class Teaching: Staff
    { 
        public Teaching() { }
        public string Subject {get;set;}
        public int NoOfHrs {get;set;}
        public Teaching(string name,int empId,int salary,string subject,int teachingHours):base(name,empId,salary)
        {
            StaffType = TypesOfStaffs.Teaching;
            Subject = subject;
            NoOfHrs = teachingHours;
        }        
       
    }
}