
using System.Numerics;

using Raylib_cs;




using static Raylib_cs.Raylib;

public class Util()
{


    bool CheckLinesCollision(Vector2 l1Start, Vector2 l1End, Vector2 l2Start, Vector2 l2End)
    {
        float x1 = l1Start.X;
        float x2 = l1End.X;
        float x3 = l2Start.X;
        float x4 = l2End.X;

        float y1 = l1Start.Y;
        float y2 = l1End.Y;
        float y3 = l2Start.Y;
        float y4 = l2End.Y;

        float uA = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));

        float uB = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3)) / ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));

        if (uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1)
        {
            return true;
        }
        return false;

    }


    Vector2 GetLinesCollision(Vector2 l1Start, Vector2 l1End, Vector2 l2Start, Vector2 l2End)
    {
        float x1 = l1Start.X;
        float x2 = l1End.X;
        float x3 = l2Start.X;
        float x4 = l2End.X;

        float y1 = l1Start.Y;
        float y2 = l1End.Y;
        float y3 = l2Start.Y;
        float y4 = l2End.Y;

        float uA = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));

        float uB = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3)) / ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));


        return new(x1 + (uA * (x2 - x1)), y1 + (uA * (y2 - y1)));
    }


    bool CheckRecLineCollsion(Rectangle rec, Vector2 lStart, Vector2 lEnd)
    {
        Vector2 recLineStart = new(rec.X, rec.Y);
        Vector2 recLineEnd = new(rec.X + rec.Width, rec.Y);

        if (CheckLinesCollision(lStart, lEnd, recLineStart, recLineEnd))
            return true;


        recLineEnd = new(rec.X, rec.Y + rec.Height);

        if (CheckLinesCollision(lStart, lEnd, recLineStart, recLineEnd))
            return true;

        recLineStart = new(rec.X + rec.Width, rec.Y + rec.Height);


        if (CheckLinesCollision(lStart, lEnd, recLineStart, recLineEnd))
            return true;

        recLineEnd = new(rec.X + rec.Width, rec.Y);

        if (CheckLinesCollision(lStart, lEnd, recLineStart, recLineEnd))
            return true;

        return false;
    }


    //Yo fucker this function is gonna return the collision on the face of the rectangle which is CLOSEST TO THE lStart!!!!
    Vector2 GetRecLineCollision(Rectangle rec, Vector2 lStart, Vector2 lEnd)
    {
        Vector2 pt;
        List<Vector2> tests = new List<Vector2>();


        Vector2 recLineStart = new(rec.X, rec.Y);
        Vector2 recLineEnd = new(rec.X + rec.Width, rec.Y);

        if (CheckLinesCollision(lStart, lEnd, recLineStart, recLineEnd))
            tests.Add(GetLinesCollision(lStart, lEnd, recLineStart, recLineEnd));


        recLineEnd = new(rec.X, rec.Y + rec.Height);

        if (CheckLinesCollision(lStart, lEnd, recLineStart, recLineEnd))
            tests.Add(GetLinesCollision(lStart, lEnd, recLineStart, recLineEnd));

        recLineStart = new(rec.X + rec.Width, rec.Y + rec.Height);


        if (CheckLinesCollision(lStart, lEnd, recLineStart, recLineEnd))
            tests.Add(GetLinesCollision(lStart, lEnd, recLineStart, recLineEnd));

        recLineEnd = new(rec.X + rec.Width, rec.Y);

        if (CheckLinesCollision(lStart, lEnd, recLineStart, recLineEnd))
            tests.Add(GetLinesCollision(lStart, lEnd, recLineStart, recLineEnd));


        pt = tests[1];
        foreach (Vector2 test in tests)
        {
            if (Math.Sqrt(((pt.X - lStart.X) * (pt.X - lStart.X)) + ((pt.Y - lStart.Y) * (pt.Y - lStart.Y))) >
                Math.Sqrt(((test.X - lStart.X) * (test.X - lStart.X)) + ((test.Y - lStart.Y) * (test.Y - lStart.Y))))
                pt = test;
        }

        return pt;
    }
}