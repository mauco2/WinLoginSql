using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinLoginSql
{
    public partial class LoginFrm : Form
    {
        int IdRol, IdUsu;
        string nombUsu;
        
        public LoginFrm()
        {
            InitializeComponent();
        }
        
        private List<Usuario> BuscarUsuario(string pUser, string pPass)
        {
            try
            {
                using (dbLoginEntities db = new dbLoginEntities())
                {
                    return db.Usuario.Where(usu => usu.Usuario1 == pUser
                            && usu.Passord == pPass).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }

        private bool ValidarCampos()
        {
            var usuario = BuscarUsuario(this.txtUsuario.Text, this.txtPasword.Text);

            foreach (var usu in usuario)
            {
                IdRol = (int)usu.RolId;
                IdUsu = usu.IdUsuario;
                nombUsu = usu.Usuario1;
            }

            if (this.txtUsuario.Text == "")
            {
                MessageBox.Show("Debe Ingresar Usuario");
                this.txtUsuario.Focus();
                return false;
            }else if (this.txtPasword.Text == "")
            {
                MessageBox.Show("Debe Ingresar Password");
                this.txtPasword.Focus();
                return false;
            }
            else if (usuario.Count <=0)
            {
                MessageBox.Show("Usuario No Registrado");
                return false;
            }
            return true;
        }

        private void LblRegistro_Click(object sender, EventArgs e)
        {
            UsuarioFrm usuarioFrm = new UsuarioFrm();
            usuarioFrm.Show();
            txtUsuario.Focus();

        }

        private void btnAcceder_Click(object sender, EventArgs e)
        {
            if (ValidarCampos() == true)
            {
                MenuFrm menu = new MenuFrm(nombUsu, IdRol, IdUsu);
                menu.Show();
                this.Hide();
            }
        }
    }
}
