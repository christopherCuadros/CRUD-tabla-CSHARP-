using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EC3_HERRAMIENTAS_DE_PROGRAMAMCION
{
    public class ACCESO_A_DATOS
    {
        #region ENCAPSULACION

        
        private string id;
        private string ap;
        private string am;
        private string nombre;
        private string edad;
        private DataGridView dgv;
        private int opcion;



        public string Id { get => id; set => id = value; }
        public string Ap { get => ap; set => ap = value; }
        public string Am { get => am; set => am = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Edad { get => edad; set => edad = value; }
        public DataGridView Dgv { get => dgv; set => dgv = value; }
        public int Opcion { get => opcion; set => opcion = value; }

        #endregion

        public void experiencia()
        {
            validar_vacios(Id, Ap, Am, Nombre, Edad, Dgv, Opcion);
        }



        #region VALIDACIONES


        //METODO VALIDAR CAJAS DE TEXTO VACIAS
        private void validar_vacios(string id, string ap, string am, string nombre, string edad, DataGridView dgv,int opcion)
        {
            if (id == ""  && am == "" && nombre =="" && edad == "")
            {
                MessageBox.Show("Campos Incompletos", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                
                if (opcion == 1)
                    validar_id_ingresar(id, ap, am, nombre, edad, dgv);

                if (Opcion == 2)
                    validar_id_modificar(id, ap, am, nombre, edad, dgv);
                if (Opcion == 3)
                    Eliminar(id);
            }
        }

        //METODO PARA VALIDAR NUMEROS
        public void validar_numeros(KeyPressEventArgs tecla)
        {
            if ((tecla.KeyChar >= 32 && tecla.KeyChar<=47)  || (tecla.KeyChar>=58  && tecla.KeyChar <= 255))
            {
                MessageBox.Show("INGRESE SOLO VALORES NUMERICOS", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tecla.Handled = true;    
            }
            
        }
        //METODO PARA VALIDAR TEXTO
        public void validar_Letras(KeyPressEventArgs tecla)
        {
            if ((tecla.KeyChar >= 33 && tecla.KeyChar <= 64) || (tecla.KeyChar >= 91 && tecla.KeyChar <= 94) ||
                (tecla.KeyChar>=123 && tecla.KeyChar <= 255))
            {
                MessageBox.Show("INGRESE SOLO VALORES DE TEXTO", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tecla.Handled = true;
            }
        }

        //VALIDACION DE ID PARA INGRESAR DATOS Y EVITARA REDUNDANCIA
        private void validar_id_ingresar(string id, string ap, string am, string nombre, string edad, DataGridView dgv)
        {
            
            bool estado = false;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                
                 if (dgv.Rows[i].Cells[0].Value.ToString() == id)
                 {
                      estado = false;
                      break;
                 }
                 else
                 {
                      estado = true;
                 }
                   
            }    
            if (estado == true)
            {
                Insertar(id, ap, am, nombre, edad);
            }
            else
            {
                MessageBox.Show("NUMERO ID EXISTENTE");
            }
            
        }

        //VALIDACION DE ID PARA MODIFICAR DATOS
        private void validar_id_modificar(string id, string ap, string am, string nombre, string edad, DataGridView dgv)
        {
            bool estado = false;
            for (int i = 0; i < dgv.RowCount; i++)
            {
                if (dgv.Rows[i].Cells[0].Value.ToString() == id)
                {
                    estado = true;
                    break;
                }


            }
            if (estado == true)
            {
                Actualizar(id, ap, am, nombre, edad);
            }
            
        }
        #endregion


        #region INTERACON CON LA BASE DE DATOS


        //METODO SE INSERCION DE DATOS 
        private void Insertar(string id,string ap , string am, string nombre,string edad)
        {
            using (BDEmpresaEntities BD = new BDEmpresaEntities())
            {
                if (edad.Length < 3)
                {
                    EMPLEADOS emp = new EMPLEADOS();
                    emp.ID = int.Parse(id);
                    emp.Paterno = ap.ToUpper();
                    emp.Materno = am.ToUpper();
                    emp.Nombres = nombre.ToUpper();
                    emp.Edad = int.Parse(edad);
                    BD.EMPLEADOS.Add(emp);
                    BD.SaveChanges();
                    MessageBox.Show("Datos Registrados", "REGISTRO EXITOSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                else
                {
                    MessageBox.Show("No se Admite edades mayores a 3 digitos","ALERTA",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            
            

        }
        //METODO PARA ACTUALIZAR DATOS
        private void Actualizar(string id, string ap, string am, string nombre, string edad)
        {
            using (BDEmpresaEntities BD = new BDEmpresaEntities())
            {
                if (edad.Length < 3)
                {
                    EMPLEADOS emp = new EMPLEADOS();
                    emp.ID = int.Parse(id);
                    emp.Paterno = ap.ToUpper();
                    emp.Materno = am.ToUpper();
                    emp.Nombres = nombre.ToUpper();
                    emp.Edad = int.Parse(edad);
                    BD.Entry(emp).State = System.Data.Entity.EntityState.Modified;
                    BD.SaveChanges();
                    MessageBox.Show("REGISTRO ACTUALIZADO CORRECTAMENTE");
                }
                else
                {
                    MessageBox.Show("Ingrese una edad valida no superior a 3 digitos");
                }
            }   
        }

        //METODO PARA ELIMINAR DATOS MEDIANTE ID
        private void Eliminar(string id)
        {
            using (BDEmpresaEntities BD = new BDEmpresaEntities())
            {
                EMPLEADOS emp = BD.EMPLEADOS.Find(int.Parse(id));
                BD.EMPLEADOS.Remove(emp);
                BD.SaveChanges();
            }
        }
        #endregion

        //METODO PARA LIMPIAR LAS CAJAS DE TEXTO
        public void limpiar(Form f)
        {
            foreach(Control c in f.Controls)
            {
                if(c is TextBox)
                {
                    c.Text = "";
                }
            }
        }
    }
}
