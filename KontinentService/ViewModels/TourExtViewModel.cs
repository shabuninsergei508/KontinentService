using KontinentService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KontinentService.ViewModels
{
    public class ToursExtViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Title { get; set; }

        public bool IsTour { get; set; }
        public bool IsBustour { get; set; }
        public bool IsCruise { get; set; }

        public virtual List<PropertiesModel> Properties { get; set; }

        public virtual List<FilterAllowablesModel> FilterAllowables { get; set; }

        public virtual List<CabinsModel> Cabins { get; set; }

        public virtual List<SchedulesModel> Schedules { get; set; }
    }
}
