namespace CommandeApp.Models;//Pourquoi namespace CommandeApp.Models ?
//Réponse : Le namespace CommandeApp.Models est utilisé pour organiser les classes liées aux modèles de l'application CommandeApp. 
// Cela permet de regrouper les classes Produit et Commande sous un même espace de noms, facilitant ainsi la gestion et l'importation des classes dans d'autres parties de l'application.

public class Produit
{
    public int Id { get; set; } //clé primaire pour l'identification unique de chaque produit, utile si l'on souhaite stocker les produits dans une base de données.
    public string Nom { get; set; }
    public decimal Prix { get; set; }
    public int Stock { get; set; }

    public Produit(string nom, decimal prix, int stock)
    {
        if (string.IsNullOrWhiteSpace(nom)) //Remarque : string.IsNullOrWhiteSpace vérifie si la chaîne est null, vide ou composée uniquement d'espaces blancs
        {
            throw new ArgumentException("Le nom du produit ne peut pas être vide.");
        }
        if (prix < 0)
        {
            throw new ArgumentException("Le prix ne peut pas être négatif.");
        }
        if(stock < 0)
        {
            throw new ArgumentException("Le stock ne peut pas être négatif.");
        }

        Nom = nom;
        Prix = prix;
        Stock = stock;
    }

    public void AfficherDetails()
    {
        Console.WriteLine($"Nom: {Nom} : {Prix:C} ({Stock} en stock)");
    }

    //Retourne vrai si le produit est en stock, sinon faux
    public bool EstEnStock()
    {
        return Stock > 0;
    }

    //Retirer une quantité du stock
    public void RetirerDuStock(int quantite)
    {
        if (quantite <= Stock)
        {
            Stock -= quantite;
            Console.WriteLine($"{quantite} unités de {Nom} retirées du stock. Stock Disponible: {Stock}");
        }
        else
        {
            Console.WriteLine($"Stock insuffisant pour {Nom}. Quantité demandée: {quantite}, Stock disponible: {Stock}");
        }
    }
}