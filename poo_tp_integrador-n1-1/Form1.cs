using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;


namespace poo_tp_integrador_n1_1
{
    public partial class Form1 : Form
    {
        //crea varibla _gestion de tipo Gestion:
        private Gestion _gestion;

        // persona seleccionada:
        Persona PersonaSeleccionada;

        //auto seleccioando:
        Auto AutoSeleccionado;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            try
            { _gestion = new Gestion(); }
            catch (Exception ex)

            { MessageBox.Show(ex.Message); }

            //------------//

            //datagrid inicializo en null:
            dataGridView1.DataSource = null;

            dataGridView2.DataSource = null;

            dataGridView3.DataSource = null;

            //seleciona por fila y no por celda
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //para no seleccionar mas de una fila por vez:
            dataGridView1.MultiSelect = false;

            //inicializo con el load persona seleccionada a null:
            //PersonaSeleccionada = null;

            button2.Enabled = false;
            button5.Enabled = false;
        }


        //creo clase Gestion: encargada de crear lista persona y metodos agregar, modificar y eliminar persona. Y mostrar:
        public class Gestion //--------CLASE GESTION---------// <HACE DE EMPRESA PARA GESTIONAR PERSONAS Y AUTOS>
        {
            //campo privado para guardar lista de personas:
            private List<Persona> lista_Persona;

            //campo privado para guardar lista de autos:
            private List<Auto> lista_Autos;

            //constructor GESTION:
            public Gestion()
            {
                lista_Persona = new List<Persona>();
                lista_Autos = new List<Auto>();
            }

            //--METODOS GESTION--//

            //--ABM PERSONA--//

            //metodo retorna lista de personas
            public List<Persona> RetornaListaPersona() { return lista_Persona; } // (para clase persona)

            //devuelve verdadero no existe y si devuelve falso s/i existe
            public bool PersonaExisteONo(Persona pPersona) //(para clase persona
            {
                return !lista_Persona.Exists(x => x.DNI == pPersona.DNI); 
            }

            //agregar persona:
            public void AgregarPersona(Persona pPersona) //(para clase persona //recibe un parametro (pPersona) de tipo Persona
            {
                try
                {
                    // exist devuelve true si x tal que x.dni es igual a la persona que le estoy pasando por parametro:
                    if(PersonaExisteONo(pPersona))
                    {

                    //agrego persona a lista:
                    lista_Persona.Add(pPersona);

                    
                    }
                    else
                    {
                        throw new Exception("El DNi ingresado ya existe!");  
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            //borrar persona:
            public void BorrarPersona(Persona pPersona)
            {
                try
                {
                    lista_Persona.Remove(pPersona); 
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }

            //--ABM AUTO--//

            //metodo retorna lista de autos
            public List<Auto> RetornaListaAuto() { return lista_Autos; } // (para clase auto)


            //devuelve verdadero no existe y si devuelve falso si existe
            public bool AutoExisteONo(Auto pAuto)
            {
                return !lista_Autos.Exists(x => x.Patente == pAuto.Patente);
            }  

            //agrega auto a lista de autos despues de hacer validacion con AutoExisteONo():
            public void AgregarAuto(Auto pAuto)
            {
                try
                {
                    // exists devuelve true si x tal que x.dni es igual a la persona que le estoy pasando por parametro:
                    if (AutoExisteONo(pAuto))
                    {
                        //agrego auto a lista:
                        lista_Autos.Add(pAuto);
                    }
                    else
                    {
                        throw new Exception("La patente ingresada ya existe!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            //borrar auto:
            public void BorrarAuto(Auto pAuto)
            {
                try
                {
                    lista_Autos.Remove(pAuto);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            //falta destructor?:

        }//-------TERMINA CLASE GESTION---------//

        //clase persona dueña de vehiculo:
        public class Persona //--------CLASE PERSONA---------//
        {
            private List<Auto> lista_Autos;
            public Persona() { lista_Autos = new List<Auto>(); } 
            public Persona(string pN, string pA, int pD) :this() //this hace referencia al constructor de la misma clase (Persona(), arriba)
            {
                Nombre = pN; Apellido = pA; DNI = pD;
            }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public int DNI { get; set; }

            //falta metodo lista_de_autos:
            public List<Auto> ListaDeAutos()
            {
                return lista_Autos;
            }

            //metodo cantidad_de_autos:
            public int cantidadDeAutos()
            {
                //lista_autos.coUnt cuenta la cantidad de items en un lista y retorna un int:
                return lista_Autos.Count;
            }

            //asginar auto a persona:
            public void AsignarAuto(Auto pAuto)
            {
                lista_Autos.Add(pAuto);
            }

            public decimal ValorAutos()
            {
                return lista_Autos.Sum(x => x.Precio);
            }

            //destructor de Persona:
            ~Persona()
            {
                //apunto a null lista_Autos: para liberarlo y que no quede basura en la memoria:
                lista_Autos = null;
                MessageBox.Show($"el DNI de la parsona es: { DNI }");
            }
        }//-------TERMINA CLASE PERSONA---------//

        //clase auto propiedad de persona:
        public class Auto //--------CLASE AUTO---------//
        {

            Persona duenio;
            public Auto() { duenio = null; }
            public Auto(string pP, string pM, string pMo, string pA, decimal pPr) : this()
            {
                Patente = pP; Marca = pM; Modelo = pMo; Año = pA; Precio = pPr;
            }
            public string Patente { get; set; }
            public string Marca { get; set; }
            public string Modelo { get; set; }
            public string Año { get; set; }
            public decimal Precio { get; set; }

            //falta metodo dueño que retorna la persona dueña del auto:
            //metodo que retorna persona llamada dueño:
            public Persona Dueño() { return duenio; }
            public void AsignaDueño(Persona pDueño) { duenio = pDueño;  }

            //destructor Auto:
            ~Auto()
            {
                MessageBox.Show($"La patente del auto es: {this.Patente}");
            }

        }//-------TERMINA CLASE AUTO---------//

        //BOTON ALTA PERSONA:
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _gestion.AgregarPersona(new Persona(Interaction.InputBox("Ingrese Nombre: "),
                                                        Interaction.InputBox("Ingrese Apellido: "),
                                                            int.Parse(Interaction.InputBox("Ingrese DNI: "))));
                Mostrar(dataGridView1, _gestion.RetornaListaPersona());

                button2.Enabled = true;
                button5.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        //funcion generalizada de mostrar que se utiliza varias veces:
        private void Mostrar(DataGridView pDGV, Object pQueMostrar) //void porque no devuelve nada
        {
            //inicializo en null:
            pDGV.DataSource = null;

            //le digo que quiero mostrar (pO enviada por parametro)
            pDGV.DataSource = pQueMostrar;
        }

        //BOTON ALTA AUTO:
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //agrego a la lista LA instancia de Auto:
                _gestion.AgregarAuto(new Auto(Interaction.InputBox("Ingrese Patente: "),
                                                Interaction.InputBox("Ingrese Marca: "),
                                                    Interaction.InputBox("Modelo: "),
                                                        Interaction.InputBox("Ingrese Año: "),
                                                            decimal.Parse(Interaction.InputBox("Ingrese Precio: "))));

                button4.Enabled = true;
                button6.Enabled = true;

                Mostrar(dataGridView2, _gestion.RetornaListaAuto());
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }


        ///boton borrar persona remueve persona seleccionada de la lista persona:
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                
                _gestion.BorrarPersona((Persona)dataGridView1.SelectedRows[0].DataBoundItem);
               
                Mostrar(dataGridView1, _gestion.RetornaListaPersona());

                    //si la lista es igual a cero entonces la visibilidad del boton es false
                if (_gestion.RetornaListaPersona().Count <= 0) { button2.Enabled = false; button5.Enabled = false; }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //boton modificar Persona:
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                //modificar Persona seleccionada:
                PersonaSeleccionada = (Persona)dataGridView1.SelectedRows[0].DataBoundItem;
                if (!(PersonaSeleccionada is null))
                {
                    PersonaSeleccionada.Nombre = Interaction.InputBox("Ingrese nuevo Nombre: ", "", PersonaSeleccionada.Nombre); //el ultimo par es para que aparezca lo escrito (mal) anteriormente
                    PersonaSeleccionada.Apellido = Interaction.InputBox("Ingrese nuevo Apellido: ", "", PersonaSeleccionada.Apellido);

                    Mostrar(dataGridView1, _gestion.RetornaListaPersona());
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
  
        }

        //boton baja Auto:
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Auto p_auto = (Auto)dataGridView2.SelectedRows[0].DataBoundItem;
                Persona p_persona = (Persona)dataGridView1.SelectedRows[0].DataBoundItem;

                Persona p_dueña_auto = p_auto.Dueño();

                if (p_dueña_auto != null) {
                    p_dueña_auto.ListaDeAutos().Remove(p_auto);
                    _gestion.BorrarAuto((Auto)dataGridView2.SelectedRows[0].DataBoundItem);
                    Mostrar(dataGridView2, _gestion.RetornaListaAuto());
                    PersonaSeleccionada = (Persona)dataGridView1.SelectedRows[0].DataBoundItem;
                    Mostrar(dataGridView3, PersonaSeleccionada.ListaDeAutos().ToList<Auto>());
                }   

            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        //boton modificar auto:
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                AutoSeleccionado = (Auto)dataGridView2.SelectedRows[0].DataBoundItem;
                if(AutoSeleccionado != null)
                {
                    AutoSeleccionado.Marca = Interaction.InputBox("Ingrese Marca: ", "Modificacion de datos", AutoSeleccionado.Marca);
                    AutoSeleccionado.Modelo = Interaction.InputBox("Modelo: ", "Modificacion de datos", AutoSeleccionado.Modelo);
                    AutoSeleccionado.Año = Interaction.InputBox("Ingrese Año: ", "Modificacion de datos", AutoSeleccionado.Año);
                    AutoSeleccionado.Precio=decimal.Parse(Interaction.InputBox("Ingrese Precio: ", "Modificacion de datos", AutoSeleccionado.Precio.ToString()));

                    Mostrar(dataGridView2, _gestion.RetornaListaAuto());

                    PersonaSeleccionada = (Persona)dataGridView1.SelectedRows[0].DataBoundItem;
                    Mostrar(dataGridView3, PersonaSeleccionada.ListaDeAutos().ToList<Auto>());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //boton asignar
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                //se verifica si existen filas en datagrid 1 y 2:
                if (dataGridView1.Rows.Count > 0 && dataGridView2.Rows.Count > 0)
                {
                    //se toma persona seleccionada:
                    PersonaSeleccionada = (Persona)dataGridView1.SelectedRows[0].DataBoundItem;
                    //si la persona seleccionada es distinta de null:
                    if (PersonaSeleccionada != null)
                    {
                        AutoSeleccionado = (Auto)dataGridView2.SelectedRows[0].DataBoundItem;
                        //si el auto seleccionado es distinta de null:
                        if (AutoSeleccionado != null)
                        {
                            if (AutoSeleccionado.Dueño() == null)
                            {
                                //bidireccionalidad:
                                PersonaSeleccionada.AsignarAuto(AutoSeleccionado);
                                AutoSeleccionado.AsignaDueño(PersonaSeleccionada);
                                //to list para que nos de una lista distinta, crea una lista distinta para visualizar porque sino tira error de indireccion
                                Mostrar(dataGridView3, PersonaSeleccionada.ListaDeAutos().ToList<Auto>());

                            }
                            else
                            {
                                throw new Exception("El auto ya se encuentra asignado");
                            }
                        }
                        else
                        {
                            throw new Exception("No hay Autos seleccionados");
                        }
                    }
                    else
                    {
                        throw new Exception("No hay Persona seleccionada");
                    }

                }
                else
                {
                    throw new Exception("No hay elementos en las grillas suficientes para efectuar la asginacion");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //evento para refrescar la grilla cada vez que selecciono una nueva persona:
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                PersonaSeleccionada = (Persona)dataGridView1.SelectedRows[0].DataBoundItem;
                Mostrar(dataGridView3, PersonaSeleccionada.ListaDeAutos().ToList<Auto>());
            }
            catch (Exception) { }
        }

        //boton desasginar:
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                //se verifica si existen filas en datagrid 1 y 2:
                if (dataGridView1.Rows.Count > 0 && dataGridView2.Rows.Count > 0)
                {
                    Auto p_auto = (Auto)dataGridView2.SelectedRows[0].DataBoundItem;
                    Persona p_persona = (Persona)dataGridView1.SelectedRows[0].DataBoundItem;

                    Persona p_dueña_auto = p_auto.Dueño();

                    if (p_persona.ListaDeAutos().Exists(x => x.Equals(p_auto)))
                    {
                        p_persona.ListaDeAutos().Remove(p_auto);
                        p_auto.AsignaDueño(null);
                        Mostrar(dataGridView3, p_persona.ListaDeAutos().ToList<Auto>());
                    }
                }
            }

            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        //boton textbox:
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        //evento rowsadded de datagrid 3:
        private void dataGridView3_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                Persona p_persona = (Persona)dataGridView1.SelectedRows[0].DataBoundItem;

                textBox1.Text = p_persona.ValorAutos().ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

