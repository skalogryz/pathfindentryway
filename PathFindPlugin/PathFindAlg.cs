using System;
using System.Collections.Generic;

namespace PathFindPlugin
{
    // чтобы не тянуть System.Drawing ради Point
    public struct Coord
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    // входные данные для алгоритма
    public class PathFindData
    {
        // поле, как одномерный массив 
        public int[] Field;
        public int Width;
        public int Height;
        // начало ищем
        public Coord Start;
        // конец
        public Coord End;
    }

    // ожидаемый результат
    public class PathFindResult
    {
        public IEnumerable<Coord> Path;
    }

    // алгоритм должен реализовать указанный метод 
    public interface IPathFindAlg
    {
        // метод FindPath должен заполенить структуру result - единственное поле Path 
        // если пройти из Start в End нельзя, то Path должен быть или Null или пустой
        void FindPath(PathFindData input, PathFindResult result);
    }
}
