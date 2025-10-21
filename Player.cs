using System.Numerics;

using Raylib_cs;

namespace Castier;

using static Raylib_cs.Raylib;

class Player(float xIn, float yIn) : Entity(new(xIn, yIn, 50, 50))
{

    Color color = Color.Green;
    Vector2 vel = new(0, 0);
    const float Gravity = 0.7f;
    bool castCollisionFound = false;
    Vector2 lastCastCoord = new(0, 0);


    readonly float maxVel = 50;

    public override void Draw()
    {
        DrawRectangleRec(bounds, color);
        
    }




    public override void Update(Entity[] entities)
    {


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


            /*
            if (IsMouseButtonPressed(MouseButton.Left))
            {
                //Vector2 swingPoint = 
                CheckSwing(entity);
                if (castCollisionFound)
                {

                    Vector2 swingPoint;
                    List<Vector2> swingEntpoints = new List<Vector2>();
                    foreach (Entity checkEnt in entities)
                    {

                        if (checkEnt == this)
                            continue;

                        CheckSwing(checkEnt);
                        if (castCollisionFound)
                        {
                            swingEntpoints.Add(CheckSwing(checkEnt));
                        }
                    }

                    if (swingEntpoints.Count >= 1)
                    {
                        swingPoint = swingEntpoints[0];
                        foreach (Vector2 collPoint in swingEntpoints)
                        {
                            if (collPoint.Y == swingEntpoints[0].Y && collPoint.X == swingEntpoints[0].Y)
                            {
                                break;
                            }

                            if (Math.Sqrt(((collPoint.X - bounds.X + (bounds.Width / 2)) * (collPoint.X - bounds.X + (bounds.Width / 2))) + ((collPoint.Y - bounds.Y + (bounds.Height / 2)) * (collPoint.Y - bounds.Y + (bounds.Height / 2)))) <
                                Math.Sqrt(((collPoint.X - swingPoint.X) * (collPoint.X - swingPoint.X)) + ((collPoint.Y - swingPoint.Y) * (collPoint.Y - swingPoint.Y))))
                            {
                                swingPoint = collPoint;
                            }
                        }
                        lastCastCoord = swingPoint;
                        
                        //DrawLineEx(new(bounds.X + (bounds.Width / 2), bounds.Y + (bounds.Height / 2)), swingPoint,40f, Color.Yellow);
                    }

                    
                }
            }
            */
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

    /*
    private Vector2 CheckSwing(Entity entity)
    {

        List<Vector2> collPoints = new List<Vector2>();


        Vector2 mousePos = GetMousePosition();


        Vector2 playerCenter = new(bounds.X + (bounds.Width / 2), bounds.Y + (bounds.Height / 2));
        Vector2 boundLinePointBottomLeft = new(entity.bounds.X, entity.bounds.Y + entity.bounds.Height);
        Vector2 boundLinePointBottomRight = new(entity.bounds.X + entity.bounds.Width, entity.bounds.Y + entity.bounds.Height);
        Vector2 boundLinePointTopLeft = new(entity.bounds.X, entity.bounds.Y);
        Vector2 boundLinePointTopRight = new(entity.bounds.X + entity.bounds.Width, entity.bounds.Y);


        float uABottom = ((boundLinePointBottomRight.X - boundLinePointBottomLeft.X) * (playerCenter.Y - boundLinePointBottomLeft.Y) - (boundLinePointBottomRight.Y - boundLinePointBottomLeft.Y) * (playerCenter.X - boundLinePointBottomLeft.X)) / ((boundLinePointBottomRight.Y - boundLinePointBottomLeft.Y) * (mousePos.X - playerCenter.X) - (boundLinePointBottomRight.X - boundLinePointBottomLeft.X) * (mousePos.Y - playerCenter.Y));
        float uBBottom = ((mousePos.X - playerCenter.X) * (playerCenter.Y - boundLinePointBottomLeft.Y) - (mousePos.Y - playerCenter.Y) * (playerCenter.X - boundLinePointBottomLeft.X)) / ((boundLinePointBottomRight.Y - boundLinePointBottomLeft.Y) * (mousePos.X - playerCenter.X) - (boundLinePointBottomRight.X - boundLinePointBottomLeft.X) * (mousePos.Y - playerCenter.Y));

        Vector2 collPointBottom = new(playerCenter.X + (uABottom * (mousePos.X - playerCenter.X)), playerCenter.Y + (uABottom * (mousePos.Y - playerCenter.Y)));

        if (uABottom >= 0 && uABottom <= 1 && uBBottom >= 0 && uBBottom <= 1)
        {

            collPoints.Add(collPointBottom);

        }
        else
        {
            //collision not found
        }


        float uATop = ((boundLinePointTopRight.X - boundLinePointTopLeft.X) * (playerCenter.Y - boundLinePointTopLeft.Y) - (boundLinePointTopRight.Y - boundLinePointTopLeft.Y) * (playerCenter.X - boundLinePointTopLeft.X)) / ((boundLinePointTopRight.Y - boundLinePointTopLeft.Y) * (mousePos.X - playerCenter.X) - (boundLinePointTopRight.X - boundLinePointTopLeft.X) * (mousePos.Y - playerCenter.Y));
        float uBTop = ((mousePos.X - playerCenter.X) * (playerCenter.Y - boundLinePointTopLeft.Y) - (mousePos.Y - playerCenter.Y) * (playerCenter.X - boundLinePointTopLeft.X)) / ((boundLinePointTopRight.Y - boundLinePointTopLeft.Y) * (mousePos.X - playerCenter.X) - (boundLinePointTopRight.X - boundLinePointTopLeft.X) * (mousePos.Y - playerCenter.Y));

        Vector2 collPointTop = new(playerCenter.X + (uATop * (mousePos.X - playerCenter.X)), playerCenter.Y + (uATop * (mousePos.Y - playerCenter.Y)));

        if (uATop >= 0 && uATop <= 1 && uBTop >= 0 && uBTop <= 1)
        {

            collPoints.Add(collPointTop);
        }
        else
        {
            //collision not found
        }

        float uALeft = ((boundLinePointBottomLeft.X - boundLinePointTopLeft.X) * (playerCenter.Y - boundLinePointTopLeft.Y) - (boundLinePointBottomLeft.Y - boundLinePointTopLeft.Y) * (playerCenter.X - boundLinePointTopLeft.X)) / ((boundLinePointBottomLeft.Y - boundLinePointTopLeft.Y) * (mousePos.X - playerCenter.X) - (boundLinePointBottomLeft.X - boundLinePointTopLeft.X) * (mousePos.Y - playerCenter.Y));
        float uBLeft = ((mousePos.X - playerCenter.X) * (playerCenter.Y - boundLinePointTopLeft.Y) - (mousePos.Y - playerCenter.Y) * (playerCenter.X - boundLinePointTopLeft.X)) / ((boundLinePointBottomLeft.Y - boundLinePointTopLeft.Y) * (mousePos.X - playerCenter.X) - (boundLinePointBottomLeft.X - boundLinePointTopLeft.X) * (mousePos.Y - playerCenter.Y));

        Vector2 collPointLeft = new(playerCenter.X + (uALeft * (mousePos.X - playerCenter.X)), playerCenter.Y + (uALeft * (mousePos.Y - playerCenter.Y)));

        if (uALeft >= 0 && uALeft <= 1 && uBLeft >= 0 && uBLeft <= 1)
        {

            collPoints.Add(collPointLeft);
        }
        else
        {
            //collision not found
        }

        float uARight = ((boundLinePointBottomRight.X - boundLinePointTopRight.X) * (playerCenter.Y - boundLinePointTopRight.Y) - (boundLinePointBottomRight.Y - boundLinePointTopRight.Y) * (playerCenter.X - boundLinePointTopRight.X)) / ((boundLinePointBottomRight.Y - boundLinePointTopRight.Y) * (mousePos.X - playerCenter.X) - (boundLinePointBottomRight.X - boundLinePointTopRight.X) * (mousePos.Y - playerCenter.Y));
        float uBRight = ((mousePos.X - playerCenter.X) * (playerCenter.Y - boundLinePointTopRight.Y) - (mousePos.Y - playerCenter.Y) * (playerCenter.X - boundLinePointTopRight.X)) / ((boundLinePointBottomRight.Y - boundLinePointTopRight.Y) * (mousePos.X - playerCenter.X) - (boundLinePointBottomRight.X - boundLinePointTopRight.X) * (mousePos.Y - playerCenter.Y));

        Vector2 collPointRight = new(playerCenter.X + (uARight * (mousePos.X - playerCenter.X)), playerCenter.Y + (uARight * (mousePos.Y - playerCenter.Y)));

        if (uARight >= 0 && uARight <= 1 && uBRight >= 0 && uBRight <= 1)
        {

            collPoints.Add(collPointRight);
        }
        else
        {
            //collision not found
        }


        if (collPoints.Count() > 1)
        {
            Vector2 goldenPoint = collPoints[0];
            foreach (Vector2 collPoint in collPoints)
            {
                if (collPoint.Y == collPoints[0].Y && collPoint.X == collPoints[0].Y)
                {
                    break;
                }

                if (Math.Sqrt(((collPoint.X - playerCenter.X) * (collPoint.X - playerCenter.X)) + ((collPoint.Y - playerCenter.Y) * (collPoint.Y - playerCenter.Y))) <
                    Math.Sqrt(((collPoint.X - goldenPoint.X) * (collPoint.X - goldenPoint.X)) + ((collPoint.Y - goldenPoint.Y) * (collPoint.Y - goldenPoint.Y))))
                {
                    goldenPoint = collPoint;
                }
            }

            castCollisionFound = true;
            return goldenPoint;
        }
        else if (collPoints.Count() == 1)
        {
            castCollisionFound = true;
            return collPoints[0];
        }
        else
        {
            //Console.WriteLine("Invalid swing location");
            castCollisionFound = false;
            return new();
        }

    }
    */


}

