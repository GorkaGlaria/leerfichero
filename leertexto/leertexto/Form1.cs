using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace leertexto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Text files|*.txt" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader read = new StreamReader(ofd.FileName))
                    {
                        string[] columnnames = { "Nombre", "Ciudad", "Teléfono" };
                            //read.ReadLine().Split('|');
                        DataTable dt = new DataTable();
                        foreach (string c in columnnames)
                        {
                            dt.Columns.Add(c);
                        }
                        string newline;
                        while ((newline = read.ReadLine()) != null)
                        {
                            DataRow dr = dt.NewRow();
                            string[] values = newline.Split('|');
                            for (int i = 0; i < values.Length; i++)
                            {
                                dr[i] = values[i];
                            }
                          
                                dt.Rows.Add(dr);
                            
                        }
                        read.Close();
                        listadoagenda.DataSource = dt;
                    }
                }
            }
        }

      

        private void nombre_TextChanged(object sender, EventArgs e)

        {
            if (listadoagenda.Rows.Count > 0)
            {
                (listadoagenda.DataSource as DataTable).DefaultView.RowFilter = string.Format("Nombre LIKE '{0}%'", nombre.Text);
            }
            else
            {
                MessageBox.Show( "Carga un txt");
            }
        }

        private void apellido_TextChanged(object sender, EventArgs e)
        {
            if (listadoagenda.Rows.Count > 0)
            {
                (listadoagenda.DataSource as DataTable).DefaultView.RowFilter = string.Format("Nombre LIKE '%{0}%'", apellido.Text);
            }
            else
            {
                MessageBox.Show( "Carga un txt");
            }
}

        private void Ciudad_TextChanged(object sender, EventArgs e)
        {
            if (listadoagenda.Rows.Count > 0)
            {
                (listadoagenda.DataSource as DataTable).DefaultView.RowFilter = string.Format("Ciudad LIKE '%{0}%'", Ciudad.Text);
            }
            else
            {
                MessageBox.Show("Carga un txt");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RecursiveClearTextBoxes(this.Controls);
        }

        private void RecursiveClearTextBoxes(Control.ControlCollection cc)

        {

            foreach (Control ctrl in cc)

            {

                TextBox tb = ctrl as TextBox;

                if (tb != null)

                    tb.Clear();

                else

                    RecursiveClearTextBoxes(ctrl.Controls);

            }

        }
    }
}
