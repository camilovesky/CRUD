using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Aplicacion.Datos;

namespace Aplicacion.Presentacion
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        base_personaEntities conector;
        public MainWindow()
        {
            InitializeComponent();
            if (conector == null)
            {
                conector = new base_personaEntities();
            }
        }

        private void ActualizarLista()
        {
            dgListaPersona.ItemsSource = null;
            dgListaPersona.ItemsSource = conector.personas.ToList();

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                persona p = new persona();
                p.nombre = txt_InsertarNombre.Text;
                p.rut = int.Parse(txt_InsertarRut.Text);
                p.fecha_nacimiento = (DateTime)DpInsertarFechaDeNacimiento.SelectedDate;

                conector.personas.Add(p);
                conector.SaveChanges();

                MessageBox.Show("Se ha ingresado una persona");
                txt_InsertarNombre.Text = string.Empty;
                txt_InsertarRut.Text = string.Empty;
                ActualizarLista();

            }
            catch (Exception ex)

            {
                MessageBox.Show(ex.Message);


            }
        }

        private void dgListaPersona_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnActualizarBuscarID_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(txtActualizarBuscarID.Text);
                int contador = conector.personas.Count(per => per.id == id);
                if (contador == 0)
                {
                    MessageBox.Show("No existen personas con ese ID");
                    tabInsertar.Focus();
                }
                else
                {
                    persona p = conector.personas.First(persona => persona.id == id);
                    txt_ActualizarNombre.IsEnabled = true;
                    txt_ActualizarRut.IsEnabled = true;
                    Dp_ActualizarFechaDeNacimiento.IsEnabled = true;
                    btnactualizar.IsEnabled = true;

                    txt_ActualizarNombre.Text = p.nombre;
                    txt_ActualizarRut.Text = p.rut.ToString();
                    DpInsertarFechaDeNacimiento.SelectedDate = p.fecha_nacimiento;
                    lblActualizarID.Content = p.id;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                tabInsertar.Focus();

            }
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(lblActualizarID.Content.ToString());
                persona per = conector.personas.First(p => p.id == id);

                per.nombre = txt_ActualizarNombre.Text;
                per.fecha_nacimiento = (DateTime)Dp_ActualizarFechaDeNacimiento.SelectedDate;
                per.rut = int.Parse(txt_ActualizarRut.Text);

                conector.SaveChanges();
                MessageBox.Show("Se ha actualizado correctamente");
                ActualizarLista();
                               
                txt_ActualizarRut.Text = string.Empty;
                txt_InsertarRut.Text = string.Empty;
                Dp_ActualizarFechaDeNacimiento.SelectedDate = DateTime.Now;

                txt_ActualizarNombre.IsEnabled = false;
                txt_ActualizarRut.IsEnabled = false;
                Dp_ActualizarFechaDeNacimiento.IsEnabled = false;
                btnactualizar.IsEnabled = false;


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                tabInsertar.Focus();


            }
        }
    }
}
