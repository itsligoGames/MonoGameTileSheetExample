using MonoTileSheetDisplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TileManagerNS
{
    public class TileManager
    {
        List<TileLayer> _layers = new List<TileLayer>();

        Tile _currentTile;

        public Tile CurrentTile
        {
            get { return _currentTile; }
            set { _currentTile = value; }
        }

        TileLayer _activeLayer;

        public TileLayer ActiveLayer
        {
            get { return _activeLayer; }
            set { _activeLayer = value; }
        }

        internal List<TileLayer> Layers
        {
            get { return _layers; }
            set { _layers = value; }
        }

        public TileLayer MakeLayer(string layerName, string[] tileNames, int[,] tileMap, List<TileRef> _tileRefs, int tileWidth, int tileHeight)
        {
            int tileMapHeight = tileMap.GetLength(0); // row int[row,col]
            int tileMapWidth = tileMap.GetLength(1); // dim 0 = row, dim 1 = col
            TileLayer layer = new TileLayer();
            layer.Tiles = new Tile[tileMapHeight, tileMapWidth];
            layer.TileRefs = _tileRefs;
            layer.Layername = layerName;
            layer.TileWidth = tileWidth;
            layer.TileHeight = tileHeight;

                for (int x = 0; x < tileMapWidth; x++)  // look at columns in row
                    for (int y = 0; y < tileMapHeight; y++) // look at rows
                    {
                        layer.Tiles[y, x] =
                            new Tile
                            {
                                X = x,
                                Y = y,
                                Id = tileMap[y, x],
                                TileName = tileNames[tileMap[y, x]],
                                TileWidth = layer.TileWidth,
                                TileHeight = layer.TileHeight,                    
                                Passable = true,
                                TileRef = layer.TileRefs[tileMap[y,x]]
                            };
                    }
                return layer;
        }

        public void addLayer(string layerName, string[] tileNames, int[,] tileMap, List<TileRef> _refs, int tileWidth, int tileHeight)
        {
            _layers.Add(MakeLayer(layerName, tileNames, tileMap, _refs,tileWidth,tileHeight));
        }

        public TileLayer getLayer(string name)
        {
            foreach (TileLayer layer in _layers)
                if (layer.Layername == name)
                    return layer;

            return null;
        }

        public bool deleteLayer(string name) 
        {
            TileLayer found = null;
            foreach (TileLayer layer in _layers)
                if (layer.Layername == name)
                {
                    found = layer;
                    break;
                }
            if (found == null) return false;
            else _layers.Remove(found);
            return true;

        }

    }

    public class Tile 
    {
        int _tileWidth;
        int _tileHeight;

        int _id;

        public int Id
        {
          get { return _id; }
          set { _id = value; }
        }
        string _tileName;

        bool _passable;

        public bool Passable
        {
            get { return _passable; }
            set { _passable = value; }
        }

        public string TileName
        {
          get { return _tileName; }
          set { _tileName = value; }
        }
        int _x;

        public int X
        {
          get { return _x; }
          set { _x = value; }
        }
        int _y;

        public int Y
        {
          get { return _y; }
          set { _y = value; }
        }

        public int TileWidth
        {
            get
            {
                return _tileWidth;
            }

            set
            {
                _tileWidth = value;
            }
        }

        public int TileHeight
        {
            get
            {
                return _tileHeight;
            }

            set
            {
                _tileHeight = value;
            }
        }
        

        // The tiles reference in the Tile sheet for drawing puposes
        public TileRef TileRef
        {
            get
            {
                return _tileRef;
            }

            set
            {
                _tileRef = value;
            }
        }

        private TileRef _tileRef;
    }

    public class TileLayer
    {
        string _layername;
        public int TileWidth;
        public int TileHeight;
        public string Layername
        {
            get { return _layername; }
            set { _layername = value; }
        }
        Tile[,] _tiles;
        public Tile[,] Tiles
        {
            get { return _tiles; }
            set { _tiles = value; }
        }
        public int MapWidth
        {
            get { return Tiles.GetLength(1); }
        }
        List<TileRef> _tileRefs;
        public int MapHeight
        {
            get { return Tiles.GetLength(0); }
        }

        public List<TileRef> TileRefs
        {
            get
            {
                return _tileRefs;
            }

            set
            {
                _tileRefs = value;
            }
        }

        // Check and see if the tile collection has valid adjacent 
        // Tile addresses (horizontal and vertical
        // return the a list of these tiles

        public List<Tile> adjacentTo(Tile t)
        {
            List<Tile> adjacentTiles = new List<Tile>();
            if (valid("above", t))
                adjacentTiles.Add(getadjacentTile("above", t));
            if (valid("below", t))
                adjacentTiles.Add(getadjacentTile("below", t));
            if (valid("left", t))
                adjacentTiles.Add(getadjacentTile("left", t));
            if (valid("right", t))
                adjacentTiles.Add(getadjacentTile("right", t));

            return adjacentTiles;

        }

        public List<Tile> adjacentImpassible(Tile t)
        {
            List<Tile> adjacentTilesImpassible = new List<Tile>();
            if (valid("above", t))
            {
                Tile tile = getadjacentTile("above", t);
                if (!tile.Passable) adjacentTilesImpassible.Add(tile);
            }
            if (valid("below", t))
            {
                Tile tile = getadjacentTile("below", t);
                if (!tile.Passable) adjacentTilesImpassible.Add(tile);
            }
            if (valid("left", t))
            {
                Tile tile = getadjacentTile("left", t);
                if (!tile.Passable) adjacentTilesImpassible.Add(tile);
            }
            if (valid("right", t))
            {
                Tile tile = getadjacentTile("right", t);
                if (!tile.Passable) adjacentTilesImpassible.Add(tile);
            }

            return adjacentTilesImpassible;

        }
        public List<Tile> adjacentPassible(Tile t)
        {
            List<Tile> adjacentTilesPassible = new List<Tile>();
            if (valid("above", t))
            {
                Tile tile = getadjacentTile("above", t);
                if (tile.Passable) adjacentTilesPassible.Add(tile);
            }
            if (valid("below", t))
            {
                Tile tile = getadjacentTile("below", t);
                if (tile.Passable) adjacentTilesPassible.Add(tile);
            }
            if (valid("left", t))
            {
                Tile tile = getadjacentTile("left", t);
                if (tile.Passable) adjacentTilesPassible.Add(tile);
            }
            if (valid("right", t))
            {
                Tile tile = getadjacentTile("right", t);
                if (tile.Passable) adjacentTilesPassible.Add(tile);
            }

            return adjacentTilesPassible;

        }

        public Tile getadjacentTile(string direction, Tile t)
        {
            switch (direction)
            {
                case "above":
                    if (t.Y - 1 >= 0)
                        return Tiles[t.Y-1,t.X];
                    break;
                case "below":
                    if (t.Y + 1 < this.MapHeight)
                        return Tiles[t.Y + 1, t.X];
                    break;
                case "left":
                    if (t.X - 1 >= 0)
                        return Tiles[t.Y, t.X - 1];
                    break;
                case "right":
                    if (t.X + 1 < this.MapWidth)
                        return Tiles[t.Y, t.X + 1];
                    break;
                default: return t;
            }
            return t;
        }
            
        public bool valid(string direction, Tile t)
        {
            switch (direction)
            {
                case "above":
                    if(t.Y -1 >= 0)
                    return true;
                    break;
                case "below":
                    if(t.Y + 1 < this.MapHeight)
                    return true;
                    break;
                case "left":
                    if(t.X -1 >= 0)
                        return true;
                        break;
                case "right":
                    if (t.X + 1 < this.MapWidth)
                        return true;
                        break;
                default: return false;
            }
            return false;
        }

        public void makeImpassable(string[] TileNames)
        {
            foreach (Tile t in this.Tiles)
                if (TileNames.Contains(t.TileName))
                    t.Passable = false;
        }
    }
}
