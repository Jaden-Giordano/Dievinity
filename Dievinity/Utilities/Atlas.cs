using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dievinity.Utilities {
    class Atlas {

        private Texture2D texture;

        public int cellSize;

        public int Rows {
            get { return texture.Height / cellSize; }
        }
        public int Columns {
            get { return texture.Width / cellSize; }
        }

        public Atlas(Texture2D texture, int cellSize) {
            this.texture = texture;
            this.cellSize = cellSize;
        }

        public void Draw(SpriteBatch spriteBatch, int index, Vector2 position, int scale = 1) {
            int row = (int) ((float) index / (float) Columns);
            int column = index % Columns;

            Rectangle source = new Rectangle(cellSize * column, cellSize * row, cellSize, cellSize);
            Rectangle destination = new Rectangle((int)position.X * cellSize * scale, (int)position.Y * cellSize * scale, cellSize * scale, cellSize * scale);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            spriteBatch.Draw(texture, destination, source, Color.White);

            spriteBatch.End();
        }

    }
}
