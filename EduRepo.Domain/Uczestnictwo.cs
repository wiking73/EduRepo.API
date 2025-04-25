using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduRepo.Domain
{
    public enum StatusUczestnika { Zaakceptowano, Odrzucono, Oczekuje}
    public class Uczestnictwo
    {
        [Key]
        public int IdUczestnictwa { get; set; }

        public int IdKursu { get; set; }
      

        public string WlascicielId { get; set; }  // FK
        public string UserName { get; set; }

        public StatusUczestnika Status { get; set; } 
    }
}
