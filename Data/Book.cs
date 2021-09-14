using System;
using System.ComponentModel.DataAnnotations;

namespace SlothBookStore.Data 
{
    public class Book
    {
        public string Id {get;set;}
        public string Name {get;set;}
        public string Author {get;set;}
        public string Desc { get; set; }
        public decimal Price {get;set;}
    }
}