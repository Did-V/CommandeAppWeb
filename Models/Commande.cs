namespace CommandeAppWeb.Models;   //Pourquoi namespace CommandeApp.Models ?
//Réponse : Le namespace CommandeApp.Models est utilisé pour organiser les classes liées aux modèles de l'application CommandeApp. 
// Cela permet de regrouper les classes Produit et Commande sous un même espace de noms, facilitant ainsi la gestion et l'importation des classes dans d'autres parties de l'application.

public class Commande
{
    public int Id { get; set; } //clé primaire pour l'identification unique de chaque commande, utile si l'on souhaite stocker les commandes dans une base de données.
    public Produit Produit { get; set; } =null!; //=null! est utilisé pour indiquer au compilateur que la propriété Produit sera initialisée avant d'être utilisée, même si elle n'est pas initialisée dans le constructeur. Cela permet d'éviter les avertissements de référence null.
    public int Quantite { get; internal set; }
    public DateTime DateCommande { get; set; }

    private Commande() { } //Constructeur privé pour EF Core, qui nécessite un constructeur sans paramètre pour instancier les objets lors de la récupération des données depuis la base de données.

    public Commande(Produit produit, int quantite)
    {
        if(produit == null)
        {
            throw new ArgumentNullException(nameof(produit), "Impossible de créer une commande avec un produit à null.");
            //Remarque : nameof(produit) permet de récupérer le nom du paramètre pour l'inclure dans le message d'exception, ce qui rend le code plus maintenable.
        }
        if (quantite <= 0)
        {
            throw new ArgumentException("Impossible de créer une commande avec une quantité nulle ou négative.");
        }
        if(!produit.EstEnStock())
        {
            throw new InvalidOperationException("Impossible de créer une commande pour un produit en rupture de stock.");
            //Remarque : InvalidOperationException est utilisée ici pour indiquer que l'état actuel de l'objet (produit en rupture de stock) ne permet pas l'opération demandée (création d'une commande).
            //Différence entre ArgumentException et InvalidOperationException : ArgumentException est utilisée lorsque l'argument fourni à une méthode n'est pas valide, tandis qu'InvalidOperationException est utilisée lorsque l'état actuel de l'objet ne permet pas l'opération demandée. Dans ce cas, le produit est en rupture de stock, ce qui rend impossible la création d'une commande.
        }
        if(quantite > produit.Stock)
        {
            throw new InvalidOperationException($"Impossible de créer une commande pour {quantite} unités de {produit.Nom}. Stock disponible: {produit.Stock}");
        }

        Produit = produit;
        produit.RetirerDuStock(quantite); //Met à jour le stock du produit lors de la création de la commande 
        Quantite = quantite;
        DateCommande = DateTime.UtcNow; //UtcNow est utilisé pour enregistrer la date et l'heure de la commande en temps universel coordonné (UTC), ce qui permet d'éviter les problèmes liés aux fuseaux horaires lors de l'enregistrement et de l'affichage des dates et heures. C'est pour NpgSQL, qui stocke les dates en UTC par défaut.
    }

    public decimal CalculerTotalCmd()
    {
        return Quantite * Produit.Prix;
    }

    public void AfficherDetails()
    {
        Console.WriteLine($"Commande du {DateCommande.ToLocalTime():dd/MM/yyyy HH:mm:ss} : {Quantite} x {Produit.Nom} = {CalculerTotalCmd():C}");
    }
}