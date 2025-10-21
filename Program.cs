using System.Numerics;

using Raylib_cs;

namespace Castier;

using static Raylib_cs.Raylib;

internal static class Program
{
    public static void Main()
    {

        InitWindow(1920, 1080, "Castier");
        SetTargetFPS(60);



        Entity[] entities = { new Player(400, 200),
                              new Ground(new(0, 800, 700, 100)),
                              new Ground(new(1100, 800, 700, 100)),
                              new Ground(new(0, 500, 100, 700)),
                              new Ground(new(700,300, 400, 100 )),
                              new Ground(new(980,0,100,100)) };

        while (!WindowShouldClose())
        {
            BeginDrawing();
            ClearBackground(Color.White);



            foreach (Entity entity in entities)
            {
                entity.Draw();
            }

            EndDrawing();

            foreach (Entity entity in entities)
            {
                entity.Update(entities);
            }
        }

        CloseWindow();
    }
}






