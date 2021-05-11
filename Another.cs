using System;
using System.Collections.Generic;
using System.Threading;

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
        public class Subject : ISubject, IObserver
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
            
            public void Update(ISubject subject)
            {
                if ((subject as Subject).State == 0 || (subject as Subject).State >= 2)
                {
                    Console.WriteLine("ConcreteObserverB: Reacted to the event.");
                }
            }

          
        }

    }
}