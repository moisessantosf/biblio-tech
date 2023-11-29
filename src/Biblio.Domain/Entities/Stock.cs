using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblio.Domain.Entities
{
    public class Stock
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int Amount { get; set; }
    }
}
