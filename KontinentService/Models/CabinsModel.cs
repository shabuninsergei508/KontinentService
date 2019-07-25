using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KontinentService.Models
{
    public class CabinsModel
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Изображение")]
        public string Image { get; set; }

        [Display(Name = "Стоимость")]
        public int Price { get; set; }

        [Display(Name = "Название судна")]
        public string ShipName { get; set; }
    }
}
