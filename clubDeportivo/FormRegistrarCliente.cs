using clubDeportivo.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clubDeportivo
{
    public partial class FormRegistrarCliente : Form
    {
        public FormRegistrarCliente()
        {
            InitializeComponent();
        }

        private void FormRegistrarCliente_Load(object sender, EventArgs e)
        {
            // Ocultar panel de datos al inicio
            panelDatosPersona.Visible = false;

            txtCheckDocumento.MaxLength = 9;
            habilitarCampos(false);

            txtCheckDocumento.Focus();
        }

        private void FormRegistrarCliente_KeyDown(object? sender, KeyEventArgs e)
        {

        }

        private void txtApellido_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDocumento_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblDocumento_Click(object sender, EventArgs e)
        {

        }

        private void dtpFNacimiento_Click(object sender, EventArgs e)
        {

        }

        private void lblFNacimiento_Click(object sender, EventArgs e)
        {

        }

        private void txtTelefono_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpFNacimiento_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lblAptoF_Click(object sender, EventArgs e)
        {

        }

        private void lblRegistrarCliente_Click(object sender, EventArgs e)
        {

        }

        private void chkPresentado_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string Documento = txtCheckDocumento.Text.Trim();

            Persona_DAO dAO = new Persona_DAO();
            _ = dAO.BuscarPorDocumento(Documento);


            // Validar que el documento no este vacío
            if (string.IsNullOrWhiteSpace(txtCheckDocumento.Text))
            {
                MessageBox.Show("Debe ingresar un número de documento", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCheckDocumento.Focus();
                return;
            }

            // Validar que se ingresan números
            if (!long.TryParse(txtCheckDocumento.Text, out long documento))
            {
                MessageBox.Show("Ingrese sólo números", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCheckDocumento.Focus();
                return;
            }
            //Buscar en la BD            
            BuscarPorDocumento(documento);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Esta seguro que desea cancelar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                limpiarCampos();
                panelDatosPersona.Visible = false;
            }
        }

        private void BuscarPorDocumento(long documento)
        {
            Persona_DAO dao = new Persona_DAO();
            Datos.Persona personaEncontrada = dao.BuscarPorDocumento(documento.ToString());

            // Mostrar el panel de datos
            panelDatosPersona.Visible = true;

            if (personaEncontrada != null)
            {
                MessageBox.Show("Persona encontrada en el sistema como " + personaEncontrada.Tipo, "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cargar los datos de la persona encontrada
                txtNombre.Text = personaEncontrada!.Nombre;
                txtApellido.Text = personaEncontrada.Apellido;
                txtDocumento.Text = personaEncontrada.Documento;
                txtTelefono.Text = personaEncontrada.Telefono ?? "";
                dtpFNacimiento.Value = personaEncontrada.FNacimiento;
                chkPresentado.Checked = personaEncontrada.Presentado.HasValue ? personaEncontrada.Presentado.Value : false;

                // Deshabilitar los campos para que no se puedan editar los datos existentes.
                habilitarCampos(false);
            }
            else
            {
                //Persona inexistente (se habilita la carga de datos)
                MessageBox.Show("Persona no encontrada. Complete los campos para registrarla.", "Nueva Persona", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar los campos
                limpiarCampos();

                // Habilitar los campos para poder completar los datos
                habilitarCampos(true);
                btnSocio.Visible = true;
                btnNoSocio.Visible = true;

                // Dar foco al primer campo
                txtNombre.Focus();
            }

            // Habilitar los botones Socio, No socio y Cancelar            
            btnCancelar.Enabled = true;
            btnSocio.Enabled = true;
            btnNoSocio.Enabled = true;
        }

        private void limpiarCampos()
        {
            // Limpiar todos los campos
            txtCheckDocumento.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDocumento.Text = "";
            txtTelefono.Text = "";

            // Reestablecer valores
            //dtpFNacimiento = DateTime.Today;
            chkPresentado.Checked = false;
        }

        private void habilitarCampos(bool habilitar)
        {
            txtNombre.Enabled = habilitar;
            txtApellido.Enabled = habilitar;
            txtDocumento.Enabled = habilitar;
            txtTelefono.Enabled = habilitar;
            dtpFNacimiento.Enabled = habilitar;
            chkPresentado.Enabled = habilitar;
        }

        private void lblChekDocumento_Click(object sender, EventArgs e)
        {

        }

        private void txtCheckDocumento_TextChanged(object sender, EventArgs e)
        {

        }

        private void panelDatosPersona_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite letrass, teclas de control y espacios
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Para bloquear el caracter
            }
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Para bloquear el caracter
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite sólo números y teclas de control
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;  //Bloquea el caracter
            }
        }

        private void txtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite sólo números y teclas de control
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;  // Bloquea el caracter
                return;
            }
            //  Pone un límite de 9 caracteres
            if (txtDocumento.Text.Length >= 8 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;  // No permite más caracteres
            }
        }

        private void txtCheckDocumento_Click(object sender, EventArgs e)
        {

        }

        private void txtChekDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite sólo números y teclas de control
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;  // Bloquea el caracter               
            }
        }

        private void btnInscribirSocio_Click(object sender, EventArgs e)
        {
            // Validar que todos los campos estén completos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtDocumento.Text))
            {
                MessageBox.Show("Por favor complete todos los campos obligatorios (Nombre, Apellido, Documento)",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmar la inscripción
            DialogResult resultado = MessageBox.Show(
                "¿Desea inscribir a esta persona como SOCIO?",
                "Confirmar Inscripción",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                // Obtener los valores del formulario
                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                string documento = txtDocumento.Text.Trim();
                string telefono = txtTelefono.Text.Trim();
                DateTime fNacimiento = dtpFNacimiento.Value;
                bool presentado = chkPresentado.Checked;

                // Crear instancia del DAO y guardar
                Persona_DAO personaDAO = new Persona_DAO();
                bool exito = personaDAO.InscribirSocio(nombre, apellido, documento,
                                                        telefono, fNacimiento, presentado);

                if (exito)
                {
                    MessageBox.Show("✓ Socio inscripto exitosamente. Será redirigido al panel de Socios.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Limpiar el formulario
                    limpiarCampos();
                    panelDatosPersona.Visible = false;
                    txtCheckDocumento.Focus();
                }

                FormSocio frm = new FormSocio();
                frm.Show();
            }
        }

        private void btnInscribirNoSocio_Click(object sender, EventArgs e)
        {
            // Validar que todos los campos estén completos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtDocumento.Text))
            {
                MessageBox.Show("Por favor complete todos los campos obligatorios (Nombre, Apellido, Documento)",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmar la inscripción
            DialogResult resultado = MessageBox.Show(
                "¿Desea inscribir a esta persona como NO SOCIO?",
                "Confirmar Inscripción",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                // Obtener los valores del formulario
                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                string documento = txtDocumento.Text.Trim();
                string telefono = txtTelefono.Text.Trim();
                DateTime fNacimiento = dtpFNacimiento.Value;
                bool presentado = chkPresentado.Checked;

                // Crear instancia del DAO y guardar
                Persona_DAO personaDAO = new Persona_DAO();
                bool exito = personaDAO.InscribirNoSocio(nombre, apellido, documento,
                                                          telefono, fNacimiento, presentado);

                if (exito)
                {
                    MessageBox.Show("✓ No Socio inscripto exitosamente. Será redirigido al panel de no Socios",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Limpiar el formulario
                    limpiarCampos();
                    panelDatosPersona.Visible = false;
                    txtCheckDocumento.Focus();
                }
            }
        }
    }
}
