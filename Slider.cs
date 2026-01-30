using System.Numerics;
using Raylib_cs;

public class Slider
{
    private Vector2 pos, extent;
    private float r, min, max;
    private Vector2 circlePos;
    private bool held;
    private string name;

    private static Slider? heldFixed;

    public Slider(float x, float y, float w, float h, float r, float min, float max, string name, float baseValue = 0.5f)
    {
        this.pos = new Vector2(x, y);
        this.extent = new Vector2(w, h);
        this.r = r;
        this.min = min;
        this.max = max;
        this.name = name;

        circlePos = new Vector2(x + baseValue * w, y + h * 0.5f);
    }

    public float Value => Single.Lerp(min, max, (circlePos.X - pos.X) / extent.X);

    public void Update()
    {
        Raylib.DrawRectanglePro(new Rectangle(pos, extent), Vector2.Zero, 0f, Color.Gray);
        Raylib.DrawCircleV(circlePos, r, Color.White);
        Raylib.DrawText(name, (int)pos.X, (int)(pos.Y - 20), 20, Color.White);

        if (!Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            if (heldFixed == this)
                heldFixed = null;
            held = false;
            return;
        }

        Vector2 mousePos = Raylib.GetMousePosition();

        if (!held)
        {
            if (heldFixed == null &&
                ((pos.X <= mousePos.X && mousePos.X <= pos.X + extent.X &&
                pos.Y <= mousePos.Y && mousePos.Y <= pos.Y + extent.Y) ||
                Vector2.Distance(mousePos, circlePos) < r))
            {
                heldFixed = this;
                held = true;
            }
            else
                return;
        }

        float y = pos.Y + extent.Y * 0.5f;
        float x = Math.Clamp(mousePos.X, pos.X, pos.X + extent.X);
        circlePos = new Vector2(x, y);
    }
}

