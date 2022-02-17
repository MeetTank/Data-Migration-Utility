using DataMigration.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain
{
    public class DestinationTable
    {
        public int ID { get; set; }
        public int Sum { get; set; }

     
        [ForeignKey("SourceTable")]
        public int SourceTableId { get; set; }
        public SourceTable sourceTable { get; set; }
    }
}
