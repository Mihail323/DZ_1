# ___DZ_1___
## __Краткое описание программы и реализации алгоритма___
### __Поле__
Для начала немного слов о поле. Поле это квадрат размером 4 на 4. Отсчёт координат введётся от левого верхнего угла, это небольшой задел для дальнейшей графической реализации. То есть ось X  от верхнего левого угла идёт вправо горизантально, а ось Y вниз вертикально. Каждая клетка пронумерована и имеет свои координты (x,y) 0<=x,y<5. Т.е. верхняя левая 0(координаты 1,1), правее от неё-1(координаты 2,1), дальше 2(координаты 3,1), 3(координаты 4,1), переходим на в следующий ряд, и снова крайне левая 4(координаты 1,2), дальше 5(координаты 2,2) и т.д. на рисунке Picture все нарисовано более подробно. 
### __Размещение объектов__
В начале создается объект клас Field, в котором инициализируется массив различных чисел от 0 до 15 включительно, каждая цифра соответсвуют номеру клеки на поле и опредлённому объекту. Так первые три цифры это цифры которые относятся к целевым точкам, слудеющие три это препятсвия, дальше одна цифра агент, и финальная точка. Таким образом мы рандомно размесщаем объекты на поле. Данные массив цифр и какие координаты соответствуеют каждому объекту ввыводятся в консольное окно.
## __Алгоритм__
В целом алгоритм работает следующим образом:
1) Создается словарь в который записывается данные о возможном напрааление движения - ключ, и стоимость шага- значение. Конечно здесь учитывается не врезается ли агент в препятсвие и не выходит ли за рамки поля.(метод Prise_step)(С эвристикой я там немного навороитл учитвается насколько далеко точка от финальной точки а как далеко агент от финальной точки, это для того чтобы он прошёлся сначала пос амым дальним целевым точкам, хотя в некоторх случая это и довльно мешает)
2) Из данного словаря выбирается тот направление которое имеет минимальную стомимость(Min_f)
3) Если таких направлений несколько, то мы берём каждую не проёденную целевую точку и расчитываем сколько будет стоить каждое напралее движение до каждой из точек выбираем из них миниальное с уловием того чтобы оно совпадало с теми направления которые были выбраны на 2 шаге.
4) Дальше агент делает шаг в данном напралении, запускается проверка прохождения целевых точек '''(метод Reaching_trpt)''', добавляется сделанные шаг в список шагов ''' Step_result_fnipt.Add(final_step) '''', и блокируется движения назад '''Anti_cycle = Block_last_step(...)'''.
5) И так по кругу пока не бдуут пройдены все целевые точки, или алгоритм не попадёт в цикл(как правло это происходит если точка заперта препятсвиями или очень сложная карта).
6) Когда агент пройдёт все целевые точки похожий цикл запускается для финальной
7) В целом алгоритм работает процентов 80% ситуаций он справляется, но все равно я пока не придумал как реализовать то чтобы агент распозновал ситуцию когда препятсвие заперто и те случаи когда он попадает в сложный цикл.
## __Дополнительно__
В папке result лежат скрины результата работы программы.
Алгоритм можно был написан в Visual_Studio.
