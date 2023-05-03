using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Package
    {
        #region Properties
        public int Id { get; set; }
        public Hotel Hotel { get; set; }
        public Ticket Ticket { get; set; }
        public string RegisterDate { get; set; }
        public double Cost { get; set; }
        public Customer Customer { get; set; }
        #endregion
    }
}
