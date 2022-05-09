using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Services.Interfaces
{
    interface IOperations : IDisposable
    {
        void Save();
    }
}