using Microsoft.Xna.Framework;

namespace Dievinity.Managers {
    public class Camera {

        private static Camera instance;
        public static Camera Instance {
            get {
                if (instance == null) {
                    instance = new Camera();
                }

                return instance;
            }
        }

        public Vector2 position;
        
        private Camera() {
            position = Vector2.Zero;
        }

    }
}
