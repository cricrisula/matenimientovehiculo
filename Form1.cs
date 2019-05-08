using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tienda_LogicaNegocio;
using Tienda_Entidades;

namespace TresCapas
{
    public partial class Form1 : Form
    {
        //
        //
        //Creamos las instancias de la clase Eproducto y ProductoBol
        private EVehiculo _vehiculo;
        private readonly VehiculoBol _vehiculoBol = new VehiculoBol();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //
        //Creamos los métodos generales llenando y leyendo objetos
        //
        private void Guardar()
        {
            try
            {
                _vehiculo = null;

                if (_vehiculo == null)
                {
                    _vehiculo = new EVehiculo();
                    _vehiculo.Codigo = Convert.ToInt32(txtCodigo.Text);
                    _vehiculo.Descripcion = txtDescripcion.Text;
                    _vehiculo.Modelo = txtModelo.Text;
                    _vehiculo.Marca = txtMarca.Text;

                    _vehiculoBol.Registrar(_vehiculo);

                    if (_vehiculoBol.stringBuilder.Length != 0)
                    {
                        MessageBox.Show(_vehiculoBol.stringBuilder.ToString(), "Para continuar:");
                    }
                    else
                    {
                        MessageBox.Show("Vehiculo registrado/actualizado con éxito");

                        TraerTodos();
                        
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
            this.Controls.OfType<TextBox>().ToList().ForEach(o => o.Text = "");
            return;

        }

        private void TraerTodos()
        {
            List<EVehiculo> vehiculos = _vehiculoBol.Todos();

            if (vehiculos.Count > 0)
            {
                dgvDatos.AutoGenerateColumns = false;
                dgvDatos.DataSource = vehiculos;
                dgvDatos.Columns["columnCodigo"].DataPropertyName = "Codigo";
                dgvDatos.Columns["columnDescripcion"].DataPropertyName = "Descripcion";
                dgvDatos.Columns["columnModelo"].DataPropertyName = "Modelo";
                dgvDatos.Columns["columnMarca"].DataPropertyName = "Marcar";
            }
            else
                MessageBox.Show("No existen vehiculos Registrado");
        }

        private void TraerPorCodigo(int Codigo)
        {
            try
            {
                _vehiculo = _vehiculoBol.TraerPorId(Codigo);

                if (_vehiculo != null)
                {
                    txtCodigo.Text = Convert.ToString(_vehiculo.Codigo);
                    txtDescripcion.Text = _vehiculo.Descripcion;
                    txtModelo.Text = _vehiculo.Modelo;
                    txtMarca.Text = _vehiculo.Marca;
                }
                else
                    MessageBox.Show("El Vehiculo solicitado no existe");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }

        private void Eliminar(int id)
        {
            try
            {
                _vehiculoBol.Eliminar(id);

                MessageBox.Show("Producto eliminado satisfactoriamente");

                TraerTodos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }

        //
        //
        //Usamos nuestros metodos y funciones generales, observe como no hemos repetido codigo en ningun lado
        //haciendo con esto que nuestras tareas de actualizacion sean mas sencillas para nosotros o para
        //al asignado en realizarlas...
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Guardar();
            
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && !string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                e.SuppressKeyPress = true;

                TraerPorCodigo(Convert.ToInt32(txtCodigo.Text));
            }
        }

        private void txtModelo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                Guardar();
            }
        }

        private void btbnBuscar_Click(object sender, EventArgs e)
        {
           //if (!string.IsNullOrWhiteSpace(txtId.Text))
           // {
           //     TraerPorId(Convert.ToInt32(txtId.Text));
           // }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                Eliminar(Convert.ToInt32(txtCodigo.Text));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TraerTodos();
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCodigo.Text = dgvDatos.CurrentRow.Cells[0].Value.ToString();
            txtDescripcion.Text= dgvDatos.CurrentRow.Cells[1].Value.ToString();
            txtModelo.Text= dgvDatos.CurrentRow.Cells[2].Value.ToString();
            txtMarca.Text= dgvDatos.CurrentRow.Cells[3].Value.ToString();
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            
            Guardar();
            this.Controls.OfType<TextBox>().ToList().ForEach(o => o.Text = "");


        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtModelo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
