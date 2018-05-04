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
            Player player = new Player(new Vector2i(1, 1), playerTexture);

            MapFile map = Content.Load<MapFile>("Maps/test");
            MapManager.Instance.SaveMap("test", map);

            SceneManger.Instance.AddScene("TestingScene", new PathTestingScene(MapManager.Instance.GetMap("test"), player));
            SceneManger.Instance.SetCurrent("TestingScene");
        }

        protected void Begin() {
            SceneManger.Instance.Begin();
        }

        protected void Input(GameTime gameTime) {
            InputManager.Instance.ProcessInput();

            SceneManger.Instance.Input(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
        }

        protected override void Update(GameTime gameTime) {
            if (first) {
                first = false;
                Begin();
            }

            Input(gameTime);

            SceneManger.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SceneManger.Instance.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
