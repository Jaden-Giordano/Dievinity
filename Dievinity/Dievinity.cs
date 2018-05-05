using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Dievinity.Utilities;
using Dievinity.Managers;
using Dievinity.Scenes;
using MapFileProcessorLib;
using Dievinity.Entities;
using Dievinity.Managers.Input;

namespace Dievinity {
    public class Dievinity : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        bool first = true;

        public Dievinity() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            Texture2D floorTexture = Content.Load<Texture2D>("Atlases/Ground");
            AtlasManager.Instance.SaveAtlas("Ground", new Atlas(floorTexture, 16));

            Texture2D wallTexture = Content.Load<Texture2D>("Atlases/Walls");
            AtlasManager.Instance.SaveAtlas("Walls", new Atlas(wallTexture, 16));

            Texture2D debugTexture = Content.Load<Texture2D>("Atlases/Debug");
            AtlasManager.Instance.SaveAtlas("Debug", new Atlas(debugTexture, 16));

            Texture2D playerTexture = Content.Load<Texture2D>("Entities/Player");
            Texture2D enemyTexture = Content.Load<Texture2D>("Entities/Enemy");

            MapFile map = Content.Load<MapFile>("Maps/larger");
            MapManager.Instance.SaveMap("test", map);

            TurnBasedScene turnBased = new TurnBasedScene(MapManager.Instance.GetMap("test"));
            Player player = new Player(turnBased, new Point(1, 1), playerTexture);
            Enemy enemy = new Enemy(turnBased, new Point(5, 4), enemyTexture);

            turnBased.AddEntities(new Entity[] { player, enemy });

            SceneManager.Instance.AddScene("TurnBasedScene", turnBased);
            SceneManager.Instance.SetCurrent("TurnBasedScene");
        }

        protected void Begin() {
            SceneManager.Instance.Begin();
        }

        protected void Input(GameTime gameTime) {
            InputManager.Instance.ProcessInput();

            SceneManager.Instance.Input(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
        }

        protected override void Update(GameTime gameTime) {
            if (first) {
                first = false;
                Begin();
            }

            Input(gameTime);

            SceneManager.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SceneManager.Instance.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
