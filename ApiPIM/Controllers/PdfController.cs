using ApiPIM.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> GeneratePDF()
        {

            var pdfCreator = new GeraRelatorio();
            var outputPath = Path.GetTempFileName();

            pdfCreator.CreatePDF(outputPath);

            var pdfBytes = System.IO.File.ReadAllBytes(outputPath);
            System.IO.File.Delete(outputPath);

            var base64EncodedPdf = Convert.ToBase64String(pdfBytes);

            return Ok(base64EncodedPdf);
        }

    }
}
