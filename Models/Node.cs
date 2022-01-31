using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Node
    {
        public Node()
        {

        }

        public Node(string value)
        {
            this.Value = value;
        }

        public string Value;
        public Node Left;
        public Node Right;
    }   
}
