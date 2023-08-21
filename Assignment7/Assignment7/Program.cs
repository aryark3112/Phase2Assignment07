using System;
using System.Data;
using System.Data.SqlClient;

namespace Assignment7
{
    class Program
    {
        static string connectionString = "server=DESKTOP-F3HQTBE;database=LibraryDB;trusted_connection=true;";

        static void Main(string[] args)
        {
            DataSet libraryDataSet = RetrieveData();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Library Management System");
                Console.WriteLine("1. Display Book Inventory");
                Console.WriteLine("2. Add New Book");
                Console.WriteLine("3. Update Book Quantity");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayBookInventory(libraryDataSet);
                        break;

                    case "2":
                        AddNewBook(libraryDataSet);
                        break;

                    case "3":
                        UpdateBookQuantity(libraryDataSet);
                        break;

                    case "4":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please select again.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine();
                }
            }

            ApplyChangesToDatabase(libraryDataSet);
        }

        static DataSet RetrieveData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Books", connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Books");
                return dataSet;
            }
        }

        static void DisplayBookInventory(DataSet dataSet)
        {
            DataTable booksTable = dataSet.Tables["Books"];
            Console.WriteLine("Book Inventory:");
            foreach (DataRow row in booksTable.Rows)
            {
                Console.WriteLine($"Title: {row["Title"]}, Author: {row["Author"]}, Genre: {row["Genre"]}, Quantity: {row["Quantity"]}");
            }
        }

        static void AddNewBook(DataSet dataSet)
        {
            DataTable booksTable = dataSet.Tables["Books"];
            DataRow newRow = booksTable.NewRow();

            Console.WriteLine("Add a New Book:");
            Console.Write("Enter BookId: ");
            newRow["BookId"] = int.Parse(Console.ReadLine());

            Console.WriteLine("Add a New Book:");
            Console.Write("Enter Book Title: ");
            newRow["Title"] = Console.ReadLine();

            Console.Write("Enter Book Author: ");
            newRow["Author"] = Console.ReadLine();

            Console.Write("Enter Book Genre: ");
            newRow["Genre"] = Console.ReadLine();

            Console.Write("Enter Quantity: ");
            newRow["Quantity"] = int.Parse(Console.ReadLine());

            booksTable.Rows.Add(newRow);
            Console.WriteLine("New Book added successfully.");
            Console.WriteLine();
        }

        static void UpdateBookQuantity(DataSet dataSet)
        {
            DataTable booksTable = dataSet.Tables["Books"];

            Console.WriteLine("Update Book Quantity:");
            Console.Write("Enter Book Title to update quantity: ");
            string bookTitle = Console.ReadLine();

            DataRow[] foundRows = booksTable.Select($"Title = '{bookTitle}'");
            if (foundRows.Length > 0)
            {
                Console.Write("Enter New Quantity: ");
                int newQuantity = int.Parse(Console.ReadLine());

                foundRows[0]["Quantity"] = newQuantity;
                Console.WriteLine("Quantity updated successfully.");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
            Console.WriteLine();
        }

        static void ApplyChangesToDatabase(DataSet dataSet)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Books", connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(dataSet.Tables["Books"]);
            }
            Console.WriteLine("Changes applied to the database.");
        }
    }
}