using System;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StaffModelsLibrary
{
    
    public enum TypesOfStaffs
        {
            Teaching=1,
            Administrative,
            Supporting
        }

    [XmlInclude(typeof(Teaching))]
    [XmlInclude(typeof(Supporting))]
    [XmlInclude(typeof(Administrative))]
   
    public class Staff
    {
        public Staff() { }
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