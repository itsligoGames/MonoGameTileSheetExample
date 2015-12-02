using AnimatedSprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TileManagerNS;
using System;
using Engine.Engines;

namespace MonoTileSheetDisplay
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int tileWidth = 64;
        int tileHeight = 64;
        PlayerWithWeapon player;
        Vector2 ViewportCentre
        {
            get
            {
                return new Vector2(GraphicsDevice.Viewport.Width / 2,
                GraphicsDevice.Viewport.Height / 2);
            }
        }

        int[,] tileMap = new int[,]
    {
        {1,3,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
        {1,2,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
        {1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
        {1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2},
        {2,2,2,2,2,2,0,0,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2},
        {2,2,2,2,2,2,0,0,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2},
        {2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,2,0,0,0,0,0,2,0,0,0,0,2,0,0,0,2,0,0,0,2,2,2,2,2,2,2,2,2},
        {2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {2,2,2,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
        {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
        {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
        {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
        {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
        {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
        {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,2,2,2,1,1,2,2,2,2,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
        {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
        {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
        {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
        {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
    };
        Texture2D _tileSheet;
        List<TileRef> _tileRefs = new List<TileRef>();
        private RotatingSprite enemy;
        private Camera cam;
        private TileManager _tManager;
        private SpriteFont _font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _tManager = new TileManager();
            cam = new Camera(ViewportCentre,
                new Vector2(tileMap.GetLength(1) * tileWidth,
                                tileMap.GetLength(0) * tileHeight));
            new InputEngine(this);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _tileSheet = Content.Load<Texture2D>(@"Tiles\tank tiles 64 x 64");
            _font = Content.Load<SpriteFont>("message");
            _tileRefs.Add(new TileRef(4, 2, 0));
            _tileRefs.Add(new TileRef(3, 3, 1));
            _tileRefs.Add(new TileRef(6, 3, 2));
            _tileRefs.Add(new TileRef(6, 4, 3));
            setupPlayer();
            setupEnemy();

            string[] backTileNames = { "free", "pavement", "ground","blue" };
            string[] impassibleTiles = { "free", "ground","blue" };
            string[] hiddenTileNames = { "NONE", "chest", "key" };

            _tManager.addLayer("background", backTileNames, tileMap,_tileRefs, tileWidth,tileHeight);
            _tManager.ActiveLayer = _tManager.getLayer("background");
            _tManager.ActiveLayer.makeImpassable(impassibleTiles);
            player.CurrentPlayerTile = _tManager.CurrentTile = _tManager.ActiveLayer.Tiles[0, 0];
            
            

            // TODO: use this.Content to load your game content here
        }

        private void setupEnemy()
        {
            List<TileRef> enemyFrames = new List<TileRef>();
            enemyFrames.Add(new TileRef(20, 2, 0));
            enemyFrames.Add(new TileRef(20, 3, 0));
            enemyFrames.Add(new TileRef(20, 4, 0));
            enemyFrames.Add(new TileRef(20, 5, 0));
            enemyFrames.Add(new TileRef(20, 6, 0));
            enemyFrames.Add(new TileRef(20, 7, 0));
            enemyFrames.Add(new TileRef(20, 8, 0));

            enemy = new RotatingSprite(new Vector2(5, 5), enemyFrames, 64, 64, 1.0f);

        }

        private void setupPlayer()
        {
            List<TileRef> playerFrames = new List<TileRef>();
            playerFrames.Add(new TileRef(15, 0, 0));
            playerFrames.Add(new TileRef(16, 0, 0));
            playerFrames.Add(new TileRef(17, 0, 0));
            playerFrames.Add(new TileRef(18, 0, 0));
            playerFrames.Add(new TileRef(19, 0, 0));
            playerFrames.Add(new TileRef(20, 0, 0));
            playerFrames.Add(new TileRef(21, 0, 0));

            player = new PlayerWithWeapon(new Vector2(0, 0), new Vector2(tileMap.GetLength(1),tileMap.GetLength(0)),
                            playerFrames,
                            64, 64, 1.0f); // Default stopped

            player.setFrameSet(DIRECTION.UP, playerFrames);
            player.setFrameSet(DIRECTION.DOWN, 
                new List<TileRef> {
                    new TileRef(15, 1, 0),
                    new TileRef(16, 1, 0),
                    new TileRef(17, 1, 0),
                    new TileRef(18, 1, 0),
                    new TileRef(19, 1, 0),
                    new TileRef(20, 1, 0),
                    new TileRef(21, 1, 0),
            });
            player.setFrameSet(DIRECTION.LEFT,
                new List<TileRef> {
                    new TileRef(16, 2, 0),
                    new TileRef(16, 3, 0),
                    new TileRef(16, 4, 0),
                    new TileRef(16, 5, 0),
                    new TileRef(16, 6, 0),
                    new TileRef(16, 7, 0),
                    new TileRef(16, 8, 0),
            });
            player.setFrameSet(DIRECTION.RIGHT,
                new List<TileRef> {
                    new TileRef(15, 2, 0),
                    new TileRef(15, 3, 0),
                    new TileRef(15, 4, 0),
                    new TileRef(15, 5, 0),
                    new TileRef(15, 6, 0),
                    new TileRef(15, 7, 0),
                    new TileRef(15, 8, 0),
            });
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //if(Keyboard.GetState().IsKeyDown(Keys.Enter))
            //{
            //    Tile CurrentTile = player.CurrentTilePostion;
            //    List<Tile> surround = _tManager.ActiveLayer.adjacentPassible(CurrentTile);
            //    player.CurrentTilePostion = surround[0];
            //    player.Tileposition = new Vector2(player.CurrentTilePostion.X, player.CurrentTilePostion.Y);
            //}
            // Update enemy animation 
            enemy.Update(gameTime);
            // Turn towards the player
            enemy.follow(player);
            // Update the player animation
            player.Update(gameTime);

            player.checkforMovement();
            if (player.MovingState == STATE.MOVING && player.Direction == DIRECTION.DOWN
                && !_tManager.ActiveLayer.getadjacentTile("below", player.CurrentPlayerTile).Passable)
                player.MovingState = STATE.STILL;

            else if (player.MovingState == STATE.MOVING && player.Direction == DIRECTION.UP
                && !_tManager.ActiveLayer.getadjacentTile("above", player.CurrentPlayerTile).Passable)
                player.MovingState = STATE.STILL;

            else if (player.MovingState == STATE.MOVING && player.Direction == DIRECTION.LEFT
                && !_tManager.ActiveLayer.getadjacentTile("left", player.CurrentPlayerTile).Passable)
                player.MovingState = STATE.STILL;

            else if (player.MovingState == STATE.MOVING && player.Direction == DIRECTION.RIGHT
                && !_tManager.ActiveLayer.getadjacentTile("right", player.CurrentPlayerTile).Passable)
                player.MovingState = STATE.STILL;
            if (_tManager.CurrentTile.X != player.CurrentPlayerTile.X || _tManager.CurrentTile.Y != player.CurrentPlayerTile.Y)
            {
                _tManager.CurrentTile.X = player.CurrentPlayerTile.X;
                _tManager.CurrentTile.Y = player.CurrentPlayerTile.Y;
            }
            cam.follow(player.PixelPosition,
                             GraphicsDevice.Viewport);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //spriteBatch.Begin();
            spriteBatch.Begin(SpriteSortMode.Immediate,
                    BlendState.AlphaBlend, null, null, null, null, cam.CurrentCameraTranslation);
                        for (int x = 0; x < tileMap.GetLength(1); x++)
                            for (int y = 0; y < tileMap.GetLength(0); y++)
                            {
                                // Get the Tile reference from the current value at y,x
                                TileRef tref = _tileRefs[tileMap[y, x]];
                                // Draw the part of the tile sheet at the current position converted to pixels
                                spriteBatch.Draw(_tileSheet,
                                    // destination rectangle
                                    new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight), 
                                    new Rectangle(tref._sheetPosX*tileWidth, tref._sheetPosY*tileHeight, 
                                                    tileWidth, tileHeight), // source rectangle
                                    Color.White);
                            }
            debugDraw();
            player.Draw(spriteBatch, _tileSheet);
            enemy.Draw(spriteBatch, _tileSheet);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void debugDraw()
        {
            //spriteBatch.DrawString(_font, "Frame " + player.CurrentFrame.ToString(), 
            //    player.PixelPosition + new Vector2(0, -(player.FrameHeight / 2)), 
            //    Color.White);
            //spriteBatch.DrawString(_font, "ctX: " + (_tManager.CurrentTile.X).ToString()
            //                + " Y: " + (_tManager.CurrentTile.Y).ToString(),
            //                player.PixelPosition + new Vector2(0, -(player.FrameHeight / 2)), Color.White);
            //spriteBatch.DrawString(_font, "tpX: " + ((int)player.Tileposition.X).ToString()
            //                            + " Y: " + ((int)player.Tileposition.Y).ToString(),
            //                            player.PixelPosition + new Vector2(0, (player.FrameHeight / 2)), Color.White);
            //spriteBatch.DrawString(_font, "pX: " + ((int)player.PixelPosition.X).ToString()
            //                            + " Y: " + ((int)player.PixelPosition.Y).ToString(),
            //                            player.PixelPosition, Color.White);

        }
    }
}
