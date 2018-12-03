using System;

namespace ProjetoFinal.Models
{
    public class Embalagem
    {
        public int ID { get; set; }
        public string nome { get; set; }
        public decimal tamanho { get; set; }
        public Produto produto  { get; set; }

        public Fornecedor fornecedor  { get; set; }
  
    }
}