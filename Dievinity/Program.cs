using System;

namespace Dievinity {
    public static class Program {
        [STAThread]
        static void Main() {
            using (var game = new Dievinity())
                game.Run();
        }
    }
}
