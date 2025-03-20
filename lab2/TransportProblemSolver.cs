using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TransportTask
{    
    class TransportProblemSolver
    {
        private int[] supply; // Запасы
        private int[] demand; // Потребности
        private decimal[,] costs; // Матрица затрат
        public int[,] allocation; // Матрица перевозок
        private decimal[] u; // Потенциалы поставщиков
        private decimal[] v; // Потенциалы потребителей

        public decimal result = 0;

        public TransportProblemSolver(int[] supply, int[] demand, decimal[,] costs)
        {
            this.supply = supply;
            this.demand = demand;
            this.costs = costs;
            this.allocation = new int[supply.Length, demand.Length];
            this.u = new decimal[supply.Length];
            this.v = new decimal[demand.Length];
        }

        // Решение транспортной задачи 
        // Функция взята отсюда: https://github.com/BioQwer/Transportation_theory_vs_M_metod
        public void solve()
        {
            int N, M;        //Размерность задачи            
            decimal[,] D;                 //адрес массива(двумерного) оценок свободных ячеек таблицы
            bool stop;              //признак достижения оптимального плана
            bool[,] T;              //массив будет хранить коодинаты ячеек, в которые уже вписывались
            bool ok = true;           //нулевые поставки при попытках устранить вырожденность плана

            N = costs.GetLength(0);  //кол-во строк (поставщиков)
            M = costs.GetLength(1);  //кол-во столбцов (потребителей)

            //-------------------Проверка на сбалансированность
            decimal Sa = 0;
            decimal Sb = 0;


            for (int i = 0; i < N; i++)          //находим суммарные запасы
                Sa = Sa + supply[i];

            for (int i = 0; i < M; i++)          //находим суммарную потребность
                Sb = Sb + demand[i];

            if (Sa != Sb)
                return;

            //----------------------Инициализация динамических массивов:
                     

            //Двумерный массив для Доставки:
            allocation = new int[N + 1, M + 1];//выделяем память под массив адресов начала строк
            /*
             В последней строке(столбце) массива Х будем записывать
             сумму заполненных клеток в соответствующем столбце(строке)
            */
            for (int i = 0; i < N + 1; i++)
                for (int j = 0; j < M + 1; j++)
                {
                    allocation[i, j] = -1;           //вначале все клетки не заполнены
                    if (i == N)
                        allocation[i, j] = 0;        //сумма заполненных клеток в j-м столбце
                    if (j == M)
                        allocation[i, j] = 0;        //сумма заполненных клеток в i-й строке
                }

            //-----------------Метод минимального элемента:
            decimal Sij = 0;
            do
            {
                int im = 0;
                int jm = 0;
                decimal Cmin = -1;
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < M; j++)
                        if (allocation[N, j] != demand[j])//если не исчерпана Потребность Bj
                            if (allocation[i, M] != supply[i])//если не исчерпан Запас Аі
                                if (allocation[i, j] < 0)//если клетка ещё не заполнена
                                {
                                    if (Cmin == -1)//если это первая подходящая ячейка
                                    {
                                        Cmin = costs[i, j];
                                        im = i;
                                        jm = j;
                                    }
                                    else //если это не первая подходящая ячейка
                                        if (costs[i, j] <= Cmin)//если в ячейке меньше,чем уже найдено
                                    {
                                        Cmin = costs[i, j];
                                        im = i;
                                        jm = j;
                                    }
                                }

                allocation[im, jm] = Math.Min(supply[im] - allocation[im, M], demand[jm] - allocation[N, jm]);//выбираем поставку
                allocation[N, jm] += allocation[im, jm];//добавляем поставку jm-му потребителю
                allocation[im, M] += allocation[im, jm];//добавляем поставку im-му поставщику
                Sij += allocation[im, jm]; //Подсчёт суммы добавленых поставок

            } while (Sij < Math.Max(Sa, Sb));//условие продолжения

            int L = 0;
            for (int i = 0; i < N; i++)
                for (int j = 0; j < M; j++)
                    if (allocation[i, j] >= 0) L++;          //подсчёт заполненных ячеек
            int d = M + N - 1 - L;                  //если d>0,то задача - вырожденная,придётся добавлять d нулевых поставок
            int d1 = d;                             //запоминаем значение d

            decimal F = 0;
            for (int i = 0; i < N; i++)
                for (int j = 0; j < M; j++)
                {
                    if (allocation[i, j] > 0)
                        F += allocation[i, j] * costs[i, j];
                }

            //--------------------------Метод потенциалов

            T = new bool[N, M];

            for (int i = 0; i < N; i++)
                for (int j = 0; j < M; j++)
                    T[i, j] = false;

            do
            {//цикл поиска оптимального решения

                stop = true;//признак оптимальности плана(после проверки может стать false)                
                bool[] ub = new bool[N];//вспомогательные массвы
                bool[] vb = new bool[M];
                for (int i = 0; i < N; i++)
                    ub[i] = false;
                for (int i = 0; i < M; i++)
                    vb[i] = false;


                //цикл расчёта потенциалов (несколько избыточен):
                u[0] = 0;              //значение одного потенциала выбираем произвольно
                ub[0] = true;
                int count = 1;
                int tmp = 0;
                do
                {
                    for (int i = 0; i < N; i++)
                        if (ub[i] == true)
                            for (int j = 0; j < M; j++)
                                if (allocation[i, j] >= 0)
                                    if (vb[j] == false)
                                    {
                                        v[j] = costs[i, j] - u[i];
                                        vb[j] = true;
                                        count++;
                                    }
                    for (int j = 0; j < M; j++)
                        if (vb[j] == true)
                            for (int i = 0; i < N; i++)
                                if (allocation[i, j] >= 0)
                                    if (ub[i] == false)
                                    {
                                        u[i] = costs[i, j] - v[j];
                                        ub[i] = true;
                                        count++;
                                    }


                    tmp++;
                } while ((count < (M + N - d * 2)) && (tmp < M * N));


                //цикл добавления нулевых поставок(в случае вырожденности):
                bool t = false;

                if ((d > 0) || ok == false) t = true;//цикл начинается, если d>0
                while (t)//цикл продолжается до тех пор, пока все потенциалы не будут найдены
                {
                    for (int i = 0; (i < N); i++)//просматриваем потенциалы поставщиков
                        if (ub[i] == false)//если среди них не заполненный потенциал
                            for (int j = 0; (j < M); j++)
                                if (vb[j] == true)
                                {
                                    if (d > 0)
                                        if (T[i, j] == false)//если раньше не пытались использовать
                                        {
                                            allocation[i, j] = 0;        //добавляем нулевую поставку
                                            d--;                //уменьшаем кол-во требуемых добавлений нулевых поставок
                                            T[i, j] = true;     //отмечаем, что эту ячейку уже использовали
                                        }
                                    if (allocation[i, j] >= 0)
                                    {
                                        u[i] = costs[i, j] - v[j];//дозаполняем потенциалы
                                        ub[i] = true;
                                    }
                                }
                    for (int j = 0; (j < M); j++)//просматриваем потенциалы потребителей
                        if (vb[j] == false)//если среди них не заполненный потенциал
                            for (int i = 0; (i < N); i++)
                                if (ub[i] == true)
                                {
                                    if (d > 0)
                                        if (T[i, j] == false)//если раньше не пытались использовать
                                        {
                                            allocation[i, j] = 0;//добавляем нулевую поставку
                                            d--;//уменьшаем кол-во требуемых добавлений нулевых поставок
                                            T[i, j] = true;//отмечаем, что эту ячейку уже использовали
                                        }
                                    if (allocation[i, j] >= 0)
                                    {
                                        v[j] = costs[i, j] - u[i];//дозаполняем потенциалы
                                        vb[j] = true;
                                    }
                                }
                    t = false; //проверяем, все ли потенциалы найдены
                    for (int i = 0; i < N; i++)
                        if (ub[i] == false) t = true;
                    for (int j = 0; j < M; j++)
                        if (vb[j] == false) t = true;
                }

                D = new decimal[N, M];//выделяем память под массив оценок свободных ячеек

                for (int i = 0; i < N; i++)
                    for (int j = 0; j < M; j++)
                    {
                        if (allocation[i, j] >= 0)//если ячейка не свободна
                            D[i, j] = 88888;//Заполняем любыми положительными числами
                        else  //если ячейка свободна
                            D[i, j] = costs[i, j] - u[i] - v[j];//находим оценку

                        if (D[i, j] < 0)
                        {
                            stop = false;//признак того, что план не оптимальный
                        }

                    }
                //
                if (stop == false)//если план не оптимальный
                {
                    int[,] Y = new int[N, M];//массив для хранения цикла перераспределения поставок

                    decimal find1, find2;//величина перераспределения поставки для цикла
                    decimal best1 = 0;//наилучшая оценка улучшения среди всех допустимых перераспределений
                    decimal best2 = 0;
                    int ib1 = -1;
                    int jb1 = -1;
                    int ib2 = -1;
                    int jb2 = -1;
                    //Ищем наилучший цикл перераспределения поставок:
                    for (int i = 0; i < N; i++)
                        for (int j = 0; j < M; j++)
                            if (D[i, j] < 0)//Идём по ВСЕМ ячейкам с отрицательной оценкой
                            {  //и ищем допустимые циклы перераспределения ДЛЯ КАЖДОЙ такой ячейки
                                //Обнуляем матрицу Y:
                                for (int i1 = 0; i1 < N; i1++)
                                    for (int j1 = 0; j1 < M; j1++)
                                        Y[i1, j1] = 0;
                                //Ищем цикл для ячейки с оценкой D[i,j]:
                                find1 = find_gor(i, j, i, j, N, M, Y, 0, -1);   //Начинаем идти по горизонтали

                                //Обнуляем матрицу Y:
                                for (int i1 = 0; i1 < N; i1++)
                                    for (int j1 = 0; j1 < M; j1++)
                                        Y[i, j] = 0;

                                find2 = find_ver(i, j, i, j, N, M, Y, 0, -1);//Начинаем по вертикали

                                if (find1 > 0)
                                    if (best1 > D[i, j] * find1)
                                    {
                                        best1 = D[i, j] * find1;     //наилучшая оценка
                                        ib1 = i;                   //запомминаем координаты ячейки
                                        jb1 = j;                   //цикл из которой даёт наибольшее улучшение
                                    }
                                if (find2 > 0)
                                    if (best2 > D[i, j] * find2)
                                    {
                                        best2 = D[i, j] * find2; //наилучшая оценка
                                        ib2 = i;              //запомминаем координаты ячейки
                                        jb2 = j;              //цикл из которой даёт наибольшее улучшение
                                    }
                            }
                    if ((best1 == 0) && (best2 == 0))
                    {
                        //stop=true;
                        //ShowMessage("Цикл перераспределения поставок не найден");
                        ok = false;
                        d = d1;//откат назад
                        for (int i = 0; i < N; i++)
                            for (int j = 0; j < M; j++)
                                if (allocation[i, j] == 0) allocation[i, j] = -1;
                        continue;
                    }
                    else
                    {   //Обнуляем матрицу Y:
                        for (int i = 0; i < N; i++)
                            for (int j = 0; j < M; j++)
                                Y[i, j] = 0;
                        //возвращаемся к вычислению цикла с наилучшим перераспределением:
                        int ib, jb;
                        if (best1 < best2)
                        {
                            find_gor(ib1, jb1, ib1, jb1, N, M, Y, 0, -1);
                            ib = ib1;
                            jb = jb1;
                        }
                        else
                        {
                            find_ver(ib2, jb2, ib2, jb2, N, M, Y, 0, -1);
                            ib = ib2;
                            jb = jb2;
                        }
                        for (int i = 0; i < N; i++)
                        {
                            for (int j = 0; j < M; j++)
                            {
                                if ((allocation[i, j] == 0) && (Y[i, j] < 0))
                                {
                                    stop = true;
                                    ok = false;
                                    d = d1;//откат назад
                                }
                                allocation[i, j] += Y[i, j];//перераспределяем поставки
                                if ((i == ib) && (j == jb)) allocation[i, j] = allocation[i, j] + 1;//добавляем 1 (т.к. до этого было -1 )
                                if ((Y[i, j] <= 0) && (allocation[i, j] == 0)) allocation[i, j] = -1;//если ячейка обнулилась, то выбрасываем её из рассмотрения
                            }
                        }
                    }

                    decimal F1 = 0;
                    for (int i = 0; i < N; i++)
                        for (int j = 0; j < M; j++)
                        {
                            if (allocation[i, j] > 0)
                                F1 += allocation[i, j] * costs[i, j];
                        }

                    ok = true;
                    for (int i = 0; i < N; i++)
                        for (int j = 0; j < M; j++)
                            T[i, j] = false;

                    //проверка на вырожденность: (?)
                    L = 0;
                    for (int i = 0; i < N; i++)
                        for (int j = 0; j < M; j++)
                            if (allocation[i, j] >= 0) L++;//подсчёт заполненных ячеек
                    d = M + N - 1 - L;//если d>0,то задача - вырожденная
                    d1 = d;
                    if (d > 0) ok = false;

                }
            } while (stop == false);

            //Оптимальный план:
            result = 0;
            for (int i = 0; i < N; i++)
                for (int j = 0; j < M; j++)
                {
                    if (allocation[i, j] > 0)
                        result += allocation[i, j] * costs[i, j];
                }
        }
        //Функуция поиска ячеек,подлежащих циклу перераспределения (вдоль строки)
        private decimal find_gor(int i_next, int j_next, int im, int jm, int n, int m, int[,] Y, int odd, int Xmin)
        {
            decimal rez = -1;
            for (int j = 0; j < m; j++)//идём вдоль строки, на которой стоим
                //ищем заполненную ячейку(кроме той,где стоим) или начальная ячейка(но уже в конце цикла:odd!=0 )
                if (((allocation[i_next, j] >= 0) && (j != j_next)) || ((j == jm) && (i_next == im) && (odd != 0)))
                {
                    odd++;//номер ячейки в цикле перерасчёта(начало с нуля)
                    int Xmin_old = -1;
                    if ((odd % 2) == 1)//если ячейка нечётная в цикле ( начальная- нулевая )
                    {
                        Xmin_old = Xmin;//Запоминаем значение минимальной поставки в цикле (на случай отката назад)
                        if (Xmin < 0) Xmin = allocation[i_next, j];//если это первая встреченная ненулевая ячейка
                        else if ((allocation[i_next, j] < Xmin) && (allocation[i_next, j] >= 0))
                        {
                            Xmin = allocation[i_next, j];

                        }
                    }
                    if ((j == jm) && (i_next == im) && ((odd % 2) == 0))//если замкнулся круг и цикл имеет чётное число ячеек
                    {
                        Y[im, jm] = Xmin;//Значение минимальной поставки, на величину которой будем изменять
                        return Xmin;
                    }
                    //если круг еще не замкнулся - переходим к поиску по вертикали:
                    else rez = find_ver(i_next, j, im, jm, n, m, Y, odd, Xmin);//рекурсивный вызов
                    if (rez >= 0)//как бы обратный ход рекурсии(в случае если круг замкнулся)
                    {
                        //для каждой ячейки цикла заполняем матрицу перерасчёта поставок:
                        if (odd % 2 == 0) Y[i_next, j] = Y[im, jm];//в чётных узлах прибавляем
                        else Y[i_next, j] = -Y[im, jm];//в нечётных узлах вычитаем
                        break;
                    }
                    else //откат назад в случае неудачи(круг не замкнулся):
                    {
                        odd--;
                        if (Xmin_old >= 0)//если мы изменяли Xmin на этой итерации
                            Xmin = Xmin_old;
                    }
                }

            return rez;//если круг замкнулся (вернулись в исходную за чётное число шагов) -
            // возвращает найденное минимальное значение поставки в нечётных ячейках цикла,
            // если круг не замкнулся, то возвращает -1.
        }
        //Функуция поиска ячеек,подлежащих циклу перераспределения (вдоль столбца)
        private decimal find_ver(int i_next, int j_next, int im, int jm, int n, int m, int[,] Y, int odd, int Xmin)
        {
            decimal rez = -1;
            int i;
            for (i = 0; i < n; i++)//идём вдоль столбца, на котором стоим
                //ищем заполненную ячейку(кроме той,где стоим) или начальная ячейка(но уже в конце цикла:odd!=0 )
                if (((allocation[i, j_next] >= 0)) && (i != i_next) || ((j_next == jm) && (i == im) && (odd != 0)))
                {
                    odd++;//номер ячейки в цикле перерасчёта(начало с нуля)
                    int Xmin_old = -1;
                    if ((odd % 2) == 1)//если ячейка нечётная в цикле ( начальная- нулевая )
                    {
                        Xmin_old = Xmin;//Запоминаем значение минимальной поставки в цикле (на случай отката назад)
                        if (Xmin < 0) Xmin = allocation[i, j_next];//если это первая встреченная ненулевая ячейка
                        else
                            if ((allocation[i, j_next] < Xmin) && (allocation[i, j_next] >= 0))
                            Xmin = allocation[i, j_next];
                    }
                    if ((i == im) && (j_next == jm) && ((odd % 2) == 0))//если замкнулся круг и цикл имеет чётное число ячеек
                    {
                        Y[im, jm] = Xmin;//Значение минимальной поставки, на величину которой будем изменять
                        return Xmin;
                    }
                    //если круг еще не замкнулся - переходим к поиску по горизонтали:
                    else
                        rez = find_gor(i, j_next, im, jm, n, m, Y, odd, Xmin);//- рекурсивный вызов
                    if (rez >= 0)//как бы обратный ход (в случае если круг замкнулся)
                    {
                        //для каждой ячейки цикла заполняем матрицу перерасчёта поставок:
                        if (odd % 2 == 0) Y[i, j_next] = Y[im, jm];//эти прибавляются
                        else Y[i, j_next] = -Y[im, jm];//эти вычитаются
                        break;
                    }
                    else //откат назад в случае неудачи (круг не замкнулся):
                    {
                        odd--;
                        if (Xmin_old >= 0)//если мы изменяли Xmin на этой итерации
                            Xmin = Xmin_old;
                    }
                }
            return rez;
        }
    }
}
