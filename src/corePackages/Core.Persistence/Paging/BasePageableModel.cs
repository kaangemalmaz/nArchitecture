namespace Core.Persistence.Paging;

public class BasePageableModel
{
    public int Index { get; set; }  //kaçıncı sayfa
    public int Size { get; set; } //bir sayfada kaç data var
    public int Count { get; set; } //toplam data
    public int Pages { get; set; } //toplam sayfa
    public bool HasPrevious { get; set; } //önceki sayfa var mı 
    public bool HasNext { get; set; } //sonraki sayfa var mı 
}