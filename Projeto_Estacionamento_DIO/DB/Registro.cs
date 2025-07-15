using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Estacionamento_DIO.DB
{
    public class Registro
    {
        [Key]
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Veiculo { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
    }
    public class Historico
    {
        [Key]
        public int Id { get; set; }
        public string Placa { get; set; }
        public DateTime Entrada { get; set; }
        public DateTime Saida {  get; set; }
        public decimal Valor { get; set; } 
    }
}
