using Gap.NetFest.Core.Interface;
using Gap.NetFest.Core.Models;
using Gap.NetFest.DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gap.NetFest.CashDesk
{
    internal class HostedService : IHostedService, IDisposable
    {
        private Timer[] Timer { get; set; }
        private IConfiguration Configuration { get; set; }
        private Behavior[] BehaviorInstanceArray;
        public List<Store> Stores = null;


        public HostedService(IConfiguration configuration)
        {
            Configuration = configuration;

            IStore storeRepository = new StoreRepository();
            var StoreModel = new Store(storeRepository);
#if DEBUG
            Stores = new List<Store>();
            Stores.AddRange(StoreModel.GetStores().Take(1).ToList());
#else
            Stores = StoreModel.GetStores().OrderBy(x => x.Id).ToList();
#endif

            Timer = new Timer[Stores.Count];
            BehaviorInstanceArray = new Behavior[Stores.Count];
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            System.Console.WriteLine(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
            System.Console.WriteLine("Timed Background Service is starting.");
            for (int i = 0; i < Stores.Count; i++)
            {
                BehaviorInstanceArray[i] = new Behavior();
                BehaviorInstanceArray[i].StoreCurrent = Stores[i];
                Timer[i] = new Timer(DoWork, BehaviorInstanceArray[i].Id, TimeSpan.Zero,
                    TimeSpan.FromSeconds(double.Parse(Configuration["Settings:ExecutionTimeSeconds"])));
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            System.Console.WriteLine("Timed Background Service is stopping.");

            for (int i = 0; i < Timer.Length; i++)
            {
                Timer[i]?.Change(Timeout.Infinite, 0);
            }

            return Task.CompletedTask;
        }
        public void Dispose()
        {
            System.Console.WriteLine("Timed Background Service Disposed.");
            for (int i = 0; i < Timer.Length; i++)
            {
                Timer[i]?.Dispose();
            }
        }

        private void DoWork(object IdBehavior)
        {
            List<Behavior> lstBehavior = BehaviorInstanceArray.OfType<Behavior>().ToList();
            var BehaviorInstance = lstBehavior.Where(x => x.Id == (Guid)IdBehavior).FirstOrDefault();

            if (!BehaviorInstance.IsCustomerModel)
                BehaviorInstance.Start(this.Configuration);


            if (BehaviorInstance.countIteration >= 20)
            {
                BehaviorInstance.countIteration = 0;
                BehaviorInstance.InProcess = StateProcess.RestarToProcess;
                System.Console.WriteLine(".");

            }

            if (BehaviorInstance.InProcess == StateProcess.ReadyToProcess)
            {
                System.Console.WriteLine($"Timed Background Service is working. {DateTime.Now}");
                try
                {
                    BehaviorInstance.countIteration = 1;
                    BehaviorInstance.InProcess = StateProcess.Inprocess;


                    System.Console.WriteLine("Iteration loop Process.");

                    BehaviorInstance.Loop();

                    System.Console.WriteLine("Finish loop Process.");
                    BehaviorInstance.InProcess = StateProcess.RestarToProcess;
                    System.Console.WriteLine($"Timed Background Service end. {DateTime.Now}");
                }
                catch (Exception exe)
                {
                    BehaviorInstance.InProcess = StateProcess.RestarToProcess;
                    System.Console.WriteLine("Error DoWork: " + exe.InnerException.ToString());
                }

            }
            else if (BehaviorInstance.InProcess == StateProcess.RestarToProcess)
            {
                BehaviorInstance.InProcess = StateProcess.ReadyToProcess;
                System.Console.WriteLine("System ready for next iteration loop process.");
            }
            else
            {
                BehaviorInstance.countIteration++;
                System.Console.WriteLine("System in Process loop.");
            }
        }

    }
}
