using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cadastro_Aluno_Modelo;

namespace Cadastro_Aluno_Controle {
    public class ctlAluno {
        public bool Cadastrar(string nome, string rg, string cpf) {
            string query = "INSERT into tblAluno(nome, rg, cpf) VALUES (`" +
                nome + "`, `" + rg + "`, `" + cpf + "` )";
            OleDbDataReader reader = null;
            OleDbConnection conn = null;
            try {
                conn = obterConexao();
                //
                // Criação do objeto comando, que recebe a query que será utilizada na operação e a conexão com o banco
                //
                OleDbCommand cmd = new OleDbCommand(query, conn);
                //
                // Executa comando
                //

                reader = cmd.ExecuteReader();
                //
                // interage com a tabela retornada
                //
                while (reader.Read()) {
                    return true;
                }
                fecharConexao(conn);
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                //
                // Garante que a conexão será fechada mesmo que ocorra algum erro.
                // Não existe problema em fechar uma conexão duas vezes.
                // O problema está em abrir uma conexão duas vezes.
                //
                if (conn != null) {
                    conn.Close();
                }
            }
            return true;

        }
        public OleDbConnection obterConexao() {
            OleDbConnection conn = null;
            //
            // string de conexão que informa dados do banco que irei acessar
            //
            string alunoConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\wmazo\Documents\aluno.mdb";
            //
            // instância da conexão
            //
            conn = new OleDbConnection(alunoConnectionString);
            //
            // verifica se a conexão está fechada, se tiver abre.
            //
            if (conn.State == ConnectionState.Closed) {
                //
                // abre conexão
                //
                conn.Open();
            }
            return conn;
        }
        public void fecharConexao(OleDbConnection conn) {
            if (conn.State == ConnectionState.Open) {
                //
                // fecha conexão
                //
                conn.Close();
            }
        }
    }
}
