//класс ключевых точек
class Target_point
{
    //массивы с координатами появления целевых точек
    private double[] coordinates1 = new double[2];
    private double[] coordinates2 = new double[2];
    private double[] coordinates3 = new double[2];

    //метод для получения значения масивов
    public double[] get(int i)
    {
        List<double[]> list_point = new List<double[]> { coordinates1, coordinates2, coordinates3 };
        return list_point[i - 1];
    }
    //метод для присваения полю(массивам) объекта соответсвующих значений
    public void set(double[] arr_point1, double[] arr_point2, double[] arr_point3)

    {
        coordinates1 = arr_point1;
        show(coordinates1, 1);

        coordinates2 = arr_point2;
        show(coordinates2, 2);

        coordinates3 = arr_point3;
        show(coordinates3, 3);
    }
    //метод ля вывода значений массива
    private protected virtual void show(double[] mass, int k)
    {
        Console.WriteLine($"Целевая точка №{k}");
        foreach (double i in mass)
        {
            Console.Write($" {i}");
        }
        Console.WriteLine(" ");
    }
    //конструктор для инициализации целевых точек и их координат 
    public Target_point(double[] arr_point1, double[] arr_point2, double[] arr_point3)
    {
        set(arr_point1, arr_point2, arr_point3);
    }
}
//класс препятствий
class Obstacles : Target_point
{
    //метод для вывода значений массива
    private protected override void show(double[] mass, int k)
    {
        Console.WriteLine($"Местоположение препятствия №{k}");
        foreach (double i in mass)
        {
            Console.Write($" {i}");
        }
        Console.WriteLine("");
    }
    public Obstacles(double[] arr_point1, double[] arr_point2, double[] arr_point3) : base(arr_point1, arr_point2, arr_point3)
    { }
}
//класс инициализации агента
class Agent

{
    private double[] coordinates = new double[2];
    public void set(double[] arr_point1)
    {
        coordinates = arr_point1;
        show(coordinates);
    }
    public double[] get()
    {
        return coordinates;
    }
    private protected virtual void show(double[] mass)
    {
        Console.WriteLine($"Местоположение агента");
        foreach (double i in mass)
        {
            Console.Write($" {i}");
        }
        Console.WriteLine("");
    }
    public Agent(double[] arr_point1)
    {
        set(arr_point1);
    }
}
//класс инициализации ключевых точек
class Final_point : Agent
{
    private protected override void show(double[] mass)
    {
        Console.WriteLine($"Местоположение финальной точки");
        foreach (double i in mass)
        {
            Console.Write($" {i}");
        }
        Console.WriteLine("");
    }
    public Final_point(double[] arr_point1) : base(arr_point1)
    { }
}
//класс поле в котором происходит распределения местоположения всех объектов
class Field
{
    private int[] Array_point = new int[8];
    private Dictionary<int, double[]> Matrix = new Dictionary<int, double[]>()
    {
        { 0, new double[]{1, 1} },
        { 1, new double[]{2, 1} },
        { 2, new double[]{3, 1} },
        { 3, new double[]{4, 1} },

        { 4, new double[]{1, 2} },
        { 5, new double[]{2, 2} },
        { 6, new double[]{3, 2} },
        { 7, new double[]{4, 2} },

        { 8, new double[]{1, 3} },
        { 9, new double[]{2, 3} },
        { 10, new double[]{3, 3} },
        { 11, new double[]{4, 3} },

        { 12, new double[]{1, 4} },
        { 13, new double[]{2, 4} },
        { 14, new double[]{3, 4} },
        { 15, new double[]{4, 4} }
    };
    private int[] Creat_Mass()
    {
        Dictionary<int, int> cheked_dictonary = new Dictionary<int, int>();
        int[] mass = new int[8];
        Random rand = new Random();
        for (int i = 0; i < 8; i++)
        {
            mass[i] = rand.Next(0, 16);
            if (cheked_dictonary.ContainsValue(mass[i]))
            {
                i--;
            }
            else
            {
                cheked_dictonary.Add(i + 1, mass[i]);
            }
        }
        return mass;
    }
    public double[] get(int i)
    {
        return Matrix[Array_point[i]];
    }
    public void show()
    {
        Console.WriteLine("Массив расположений:");
        foreach (var value in Array_point)
        {
            Console.Write($"  {value}");
        }
        Console.WriteLine();
    }
    public Field()
    {
        Array_point = Creat_Mass();
        show();
    }
}
//класс в котором реализуется весь алгоритм поиска пути
class Algoritm_A
{
    private double Calculation_heuristics(double[] trpt_1, double[] trpt_2, double[] trpt_3, double[] agent, bool f1, bool f2, bool f3, double[] fnipt)
    {
        double h_result;
        double h1 = 0;
        double h2 = 0;
        double h3 = 0;
        if (f1 == false)
            h1 = Dop_calculation(trpt_1, agent) + Dop_calculation(fnipt, trpt_1);
        if (f2 == false)
            h2 = Dop_calculation(trpt_2, agent) + Dop_calculation(fnipt, trpt_2);
        if (f3 == false)
            h3 = Dop_calculation(trpt_3, agent) + Dop_calculation(fnipt, trpt_3);
        h_result = h1 + h2 + h3 - Dop_calculation(fnipt, agent); ;
        return h_result;
    }
    private double Calculation_heuristics(double[] fnipt, double[] agent)
    {
        return Dop_calculation(fnipt, agent);
    }
    private double Dop_calculation(double[] trpt_1, double[] agent)
    {
        return (Math.Abs(trpt_1[0] - agent[0]) + Math.Abs(trpt_1[1] - agent[1]));
    }
    private bool Сheck(double[] mass)
    {
        if ((mass[0] < 5 & mass[0] > 0) & (mass[1] < 5 & mass[1] > 0))
            return true;
        else
            return false;
    }
    private bool Checke(double[] obpt_1, double[] obpt_2, double[] obpt_3, double[] agent)
    {
        bool f = true;
        List<double[]> list_obpt = new List<double[]>() { obpt_1, obpt_2, obpt_3 };
        foreach (var mass in list_obpt)
        {
            if (agent[0] == mass[0] && agent[1] == mass[1])
            {
                f = false;
                break;
            }
        }
        return f;
    }
    private double[] Shift_agent(double[] agent, int i)
    {
        switch (i)
        {
            case 0:
                {
                    agent[1]++;
                    break;
                }
            case 1:
                {
                    agent[0]--;
                    break;
                }
            case 2:
                {
                    agent[1]--;
                    break;
                }
            case 3:
                {
                    agent[0]++;
                    break;
                }
        }
        return agent;
    }
    private double[] Shift_agent(double[] agent, int i, out string step)
    {
        step = "0";
        switch (i)
        {
            case 0:
                {
                    agent[1]++;
                    Console.WriteLine("Up");
                    step = "Up";
                    break;
                }
            case 1:
                {
                    agent[0]--;
                    Console.WriteLine("Right");
                    step = "Right";
                    break;
                }
            case 2:
                {
                    agent[1]--;
                    Console.WriteLine("Down");
                    step = "Down";
                    break;
                }
            case 3:
                {
                    agent[0]++;
                    Console.WriteLine("Left");
                    step = "Left";
                    break;
                }
        }
        return agent;
    }
    private Dictionary<int, double> Price_step(double[] obpt_1, double[] obpt_2, double[] obpt_3, double[] trpt_1, double[] trpt_2, double[] trpt_3, double[] agent, int count_step, bool f1, bool f2, bool f3, double[] fnipt)
    {
        Dictionary<int, double> Mass_step = new Dictionary<int, double>();
        count_step++;
        double f_t;
        for (int i = 0; i < 4; i++)
        {
            double[] mass_agent = new double[] { agent[0], agent[1] };
            bool[] mass_f = new bool[] { f1, f2, f3 };
            mass_agent = Shift_agent(mass_agent, i);
            if (Сheck(mass_agent) & Checke(obpt_1, obpt_2, obpt_3, mass_agent))
            {
                mass_f = Reaching_trpt(trpt_1, trpt_2, trpt_3, mass_agent, mass_f);
                f_t = count_step + Calculation_heuristics(trpt_1, trpt_2, trpt_3, mass_agent, mass_f[0], mass_f[1], mass_f[2], fnipt);
                Mass_step.Add(i, f_t);
            }
        }
        return Mass_step;
    }
    private int Min_f(Dictionary<int, double> Mass_step)
    {
        if (Mass_step.Count() == 0)
        {
            return -1;
        }
        int[] mass_key = new int[Mass_step.Count()];
        int j = 0;
        for (int i = 0; i < 4; i++)
        {
            if (Mass_step.ContainsKey(i))
            {
                mass_key[j] = i;
                j++;
            }
        }
        double min_f = Mass_step[mass_key[0]];
        int key_min_f = mass_key[0];
        //int count_similar = 0;
        foreach (var key in mass_key)
        {
            if (Mass_step[key] < min_f)
            {
                min_f = Mass_step[key];
                key_min_f = key;
            }
        }
        return key_min_f;
        /*List<int> key_similar=new List<int>();
        foreach (var key in mass_key)
        {
            if (Mass_step[key_min_f] == Mass_step[key])
            {
                count_similar++;
                key_similar.Add(key);
            }
        }
        if (count_similar > 1) 
        {

        }
        else 
        {

        }
        */
    }
    private bool[] Reaching_trpt(double[] trpt_1, double[] trpt_2, double[] trpt_3, double[] agent, bool[] mass_f)
    {
        if ((trpt_1[0] == agent[0] & trpt_1[1] == agent[1]) | mass_f[0] == true)
            mass_f[0] = true;
        else
            mass_f[0] = false;
        if ((trpt_2[0] == agent[0] & trpt_2[1] == agent[1]) | mass_f[1] == true)
            mass_f[1] = true;
        else
            mass_f[1] = false;
        if ((trpt_3[0] == agent[0] & trpt_3[1] == agent[1]) | mass_f[2] == true)
            mass_f[2] = true;
        else
            mass_f[2] = false;
        return mass_f;
    }
    private void show(List<string> result)
    {
        Console.WriteLine("Ходы чтобы пройти все целевые точки: ");
        foreach (var str in result)
        {
            Console.Write(" " + str);
        }
    }
    public Algoritm_A(double[] trpt_1, double[] trpt_2, double[] trpt_3, double[] obpt_1, double[] obpt_2, double[] obpt_3, double[] agent, double[] fnipt)
    {
        int count_step = 0;
        Dictionary<int, double> Possibel_step = new Dictionary<int, double>();
        List<string> Step_result = new List<string>();
        bool[] mass_f = new bool[] { false, false, false };
        bool F = true;
        string final_step;
        //ручное задание координат
        trpt_1[0] = 1; trpt_1[1] = 4;
        trpt_2[0] = 2; trpt_2[1] = 2;
        trpt_3[0] = 4; trpt_3[1] = 3;
        obpt_1[0] = 1; obpt_1[1] = 1;
        obpt_2[0] = 1; obpt_2[1] = 2;
        obpt_3[0] = 3; obpt_3[1] = 3;
        agent[0] = 2; agent[1] = 4;
        fnipt[0] = 1; fnipt[1] = 3;
        while (F)
        {
            Possibel_step = Price_step(obpt_1, obpt_2, obpt_3, trpt_1, trpt_2, trpt_3, agent, count_step, mass_f[0], mass_f[1], mass_f[2], fnipt);
            int key_min_f = Min_f(Possibel_step);
            if (key_min_f == -1)
            {
                Console.WriteLine("Хьюстон, у нас проблемы. Решение невозможно, агент007 в западне.");
                break;
            }
            agent = Shift_agent(agent, key_min_f,out final_step);
            mass_f = Reaching_trpt(trpt_1, trpt_2, trpt_3, agent, mass_f);
            Step_result.Add(final_step);
            count_step++;
            if (mass_f[0] & mass_f[1] & mass_f[2])
                F = false;
        }
        show(Step_result);
    }
}
class project
{
    static void Main()
    {
        Field field = new Field();
        Target_point target_coordinates = new Target_point(field.get(0), field.get(1), field.get(2));
        Obstacles obstacles_coordinates = new Obstacles(field.get(3), field.get(4), field.get(5));
        Agent agent = new Agent(field.get(6));
        Final_point final_point = new Final_point(field.get(7));
        Algoritm_A algoritm = new Algoritm_A(field.get(0), field.get(1), field.get(2), field.get(3), field.get(4), field.get(5), field.get(6), field.get(7));
    }
}
