using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;


namespace MedimasIQ
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }

        Consulta oConsulta = new Consulta();
        String RegistroControl;
        String RegistroCabecera;
        String[] RegistroDetalle = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            txtNIT.Text = "8909814942";
            txtConsecutivo.Text = "00001";

            string Factura = txtFactura.Text;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtRuta.Text = folderBrowserDialog1.SelectedPath;
            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            

            sfd.FileName = txtNIT.Text + txtConsecutivo.Text + ".txt";
            sfd.DefaultExt = "txt";
            sfd.Filter = "txt Files (*.txt)|*.txt";


            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName))
                {

                    RegistroControl = oConsulta.RegistroControl("121181");
                    RegistroCabecera = oConsulta.RegistroCabecera("121181");
                    RegistroDetalle = oConsulta.RegistroDetalle("121181");

                    sw.WriteLine(RegistroControl);
                    sw.WriteLine(RegistroCabecera);



                   /*sw.WriteLine(
                       RegistroDetalle[0].ToString() +
                       RegistroDetalle[1].ToString()
                            );
                    */
                    foreach (String Registro in RegistroDetalle)
                    {
                        sw.WriteLine(
                            RegistroDetalle[0].ToString() +
                            RegistroDetalle[1].ToString() +
                            RegistroDetalle[2].ToString()
                            );
                    }
                    

                }
           
            }

        }
    }
}
