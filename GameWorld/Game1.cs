using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameWorld
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Camera camera;
        Map level;
        Enemy enemy1, enemy2;
        List<Enemy> enemies;
        Map map;
        Player player;
        //bool isDeadly;
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
            
            map = new Map();
            enemies = new List<Enemy>();
            enemy1 = new Enemy();
            enemy2 = new Enemy();
            player = new Player();
            initiateEnemies();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 

        private void initiateEnemies()
        {
            enemy1.position = new Vector2(900, 380);
            enemy2.position = new Vector2(100, 380);
            enemy1.speed = 1f;
            enemy2.speed = 2f;
            enemies.Add(enemy1);
            enemies.Add(enemy2);
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            IsMouseVisible = true;
            //enemy = new Enemy(Content.Load<Texture2D>("Player"), new Vector2(900, 384), 1000);
        
            Tiles.Content = Content;
            Texture2D blokText = Content.Load<Texture2D>("Tile1");
            camera = new Camera(GraphicsDevice.Viewport);

            /*map.Generate(new int[,]{
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,1,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,1,2,1,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,1,2,2,0,1,0,0,0,0,0,0,0},
                { 0,1,1,0,0,1,2,2,2,0,0,0,0,0,0,0,1,0},
                { 1,2,2,3,3,2,2,2,2,2,2,2,3,2,1,3,2,1},
               
            }, 64);*/

            map.Generate(new int[,]{
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0},
                { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},

            }, 64);



            /*  level = new Map();
              level.texture = blokText;
              level.CreateWorld();*/

            player.Load(Content);
            foreach (Enemy enemy in enemies)
            {

                enemy.Load(Content);
            }
            //enemy.Load(Content);
            // TODO: use this.Content to load your game content here
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
            player.Update(gameTime);
            foreach (Enemy enemy in enemies)
            {
                enemy.Update(gameTime, player);
            }
            //enemy.Update(gameTime, player);
            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                player.Collision(tile.Rectangle, map.Width, map.Height, tile.isDeadly);
                foreach (Enemy enemy in enemies)
                {
                    enemy.Collision(tile.Rectangle, map.Width, map.Height);
                }
                //enemy.Collision(tile.Rectangle, map.Width, map.Height);
                camera.Update(player.Position, map.Width, map.Height);
            }
            foreach (Enemy enemy in enemies)
            { 
            player.checkEnemyCollision(enemy);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null,camera.Transform);
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
            //enemy.Draw(spriteBatch);
            map.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
