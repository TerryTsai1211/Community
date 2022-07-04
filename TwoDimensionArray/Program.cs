namespace TwoDimensionArray
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Demo01();
            
            // Demo02();
        }

        private static void Demo01()
        {
            int[,] data = new int[,] // 也可以寫成 new int[2, 3]
                {
                    {0,1,2},
                    {3,4,5},
                };

            Console.WriteLine("------ HasNext 搭配 Next ------");

            var iterator = new TwoDimensionArrayIterator<int>(data);
            while (iterator.HasNext())
            {
                var item = iterator.Next();
                Console.WriteLine(item.ToString()); // 應該顯示 0, 1, 2, 3, 4, 5
            }

            Console.WriteLine("------ 單獨呼叫 Next ------");

            var iterator2 = new TwoDimensionArrayIterator<int>(data);
            // 因為是單獨呼叫 Next 所以只要跑到 i 超過 6，就會超出陣列拋出 Exception
            for (int i = 0; i <= 5; i++)
            {
                var item = iterator2.Next();
                Console.WriteLine(item.ToString()); // 應該顯示 0, 1, 2, 3, 4, 5
            }
        }

        private static void Demo02()
        {
            int[,] data = new int[,]
            {
                {0,1,2},
                {3,4,5},
            };

            var helper = new LocationHelper<int>(data);
            var current = helper.SetCurrentLocation(1, 1); // 設定目前位置在[1,1],value=4

            Console.WriteLine("------ AllenKuoTestCase ------");
            // AllenKuoTestCase1：
            ArrayItem<int> item1 = helper.MoveRight(1); // 傳回一個表示[1,2], value=5的物件
            Console.WriteLine(item1?.Location.ToString()); // 在這裡,必需能顯示item1 所在位置
            Console.WriteLine(item1?.Value); // 顯示其值,5

            // AllenKuoTestCase2：
            current = helper.SetCurrentLocation(0, 2); // 設定目前位置在[0,2],value=2
            bool canMoveUp = helper.CanMoveUp(); // 傳回 false, 因為位置[0,2]已經無法再向上移動
            bool canMoveRight = helper.CanMoveRight(); // 傳回 false, 因為位置[0,2]已經無法再向右移動
            Console.WriteLine($"canMoveUp：{canMoveUp}");
            Console.WriteLine($"canMoveRight：{canMoveRight}");

            Console.WriteLine("------ TestCase ------");
            // TestCase3：向下
            current = helper.SetCurrentLocation(0, 1);
            Console.WriteLine(helper.CanMoveDown());
            current = helper.MoveDown();
            Console.WriteLine(current?.Location.ToString());
            Console.WriteLine(current?.Value);

            // TestCase４：向左
            current = helper.SetCurrentLocation(0, 1);
            Console.WriteLine(helper.CanMoveLeft());
            current = helper.MoveLeft();
            Console.WriteLine(current?.Location.ToString());
            Console.WriteLine(current?.Value);
        }
    }

    #region 20220618.10 第二題實作一個可以在二維陣列裡移動位置的功能
    public struct Location
    {
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }

        public override string ToString()
        {
            return $"RowIndex：{RowIndex}-ColumnIndex：{ColumnIndex}";
        }
    }

    public class ArrayItem<T>
    {
        public Location Location { get; set; }
        public T Value { get; set; }
    }

    public class LocationHelper<T>
    {
        private T[,] data;
        private Location currentLocation;

        private const int _length_Index_Diff = 1;
        private readonly int _rowLength;
        private readonly int _columnLength;
        private readonly int _rowMaxIndex;
        private readonly int _columnMaxIndex;

        public LocationHelper(T[,] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            this.data = data;

            int firstDimension = 0;
            _rowLength = data.GetLength(firstDimension);
            _rowMaxIndex = _rowLength - _length_Index_Diff;

            int secondDimension = 1;
            _columnLength = data.GetLength(secondDimension);
            _columnMaxIndex = _columnLength - _length_Index_Diff;
        }

        #region SetCurrentLocation 相關
        public ArrayItem<T> SetCurrentLocation(int newRowIndex, int newColumnIndex)
        {
            if (IsInArray(newRowIndex, newColumnIndex) == false)
                return null;

            Location newLocation = new Location() { RowIndex = newRowIndex, ColumnIndex = newColumnIndex };
            this.currentLocation = newLocation;

            return new ArrayItem<T>()
            {
                Location = newLocation,
                Value = (T)data[newRowIndex, newColumnIndex]
            };
        }

        public ArrayItem<T> SetCurrentLocation(Location location)
        => SetCurrentLocation(location.RowIndex, location.ColumnIndex);

        private bool IsInArray(int rowIndex, int colIndex)
        {
            bool inArray = true;

            if (rowIndex < 0 ||
                rowIndex > _rowMaxIndex)
                return !inArray;

            if (colIndex < 0 ||
                colIndex > _columnMaxIndex)
                return !inArray;

            return inArray;
        }
        #endregion

        #region Move 相關
        public bool CanMoveRight(int offsetColumn = 1)
        {
            return (currentLocation.ColumnIndex + offsetColumn <= _columnMaxIndex);
        }

        public ArrayItem<T> MoveRight(int offsetColumn = 1)
        {
            if (CanMoveRight(offsetColumn) == false)
                return null;

            return SetCurrentLocation(currentLocation.RowIndex, currentLocation.ColumnIndex + offsetColumn);
        }

        public bool CanMoveLeft(int offsetColumn = 1)
        {
            return (currentLocation.ColumnIndex - offsetColumn >= 0);
        }

        public ArrayItem<T> MoveLeft(int offsetColumn = 1)
        {
            if (CanMoveLeft(offsetColumn) == false)
                return null;

            return SetCurrentLocation(currentLocation.RowIndex, currentLocation.ColumnIndex - offsetColumn);
        }

        public bool CanMoveUp(int offsetRow = 1)
        {
            if (currentLocation.RowIndex - offsetRow < 0)
                return false;

            return (currentLocation.RowIndex - offsetRow < _rowMaxIndex);
        }

        public ArrayItem<T> MoveUp(int offsetRow = 1)
        {
            if (CanMoveRight(offsetRow) == false)
                return null;

            return SetCurrentLocation(currentLocation.RowIndex + offsetRow, currentLocation.ColumnIndex);
        }

        public bool CanMoveDown(int offsetRow = 1)
        {
            return (currentLocation.RowIndex + offsetRow <= _rowMaxIndex);
        }

        public ArrayItem<T> MoveDown(int offsetRow = 1)
        {
            if (CanMoveDown(offsetRow) == false)
                return null;

            return SetCurrentLocation(currentLocation.RowIndex + offsetRow, currentLocation.ColumnIndex);
        }
        #endregion
    } 
    #endregion

    #region 20220618.10 寫一個 iterator class ,能簡單的遍歷每一個元素
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
            return (_currentIndex <= _data.Length - length_Index_Diff);
        }

        public T Next()
        {
            int index = _currentIndex++;
            int rowIndex = index / _colLength;
            int colIndex = index % _colLength;

            return (T)_data[rowIndex, colIndex];
        }
    }
    #endregion

}