namespace StringApp.PL
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;
            Menu menu = new Menu();
            menu.MainMenu();
        }
    }
}