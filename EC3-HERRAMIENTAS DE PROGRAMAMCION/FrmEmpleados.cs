using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EC3_HERRAMIENTAS_DE_PROGRAMAMCION
{
    public partial class FrmEmpleados : Form
    {
        public FrmEmpleados()
        {
            InitializeComponent();
        }
        //variable global de la otra clase
        ACCESO_A_DATOS ac = new ACCESO_A_DATOS();

        #region METODOS DE SALTO Y VISUALIZAR Y INTERACCION CON ACCESO A DATOS

        //METODO PARA SALTO DE CAJAS DE TEXTO
        private void enviar(KeyPressEventArgs tecla, Control foco)
        {
            if (tecla.KeyChar == 13)
            {
                foco.Focus();
                tecla.Handled = true;
            }
        }

        //METODO VISUALIZAR
        private void Visualizar()
        {
            using (BDEmpresaEntities BD = new BDEmpresaEntities())
            {
                var lst = from d in BD.EMPLEADOS select d;
                dgvDatos.DataSource = lst.ToList();
                
            }
        }
        private void Interacion_accesodatos(int opcion)
        {
            ac.Id = txtId.Text;
            ac.Ap = txtApellidoP.Text;
            ac.Am = txtApellidoM.Text;
            ac.Nombre = txtNombres.Text;
            ac.Edad = txtEdad.Text;
            ac.Dgv = dgvDatos;
            ac.Opcion = opcion;
            ac.experiencia();
            Visualizar();
            ac.limpiar(this);

        }

        #endregion

        #region BOTONES

        //METODO SALIR
        private void btnSalir_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("¿Desea Cerrar el Programa ?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        //METODO REGISTRAR
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            Interacion_accesodatos(1);

        }
        private void FrmEmpleados_Load(object sender, EventArgs e)
        {
            ac.limpiar(this);
            Visualizar();
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            

        }

        //METODO PARA SELECCIONAR AL DARLE CLICK AL DATAGRIDVIEW
        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1 && dgvDatos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dgvDatos.CurrentRow.Selected = true;
                txtId.Text = dgvDatos.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString();
                txtApellidoP.Text = dgvDatos.Rows[e.RowIndex].Cells["Paterno"].FormattedValue.ToString();
                txtApellidoM.Text = dgvDatos.Rows[e.RowIndex].Cells["Materno"].FormattedValue.ToString();
                txtNombres.Text = dgvDatos.Rows[e.RowIndex].Cells["Nombres"].FormattedValue.ToString();
                txtEdad.Text = dgvDatos.Rows[e.RowIndex].Cells["Edad"].FormattedValue.ToString();
                btnRegistrar.Enabled = false;
                //btnNuevo.Enabled = false;
                btnEliminar.Enabled = true;
                btnModificar.Enabled = true;
                txtId.Enabled = false;
            }
        }
        //MODIFICAR
        private void btnModificar_Click(object sender, EventArgs e)
        {
            Interacion_accesodatos(2);
            btnRegistrar.Enabled = true;
            btnNuevo.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            txtId.Enabled = true;
        }

        //ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿DESEA ELIMINAR EL REGISTRO  " + txtId.Text + " - " + txtNombres.Text + "?", "INFORMATION", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Interacion_accesodatos(3);
                btnRegistrar.Enabled = true;
                btnNuevo.Enabled = true;
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
                txtId.Enabled = true;
            }

        }
        //NUEVO
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ac.limpiar(this);
            txtId.Enabled = true;
        }
        //CANCELAR ACCIONES
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ac.limpiar(this);
            Visualizar();
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnRegistrar.Enabled = true;
            txtId.Enabled = true;
        }

        #endregion

        #region METODO KEYPRES


        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {  
            ac.validar_numeros(e);
            enviar(e, txtApellidoP);
        }

        private void txtApellidoP_KeyPress(object sender, KeyPressEventArgs e)
        {
            ac.validar_Letras(e);
            enviar(e, txtApellidoM);
        }

        private void txtApellidoM_KeyPress(object sender, KeyPressEventArgs e)
        {
            ac.validar_Letras(e);
            enviar(e, txtNombres);
        }

        private void txtNombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            ac.validar_Letras(e);
            enviar(e, txtEdad);
        }

        private void txtEdad_KeyPress(object sender, KeyPressEventArgs e)
        {
            ac.validar_numeros(e);
        }

        #endregion

        
        
        
    }
}
