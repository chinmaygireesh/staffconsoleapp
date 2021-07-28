using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffModelsLibrary.interfaces
{
    public interface ISerialize
    {
        void  Serialize(string path);
        List<Staff> Deserialize(string filename);
    }
}
