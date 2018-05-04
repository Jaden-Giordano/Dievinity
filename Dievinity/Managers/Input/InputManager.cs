using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Dievinity.Managers.Input {
    public class InputManager {

        private static InputManager instance;
        public static InputManager Instance {
            get {
                if (instance == null) {
                    instance = new InputManager();
                }

                return instance;
            }
        }

        private List<Keys> keysJustPressed;
        private List<Keys> keysDown;
        private List<Keys> keysJustReleased;

        private List<MouseButtons> mouseJustPressed;
        private List<MouseButtons> mouseDown;
        private List<MouseButtons> mouseJustReleased;

        private InputManager() {
            keysJustPressed = new List<Keys>();
            keysDown = new List<Keys>();
            keysJustReleased = new List<Keys>();

            mouseJustPressed = new List<MouseButtons>();
            mouseDown = new List<MouseButtons>();
            mouseJustReleased = new List<MouseButtons>();
        }

        public void ProcessInput() {
            keysJustPressed.Clear();
            keysJustReleased.Clear();

            Keys[] pressed = Keyboard.GetState().GetPressedKeys();

            foreach (Keys i in pressed) {
                if (!keysDown.Contains(i)) {
                    keysJustPressed.Add(i);
                    keysDown.Add(i);
                }
            }

            List<Keys> remove = new List<Keys>();
            foreach (Keys i in keysDown) {
                if (Keyboard.GetState().IsKeyUp(i)) {
                    remove.Add(i);
                    keysJustReleased.Add(i);
                }
            }
            foreach (Keys i in remove) {
                keysDown.Remove(i);
            }

            mouseJustPressed.Clear();
            mouseJustReleased.Clear();

            ButtonState leftState = Mouse.GetState().LeftButton;
            if (leftState == ButtonState.Pressed && !mouseDown.Contains(MouseButtons.Left)) {
                mouseJustPressed.Add(MouseButtons.Left);
                mouseDown.Add(MouseButtons.Left);
            } else if (leftState == ButtonState.Released && mouseDown.Contains(MouseButtons.Left)) {
                mouseJustReleased.Add(MouseButtons.Left);
                mouseDown.Remove(MouseButtons.Left);
            }

            ButtonState rightState = Mouse.GetState().RightButton;
            if (rightState == ButtonState.Pressed && !mouseDown.Contains(MouseButtons.Right)) {
                mouseJustPressed.Add(MouseButtons.Right);
                mouseDown.Add(MouseButtons.Right);
            } else if (rightState == ButtonState.Released && mouseDown.Contains(MouseButtons.Right)) {
                mouseJustReleased.Add(MouseButtons.Right);
                mouseDown.Remove(MouseButtons.Right);
            }

            ButtonState middleState = Mouse.GetState().MiddleButton;
            if (middleState == ButtonState.Pressed && !mouseDown.Contains(MouseButtons.Middle)) {
                mouseJustPressed.Add(MouseButtons.Middle);
                mouseDown.Add(MouseButtons.Middle);
            } else if (middleState == ButtonState.Released && mouseDown.Contains(MouseButtons.Middle)) {
                mouseJustReleased.Add(MouseButtons.Middle);
                mouseDown.Remove(MouseButtons.Middle);
            }
        }

        public bool GetKeyPressed(Keys key) {
            return keysJustPressed.Contains(key);
        }

        public bool GetKeyReleased(Keys key) {
            return keysJustReleased.Contains(key);
        }

        public bool GetKeyDown(Keys key) {
            return keysDown.Contains(key);
        }

        public bool GetMousePressed(MouseButtons button) {
            return mouseJustPressed.Contains(button);
        }

        public bool GetMouseReleased(MouseButtons button) {
            return mouseJustReleased.Contains(button);
        }

        public bool GetMouseDown(MouseButtons button) {
            return mouseDown.Contains(button);
        }
    }
}
