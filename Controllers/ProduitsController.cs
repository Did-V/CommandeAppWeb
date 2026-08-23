using Microsoft.AspNetCore.Mvc;
using CommandeAppWeb;
using CommandeAppWeb.Data;
using CommandeAppWeb.Models;

namespace CommandeAppWeb.Controllers;

public class ProduitsController : Controller
{
    private readonly AppDbContext _context;

    public ProduitsController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var produits = _context.Produits.ToList();
        return View(produits);
    }

    public IActionResult Details(int id)
    {
        var produit = _context.Produits.Find(id);
        if (produit == null)
        {
            return NotFound();
        }
        return View(produit);
    }

    //Afficher le formulaire vide
    public IActionResult Create()
    {
        return View();
    }

    //Traiter l'envoi du formulaire
    [HttpPost]
    public IActionResult Create(Produit produit)
    {
        if (ModelState.IsValid)
        {
            _context.Produits.Add(produit);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(produit);
    }

    //GET /Produits/EDit/3 -> affiche le formulaire pré-rempli
    public IActionResult Edit(int id)
    {
        var produit = _context.Produits.Find(id);
        if (produit == null) return NotFound();
        return View(produit);
    }

    //POST /Produits/EDit/3 -> traite la modification
    [HttpPost]
    public IActionResult Edit(int id, Produit produit)
    {
        if (id != produit.Id) return BadRequest();

        if (ModelState.IsValid)
        {
            _context.Produits.Update(produit);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(produit);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var produit = _context.Produits.Find(id);
        if (produit == null) return NotFound();

        _context.Produits.Remove(produit);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}