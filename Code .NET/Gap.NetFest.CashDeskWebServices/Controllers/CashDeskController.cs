using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gap.NetFest.Core.Interface;
using Gap.NetFest.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Database = Gap.NetFest.DataAccess;

namespace Gap.NetFest.CashDeskWebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashDeskController : ControllerBase
    {
        // GET api/CashDesk
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/CashDesk/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/CashDesk
        [HttpPost]
        public ActionResult<bool> Post(Invoice invoice)
        {
            try
            {
                //IInvoice invoiceRepository = new Database.Repositories.InvoiceRepository();
                //invoice.SetRepository(invoiceRepository);
                //return invoice.SaveInvoce();
                return true;
            }
            catch (Exception exe) {
                throw new Exception(exe.Message,exe.InnerException);
            }
        }

        // PUT api/CashDesk/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/CashDesk/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
