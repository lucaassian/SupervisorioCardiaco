using SupervisorioCardiaco.Models;
using SupervisorioCardiaco.Utils;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SupervisorioCardiaco.DAL
{
    public class LogMedicaoDAL
    {
        //INSERIR - LOG
        public void Inserir(LogMedicao log)
        {
            //Resetando erro
            Erro.setErro(false);

            //Definindo quais colunaas irão gravar cada parâmetro
            string sql = @"INSERT INTO LogsMedicoes (PacienteId, DataHoraInicio, Duracao, BpmMedio, BpmMinimo, BpmMaximo, StatusFinal) 
                           VALUES (@PacienteId, @DataHoraInicio, @Duracao, @BpmMedio, @BpmMinimo, @BpmMaximo, @StatusFinal)";

            //Solicitando uma conexão nova 
            using (OleDbConnection conn = ConexaoDAL.ObterConexao())

            //Atribuindo valores de log em @
            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@PacienteId", log.PacienteId);
                // O Access prefere a data formatada sem milissegundos no OleDb
                cmd.Parameters.AddWithValue("@DataHoraInicio", log.DataHoraInicio.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@Duracao", log.Duracao);
                cmd.Parameters.AddWithValue("@BpmMedio", log.BpmMedio);
                cmd.Parameters.AddWithValue("@BpmMinimo", log.BpmMinimo);
                cmd.Parameters.AddWithValue("@BpmMaximo", log.BpmMaximo);
                cmd.Parameters.AddWithValue("@StatusFinal", log.StatusFinal);

                //Tentando executar
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                //Retornando erro caso falhe
                catch (Exception ex)
                {
                    Erro.setMsg("Erro ao gravar log de medição: " + ex.Message);
                }
            }
        }


        //LISTAR - LOG
        public List<LogMedicao> Listar(int? pacienteId = null, DateTime? dataFiltro = null)
        {
            //Resetando erro
            Erro.setErro(false);

            //Cria um local para receber as fichas dos pacientes que irão vir do banco
            List<LogMedicao> lista = new List<LogMedicao>();

            // INNER JOIN traz o nome do paciente junto com o histórico
            string sql = @"SELECT L.*, P.Nome AS NomePaciente 
                           FROM LogsMedicoes L 
                           INNER JOIN Pacientes P ON L.PacienteId = P.Id 
                           WHERE 1=1 ";

            if (pacienteId.HasValue && pacienteId.Value > 0)
                sql += " AND L.PacienteId = @PacienteId ";

            if (dataFiltro.HasValue)
                sql += " AND DateValue(L.DataHoraInicio) = DateValue(@DataFiltro) ";

            sql += " ORDER BY L.DataHoraInicio DESC";

            //Solicitando uma conexão nova 
            using (OleDbConnection conn = ConexaoDAL.ObterConexao())

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                //Ativando o filtro do paciente
                if (pacienteId.HasValue && pacienteId.Value > 0)
                    cmd.Parameters.AddWithValue("@PacienteId", pacienteId.Value);

                //Ativando o filtro da data
                if (dataFiltro.HasValue)
                    cmd.Parameters.AddWithValue("@DataFiltro", dataFiltro.Value.ToString("yyyy-MM-dd"));

                //Tentando executar
                try
                {
                    conn.Open();

                    //Realizando a leitura
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new LogMedicao
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                PacienteId = Convert.ToInt32(reader["PacienteId"]),
                                NomePaciente = reader["NomePaciente"].ToString(),
                                DataHoraInicio = Convert.ToDateTime(reader["DataHoraInicio"]),
                                Duracao = Convert.ToInt32(reader["Duracao"]),
                                BpmMedio = Convert.ToInt32(reader["BpmMedio"]),
                                BpmMinimo = Convert.ToInt32(reader["BpmMinimo"]),
                                BpmMaximo = Convert.ToInt32(reader["BpmMaximo"]),
                                StatusFinal = reader["StatusFinal"].ToString()
                            });
                        }
                    }
                }

                //Retornando erro caso falhe
                catch (Exception ex)
                {
                    Erro.setMsg("Erro ao carregar histórico: " + ex.Message);
                }
            }

            //Retornando a lista
            return lista;
        }

        //ZERANDO - LOGs
        public void ZerarHistorico()
        {
            //Resetando erro
            Erro.setErro(false);

            //Definindo o que será deletado
            string sql = "DELETE FROM LogsMedicoes";

            //Solicitando uma conexão nova 
            using (OleDbConnection conn = ConexaoDAL.ObterConexao())

            
            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {

                //Tentando executar
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                //Retornando erro caso falhe
                catch (Exception ex)
                {
                    Erro.setMsg("Erro ao zerar histórico: " + ex.Message);
                }
            }
        }

    }
}
