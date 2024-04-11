using System;

namespace Final_Demo
{
    /// <summary>
    /// Represents the main program and user interface for the parking management system.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the main program.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Create an instance of the HandleParking class to manage parking operations
            HandleParking objHandleParking = new HandleParking();
            int choice;

            // Start an infinite loop for the user interface
            while (true)
            {
                // Display menu options
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Park Vehicle");
                Console.WriteLine("2. Update Vehicle");
                Console.WriteLine("3. Remove Vehicle");
                Console.WriteLine("4. Display Parked Vehicles");
                Console.WriteLine("5. Exit");

                // Prompt user for choice
                Console.Write("Enter your choice (1-5): ");

                // Validate and parse user input
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    // Execute the chosen operation
                    switch (choice)
                    {
                        case 1:
                            // Prompt user for vehicle information and park the vehicle
                            Vehicle parkedVehicle = objHandleParking.GetUserVehicleInput();
                            objHandleParking.AddVehicle(parkedVehicle);

                            // Prompt user for download option
                            Console.Write("Do you want to download the parking ticket? (yes/no): ");
                            string downloadOption = Console.ReadLine().ToLower();

                            // Check for download option
                            if (downloadOption == "yes" || downloadOption == "y")
                            {
                                objHandleParking.DownloadParkingTicket(parkedVehicle);
                            }
                            break;

                        case 2:
                            // Prompt user for vehicle information to update
                            Console.Write("Enter License Plate of Vehicle to Update: ");
                            string plateToUpdate = Console.ReadLine();
                            Console.Write("Enter Updated Car Model: ");
                            string updatedModel = Console.ReadLine();

                            // Update a vehicle
                            objHandleParking.UpdateVehicle(plateToUpdate, new Vehicle { LicensePlate = plateToUpdate, Model = updatedModel });
                            break;

                        case 3:
                            // Prompt user for vehicle information to remove
                            Console.Write("Enter License Plate of Vehicle to Remove: ");
                            string plateToRemove = Console.ReadLine();

                            // Remove a vehicle
                            objHandleParking.RemoveVehicle(plateToRemove);
                            break;

                        case 4:
                            // Display all parked vehicles
                            objHandleParking.DisplayVehicles();
                            break;

                        case 5:
                            // Exit the program
                            Console.WriteLine("Exiting program. Goodbye!");
                            return;

                        default:
                            // Inform the user of an invalid choice
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                            break;
                    }
                }
                else
                {
                    // Inform the user of invalid input
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
        }
    }
}
