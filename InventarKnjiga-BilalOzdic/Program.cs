using System;
using BibliotecaSystem.Services;

namespace BibliotecaSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            ManagmentInventara managment = new ManagmentInventara();
            managment.MeniInventara();

            Console.WriteLine("\nDoviđenja!");
        }
    }
}
