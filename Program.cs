using controltest;

namespace controltest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Клиентский код.
            var subject = new Another.Subject();
            var observerA = new ConcreteObserverA();
            subject.Attach(observerA);

            var observerB = new ConcreteObserverB();
            subject.Attach(observerB);

            subject.SomeBusinessLogic();
            subject.SomeBusinessLogic();

            subject.Detach(observerB);

            subject.SomeBusinessLogic();
        }
    }
    
}