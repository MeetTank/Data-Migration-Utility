using DataMigration.DataClass;
using DataMigration.DomainClass;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DataBaseMigrationApp
{
    class Program : Operation
    {
        static async Task Main()
        {
            Console.WriteLine("\t Data Migration Utility App");
        

            ConsoleKeyInfo InputKey;
            do
            {
                var cancellationToken = new CancellationTokenSource();
                var token = cancellationToken.Token;

                int StartNumber, EndNumber;
                Console.WriteLine("\t Enter Start number and End number for migratation,");
                
                int i = 0;
                do
                {
                    if (i != 0)
                    {
                        Console.WriteLine("\t End Number must be greater than Start Number");                        
                    }
                    TakeValidInputs(out StartNumber, out EndNumber);
                    i++;
                }
                while (StartNumber > EndNumber);

                
                Console.WriteLine("\t Enter CANCEL for canceling migration");
                Console.WriteLine("\t Enter STATUS to know migration status");
                

                DataMigrationTable newMigrationData = CreateNewMigration(StartNumber, EndNumber);

                int newId = newMigrationData.ID;

                Task applyMigration = ApplyMigrations(StartNumber, EndNumber, token);

                Task CancelStatusTask = Task.Run(() =>
                {
                    while (true)
                    {
                        if (!applyMigration.IsCompleted)
                        {
                            string str = Console.ReadLine();
                            if (str.Equals("cancel", StringComparison.OrdinalIgnoreCase))
                            {
                                cancellationToken.Cancel();
                                break;
                            }
                            else if (str.Equals("status", StringComparison.OrdinalIgnoreCase))
                            {
                                ShowStatus();
                            }
                        }
                    }
                });

                Task newTask = await Task.WhenAny(applyMigration, CancelStatusTask);


                if (newTask.IsCompleted && token.IsCancellationRequested)
                {
                    CanceledMigrations(newId);
                }
                else
                {
                    CompletedMigrations(StartNumber, EndNumber, newId);
                }

                Console.WriteLine("Press any key to continue or enter X to exit....");
                InputKey = Console.ReadKey();


            } while (InputKey.Key != ConsoleKey.X);
            
            Console.WriteLine("\n\t Application Ended");
            
        }


    }
}
