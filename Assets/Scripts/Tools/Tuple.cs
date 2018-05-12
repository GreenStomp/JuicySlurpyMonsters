public class Tuple<T, K>
{
    public T Item1 { get; set; }
    public K Item2 { get; set; }
    public Tuple(T item1, K item2)
    {
        this.Item1 = item1;
        this.Item2 = item2;
    }
    public Tuple()
    {

    }
}
public class Tuple<T, K, Z>
{
    public T Item1 { get; set; }
    public K Item2 { get; set; }
    public Z Item3 { get; set; }
    public Tuple(T item1, K item2, Z item3)
    {
        this.Item1 = item1;
        this.Item2 = item2;
        this.Item3 = item3;
    }
    public Tuple()
    {

    }
}
public class Tuple<T, K, Z, U>
{
    public T Item1 { get; set; }
    public K Item2 { get; set; }
    public Z Item3 { get; set; }
    public U Item4 { get; set; }
    public Tuple(T item1, K item2, Z item3, U item4)
    {
        this.Item1 = item1;
        this.Item2 = item2;
        this.Item3 = item3;
        this.Item4 = item4;
    }
    public Tuple()
    {

    }
}
public class Tuple<T, K, Z, U, Y>
{
    public T Item1 { get; set; }
    public K Item2 { get; set; }
    public Z Item3 { get; set; }
    public U Item4 { get; set; }
    public Y Item5 { get; set; }
    public Tuple(T item1, K item2, Z item3, U item4, Y item5)
    {
        this.Item1 = item1;
        this.Item2 = item2;
        this.Item3 = item3;
        this.Item4 = item4;
        this.Item5 = item5;
    }
    public Tuple()
    {

    }
}