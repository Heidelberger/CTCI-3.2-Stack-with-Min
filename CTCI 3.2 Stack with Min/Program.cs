using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTCI_3._2_Stack_with_Min
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeaderMsg(3, 2, "Stack with 'min' function");

            Random rnd = new Random((int)DateTime.Now.Ticks);

            StackWithMin myStack = new StackWithMin();

            // Push 10 values onto the stack
            for (int i = 0; i < 10; ++i)
            {
                myStack.Push(rnd.Next(10, 99));
            }

            myStack.PrintStack();

            // Pop 5 values off stack
            Console.WriteLine("Popping: " + myStack.Pop() + " min is now: " + myStack.Min());
            Console.WriteLine("Popping: " + myStack.Pop() + " min is now: " + myStack.Min());
            Console.WriteLine("Popping: " + myStack.Pop() + " min is now: " + myStack.Min());
            Console.WriteLine("Popping: " + myStack.Pop() + " min is now: " + myStack.Min());
            Console.WriteLine("Popping: " + myStack.Pop() + " min is now: " + myStack.Min());

            Console.ReadLine();
        }      

        private static void PrintHeaderMsg(int chapter, int problem, string title)
        {
            Console.WriteLine("Cracking the Coding Interview");
            Console.WriteLine("Chapter " + chapter + ", Problem " + chapter + "." + problem + ": " + title);
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 
    /// 1. Create an object with a "value" and a "min below" (both ints)
    /// 2. Creaet an array of the above objects
    /// 3. "Push()" adds the passed int as "value" and "min below"
    /// 4. Every subsequent Push() adds passed int as "value" and "min below"
    ///    if passed int is lower than "min below". Otherwise, "min below"
    ///    is copied from the last object.
    ///    
    /// NOTE: Storing a copy of "min below" with every stack entry is wasteful
    ///       but it's fast and easy to implement/maintain.
    ///    
    /// Complexity:     Push() runs in O(1)
    ///                 Pop()  runs in O(1)
    ///                 Min()  runs in O(1)
    ///                 Time is constant regardless of input.
    ///                 
    ///                 Memory is O(N)
    ///                 Stack size is statically defined (unchanging), however
    ///                 new objects are created to place on the stack for every
    ///                 Push().
    /// 
    /// </summary>
    class StackWithMin
    {
        int cursor;
        StackObject[] soArray;
        int totalElements;

        public StackWithMin()
        {
            totalElements = 100;
            soArray = new StackObject[totalElements];
            cursor = -1;
        }

        public void Push(int passed_value)
        {
            if (cursor >= totalElements)            
                throw new StackOverflowException();

            int min = 0;

            if (cursor < 0)
                min = passed_value;
            else
                min = Math.Min(soArray[cursor].min_below, passed_value);

            soArray[++cursor] = new StackObject(passed_value, min);
        }

        public int Pop()
        {
            return soArray[cursor--].value;
        }

        public int Min()
        {
            return soArray[cursor].min_below;
        }

        public void PrintStack()
        {
            for (int i = cursor; i >= 0; --i)
            {
                Console.WriteLine("Value: " + soArray[i].value + " Min Beneath: " + soArray[i].min_below);
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 
    /// "min_below" tracks the smallest value of this object or any object below it on the stack
    /// It's assigned in the Push() method.
    /// 
    /// </summary>
    class StackObject
    {
        public int value;      // the value pushed onto the stack
        public int min_below;  // represents the lowest value on the stack beneath this object

        public StackObject(int passed_value, int passed_min)
        {
            value = passed_value;
            min_below = passed_min;
        }
    }
}