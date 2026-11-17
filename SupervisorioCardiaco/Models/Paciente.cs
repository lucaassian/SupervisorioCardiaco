using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupervisorioCardiaco.Models
{
    public class Paciente
    {
        //Id
        public int Id { get; set; }

        //Nome
        public string Nome { get; set; }

        //Genero
        public string Genero { get; set; }

        //Idade
        public int Idade { get; set; }

        //Peso
        public double Peso { get; set; }

        //Altura
        public double Altura { get; set; }

        //Limite Mínimo
        public int LimiteMin { get; set; }

        //Limite Máximo
        public int LimiteMax { get; set; }

        //Observações
        public string Observacoes { get; set; }

        //IMC (Calculado)
        public double IMC
        {
            get
            {
                if (Altura <= 0) return 0;
                return Peso / (Altura * Altura);
            }
        }
    }
}
