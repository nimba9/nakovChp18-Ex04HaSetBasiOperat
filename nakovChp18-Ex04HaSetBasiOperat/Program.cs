using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

class DictHashSet<T> : IEnumerable<T>
{
    private Dictionary<T, T> container;
    public int Count
    {
        get
        {
            return container.Count;
        }
    }

    public DictHashSet()
    {
        this.container = new Dictionary<T, T>();
    }

    public bool Add(T element)
    {
        bool containsElement = this.Contains(element);
        if (containsElement)
        {
            return false;
        }
        else
        {
            container.Add(element, element);
            return true;
        }
    }

    public bool Contains(T element)
    {
        T value;
        if (container.TryGetValue(element, out value))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Remove(T element)
    {
        bool isElementRemoved = container.Remove(element);
        return isElementRemoved;
    }

    public void Clear()
    {
        this.container.Clear();
    }

    public void Union(DictHashSet<T> other)
    {
        if (other == null)
        {
            throw new ArgumentNullException();
        }

        foreach (var item in other)
        {
            this.Add(item);
        }
    }

    public void Intersect(DictHashSet<T> otherSet)
    {
        if (otherSet == null)
        {
            throw new ArgumentNullException();
        }

        DictHashSet<T> intersection = new DictHashSet<T>();
        foreach (var item in otherSet)
        {
            if (this.Contains(item))
            {
                intersection.Add(item);
            }
        }

        this.container = intersection.container;
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in this.container)
        {
            yield return item.Key;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

public class CommandExecuter
{
    static StringBuilder output = new StringBuilder();
    public static void Main(string[] args)
    {
        DictHashSet<int> set = new DictHashSet<int>();
        ExecuteCommands(set);
        Console.WriteLine(output);
    }

    private static void ExecuteCommands(DictHashSet<int> set)
    {
        string commandsCountInput = Console.ReadLine();
        int commandsCount = int.Parse(commandsCountInput);
        for (int i = 0; i < commandsCount; i++)
        {
            string command = Console.ReadLine();
            ExecuteCommands(command, set);
        }
    }

    private static void ExecuteCommands(string command, DictHashSet<int> set)
    {
        string[] tokens = command.Split(' ');
        string commandAction = tokens[0];
        switch (commandAction)
        {
            case "add":
                ExecuteAddCommand(tokens, set);
                break;
            case "count":
                ExecuteCountCommand(set);
                break;
            case "remove":
                ExecuteRemoveCommand(tokens, set);
                break;
            case "contains":
                ExecuteContainsCommand(tokens, set);
                break;
            case "clear":
                ExecuteClearCommand(set);
                break;
            case "union":
                ExecuteUnionCommand(tokens, set);
                break;
            case "intersect":
                ExecuteIntersectCommand(tokens, set);
                break;
            case "elements":
                ExecuteElementsCommand(set);
                break;
        }
    }

    private static void PrintLine(string line)
    {
        if (line.Equals(String.Empty))
        {
            return;
        }

        output.AppendLine(line);
    }

    private static void ExecuteAddCommand(string[] tokens, DictHashSet<int> set)
    {
        int newElement = int.Parse(tokens[1]);
        bool isCommandSuccessful = set.Add(newElement);
        if (!isCommandSuccessful)
        {
            PrintLine(isCommandSuccessful.ToString());
        }
    }

    private static void ExecuteCountCommand(DictHashSet<int> set)
    {
        int count = set.Count;
        PrintLine(count.ToString());
    }

    private static void ExecuteRemoveCommand(string[] tokens, DictHashSet<int> set)
    {
        int element = int.Parse(tokens[1]);
        bool isRemoved = set.Remove(element);
        if (!isRemoved)
        {
            PrintLine(isRemoved.ToString());
        }
    }

    private static void ExecuteContainsCommand(string[] tokens, DictHashSet<int> set)
    {
        int element = int.Parse(tokens[1]);
        bool contains = set.Contains(element);
        PrintLine(contains.ToString());
    }

    private static void ExecuteClearCommand(DictHashSet<int> set)
    {
        set.Clear();
    }

    private static void ExecuteUnionCommand(string[] tokens, DictHashSet<int> set)
    {
        DictHashSet<int> unionSet = new DictHashSet<int>();
        for (int i = 1; i < tokens.Length; i++)
        {
            int otherSetElement = int.Parse(tokens[i]);
            unionSet.Add(otherSetElement);
        }

        set.Union(unionSet);
    }

    private static void ExecuteIntersectCommand(string[] tokens, DictHashSet<int> set)
    {
        DictHashSet<int> intersectionSet = new DictHashSet<int>();
        for (int i = 1; i < tokens.Length; i++)
        {
            int otherSetElement = int.Parse(tokens[i]);
            intersectionSet.Add(otherSetElement);
        }

        set.Intersect(intersectionSet);
    }

    private static void ExecuteElementsCommand(DictHashSet<int> set)
    {
        SortedSet<int> sortedSet = new SortedSet<int>();
        foreach (var item in set)
        {
            sortedSet.Add(item);
        }

        StringBuilder buffer = new StringBuilder();
        foreach (var item in sortedSet)
        {
            buffer.AppendFormat("{0} ", item);
        }

        PrintLine(buffer.ToString());
    }
}