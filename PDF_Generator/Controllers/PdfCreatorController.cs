using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace PDF_Generator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfCreatorController : ControllerBase
    {
        private IConverter _converter;

        public PdfCreatorController(IConverter converter)
        {
            _converter = converter;
        }

        [HttpGet]
        public IActionResult CreatePDF()
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "百度"
                // Out = @"D:\PDFCreator\Employee_Report.pdf"
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                // HtmlContent = TemplateGenerator.GetHTMLString(),
                Page = "https://www.cnblogs.com/",
                //WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                //HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                //FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);
            return File(file, "application/pdf", "百度.pdf");
          //  return Ok("Successfully created PDF document.");
        }
    }
}