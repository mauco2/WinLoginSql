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
    public partial class MenuFrm : Form
    {
        public MenuFrm()
        {
            InitializeComponent();
        }

        public MenuFrm(string pUser, int pIdRol, int pIdUsu)
        {
            InitializeComponent();
            lblUsu.Text = pUser;
            IdRol = pIdRol;
            IdUsu = pIdUsu;
        }

        int IdRol, IdUsu;
        private List<Permiso> SelectOpcion(int pId)
        {
            using (dbLoginEntities db = new dbLoginEntities())
            {
                return db.Permiso.Where(per => per.RolUsuId == pId).ToList();
            }
        }

        private void ConsultarRol(Control pCon)
        {
            var opcion = SelectOpcion(IdRol);

            foreach(Control opci in pCon.Controls)
            {
                if(opci is Button)
                {
                    foreach(Permiso opc in opcion)
                    {
                        if (opc.OpcionId == Convert.ToInt32(opci.Tag))
                        {
                            if (opc.Permitido == false)
                            {
                                opci.Enabled = false;
                            }
                            else
                            {
                                opci.Enabled = true;
                            }
                        }
                    }
                }
            }
        }
        
        Panel p = new Panel();
        private void btnMouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            pMenu.Controls.Add(p);
            p.BackColor = Color.FromArgb(90, 210, 2);
            p.Size = new Size(140, 5);
            p.Location = new
            Point(btn.Location.X, btn.Location.Y + 40);
        }
        
        private void btnMouseLeave(object sender, EventArgs e)
        {
            pMenu.Controls.Remove(p);
        }
        
        private void ocultarSubMenu()
        {
            if (pAdmin.Visible == true)
            {
                pAdmin.Visible = false;
            }
            if (pMantenimiento.Visible == true)
            {
                pMantenimiento.Visible = false;
            }
            if (pClientes.Visible == true)
            {
                pClientes.Visible = false;
            }
        }

        private void mostrarSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                ocultarSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            mostrarSubMenu(pAdmin);
        }

        private void btnMantenimiento_Click(object sender, EventArgs e)
        {
            mostrarSubMenu(pMantenimiento);
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            mostrarSubMenu(pClientes);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MenuFrm_Load(object sender, EventArgs e)
        {
            ConsultarRol(pAdmin);
            ConsultarRol(pMantenimiento);
            ConsultarRol(pClientes);
        }

        private void btnRol_Click(object sender, EventArgs e)
        {
            RolUsuarioFrm rol = new RolUsuarioFrm();
            rol.ShowDialog();
        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {
            UsuarioFrm usu = new UsuarioFrm();
            usu.ShowDialog();
        }
    }
}
