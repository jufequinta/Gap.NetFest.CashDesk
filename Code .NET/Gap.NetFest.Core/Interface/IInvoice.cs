using Gap.NetFest.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gap.NetFest.Core.Interface
{
    public interface IInvoice
    {
        bool SaveInvoice(Invoice invoiceSimulation);
        bool SaveInvoice(string invoiceSimulation);
        string DequeueMessage();
    }
}
