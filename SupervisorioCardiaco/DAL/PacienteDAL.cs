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
    public class PacienteDAL
    {

        //INSERIR - Paciente
        public void Inserir(Paciente p)
        {
            //Resetando erro
            Erro.setErro(false);
            
            //Definindo quais colunaas irão gravar cada parâmetro
            string sql = @"INSERT INTO Pacientes (Nome, Genero, Idade, Peso, Altura, LimiteMin, LimiteMax, Observacoes) 
                           VALUES (@Nome, @Genero, @Idade, @Peso, @Altura, @LimiteMin, @LimiteMax, @Observacoes)";

            //Solicitando uma conexão nova 
            using (OleDbConnection conn = ConexaoDAL.ObterConexao())

            //Atribuindo valores de p em @
            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Nome", p.Nome);
                cmd.Parameters.AddWithValue("@Genero", p.Genero);
                cmd.Parameters.AddWithValue("@Idade", p.Idade);
                cmd.Parameters.AddWithValue("@Peso", p.Peso);
                cmd.Parameters.AddWithValue("@Altura", p.Altura);
                cmd.Parameters.AddWithValue("@LimiteMin", p.LimiteMin);
                cmd.Parameters.AddWithValue("@LimiteMax", p.LimiteMax);
                cmd.Parameters.AddWithValue("@Observacoes", string.IsNullOrEmpty(p.Observacoes) ? (object)DBNull.Value : p.Observacoes);

                //Tentando executar
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                //Retornando erro caso falhe
                catch (Exception ex)
                {
                    Erro.setMsg("Erro ao salvar paciente: " + ex.Message);
                }
            }
        }

        //ATUALIZAR - Paciente
        public void Atualizar(Paciente p)
        {
            //Resetando erro
            Erro.setErro(false);

            //Definindo os parâmetros de um ID especifico
            string sql = @"UPDATE Pacientes SET Nome = @Nome, Genero = @Genero, Idade = @Idade, 
                           Peso = @Peso, Altura = @Altura, LimiteMin = @LimiteMin, 
                           LimiteMax = @LimiteMax, Observacoes = @Observacoes 
                           WHERE Id = @Id";

            //Solicitando uma conexão nova 
            using (OleDbConnection conn = ConexaoDAL.ObterConexao())

            //Atribuindo novos valores de p em @
            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Nome", p.Nome);
                cmd.Parameters.AddWithValue("@Genero", p.Genero);
                cmd.Parameters.AddWithValue("@Idade", p.Idade);
                cmd.Parameters.AddWithValue("@Peso", p.Peso);
                cmd.Parameters.AddWithValue("@Altura", p.Altura);
                cmd.Parameters.AddWithValue("@LimiteMin", p.LimiteMin);
                cmd.Parameters.AddWithValue("@LimiteMax", p.LimiteMax);
                cmd.Parameters.AddWithValue("@Observacoes", string.IsNullOrEmpty(p.Observacoes) ? (object)DBNull.Value : p.Observacoes);
                cmd.Parameters.AddWithValue("@Id", p.Id);

                //Tentando executar
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                //Retornando erro caso falhe
                catch (Exception ex)
                {
                    Erro.setMsg("Erro ao atualizar paciente: " + ex.Message);
                }
            }
        }

        //EXCLUIR - Paciente
        public void Excluir(int id)
        {
            //Resetando erro
            Erro.setErro(false);

            //Deletando paciente no qual id for igual ao parametro
            string sql = "DELETE FROM Pacientes WHERE Id = @Id";

            //Solicitando uma conexão nova 
            using (OleDbConnection conn = ConexaoDAL.ObterConexao())

            //Atribuindo novos valores de p em @
            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);

                //Tentando executar
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                //Retornando erro caso falhe
                catch (Exception ex)
                {
                    Erro.setMsg("Erro ao excluir paciente: " + ex.Message);
                }
            }
        }

        //LISTANDO - Pacientes
        public List<Paciente> Listar(string filtroNome = "")
        {
            //Resetando erro
            Erro.setErro(false);

            //Cria um local para receber as fichas dos pacientes que irão vir do banco
            List<Paciente> lista = new List<Paciente>();

            //Pega todas as colunas e ordena tudo por ordem alfabética
            string sql = "SELECT * FROM Pacientes WHERE Nome LIKE @Nome ORDER BY Nome ASC";

            //Solicitando uma conexão nova 
            using (OleDbConnection conn = ConexaoDAL.ObterConexao())

            // Assimila as pesquisas com textos
            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Nome", "%" + filtroNome + "%");

                //Tenta pegar todos os pacientes
                try
                {
                    conn.Open();

                    //Realiza a busaca e traz um leitor
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {

                        //Loop que seleciona e armazena as informações desejadas
                        while (reader.Read())
                        {

                            //Faz uma nova ficha de paciente preenchendo os camos e guardando dentro da lista
                            lista.Add(new Paciente
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nome = reader["Nome"].ToString(),
                                Genero = reader["Genero"].ToString(),
                                Idade = Convert.ToInt32(reader["Idade"]),
                                Peso = Convert.ToDouble(reader["Peso"]),
                                Altura = Convert.ToDouble(reader["Altura"]),
                                LimiteMin = Convert.ToInt32(reader["LimiteMin"]),
                                LimiteMax = Convert.ToInt32(reader["LimiteMax"]),
                                Observacoes = reader["Observacoes"] != DBNull.Value ? reader["Observacoes"].ToString() : ""
                            });
                        }
                    }
                }

                //Retornando erro caso falhe
                catch (Exception ex)
                {
                    Erro.setMsg("Erro ao buscar pacientes: " + ex.Message);
                }
            }

            //Retornando a lista
            return lista;
        }


    }
}
