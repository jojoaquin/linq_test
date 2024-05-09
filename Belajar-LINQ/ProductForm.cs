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
    public partial class ProductForm : Form
    {
        belajar_linqEntities entities = new belajar_linqEntities();
        public ProductForm()
        {
            InitializeComponent();
        }

        void loadAndClearForm()
        {
            txtName.Text = "";
            txtPrice.Text = "";
            txtStock.Text = "";

            var products = from p in entities.products
                           select p;

            dataGridView1.DataSource = products.ToList();

          
        }

        product validateForm()
        {
            if (txtName.Text == "" || txtStock.Text == "" || txtPrice.Text == "")
            {
                throw new Exception("Data can't empty");
            }

            if (!(int.TryParse(txtStock.Text, out int resultStock) && long.TryParse(txtPrice.Text, out long resultPrice)))
            {
                throw new Exception("Price or Stock must be number");
            }

            product product = new product();
            product.name = txtName.Text;
            product.stock = resultStock;
            product.price = resultPrice;
            product.expired_at = dateTimePicker1.Value.Date;
            return product;
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            loadAndClearForm();

            var btnDelete = new DataGridViewButtonColumn();
            btnDelete.UseColumnTextForButtonValue = true;
            btnDelete.Text = "Delete";
            dataGridView1.Columns.Add(btnDelete);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                var result = validateForm();

                entities.products.Add(result);
                entities.SaveChanges();
                loadAndClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var cellValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            if (cellValue != null && cellValue.ToString() == "Delete")
            {
                var selectedRow = dataGridView1.Rows[e.RowIndex];

                var dialog = MessageBox.Show("Are you sure to delete " + selectedRow.Cells["name"].Value + " ?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.OK)
                {
                    int.TryParse(selectedRow.Cells["id"].Value.ToString(), out int intId);
                    var product = entities.products.FirstOrDefault(p => p.id == intId);
                    entities.products.Remove(product);
                    entities.SaveChanges();

                    loadAndClearForm();
                }
            } else if(cellValue != null && cellValue.ToString() == "Update")
            {

                var selectedRow = dataGridView1.Rows[e.RowIndex];


            }

        }
    }
}
