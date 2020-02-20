using Gap.NetFest.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gap.NetFest.Core.Interface
{
    public interface IChocolate
    {
        /// <summary>
        /// Method for getting Chocolate.
        /// </summary>
        /// <returns></returns>
        List<Chocolate> GetChocolates();
    }
}
