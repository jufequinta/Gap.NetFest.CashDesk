using Gap.NetFest.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gap.NetFest.Core.Interface
{
    public interface IStore
    {
        List<Store> GetAll();
    }
}
