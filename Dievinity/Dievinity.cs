using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Dievinity.Utilities;
using Dievinity.Maps;
using Dievinity.Managers;
using Dievinity.Maps.Pathing;
using MapFileProcessorLib;

namespace Dievinity {
    public class Dievinity : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Map world;
        Map debugWorld;

        bool first = true;

        public Dievinity() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            IsMouseVisible = true;

            world = new Map(20, 20);

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

            MapFile map = Content.Load<MapFile>("Maps/test");
            MapManager.Instance.SaveMap("test", map);
        }

        protected void Begin() {
            world = MapManager.Instance.GetMap("test");
            debugWorld = new Map(world.width, world.height);
        }

        protected override void Update(GameTime gameTime) {
            if (first) {
                first = false;
                Begin();
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Point mousePosition = Mouse.GetState().Position;

            Vector2i cell = new Vector2i(mousePosition.X / (16 * 4), mousePosition.Y / (16 * 4));

            PathFinder pf = new PathFinder(new Vector2i(1, 1), cell, world);
            Vector2i[] path = pf.FindPath();

            debugWorld.Clear();

            if (path != null) {
                foreach (Vector2i point in path) {
                    Tile debugTile = new Tile(AtlasManager.Instance.GetAtlas("Debug"), 0, point);
                    debugWorld.SetTile(point, debugTile);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            for (int x = -world.width / 2; x < world.width / 2; x += 1) {
                for (int y = -world.height / 2; y < world.height / 2; y += 1) {
                    Tile tile = world.GetTile(new Vector2i(x, y));
                    if (tile != null) {
                        tile.atlas.Draw(spriteBatch, tile.id, new Vector2(x, y), 4);
                    }

                    Tile debugTile = debugWorld.GetTile(new Vector2i(x, y));
                    if (debugTile != null) {
                        debugTile.atlas.Draw(spriteBatch, debugTile.id, new Vector2(x, y), 4);
                    }
                }
            }

            base.Draw(gameTime);
        }
    }
}
