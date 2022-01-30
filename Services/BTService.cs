using Models;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Services
{
    public class BTService : IBTService
    {
        static char[] validChars = { '+', '-', 'x', '÷', '(', ')', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static char[] operands = { '+', '-', 'x', '÷' };
        static char[] multiplyDevideOps = { 'x', '÷' };

        public string CalculateBTree(BTree btree)
        {
            return Process(btree.rootNode);
        }

        private string Process(Node node)
        {
            if (node == null) return null;
            //if root value is a number, it is a leaf node. just return the value.
            double number;
            bool isNumeric = double.TryParse(node.Value, out number);
            if (isNumeric) return number.ToString();

            //if root value is an operand, process further.
            string leftValue = Process(node.Left);
            string rightValue = Process(node.Right);



            double l, r;

            switch (node.Value)
            {
                case "+":
                    if (leftValue == "Infinity") return "Infinity";
                    l = Convert.ToDouble(leftValue);
                    r = Convert.ToDouble(rightValue);
                    number = l + r;
                    break;
                case "-":
                    if (leftValue == "Infinity") return "Infinity";
                    l = Convert.ToDouble(leftValue);
                    r = Convert.ToDouble(rightValue);
                    number = l - r;
                    break;
                case "x":
                    if (leftValue == "Infinity") return "Infinity";
                    l = Convert.ToDouble(leftValue);
                    r = Convert.ToDouble(rightValue);
                    number = l * r;
                    break;
                case "÷":
                    if (rightValue == "0") return "Infinity";
                    l = Convert.ToDouble(leftValue);
                    r = Convert.ToDouble(rightValue);
                    number = l / r;
                    break;
                default:
                    throw new ApplicationException("Invalid oprator found: " + node.Value);
                    break;
            }

            return number.ToString();
        }

        public BTree GenerateBTreeFromString(string input)
        {
            //Notes:
            //empty spaces will be ignored
            //paranthesis must be used for program to understand execution order. For example input string should be (2 + (1 + 1)) instead of (2 + 1 + 1)
            //binary tree creating will continue even if invalid input string found. This is to capture all the errors and prompt to the user.

            //wrap the input string inside paranthasis (if not exists)
            input = "(" + input + ")";

            char[] lexicals = input.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray();
            IList<string> errors = new List<string>();
            Stack<Node> nodeStack = new Stack<Node>();

            int openP = 0;
            for (int i = 0; i < lexicals.Length; i++)
            {
                if (lexicals[i] == '(')
                {
                    openP++;
                }
                else if (lexicals[i] == ')')
                {
                    if (nodeStack.Count < 1) continue;

                    Node rNode = nodeStack.Pop();
                    //rnode should always be a number

                    Node valueNode = nodeStack.Count > 0 ? nodeStack.Pop() : new Node { Value = "+" };
                    Node lNode = nodeStack.Count > 0 ? nodeStack.Pop() : new Node { Value = "0" };
                    Node updatedNode = new Node { Value = valueNode.Value, Left = lNode, Right = rNode };

                    nodeStack.Push(updatedNode);
                    openP--;
                }
                else
                {
                    // only + - x ÷ ( ) and whole numbers expected.

                    if (!validChars.Contains(lexicals[i])) errors.Add(string.Format("Unexpected character found!. Character: '{0}' CharCode: {1}", lexicals[i], (int)lexicals[i]));

                    // if more than one digits come next to each other, should concat them as a single number
                    if (i > 0 && Char.IsNumber(lexicals[i - 1]) && Char.IsNumber(lexicals[i]))
                    {
                        Node prevNode = nodeStack.Pop();
                        prevNode.Value += lexicals[i].ToString();
                        nodeStack.Push(prevNode);
                    }
                    // handle negative values. '-' just after 'x' or '÷' operands considered valid.Ex, this is valid -> (1 × -3) or (1 ÷ -3)
                    // other scenarios, two operands should not come together
                    else if (i > 1 && operands.Contains(lexicals[i - 2]) && operands.Contains(lexicals[i - 1]) && numbers.Contains(lexicals[i]))
                    {
                        char oparand1 = lexicals[i - 2];
                        char oparand2 = lexicals[i - 1];
                        char number = lexicals[i - 1];

                        if (multiplyDevideOps.Contains(oparand1) && oparand2 == '-')
                        {
                            /*valid for negative numbers. ex: 1 × -3) or (1 ÷ -3)*/
                            Node lNode = new Node { Value = "0" };
                            Node valueNode = nodeStack.Pop();
                            Node rNode = new Node { Value = lexicals[i].ToString() };
                            Node updatedNode = new Node { Value = valueNode.Value, Left = lNode, Right = rNode };
                            nodeStack.Push(updatedNode);
                        }
                        else
                        {
                            errors.Add(string.Format("Two operands should not come together except in case for negative numbers!. Found: '{0}' and {1}", oparand1, oparand2));
                        }
                    }
                    else
                    {
                        Node node = new Node { Value = lexicals[i].ToString() };
                        nodeStack.Push(node);
                    }
                }

            }

            BTree btree = new BTree();
            //if the given input is valid, there should be one node in nodeStack.
            if (nodeStack.Count != 1) errors.Add("Input string is invalid!");

            if (errors.Count > 0) btree.Errors = errors;
            else btree.rootNode = nodeStack.Pop();

            return btree;
        }

        private StringBuilder sb = new StringBuilder();
        private int indent = 0;
        public string GetPrintableString(BTree btree)
        {
            GetString(btree.rootNode);
            return sb.ToString();
        }

        private void GetString(Node node)
        {
            sb.Append(node.Value + Environment.NewLine);

            if (node.Left != null)
            {
                sb.Append(Indent(indent)).Append("├──");
                indent++;
                GetString(node.Left);
                indent--;
            }

            if (node.Right != null)
            {
                sb.Append(Indent(indent)).Append("└──");
                indent++;
                GetString(node.Right);
                indent--;
            }
        }

        private StringBuilder Indent(int number)
        {
            if (number == 0) return null;
            StringBuilder sb = new StringBuilder("|");
            int count = 0;
            while (count < number)
            {
                sb.Append("     ");
                count++;
            }
            return sb;
        }

        public CalResult ProcessString(string input)
        {
            CalResult calResult = new CalResult();
            BTree bTree = GenerateBTreeFromString(input);
            if (bTree.Errors.Count == 0)
            {
                calResult.BTreeString = GetPrintableString(bTree);
                calResult.Result = CalculateBTree(bTree);
            }
            else calResult.Errors = String.Join(", ", bTree.Errors.ToArray());

            return calResult;
        }
    }
}
