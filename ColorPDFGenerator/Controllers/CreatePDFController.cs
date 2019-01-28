using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelectPdf;

namespace ColorPDFGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreatePDFController : ControllerBase
    {
        [HttpGet]
        public IActionResult CreatePDF(string url, string FileName)
        {
            if (string.IsNullOrEmpty(url))
            {
                return Content("请上传网址");
            }

            if (string.IsNullOrEmpty(FileName))
            {
                FileName = "Lexus车库.pdf";
            }
            else
            {
                if (!FileName.Contains(".pdf"))
                {
                    FileName = FileName += ".pdf";
                }
            }
            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.WebPageWidth = 1024;
            converter.Options.WebPageHeight = 0;

            // create a new pdf document converting an url
            PdfDocument doc = converter.ConvertUrl(url);

            // save pdf document
            byte[] pdf = doc.Save();

            // close pdf document
            doc.Close();

            // return resulted pdf document
            FileResult fileResult = new FileContentResult(pdf, "application/pdf")
            {
                FileDownloadName = FileName
            };
            return fileResult;
        }
    }
}