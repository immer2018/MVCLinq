using MVCEliminatoria_EdgarDuarte_1193520.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCEliminatoria_EdgarDuarte_1193520.Controllers
{
    public class PartidoController : Controller
    {
        private PartidoDataContext db = new PartidoDataContext();
        // tabla de posiciones de  los equipos
        public ActionResult Index()
        {
            var listadoEquipo = db.SP_ListarPosiciones();
            return View(listadoEquipo);
        }
        // los equipos que se encuentran registrados y cuantos puntos tienen
        public ActionResult ListadoEquipos()
        {
            var listadoEquipo = from eq in db.Equipo orderby eq.puntos descending select eq;
            return View(listadoEquipo);
        }
        // vista registro de equipos
        public ActionResult RegistroEquipo()
        {
            return View();
        }
        // insertar nuevos equipos
        [HttpPost]
        public ActionResult RegistroEquipo(Equipo objEquipo)
        {
            try
            {
                objEquipo.puntos = 0;
                // insertar datos del equipo
                db.Equipo.InsertOnSubmit(objEquipo);
                db.SubmitChanges();
                return RedirectToAction("ListadoEquipos");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Partido()
        {
            // se llena un select con el listado de equipos
            List<SelectListItem> lista1 = new List<SelectListItem>();
            foreach (var item in db.Equipo)
            {
                lista1.Add(new SelectListItem()
                {
                    Value = item.id.ToString(),
                    Text = item.nombre
                });
            }


            //SelectListItem item2 = new SelectListItem();
            //item1.Text = "texto2";
            //item1.Value = "valor2";


            //lista1.Add(item2);

            ViewBag.lista = lista1;
            return View();
        }
        // vista formulario edicion
        public ActionResult Edit(int id)
        {
            var listaequipo = db.Equipo.Single(x => x.id == id);
            return View(listaequipo);
        }

        // actualizando equipo
        [HttpPost]
        public ActionResult Edit(int id, Equipo collection)
        {
            try
            {
                Equipo emp = db.Equipo.Single(x => x.id == id);
                emp.nombre = collection.nombre;
                emp.puntos = collection.puntos;
                db.SubmitChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult RegistroDetalle(Partido objDetalle )
        {
            try
            {
                // insertar datos del equipo
               // db.RegistroDetalleFecha(objfecha.fecha1, objfecha.fkIdE1, objfecha.fkIdE2, objDetalle.goles1, objDetalle.goles2);
                db.SPRegistroPartido(objDetalle.idequipo1, objDetalle.idequipo2, objDetalle.fecha, objDetalle.goles1, objDetalle.goles2);
                return RedirectToAction("Index");
            }
            catch
            {
                // se llena un select con el listado de equipos
                List<SelectListItem> lista1 = new List<SelectListItem>();
                foreach (var item in db.Equipo)
                {
                    lista1.Add(new SelectListItem()
                    {
                        Value = item.id.ToString(),
                        Text = item.nombre
                    });
                }
                ViewBag.lista = lista1;
                return View("Partido");


            }





        }




    }
}
