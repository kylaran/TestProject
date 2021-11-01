using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testProject.Models
{
    public class Card
    {
        public Card(string Bills, string Amout)
        {
            this.Bills = Bills;
            this.Amout = Amout;
        }
        public string Bills { get; set; }
        public string Amout { get; set; }
    }
}