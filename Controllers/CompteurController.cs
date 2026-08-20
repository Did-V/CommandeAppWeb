using Microsoft.AspNetCore.Mvc;

public class CompteurController : Controller
{
    public IActionResult Index()
    {
        int valeur = HttpContext.Session.GetInt32("Valeur") ?? 0;
        return View(valeur);
    }

    [HttpPost]
    public IActionResult Incrementer()
    {
        int valeur = HttpContext.Session.GetInt32("Valeur") ?? 0;
        valeur++;
        HttpContext.Session.SetInt32("Valeur", valeur);
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult Decrementer()
    {
        int valeur = HttpContext.Session.GetInt32("Valeur") ?? 0;
        valeur--;
        HttpContext.Session.SetInt32("Valeur", valeur);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult RemettreAZero()
    {
        HttpContext.Session.SetInt32("Valeur",0);
        return RedirectToAction("Index");
    }
}