using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProjetoPI
{
    public partial class frmPesquisaFunc : Form
    {
        public frmPesquisaFunc()
        {
            InitializeComponent();
            txtDescricao.Enabled = false;
            rdbCodigo.Checked = false;
            rdbNome.Checked = false;
        }
        private void rdbCodigo_CheckedChanged(object sender, EventArgs e)
        {
            txtDescricao.Enabled = true;
            txtDescricao.Focus();
        }
        private void rdbNome_CheckedChanged(object sender, EventArgs e)
        {
            txtDescricao.Enabled = true;
            txtDescricao.Focus();
        }
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (rdbCodigo.Checked)
            {
                ltbItensPesquisados.Items.Clear();
                //ltbItensPesquisados.Items.Add(txtDescricao.Text);
                pesquisaCodigo(txtDescricao.Text);

            }
            else if (rdbNome.Checked)
            {
                //ltbItensPesquisados.Items.Clear();
                //ltbItensPesquisados.Items.Add(txtDescricao.Text);
                pesquisaNome(txtDescricao.Text);
            }
        }
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtDescricao.Enabled = false;
            rdbNome.Checked = false;
            rdbCodigo.Checked = false;
            txtDescricao.Clear();
            ltbItensPesquisados.Items.Clear();
        }
        public void pesquisaCodigo(string codigo)
        {
            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = "select * from tbUsuario where codUsu = " + codigo + ";";
            comm.CommandType = CommandType.Text;
            comm.Connection = Conexao.obterConexao();

            MySqlDataReader DR;
            DR = comm.ExecuteReader();
            DR.Read();

            ltbItensPesquisados.Items.Clear();

            ltbItensPesquisados.Items.Add(DR.GetString(1));

            Conexao.fecharConexao();
        }
        public void pesquisaNome(string nome)
        {
            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = "select * from tbUsuario where nome like '%" + nome + "%';";
            comm.CommandType = CommandType.Text;
            comm.Connection = Conexao.obterConexao();

            MySqlDataReader DR;

            DR = comm.ExecuteReader();

            ltbItensPesquisados.Items.Clear();

            while (DR.Read())
            {

                ltbItensPesquisados.Items.Add(DR.GetString(1));
            }
            Conexao.fecharConexao();
        }

        private void ltbItensPesquisados_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valor = ltbItensPesquisados.SelectedItem.ToString();
            //MessageBox.Show("Resultado: " + valor);
            frmfuncionarios abrir = new frmfuncionarios (valor);
            abrir.Show();
            this.Hide();
        }
    }
}
