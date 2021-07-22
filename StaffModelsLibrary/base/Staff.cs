using System;

namespace StaffModelsLibrary
{
    public enum TypesOfStaffs
        {
            Teaching=1,
            Administrative,
            Supporting
        }
    public class Staff
    {   
        public Staff(string name,int empId,int salary)
        {
            Name = name;
            EmpId = empId;
            Salary = salary;   
        }   

        public string Name {get;set;}
        public int EmpId {get;set;}
        public int Salary {get;set;}
        public TypesOfStaffs StaffType{get;set;}
    }
}    