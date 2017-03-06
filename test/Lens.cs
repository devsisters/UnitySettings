namespace Test
{
    public class Lens<Obj, Value>
    {
        public delegate Value FromCallback(Obj obj);
        public delegate Obj ToCallback(Value value, Obj obj);

        public Lens(FromCallback from, ToCallback to)
        {
            From = from;
            To = to;
        }

        public readonly FromCallback From;
        public readonly ToCallback To;
    }
}
