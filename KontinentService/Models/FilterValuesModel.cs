using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KontinentService.Models
{
    public class FilterValuesModel
    {
        public int Id { get; set; }

        public int? TourId { get; set; }
        public ToursModel Tour { get; set; }

        public int FilterAllowableId { get; set; }
        public virtual FilterAllowablesModel FilterAllowable { get; set; }
    }
}
