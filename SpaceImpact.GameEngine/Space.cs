using System.Collections.Generic;

namespace SpaceImpact.GameEngine
{
    public class Space
    {
        public List<int> Bounds { get; set; }
        public int MinWidth { get; private set; }
        public int MinHeight { get; private set; }
        public int MaxWidth { get; private set; }
        public int MaxHeight { get; private set; }

        public event Dispatcher.SpaceDrawing BordersDraw;

        public event Dispatcher.SpaceDrawing GameSpaceDraw;

        public void OnBordersDraw()
        {
            if (BordersDraw != null)
            {
                BordersDraw();
            }
        }

        public void OnGameSpaceDraw()
        {
            if (GameSpaceDraw != null)
            {
                GameSpaceDraw();
            }
        }

        public Space(int minWidth, int minHeight, int maxWidth,
                     int maxHeight, int widthBackdown, int heightUpBackdown, int heightDownBackdown)
        {
            MinWidth = minWidth;
            MinHeight = minHeight;
            MaxWidth = maxWidth;
            MaxHeight = maxHeight;
            Bounds = new List<int>();
            InitBounds(widthBackdown, heightUpBackdown, heightDownBackdown);
        }

        private void InitBounds(int widthBackdown, int heightUpBackdown, int heightDownBackdown)
        {
            
            Bounds.Add(MinWidth + widthBackdown);
            Bounds.Add(MaxWidth - widthBackdown);
            Bounds.Add(MinHeight + heightUpBackdown);
            Bounds.Add(MaxHeight - heightDownBackdown);
        }
    }
}