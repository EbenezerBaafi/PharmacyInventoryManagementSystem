using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyInventorySystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region Form Events
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadAllMedicines();
        }
        #endregion

        #region Button Click Events
        // event handler for Add Medicine button
        private void btnAddMedicine_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                AddMedicine();
            }
        }

        // event handler for Update Stock button
        private void btnUpdateStock_Click(object sender, EventArgs e)
        {
            if (ValidateUpdateInputs())
            {
                UpdateStock();
            }
        }

        // event handler for Record Sale button
        private void btnRecordSale_Click(object sender, EventArgs e)
        {
            if (ValidateSaleInputs())
            {
                RecordSale();
            }
        }

        // event handler for View All button
        private void btnViewAll_Click(object sender, EventArgs e)
        {
            ClearInputFields();
            LoadAllMedicines();
        }

        // event handler for Search button
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchMedicines();
        }

        // event handler for Clear button
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputFields();
            LoadAllMedicines();
        }
        #endregion

        #region DataGridView Events
        private void dgvMedicines_SelectionChanged(object sender, EventArgs e)
        {
            PopulateFieldsFromSelectedRow();
        }
        #endregion

        #region Helper Methods
        private void PopulateFieldsFromSelectedRow()
        {
            if (dgvMedicines.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvMedicines.SelectedRows[0];
                txtMedicineID.Text = row.Cells["MedicineID"].Value?.ToString();
                txtMedicineName.Text = row.Cells["Name"].Value?.ToString();
                txtCategory.Text = row.Cells["Category"].Value?.ToString();
                txtPrice.Text = row.Cells["Price"].Value?.ToString();
                txtQuantity.Text = row.Cells["Quantity"].Value?.ToString();

                // Calculate potential sale amount
                if (decimal.TryParse(txtPrice.Text, out decimal price) &&
                    int.TryParse(txtQuantitySold.Text, out int qtySold) && qtySold > 0)
                {
                    txtSaleAmount.Text = (price * qtySold).ToString("F2");
                }
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtMedicineName.Text))
            {
                MessageBox.Show("Please enter medicine name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMedicineName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCategory.Text))
            {
                MessageBox.Show("Please enter category.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategory.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPrice.Text) || !decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Please enter a valid positive price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtQuantity.Text) || !int.TryParse(txtQuantity.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Please enter a valid non-negative quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return false;
            }
            return true;
        }

        private bool ValidateUpdateInputs()
        {
            if (string.IsNullOrEmpty(txtMedicineID.Text))
            {
                MessageBox.Show("Please select a medicine to update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtQuantity.Text) || !int.TryParse(txtQuantity.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Please enter a valid non-negative quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return false;
            }
            return true;
        }

        private bool ValidateSaleInputs()
        {
            if (string.IsNullOrEmpty(txtMedicineID.Text))
            {
                MessageBox.Show("Please select a medicine for sale.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtQuantitySold.Text) || !int.TryParse(txtQuantitySold.Text, out int qtySold) || qtySold <= 0)
            {
                MessageBox.Show("Please enter a valid positive quantity to sell.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantitySold.Focus();
                return false;
            }
            return true;
        }
        #endregion

        private void MainForm_Load_1(object sender, EventArgs e)
        {

        }

        private void dgvMedicines_SelectionChanged_1(object sender, EventArgs e)
        {

        }
    }
}