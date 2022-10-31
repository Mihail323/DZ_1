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
    private double[] Creat_Mass()
    {
        double[] mass = new double[2];
        Random rand = new Random();
        for (int i = 0; i < 2; i++)
        {
            mass[i] = rand.Next(0, 5);
        }
        return mass;
    }
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
    public bool check(double i, double j)
    {
        if ((i < 5 & i > -1) & (j < 5 & j > -1))
            return true;
        else
            return false;
    }
    public Agent()
    {
        set(Creat_Mass());
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
class project
{
    static void Main()
    {
        /* 
        Target_point target_coordinates = new Target_point();
        Obstacles obstacles_coordinates = new Obstacles();
        Agent agent = new Agent();
        Final_point final_point = new Final_point();
        */
        Field field = new Field();
        Target_point target_coordinates = new Target_point(field.get(0), field.get(1), field.get(2));
        Obstacles obstacles_coordinates = new Obstacles(field.get(3), field.get(4), field.get(5));
    }


}
//аналогичну агенту, разобрать наследование чтобы было проще, нужен скласс который будет сверять не находятсся какие-либо два объекта одновременно в одном месте
// нужен сам алгоритм решения
//подумать о реализации следующего метода в классе field генеррируется массив случайных чисел(обозначающих клетки на поле а потом он раскидывается каждому объекту)

















//возможно оптимизация через списки
/*Random rand1=new Random();

double[,] kl,mn,ij;

List<double[,]> list_mass=new List<double[,]>() {kl,mn,ij};

foreach (double[,] mass in list_mass)

{

for( int i=0; i<2; i++)

{

for( int j=0; j<2; j++)

{

mass[i,j]=rand1.Next(1,5);

}

}

}*/