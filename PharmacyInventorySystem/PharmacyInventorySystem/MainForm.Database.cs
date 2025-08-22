using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PharmacyInventorySystem  // Notice: Same namespace as your MainForm
{
    public partial class MainForm  // Same class name - this extends MainForm
    {
        // Connection string - update server name as needed
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=PharmacyDB;Integrated Security=True";
        // HERE IS YOUR AddMedicine() method!
        private void AddMedicine()
        {
            // This method will add medicine to database
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("AddMedicine", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        command.Parameters.AddWithValue("@Name", txtMedicineName.Text.Trim());
                        command.Parameters.AddWithValue("@Category", txtCategory.Text.Trim());
                        command.Parameters.AddWithValue("@Price", decimal.Parse(txtPrice.Text));
                        command.Parameters.AddWithValue("@Quantity", int.Parse(txtQuantity.Text));

                        connection.Open();
                        command.ExecuteNonQuery();

                        MessageBox.Show("Medicine added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInputFields();
                        LoadAllMedicines();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding medicine: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // HERE ARE THE OTHER METHODS your MainForm.cs is calling:
        private void LoadAllMedicines()
        {
            // Load all medicines into DataGridView
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetAllMedicines", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dgvMedicines.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading medicines: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchMedicines()
        {
            // Search medicines by name or category
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SearchMedicine", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SearchTerm", txtSearchTerm.Text.Trim());

                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dgvMedicines.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching medicines: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStock()
        {
            // Update stock for selected medicine
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("UpdateStock", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MedicineID", int.Parse(txtMedicineID.Text));
                        command.Parameters.AddWithValue("@Quantity", int.Parse(txtQuantity.Text));

                        connection.Open();
                        command.ExecuteNonQuery();

                        MessageBox.Show("Stock updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAllMedicines();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating stock: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RecordSale()
        {
            // Record a sale
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("RecordSale", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MedicineID", int.Parse(txtMedicineID.Text));
                        command.Parameters.AddWithValue("@QuantitySold", int.Parse(txtQuantitySold.Text));

                        connection.Open();
                        int result = (int)command.ExecuteScalar();

                        if (result == 1)
                        {
                            MessageBox.Show("Sale recorded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearInputFields();
                            LoadAllMedicines();
                        }
                        else if (result == 0)
                        {
                            MessageBox.Show("Insufficient stock for this sale!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Error recording sale!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error recording sale: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputFields()
        {
            txtMedicineName.Clear();
            txtCategory.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            txtMedicineID.Clear();
            txtQuantitySold.Clear();
            txtSaleAmount.Clear();
            txtSearchTerm.Clear();
        }
    }
}