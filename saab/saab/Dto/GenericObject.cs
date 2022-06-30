namespace saab.Dto
{
    public class GenericObject<T,TD>
    {
        public T Id { get; set; }
        public TD Description { get; set; }
    }
}