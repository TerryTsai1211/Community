namespace TwoDimensionArray
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] data = new int[,] // 也可以寫成 new int[2, 3]
                {
                    {0,1,2},
                    {3,4,5},
                };

            var iterator = new TwoDimensionArrayIterator<int>(data);
            while (iterator.HasNext())
            {
                var item = iterator.Next();
                Console.WriteLine(item.ToString()); // 應該顯示 0, 1, 2, 3, 4, 5
            }
        }
    }

    public interface IIterator<T>
    {
        bool HasNext();
        T Next();
    }

    public class TwoDimensionArrayIterator<T> : IIterator<T>
    {
        private readonly T[,] _data;
        private readonly int _colLength;

        private const int length_Index_Diff = 1;
        private int _currentIndex = 0;
        private int _rowIndex = 0;
        private int _colIndex = 0;

        public TwoDimensionArrayIterator(T[,] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            _data = data;

            int secondDimension = 1;
            _colLength = data.GetLength(secondDimension);
        }

        public bool HasNext()
        {
            bool hasNext = true;

            int index = _currentIndex++;
            if (index > _data.Length - length_Index_Diff)
                return !hasNext;

            _rowIndex = index / _colLength;
            _colIndex = index % _colLength;

            return hasNext;
        }

        public T Next()
        {
            return (T)_data[_rowIndex, _colIndex];
        }
    }
}