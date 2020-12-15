using System.Windows.Controls;

namespace Tubes
{
    public enum WaterDirection 
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
    public enum TubeType
    {
        Empty,
        Crane,
        StraightVertical,
        StraightHorizontal,
        TurnDownRight,
        TurnDownLeft,
        TurnTopLeft,
        TurnTopRight,
        StraightVerticalW,
        StraightHorizontalW,
        TurnDownRightW,
        TurnDownLeftW,
        TurnTopLeftW,
        TurnTopRightW,
    }
    public class Tube
    {
        public Image image;
        public TubeType tubeType { get; set; }
        public WaterDirection waterCameFrom { get; set; } = WaterDirection.None;
        public WaterDirection waterComesTo { get; set; } = WaterDirection.None;
        public bool hasWater { get; set; } = false;
        public Tube(TubeType _tubeType) 
        {
            tubeType = _tubeType;
            image = ImageHolder.GetInstance().GetImage(tubeType);
        }
        public void SetWaterDirection() 
        {
            switch (waterCameFrom) 
            {
                case WaterDirection.None:
                    waterComesTo = WaterDirection.Down; break;
                case WaterDirection.Up:
                    if (tubeType == TubeType.StraightVertical)
                        waterComesTo = WaterDirection.Down;
                    else if (tubeType == TubeType.TurnTopLeft)
                        waterComesTo = WaterDirection.Left;
                    else if (tubeType == TubeType.TurnTopRight)
                        waterComesTo = WaterDirection.Right;
                    else waterComesTo = WaterDirection.None;
                    break;
                case WaterDirection.Down:
                    if (tubeType == TubeType.TurnDownLeft)
                        waterComesTo = WaterDirection.Left;
                    else if (tubeType == TubeType.TurnDownRight)
                        waterComesTo = WaterDirection.Right;
                    else if (tubeType == TubeType.StraightVertical)
                        waterComesTo = WaterDirection.Up;
                    else waterComesTo = WaterDirection.None;
                    break;
                case WaterDirection.Left:
                    if (tubeType == TubeType.TurnDownLeft)
                        waterComesTo = WaterDirection.Down;
                    else if (tubeType == TubeType.TurnTopLeft)
                        waterComesTo = WaterDirection.Up;
                    else if (tubeType == TubeType.StraightHorizontal)
                        waterComesTo = WaterDirection.Right;
                    else waterComesTo = WaterDirection.None;
                    break;
                case WaterDirection.Right:
                    if (tubeType == TubeType.TurnDownRight)
                        waterComesTo = WaterDirection.Down;
                    else if (tubeType == TubeType.TurnTopRight)
                        waterComesTo = WaterDirection.Right;
                    else if (tubeType == TubeType.StraightHorizontal)
                        waterComesTo = WaterDirection.Left;
                    else waterComesTo = WaterDirection.None;
                    break;
            }
        }
        public void Rotate() 
        {
            if (tubeType == TubeType.StraightHorizontal)
            {
                tubeType--;
            }
            else if (tubeType == TubeType.StraightVertical) 
            {
                tubeType++;
            }
            else if (tubeType == TubeType.TurnTopRight)
            {
                tubeType = TubeType.TurnDownRight;
            }
            else if ((int)tubeType != 0 && (int)tubeType != 1)
            {
                tubeType++;
            }
            UpdateDirectionImage();
        }
        public void SetWater() 
        {
            tubeType += 6;  // :)
            hasWater = true;
            UpdateDirectionImage();
        }
        private void UpdateDirectionImage() 
        {
            image = ImageHolder.GetInstance().GetImage(tubeType);
        }

    }
}
