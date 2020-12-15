namespace Tubes
{
    public struct RowIndexMap
    {
        public int row { get; set; }
        public int index { get; set; }
        public RowIndexMap(int r, int i) { row = r; index = i; }
    };
}
