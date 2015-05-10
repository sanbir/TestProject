using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Data.Models
{
    public class Project : EntityBase
    {
        public Project()
        {
            ProjectsEmployees = new HashSet<ProjectsEmployee>();
        }

        [Required(ErrorMessage = "Введите название проекта")]
        [Display(Name = "Название проекта", Prompt = "Название проекта")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Введите название компании-заказчика")]
        [Display(Name = "Название компании-заказчика", Prompt = "Название компании-заказчика")]
        public string CustomerCompanyName { get; set; }

        [Browsable(false)]
        public int ManagerId { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Введите дату начала проекта")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата начала проекта", Prompt = "Дата начала проекта")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Введите дату окончания проекта")]
        [Display(Name = "Дата окончания проекта", Prompt = "Дата окончания проекта")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Введите приоритет проекта")]
        [Range(0, int.MaxValue, ErrorMessage = "Приоритетом проекта должно быть целочисленное значение >= 0")]
        [Display(Name = "Приоритет проекта", Prompt = "Приоритет проекта")]
        public int Priority { get; set; }

        [Display(Name = "Текстовый комментарий", Prompt = "Текстовый комментарий")]
        public string Comment { get; set; }

        [Browsable(false)]
        public virtual Employee Employee { get; set; }

        [Browsable(false)]
        public virtual ICollection<ProjectsEmployee> ProjectsEmployees { get; set; }
    }
}
