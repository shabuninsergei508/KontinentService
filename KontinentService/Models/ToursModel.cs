using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KontinentService.Models
{
    public class ToursModel
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Краткое описание")]
        public string ShortDescription { get; set; }

        [Display(Name = "Изображение")]
        public string Image { get; set; }

        [Display(Name = "Цена")]
        public int Price { get; set; }

        [Display(Name = "Скидка")]
        public int Discount { get; set; }

        [Display(Name = "URL")]
        public string UrlRus { get; set; }

        [Display(Name = "Дополнительное описание")]
        public string SpecialDescription { get; set; } // Дополнительное описание
        [Display(Name = "Количество заказов")]
        public int NumberOfOrders { get; set; } // Количество заказов данного тура
        [Display(Name = "Номер на странице")]
        public int IndexOnPage { get; set; } // Индекс выдачи на странице

        //Поля для автобусников и круизов
        [Display(Name = "Дата отправления")]
        public DateTime DateIn { get; set; }
        [Display(Name = "Дата прибытия")]
        public DateTime DateOut { get; set; }
        [Display(Name = "Маршрут")]
        public string Route { get; set; }

        [Display(Name = "Спецпредложение")]
        public bool IsHot { get; set; } //Тур - спецпредложение на главной
        [Display(Name = "Стандарный тур")]
        public bool IsTour { get; set; }
        [Display(Name = "Автобусный тур")]
        public bool IsBustour { get; set; }
        [Display(Name = "Круиз")]
        public bool IsCruise { get; set; }

        
        public int SubcategoryId { get; set; }
        [Display(Name = "Направление")]
        public virtual SubcategoriesModel Subcategory { get; set; }
    }
}
