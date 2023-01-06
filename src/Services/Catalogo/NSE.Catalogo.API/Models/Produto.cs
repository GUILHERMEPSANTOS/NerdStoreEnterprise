using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DomainObjects;

namespace NSE.Catalogo.API.Models
{
    public class Produto: Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal Price { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Image { get; set; }
        public int StockQuantity { get; set; }
    }
}