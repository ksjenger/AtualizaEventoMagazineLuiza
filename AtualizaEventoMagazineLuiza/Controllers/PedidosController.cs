using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AtualizaEventoMagazineLuiza.Models;
using AtualizaEventoMagazineLuiza.Data;

namespace AtualizaEventoMagazineLuiza.Controllers
{
    public class PedidosController : Controller
    {
        public IActionResult Index(Pedidos Opedido)
        {
            if (Opedido.Pedido != 0)
            {
                try
                {
                    Opedido = MagazineLuizaData.BuscaEtiqueta(Opedido.Pedido);
                    ViewData["Etiqueta"] = Opedido.Etiqueta;
                    ViewData["Pedido"] = Opedido.Pedido;
                    ViewData["DataOcorrencia"] = Opedido.DataOcorrencia.ToString("yyy-MM-ddTHH:mm");
                    if (Opedido != null)
                    {
                        MagazineLuizaData.EnviaPedidos(Opedido);
                    }
                    else
                    {
                        ViewData["Info"] = "Objeto não localizado";
                    }
                }
                catch
                {
                    ViewData["Info"] = "Objeto não localizado";
                }
            }
            else if (!Opedido.Etiqueta.Equals(string.Empty))
            {
                try
                {
                    Opedido = MagazineLuizaData.BuscaObservacaoUm(Opedido.Etiqueta);
                    ViewData["Etiqueta"] = Opedido.Etiqueta;
                    ViewData["Pedido"] = Opedido.Pedido;
                    ViewData["DataOcorrencia"] = Opedido.DataOcorrencia.ToString("yyy-MM-ddTHH:mm");
                    if (Opedido != null)
                    {
                        MagazineLuizaData.EnviaPedidos(Opedido);
                    }
                    else
                    {
                        ViewData["Info"] = "Objeto não localizado";
                    }
                }
                catch
                {
                    ViewData["Info"] = "Objeto não localizado";
                }
            }
            else
            {
                ViewData["DataOcorrencia"] = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
            }

            return View(Opedido);
        }

        [HttpPost]
        public string BuscarEtiqueta(string p)
        {
            try
            {
                Pedidos oPedido = MagazineLuizaData.BuscaEtiqueta(int.Parse(p));
                return oPedido.Etiqueta;
                
            }
            catch (NullReferenceException)
            {
                return "Objeto não encontrado";
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        [HttpPost]
        public string BuscarPedido(string p)
        {
            try
            {
                Pedidos oPedido = MagazineLuizaData.BuscaObservacaoUm(p);
                return oPedido.Pedido.ToString();

            }
            catch (NullReferenceException)
            {
                return "Objeto não encontrado";
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }
    }
}
