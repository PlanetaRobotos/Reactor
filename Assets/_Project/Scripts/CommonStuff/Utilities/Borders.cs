using _Project.Scripts.SettingsStuff;

namespace _Project.Scripts.Mechanics
{
    public class Borders
    {
        public Borders()
        {
            XBorder = MinMaxFloat.Create(0f, 0f);
            YBorder = MinMaxFloat.Create(0f, 0f);
        }

        public Borders(MinMaxFloat xBorder, MinMaxFloat yBorder)
        {
            XBorder = xBorder;
            YBorder = yBorder;
        }

        public MinMaxFloat XBorder { get; }

        public MinMaxFloat YBorder { get; }
        
        public static Borders Create(MinMaxFloat xBorder, MinMaxFloat yBorder) => new Borders(xBorder, yBorder);
    }
}