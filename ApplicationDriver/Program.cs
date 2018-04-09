using ComTestWCFService;
using System;

namespace ApplicationDriver
{
    public class Program
    {
        static void Main(string[] args)
        {
            ComTestController controller = new ComTestController();
            controller.Run();

            Console.WriteLine("ComTest WCF Service running, press <ENTER> to stop!");
            Console.ReadLine();

            controller.Stop();


        }
    }
}
