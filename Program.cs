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
    //словарь в котором каждому номеру соответвует  массив координаты(каждый номер обозначает определённую клетку на поле в соответствии с рисунком)
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
    //метод к котором создаётся массив различных чисел. Каждое число соответствует номеру клетки в последующем каждое число отойдёт агенту, целевым точкам и т.д.
    //тем самым мы рандомно разместим все объекты на поле 
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
    //метод для подсчёта эвристики в зависимости от того проёдена целевая точка или нет и сколько такихцелевых точек пройденно
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
        if (f1 & f2 | f1 & f3 | f2 & f3)
            h_result = h1 + h2 + h3;
        else
            h_result = h1 + h2 + h3 - Dop_calculation(fnipt, agent);//здесь вычитаем растояние между агентом и финальной точкой чтобы он старался вначале пройти по дальним точкам.
        return h_result;
    }
    //также для расчёты эвристики но уже для движения до финальной точки
    private double Calculation_heuristics(double[] fnipt, double[] agent)
    {
        return Dop_calculation(fnipt, agent);
    }
    // сама функция подсчета манхетновского расстояния
    private double Dop_calculation(double[] trpt_1, double[] agent)
    {
        return (Math.Abs(trpt_1[0] - agent[0]) + Math.Abs(trpt_1[1] - agent[1]));
    }
    //метод проверки того, что агент не вышел за границы поля
    private bool Сheck(double[] mass)
    {
        if ((mass[0] < 5 & mass[0] > 0) & (mass[1] < 5 & mass[1] > 0))
            return true;
        else
            return false;
    }
    //метод проверки того что агент не врезался в препятствие
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
    //метод сдвига агента по полю в соответсвие с выбранным направление(используется в методах для предварительного расчета куда можно сходить)
    //за направление отвечает i, 0-вверх, 1-вправо, 2-вниз, 3-влево
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
    //метод сдвига агента по полю в соответсвие с выбранным направление, но с дополнением того что вывводится шаг агента(используется для уже конкретного шага)
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
    //ввыводит массив ключей словаря
    private int[] Array_key(Dictionary<int, double> Dictonary)
    {
        int[] mass_key = new int[Dictonary.Count()];
        int j = 0;
        for (int i = 0; i < 4; i++)
        {
            if (Dictonary.ContainsKey(i))
            {
                mass_key[j] = i;
                j++;
            }
        }
        return mass_key;
    }
    //метод расчета стоимости шага во всех четырёх направлениях если это конечно возможно
    private Dictionary<int, double> Price_step(double[] obpt_1, double[] obpt_2, double[] obpt_3, double[] trpt_1, double[] trpt_2, double[] trpt_3, double[] agent, int count_step, bool f1, bool f2, bool f3, double[] fnipt, Dictionary<int, bool> Anti_cycle)
    {
        Dictionary<int, double> Mass_step = new Dictionary<int, double>();
        count_step++;
        double f_t;
        for (int i = 0; i < 4; i++)
        {
            double[] mass_agent = new double[] { agent[0], agent[1] };
            bool[] mass_f = new bool[] { f1, f2, f3 };
            mass_agent = Shift_agent(mass_agent, i);
            if (Сheck(mass_agent) & Checke(obpt_1, obpt_2, obpt_3, mass_agent) & Anti_cycle[i])
            {
                mass_f = Reaching_trpt(trpt_1, trpt_2, trpt_3, mass_agent, mass_f);
                f_t = count_step + Calculation_heuristics(trpt_1, trpt_2, trpt_3, mass_agent, mass_f[0], mass_f[1], mass_f[2], fnipt);
                Mass_step.Add(i, f_t);
            }
        }
        return Mass_step;
    }
    //метод выбора из словаря возможных шагов, минимального шага
    private Dictionary<int, double> Min_f(Dictionary<int, double> Mass_step, out int min_step)
    {
        int[] mass_key = Array_key(Mass_step);
        double min_f = Mass_step[mass_key[0]];
        min_step = mass_key[0];
        foreach (var key in mass_key)
        {
            if (Mass_step[key] < min_f)
            {
                min_f = Mass_step[key];
                min_step = key;
            }
        }
        foreach (var key in mass_key)
        {
            if (Mass_step[min_step] != Mass_step[key])
            {
                Mass_step.Remove(key);
            }
        }
        return Mass_step;
    }
    //похожий метод расчета стоимости шагов агента, но уже до конкретной  целевой точки.
    private List<double[]> Price_step(double[] obpt_1, double[] obpt_2, double[] obpt_3, double[] trpt, double[] agent, int count_step, bool f1, bool f2, bool f3, double[] fnipt, Dictionary<int, bool> Anti_cycle, double[] trpt_1, double[] trpt_2, double[] trpt_3)
    {
        List<double[]> Mass_step = new List<double[]>();
        count_step++;
        double h_t = 0;
        for (int i = 0; i < 4; i++)
        {
            double[] mass_agent = new double[] { agent[0], agent[1] };
            bool[] mass_f = new bool[] { f1, f2, f3 };
            mass_agent = Shift_agent(mass_agent, i);
            if (Сheck(mass_agent) & Checke(obpt_1, obpt_2, obpt_3, mass_agent) & Anti_cycle[i])
            {
                mass_f = Reaching_trpt(trpt_1, trpt_2, trpt_3, mass_agent, mass_f);
                if (mass_f[0] | mass_f[1] | mass_f[2])
                    h_t = count_step + Calculation_heuristics(trpt, mass_agent);
                else
                    h_t = count_step + Calculation_heuristics(trpt, mass_agent) - Calculation_heuristics(mass_agent, fnipt);
                Mass_step.Add(new double[] { i, h_t });
            }
        }
        return Mass_step;
    }
    //здесь в методе расчитываються стоимости шагов до каждой целевой точки, которая ешё не пройдена и выбирается шаг для которого эвристика наименьшая,
    //причем возможные шаги отбрасываються если они не совпадают с направлениями которые были выбраны как нимменьшие  в самом первом случае 
    private double Exit_the_loop(double[] obpt_1, double[] obpt_2, double[] obpt_3, double[] trpt_1, double[] trpt_2, double[] trpt_3, double[] agent, int count_step, bool[] mass_f, double[] fnipt, Dictionary<int, double> Filter_possibel_step, Dictionary<int, bool> Anti_cycle)
    {
        List<double[]> all_step_to_point = new List<double[]>();
        List<double[]> Mass_trpt = new List<double[]>() { trpt_1, trpt_2, trpt_3 };
        for (int i = 0; i < 3; i++)
        {
            if (mass_f[i] == false)
            {
                all_step_to_point.AddRange(Price_step(obpt_1, obpt_2, obpt_3, Mass_trpt[i], agent, count_step, mass_f[0], mass_f[1], mass_f[2], fnipt, Anti_cycle, trpt_1, trpt_2, trpt_3));
            }
        }
        double direct_step = 0;
        double min = 1000;
        foreach (var array in all_step_to_point)
        {
            if (min > array[1] & Filter_possibel_step.ContainsKey((int)array[0]))
            {
                min = array[1];
                direct_step = array[0];
            }
        }
        return direct_step;
    }
    //Метод, которые нужен для блокировки направления движения назад, то есть если агент сдвинулся вверх, то ему блокируется движение вниз ровно на один ход, и 
    //только в том случае если это ход назад не является единственным. Нужно это чтобы выходить циклов, когда он не может определиться куда идти и просто постоянно двигается вверх вниз или влево вправо.
    private Dictionary<int, bool> Block_last_step(double[] obpt_1, double[] obpt_2, double[] obpt_3, double[] trpt_1, double[] trpt_2, double[] trpt_3, double[] agent, int count_step, bool[] mass_f, double[]  fnipt, int direct_step)
    {
        Dictionary<int, bool> Anti_cycle = new Dictionary<int, bool>() { { 0, true }, { 1, true }, { 2, true }, { 3, true } };
        Dictionary<int, double> Possibel_step = new Dictionary<int, double>(); ;
        switch (direct_step)
        {
            case 0:
                {
                    Anti_cycle[2]=false;
                    Possibel_step = Price_step(obpt_1, obpt_2, obpt_3, trpt_1, trpt_2, trpt_3, agent, count_step, mass_f[0], mass_f[1], mass_f[2], fnipt, Anti_cycle);
                    if (Possibel_step.Count() == 0)
                        Anti_cycle[2] = true;
                    break;
                }
            case 1:
                {
                    Anti_cycle[3]= false;
                    Possibel_step = Price_step(obpt_1, obpt_2, obpt_3, trpt_1, trpt_2, trpt_3, agent, count_step, mass_f[0], mass_f[1], mass_f[2], fnipt, Anti_cycle);
                    if (Possibel_step.Count() == 0)
                        Anti_cycle[3] = true;
                    break;
                }
            case 2:
                {
                    Anti_cycle[0]= false;
                    Possibel_step = Price_step(obpt_1, obpt_2, obpt_3, trpt_1, trpt_2, trpt_3, agent, count_step, mass_f[0], mass_f[1], mass_f[2], fnipt, Anti_cycle);
                    if (Possibel_step.Count() == 0)
                        Anti_cycle[0] = true;
                    break;
                }
            case 3:
                {
                    Anti_cycle[1]= false;
                    Possibel_step = Price_step(obpt_1, obpt_2, obpt_3, trpt_1, trpt_2, trpt_3, agent, count_step, mass_f[0], mass_f[1], mass_f[2], fnipt, Anti_cycle);
                    if (Possibel_step.Count() == 0)
                        Anti_cycle[1] = true;
                    break;
                }
        }
        return Anti_cycle;
    }
    //точно такой же метод но для финальной точки
    private Dictionary<int, bool> Block_last_step(double[] obpt_1, double[] obpt_2, double[] obpt_3, double[] agent, int count_step, double[] fnipt, int direct_step)
    {
        Dictionary<int, bool> Anti_cycle = new Dictionary<int, bool>() { { 0, true }, { 1, true }, { 2, true }, { 3, true } };
        Dictionary<int, double> Possibel_step = new Dictionary<int, double>(); ;
        switch (direct_step)
        {
            case 0:
                {
                    Anti_cycle[2] = false;
                    Possibel_step = Price_step(obpt_1, obpt_2, obpt_3, agent, count_step, fnipt, Anti_cycle);
                    if (Possibel_step.Count() == 0)
                        Anti_cycle[2] = true;
                    break;
                }
            case 1:
                {
                    Anti_cycle[3] = false;
                    Possibel_step = Price_step(obpt_1, obpt_2, obpt_3, agent, count_step, fnipt, Anti_cycle);
                    if (Possibel_step.Count() == 0)
                        Anti_cycle[3] = true;
                    break;
                }
            case 2:
                {
                    Anti_cycle[0] = false;
                    Possibel_step = Price_step(obpt_1, obpt_2, obpt_3, agent, count_step, fnipt, Anti_cycle);
                    if (Possibel_step.Count() == 0)
                        Anti_cycle[0] = true;
                    break;
                }
            case 3:
                {
                    Anti_cycle[1] = false;
                    Possibel_step = Price_step(obpt_1, obpt_2, obpt_3, agent, count_step, fnipt, Anti_cycle);
                    if (Possibel_step.Count() == 0)
                        Anti_cycle[1] = true;
                    break;
                }
        }
        return Anti_cycle;
    }
    //метод расчета стоимости кадого направления, но для финальной точки
    private Dictionary<int, double>  Price_step(double[] obpt_1, double[] obpt_2, double[] obpt_3, double[] agent, int count_step, double[] fnipt, Dictionary<int, bool> Anti_cycle)
    {
        Dictionary<int, double> Mass_step = new Dictionary<int, double>();
        count_step++;
        double f_t;
        for (int i = 0; i < 4; i++)
        {
            double[] mass_agent = new double[] { agent[0], agent[1] };
            mass_agent = Shift_agent(mass_agent, i);
            if (Сheck(mass_agent) & Checke(obpt_1, obpt_2, obpt_3, mass_agent) & Anti_cycle[i])
            {
                f_t = count_step + Calculation_heuristics(fnipt, mass_agent);
                Mass_step.Add(i, f_t);
            }
        }
        return Mass_step;
    }
    //проверка дошёл агент до финальной точки или нет
    private bool Reaching_trpt(double[] fnipt, double[] agent)
    {
        if ((fnipt[0] == agent[0] & fnipt[1] == agent[1]))
            return  false;
        else
            return  true;
    }
    //метод проверки прохождения целевых точек, если агент их проходит то в массиве для точки ставим true
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
    //Отображение всех шагов
    private void show(List<string> result)
    {
        foreach (var str in result)
        {
            Console.Write(" " + str);
        }
        Console.WriteLine();
    }
    public Algoritm_A(double[] trpt_1, double[] trpt_2, double[] trpt_3, double[] obpt_1, double[] obpt_2, double[] obpt_3, double[] agent, double[] fnipt)
    {
        //инициалтзация различных необходимых переменных
        int count_step = 0;
        Dictionary<int, double> Possibel_step = new Dictionary<int, double>();//словарь возможных направлений вместе с их стоимостью(где ключ словаря это направление, занчение словаря-оценка стоимости)
        Dictionary<int, double> Filter_possibel_step = new Dictionary<int, double>();//то же самое но отфильтрофаное от дорогих шагов
        Dictionary<int, bool> Anti_cycle = new Dictionary<int, bool>() { { 0, true }, { 1, true }, { 2, true }, { 3, true } };//для блокировки движения назад
        List<string> Step_result_trpt = new List<string>();//записываем все шаги для достижения целевых точек
        List<string> Step_result_fnipt = new List<string>();//записываем все шаги для достижения финальной точек
        bool[] mass_f = new bool[] { false, false, false };
        bool F1 = true;
        bool F2 = true;
        string final_step;
        //ручное задание координат
        /*
        trpt_1[0] = 3; trpt_1[1] = 4;
        trpt_2[0] = 2; trpt_2[1] = 3;
        trpt_3[0] = 4; trpt_3[1] = 1;
        obpt_1[0] = 3; obpt_1[1] = 1;
        obpt_2[0] =3; obpt_2[1] = 2;
        obpt_3[0] = 4; obpt_3[1] = 2;
        agent[0] = 4; agent[1] = 3;
        fnipt[0] = 1; fnipt[1] = 2;
        */   
        //здесь в бесконечном цикле расчитвается стомость шагов, выбирается минимальный, и так до тех пор пока все целевые точки не будут пройдены
        while (F1)
        {
            int direct_step;
            Possibel_step = Price_step(obpt_1, obpt_2, obpt_3, trpt_1, trpt_2, trpt_3, agent, count_step, mass_f[0], mass_f[1], mass_f[2], fnipt, Anti_cycle);
            if (Possibel_step.Count() == 0)
            {
                Console.WriteLine("Хьюстон, у нас проблема, агент в тупике");
                break;
            }
            Filter_possibel_step = Min_f(Possibel_step, out direct_step);
            if (Filter_possibel_step.Count() == 1)
            {
                agent = Shift_agent(agent, direct_step, out final_step);
                mass_f = Reaching_trpt(trpt_1, trpt_2, trpt_3, agent, mass_f);
                Step_result_trpt.Add(final_step);
                count_step++;
            }
            else
            {
                direct_step = (int)Exit_the_loop(obpt_1, obpt_2, obpt_3, trpt_1, trpt_2, trpt_3, agent, count_step, mass_f, fnipt, Filter_possibel_step, Anti_cycle);
                agent = Shift_agent(agent, (int)direct_step, out final_step);
                mass_f = Reaching_trpt(trpt_1, trpt_2, trpt_3, agent, mass_f);
                Step_result_trpt.Add(final_step);
                count_step++;
            }
            if (mass_f[0] & mass_f[1] & mass_f[2])
                F1 = false;
            if (F1 == false)
            {
                Console.WriteLine("Ходы чтобы пройти все целевые точки: ");
                show(Step_result_trpt);
            }
            Anti_cycle = Block_last_step(obpt_1, obpt_2, obpt_3, trpt_1, trpt_2, trpt_3, agent, count_step, mass_f, fnipt, direct_step);
            if (count_step > 100)
                Console.WriteLine("Что-то пошло не по плану, походу мы ходим кругами");

        }
        //здесь в бесконечном цикле расчитвается стомость шагов, выбирается минимальный, и так до тех пор пока финальная точка не будет достигнута.
        while (F2)
        {
            int direct_step;
            Possibel_step = Price_step(obpt_1, obpt_2,obpt_3, agent, count_step, fnipt, Anti_cycle);
            if (Possibel_step.Count() == 0)
            {
                Console.WriteLine("Хьюстон, у нас проблема, агент в тупике");
                break;
            }
            Filter_possibel_step = Min_f(Possibel_step, out direct_step);
            agent = Shift_agent(agent, direct_step, out final_step);
            F2 = Reaching_trpt(fnipt,agent);
            Step_result_fnipt.Add(final_step);
            count_step++;
            Anti_cycle = Block_last_step(obpt_1, obpt_2, obpt_3, agent, count_step, fnipt, direct_step);
            if (F2 == false)
            {
                Console.WriteLine("Ходы чтобы дойти до финальной точки: ");
                show(Step_result_fnipt);
            }
            if (count_step > 100)
                Console.WriteLine("Что-то пошло не по плану, походу мы ходим кругами");
        }
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










/// это не важно, но в целом может в будущем пригодится
/// просто пусть пока тут полежит

/*
if (count_block >= 1)
{
    if (count_step > 10)
        doorstep = 5;
    if (count_step > 15)
        doorstep = 7;
    count_block++;
    if (count_block == doorstep)
    {
        count_block = 0;
        for (int i = 0; i < 4; i++)
        {
            Anti_cycle[i] = true;
        }
    }
}

if (count_step > 4)
{
    if (Step_result[Step_result.Count()-1] == Step_result[Step_result.Count() - 3] && Step_result[Step_result.Count() - 2] == Step_result[Step_result.Count() - 4])
    {
        if (Step_result.Last() == "Up")
        {
            if ((Сheck(new double[] { agent[0] - 1, agent[1] }) && Checke(obpt_1, obpt_2, obpt_3, new double[] { agent[0] - 1, agent[1] })) || (Сheck(new double[] { agent[0] + 1, agent[1] }) && Checke(obpt_1, obpt_2, obpt_3, new double[] { agent[0] + 1, agent[1] })))
            {
                if ((Сheck(new double[] { agent[0], agent[1] + 1 }) && Checke(obpt_1, obpt_2, obpt_3, new double[] { agent[0], agent[1] + 1 })) == false)
                    ///просмоторщик будущего
                    Anti_cycle[2] = false;
            }
            count_block++;
        }
        if (Step_result.Last() == "Right")
        {
            if ((Сheck(new double[] { agent[0], agent[1] + 1 }) && Checke(obpt_1, obpt_2, obpt_3, new double[] { agent[0], agent[1] + 1 })) || (Сheck(new double[] { agent[0], agent[1] - 1 }) && Checke(obpt_1, obpt_2, obpt_3, new double[] { agent[0], agent[1] - 1 })))
            {
                if ((Сheck(new double[] { agent[0] - 1, agent[1] }) && Checke(obpt_1, obpt_2, obpt_3, new double[] { agent[0] - 1, agent[1] })) == false)
                    Anti_cycle[3] = false;
            }
            count_block++;
        }
        if (Step_result.Last() == "Down")
        {
            if ((Сheck(new double[] { agent[0] - 1, agent[1] }) && Checke(obpt_1, obpt_2, obpt_3, new double[] { agent[0] - 1, agent[1] })) || (Сheck(new double[] { agent[0] + 1, agent[1] }) && Checke(obpt_1, obpt_2, obpt_3, new double[] { agent[0] + 1, agent[1] })))
            {
                if ((Сheck(new double[] { agent[0], agent[1] - 1 }) && Checke(obpt_1, obpt_2, obpt_3, new double[] { agent[0], agent[1] - 1 })) == false)
                    Anti_cycle[0] = false;
            }
            count_block++;
        }
        if (Step_result.Last() == "Left")
        {
            if ((Сheck(new double[] { agent[0], agent[1] + 1 }) && Checke(obpt_1, obpt_2, obpt_3, new double[] { agent[0], agent[1] + 1 })) || (Сheck(new double[] { agent[0], agent[1] - 1 }) && Checke(obpt_1, obpt_2, obpt_3, new double[] { agent[0], agent[1] - 1 })))
            {
                if ((Сheck(new double[] { agent[0] + 1, agent[1] }) && Checke(obpt_1, obpt_2, obpt_3, new double[] { agent[0] + 1, agent[1] })) == false)
                    Anti_cycle[1] = false;
            }
            count_block++;
        }
    }
}
}
*/