using System;
using System.Collections;
using System.Collections.Generic;

namespace Collections
{
    /// <summary>
    /// A program demonstrating non-generic and generic collections in C#.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            #region Non Generic Collection

            // Non-generic collections
            Console.WriteLine("Non-Generic Collections:");

            #region ArrayList
            // ArrayList
            ArrayList arrayList = new ArrayList();
            arrayList.Add(1);
            arrayList.Add("Hello");
            arrayList.Add(3.14);

            Console.WriteLine("ArrayList:");
            foreach (var item in arrayList)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            #endregion

            #region HashTable
            // Hashtable
            Hashtable hashtable = new Hashtable();
            hashtable.Add("key1", "value1");
            hashtable.Add("key2", 42);
            hashtable.Add(3, "value3");

            Console.WriteLine("Hashtable:");
            foreach (DictionaryEntry entry in hashtable)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
            Console.WriteLine();
            #endregion

            #region Sorted List

            // SortedList
            SortedList sortedList = new SortedList();
            sortedList.Add("key3", 30);
            sortedList.Add("key1", 10);
            sortedList.Add("key2", 20);

            Console.WriteLine("SortedList:");
            foreach (DictionaryEntry entry in sortedList)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
            Console.WriteLine();

            #endregion

            #region Stack
            // Stack
            Stack stack = new Stack();
            stack.Push("Top");
            stack.Push(42);
            stack.Push(false);

            Console.WriteLine("Stack:");
            while (stack.Count > 0)
            {
                Console.WriteLine(stack.Pop());
            }
            Console.WriteLine();
            #endregion

            #region Queue
            // Queue
            Queue queue = new Queue();
            queue.Enqueue("First");
            queue.Enqueue(2);
            queue.Enqueue(true);

            Console.WriteLine("Queue:");
            while (queue.Count > 0)
            {
                Console.WriteLine(queue.Dequeue());
            }
            Console.WriteLine();
            #endregion

            #endregion

            #region Generic Collections
            // Generic collections
            Console.WriteLine("Generic Collections:");

            #region List
            // List
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            Console.WriteLine("List:");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            #endregion

            #region Dictionary
            // Dictionary
            Dictionary<string, int> dictionary = new Dictionary<string, int>
            {
                { "One", 1 },
                { "Two", 2 },
                { "Three", 3 }
            };

            Console.WriteLine("Dictionary:");
            foreach (var kvp in dictionary)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
            Console.WriteLine();
            #endregion

            #region Sorted List

            // SortedList (Generic)
            SortedList<string, int> genericSortedList = new SortedList<string, int>
            {
                { "key3", 30 },
                { "key1", 10 },
                { "key2", 20 }
            };


            Console.WriteLine("Generic SortedList:");
            foreach (var kvp in genericSortedList)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
            Console.WriteLine();

            #endregion

            #region Stack
            // Stack (Generic)
            Stack<string> genericStack = new Stack<string>();
            genericStack.Push("Top");
            genericStack.Push("Middle");
            genericStack.Push("Bottom");

            Console.WriteLine("Generic Stack:");
            while (genericStack.Count > 0)
            {
                Console.WriteLine(genericStack.Pop());
            }
            Console.WriteLine();

            #endregion

            #region Queue
            // Queue (Generic)
            Queue<bool> genericQueue = new Queue<bool>();
            genericQueue.Enqueue(true);
            genericQueue.Enqueue(false);
            genericQueue.Enqueue(true);

            Console.WriteLine("Generic Queue:");
            while (genericQueue.Count > 0)
            {
                Console.WriteLine(genericQueue.Dequeue());
            }

            #endregion

            #endregion

            // Wait for a key press before closing the console window
            Console.ReadKey();
        }
    }
}
