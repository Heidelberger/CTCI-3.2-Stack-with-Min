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
            Console.WriteLine("Popping: " + myStack.Pop() + ", min is now: " + myStack.Min());
            Console.WriteLine("Popping: " + myStack.Pop() + ", min is now: " + myStack.Min());
            Console.WriteLine("Popping: " + myStack.Pop() + ", min is now: " + myStack.Min());
            Console.WriteLine("Popping: " + myStack.Pop() + ", min is now: " + myStack.Min());
            Console.WriteLine("Popping: " + myStack.Pop() + ", min is now: " + myStack.Min());

            myStack.PrintStack();

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
    /// 1. Create an object with a "value" and a "min_value" (both ints)
    /// 2. Creaet an array of the above objects
    /// 3. "Push()" adds the passed int as "value" and "min value"
    /// 4. Every subsequent Push() adds passed_int as "value" and "min_value"
    ///    if passed int is lower than "min_value". Otherwise, "min_value"
    ///    is copied from the last object.  In this way, min_value is always 
    ///    the lowest value of the current object or any object below it on 
    ///    the stack.  Reading min_value from the object on top of the stack 
    ///    always provides O(1) access to the minimum value on the entire stack.
    ///    
    /// NOTE: Storing a copy of "min below" with every stack entry is wasteful
    ///       but it's fast and easy to implement/maintain.
    ///       A better solution would be a separate stack containing min_values
    ///       which are pushed/popped only when necessary (e.g., if current val 
    ///       is less than min_value, min_stack.Push(current_val))
    ///    
    /// Complexity:     Push() runs in O(1)
    ///                 Pop()  runs in O(1)
    ///                 Min()  runs in O(1)
    ///                 Time is constant regardless of input.
    ///                 
    ///                 Memory is O(N)
    ///                 Stack size is statically defined, however
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
                min = Math.Min(soArray[cursor].min_value, passed_value);

            soArray[++cursor] = new StackObject(passed_value, min);
        }

        public int Pop()
        {
            return soArray[cursor--].value;
        }

        public int Min()
        {
            return soArray[cursor].min_value;
        }

        public void PrintStack()
        {
            Console.WriteLine();
            Console.WriteLine("Current stack items:");
            for (int i = cursor; i >= 0; --i)
            {
                Console.WriteLine("Value: " + soArray[i].value + " Min Beneath: " + soArray[i].min_value);
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
        public int min_value;  // represents the lowest value on the stack at the time this object
                               // was added. This provides O(1) access to lowest value on the stack.

        public StackObject(int passed_value, int passed_min)
        {
            value = passed_value;
            min_value = passed_min;
        }
    }
}