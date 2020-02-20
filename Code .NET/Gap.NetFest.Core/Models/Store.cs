using Gap.NetFest.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gap.NetFest.Core.Models
{
    public class Store
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public short PosMachineAmount { get; set; }

        private static IStore StoreRepository;

        /// <summary>
        /// Constructor Empty.
        /// </summary>
        public Store()
        {
        }

        public Store(IStore storeRepository)
        {
            StoreRepository = storeRepository;
        }

        /// <summary>
        /// Get Store List.
        /// </summary>
        /// <returns>Get Customers.</returns>
        public List<Store> GetStores()
        {
            return StoreRepository.GetAll();
        }
    }
}
