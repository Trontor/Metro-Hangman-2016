using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MetroHangman
{
    internal abstract class ThemeContainer154 : ContainerControl
    {

        #region " Initialization "

        protected Graphics G;

        protected Bitmap B;
        public ThemeContainer154()
        {
            SetStyle((ControlStyles)139270, true);

            ImageSize = Size.Empty;
            Font = new Font("Verdana", 8);

            _measureBitmap = new Bitmap(1, 1);
            _measureGraphics = Graphics.FromImage(_measureBitmap);

            _drawRadialPath = new GraphicsPath();

            InvalidateCustimization();
        }

        protected override sealed void OnHandleCreated(EventArgs e)
        {
            if (_doneCreation)
                InitializeMessages();

            InvalidateCustimization();
            ColorHook();

            if (!(_lockWidth == 0))
                Width = _lockWidth;
            if (!(_lockHeight == 0))
                Height = _lockHeight;
            if (!_controlMode)
                base.Dock = DockStyle.Fill;

            Transparent = _transparent;
            if (_transparent && _backColor)
                BackColor = Color.Transparent;

            base.OnHandleCreated(e);
        }

        private bool _doneCreation;
        protected override sealed void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            if (Parent == null)
                return;
            IsParentForm = Parent is Form;

            if (!_controlMode)
            {
                InitializeMessages();

                if (IsParentForm)
                {
                    ParentForm.FormBorderStyle = _borderStyle;
                    ParentForm.TransparencyKey = _transparencyKey;

                    if (!DesignMode)
                    {
                        ParentForm.Shown += FormShown;
                    }
                }

                Parent.BackColor = BackColor;
            }

            OnCreation();
            _doneCreation = true;
            InvalidateTimer();
        }

        #endregion

        private void DoAnimation(bool i)
        {
            OnAnimation();
            if (i)
                Invalidate();
        }

        protected override sealed void OnPaint(PaintEventArgs e)
        {
            if (Width == 0 || Height == 0)
                return;

            if (_transparent && _controlMode)
            {
                PaintHook();
                e.Graphics.DrawImage(B, 0, 0);
            }
            else
            {
                G = e.Graphics;
                PaintHook();
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            ThemeShare.RemoveAnimationCallback(DoAnimation);
            base.OnHandleDestroyed(e);
        }

        private bool _hasShown;
        private void FormShown(object sender, EventArgs e)
        {
            if (_controlMode || _hasShown)
                return;

            if (_startPosition == FormStartPosition.CenterParent || _startPosition == FormStartPosition.CenterScreen)
            {
                Rectangle sb = Screen.PrimaryScreen.Bounds;
                Rectangle cb = ParentForm.Bounds;
                ParentForm.Location = new Point(sb.Width / 2 - cb.Width / 2, sb.Height / 2 - cb.Width / 2);
            }

            _hasShown = true;
        }


        #region " Size Handling "

        private Rectangle _frame;
        protected override sealed void OnSizeChanged(EventArgs e)
        {
            if (Movable && !_controlMode)
            {
                _frame = new Rectangle(7, 7, Width - 14, _header - 7);
            }

            InvalidateBitmap();
            Invalidate();

            base.OnSizeChanged(e);
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (!(_lockWidth == 0))
                width = _lockWidth;
            if (!(_lockHeight == 0))
                height = _lockHeight;
            base.SetBoundsCore(x, y, width, height, specified);
        }

        #endregion

        #region " State Handling "

        protected MouseState State;
        private void SetState(MouseState current)
        {
            State = current;
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!(IsParentForm && ParentForm.WindowState == FormWindowState.Maximized))
            {
                if (Sizable && !_controlMode)
                    InvalidateMouse();
            }

            base.OnMouseMove(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            if (Enabled)
                SetState(MouseState.None);
            else
                SetState(MouseState.Block);
            base.OnEnabledChanged(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            SetState(MouseState.Over);
            base.OnMouseEnter(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            SetState(MouseState.Over);
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            SetState(MouseState.None);

            if (GetChildAtPoint(PointToClient(MousePosition)) != null)
            {
                if (Sizable && !_controlMode)
                {
                    Cursor = Cursors.Default;
                    _previous = 0;
                }
            }

            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                SetState(MouseState.Down);

            if (!(IsParentForm && ParentForm.WindowState == FormWindowState.Maximized || _controlMode))
            {
                if (Movable && _frame.Contains(e.Location))
                {
                    Capture = false;
                    _wmLmbuttondown = true;
                    DefWndProc(ref _messages[0]);
                }
                else if (Sizable && !(_previous == 0))
                {
                    Capture = false;
                    _wmLmbuttondown = true;
                    DefWndProc(ref _messages[_previous]);
                }
            }

            base.OnMouseDown(e);
        }

        private bool _wmLmbuttondown;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (_wmLmbuttondown && m.Msg == 513)
            {
                _wmLmbuttondown = false;

                SetState(MouseState.Over);
                if (!SmartBounds)
                    return;

                if (IsParentMdi)
                {
                    CorrectBounds(new Rectangle(Point.Empty, Parent.Parent.Size));
                }
                else
                {
                    CorrectBounds(Screen.FromControl(Parent).WorkingArea);
                }
            }
        }

        private Point _getIndexPoint;
        private bool _b1;
        private bool _b2;
        private bool _b3;
        private bool _b4;
        private int GetIndex()
        {
            _getIndexPoint = PointToClient(MousePosition);
            _b1 = _getIndexPoint.X < 7;
            _b2 = _getIndexPoint.X > Width - 7;
            _b3 = _getIndexPoint.Y < 7;
            _b4 = _getIndexPoint.Y > Height - 7;

            if (_b1 && _b3)
                return 4;
            if (_b1 && _b4)
                return 7;
            if (_b2 && _b3)
                return 5;
            if (_b2 && _b4)
                return 8;
            if (_b1)
                return 1;
            if (_b2)
                return 2;
            if (_b3)
                return 3;
            if (_b4)
                return 6;
            return 0;
        }

        private int _current;
        private int _previous;
        private void InvalidateMouse()
        {
            _current = GetIndex();
            if (_current == _previous)
                return;

            _previous = _current;
            switch (_previous)
            {
                case 0:
                    Cursor = Cursors.Default;
                    break;
                case 1:
                case 2:
                    Cursor = Cursors.SizeWE;
                    break;
                case 3:
                case 6:
                    Cursor = Cursors.SizeNS;
                    break;
                case 4:
                case 8:
                    Cursor = Cursors.SizeNWSE;
                    break;
                case 5:
                case 7:
                    Cursor = Cursors.SizeNESW;
                    break;
            }
        }

        private readonly Message[] _messages = new Message[9];
        private void InitializeMessages()
        {
            _messages[0] = Message.Create(Parent.Handle, 161, new IntPtr(2), IntPtr.Zero);
            for (int I = 1; I <= 8; I++)
            {
                _messages[I] = Message.Create(Parent.Handle, 161, new IntPtr(I + 9), IntPtr.Zero);
            }
        }

        private void CorrectBounds(Rectangle bounds)
        {
            if (Parent.Width > bounds.Width)
                Parent.Width = bounds.Width;
            if (Parent.Height > bounds.Height)
                Parent.Height = bounds.Height;

            int x = Parent.Location.X;
            int y = Parent.Location.Y;

            if (x < bounds.X)
                x = bounds.X;
            if (y < bounds.Y)
                y = bounds.Y;

            int width = bounds.X + bounds.Width;
            int height = bounds.Y + bounds.Height;

            if (x + Parent.Width > width)
                x = width - Parent.Width;
            if (y + Parent.Height > height)
                y = height - Parent.Height;

            Parent.Location = new Point(x, y);
        }

        #endregion


        #region " Base Properties "

        public override DockStyle Dock
        {
            get { return base.Dock; }
            set
            {
                if (!_controlMode)
                    return;
                base.Dock = value;
            }
        }

        private bool _backColor;
        [Category("Misc")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                if (value == base.BackColor)
                    return;

                if (!IsHandleCreated && _controlMode && value == Color.Transparent)
                {
                    _backColor = true;
                    return;
                }

                base.BackColor = value;
                if (Parent != null)
                {
                    if (!_controlMode)
                        Parent.BackColor = value;
                    ColorHook();
                }
            }
        }

        public override Size MinimumSize
        {
            get { return base.MinimumSize; }
            set
            {
                base.MinimumSize = value;
                if (Parent != null)
                    Parent.MinimumSize = value;
            }
        }

        public override Size MaximumSize
        {
            get { return base.MaximumSize; }
            set
            {
                base.MaximumSize = value;
                if (Parent != null)
                    Parent.MaximumSize = value;
            }
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                Invalidate();
            }
        }

        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                Invalidate();
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color ForeColor
        {
            get { return Color.Empty; }
            set { }
        }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Image BackgroundImage
        {
            get { return null; }
            set { }
        }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ImageLayout BackgroundImageLayout
        {
            get { return ImageLayout.None; }
            set { }
        }

        #endregion

        #region " Public Properties "

        public bool SmartBounds { get; set; } = true;

        public bool Movable { get; set; } = true;

        public bool Sizable { get; set; } = true;

        private Color _transparencyKey;
        public Color TransparencyKey
        {
            get
            {
                if (IsParentForm && !_controlMode)
                    return ParentForm.TransparencyKey;
                return _transparencyKey;
            }
            set
            {
                if (value == _transparencyKey)
                    return;
                _transparencyKey = value;

                if (IsParentForm && !_controlMode)
                {
                    ParentForm.TransparencyKey = value;
                    ColorHook();
                }
            }
        }

        private FormBorderStyle _borderStyle;
        public FormBorderStyle BorderStyle
        {
            get
            {
                if (IsParentForm && !_controlMode)
                    return ParentForm.FormBorderStyle;
                return _borderStyle;
            }
            set
            {
                _borderStyle = value;

                if (IsParentForm && !_controlMode)
                {
                    ParentForm.FormBorderStyle = value;

                    if (!(value == FormBorderStyle.None))
                    {
                        Movable = false;
                        Sizable = false;
                    }
                }
            }
        }

        private FormStartPosition _startPosition;
        public FormStartPosition StartPosition
        {
            get
            {
                if (IsParentForm && !_controlMode)
                    return ParentForm.StartPosition;
                return _startPosition;
            }
            set
            {
                _startPosition = value;

                if (IsParentForm && !_controlMode)
                {
                    ParentForm.StartPosition = value;
                }
            }
        }

        private bool _noRounding;
        public bool NoRounding
        {
            get { return _noRounding; }
            set
            {
                _noRounding = value;
                Invalidate();
            }
        }

        private Image _image;
        public Image Image
        {
            get { return _image; }
            set
            {
                if (value == null)
                    ImageSize = Size.Empty;
                else
                    ImageSize = value.Size;

                _image = value;
                Invalidate();
            }
        }

        private readonly Dictionary<string, Color> _items = new Dictionary<string, Color>();
        public Bloom[] Colors
        {
            get
            {
                List<Bloom> T = new List<Bloom>();
                Dictionary<string, Color>.Enumerator e = _items.GetEnumerator();

                while (e.MoveNext())
                {
                    T.Add(new Bloom(e.Current.Key, e.Current.Value));
                }

                return T.ToArray();
            }
            set
            {
                foreach (Bloom b in value)
                {
                    if (_items.ContainsKey(b.Name))
                        _items[b.Name] = b.Value;
                }

                InvalidateCustimization();
                ColorHook();
                Invalidate();
            }
        }

        private string _customization;
        public string Customization
        {
            get { return _customization; }
            set
            {
                if (value == _customization)
                    return;

                byte[] data = null;
                Bloom[] items = Colors;

                try
                {
                    data = Convert.FromBase64String(value);
                    for (int I = 0; I <= items.Length - 1; I++)
                    {
                        items[I].Value = Color.FromArgb(BitConverter.ToInt32(data, I * 4));
                    }
                }
                catch
                {
                    return;
                }

                _customization = value;

                Colors = items;
                ColorHook();
                Invalidate();
            }
        }

        private bool _transparent;
        public bool Transparent
        {
            get { return _transparent; }
            set
            {
                _transparent = value;
                if (!(IsHandleCreated || _controlMode))
                    return;

                if (!value && !(BackColor.A == 255))
                {
                    throw new Exception("Unable to change value to false while a transparent BackColor is in use.");
                }

                SetStyle(ControlStyles.Opaque, !value);
                SetStyle(ControlStyles.SupportsTransparentBackColor, value);

                InvalidateBitmap();
                Invalidate();
            }
        }

        #endregion

        #region " Private Properties "

        protected Size ImageSize { get; private set; }

        protected bool IsParentForm { get; private set; }

        protected bool IsParentMdi
        {
            get
            {
                if (Parent == null)
                    return false;
                return Parent.Parent != null;
            }
        }

        private int _lockWidth;
        protected int LockWidth
        {
            get { return _lockWidth; }
            set
            {
                _lockWidth = value;
                if (!(LockWidth == 0) && IsHandleCreated)
                    Width = LockWidth;
            }
        }

        private int _lockHeight;
        protected int LockHeight
        {
            get { return _lockHeight; }
            set
            {
                _lockHeight = value;
                if (!(LockHeight == 0) && IsHandleCreated)
                    Height = LockHeight;
            }
        }

        private int _header = 24;
        protected int Header
        {
            get { return _header; }
            set
            {
                _header = value;

                if (!_controlMode)
                {
                    _frame = new Rectangle(7, 7, Width - 14, value - 7);
                    Invalidate();
                }
            }
        }

        private bool _controlMode;
        protected bool ControlMode
        {
            get { return _controlMode; }
            set
            {
                _controlMode = value;

                Transparent = _transparent;
                if (_transparent && _backColor)
                    BackColor = Color.Transparent;

                InvalidateBitmap();
                Invalidate();
            }
        }

        private bool _isAnimated;
        protected bool IsAnimated
        {
            get { return _isAnimated; }
            set
            {
                _isAnimated = value;
                InvalidateTimer();
            }
        }

        #endregion


        #region " Property Helpers "

        protected Pen GetPen(string name)
        {
            return new Pen(_items[name]);
        }
        protected Pen GetPen(string name, float width)
        {
            return new Pen(_items[name], width);
        }

        protected SolidBrush GetBrush(string name)
        {
            return new SolidBrush(_items[name]);
        }

        protected Color GetColor(string name)
        {
            return _items[name];
        }

        protected void SetColor(string name, Color value)
        {
            if (_items.ContainsKey(name))
                _items[name] = value;
            else
                _items.Add(name, value);
        }
        protected void SetColor(string name, byte r, byte g, byte b)
        {
            SetColor(name, Color.FromArgb(r, g, b));
        }
        protected void SetColor(string name, byte a, byte r, byte g, byte b)
        {
            SetColor(name, Color.FromArgb(a, r, g, b));
        }
        protected void SetColor(string name, byte a, Color value)
        {
            SetColor(name, Color.FromArgb(a, value));
        }

        private void InvalidateBitmap()
        {
            if (_transparent && _controlMode)
            {
                if (Width == 0 || Height == 0)
                    return;
                B = new Bitmap(Width, Height, PixelFormat.Format32bppPArgb);
                G = Graphics.FromImage(B);
            }
            else
            {
                G = null;
                B = null;
            }
        }

        private void InvalidateCustimization()
        {
            MemoryStream m = new MemoryStream(_items.Count * 4);

            foreach (Bloom b in Colors)
            {
                m.Write(BitConverter.GetBytes(b.Value.ToArgb()), 0, 4);
            }

            m.Close();
            _customization = Convert.ToBase64String(m.ToArray());
        }

        private void InvalidateTimer()
        {
            if (DesignMode || !_doneCreation)
                return;

            if (_isAnimated)
            {
                ThemeShare.AddAnimationCallback(DoAnimation);
            }
            else
            {
                ThemeShare.RemoveAnimationCallback(DoAnimation);
            }
        }

        #endregion


        #region " User Hooks "

        protected abstract void ColorHook();
        protected abstract void PaintHook();

        protected virtual void OnCreation()
        {
        }

        protected virtual void OnAnimation()
        {
        }

        #endregion


        #region " Offset "

        private Rectangle _offsetReturnRectangle;
        protected Rectangle Offset(Rectangle r, int amount)
        {
            _offsetReturnRectangle = new Rectangle(r.X + amount, r.Y + amount, r.Width - amount * 2, r.Height - amount * 2);
            return _offsetReturnRectangle;
        }

        private Size _offsetReturnSize;
        protected Size Offset(Size s, int amount)
        {
            _offsetReturnSize = new Size(s.Width + amount, s.Height + amount);
            return _offsetReturnSize;
        }

        private Point _offsetReturnPoint;
        protected Point Offset(Point p, int amount)
        {
            _offsetReturnPoint = new Point(p.X + amount, p.Y + amount);
            return _offsetReturnPoint;
        }

        #endregion

        #region " Center "


        private Point _centerReturn;
        protected Point Center(Rectangle p, Rectangle c)
        {
            _centerReturn = new Point(p.Width / 2 - c.Width / 2 + p.X + c.X, p.Height / 2 - c.Height / 2 + p.Y + c.Y);
            return _centerReturn;
        }
        protected Point Center(Rectangle p, Size c)
        {
            _centerReturn = new Point(p.Width / 2 - c.Width / 2 + p.X, p.Height / 2 - c.Height / 2 + p.Y);
            return _centerReturn;
        }

        protected Point Center(Rectangle child)
        {
            return Center(Width, Height, child.Width, child.Height);
        }
        protected Point Center(Size child)
        {
            return Center(Width, Height, child.Width, child.Height);
        }
        protected Point Center(int childWidth, int childHeight)
        {
            return Center(Width, Height, childWidth, childHeight);
        }

        protected Point Center(Size p, Size c)
        {
            return Center(p.Width, p.Height, c.Width, c.Height);
        }

        protected Point Center(int pWidth, int pHeight, int cWidth, int cHeight)
        {
            _centerReturn = new Point(pWidth / 2 - cWidth / 2, pHeight / 2 - cHeight / 2);
            return _centerReturn;
        }

        #endregion

        #region " Measure "

        private readonly Bitmap _measureBitmap;

        private readonly Graphics _measureGraphics;
        protected Size Measure()
        {
            lock (_measureGraphics)
            {
                return _measureGraphics.MeasureString(Text, Font, Width).ToSize();
            }
        }
        protected Size Measure(string text)
        {
            lock (_measureGraphics)
            {
                return _measureGraphics.MeasureString(text, Font, Width).ToSize();
            }
        }

        #endregion


        #region " DrawPixel "


        private SolidBrush _drawPixelBrush;
        protected void DrawPixel(Color c1, int x, int y)
        {
            if (_transparent)
            {
                B.SetPixel(x, y, c1);
            }
            else
            {
                _drawPixelBrush = new SolidBrush(c1);
                G.FillRectangle(_drawPixelBrush, x, y, 1, 1);
            }
        }

        #endregion

        #region " DrawCorners "


        private SolidBrush _drawCornersBrush;
        protected void DrawCorners(Color c1, int offset)
        {
            DrawCorners(c1, 0, 0, Width, Height, offset);
        }
        protected void DrawCorners(Color c1, Rectangle r1, int offset)
        {
            DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height, offset);
        }
        protected void DrawCorners(Color c1, int x, int y, int width, int height, int offset)
        {
            DrawCorners(c1, x + offset, y + offset, width - offset * 2, height - offset * 2);
        }

        protected void DrawCorners(Color c1)
        {
            DrawCorners(c1, 0, 0, Width, Height);
        }
        protected void DrawCorners(Color c1, Rectangle r1)
        {
            DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height);
        }
        protected void DrawCorners(Color c1, int x, int y, int width, int height)
        {
            if (_noRounding)
                return;

            if (_transparent)
            {
                B.SetPixel(x, y, c1);
                B.SetPixel(x + (width - 1), y, c1);
                B.SetPixel(x, y + (height - 1), c1);
                B.SetPixel(x + (width - 1), y + (height - 1), c1);
            }
            else
            {
                _drawCornersBrush = new SolidBrush(c1);
                G.FillRectangle(_drawCornersBrush, x, y, 1, 1);
                G.FillRectangle(_drawCornersBrush, x + (width - 1), y, 1, 1);
                G.FillRectangle(_drawCornersBrush, x, y + (height - 1), 1, 1);
                G.FillRectangle(_drawCornersBrush, x + (width - 1), y + (height - 1), 1, 1);
            }
        }

        #endregion

        #region " DrawBorders "

        protected void DrawBorders(Pen p1, int offset)
        {
            DrawBorders(p1, 0, 0, Width, Height, offset);
        }
        protected void DrawBorders(Pen p1, Rectangle r, int offset)
        {
            DrawBorders(p1, r.X, r.Y, r.Width, r.Height, offset);
        }
        protected void DrawBorders(Pen p1, int x, int y, int width, int height, int offset)
        {
            DrawBorders(p1, x + offset, y + offset, width - offset * 2, height - offset * 2);
        }

        protected void DrawBorders(Pen p1)
        {
            DrawBorders(p1, 0, 0, Width, Height);
        }
        protected void DrawBorders(Pen p1, Rectangle r)
        {
            DrawBorders(p1, r.X, r.Y, r.Width, r.Height);
        }
        protected void DrawBorders(Pen p1, int x, int y, int width, int height)
        {
            G.DrawRectangle(p1, x, y, width - 1, height - 1);
        }

        #endregion

        #region " DrawText "

        private Point _drawTextPoint;

        private Size _drawTextSize;
        protected void DrawText(Brush b1, HorizontalAlignment a, int x, int y)
        {
            DrawText(b1, Text, a, x, y);
        }
        protected void DrawText(Brush b1, string text, HorizontalAlignment a, int x, int y)
        {
            if (text.Length == 0)
                return;

            _drawTextSize = Measure(text);
            _drawTextPoint = new Point(Width / 2 - _drawTextSize.Width / 2, Header / 2 - _drawTextSize.Height / 2);

            switch (a)
            {
                case HorizontalAlignment.Left:
                    G.DrawString(text, Font, b1, x, _drawTextPoint.Y + y);
                    break;
                case HorizontalAlignment.Center:
                    G.DrawString(text, Font, b1, _drawTextPoint.X + x, _drawTextPoint.Y + y);
                    break;
                case HorizontalAlignment.Right:
                    G.DrawString(text, Font, b1, Width - _drawTextSize.Width - x, _drawTextPoint.Y + y);
                    break;
            }
        }

        protected void DrawText(Brush b1, Point p1)
        {
            if (Text.Length == 0)
                return;
            G.DrawString(Text, Font, b1, p1);
        }
        protected void DrawText(Brush b1, int x, int y)
        {
            if (Text.Length == 0)
                return;
            G.DrawString(Text, Font, b1, x, y);
        }

        #endregion

        #region " DrawImage "


        private Point _drawImagePoint;
        protected void DrawImage(HorizontalAlignment a, int x, int y)
        {
            DrawImage(_image, a, x, y);
        }
        protected void DrawImage(Image image, HorizontalAlignment a, int x, int y)
        {
            if (image == null)
                return;
            _drawImagePoint = new Point(Width / 2 - image.Width / 2, Header / 2 - image.Height / 2);

            switch (a)
            {
                case HorizontalAlignment.Left:
                    G.DrawImage(image, x, _drawImagePoint.Y + y, image.Width, image.Height);
                    break;
                case HorizontalAlignment.Center:
                    G.DrawImage(image, _drawImagePoint.X + x, _drawImagePoint.Y + y, image.Width, image.Height);
                    break;
                case HorizontalAlignment.Right:
                    G.DrawImage(image, Width - image.Width - x, _drawImagePoint.Y + y, image.Width, image.Height);
                    break;
            }
        }

        protected void DrawImage(Point p1)
        {
            DrawImage(_image, p1.X, p1.Y);
        }
        protected void DrawImage(int x, int y)
        {
            DrawImage(_image, x, y);
        }

        protected void DrawImage(Image image, Point p1)
        {
            DrawImage(image, p1.X, p1.Y);
        }
        protected void DrawImage(Image image, int x, int y)
        {
            if (image == null)
                return;
            G.DrawImage(image, x, y, image.Width, image.Height);
        }

        #endregion

        #region " DrawGradient "

        private LinearGradientBrush _drawGradientBrush;

        private Rectangle _drawGradientRectangle;
        protected void DrawGradient(ColorBlend blend, int x, int y, int width, int height)
        {
            _drawGradientRectangle = new Rectangle(x, y, width, height);
            DrawGradient(blend, _drawGradientRectangle);
        }
        protected void DrawGradient(ColorBlend blend, int x, int y, int width, int height, float angle)
        {
            _drawGradientRectangle = new Rectangle(x, y, width, height);
            DrawGradient(blend, _drawGradientRectangle, angle);
        }

        protected void DrawGradient(ColorBlend blend, Rectangle r)
        {
            _drawGradientBrush = new LinearGradientBrush(r, Color.Empty, Color.Empty, 90f);
            _drawGradientBrush.InterpolationColors = blend;
            G.FillRectangle(_drawGradientBrush, r);
        }
        protected void DrawGradient(ColorBlend blend, Rectangle r, float angle)
        {
            _drawGradientBrush = new LinearGradientBrush(r, Color.Empty, Color.Empty, angle);
            _drawGradientBrush.InterpolationColors = blend;
            G.FillRectangle(_drawGradientBrush, r);
        }


        protected void DrawGradient(Color c1, Color c2, int x, int y, int width, int height)
        {
            _drawGradientRectangle = new Rectangle(x, y, width, height);
            DrawGradient(c1, c2, _drawGradientRectangle);
        }
        protected void DrawGradient(Color c1, Color c2, int x, int y, int width, int height, float angle)
        {
            _drawGradientRectangle = new Rectangle(x, y, width, height);
            DrawGradient(c1, c2, _drawGradientRectangle, angle);
        }

        protected void DrawGradient(Color c1, Color c2, Rectangle r)
        {
            _drawGradientBrush = new LinearGradientBrush(r, c1, c2, 90f);
            G.FillRectangle(_drawGradientBrush, r);
        }
        protected void DrawGradient(Color c1, Color c2, Rectangle r, float angle)
        {
            _drawGradientBrush = new LinearGradientBrush(r, c1, c2, angle);
            G.FillRectangle(_drawGradientBrush, r);
        }

        #endregion

        #region " DrawRadial "

        private readonly GraphicsPath _drawRadialPath;
        private PathGradientBrush _drawRadialBrush1;
        private LinearGradientBrush _drawRadialBrush2;

        private Rectangle _drawRadialRectangle;
        public void DrawRadial(ColorBlend blend, int x, int y, int width, int height)
        {
            _drawRadialRectangle = new Rectangle(x, y, width, height);
            DrawRadial(blend, _drawRadialRectangle, width / 2, height / 2);
        }
        public void DrawRadial(ColorBlend blend, int x, int y, int width, int height, Point center)
        {
            _drawRadialRectangle = new Rectangle(x, y, width, height);
            DrawRadial(blend, _drawRadialRectangle, center.X, center.Y);
        }
        public void DrawRadial(ColorBlend blend, int x, int y, int width, int height, int cx, int cy)
        {
            _drawRadialRectangle = new Rectangle(x, y, width, height);
            DrawRadial(blend, _drawRadialRectangle, cx, cy);
        }

        public void DrawRadial(ColorBlend blend, Rectangle r)
        {
            DrawRadial(blend, r, r.Width / 2, r.Height / 2);
        }
        public void DrawRadial(ColorBlend blend, Rectangle r, Point center)
        {
            DrawRadial(blend, r, center.X, center.Y);
        }
        public void DrawRadial(ColorBlend blend, Rectangle r, int cx, int cy)
        {
            _drawRadialPath.Reset();
            _drawRadialPath.AddEllipse(r.X, r.Y, r.Width - 1, r.Height - 1);

            _drawRadialBrush1 = new PathGradientBrush(_drawRadialPath);
            _drawRadialBrush1.CenterPoint = new Point(r.X + cx, r.Y + cy);
            _drawRadialBrush1.InterpolationColors = blend;

            if (G.SmoothingMode == SmoothingMode.AntiAlias)
            {
                G.FillEllipse(_drawRadialBrush1, r.X + 1, r.Y + 1, r.Width - 3, r.Height - 3);
            }
            else
            {
                G.FillEllipse(_drawRadialBrush1, r);
            }
        }


        protected void DrawRadial(Color c1, Color c2, int x, int y, int width, int height)
        {
            _drawRadialRectangle = new Rectangle(x, y, width, height);
            DrawRadial(c1, c2, _drawGradientRectangle);
        }
        protected void DrawRadial(Color c1, Color c2, int x, int y, int width, int height, float angle)
        {
            _drawRadialRectangle = new Rectangle(x, y, width, height);
            DrawRadial(c1, c2, _drawGradientRectangle, angle);
        }

        protected void DrawRadial(Color c1, Color c2, Rectangle r)
        {
            _drawRadialBrush2 = new LinearGradientBrush(r, c1, c2, 90f);
            G.FillRectangle(_drawGradientBrush, r);
        }
        protected void DrawRadial(Color c1, Color c2, Rectangle r, float angle)
        {
            _drawRadialBrush2 = new LinearGradientBrush(r, c1, c2, angle);
            G.FillEllipse(_drawGradientBrush, r);
        }

        #endregion

        #region " CreateRound "

        private GraphicsPath _createRoundPath;

        private Rectangle _createRoundRectangle;
        public GraphicsPath CreateRound(int x, int y, int width, int height, int slope)
        {
            _createRoundRectangle = new Rectangle(x, y, width, height);
            return CreateRound(_createRoundRectangle, slope);
        }

        public GraphicsPath CreateRound(Rectangle r, int slope)
        {
            _createRoundPath = new GraphicsPath(FillMode.Winding);
            _createRoundPath.AddArc(r.X, r.Y, slope, slope, 180f, 90f);
            _createRoundPath.AddArc(r.Right - slope, r.Y, slope, slope, 270f, 90f);
            _createRoundPath.AddArc(r.Right - slope, r.Bottom - slope, slope, slope, 0f, 90f);
            _createRoundPath.AddArc(r.X, r.Bottom - slope, slope, slope, 90f, 90f);
            _createRoundPath.CloseFigure();
            return _createRoundPath;
        }

        #endregion

    }

    internal abstract class ThemeControl154 : Control
    {


        #region " Initialization "

        protected Graphics G;

        protected Bitmap B;
        public ThemeControl154()
        {
            SetStyle((ControlStyles)139270, true);

            ImageSize = Size.Empty;
            Font = new Font("Verdana", 8);

            _measureBitmap = new Bitmap(1, 1);
            _measureGraphics = Graphics.FromImage(_measureBitmap);

            _drawRadialPath = new GraphicsPath();

            InvalidateCustimization();
            //Remove?
        }

        protected override sealed void OnHandleCreated(EventArgs e)
        {
            InvalidateCustimization();
            ColorHook();

            if (!(_lockWidth == 0))
                Width = _lockWidth;
            if (!(_lockHeight == 0))
                Height = _lockHeight;

            Transparent = _transparent;
            if (_transparent && _backColor)
                BackColor = Color.Transparent;

            base.OnHandleCreated(e);
        }

        private bool _doneCreation;
        protected override sealed void OnParentChanged(EventArgs e)
        {
            if (Parent != null)
            {
                OnCreation();
                _doneCreation = true;
                InvalidateTimer();
            }

            base.OnParentChanged(e);
        }

        #endregion

        private void DoAnimation(bool i)
        {
            OnAnimation();
            if (i)
                Invalidate();
        }

        protected override sealed void OnPaint(PaintEventArgs e)
        {
            if (Width == 0 || Height == 0)
                return;

            if (_transparent)
            {
                PaintHook();
                e.Graphics.DrawImage(B, 0, 0);
            }
            else
            {
                G = e.Graphics;
                PaintHook();
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            ThemeShare.RemoveAnimationCallback(DoAnimation);
            base.OnHandleDestroyed(e);
        }

        #region " Size Handling "

        protected override sealed void OnSizeChanged(EventArgs e)
        {
            if (_transparent)
            {
                InvalidateBitmap();
            }

            Invalidate();
            base.OnSizeChanged(e);
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (!(_lockWidth == 0))
                width = _lockWidth;
            if (!(_lockHeight == 0))
                height = _lockHeight;
            base.SetBoundsCore(x, y, width, height, specified);
        }

        #endregion

        #region " State Handling "

        private bool _inPosition;
        protected override void OnMouseEnter(EventArgs e)
        {
            _inPosition = true;
            SetState(MouseState.Over);
            base.OnMouseEnter(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_inPosition)
                SetState(MouseState.Over);
            base.OnMouseUp(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                SetState(MouseState.Down);
            base.OnMouseDown(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _inPosition = false;
            SetState(MouseState.None);
            base.OnMouseLeave(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            if (Enabled)
                SetState(MouseState.None);
            else
                SetState(MouseState.Block);
            base.OnEnabledChanged(e);
        }

        protected MouseState State;
        private void SetState(MouseState current)
        {
            State = current;
            Invalidate();
        }

        #endregion


        #region " Base Properties "

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color ForeColor
        {
            get { return Color.Empty; }
            set { }
        }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Image BackgroundImage
        {
            get { return null; }
            set { }
        }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ImageLayout BackgroundImageLayout
        {
            get { return ImageLayout.None; }
            set { }
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                Invalidate();
            }
        }
        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                Invalidate();
            }
        }

        private bool _backColor;
        [Category("Misc")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                if (!IsHandleCreated && value == Color.Transparent)
                {
                    _backColor = true;
                    return;
                }

                base.BackColor = value;
                if (Parent != null)
                    ColorHook();
            }
        }

        #endregion

        #region " Public Properties "

        private bool _noRounding;
        public bool NoRounding
        {
            get { return _noRounding; }
            set
            {
                _noRounding = value;
                Invalidate();
            }
        }

        private Image _image;
        public Image Image
        {
            get { return _image; }
            set
            {
                if (value == null)
                {
                    ImageSize = Size.Empty;
                }
                else
                {
                    ImageSize = value.Size;
                }

                _image = value;
                Invalidate();
            }
        }

        private bool _transparent;
        public bool Transparent
        {
            get { return _transparent; }
            set
            {
                _transparent = value;
                if (!IsHandleCreated)
                    return;

                if (!value && !(BackColor.A == 255))
                {
                    throw new Exception("Unable to change value to false while a transparent BackColor is in use.");
                }

                SetStyle(ControlStyles.Opaque, !value);
                SetStyle(ControlStyles.SupportsTransparentBackColor, value);

                if (value)
                    InvalidateBitmap();
                else
                    B = null;
                Invalidate();
            }
        }

        private readonly Dictionary<string, Color> _items = new Dictionary<string, Color>();
        public Bloom[] Colors
        {
            get
            {
                List<Bloom> T = new List<Bloom>();
                Dictionary<string, Color>.Enumerator e = _items.GetEnumerator();

                while (e.MoveNext())
                {
                    T.Add(new Bloom(e.Current.Key, e.Current.Value));
                }

                return T.ToArray();
            }
            set
            {
                foreach (Bloom b in value)
                {
                    if (_items.ContainsKey(b.Name))
                        _items[b.Name] = b.Value;
                }

                InvalidateCustimization();
                ColorHook();
                Invalidate();
            }
        }

        private string _customization;
        public string Customization
        {
            get { return _customization; }
            set
            {
                if (value == _customization)
                    return;

                byte[] data = null;
                Bloom[] items = Colors;

                try
                {
                    data = Convert.FromBase64String(value);
                    for (int I = 0; I <= items.Length - 1; I++)
                    {
                        items[I].Value = Color.FromArgb(BitConverter.ToInt32(data, I * 4));
                    }
                }
                catch
                {
                    return;
                }

                _customization = value;

                Colors = items;
                ColorHook();
                Invalidate();
            }
        }

        #endregion

        #region " Private Properties "

        protected Size ImageSize { get; private set; }

        private int _lockWidth;
        protected int LockWidth
        {
            get { return _lockWidth; }
            set
            {
                _lockWidth = value;
                if (!(LockWidth == 0) && IsHandleCreated)
                    Width = LockWidth;
            }
        }

        private int _lockHeight;
        protected int LockHeight
        {
            get { return _lockHeight; }
            set
            {
                _lockHeight = value;
                if (!(LockHeight == 0) && IsHandleCreated)
                    Height = LockHeight;
            }
        }

        private bool _isAnimated;
        protected bool IsAnimated
        {
            get { return _isAnimated; }
            set
            {
                _isAnimated = value;
                InvalidateTimer();
            }
        }

        #endregion


        #region " Property Helpers "

        protected Pen GetPen(string name)
        {
            return new Pen(_items[name]);
        }
        protected Pen GetPen(string name, float width)
        {
            return new Pen(_items[name], width);
        }

        protected SolidBrush GetBrush(string name)
        {
            return new SolidBrush(_items[name]);
        }

        protected Color GetColor(string name)
        {
            return _items[name];
        }

        protected void SetColor(string name, Color value)
        {
            if (_items.ContainsKey(name))
                _items[name] = value;
            else
                _items.Add(name, value);
        }
        protected void SetColor(string name, byte r, byte g, byte b)
        {
            SetColor(name, Color.FromArgb(r, g, b));
        }
        protected void SetColor(string name, byte a, byte r, byte g, byte b)
        {
            SetColor(name, Color.FromArgb(a, r, g, b));
        }
        protected void SetColor(string name, byte a, Color value)
        {
            SetColor(name, Color.FromArgb(a, value));
        }

        private void InvalidateBitmap()
        {
            if (Width == 0 || Height == 0)
                return;
            B = new Bitmap(Width, Height, PixelFormat.Format32bppPArgb);
            G = Graphics.FromImage(B);
        }

        private void InvalidateCustimization()
        {
            MemoryStream m = new MemoryStream(_items.Count * 4);

            foreach (Bloom b in Colors)
            {
                m.Write(BitConverter.GetBytes(b.Value.ToArgb()), 0, 4);
            }

            m.Close();
            _customization = Convert.ToBase64String(m.ToArray());
        }

        private void InvalidateTimer()
        {
            if (DesignMode || !_doneCreation)
                return;

            if (_isAnimated)
            {
                ThemeShare.AddAnimationCallback(DoAnimation);
            }
            else
            {
                ThemeShare.RemoveAnimationCallback(DoAnimation);
            }
        }
        #endregion


        #region " User Hooks "

        protected abstract void ColorHook();
        protected abstract void PaintHook();

        protected virtual void OnCreation()
        {
        }

        protected virtual void OnAnimation()
        {
        }

        #endregion


        #region " Offset "

        private Rectangle _offsetReturnRectangle;
        protected Rectangle Offset(Rectangle r, int amount)
        {
            _offsetReturnRectangle = new Rectangle(r.X + amount, r.Y + amount, r.Width - amount * 2, r.Height - amount * 2);
            return _offsetReturnRectangle;
        }

        private Size _offsetReturnSize;
        protected Size Offset(Size s, int amount)
        {
            _offsetReturnSize = new Size(s.Width + amount, s.Height + amount);
            return _offsetReturnSize;
        }

        private Point _offsetReturnPoint;
        protected Point Offset(Point p, int amount)
        {
            _offsetReturnPoint = new Point(p.X + amount, p.Y + amount);
            return _offsetReturnPoint;
        }

        #endregion

        #region " Center "


        private Point _centerReturn;
        protected Point Center(Rectangle p, Rectangle c)
        {
            _centerReturn = new Point(p.Width / 2 - c.Width / 2 + p.X + c.X, p.Height / 2 - c.Height / 2 + p.Y + c.Y);
            return _centerReturn;
        }
        protected Point Center(Rectangle p, Size c)
        {
            _centerReturn = new Point(p.Width / 2 - c.Width / 2 + p.X, p.Height / 2 - c.Height / 2 + p.Y);
            return _centerReturn;
        }

        protected Point Center(Rectangle child)
        {
            return Center(Width, Height, child.Width, child.Height);
        }
        protected Point Center(Size child)
        {
            return Center(Width, Height, child.Width, child.Height);
        }
        protected Point Center(int childWidth, int childHeight)
        {
            return Center(Width, Height, childWidth, childHeight);
        }

        protected Point Center(Size p, Size c)
        {
            return Center(p.Width, p.Height, c.Width, c.Height);
        }

        protected Point Center(int pWidth, int pHeight, int cWidth, int cHeight)
        {
            _centerReturn = new Point(pWidth / 2 - cWidth / 2, pHeight / 2 - cHeight / 2);
            return _centerReturn;
        }

        #endregion

        #region " Measure "

        private readonly Bitmap _measureBitmap;
        //TODO: Potential issues during multi-threading.
        private readonly Graphics _measureGraphics;

        protected Size Measure()
        {
            return _measureGraphics.MeasureString(Text, Font, Width).ToSize();
        }
        protected Size Measure(string text)
        {
            return _measureGraphics.MeasureString(text, Font, Width).ToSize();
        }

        #endregion


        #region " DrawPixel "


        private SolidBrush _drawPixelBrush;
        protected void DrawPixel(Color c1, int x, int y)
        {
            if (_transparent)
            {
                B.SetPixel(x, y, c1);
            }
            else
            {
                _drawPixelBrush = new SolidBrush(c1);
                G.FillRectangle(_drawPixelBrush, x, y, 1, 1);
            }
        }

        #endregion

        #region " DrawCorners "


        private SolidBrush _drawCornersBrush;
        protected void DrawCorners(Color c1, int offset)
        {
            DrawCorners(c1, 0, 0, Width, Height, offset);
        }
        protected void DrawCorners(Color c1, Rectangle r1, int offset)
        {
            DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height, offset);
        }
        protected void DrawCorners(Color c1, int x, int y, int width, int height, int offset)
        {
            DrawCorners(c1, x + offset, y + offset, width - offset * 2, height - offset * 2);
        }

        protected void DrawCorners(Color c1)
        {
            DrawCorners(c1, 0, 0, Width, Height);
        }
        protected void DrawCorners(Color c1, Rectangle r1)
        {
            DrawCorners(c1, r1.X, r1.Y, r1.Width, r1.Height);
        }
        protected void DrawCorners(Color c1, int x, int y, int width, int height)
        {
            if (_noRounding)
                return;

            if (_transparent)
            {
                B.SetPixel(x, y, c1);
                B.SetPixel(x + (width - 1), y, c1);
                B.SetPixel(x, y + (height - 1), c1);
                B.SetPixel(x + (width - 1), y + (height - 1), c1);
            }
            else
            {
                _drawCornersBrush = new SolidBrush(c1);
                G.FillRectangle(_drawCornersBrush, x, y, 1, 1);
                G.FillRectangle(_drawCornersBrush, x + (width - 1), y, 1, 1);
                G.FillRectangle(_drawCornersBrush, x, y + (height - 1), 1, 1);
                G.FillRectangle(_drawCornersBrush, x + (width - 1), y + (height - 1), 1, 1);
            }
        }

        #endregion

        #region " DrawBorders "

        protected void DrawBorders(Pen p1, int offset)
        {
            DrawBorders(p1, 0, 0, Width, Height, offset);
        }
        protected void DrawBorders(Pen p1, Rectangle r, int offset)
        {
            DrawBorders(p1, r.X, r.Y, r.Width, r.Height, offset);
        }
        protected void DrawBorders(Pen p1, int x, int y, int width, int height, int offset)
        {
            DrawBorders(p1, x + offset, y + offset, width - offset * 2, height - offset * 2);
        }

        protected void DrawBorders(Pen p1)
        {
            DrawBorders(p1, 0, 0, Width, Height);
        }
        protected void DrawBorders(Pen p1, Rectangle r)
        {
            DrawBorders(p1, r.X, r.Y, r.Width, r.Height);
        }
        protected void DrawBorders(Pen p1, int x, int y, int width, int height)
        {
            G.DrawRectangle(p1, x, y, width - 1, height - 1);
        }

        #endregion

        #region " DrawText "

        private Point _drawTextPoint;

        private Size _drawTextSize;
        protected void DrawText(Brush b1, HorizontalAlignment a, int x, int y)
        {
            DrawText(b1, Text, a, x, y);
        }
        protected void DrawText(Brush b1, string text, HorizontalAlignment a, int x, int y)
        {
            if (text.Length == 0)
                return;

            _drawTextSize = Measure(text);
            _drawTextPoint = Center(_drawTextSize);

            switch (a)
            {
                case HorizontalAlignment.Left:
                    G.DrawString(text, Font, b1, x, _drawTextPoint.Y + y);
                    break;
                case HorizontalAlignment.Center:
                    G.DrawString(text, Font, b1, _drawTextPoint.X + x, _drawTextPoint.Y + y);
                    break;
                case HorizontalAlignment.Right:
                    G.DrawString(text, Font, b1, Width - _drawTextSize.Width - x, _drawTextPoint.Y + y);
                    break;
            }
        }

        protected void DrawText(Brush b1, Point p1)
        {
            if (Text.Length == 0)
                return;
            G.DrawString(Text, Font, b1, p1);
        }
        protected void DrawText(Brush b1, int x, int y)
        {
            if (Text.Length == 0)
                return;
            G.DrawString(Text, Font, b1, x, y);
        }

        #endregion

        #region " DrawImage "


        private Point _drawImagePoint;
        protected void DrawImage(HorizontalAlignment a, int x, int y)
        {
            DrawImage(_image, a, x, y);
        }
        protected void DrawImage(Image image, HorizontalAlignment a, int x, int y)
        {
            if (image == null)
                return;
            _drawImagePoint = Center(image.Size);

            switch (a)
            {
                case HorizontalAlignment.Left:
                    G.DrawImage(image, x, _drawImagePoint.Y + y, image.Width, image.Height);
                    break;
                case HorizontalAlignment.Center:
                    G.DrawImage(image, _drawImagePoint.X + x, _drawImagePoint.Y + y, image.Width, image.Height);
                    break;
                case HorizontalAlignment.Right:
                    G.DrawImage(image, Width - image.Width - x, _drawImagePoint.Y + y, image.Width, image.Height);
                    break;
            }
        }

        protected void DrawImage(Point p1)
        {
            DrawImage(_image, p1.X, p1.Y);
        }
        protected void DrawImage(int x, int y)
        {
            DrawImage(_image, x, y);
        }

        protected void DrawImage(Image image, Point p1)
        {
            DrawImage(image, p1.X, p1.Y);
        }
        protected void DrawImage(Image image, int x, int y)
        {
            if (image == null)
                return;
            G.DrawImage(image, x, y, image.Width, image.Height);
        }

        #endregion

        #region " DrawGradient "

        private LinearGradientBrush _drawGradientBrush;

        private Rectangle _drawGradientRectangle;
        protected void DrawGradient(ColorBlend blend, int x, int y, int width, int height)
        {
            _drawGradientRectangle = new Rectangle(x, y, width, height);
            DrawGradient(blend, _drawGradientRectangle);
        }
        protected void DrawGradient(ColorBlend blend, int x, int y, int width, int height, float angle)
        {
            _drawGradientRectangle = new Rectangle(x, y, width, height);
            DrawGradient(blend, _drawGradientRectangle, angle);
        }

        protected void DrawGradient(ColorBlend blend, Rectangle r)
        {
            _drawGradientBrush = new LinearGradientBrush(r, Color.Empty, Color.Empty, 90f);
            _drawGradientBrush.InterpolationColors = blend;
            G.FillRectangle(_drawGradientBrush, r);
        }
        protected void DrawGradient(ColorBlend blend, Rectangle r, float angle)
        {
            _drawGradientBrush = new LinearGradientBrush(r, Color.Empty, Color.Empty, angle);
            _drawGradientBrush.InterpolationColors = blend;
            G.FillRectangle(_drawGradientBrush, r);
        }


        protected void DrawGradient(Color c1, Color c2, int x, int y, int width, int height)
        {
            _drawGradientRectangle = new Rectangle(x, y, width, height);
            DrawGradient(c1, c2, _drawGradientRectangle);
        }
        protected void DrawGradient(Color c1, Color c2, int x, int y, int width, int height, float angle)
        {
            _drawGradientRectangle = new Rectangle(x, y, width, height);
            DrawGradient(c1, c2, _drawGradientRectangle, angle);
        }

        protected void DrawGradient(Color c1, Color c2, Rectangle r)
        {
            _drawGradientBrush = new LinearGradientBrush(r, c1, c2, 90f);
            G.FillRectangle(_drawGradientBrush, r);
        }
        protected void DrawGradient(Color c1, Color c2, Rectangle r, float angle)
        {
            _drawGradientBrush = new LinearGradientBrush(r, c1, c2, angle);
            G.FillRectangle(_drawGradientBrush, r);
        }

        #endregion

        #region " DrawRadial "

        private readonly GraphicsPath _drawRadialPath;
        private PathGradientBrush _drawRadialBrush1;
        private LinearGradientBrush _drawRadialBrush2;

        private Rectangle _drawRadialRectangle;
        public void DrawRadial(ColorBlend blend, int x, int y, int width, int height)
        {
            _drawRadialRectangle = new Rectangle(x, y, width, height);
            DrawRadial(blend, _drawRadialRectangle, width / 2, height / 2);
        }
        public void DrawRadial(ColorBlend blend, int x, int y, int width, int height, Point center)
        {
            _drawRadialRectangle = new Rectangle(x, y, width, height);
            DrawRadial(blend, _drawRadialRectangle, center.X, center.Y);
        }
        public void DrawRadial(ColorBlend blend, int x, int y, int width, int height, int cx, int cy)
        {
            _drawRadialRectangle = new Rectangle(x, y, width, height);
            DrawRadial(blend, _drawRadialRectangle, cx, cy);
        }

        public void DrawRadial(ColorBlend blend, Rectangle r)
        {
            DrawRadial(blend, r, r.Width / 2, r.Height / 2);
        }
        public void DrawRadial(ColorBlend blend, Rectangle r, Point center)
        {
            DrawRadial(blend, r, center.X, center.Y);
        }
        public void DrawRadial(ColorBlend blend, Rectangle r, int cx, int cy)
        {
            _drawRadialPath.Reset();
            _drawRadialPath.AddEllipse(r.X, r.Y, r.Width - 1, r.Height - 1);

            _drawRadialBrush1 = new PathGradientBrush(_drawRadialPath);
            _drawRadialBrush1.CenterPoint = new Point(r.X + cx, r.Y + cy);
            _drawRadialBrush1.InterpolationColors = blend;

            if (G.SmoothingMode == SmoothingMode.AntiAlias)
            {
                G.FillEllipse(_drawRadialBrush1, r.X + 1, r.Y + 1, r.Width - 3, r.Height - 3);
            }
            else
            {
                G.FillEllipse(_drawRadialBrush1, r);
            }
        }


        protected void DrawRadial(Color c1, Color c2, int x, int y, int width, int height)
        {
            _drawRadialRectangle = new Rectangle(x, y, width, height);
            DrawRadial(c1, c2, _drawRadialRectangle);
        }
        protected void DrawRadial(Color c1, Color c2, int x, int y, int width, int height, float angle)
        {
            _drawRadialRectangle = new Rectangle(x, y, width, height);
            DrawRadial(c1, c2, _drawRadialRectangle, angle);
        }

        protected void DrawRadial(Color c1, Color c2, Rectangle r)
        {
            _drawRadialBrush2 = new LinearGradientBrush(r, c1, c2, 90f);
            G.FillEllipse(_drawRadialBrush2, r);
        }
        protected void DrawRadial(Color c1, Color c2, Rectangle r, float angle)
        {
            _drawRadialBrush2 = new LinearGradientBrush(r, c1, c2, angle);
            G.FillEllipse(_drawRadialBrush2, r);
        }

        #endregion

        #region " CreateRound "

        private GraphicsPath _createRoundPath;

        private Rectangle _createRoundRectangle;
        public GraphicsPath CreateRound(int x, int y, int width, int height, int slope)
        {
            _createRoundRectangle = new Rectangle(x, y, width, height);
            return CreateRound(_createRoundRectangle, slope);
        }

        public GraphicsPath CreateRound(Rectangle r, int slope)
        {
            _createRoundPath = new GraphicsPath(FillMode.Winding);
            _createRoundPath.AddArc(r.X, r.Y, slope, slope, 180f, 90f);
            _createRoundPath.AddArc(r.Right - slope, r.Y, slope, slope, 270f, 90f);
            _createRoundPath.AddArc(r.Right - slope, r.Bottom - slope, slope, slope, 0f, 90f);
            _createRoundPath.AddArc(r.X, r.Bottom - slope, slope, slope, 90f, 90f);
            _createRoundPath.CloseFigure();
            return _createRoundPath;
        }

        #endregion

    }

    internal static class ThemeShare
    {

        #region " Animation "

        private static int _frames;
        private static bool _invalidate;

        public static PrecisionTimer ThemeTimer = new PrecisionTimer();
        //1000 / 50 = 20 FPS
        private const int Fps = 50;

        private const int Rate = 10;
        public delegate void AnimationDelegate(bool invalidate);


        private static readonly List<AnimationDelegate> _callbacks = new List<AnimationDelegate>();
        private static void HandleCallbacks(IntPtr state, bool reserve)
        {
            _invalidate = _frames >= Fps;
            if (_invalidate)
                _frames = 0;

            lock (_callbacks)
            {
                for (int I = 0; I <= _callbacks.Count - 1; I++)
                {
                    _callbacks[I].Invoke(_invalidate);
                }
            }

            _frames += Rate;
        }

        private static void InvalidateThemeTimer()
        {
            if (_callbacks.Count == 0)
            {
                ThemeTimer.Delete();
            }
            else
            {
                ThemeTimer.Create(0, Rate, HandleCallbacks);
            }
        }

        public static void AddAnimationCallback(AnimationDelegate callback)
        {
            lock (_callbacks)
            {
                if (_callbacks.Contains(callback))
                    return;

                _callbacks.Add(callback);
                InvalidateThemeTimer();
            }
        }

        public static void RemoveAnimationCallback(AnimationDelegate callback)
        {
            lock (_callbacks)
            {
                if (!_callbacks.Contains(callback))
                    return;

                _callbacks.Remove(callback);
                InvalidateThemeTimer();
            }
        }

        #endregion

    }

    internal enum MouseState : byte
    {
        None = 0,
        Over = 1,
        Down = 2,
        Block = 3
    }

    internal struct Bloom
    {

        public string _Name;
        public string Name
        {
            get { return _Name; }
        }

        public Color Value { get; set; }

        public string ValueHex
        {
            get { return string.Concat("#", Value.R.ToString("X2", null), Value.G.ToString("X2", null), Value.B.ToString("X2", null)); }
            set
            {
                try
                {
                    Value = ColorTranslator.FromHtml(value);
                }
                catch
                {
                }
            }
        }


        public Bloom(string name, Color value)
        {
            _Name = name;
            Value = value;
        }
    }

    //------------------
    //Creator: aeonhack
    //Site: elitevs.net
    //Created: 11/30/2011
    //Changed: 11/30/2011
    //Version: 1.0.0
    //------------------
    internal class PrecisionTimer : IDisposable
    {
        public bool Enabled { get; private set; }

        private IntPtr _handle;

        private TimerDelegate _timerCallback;
        [DllImport("kernel32.dll", EntryPoint = "CreateTimerQueueTimer")]
        private static extern bool CreateTimerQueueTimer(ref IntPtr handle, IntPtr queue, TimerDelegate callback, IntPtr state, uint dueTime, uint period, uint flags);

        [DllImport("kernel32.dll", EntryPoint = "DeleteTimerQueueTimer")]
        private static extern bool DeleteTimerQueueTimer(IntPtr queue, IntPtr handle, IntPtr callback);

        public delegate void TimerDelegate(IntPtr r1, bool r2);

        public void Create(uint dueTime, uint period, TimerDelegate callback)
        {
            if (Enabled)
                return;

            _timerCallback = callback;
            bool success = CreateTimerQueueTimer(ref _handle, IntPtr.Zero, _timerCallback, IntPtr.Zero, dueTime, period, 0);

            if (!success)
                ThrowNewException("CreateTimerQueueTimer");
            Enabled = success;
        }

        public void Delete()
        {
            if (!Enabled)
                return;
            bool success = DeleteTimerQueueTimer(IntPtr.Zero, _handle, IntPtr.Zero);

            if (!success && !(Marshal.GetLastWin32Error() == 997))
            {
                ThrowNewException("DeleteTimerQueueTimer");
            }

            Enabled = !success;
        }

        private void ThrowNewException(string name)
        {
            throw new Exception($"{name} failed. Win32Error: {Marshal.GetLastWin32Error()}");
        }

        public void Dispose()
        {
            Delete();
        }
    }
}
