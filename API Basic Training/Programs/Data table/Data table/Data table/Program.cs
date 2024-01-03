using System;
using System.Data;

namespace Data_table
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a DataTable
            DataTable dataTable = new DataTable("EmployeeTable");

            // Add columns to the DataTable
            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Age", typeof(int));
            dataTable.Columns.Add("Salary", typeof(double));

            // Add data rows
            dataTable.Rows.Add(1, "Dimple Mithiya", 30, 50000);
            dataTable.Rows.Add(2, "Shiv Shakti", 25, 60000);
            dataTable.Rows.Add(3, "Ankit Katarmal", 35, 75000);

            // Display Original DataTable
            Console.WriteLine("Original DataTable:");
            DisplayDataTable(dataTable);

            // Modify a value in the DataTable
            dataTable.Rows[1]["Salary"] = 75000;

            // Add a new row
            DataRow newRow = dataTable.NewRow();
            newRow["ID"] = 4;
            newRow["Name"] = "Alice Cullen";
            newRow["Age"] = 28;
            newRow["Salary"] = 70000;
            dataTable.Rows.Add(newRow);

            dataTable.AcceptChanges();

            // Display DataTable after modification
            Console.WriteLine("\nDataTable after modification:");
            DisplayDataTable(dataTable);

            // Clone the DataTable
            DataTable clonedTable = dataTable.Clone();
            Console.WriteLine("\nCloned DataTable:");
            DisplayDataTable(clonedTable);

            // Copy the DataTable
            DataTable copiedTable = dataTable.Copy();
            Console.WriteLine("\nCopied DataTable:");
            DisplayDataTable(copiedTable);

            // Note: Remove the line dataTable.Clear() to avoid clearing the DataTable before using Select

            // Select rows using Select method
            DataRow[] selectedRows = dataTable.Select("Age > 30");
            Console.WriteLine("\nSelected Rows (Age > 30):");
            DisplayDataRowArray(selectedRows);

            Console.ReadKey();
        }

        //method to display DataTable
        static void DisplayDataTable(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine($"{row["ID"]}\t{row["Name"]}\t{row["Age"]}\t{row["Salary"]}");
            }
        }

        //method to display DataRow array
        static void DisplayDataRowArray(DataRow[] rows)
        {
            foreach (DataRow row in rows)
            {
                Console.WriteLine($"{row["ID"]}\t{row["Name"]}\t{row["Age"]}\t{row["Salary"]}");
            }
        }
    }
}
