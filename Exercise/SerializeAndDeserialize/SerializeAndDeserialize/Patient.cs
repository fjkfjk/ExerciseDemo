using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializeAndDeserialize
{
    [Serializable]
    public class Patient
    {
        public string name;
        public int id;
        public int age;
        public Report report;
    }
}
