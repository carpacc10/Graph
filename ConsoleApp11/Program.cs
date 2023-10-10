using System.ComponentModel;
//Add_edge(mass[i].Split(" : ")[0], a.Split(" ")[j].Split(",")[0], Convert.ToDouble(a.Split(" ")[j].Split(",")[1]));
Graf graf = new Graf();
graf.printq();


class Graf
{
    public Dictionary<string, List<(string, double)>> Adjacency = new();
    public bool orient = false;
    public bool weight = false;

    public Graf()
    {
        Adjacency = new Dictionary<string, List<(string, double)>>();
        orient = new bool();
        weight = new bool();
    }

    public Graf(string filePath)
    {
        string[] mass = File.ReadAllLines(filePath);
        for (int i = 0; i < mass.Length; i++)
        {
            Add_Vertex(mass[i].Split(" : ")[0]);
        }
        for (int i = 0; i < mass.Length; i++)
        {
            string a = mass[i].Split(" : ")[1];
            for (int j = 0; j < a.Split(" ").Count(); j++)
            {
                Adjacency[mass[i].Split(" : ")[0]].Add((a.Split(" ")[j].Split(",")[0], Convert.ToDouble(a.Split(" ")[j].Split(",")[1])));
            }
        }
    }

    public Graf(Graf garf)
    {
        Adjacency = new Dictionary<string, List<(string, double)>>(garf.Adjacency);
        foreach (var item in Adjacency.Keys) Adjacency[item] = new List<(string, double)>(garf.Adjacency[item]);
        orient = garf.orient;
        weight = garf.weight;
    }

    public void Check_wgNor()
    {

        foreach (var v in Adjacency)
        {
            foreach (var v2 in v.Value)
            {

                if (!weight && ((v2.Item2 != 1.0) && (v2.Item2 != -1.0)))
                {
                    weight = true;
                }
                if (!orient && (v2.Item2 < 0))
                {
                    orient = true;
                }
            }
        }
    }
    public bool Check_Ver(string ver_name)
    {
        if (Adjacency.Keys.Contains(ver_name)) return true;
        else return false;
    }
    public void Add_Vertex(string ver_name)
    {
        Adjacency.Add(ver_name, new List<(string, double)>());
    }
    public bool Check_Edge(string ver_name1, string ver_name2)
    {
        if (!Adjacency.Keys.Contains(ver_name1) || !Adjacency.Keys.Contains(ver_name2)) return true;
        else return false;
    }
    public void Add_edge(string ver_name1, string ver_name2, double weights = 1)
    {

        if (ver_name1 == ver_name2 || orient)
        {
            Adjacency[ver_name1].Add((ver_name1, weights));
        }
        else
        {
            Adjacency[ver_name1].Add((ver_name2, weights));
            Adjacency[ver_name2].Add((ver_name1, weights));
        }

    }
    public void Remove_Vertex(string ver_name)
    {

        Adjacency.Remove(ver_name);
        foreach (var v in Adjacency)
        {
            foreach (var v2 in v.Value)
            {
                if (v2.Item1 == ver_name)
                {
                    v.Value.Remove((ver_name, v2.Item2)); break;
                }
            }
        }

    }
    public void Remove_Edge(string ver_name1, string ver_name2)
    {
        if (ver_name1 == ver_name2)
        {
            foreach (var v in Adjacency[ver_name1]) if (v.Item1 == ver_name2) Adjacency[ver_name1].Remove((v.Item1, v.Item2));

        }
        else
        {
            foreach (var v in Adjacency[ver_name1]) if (v.Item1 == ver_name2) Adjacency[ver_name1].Remove((v.Item1, v.Item2));
            foreach (var v in Adjacency[ver_name2]) if (v.Item1 == ver_name1) Adjacency[ver_name2].Remove((v.Item1, v.Item2));
        }
    }


    public void Adjacency_List(string path)
    {
        using (StreamWriter wr = new StreamWriter(path))
        {
            foreach (var v in Adjacency)
            {
                wr.Write(v.Key.ToString() + " :");
                foreach (var v2 in v.Value) wr.Write(" " + (v2.Item1) + "," + (v2.Item2).ToString());
                wr.WriteLine();
            }
        }
    }


    public void Adjacency_List_print()
    {
        foreach (var v in Adjacency)
        {
            Console.Write(v.Key.ToString() + " :");
            foreach (var v2 in v.Value) Console.Write(" " + (v2.Item1) + "," + (v2.Item2).ToString());
            Console.WriteLine();
        }
    }

    public void printq()
    {
        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Добавить вершину");
            Console.WriteLine("2. Добавить ребро");
            Console.WriteLine("3. Удалить вершину");
            Console.WriteLine("4. Удалить ребро");
            Console.WriteLine("5. Сохранить список в файл");
            Console.WriteLine("6. Вывести список");

            Console.WriteLine("7. Выход");

            int choice = int.Parse(Console.ReadLine());
            string vertex, vertex2;
            double wg;
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Введите значение вершины:");
                    vertex = Console.ReadLine();
                    if (!Check_Ver(vertex))
                    {
                        Add_Vertex(vertex);
                    }
                    else Console.WriteLine("Такая вершина уже существует");
                    break;
                case 2:
                    Console.WriteLine("Введите исходную вершину и конечную вершину:");
                    vertex = Console.ReadLine();
                    vertex2 = Console.ReadLine();
                    wg = int.Parse(Console.ReadLine());
                    if (!Check_Edge(vertex, vertex2))
                    {
                        Add_edge(vertex, vertex2);
                    }
                    else Console.WriteLine("Такое ребро невозможно построить");
                    break;
                case 3:
                    Console.WriteLine("Введите вершину, которую нужно удалить:");
                    vertex = Console.ReadLine();
                    if (Check_Ver(vertex))
                    {
                        Remove_Vertex(vertex);
                    }
                    else Console.WriteLine("Такой вершины не существует");
                    break;
                case 4:
                    Console.WriteLine("Введите исходную и конечную вершину для удаления:");
                    vertex = Console.ReadLine();
                    vertex2 = Console.ReadLine();
                    wg = int.Parse(Console.ReadLine());
                    if (!Check_Edge(vertex, vertex2))
                    {
                        Remove_Edge(vertex, vertex2);
                    }
                    else Console.WriteLine("Такого ребра не существует");
                    break;
                case 5:
                    Console.WriteLine("Введите путь к файлу, чтобы сохранить список смежности:");
                    Adjacency_List(Console.ReadLine());
                    break;

                case 6:
                    Adjacency_List_print();
                    Check_wgNor();
                    Console.WriteLine("Вес: " + weight + " " + "Ориентация: " + orient);
                    break;
                case 7:

                    return;
                default:
                    Console.WriteLine("Неверная команда. Попробуйте снова");

                    break;
            }
        }
    }
}

