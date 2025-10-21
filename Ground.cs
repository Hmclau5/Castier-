using Raylib_cs;

namespace Castier;

using static Raylib_cs.Raylib;
public class Ground(Rectangle boundsIn) : Entity(boundsIn)
{
    new Rectangle bounds = boundsIn;
    Color color = Color.Brown;

    public override void Draw()
    {
        DrawRectangleRec(bounds, color);
    }


}