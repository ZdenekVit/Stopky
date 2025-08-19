namespace Stopky_test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("(S)tart");
            Casovac stopky = new Casovac();
            string starttt = Console.ReadLine();
            if (starttt == "s")
            {
                stopky.CasStart(DateTime.Now);
            }
        }
    }
}