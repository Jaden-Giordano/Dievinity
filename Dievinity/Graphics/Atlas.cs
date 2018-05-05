using Dievinity.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Dievinity.Graphics {
    public class Atlas : Sprite {

        private int cellSize;
        private int index;

        public int Rows {
            get { return texture.Height / cellSize; }
        }
        public int Columns {
            get { return texture.Width / cellSize; }
        }

        public Atlas(Texture2D texture, int cellSize, int index = 0, int order = 0) : base(texture, 1, 0) {
            this.cellSize = cellSize;
            this.index = index;
        }

        public Atlas(Texture2D texture, Vector2 position, int cellSize, int index = 0, int order = 0) : base(texture, position, order) {
            this.cellSize = cellSize;
            this.index = index;
        }

        public Atlas(Texture2D texture, Vector2 position, int cellSize, int index = 0, float scale = 1, int order = 0) : base(texture, position, scale, order) {
            this.cellSize = cellSize;
            this.index = index;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            int row = (int) ((float) index / (float) Columns);
            int column = index % Columns;

            Rectangle source = new Rectangle(cellSize * column, cellSize * row, cellSize, cellSize);
            Rectangle destination = new Rectangle((int) (position.X * cellSize * scale), (int) (position.Y * cellSize * scale), (int) (cellSize * scale), (int) (cellSize * scale));

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            spriteBatch.Draw(texture, destination, source, color);

            spriteBatch.End();
        }

    }
}
