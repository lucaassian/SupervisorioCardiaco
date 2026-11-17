using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SupervisorioCardiaco.DAL
{
    public class ConexaoDAL
    {
        private static string caminhoBanco = Application.StartupPath + @"\SupervisorioCardiaco.accdb";
        private static string stringConexao = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={caminhoBanco};Persist Security Info=False;";

        public static OleDbConnection ObterConexao()
        {
            return new OleDbConnection(stringConexao);
        }
    }
}
