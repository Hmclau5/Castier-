using Raylib_cs;

namespace Castier;

using static Raylib_cs.Raylib;


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