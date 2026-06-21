namespace UnfathomableBattleship.Forms;

public class GameCanvas : IDisposable
{
    private bool _isLeftMouseDown;
    private bool _wasLeftMouseDown;

    public event EventHandler<Point> TileClicked = delegate { };

    private Point? MousePosition { get; set; }

    public Point? CursorPosition
    {
        get
        {
            if (MousePosition is not Point position) return null;

            var mouseTileX = position.X / GameForm.TileDimension;
            var mouseTileY = position.Y / GameForm.TileDimension;
            return new Point(mouseTileX, mouseTileY);
        }
    }

    private PictureBox PictureBox { get; }
    private Bitmap Bitmap { get; set; }
    public Graphics Graphics { get; private set; }

    public GameCanvas(PictureBox pictureBox, Size size)
    {
        PictureBox = pictureBox;
        PictureBox.Resize += (s, e) => Rebuild();
        PictureBox.Width = size.Width * GameForm.TileDimension;
        PictureBox.Height = size.Height * GameForm.TileDimension;
        PictureBox.MouseMove += MouseMove;
        PictureBox.MouseDown += MouseDown;
        PictureBox.MouseUp += MouseUp;
        PictureBox.MouseLeave += MouseLeave;

        Rebuild();
    }

    private void Rebuild()
    {
        if (PictureBox.Width <= 0 || PictureBox.Height <= 0) return;

        Graphics.Dispose();
        Bitmap.Dispose();

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
            _isLeftMouseDown = true;
        }
    }

    private void MouseUp(object? sender, MouseEventArgs eventArgs)
    {
        if (eventArgs.Button == MouseButtons.Left)
        {
            _isLeftMouseDown = false;
        }
    }

    private void MouseLeave(object? sender, EventArgs eventArgs)
    {
        MousePosition = null;
        _isLeftMouseDown = false;
    }

    public void Present()
    {
        PictureBox.Image = Bitmap;
    }

    public void Update()
    {
        if (_isLeftMouseDown && !_wasLeftMouseDown)
        {
            if (CursorPosition is Point position)
            {
                TileClicked.Invoke(this, position);
            }
        }

        _wasLeftMouseDown = _isLeftMouseDown;
    }

    public void Dispose()
    {
        Graphics.Dispose();
        Bitmap.Dispose();
        GC.SuppressFinalize(this);
    }
}
