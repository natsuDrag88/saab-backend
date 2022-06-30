using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.StaticFiles;
using saab.Data;
using saab.Model.Error;
using saab.Services.Billing;
using saab.Util.Enum;
using saab.Util.ResponseDictionary;
using Swashbuckle.AspNetCore.Annotations;

namespace saab.Controllers.Billing
{
    [Route("api/v1/facturas/download")]
    [ApiController]
    public class DownloadInvoiceController : ControllerBase
    {
        private readonly IBillingService _billingService;

        public DownloadInvoiceController(IBillingService billingService)
        {
            _billingService = billingService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Descargar Factura de un solo periodo.",
            Description = "Descargar Factura de un solo periodo.",
            OperationId = "Get",
            Tags = new[] { "Proyectos" })]
        public Task<IActionResult> Get([BindRequired, FromQuery] string idFactura)
        {
            try
            {
                var myUuid = Guid.NewGuid();
                var listIdInvoice = idFactura.Split(',').ToList();
                var dataInvoice = _billingService.GetInvoice(idInvoice: listIdInvoice);
                var fileByte = _billingService.GenerateByteZip(dataInvoice: dataInvoice, uuid: myUuid);

                
                return Task.FromResult<IActionResult>(File(fileByte, "application/octet-stream", myUuid + ".zip"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }

}