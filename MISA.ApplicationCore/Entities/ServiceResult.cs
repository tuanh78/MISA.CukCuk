using MISA.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class ServiceResult
    {
        public object Data { get; set; }
        public List<string> Messenger { get; set; }
        public MISAServiceCode MisaServiceCode { get; set; }
    }
}