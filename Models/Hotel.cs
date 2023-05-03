using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Hotel
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public string RegisterDate { get; set; }
        public double Cost { get; set; }
        #endregion
    }
}
