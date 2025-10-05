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
                              new Ground(new(800, 400, 100, 200)) };

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



class Player(float xIn, float yIn) : Entity(new(xIn, yIn, 50, 50))
{

    Color color = Color.Green;
    Vector2 vel = new(0, 0);

    float maxVel = 50;

    public override void Draw()
    {
        DrawRectangleRec(bounds, color);
    }


    public override void Update(Entity[] entities)
    {




        if (IsKeyDown(KeyboardKey.W))
        {
            vel.Y -= 1;
        }
        if (IsKeyDown(KeyboardKey.A))
        {
            vel.X -= 1;
        }

        if (IsKeyDown(KeyboardKey.S))
        {
            vel.Y += 1;
        }
        if (IsKeyDown(KeyboardKey.D))
        {
            vel.X += 1;
        }
        if (IsKeyPressed(KeyboardKey.R))
        {
            vel = new(0, 0);
            bounds.X = 0;
            bounds.Y = 0;
        }

        foreach (Entity entity in entities)
            {

                if (CheckCollisionRecs(bounds, entity.bounds) && entity != this)
                {
                Rectangle collide = GetCollisionRec(bounds, entity.bounds);
                if (collide.Height > collide.Width)
                {
                    if (collide.X > bounds.X )
                    {
                        bounds.X -= collide.Width;
                    }

                    if (collide.X <= bounds.X )
                    {
                        bounds.X += collide.Width;
                    }
                }
                if (collide.Height <= collide.Width)
                {
                    if (collide.Y > bounds.Y )
                    {
                        bounds.Y -= collide.Height;
                    }

                    if (collide.Y <= bounds.Y )
                    {
                        bounds.Y += collide.Height;
                    }
                }

                vel = new(0, 0);

                
                }
            }


        if (vel.X > maxVel)
        {
            vel.X = maxVel;
        }
        if (vel.X < -maxVel)
        {
            vel.X = -maxVel;
        }
        if (vel.Y > maxVel)
        {
            vel.Y = maxVel;
        }
        if (vel.Y < -maxVel)
        {
            vel.Y = -maxVel;
        }


        bounds.Y += vel.Y;
        bounds.X += vel.X;


        

        
        
       
        
       

        
        



    }
}




class Ground(Rectangle boundsIn) : Entity(boundsIn)
{
    new Rectangle bounds = boundsIn;
    Color color = Color.Brown;

    public override void Draw()
    {
        DrawRectangleRec(bounds, color);
    }


}

public class Entity(Rectangle boundsIn)
{
    public Rectangle bounds = boundsIn;

    Color color = Color.Red;

    public virtual void Draw()
    {
        DrawRectangleRec(bounds, color);
    }

    public virtual void Update(Entity[] entities)
    {
        // Default does nothing
    }

}