namespace MonoTileSheetDisplay
{
    public  class TileRef
    {
        public int _sheetPosX;
        public int _sheetPosY;
        public int _tileMapValue;

        public TileRef(int x, int y, int val)
        {
            _sheetPosX = x;
            _sheetPosY = y;
            _tileMapValue = val;
        }
    }
}