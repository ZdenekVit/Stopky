namespace Stopky_test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            menu();
            Casovac stopky = new Casovac();
        }

        static void menu()
        {
            Console.SetCursorPosition(0, 1);
            Console.WriteLine("*-------------------------------------------*");
            Console.WriteLine("(S)tart - (R)eset - (P)auza - (K)olo");
            Console.WriteLine("(H)istorie - (U)lozit zaznam - Ulozene (C)asy");
            Console.WriteLine("*-------------Nedávna Historie--------------*");
        }
    }
}