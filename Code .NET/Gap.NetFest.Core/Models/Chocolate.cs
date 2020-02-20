using Gap.NetFest.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gap.NetFest.Core.Models
{
    public class Chocolate
    {
        private IChocolate ChocolateRepository;

        public Chocolate(IChocolate chocolateRepository)
        {
            if (this.ChocolateRepository != null)
                this.ChocolateRepository = null;

            this.ChocolateRepository = chocolateRepository;
        }

        public Chocolate() { }

        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descripcion { get; set; }
        public int IdProvider { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        #endregion

        private List<Chocolate> GetChocolates()
        {
            var listChocolate = this.ChocolateRepository.GetChocolates();
            return listChocolate;
        }

        public List<Chocolate> GetRandomChocolates(int randomItem)
        {
            var chocolateList = this.GetChocolates();
            var randomList = new List<Chocolate>();
            Random random = new Random();

            for (int i = 0; i < randomItem; i++)
            {
                var flagRepeatItem = true;
                while (flagRepeatItem)
                {
                    var item = random.Next(0, chocolateList.Count);
                    var chocolateTMP = chocolateList[item];
                    if (!randomList.Exists(x => x.Id == chocolateTMP.Id))
                    {
                        chocolateTMP.Amount = random.Next(1, 5);
                        randomList.Add(chocolateTMP);
                        flagRepeatItem = false;
                    }
                }
            }
            return randomList;
        }
    }
}
