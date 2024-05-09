using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Belajar_LINQ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void columnButton()
        {
            var btnEdit = new DataGridViewButtonColumn();
            btnEdit.UseColumnTextForButtonValue = true;
            // btnEdit.HeaderText = "Edit";
            btnEdit.Text = "Edit";
            dataGridView1.Columns.Add(btnEdit);

            var btnDelete = new DataGridViewButtonColumn();
            btnDelete.UseColumnTextForButtonValue = true;
            // btnDelete.HeaderText = "Delete";
            btnDelete.Text = "Delete";
            dataGridView1.Columns.Add(btnDelete);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                var entities = new belajar_linqEntities();
          
                var user = from u in entities.users
                           select new
                           {
                          
                               FirstName = u.firstName,
                               LastName = u.lastName,
                               Email = u.email
                           };

                dataGridView1.DataSource = user.ToList();
            } catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }

            columnButton();


        }

        private void btn_goToProduct_Click(object sender, EventArgs e)
        {
            new ProductForm().Show();
            this.Hide();
        }
    }
}
