using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    public class Membership : BaseEntity
    {
        public Guid MembershipId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalPoint { get; set; }
    }
}