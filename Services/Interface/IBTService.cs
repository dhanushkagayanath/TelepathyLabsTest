using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interface
{
    public interface IBTService
    {
        BTree GenerateBTreeFromString(string input);
        string CalculateBTree(BTree btree);
        string GetPrintableString(BTree btree);

        CalResult ProcessString(string input);
    }
}
