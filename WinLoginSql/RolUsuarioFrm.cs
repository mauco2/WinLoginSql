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
    public partial class RolUsuarioFrm : Form
    {
        public RolUsuarioFrm()
        {
            InitializeComponent();
        }

        private void GuardarRol()
        {
            try
            {
                using (dbLoginEntities db = new dbLoginEntities())
                {
                    RolUsuario rol = new RolUsuario();
                    rol.RolNombre = this.txtUsuario.Text.ToUpper().Trim();
                    db.RolUsuario.Add(rol);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void GuardarPermiso(Permiso pPermiso)
        {
            try
            {
                using(dbLoginEntities db = new dbLoginEntities())
                {
                    db.Permiso.Add(pPermiso);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private int UltimoRegistro()
        {
            using (dbLoginEntities db = new dbLoginEntities())
            {
                var ultimo = (from c in db.RolUsuario
                              orderby c.IdRol descending
                              select c.IdRol).FirstOrDefault();
                return ultimo;
            }
        }

        private void CheckRol()
        {
            int Id = UltimoRegistro();
            Permiso permisoRol = new Permiso();
            foreach (Control chk in panelRol.Controls)
            {
                permisoRol.RolUsuId = Id;
                if(chk is CheckBox)
                {
                    if (((CheckBox)chk).Checked)
                    {
                        permisoRol.OpcionId = Convert.ToInt32(chk.Tag);
                        permisoRol.Permitido = true;
                        GuardarPermiso(permisoRol);
                    }
                    else
                    {
                        permisoRol.OpcionId = Convert.ToInt32(chk.Tag);
                        permisoRol.Permitido = false;
                        GuardarPermiso(permisoRol);
                    }
                }
            }
        }

        private void Limpiar()
        {
            this.txtUsuario.Text = "";
            this.txtUsuario.Focus();

            foreach (Control chk in panelRol.Controls)
            {
                if (chk is CheckBox)
                {
                    if (((CheckBox)chk).Checked)
                    {
                        ((CheckBox)chk).Checked = false;
                    }
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarRol();
            CheckRol();
            Limpiar();
            MessageBox.Show("Rol Usuario Incluido");
        }
    }
}
