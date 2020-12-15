using System;
using System.Collections.Generic;

namespace Tubes
{
    public class TubeField
    {
        private List<RowIndexMap> map = new List<RowIndexMap>();
        private static Random rand = new Random();
        public Tube[] tubes;
        private int field_width;
        public TubeField(int FIELD_WIDTH)
        {
            field_width = FIELD_WIDTH;
            GenerateTubes();
            GenerateRowIndexMap();
        }
        private void GenerateTubes()
        {
            tubes = new Tube[field_width * field_width];
            for (int i = 0; i < (field_width * field_width); i++)
            {
                if (i == 0) { tubes[i] = new Tube(TubeType.Crane); tubes[i].hasWater = true; continue; }
                tubes[i] = GenerateTube();
            }
            tubes[24] = new Tube(TubeType.Empty);
        }
        private void GenerateRowIndexMap() 
        {
            var index = 0;
            for (int row = 0; row < field_width; row++) 
            {
                for (int j = 0; j < field_width; j++) 
                {
                    map.Add(new RowIndexMap(row, index));
                    index++;
                }
            }
;        }
        private Tube GenerateTube()
        {
            TubeType type = (TubeType)rand.Next(2, 7);
            return new Tube(type);
        }
        public void Rotate(int index)
        {
            if(index != 0 && index < tubes.Length - 1)
                tubes[index].Rotate();
        }

        public int TryMoveToNextTube(int index) 
        {
            int newIndex = -1;
            switch (tubes[index].waterComesTo) 
            {
                case WaterDirection.Up:
                {
                        newIndex = index - field_width;
                        tubes[newIndex].waterCameFrom = WaterDirection.Down;
                        break;
                }
                case WaterDirection.Down: 
                {
                        newIndex = index + field_width;
                        tubes[newIndex].waterCameFrom = WaterDirection.Up;
                        break;
                }
                case WaterDirection.Left:
                {
                        newIndex = index - 1;
                        tubes[newIndex].waterCameFrom = WaterDirection.Right;
                        break;
                }
                case WaterDirection.Right:
                {
                        newIndex = index + 1;
                        tubes[newIndex].waterCameFrom = WaterDirection.Left;
                        break;
                }
                default: 
                    return -1;
            }

            if (index < 0 || index > (field_width * field_width) - 1)
                return -1;

            if (!CheckNewIndex(index, newIndex) || newIndex == -1)
                return -1;

            tubes[newIndex].SetWaterDirection();
            return newIndex;
        }
        private bool CheckNewIndex(int index, int newIndex) 
        {
            // Если двигаемся влево или вправо, то обязаны оставаться на одной строке
            if (tubes[index].waterComesTo == WaterDirection.Left ||
                tubes[index].waterComesTo == WaterDirection.Right)
            {
                if (map[index].row == map[newIndex].row)
                    return true;
                else return false;
            }
            else return true;
        }
    }
}
