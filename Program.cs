using System;
using System.Collections;
using System.Collections.Generic;

public class Document : IComparable<Document>
{
    public string Title { get; set; }
    public int PageCount { get; set; }
    public int SecrecyLevel { get; set; }  // 1 - цілком таємно, 2 - таємно, 3 - для службового використання, 4 - для загального доступу

    public Document(string title, int pageCount, int secrecyLevel)
    {
        Title = title;
        PageCount = pageCount;
        SecrecyLevel = secrecyLevel;
    }

    // Реалізація IComparable для сортування за кількістю сторінок
    public int CompareTo(Document other)
    {
        return PageCount.CompareTo(other.PageCount);
    }

    public override string ToString()
    {
        return $"Title: {Title}, Pages: {PageCount}, Secrecy Level: {SecrecyLevel}";
    }
}

// Реалізація IComparer для порівняння за кількістю сторінок і рівнем таємності
public class DocumentComparer : IComparer<Document>
{
    public int Compare(Document x, Document y)
    {
        if (x.PageCount == y.PageCount)
        {
            return x.SecrecyLevel.CompareTo(y.SecrecyLevel);
        }
        return x.PageCount.CompareTo(y.PageCount);
    }
}

// Колекція документів, яка реалізує IEnumerable
public class DocumentCollection : IEnumerable<Document>
{
    private List<Document> documents = new List<Document>();

    public void Add(Document document)
    {
        documents.Add(document);
    }

    public IEnumerator<Document> GetEnumerator()
    {
        return documents.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Створення масиву об'єктів класу Документ
        DocumentCollection documents = new DocumentCollection
        {
            new Document("Document A", 10, 2),
            new Document("Document B", 5, 1),
            new Document("Document C", 15, 4),
            new Document("Document D", 10, 3),
            new Document("Document E", 7, 2)
        };

        // Сортування та виведення на консоль за кількістю сторінок
        List<Document> documentList = new List<Document>(documents);
        documentList.Sort(); // Використовує CompareTo для сортування за кількістю сторінок

        Console.WriteLine("Documents sorted by page count:");
        foreach (var doc in documentList)
        {
            Console.WriteLine(doc);
        }

        // Сортування та виведення на консоль за кількістю сторінок і рівнем таємності
        documentList.Sort(new DocumentComparer());

        Console.WriteLine("\nDocuments sorted by page count and secrecy level:");
        foreach (var doc in documentList)
        {
            Console.WriteLine(doc);
        }
    }
}

