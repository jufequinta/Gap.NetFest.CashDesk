using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gap.NetFest.DataAccess.Repositories
{
    public class BaseRepository
    {
        public ChocolateCompanyContext Context{ get; set; }

        public BaseRepository()
        {
            this.Context = new ChocolateCompanyContext();
            this.Context.Database.SetCommandTimeout(150000);
        }

        public ChocolateCompanyContext GetInstance() {
            return this.Context;
        }
    }
}
