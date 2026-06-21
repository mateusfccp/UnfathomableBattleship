using UnfathomableBattleship.Forms;

public class GameCanvas : IDisposable
{
    private bool isLeftMouseDown;
    private bool wasLeftMouseDown;

    public event EventHandler<Point> TileClicked;

    public Point? MousePosition { get; private set; }

    public Point? CursorPosition
    {
        get
        {
            if (MousePosition is Point position)
            {
                var mouseTileX = position.X / GameForm.TileSize;
                var mouseTileY = position.Y / GameForm.TileSize;
                return new Point(mouseTileX, mouseTileY);
            } else
            {
                return null;
            }
        }
    }

    public PictureBox PictureBox { get; }
    public Bitmap Bitmap { get; private set; }
    public Graphics Graphics { get; private set; }

    public GameCanvas(PictureBox pictureBox, Size size)
    {
        PictureBox = pictureBox;
        PictureBox.Resize += (s, e) => Rebuild();
        PictureBox.Width = size.Width * GameForm.TileSize;
        PictureBox.Height = size.Height * GameForm.TileSize;
        PictureBox.MouseMove += MouseMove;
        PictureBox.MouseDown += MouseDown;
        PictureBox.MouseUp += MouseUp;
        PictureBox.MouseLeave += MouseLeave;
        Rebuild();
    }

    public void Rebuild()
    {
        if (PictureBox.Width <= 0 || PictureBox.Height <= 0) return;

        Graphics?.Dispose();
        Bitmap?.Dispose();

        Bitmap = new Bitmap(PictureBox.Width, PictureBox.Height);
        Graphics = Graphics.FromImage(Bitmap);
    }

    private void MouseMove(object? sender, MouseEventArgs eventArgs)
    {
        MousePosition = eventArgs.Location;
    }

    private void MouseDown(object? sender, MouseEventArgs eventArgs)
    {
        if (eventArgs.Button == MouseButtons.Left)
        {
            isLeftMouseDown = true;
        }
    }

    private void MouseUp(object? sender, MouseEventArgs eventArgs)
    {
        if (eventArgs.Button == MouseButtons.Left)
        {
            isLeftMouseDown = false;
        }
    }

    private void MouseLeave(object? sender, EventArgs eventArgs)
    {
        MousePosition = null;
        isLeftMouseDown = false;
    }

    public void Present()
    {
        PictureBox.Image = Bitmap;
    }

    public void Update()
    {
        if (isLeftMouseDown && !wasLeftMouseDown)
        {
            if (CursorPosition is Point position)
            {
                TileClicked.Invoke(this, position);
            }
        }

        wasLeftMouseDown = isLeftMouseDown;
    }

    public void Dispose()
    {
        Graphics?.Dispose();
        Bitmap?.Dispose();
    }
}