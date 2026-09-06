using CommandeAppWeb.Data;
using CommandeAppWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandeAppWeb.Controllers
{
    [ApiController]
    [Route("api/Produits")]
    public class ProduitsApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProduitsApiController(AppDbContext context)
        {
            _context = context;
        }

        //GET api/Produits
        [HttpGet]
        public IActionResult GetAll()
        {
            var produits = _context.Produits.ToList();
            return Ok(produits);
        }

        //GET api/Produits/3
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var produit = _context.Produits.Find(id);
            if(produit == null) return NotFound();
            return Ok(produit);
        }

        //POST api/Produits
        [HttpPost]
        public IActionResult Create(Produit produit)
        {
            _context.Produits.Add(produit);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = produit.Id }, produit);
        }

        //PUT api/Produits
        [HttpPut("{id}")]
        public IActionResult Update(int id, Produit produit)
        {
            if (id != produit.Id) return BadRequest();

            var produitExistant = _context.Produits.Find(id);
            if (produitExistant == null) return NotFound();

            produitExistant.Nom = produit.Nom;
            produitExistant.Prix = produit.Prix;
            produitExistant.Stock = produit.Stock;

            _context.SaveChanges();
            return NoContent();
        }

        //DELETE api/Produits/3
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var produit = _context.Produits.Find(id);
            if (produit == null) return NotFound();

            _context.Produits.Remove(produit);
            _context.SaveChanges();
            return NoContent();
        }

    }
}