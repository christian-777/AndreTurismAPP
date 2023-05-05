using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Ticket
    {
        #region Properties
        public int Id { get; set; }
        public Address Source { get; set; }
        public Address Destiny { get; set; }
        public Customer Customer { get; set; }
        public string TravelDate { get; set; }
        public double Cost { get; set; }
        #endregion

        public override string ToString()
        {
            return "foi inserido";
        }
    }
}
