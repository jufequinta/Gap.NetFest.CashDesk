using AutoMapper;
using Gap.NetFest.Core.Interface;
using Gap.NetFest.Core.Models;
using Gap.NetFest.DataAccess.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gap.NetFest.DataAccess.Repositories
{
    public class ChocolateRepository : BaseRepository, IChocolate
    {
        private MapperConfiguration config;

        public ChocolateRepository() : base()
        {
            config = new MapperConfiguration(cfg => cfg.CreateMap<Chocolates_Brands, Chocolate>());
        }

        public List<Chocolate> GetChocolates()
        {
            var ListChocolateReference = new List<Chocolate>();
            var mapper = new Mapper(config);
            var list = this.GetInstance().Chocolates_Brands.Include(x => x.Provider).ToList();
            list.ForEach(x =>
            {
                ListChocolateReference.Add(mapper.Map<Chocolate>(x));
            });

            return ListChocolateReference.OrderBy(x => x.Id).ToList(); ;
        }
    }
}
