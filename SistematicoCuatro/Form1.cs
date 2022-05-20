using SistematicoCuatro.AppCore.IServices;
using SistematicoCuatro.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistematicoCuatro
{
    public partial class Form1 : Form
    {
        /*
            El promedio se encuentra en el datagridview y donde ocupo el metodo se encuentra
            en FillDGV();
        */
        IStudentServices studentServices;
        private int selection = -1;
        public Form1(IStudentServices studentservi)
        {
            this.studentServices = studentservi;
            InitializeComponent();
        }

        private void txtMat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.Handled = true;

                MessageBox.Show("NO SE PUEDEN LETRAS");

            }


        }

        private void txtNombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;

                MessageBox.Show("NO SE PUEDEN LETRAS");

            }


        }
        private bool validate()
        {

            if (string.IsNullOrEmpty(txtApellidos.Text) && string.IsNullOrEmpty(txtCarnet.Text) && string.IsNullOrEmpty(txtCelular.Text)
                && string.IsNullOrEmpty(txtCelular.Text) && string.IsNullOrEmpty(txtCont.Text) && string.IsNullOrEmpty(txtCorreo.Text) &&
                string.IsNullOrEmpty(txtEsta.Text) && string.IsNullOrEmpty(txtMat.Text) && string.IsNullOrEmpty(txtNombres.Text) &&
                string.IsNullOrEmpty(txtProg.Text) && string.IsNullOrEmpty(rctDireccion.Text))
            {
                MessageBox.Show("Tienes que rellenar todos los formularios.");
                return false;
            }

            if (this.studentServices.verifyCarnet(txtCarnet.Text))
            {
                MessageBox.Show($"Carnet {txtCarnet.Text} ya se encuentra");
                return false;
            }
            if (this.studentServices.verifyEmail(txtCorreo.Text))
            {
                MessageBox.Show($"correo {txtCorreo.Text} ya se encuentra");
                return false;
            }
            if (double.Parse(txtMat.Text) > 100
            || double.Parse(txtCont.Text) > 100
            || double.Parse(txtEsta.Text) > 100
            || double.Parse(txtProg.Text) > 100)
            {
                MessageBox.Show("Nota del 1 al 100");
                return false;
            }
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!validate())
            {
                return;
            }
            Estudiante estudiante = new Estudiante
            {
                Apellidos = txtApellidos.Text,
                Carnet = txtCarnet.Text,
                Contabilidad = (int)Math.Round(double.Parse(txtCont.Text)),
                Correo = txtCorreo.Text,
                Direccion = rctDireccion.Text,
                Estadistica = (int)Math.Round(double.Parse(txtEsta.Text)),
                Matematica = (int)Math.Round(double.Parse(txtMat.Text)),
                Nombres = txtNombres.Text,
                Phone = txtCelular.Text,
                Programacion = (int)Math.Round(double.Parse(txtProg.Text)),
            };
            studentServices.Create(estudiante);
            FillDGV();
            this.btnUpdate.Visible = false;
            CleanForm();
        }
        private void CleanForm()
        {
            rctDireccion.Clear();
            txtApellidos.Clear();
            txtCarnet.Clear();
            txtCelular.Clear();
            txtCont.Clear();
            txtCorreo.Clear();
            txtEsta.Clear();
            txtMat.Clear();
            txtNombres.Clear();
            txtProg.Clear();
        }
        private void FillDGV()
        {
            dataGridView1.Rows.Clear();
            foreach (Estudiante estudiante in this.studentServices.GetAll())
            {
                /*
                 *  Aqui ocupo el metodo CalculateAverage y lo paso a una celda del datagridview. 
                 */
                dataGridView1.Rows.Add(estudiante.Id, estudiante.Nombres, estudiante.Apellidos, estudiante.Carnet, estudiante.Phone, estudiante.Direccion, estudiante.Correo,
                    this.studentServices.CalculateAverage(estudiante), estudiante.Matematica, estudiante.Contabilidad, estudiante.Programacion, estudiante.Estadistica);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            if (e.RowIndex >= 0)
            {
                int Id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                Estudiante estudiante = studentServices.FindById(Id);
                selection = e.RowIndex;
                FillForm(estudiante);
                btnUpdate.Visible = true;
            }
        }
        private void FillForm(Estudiante estudiante)
        {
            rctDireccion.Text = estudiante.Direccion;
            txtApellidos.Text = estudiante.Apellidos;
            txtCarnet.Text = estudiante.Carnet;
            txtCelular.Text = estudiante.Phone;
            txtCont.Text = estudiante.Contabilidad.ToString();
            txtCorreo.Text = estudiante.Correo;
            txtEsta.Text = estudiante.Estadistica.ToString();
            txtMat.Text = estudiante.Matematica.ToString();
            txtNombres.Text = estudiante.Nombres;
            txtProg.Text = estudiante.Programacion.ToString();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            FillDGV();
            if (this.studentServices.GetAll().Count > 0)
            {
                Search(1);
            }
        }
        private void Search(int opcion)
        {
            if (opcion == 1) {
                lblNombre.Visible = true;
                txtBuscar.Visible = true;
                btnBuscar.Visible = true;
                btnVolver.Visible = true;
            }
            else if (opcion == 2)
            {
                lblNombre.Visible = false;
                txtBuscar.Visible = false;
                btnBuscar.Visible = false;
                btnVolver.Visible = false;
            }
           
        }
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selection >= 0)
            {
                int Id = (int)dataGridView1.Rows[selection].Cells[0].Value;
                Estudiante estudiante = studentServices.FindById(Id);
                this.studentServices.Delete(estudiante);
                selection = -1;
                FillDGV();
                CleanForm();

            }
            this.btnUpdate.Visible = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(txtApellidos.Text) && string.IsNullOrEmpty(txtCarnet.Text) && string.IsNullOrEmpty(txtCelular.Text)
                && string.IsNullOrEmpty(txtCelular.Text) && string.IsNullOrEmpty(txtCont.Text) && string.IsNullOrEmpty(txtCorreo.Text) &&
                string.IsNullOrEmpty(txtEsta.Text) && string.IsNullOrEmpty(txtMat.Text) && string.IsNullOrEmpty(txtNombres.Text) &&
                string.IsNullOrEmpty(txtProg.Text) && string.IsNullOrEmpty(rctDireccion.Text))
            {
                MessageBox.Show("Tienes que rellenar todos los formularios.");
                return;
            }
            if (double.Parse(txtMat.Text) > 100
           || double.Parse(txtCont.Text) > 100
           || double.Parse(txtEsta.Text) > 100
           || double.Parse(txtProg.Text) > 100)
            {
                MessageBox.Show("Nota del 1 al 100");
                return;
            }

            Estudiante estudiante = new Estudiante
            {
                Id = (int)dataGridView1.Rows[selection].Cells[0].Value,
                Apellidos = txtApellidos.Text,
                Carnet = txtCarnet.Text,
                Contabilidad = (int)Math.Round(double.Parse(txtCont.Text)),
                Correo = txtCorreo.Text,
                Direccion = rctDireccion.Text,
                Estadistica = (int)Math.Round(double.Parse(txtEsta.Text)),
                Matematica = (int)Math.Round(double.Parse(txtMat.Text)),
                Nombres = txtNombres.Text,
                Phone = txtCelular.Text,
                Programacion = (int)Math.Round(double.Parse(txtProg.Text)),
            };
            this.studentServices.Update(estudiante);
            btnUpdate.Visible = false;
            FillDGV();
            CleanForm();
        }

        private void txtMat_TextChanged(object sender, EventArgs e)
        {
            if (txtMat.Text.StartsWith("-"))
            {
                MessageBox.Show("No se pueden números negativos");
                txtMat.Clear();

            }
            else if (txtEsta.Text.StartsWith("-"))
            {
                MessageBox.Show("No se pueden números negativos");
                txtEsta.Clear();
            }
            else if (txtCont.Text.StartsWith("-"))
            {
                MessageBox.Show("No se pueden números negativos");
                txtCont.Clear();
            }
            else if (txtProg.Text.StartsWith("-"))
            {
                MessageBox.Show("No se pueden números negativos");
                txtProg.Clear();
            }

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtBuscar.Text))
            {
                MessageBox.Show("Escriba el nombre, por favor");
                return;
            }
           

            dataGridView1.Rows.Clear();
            foreach (Estudiante estudiante in this.studentServices.FindByName(txtBuscar.Text)) 
            {
                
                dataGridView1.Rows.Add(estudiante.Id, estudiante.Nombres, estudiante.Apellidos, estudiante.Carnet, estudiante.Phone, estudiante.Direccion, estudiante.Correo,
                    this.studentServices.CalculateAverage(estudiante), estudiante.Matematica, estudiante.Contabilidad, estudiante.Programacion, estudiante.Estadistica);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            FillDGV();
        }
    }
}
