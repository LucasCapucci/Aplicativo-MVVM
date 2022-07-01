using SQLite;
using System;

namespace HealthFitness.Models
{
    public class Exercicio
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public double? Peso { get; set; }
        public string Observacoes { get; set; }

    }
}
