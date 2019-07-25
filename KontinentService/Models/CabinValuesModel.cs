using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KontinentService.Models
{
    public class CabinValuesModel
    {
        public int Id { get; set; }

        public int TourId { get; set; }
        public virtual ToursModel Tour { get; set; }

        public int CabinId { get; set; }
        public virtual CabinsModel Cabin { get; set; }
    }
}
