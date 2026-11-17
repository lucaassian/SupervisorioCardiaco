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
    public class PacienteBLL
    {
        //Criando um novo objeto Paciente
        private PacienteDAL dal = new PacienteDAL();

        //SALVAR - Paciente
        public void Salvar(Paciente p)
        {
            //Resetando erro
            Erro.setErro(false);

            //Exigindo Nome do paciente
            if (string.IsNullOrWhiteSpace(p.Nome))
            {
                Erro.setMsg("O nome do paciente é obrigatório.");
                return;
            }

            //Exigindo Genero do paciente
            if (string.IsNullOrWhiteSpace(p.Genero))
            {
                Erro.setMsg("Selecione o gênero do paciente.");
                return;
            }

            //Exigindo Idade valida do paciente
            if (p.Idade <= 0 || p.Idade > 130)
            {
                Erro.setMsg("Informe uma idade válida.");
                return;
            }

            //Exigindo Altura do paciente
            if (p.Peso <= 0 || p.Altura <= 0)
            {
                Erro.setMsg("Peso e altura devem ser maiores que zero.");
                return;
            }

            //Exigindo Genero do paciente
            if (p.LimiteMin <= 0 || p.LimiteMax <= 0)
            {
                Erro.setMsg("Os limites de batimentos devem ser maiores que zero.");
                return;
            }

            //Exigindo limites adequados
            if (p.LimiteMin >= p.LimiteMax)
            {
                Erro.setMsg("O limite mínimo deve ser menor que o limite máximo.");
                return;
            }

            //Encaminhando para o Salvar
            if (p.Id == 0)
                dal.Inserir(p);

            //Encaminhando para o Editar
            else
                dal.Atualizar(p);
        }

        //EXCLUIR - Paciente
        public void Excluir(int id)
        {
            //Resetando erro
            Erro.setErro(false);

            //Exigindo um id válido
            if (id <= 0)
            {
                Erro.setMsg("Selecione um paciente válido para excluir.");
                return;
            }

            //Excluindo id
            dal.Excluir(id);
        }


        //LISTAR - Paciente
        public List<Paciente> Listar(string filtroNome = "")
        {
            //Encaminha para a dal
            return dal.Listar(filtroNome);
        }


    }
}
