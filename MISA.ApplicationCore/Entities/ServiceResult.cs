using MISA.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class ServiceResult<T>
    {
        public IEnumerable<T> Data { get; set; }
        public string Messenger { get; set; }
        public MISACode MisaCode { get; set; }
    }
}