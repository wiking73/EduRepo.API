﻿using System.ComponentModel.DataAnnotations;

namespace EduRepo.Domain
{
    public class Odpowiedz
    {
        [Key]
        public int IdOdpowiedzi { get; set; }
        public int IdZadania { get; set; }
        //  public Zadanie Zadanie { get; set; }



        public string WlascicielId { get; set; }  // FK
        public string UserName { get; set; }
        public DateTime DataOddania { get; set; }
        public string KomentarzNauczyciela { get; set; }
        public string NazwaPliku { get; set; }
        public string Ocena { get; set; }
    }
}
