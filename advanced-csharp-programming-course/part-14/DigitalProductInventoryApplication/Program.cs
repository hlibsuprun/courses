namespace DigitalProductInventoryApplication;

internal class Program
{
    private static void Main(string[] args)
    {
        var products = new List<ProductBase>();
        var categories = new List<CategoryBase>();

        var digitalBook = FactoryPattern<DigitalBook, ProductBase>.GetInstance();

        AddPropertiesToProduct(digitalBook, 1, "The Old Man and the Sea", 1);

        products.Add(digitalBook);

        var movie = FactoryPattern<Movie, ProductBase>.GetInstance();

        AddPropertiesToProduct(movie, 2, "Highlander", 2);

        products.Add(movie);

        movie = FactoryPattern<Movie, ProductBase>.GetInstance();

        AddPropertiesToProduct(movie, 3, "Shawshank Redemption", 2);

        products.Add(movie);

        var album = FactoryPattern<MusicRecording, ProductBase>.GetInstance();

        AddPropertiesToProduct(album, 4, "Iron Man Soundtrack", 3);

        products.Add(album);

        var digitalBookCategory = FactoryPattern<DigitalBookCategory, CategoryBase>.GetInstance();

        AddPropertiesToCategory(digitalBookCategory, 1, "Book", "Books digitised for download");

        categories.Add(digitalBookCategory);

        var movieCategory = FactoryPattern<MovieCategory, CategoryBase>.GetInstance();

        AddPropertiesToCategory(movieCategory, 2, "Movie", "Movies digitised for download");

        categories.Add(movieCategory);

        var musicCategory = FactoryPattern<MusicCategory, CategoryBase>.GetInstance();

        AddPropertiesToCategory(musicCategory, 3, "Music", "Music digitised for download");

        categories.Add(musicCategory);

        var queryResults = GetProducts(products, categories);


        foreach (var result in queryResults)
        {
            Console.WriteLine($"Product Id: {result.ProductId}");
            Console.WriteLine($"Title: {result.Title}");
            Console.WriteLine($"Category: {result.Category}");
            Console.WriteLine($"Category Description: {result.CategoryDescription}");
            Console.WriteLine();
        }


        Console.ReadKey();
    }

    private static IEnumerable<ProductViewModel> GetProducts(List<ProductBase> products, List<CategoryBase> categories)
    {
        return from p in products
            join c in categories on p.CategoryId equals c.Id
            select new ProductViewModel
            {
                ProductId = p.Id,
                Title = p.Title,
                Category = c.Title,
                CategoryDescription = c.Description
            };
    }

    private static void AddPropertiesToCategory(CategoryBase category, int id, string title, string description)
    {
        category.Id = id;
        category.Title = title;
        category.Description = description;
    }

    private static void AddPropertiesToProduct(ProductBase product, int id, string title, int categoryId)
    {
        product.Id = id;
        product.Title = title;
        product.CategoryId = categoryId;
    }
}

public class ProductViewModel
{
    public int ProductId { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public string CategoryDescription { get; set; }
}

public interface IPrimaryProperties
{
    int Id { get; set; }
    string Title { get; set; }
}

public abstract class ProductBase : IPrimaryProperties
{
    public int CategoryId { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
}

public class Movie : ProductBase
{
    public string Director { get; set; }
    public string Producer { get; set; }
}

public class DigitalBook : ProductBase
{
    public string Author { get; set; }
}

public class MusicRecording : ProductBase
{
    public string RecordCompany { get; set; }
}

public abstract class CategoryBase : IPrimaryProperties
{
    public string Description { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
}

public class MovieCategory : CategoryBase
{
}

public class DigitalBookCategory : CategoryBase
{
}

public class MusicCategory : CategoryBase
{
}

public static class FactoryPattern<T, U> where T : class, U, new()
    where U : IPrimaryProperties
{
    public static U GetInstance()
    {
        U objT;
        objT = new T();
        return objT;
    }
}