using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class DepartamentoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = new ML.Result(); //instanceamos la clase result 
            result = BL.Departamento.GetAll(); //llamamos el metodo

            if (result.Correct) //validacion 
            {
                ML.Departamento departamento = new ML.Departamento();
                departamento.Departamentos = result.Objects;
                return View(departamento);
            }
            else
            {
                ViewBag.Massage = "Ocurrio un error al consultar los departamentos. ";
                return View();
            }
        }
    }
}
