namespace Core.Persistence.Paging;


//Get ile başlayan repo yapılara Ipaginateden türeyerek dönmektedir.

public interface IPaginate<T>
{
    int From { get; }
    int Index { get; }
    int Size { get; }
    int Count { get; }
    int Pages { get; }
    IList<T> Items { get; }
    bool HasPrevious { get; }
    bool HasNext { get; }
}