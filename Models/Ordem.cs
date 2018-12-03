using System;

namespace ProjetoFinal.Models
{
    public class Ordem 
    {
        public int ID { get; set; }
        public Cliente cliente { get; set; }
        public DateTime data { get; set; }
        public TimeSpan hora  { get; set; }
        public Produto produto  { get; set; }
        public int qdte { get; set; }
    }
}