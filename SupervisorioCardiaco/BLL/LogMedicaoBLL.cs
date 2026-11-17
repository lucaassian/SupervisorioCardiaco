using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupervisorioCardiaco.DAL;
using SupervisorioCardiaco.Models;
using SupervisorioCardiaco.Utils;

namespace SupervisorioCardiaco.BLL
{
    public class LogMedicaoBLL
    {
        //Criando um novo objeto LOG
        private LogMedicaoDAL dal = new LogMedicaoDAL();

        //GRAVAR - LOG
        public void GravarLog(LogMedicao log)
        {
            //Resetando erro
            Erro.setErro(false);

            //Exigindo um paciente válido
            if (log.PacienteId <= 0)
            {
                Erro.setMsg("Nenhum paciente selecionado para gravar o log.");
                return;
            }

            //Exigindo uma duração válida
            if (log.Duracao <= 0)
            {
                Erro.setMsg("A medição precisa ter pelo menos 1 segundo de duração.");
                return;
            }

            //Encaminha para a dal
            dal.Inserir(log);
        }

        //LISTAR - LOG
        public List<LogMedicao> Listar(int? pacienteId = null, DateTime? dataFiltro = null)
        {
            //Encaminha para a dal com os filtros
            return dal.Listar(pacienteId, dataFiltro);
        }

        //REINICIAR HISTÓRICO
        public void ZerarHistorico()
        {
            //Encaminha para a dal
            dal.ZerarHistorico();
        }

    }
}
