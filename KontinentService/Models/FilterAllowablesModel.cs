using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KontinentService.Models
{
    public class FilterAllowablesModel
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required]
        public string Title { get; set; }

        public string Image { get; set; }

        public int FilterId { get; set; }

        [Display(Name = "Фильтр")]
        public virtual FiltersModel Filter { get; set; }
    }
}
