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
    public partial class UsuarioFrm : Form
    {
        public UsuarioFrm()
        {
            InitializeComponent();
        }

        private List<RolUsuario> RolCombo()
        {
            using (dbLoginEntities db = new dbLoginEntities())
            {
                return db.RolUsuario.ToList();
            }
        }

        private void CargarCombo()
        {
            try
            {
                var ListaRol = RolCombo();
                cbRol.DataSource = ListaRol;
                cbRol.DisplayMember = "RolNombre";
                cbRol.ValueMember = "IdRol";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void GuardarUsu(Usuario pUsuario)
        {
            try
            {
                using (dbLoginEntities db = new dbLoginEntities())
                {
                    db.Usuario.Add(pUsuario);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private bool ValidarCampos()
        {
            if (this.txtUsuario.Text == "")
            {
                MessageBox.Show("Debe Ingresar Usuario");
                this.txtUsuario.Focus();
                return false;
            }
            else if (this.txtPasword.Text == "")
            {
                MessageBox.Show("Debe Ingresar Password");
                this.txtPasword.Focus();
                return false;
            }else if ( this.cbRol.SelectedIndex != -1 )
            {
                //this.cbRol.Text = this.cbRol.SelectedValue.ToString();
            }
            else
            {
                MessageBox.Show("Debe Seleccionar Rol");
                this.cbRol.Focus();
                return false;
            }
            return true;
        }
        private void Limpiar()
        {
            this.txtUsuario.Text = "";
            this.txtPasword.Text = "";
            CargarCombo();
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var validaCam = ValidarCampos();
            if (validaCam == true)
            {
                Usuario usuarioEntrada = new Usuario();
                usuarioEntrada.Usuario1 = this.txtUsuario.Text;
                usuarioEntrada.Passord = this.txtPasword.Text;
                usuarioEntrada.RolId = (int)this.cbRol.SelectedValue;
                GuardarUsu(usuarioEntrada);
                MessageBox.Show("Usuario Incluido");
                Limpiar();
            }
        }

        private void UsuarioFrm_Load(object sender, EventArgs e)
        {
            CargarCombo();
        }
    }
}
