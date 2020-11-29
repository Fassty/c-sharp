namespace GateNetworks
{
    static class ChargeBuilder
    {
        public static Charge Parse(string charge)
        {
            if(charge.Length != 1) throw new ChargeParsingError();
            switch (charge[0])
            {
                case '0': return Charge.Zero;
                case '1': return Charge.One;
                case '?': return Charge.Undef;
                default: throw new ChargeParsingError();
            }
        }

        public static string ChargeToString(Charge charge)
        {
            switch (charge)
            {
                case Charge.Zero: return "0";
                case Charge.One: return "1";
                case Charge.Undef: return "?";
                default:throw new MyException();
            }
        }
    }

    public enum Charge { Zero = 0, One = 1, Undef = 2, Size = 3 }
}