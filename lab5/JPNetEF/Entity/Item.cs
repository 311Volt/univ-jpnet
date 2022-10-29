using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JPNetEF.Entity
{
    internal class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int NumAvailable { get; set; }

        public Item(int id, string name, decimal price, int numAvailable)
        {
            Id = id;
            Name = name;
            Price = price;
            NumAvailable = numAvailable;
        }

        public override string ToString()
        {
            return String.Format("[#{0,-5}] {1} ({2:C2}) | {3} in stock", Id, Name, Price, NumAvailable);
        }
    }
}
