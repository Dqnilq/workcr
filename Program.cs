using controltest;

namespace controltest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Клиентский код.
            var subject = new Another.Subject();
            var observerA = new Another.Subject();
            subject.Attach(observerA);

            var observerB = new Another.Subject();
            subject.Attach(observerB);

            subject.SomeBusinessLogic();
            subject.SomeBusinessLogic();

            subject.Detach(observerB);

            subject.SomeBusinessLogic();
        }
    }
    
}