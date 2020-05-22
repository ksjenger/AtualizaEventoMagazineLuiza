using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AtualizaEventoMagazineLuiza.Models;
using System.IO;
using AtualizaEventoMagazineLuiza.Data;

namespace AtualizaEventoMagazineLuiza.Controllers
{
    public class MontarRelatoriosController : Controller
    {
        public async Task<IActionResult> Index()
        {
            ViewData["DtInicial"] = DateTime.Now.ToString("yyyy-MM-ddT00:01");
            ViewData["DtFinal"] = DateTime.Now.ToString("yyyy-MM-ddT23:59");
            return View();
        }

        [HttpPost]
        public FileStreamResult Stream(MontarRelatorios montarRelatorios)
        {
            var fileContent = MagazineLuizaData.MontaArquivoRetorno(montarRelatorios);
            if (fileContent.Length > 1)
            {
                var stream = new MemoryStream(fileContent);
                var fileStreamResult = new FileStreamResult(stream, contentType: "text/csv");
                fileStreamResult.FileDownloadName = "ManuseioMagazineLuzia.csv";
                return fileStreamResult;
            }
            return null;
        }

    }
}
