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
    public bool check(double[] mass)
    {
        if ((mass[0] < 5 & mass[0] > -1) & (mass[1] < 5 & mass[1] > -1))
            return true;
        else
            return false;
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
    private double Calculation_heuristics(double[] trpt_1, double[] trpt_2, double[] trpt_3, double[] agent)
    {
        double h_result = 0;
        double h1 = Dop_calculation(trpt_1, agent);
        double h2 = Dop_calculation(trpt_2, agent);
        double h3 = Dop_calculation(trpt_3, agent);
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
    //direction= 1-Up, 2-right, 3-down, 4-left.
    private void Step_priece(Agent agent,int direction, out int count_step, out double funct_tesult, out bool flag)
    {
        funct_tesult = 0;
        double[] mass = agent.get();
        switch(direction)
        {
            case 1:
                {
                    if (agent.check(new double[2] { mass[0], mass[1] + 1 }))
                    {
                        flag = true;

                    }
                    else
                    {
                        flag = false;
                        count_step = count_step;
                    }
                    break;
                }
            case 2:
                {

                    break;
                }
            case 3:
                {

                    break;
                }
            case 4:
                {

                    break;
                }
        }


    }
    public Algoritm_A(double[] trpt_1, double[] trpt_2, double[] trpt_3, double[] obpt_1, double[] obpt_2, double[] obpt_3, double[] agent, double[] fnipt)
    {

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
    }


}
