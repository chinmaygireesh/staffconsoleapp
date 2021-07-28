using System;

namespace StaffModelsLibrary
{
    public class Supporting: Staff
    {
        public string Superior {get;set;}
        public string Field {get;set;}


        public Supporting(string name,int empId,int salary,string superior,string supportingField):base(name,empId,salary)
        {
            StaffType = TypesOfStaffs.Supporting;
            Superior = superior;
            Field = supportingField;
        }
        public Supporting() { }
    }
}