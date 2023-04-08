﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZID.Automat.Domain.Interfaces;

namespace ZID.Automat.Domain.Models
{
    public class Categorie:HasName
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        private List<Item> _Items { get; set; } = new List<Item>();
        public IReadOnlyList<Item> Items => _Items;
    }
}
