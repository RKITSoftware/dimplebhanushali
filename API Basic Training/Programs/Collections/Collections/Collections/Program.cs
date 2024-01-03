using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            // Non-generic collections
            Console.WriteLine("Non-Generic Collections:");

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


            // Generic collections
            Console.WriteLine("Generic Collections:");

            // List
            List<int> list = new List<int> { 1, 2, 3, 4, 5 };

            Console.WriteLine("List:");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

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

            Console.ReadKey();

        }
    }
}
