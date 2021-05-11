using System;
using System.Collections.Generic;
using System.Threading;
using controltest;

namespace controltest
{
    public class Another
    {

        public interface IObserver
        {
            void Update(ISubject subject);
        }

        public interface ISubject
        {
            void Attach(IObserver observer);

            void Detach(IObserver observer);

            void Notify();
        }

        // Рассмотрим реализацю паттерна "Observer" 
        // Как мы видим, его реализация имеет две интерфейса "IObserver" & "ISubject" 
        // Однако наследование двух интерфейсов происходит сразу в одном классе "Subject", параллельно "склеивая" логикую обоих интерфейсов в одном классе никак не связываю их логику между собой.
        // Что в следствии чего создает запах "Неуместная близость" 
        // Для реализации техники "Извлечение класс" избавимся от этого запаха, создав новый класс под наследование другого интерфейса.. в другой ветке.
        public class Subject : ISubject
        {
            public int State { get; set; } = -0;

            private List<IObserver> _observers = new();

            public void Attach(IObserver observer)
            {
                Console.WriteLine("Subject: Attached an observer.");
                this._observers.Add(observer);
            }

            public void Detach(IObserver observer)
            {
                this._observers.Remove(observer);
                Console.WriteLine("Subject: Detached an observer.");
            }

            public void Notify()
            {
                Console.WriteLine("Subject: Notifying observers...");

                foreach (var observer in _observers)
                {
                    observer.Update(this);
                }
            }

            public void SomeBusinessLogic()
            {
                Console.WriteLine("\nSubject: I'm doing something important.");
                this.State = new Random().Next(0, 10);

                Thread.Sleep(15);

                Console.WriteLine("Subject: My state has just changed to: " + this.State);
                this.Notify();
            }

        }
    }
}

// Выделим реализацию интефейса в отдельные два класса для показательного клиента!
class ConcreteObserverA : Another.IObserver
{
    public void Update(Another.ISubject subject)
    {            
        if ((subject as Another.Subject).State < 3)
        {
            Console.WriteLine("ConcreteObserverA: Reacted to the event.");
        }
    }
}

class ConcreteObserverB : Another.IObserver
{
    public void Update(Another.ISubject subject)
    {
        if ((subject as Another.Subject).State == 0 || (subject as Another.Subject).State >= 2)
        {
            Console.WriteLine("ConcreteObserverB: Reacted to the event.");
        }
    }
}