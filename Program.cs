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
    //метод для единичного присваивания одному из массивов координат определённых значений
    public void SetMass(int i, double[] mass)
    {
        switch (i)
        {
            case 1:
                {
                    coordinates1 = mass;
                    break;
                }
            case 2:
                {
                    coordinates2 = mass;
                    break;
                }
            case 3:
                {
                    coordinates3 = mass;
                    break;
                }
        }
    }
    //метод для присваения массивам рандомных значений
    public double[] SetMass()
    {
        double[] mass = new double[2];
        Random rand = new Random();
        for (int i = 0; i < 2; i++)
        {
            mass[i] = rand.Next(0, 5);
        }
        return mass;
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
    private void show(double[] mass, int k)
    {
        Console.WriteLine($"Целевая точка №{k}");
        foreach (double i in mass)
        {
            Console.Write($" {i}");
        }
        Console.WriteLine(" ");
    }
    private bool Comprasion_mass(double[] mass1, double[] mass2)
    {
        bool result = true;
        if (mass1[0] == mass2[0] && mass1[1] == mass2[1])
        {
            result = false;
        }
        return result;
    }
    //конструктор для инициализации целевых точек и их координат 
    public Target_point()
    {
        bool f = true;
        while (f)
        {
            double[] arr_point1 = SetMass();
            double[] arr_point2 = SetMass();
            double[] arr_point3 = SetMass();
            //обеспечение различных координат для всех 3 точек
            if (Comprasion_mass(arr_point1, arr_point2) && Comprasion_mass(arr_point1,arr_point3) && Comprasion_mass(arr_point2, arr_point3))
            {
                set(arr_point1, arr_point2, arr_point3);
                f = false;
            }
        }
    }
}
//класс препятствий
class Obstacles
{
    //массивы с координатами препятствий
    private double[] coordinates1 = new double[2];
    private double[] coordinates2 = new double[2];
    private double[] coordinates3 = new double[2];

    //метод для получения значения масивов
    public double[] get(int i)
    {
        List<double[]> list_point = new List<double[]> { coordinates1, coordinates2, coordinates3 };
        return list_point[i - 1];
    }
    //метод для единичного присваивания одному из массивов координат определённых значений
    public void SetMass(int i, double[] mass)
    {
        switch (i)
        {
            case 1:
                {
                    coordinates1 = mass;
                    break;
                }
            case 2:
                {
                    coordinates2 = mass;
                    break;
                }
            case 3:
                {
                    coordinates3 = mass;
                    break;
                }
        }
    }
    //метод для присваения массивам рандомных значений
    public double[] SetMass()
    {
        double[] mass = new double[2];
        Random rand = new Random();
        for (int i = 0; i < 2; i++)
        {
            mass[i] = rand.Next(0, 5);
        }
        return mass;
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
    //метод для вывода значений массива
    private void show(double[] mass, int k)
    {
        Console.WriteLine($"Местоположение препятствия №{k}");
        foreach (double i in mass)
        {
            Console.Write($" {i}");
        }
        Console.WriteLine("");
    }
    private bool Comprasion_mass(double[] mass1, double[] mass2)
    {
        bool result = true;
        if (mass1[0] == mass2[0] && mass1[1] == mass2[1])
        {
            result = false;
        }
        return result;
    }
    //конструктор для инициализации целевых точек и их координат 
    public Obstacles()
    {
        bool f = true;
        while (f)
        {
            double[] arr_point1 = SetMass();
            double[] arr_point2 = SetMass();
            double[] arr_point3 = SetMass();
            //обеспечение различных координат для всех 3 точек
            if (Comprasion_mass(arr_point1, arr_point2) && Comprasion_mass(arr_point1, arr_point3) && Comprasion_mass(arr_point2, arr_point3))
            {
                set(arr_point1, arr_point2, arr_point3);
                f = false;
            }
        }
    }
}
class Agent
{
    private double[] coordinates = new double[2];
    public double[] SetMass()
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
    private void show(double[] mass)
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
        set(SetMass());
    }
}
class Final_point
{
    private double[] coordinates = new double[2];
    public double[] SetMass()
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
    private void show(double[] mass)
    {
        Console.WriteLine($"Местоположение финальной точки");
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
    public Final_point()
    {
        set(SetMass());
    }
}
class project
{
    static void Main()
    {
        Target_point target_coordinates = new Target_point();
        Obstacles obstacles_coordinates = new Obstacles();
        Agent agent = new Agent();
        Final_point final_point = new Final_point();

        List<double[]> Set_array = new List<double[]>(8) { target_coordinates.get(1), target_coordinates.get(2), target_coordinates.get(3), obstacles_coordinates.get(1), obstacles_coordinates.get(2), obstacles_coordinates.get(3), agent.get(), final_point.get() };
        Set_array = Cheked_mass(Set_array);
        Console.WriteLine("\nОбновлённые координаты:");
        target_coordinates.set(Set_array[0], Set_array[1], Set_array[2]);
        obstacles_coordinates.set(Set_array[3], Set_array[4], Set_array[5]);
        agent.set(Set_array[6]);
        final_point.set(Set_array[7]);
    }
    static public List<double[]> Cheked_mass(List<double[]> Set_array)
    {
        bool f1 = false;
        bool f2 = true;
        while (f1 == false)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = i + 1; j < 8; j++)
                {
                    if (Set_array[i][0] == Set_array[j][0] && Set_array[i][1] == Set_array[j][1])
                    {
                        Set_array[j] = SetMass();
                        f2 = false;
                    }
                }
            }
            if (f2 == false)
            {
                f1 = false;
            }
            else
            {
                f1 = true;
            }
            f2 = true;
        }
        return Set_array;
    }
    static public double[] SetMass()
    {
        double[] mass = new double[2];
        Random rand3 = new Random();
        for (int i = 0; i < 2; i++)
        {
            mass[i] = rand3.Next(0, 5);
        }
        return mass;
    }
    //аналогичну агенту, разобрать наследование чтобы было проще, нужен скласс который будет сверять не находятсся какие-либо два объекта одновременно в одном месте
    // нужен сам алгоритм решения
}
//работает последняя проверка но проверка изначальная в классах говно полное


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