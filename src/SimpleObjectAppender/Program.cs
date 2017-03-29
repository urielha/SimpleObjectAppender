using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleObjectAppender
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = log4net.LogManager.GetLogger("mylogger");
            logger.Info("hello");
            while (true)
            {
                logger.Info(new Person { Age = 1, Name = "Person" });
                Console.ReadKey();
            }
        }

        class Person
        {
            public int Age { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return "A person";
            }
        }
    }
}
