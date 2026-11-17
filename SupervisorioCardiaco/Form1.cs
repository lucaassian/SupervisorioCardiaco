using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SupervisorioCardiaco
{
    public partial class Form1 : Form
    {   

        //Inicializando Form
        public Form1()
        {
            InitializeComponent();
            customizeDesing();
        }

        //Definindo SubMenu
        private void customizeDesing()
        {
            panelPacientesSubMenu.Visible = false;
        }

        //Escondedo SubMenu
        private void hideSubMenu()
        {
            if (panelPacientesSubMenu.Visible == true)
            {
                panelPacientesSubMenu.Visible = false;
            }
        }

        //Mostrando SubMenu
        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {

        }

        //Pacientes ShowMenu
        private void button1_Click(object sender, EventArgs e)
        {
            showSubMenu(panelPacientesSubMenu);
        }

        //Botão Visualizar Pacientes
        private void button5_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        //Botão Adicionar Pacientes
        private void button4_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }
    }
}
