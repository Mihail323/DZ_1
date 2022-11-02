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
        set(arr_point1,arr_point2,arr_point3);
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
    public Obstacles(double[] arr_point1, double[] arr_point2, double[] arr_point3): base(arr_point1, arr_point2, arr_point3)
    { }
}
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
class Final_point :Agent
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
    public Final_point(double[] arr_point1):base(arr_point1)
    {}
}
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
        Array_point=Creat_Mass();
        show();
    }
}
class Algoritm_A
{
    private double Calculation_heuristics(double[] trpt_1, double[] trpt_2, double[] trpt_3, double[] agent, bool f1, bool f2, bool f3)
    {
        double h_result = 0;
        double h1 = 0;
        double h2 = 0;
        double h3 = 0;
        if (f1 == false)
            h1 = Dop_calculation(trpt_1, agent);
        if (f2 == false)
            h2 = Dop_calculation(trpt_2, agent);
        if (f3 == false)
            h3 = Dop_calculation(trpt_3, agent);
        h_result = h1 + h2 + h3;
        return h_result;
    }
    private double Calculation_heuristics(double[] fnipt, double[] agent)
    {
        double h = Dop_calculation(fnipt, agent); ;
        return h;
    }
    private double Dop_calculation(double[] trpt_1, double[] agent)
    {
        double h = 0;
        for (int i = 0; i < 2; i++)
        {
            double result = Math.Abs(trpt_1[i] - agent[i]);
            h += result;
        }
        return h;
    }
    private bool Сheck(double[] mass)
    {
        if ((mass[0] < 5 & mass[0] > -1) & (mass[1] < 5 & mass[1] > -1))
            return true;
        else
            return false;
    }
    private bool Checke(double[] obpt_1, double[] obpt_2, double[] obpt_3, double[] agent)
    {
        bool f = true;
        List<double[]> list_obpt= new List<double[]>() { obpt_1, obpt_2, obpt_3};
        foreach (var mass in list_obpt)
        {
            for (int i = 0; i < 3; i++)
            {
                if (agent[0] == mass[0] && agent[1] == mass[1])
                {
                    f = false;
                    break;
                }
            }
        }
        return f;
    }
    private Dictionary<int, double> Price_step(double[] obpt_1, double[] obpt_2, double[] obpt_3, double[] trpt_1, double[] trpt_2, double[] trpt_3, double[] agent, int count_step, bool f1, bool f2, bool f3)
    {
        Dictionary<int, double> Mass_step= new Dictionary<int, double>();
        count_step++;
        double f_t;
        for (int i = 0; i < 4; i++)
        {
            double[] mass = new double[] { agent[0], agent[1] };
            switch (i)
            {
                case 0:
                   {
                        if(Сheck(mass) & Checke(obpt_1, obpt_2, obpt_3, mass))
                        {
                            mass[1]++;
                            f_t = count_step + Calculation_heuristics(trpt_1, trpt_2, trpt_3, mass,f1,f2,f3);
                            Mass_step.Add(i, f_t);
                        }
                        break;
                   }
                case 1:
                    {
                        if (Сheck(mass) & Checke(obpt_1, obpt_2, obpt_3, mass))
                        {
                            mass[0]--;
                            f_t = count_step + Calculation_heuristics(trpt_1, trpt_2, trpt_3, mass, f1, f2, f3);
                            Mass_step.Add(i, f_t);
                        }
                        break;
                    }
                case 2:
                    {
                        if (Сheck(mass) & Checke(obpt_1, obpt_2, obpt_3, mass))
                        {
                            mass[1]--;
                            f_t = count_step + Calculation_heuristics(trpt_1, trpt_2, trpt_3, mass, f1, f2, f3);
                            Mass_step.Add(i, f_t);
                        }
                        break;
                    }
                case 3:
                    {
                        if (Сheck(mass) & Checke(obpt_1, obpt_2, obpt_3, mass))
                        {
                            mass[0]++;
                            f_t = count_step + Calculation_heuristics(trpt_1, trpt_2, trpt_3, mass, f1, f2, f3);
                            Mass_step.Add(i, f_t);
                        }
                        break;
                    }
            }
        }
        return Mass_step;
    }
    private int  Min_f(Dictionary<int, double> Mass_step)
    {
        double min_f = Mass_step[0];
        int key=0;
        for(int i=1; i<Mass_step.Count(); i++)
        {
            if (Mass_step[i]<min_f)
            {
                min_f = Mass_step[i];
                key = i;
            }
        }
        return key;
    }
    private void Reaching_trpt(double[] trpt_1, double[] trpt_2, double[] trpt_3, double[] agent,out bool f1, out bool f2, out bool f3)
    {
        if (trpt_1[0] == agent[0] & trpt_1[1] == agent[1])
            f1 = true;
        else
            f1 = false;
        if (trpt_2[0] == agent[0] & trpt_2[1] == agent[1])
            f2 = true;
        else
            f2 = false;
        if (trpt_3[0] == agent[0] & trpt_3[1] == agent[1])
            f3 = true;
        else
            f3 = false;
    }
    private void show(List<string> result) 
    {
        Console.WriteLine("Ходы чтобы пройти все целевые точки: ");
        foreach(var str in result)
        {
            Console.Write(" " + str);
        }
    }
    public Algoritm_A(double[] trpt_1, double[] trpt_2, double[] trpt_3, double[] obpt_1, double[] obpt_2, double[] obpt_3, double[] agent, double[] fnipt)
    {
        int count_step = 0;
        Dictionary<int, double> Possibel_step = new Dictionary<int, double>();
        List<string> Step_result = new List<string>();
        bool f1=false, f2=false, f3 = false;
        bool F = true;
        while (F)
        {
            Possibel_step = Price_step(obpt_1, obpt_2, obpt_3, trpt_1, trpt_2, trpt_3, agent, count_step,f1,f2,f3);
            int key_min_f=Min_f(Possibel_step);
            switch(key_min_f)
            {
                case 0:
                    {
                        agent[1]++;
                        Reaching_trpt(trpt_1, trpt_2, trpt_3, agent, out f1, out f2, out f3);
                        Step_result.Add("Up");
                        count_step++;
                        break;
                    }
                case 1:
                    {
                        agent[0]--;
                        Reaching_trpt(trpt_1, trpt_2, trpt_3, agent, out f1, out f2, out f3);
                        Step_result.Add("Right");
                        count_step++;
                        break;
                    }
                case 2:
                    {
                        agent[1]--;
                        Reaching_trpt(trpt_1, trpt_2, trpt_3, agent, out f1, out f2, out f3);
                        Step_result.Add("Down");
                        count_step++;
                        break;
                    }
                case 3:
                    {
                        agent[0]++;
                        Reaching_trpt(trpt_1, trpt_2, trpt_3, agent, out f1, out f2, out f3);
                        Step_result.Add("Left");
                        count_step++;
                        break;
                    }
            }
            if (f1 & f2 & f3)
                F = true;
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
