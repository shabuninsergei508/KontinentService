using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KontinentService.Models
{
    public class ScheduleValuesModel
    {
        public int Id { get; set; }
        public int Day { get; set; }
        public string Text { get; set; }
        public string Place { get; set; }

        public int ScheduleId { get; set; }
        public virtual SchedulesModel Schedule { get; set; }
    }
}
