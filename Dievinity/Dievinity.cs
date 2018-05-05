using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Dievinity.Graphics;
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
            AtlasManager.Instance.SaveAtlas("Ground", floorTexture);

            Texture2D wallTexture = Content.Load<Texture2D>("Atlases/Walls");
            AtlasManager.Instance.SaveAtlas("Walls", wallTexture);

            Texture2D ghostTexture = Content.Load<Texture2D>("Atlases/Ghost");
            AtlasManager.Instance.SaveAtlas("Ghost", ghostTexture);

            Texture2D playerTexture = Content.Load<Texture2D>("Entities/Player");
            Texture2D enemyTexture = Content.Load<Texture2D>("Entities/Enemy");

            MapFile map = Content.Load<MapFile>("Maps/larger");

            TurnBasedScene turnBased = new TurnBasedScene();
            MapManager.Instance.SaveMap("test", turnBased, map);
            turnBased.SetMap(MapManager.Instance.GetMap("test"));

            Player player = new Player(turnBased, new Point(2, 2), playerTexture);
            Enemy enemy = new Enemy(turnBased, new Point(5, 4), enemyTexture);
            Enemy enemy2 = new Enemy(turnBased, new Point(5, 1), enemyTexture);

            turnBased.AddEntities(new Entity[] { player, enemy, enemy2 });

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

            SceneManager.Instance.Draw();

            Renderer.Instance.DrawAllSprites(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
