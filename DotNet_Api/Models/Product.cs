﻿namespace DotNet_Api.Models
{
    public class Product
    {
        public int ID { get; set; }

        public string? Name { get; set; }

        public int Price { get; set; }

        public string? Description { get; set; }

        //public int? CategoryID { get; set; }

        //List<Category> Categories { get; set; }
    }
}
