using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupervisorioCardiaco.Models
{
    public class LogMedicao
    {
        //Id
        public int Id { get; set; }

        //Id - Paciente
        public int PacienteId { get; set; }

        //Nome - Paciente
        public string NomePaciente { get; set; }

        //Data
        public DateTime DataHoraInicio { get; set; }

        //Duração em segundos
        public int Duracao { get; set; }

        //BPM Médio
        public int BpmMedio { get; set; }

        //BPM Mínimo
        public int BpmMinimo { get; set; }

        //BPM Méximo
        public int BpmMaximo { get; set; }

        //Status Final
        public string StatusFinal { get; set; }
    }

}
