using System.Numerics;
using Raylib_cs;

public class Program
{
    private static Slider base_len = new Slider(
            x: 20, y: 40, w: 150, h: 10, r: 10, min: 50f, max: 120f, "base len");

    private static Slider base_angle = new Slider(
            x: 20, y: 80, w: 150, h: 10, r: 10, min: 0f, max: MathF.PI * 0.5f, "base angle");

    private static Slider len_reduce = new Slider(
            x: 20, y: 120, w: 150, h: 10, r: 10, min: 0f, max: 1f, "len reduce", baseValue: 1f);

    private static Slider angle_reduce = new Slider(
            x: 20, y: 160, w: 150, h: 10, r: 10, min: 0f, max: 1f, "angle reduce", baseValue: 1f);

    private static Slider gens = new Slider(
            x: 20, y: 200, w: 150, h: 10, r: 10, min: 1f, max: 15f, "gens", baseValue: 0.5f);

    public static void Main(string[] args)
    {
        Vector2 root = new Vector2(512, 900);
        Vector2 split = root - new Vector2(0, base_len.Value);

        Raylib.InitWindow(1024, 1024, "Hello world");
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Raylib.DrawLineEx(root, split, 1f, Color.Green);
            Draw(split, 0f, 1);

            base_len.Update();
            base_angle.Update();
            len_reduce.Update();
            angle_reduce.Update();
            gens.Update();

            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }

    private static void Draw(Vector2 pos, float angle, int gen)
    {
        if (gen == (int)gens.Value)
            return;

        float len = base_len.Value * MathF.Pow(len_reduce.Value, gen);
        float delta_angle = 0.5f * base_angle.Value * MathF.Pow(angle_reduce.Value, gen);

        Vector2 point_1 = pos - len * Vector2.UnitY.Rotate(angle + delta_angle);
        Vector2 point_2 = pos - len * Vector2.UnitY.Rotate(angle - delta_angle);

        Raylib.DrawLineEx(pos, point_1, 1f, Color.Green);
        Raylib.DrawLineEx(pos, point_2, 1f, Color.Green);

        Draw(point_1, angle + delta_angle, gen + 1);
        Draw(point_2, angle - delta_angle, gen + 1);
    }
}
