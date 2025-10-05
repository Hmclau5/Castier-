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
                              new Ground(new(700,300, 400, 100 )) };

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
    const float Gravity = 0.7f;

    

    readonly float maxVel = 50;

    public override void Draw()
    {
        DrawRectangleRec(bounds, color);
    }


    public override void Update(Entity[] entities)
    {
        bool castCollisionFound = false;

        if (IsKeyDown(KeyboardKey.W))
        {
            vel.Y -= 1;
            bounds.Y -= 5;
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
                if (collide.Height > collide.Width && vel.X != 0)
                {
                    if (collide.X > bounds.X)
                    {
                        bounds.X -= collide.Width;
                    }

                    if (collide.X <= bounds.X)
                    {
                        bounds.X += collide.Width;
                    }
                    vel.X = -(.5f * vel.X);
                }
                if (collide.Height <= collide.Width && vel.Y != 0)
                {
                    if (collide.Y > bounds.Y)
                    {
                        bounds.Y -= collide.Height;
                    }

                    if (collide.Y <= bounds.Y)
                    {
                        bounds.Y += collide.Height;
                    }
                    vel.Y = -(.5f * vel.Y);
                }




            }

            if (IsMouseButtonDown(MouseButton.Left) && castCollisionFound == false)
            {
                Vector2 mousePos = GetMousePosition();

                //DrawLineV(new(bounds.X + (bounds.Width / 2), bounds.Y + (bounds.Height / 2)), mousePos, Color.Red);

                //Vector2* collisonPointRef;

                Vector2 playerCenter = new(bounds.X + (bounds.Width / 2), bounds.Y + (bounds.Height / 2));
                Vector2 boundLinePointBottomLeft = new(entity.bounds.X, entity.bounds.Y + entity.bounds.Height);
                Vector2 boundLinePointBottomRight = new(entity.bounds.X + entity.bounds.Width, entity.bounds.Y + entity.bounds.Height);

                float uA = ((boundLinePointBottomRight.X - boundLinePointBottomLeft.X) * (playerCenter.Y - boundLinePointBottomLeft.Y) - (boundLinePointBottomRight.Y - boundLinePointBottomLeft.Y) * (playerCenter.X - boundLinePointBottomLeft.X)) / ((boundLinePointBottomRight.Y - boundLinePointBottomLeft.Y) * (mousePos.X - playerCenter.X) - (boundLinePointBottomRight.X - boundLinePointBottomLeft.X) * (mousePos.Y - playerCenter.Y));
                float uB = ((mousePos.X - playerCenter.X) * (playerCenter.Y - boundLinePointBottomLeft.Y) - (mousePos.Y - playerCenter.Y) * (playerCenter.X - boundLinePointBottomLeft.X)) / ((boundLinePointBottomRight.Y - boundLinePointBottomLeft.Y) * (mousePos.X - playerCenter.X) - (boundLinePointBottomRight.X - boundLinePointBottomLeft.X) * (mousePos.Y - playerCenter.Y));

                Vector2 collPoint = new(playerCenter.X + (uA * (mousePos.X - playerCenter.X)), playerCenter.Y + (uA * (mousePos.Y - playerCenter.Y)));

                if (uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1)
                {
                    color = Color.Purple;
                    castCollisionFound = true;
                    DrawLineV(playerCenter, collPoint, Color.DarkGreen);
                }
                else
                {
                    color = Color.Green;
                    castCollisionFound = false;
                }

                //float intersectionX = playerCenter.X + (uA * (mousePos.X - playerCenter.X));
                //float intersectionY = playerCenter.Y + (uA * (mousePos.Y - playerCenter.Y));




                
                
                

            }

            if (!IsMouseButtonDown(MouseButton.Left))
            {
                color = Color.Green;
                castCollisionFound = false;
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


        vel.Y += Gravity;



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