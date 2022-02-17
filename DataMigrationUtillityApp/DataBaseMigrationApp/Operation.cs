using ConsoleTables;
using Data.Domain;
using DataMigration.DataClass;
using DataMigration.Domain;
using DataMigration.DomainClass;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataBaseMigrationApp
{
    public class Operation
    {
        public static async Task ApplyMigrations(int start, int end, CancellationToken cancellationToken)
        
        {
           

            int total = end - start + 1;
            int temp = start;
            if (!cancellationToken.IsCancellationRequested)
            {
                while (total != 0)
                {
                    var SourceTableData = new List<SourceTable>();

                    try
                    {
                        int tempEnd = (total > 100) ? (temp + 100) : end + 1;
                        var newContext = new DataMigrationDbContext();
                        SourceTableData = await newContext.SourceTable
                        .Where(x => (x.ID >= temp && x.ID < tempEnd)).ToListAsync();
                    }
                    catch (ArgumentNullException ex)
                    {
                        Console.WriteLine("ArgumentNullException :" + ex.Message);
                    }

                    if (total > 100)
                    {
                        temp += 100;
                        total -= 100;
                    }
                    else
                        total = 0;

                    try
                    {
                        Task t = Task.Factory.StartNew(() => AddData(SourceTableData, cancellationToken));
                        await t;
                    }
                    catch (ArgumentNullException ex)
                    {
                        Console.WriteLine("ArgumentNullException :" + ex.Message);
                    }
                }
                
                
            }
        }

        private static async Task AddData(List<SourceTable> SourceTableData, CancellationToken cancellationToken)
        {
            
            
            var SourceTableIdData = SourceTableData.Select(x => x.ID).ToArray();

            var DestinationTableData = new List<DestinationTable>();
            int i = 0;
            foreach (var item in SourceTableData)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }


                    DestinationTableData.Add(new DestinationTable()
                {
                    SourceTableId = SourceTableIdData[i],
                    Sum = await Sum(item.FirstNumber, item.SecondNumber)
                });
                i++;
            }
            try
            {
                using (var newContext = new DataMigrationDbContext())
                {
                    await newContext.DestinationTable.AddRangeAsync(DestinationTableData);
                    newContext.SaveChanges();
                }
            }
            catch(DbUpdateException e)
            {
                Console.WriteLine(e.InnerException.Message);
               
                    
            }

            
        }

        public static DataMigrationTable CreateNewMigration(int StartNumber, int EndNumber)
        {
            var newMigrationData = new DataMigrationTable()
            {
                Start = StartNumber,
                End = EndNumber,
                Status = "OnGoing"
            };

            using (var newContext = new DataMigrationDbContext())
            {
                newContext.DataMigrationTable.Add(newMigrationData);
                newContext.SaveChanges();
                return newMigrationData;
            }
        }

        public static void ShowStatus()
        {
            var newContext = new DataMigrationDbContext();
            var MigrataionData = newContext.DataMigrationTable.ToList();

            var table = new ConsoleTable("Id", "Start", "End", "Status");
            foreach (var status in MigrataionData)
            {
                table.AddRow($"{status.ID}", $"{status.Start}", $"{status.End}", $"{status.Status}");
            }
            Console.WriteLine(table);
        }

        public static async Task<int> Sum(int FirstNumber, int SecondNumber)
        {
            await Task.Delay(50);
          
            return FirstNumber + SecondNumber;
        }

        public static void TakeValidInputs(out int StartNumber, out int EndNumber)
        {
            Console.Write("Enter Start Number : ");
            var IDAsString = Console.ReadLine();

            while (!int.TryParse(IDAsString, out StartNumber) || StartNumber < 0 || StartNumber > 1000000)
            {
                Console.WriteLine("Enter Valid Number!\n");
                Console.Write("Enter Start Number : ");
                IDAsString = Console.ReadLine();
            }

            Console.Write("Enter End Number : ");
            IDAsString = Console.ReadLine();

            while (!int.TryParse(IDAsString, out EndNumber) || EndNumber < 0 || EndNumber > 1000000)
            {
                Console.WriteLine("Enter Valid Number!\n");
                Console.Write("Enter End Number : ");
                IDAsString = Console.ReadLine();
            }
        }

        public static void CanceledMigrations(int newId)
        {
            using (var newContext = new DataMigrationDbContext())
            {
                var MigrationData = newContext.DataMigrationTable.Find(newId);
                MigrationData.Status = "Canceled";
                newContext.DataMigrationTable.Attach(MigrationData);
                newContext.SaveChanges();
                Console.WriteLine($"{MigrationData.Status}");
            }
        }

        public static void CompletedMigrations(int StartNumber, int EndNumber, int newId)
        {
           

            using (var newContext = new DataMigrationDbContext())
            {
                var MigrationData = newContext.DataMigrationTable.Find(newId);
                MigrationData.Status = "Completed";
                newContext.DataMigrationTable.Attach(MigrationData);
                newContext.SaveChanges();
                Console.WriteLine($"Migration {StartNumber} to {EndNumber} {MigrationData.Status}");
            }
        }
       

    }
}
