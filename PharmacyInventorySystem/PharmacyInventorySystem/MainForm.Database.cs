using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PharmacyManagementSystem
{
    public partial class Form1
    {
        // Connection string - update server name as needed
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=PharmacyDB;Integrated Security=True";

        // Database operation methods will go here
        private void LoadAllMedicines()
        {
            // Database logic for loading medicines
        }

        private void AddMedicine()
        {
            // Database logic for adding medicine
        }

        private void SearchMedicines()
        {
            // Database logic for searching medicines
        }

        private void UpdateStock()
        {
            // Database logic for updating stock
        }

        private void RecordSale()
        {
            // Database logic for recording sale
        }

        // Helper methods
        private void ClearInputFields()
        {
            // Clear all textboxes
        }

        private bool ValidateInputs()
        {
            // Validate form inputs
            return true;
        }
    }
}