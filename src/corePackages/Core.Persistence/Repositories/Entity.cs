namespace Core.Persistence.Repositories;

//veritabanı nesnesi olduğunu gösterir.
public class Entity
{
    public int Id { get; set; }

    public Entity()
    {
    }

    public Entity(int id) : this()
    {
        Id = id;
    }
}