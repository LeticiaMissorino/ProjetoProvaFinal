using System;

namespace ProjetoFinal.Models
{
    public class AnaliseQualidade
    {
        public int ID { get; set; }
        public DateTime data { get; set; }
        public bool aprovado { get; set; }
        public string responsavel  { get; set; }
        public Ordem ordem  { get; set; }
  
    }     
}