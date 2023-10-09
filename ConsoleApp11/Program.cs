Graf graf = new Graf();
graf.printq();


class Graf
{
    public Dictionary<string, List<string>> Adjacency = new Dictionary<string, List<string>>();
    public bool orient;
    public bool weight;

    public Graf() { }

    public Graf(string filePath)
    {
        try
        {
            
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    
                    string[] parts = line.Split(':');
                    if (parts.Length != 2)
                    {
                        throw new FormatException("Неправильный формат строки в файле.");
                    }

                    string vertex = parts[0].Trim();
                    string[] neighbors = parts[1].Split(',').Select(n => n.Trim()).ToArray();

                    
                    Adjacency[vertex] = neighbors.ToList();
                }
            }
        }
        catch (Exception ex)
        {
            
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }




    public Graf(Graf garf)
    {
        Adjacency = new Dictionary<string, List<string>>(garf.Adjacency);
        foreach (var item in Adjacency.Keys) Adjacency[item] = new List<string>(garf.Adjacency[item]);
        orient = garf.orient;
        weight = garf.weight;
    }

    public void Add_Vertex(string ver_name)
    {
        bool check = false;
        foreach (var v in Adjacency)
        {
            if (v.Key.Equals(ver_name))
            {
                check = true;
            }
        }
        if (!check)
        {
            Adjacency.Add(ver_name, new List<string>());
        }
        else
        {
            Console.WriteLine("Такая вершина уже есть");
        }
    }
    public void Add_edge(string ver_name1, string ver_name2)
    {
        bool check = false;
        if (!Adjacency.Keys.Contains(ver_name1) || !Adjacency.Keys.Contains(ver_name2)) check = true;


        if (!check)
        {
            if (ver_name1 == ver_name2)
            {
                Adjacency[ver_name1].Add(ver_name1);
            }
            else
            {
                Adjacency[ver_name1].Add(ver_name2);
                Adjacency[ver_name2].Add(ver_name1);
            }
        }
        else
        {
            Console.WriteLine("Такое ребро невозможно построить");
        }
    }
    public void Remove_Vertex(string ver_name)
    {
        bool check = false;

        if (Adjacency.Keys.Contains(ver_name)) check = true;
        if (check)
        {
            Adjacency.Remove(ver_name);
            foreach (var v in Adjacency)
            {
                foreach (var v2 in v.Value)
                {
                    if (v2 == ver_name)
                    {
                        v.Value.Remove(ver_name); break;
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Такой вершины не существует");
        }
    }

    public void Remove_Edge(string ver_name1, string ver_name2)
    {
        bool check = false;
        if (!Adjacency.Keys.Contains(ver_name1) || !Adjacency.Keys.Contains(ver_name2)) check = true;
        if (!check)
        {
            if (ver_name1 == ver_name2)
            {
                Adjacency[ver_name1].Remove(ver_name1);
            }
            else
            {
                Adjacency[ver_name1].Remove(ver_name2);
                Adjacency[ver_name2].Remove(ver_name1);
            }

        }
        else Console.WriteLine("Такого ребра не существует");
    }


    public void Adjacency_List(string path)
    {
        using (StreamWriter wr = new StreamWriter(path))
        {
            foreach (var v in Adjacency)
            {
                wr.Write(v.Key.ToString() + " : ");
                foreach (var v2 in v.Value) wr.Write(v2.ToString() + " ");
                wr.WriteLine();
            }
        }
    }


    public void print()
    {
        foreach (var v in Adjacency)
        {
            Console.Write(v.Key.ToString() + " : ");
            foreach (var v2 in v.Value) Console.Write(v2.ToString());
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

            switch (choice)
            {
                case 1:
                    //Console.Clear();
                    Console.WriteLine("Введите значение вершины:");
                    Add_Vertex(Console.ReadLine());
                    break;
                case 2:
                    Console.WriteLine("Введите исходную вершину и конечную вершину:");
                    Add_edge(Console.ReadLine(), Console.ReadLine());
                    break;
                case 3:
                    Console.WriteLine("Введите вершину, которую нужно удалить:");
                    Remove_Vertex(Console.ReadLine());
                    break;
                case 4:
                    Console.WriteLine("Введите исходную и конечную вершину для удаления:");
                    Remove_Edge(Console.ReadLine(), Console.ReadLine());
                    break;
                case 5:
                    Console.WriteLine("Введите путь к файлу, чтобы сохранить список смежности:");
                    Adjacency_List(Console.ReadLine());
                    break;

                case 6:
                    print();
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

