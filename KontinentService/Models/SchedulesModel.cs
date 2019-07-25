using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KontinentService.Models
{
    public class SchedulesModel
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int TourId { get; set; }
        public virtual ToursModel Tour { get; set; }
    }
}
