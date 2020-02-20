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
    public class StoreRepository : BaseRepository, IStore
    {
        private MapperConfiguration config;
        private static List<Store> StoreList { get; set; }
        public StoreRepository() : base()
        {
            config = new MapperConfiguration(cfg => cfg.CreateMap<Stores, Store>());
            StoreList = new List<Store>();
        }

        public List<Store> GetAll()
        {
            if (StoreList.Count == 0 || (StoreList.Count != this.GetInstance().Stores.Count()))
            {
                FillUpListStore();
            }
            return StoreList;
        }

        private void FillUpListStore()
        {
            var mapper = new Mapper(config);
            var list = this.GetInstance().Stores.ToList();
            list.ForEach(x =>
            {
                StoreList.Add(mapper.Map<Store>(x));
            });
            StoreList = StoreList.OrderBy(x => x.Id).ToList();
        }
    }
}
