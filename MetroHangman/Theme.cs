using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace MetroHangman
{
    #region RoundRectangle

    static class RoundRectangle
    {
        public static GraphicsPath RoundRect(Rectangle rectangle, int curve)
        {
            GraphicsPath p = new GraphicsPath();
            int arcRectangleWidth = curve * 2;
            p.AddArc(new Rectangle(rectangle.X, rectangle.Y, arcRectangleWidth, arcRectangleWidth), -180, 90);
            p.AddArc(new Rectangle(rectangle.Width - arcRectangleWidth + rectangle.X, rectangle.Y, arcRectangleWidth, arcRectangleWidth), -90, 90);
            p.AddArc(new Rectangle(rectangle.Width - arcRectangleWidth + rectangle.X, rectangle.Height - arcRectangleWidth + rectangle.Y, arcRectangleWidth, arcRectangleWidth), 0, 90);
            p.AddArc(new Rectangle(rectangle.X, rectangle.Height - arcRectangleWidth + rectangle.Y, arcRectangleWidth, arcRectangleWidth), 90, 90);
            p.AddLine(new Point(rectangle.X, rectangle.Height - arcRectangleWidth + rectangle.Y), new Point(rectangle.X, curve + rectangle.Y));
            return p;
        }
        public static GraphicsPath RoundRect(int x, int y, int width, int height, int curve)
        {
            Rectangle rectangle = new Rectangle(x, y, width, height);
            GraphicsPath p = new GraphicsPath();
            int arcRectangleWidth = curve * 2;
            p.AddArc(new Rectangle(rectangle.X, rectangle.Y, arcRectangleWidth, arcRectangleWidth), -180, 90);
            p.AddArc(new Rectangle(rectangle.Width - arcRectangleWidth + rectangle.X, rectangle.Y, arcRectangleWidth, arcRectangleWidth), -90, 90);
            p.AddArc(new Rectangle(rectangle.Width - arcRectangleWidth + rectangle.X, rectangle.Height - arcRectangleWidth + rectangle.Y, arcRectangleWidth, arcRectangleWidth), 0, 90);
            p.AddArc(new Rectangle(rectangle.X, rectangle.Height - arcRectangleWidth + rectangle.Y, arcRectangleWidth, arcRectangleWidth), 90, 90);
            p.AddLine(new Point(rectangle.X, rectangle.Height - arcRectangleWidth + rectangle.Y), new Point(rectangle.X, curve + rectangle.Y));
            return p;
        }
        public static GraphicsPath RoundedTopRect(Rectangle rectangle, int curve)
        {
            GraphicsPath p = new GraphicsPath();
            int arcRectangleWidth = curve * 2;
            p.AddArc(new Rectangle(rectangle.X, rectangle.Y, arcRectangleWidth, arcRectangleWidth), -180, 90);
            p.AddArc(new Rectangle(rectangle.Width - arcRectangleWidth + rectangle.X, rectangle.Y, arcRectangleWidth, arcRectangleWidth), -90, 90);
            p.AddLine(new Point(rectangle.X + rectangle.Width, rectangle.Y + arcRectangleWidth), new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height - 1));
            p.AddLine(new Point(rectangle.X, rectangle.Height - 1 + rectangle.Y), new Point(rectangle.X, rectangle.Y + curve));
            return p;
        }
    }

    #endregion

    #region  ThemeContainer

    public class AmbianceThemeContainer : ContainerControl
    {

        #region  Enums

        public enum MouseState
        {
            None = 0,
            Over = 1,
            Down = 2,
            Block = 3
        }

        #endregion
        #region  Variables

        private Rectangle _headerRect;
        protected MouseState State;
        private int _moveHeight;
        private Point _mouseP = new Point(0, 0);
        private bool _cap = false;
        private bool _hasShown;

        #endregion
        #region  Properties

        private bool _sizable = true;
        public bool Sizable
        {
            get
            {
                return _sizable;
            }
            set
            {
                _sizable = value;
            }
        }

        private bool _smartBounds = true;
        public bool SmartBounds
        {
            get
            {
                return _smartBounds;
            }
            set
            {
                _smartBounds = value;
            }
        }

        private bool _roundCorners = true;
        public bool RoundCorners
        {
            get
            {
                return _roundCorners;
            }
            set
            {
                _roundCorners = value;
                Invalidate();
            }
        }

        private bool _isParentForm;
        protected bool IsParentForm
        {
            get
            {
                return _isParentForm;
            }
        }

        protected bool IsParentMdi
        {
            get
            {
                if (Parent == null)
                {
                    return false;
                }
                return Parent.Parent != null;
            }
        }

        private bool _controlMode;
        protected bool ControlMode
        {
            get
            {
                return _controlMode;
            }
            set
            {
                _controlMode = value;
                Invalidate();
            }
        }

        private FormStartPosition _startPosition;
        public FormStartPosition StartPosition
        {
            get
            {
                if (_isParentForm && !_controlMode)
                {
                    return ParentForm.StartPosition;
                }
                else
                {
                    return _startPosition;
                }
            }
            set
            {
                _startPosition = value;

                if (_isParentForm && !_controlMode)
                {
                    ParentForm.StartPosition = value;
                }
            }
        }

        #endregion
        #region  EventArgs

        protected sealed override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            if (Parent == null)
            {
                return;
            }
            _isParentForm = Parent is Form;

            if (!_controlMode)
            {
                InitializeMessages();

                if (_isParentForm)
                {
                    this.ParentForm.FormBorderStyle = FormBorderStyle.None;
                    this.ParentForm.TransparencyKey = Color.Fuchsia;

                    if (!DesignMode)
                    {
                        ParentForm.Shown += FormShown;
                    }
                }
                Parent.BackColor = BackColor;
                Parent.MinimumSize = new Size(261, 65);
            }
        }

        protected sealed override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (!_controlMode)
            {
                _headerRect = new Rectangle(0, 0, Width - 14, _moveHeight - 7);
            }
            Invalidate();
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                SetState(MouseState.Down);
            }
            if (!(_isParentForm && ParentForm.WindowState == FormWindowState.Maximized || _controlMode))
            {
                if (_headerRect.Contains(e.Location))
                {
                    Capture = false;
                    _wmLmbuttondown = true;
                    DefWndProc(ref _messages[0]);
                }
                else if (_sizable && !(_previous == 0))
                {
                    Capture = false;
                    _wmLmbuttondown = true;
                    DefWndProc(ref _messages[_previous]);
                }
            }
        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _cap = false;
        }

        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!(_isParentForm && ParentForm.WindowState == FormWindowState.Maximized))
            {
                if (_sizable && !_controlMode)
                {
                    InvalidateMouse();
                }
            }
            if (_cap)
            {
                Parent.Location = (System.Drawing.Point)(object)(System.Convert.ToDouble(MousePosition) - System.Convert.ToDouble(_mouseP));
            }
        }

        protected override void OnInvalidated(System.Windows.Forms.InvalidateEventArgs e)
        {
            base.OnInvalidated(e);
            ParentForm.Text = Text;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        protected override void OnTextChanged(System.EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        private void FormShown(object sender, EventArgs e)
        {
            if (_controlMode || _hasShown)
            {
                return;
            }

            if (_startPosition == FormStartPosition.CenterParent || _startPosition == FormStartPosition.CenterScreen)
            {
                Rectangle sb = Screen.PrimaryScreen.Bounds;
                Rectangle cb = ParentForm.Bounds;
                ParentForm.Location = new Point(sb.Width / 2 - cb.Width / 2, sb.Height / 2 - cb.Width / 2);
            }
            _hasShown = true;
        }

        #endregion
        #region  Mouse & Size

        private void SetState(MouseState current)
        {
            State = current;
            Invalidate();
        }

        private Point _getIndexPoint;
        private bool _b1X;
        private bool _b2X;
        private bool _b3;
        private bool _b4;
        private int GetIndex()
        {
            _getIndexPoint = PointToClient(MousePosition);
            _b1X = _getIndexPoint.X < 7;
            _b2X = _getIndexPoint.X > Width - 7;
            _b3 = _getIndexPoint.Y < 7;
            _b4 = _getIndexPoint.Y > Height - 7;

            if (_b1X && _b3)
            {
                return 4;
            }
            if (_b1X && _b4)
            {
                return 7;
            }
            if (_b2X && _b3)
            {
                return 5;
            }
            if (_b2X && _b4)
            {
                return 8;
            }
            if (_b1X)
            {
                return 1;
            }
            if (_b2X)
            {
                return 2;
            }
            if (_b3)
            {
                return 3;
            }
            if (_b4)
            {
                return 6;
            }
            return 0;
        }

        private int _current;
        private int _previous;
        private void InvalidateMouse()
        {
            _current = GetIndex();
            if (_current == _previous)
            {
                return;
            }

            _previous = _current;
            switch (_previous)
            {
                case 0:
                    Cursor = Cursors.Default;
                    break;
                case 6:
                    Cursor = Cursors.SizeNS;
                    break;
                case 8:
                    Cursor = Cursors.SizeNWSE;
                    break;
                case 7:
                    Cursor = Cursors.SizeNESW;
                    break;
            }
        }

        private Message[] _messages = new Message[9];
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
            {
                Parent.Width = bounds.Width;
            }
            if (Parent.Height > bounds.Height)
            {
                Parent.Height = bounds.Height;
            }

            int x = Parent.Location.X;
            int y = Parent.Location.Y;

            if (x < bounds.X)
            {
                x = bounds.X;
            }
            if (y < bounds.Y)
            {
                y = bounds.Y;
            }

            int width = bounds.X + bounds.Width;
            int height = bounds.Y + bounds.Height;

            if (x + Parent.Width > width)
            {
                x = width - Parent.Width;
            }
            if (y + Parent.Height > height)
            {
                y = height - Parent.Height;
            }

            Parent.Location = new Point(x, y);
        }

        private bool _wmLmbuttondown;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (_wmLmbuttondown && m.Msg == 513)
            {
                _wmLmbuttondown = false;

                SetState(MouseState.Over);
                if (!_smartBounds)
                {
                    return;
                }

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

        #endregion

        protected override void CreateHandle()
        {
            base.CreateHandle();
        }

        public AmbianceThemeContainer()
        {
            SetStyle((ControlStyles)139270, true);
            BackColor = Color.FromArgb(244, 241, 243);
            Padding = new Padding(20, 56, 20, 16);
            DoubleBuffered = true;
            Dock = DockStyle.Fill;
            _moveHeight = 48;
            Font = new Font("Segoe UI", 9);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.Clear(Color.FromArgb(69, 68, 63));

            g.DrawRectangle(new Pen(Color.FromArgb(38, 38, 38)), new Rectangle(0, 0, Width - 1, Height - 1));
            // Use [Color.FromArgb(87, 86, 81), Color.FromArgb(60, 59, 55)] for a darker taste
            // And replace each (60, 59, 55) with (69, 68, 63)
            g.FillRectangle(new LinearGradientBrush(new Point(0, 0), new Point(0, 36), Color.FromArgb(87, 85, 77), Color.FromArgb(69, 68, 63)), new Rectangle(1, 1, Width - 2, 36));
            g.FillRectangle(new LinearGradientBrush(new Point(0, 0), new Point(0, Height), Color.FromArgb(69, 68, 63), Color.FromArgb(69, 68, 63)), new Rectangle(1, 36, Width - 2, Height - 46));

            g.DrawRectangle(new Pen(Color.FromArgb(38, 38, 38)), new Rectangle(9, 47, Width - 19, Height - 55));
            g.FillRectangle(new SolidBrush(Color.FromArgb(244, 241, 243)), new Rectangle(10, 48, Width - 20, Height - 56));

            if (_roundCorners == true)
            {

                // Draw Left upper corner
                g.FillRectangle(Brushes.Fuchsia, 0, 0, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, 1, 0, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, 2, 0, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, 3, 0, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, 0, 1, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, 0, 2, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, 0, 3, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, 1, 1, 1, 1);

                g.FillRectangle(new SolidBrush(Color.FromArgb(38, 38, 38)), 1, 3, 1, 1);
                g.FillRectangle(new SolidBrush(Color.FromArgb(38, 38, 38)), 1, 2, 1, 1);
                g.FillRectangle(new SolidBrush(Color.FromArgb(38, 38, 38)), 2, 1, 1, 1);
                g.FillRectangle(new SolidBrush(Color.FromArgb(38, 38, 38)), 3, 1, 1, 1);

                // Draw right upper corner
                g.FillRectangle(Brushes.Fuchsia, Width - 1, 0, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, Width - 2, 0, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, Width - 3, 0, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, Width - 4, 0, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, Width - 1, 1, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, Width - 1, 2, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, Width - 1, 3, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, Width - 2, 1, 1, 1);

                g.FillRectangle(new SolidBrush(Color.FromArgb(38, 38, 38)), Width - 2, 3, 1, 1);
                g.FillRectangle(new SolidBrush(Color.FromArgb(38, 38, 38)), Width - 2, 2, 1, 1);
                g.FillRectangle(new SolidBrush(Color.FromArgb(38, 38, 38)), Width - 3, 1, 1, 1);
                g.FillRectangle(new SolidBrush(Color.FromArgb(38, 38, 38)), Width - 4, 1, 1, 1);

                // Draw Left bottom corner
                g.FillRectangle(Brushes.Fuchsia, 0, Height - 1, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, 0, Height - 2, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, 0, Height - 3, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, 0, Height - 4, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, 1, Height - 1, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, 2, Height - 1, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, 3, Height - 1, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, 1, Height - 1, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, 1, Height - 2, 1, 1);

                g.FillRectangle(new SolidBrush(Color.FromArgb(38, 38, 38)), 1, Height - 3, 1, 1);
                g.FillRectangle(new SolidBrush(Color.FromArgb(38, 38, 38)), 1, Height - 4, 1, 1);
                g.FillRectangle(new SolidBrush(Color.FromArgb(38, 38, 38)), 3, Height - 2, 1, 1);
                g.FillRectangle(new SolidBrush(Color.FromArgb(38, 38, 38)), 2, Height - 2, 1, 1);

                // Draw right bottom corner
                g.FillRectangle(Brushes.Fuchsia, Width - 1, Height, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, Width - 2, Height, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, Width - 3, Height, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, Width - 4, Height, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, Width - 1, Height - 1, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, Width - 1, Height - 2, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, Width - 1, Height - 3, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, Width - 2, Height - 1, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, Width - 3, Height - 1, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, Width - 4, Height - 1, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, Width - 1, Height - 4, 1, 1);
                g.FillRectangle(Brushes.Fuchsia, Width - 2, Height - 2, 1, 1);

                g.FillRectangle(new SolidBrush(Color.FromArgb(38, 38, 38)), Width - 2, Height - 3, 1, 1);
                g.FillRectangle(new SolidBrush(Color.FromArgb(38, 38, 38)), Width - 2, Height - 4, 1, 1);
                g.FillRectangle(new SolidBrush(Color.FromArgb(38, 38, 38)), Width - 4, Height - 2, 1, 1);
                g.FillRectangle(new SolidBrush(Color.FromArgb(38, 38, 38)), Width - 3, Height - 2, 1, 1);
            }

            g.DrawString(Text, new Font("Tahoma", 12, FontStyle.Bold), new SolidBrush(Color.FromArgb(223, 219, 210)), new Rectangle(0, 14, Width - 1, Height), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near });
        }
    }

    #endregion
    #region  ControlBox

    public class AmbianceControlBox : Control
    {

        #region  Enums

        public enum MouseState
        {
            None = 0,
            Over = 1,
            Down = 2
        }

        #endregion
        #region  MouseStates
        MouseState _state = MouseState.None;
        int _x;
        Rectangle _closeBtn = new Rectangle(3, 2, 17, 17);
        Rectangle _minBtn = new Rectangle(23, 2, 17, 17);
        Rectangle _maxBtn = new Rectangle(43, 2, 17, 17);

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseDown(e);

            _state = MouseState.Down;
            Invalidate();
        }
        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (_x > 3 && _x < 20)
            {
                FindForm().Close();
            }
            else if (_x > 23 && _x < 40)
            {
                FindForm().WindowState = FormWindowState.Minimized;
            }
            else if (_x > 43 && _x < 60)
            {
                if (_enableMaximize == true)
                {
                    if (FindForm().WindowState == FormWindowState.Maximized)
                    {
                        FindForm().WindowState = FormWindowState.Minimized;
                        FindForm().WindowState = FormWindowState.Normal;
                    }
                    else
                    {
                        FindForm().WindowState = FormWindowState.Minimized;
                        FindForm().WindowState = FormWindowState.Maximized;
                    }
                }
            }
            _state = MouseState.Over;
            Invalidate();
        }
        protected override void OnMouseEnter(System.EventArgs e)
        {
            base.OnMouseEnter(e);
            _state = MouseState.Over;
            Invalidate();
        }
        protected override void OnMouseLeave(System.EventArgs e)
        {
            base.OnMouseLeave(e);
            _state = MouseState.None;
            Invalidate();
        }
        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseMove(e);
            _x = e.Location.X;
            Invalidate();
        }
        #endregion
        #region  Properties

        bool _enableMaximize = true;
        public bool EnableMaximize
        {
            get
            {
                return _enableMaximize;
            }
            set
            {
                _enableMaximize = value;
                if (_enableMaximize == true)
                {
                    this.Size = new Size(64, 22);
                }
                else
                {
                    this.Size = new Size(44, 22);
                }
                Invalidate();
            }
        }

        #endregion

        public AmbianceControlBox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer, true);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            Font = new Font("Marlett", 7);
            Anchor = AnchorStyles.Top | AnchorStyles.Left;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_enableMaximize == true)
            {
                this.Size = new Size(64, 22);
            }
            else
            {
                this.Size = new Size(44, 22);
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            // Auto-decide control location on the theme container
            Location = new Point(5, 13);
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Bitmap b = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(b);

            base.OnPaint(e);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            LinearGradientBrush lgbClose = new LinearGradientBrush(_closeBtn, Color.FromArgb(242, 132, 99), Color.FromArgb(224, 82, 33), 90);
            g.FillEllipse(lgbClose, _closeBtn);
            g.DrawEllipse(new Pen(Color.FromArgb(57, 56, 53)), _closeBtn);
            g.DrawString("r", new Font("Marlett", 7), new SolidBrush(Color.FromArgb(52, 50, 46)), new Rectangle((int)6.5, 8, 0, 0));

            LinearGradientBrush lgbMinimize = new LinearGradientBrush(_minBtn, Color.FromArgb(130, 129, 123), Color.FromArgb(103, 102, 96), 90);
            g.FillEllipse(lgbMinimize, _minBtn);
            g.DrawEllipse(new Pen(Color.FromArgb(57, 56, 53)), _minBtn);
            g.DrawString("0", new Font("Marlett", 7), new SolidBrush(Color.FromArgb(52, 50, 46)), new Rectangle(26, (int)4.4, 0, 0));

            if (_enableMaximize == true)
            {
                LinearGradientBrush lgbMaximize = new LinearGradientBrush(_maxBtn, Color.FromArgb(130, 129, 123), Color.FromArgb(103, 102, 96), 90);
                g.FillEllipse(lgbMaximize, _maxBtn);
                g.DrawEllipse(new Pen(Color.FromArgb(57, 56, 53)), _maxBtn);
                g.DrawString("1", new Font("Marlett", 7), new SolidBrush(Color.FromArgb(52, 50, 46)), new Rectangle(46, 7, 0, 0));
            }

            switch (_state)
            {
                case MouseState.None:
                    LinearGradientBrush xLgbClose1 = new LinearGradientBrush(_closeBtn, Color.FromArgb(242, 132, 99), Color.FromArgb(224, 82, 33), 90);
                    g.FillEllipse(xLgbClose1, _closeBtn);
                    g.DrawEllipse(new Pen(Color.FromArgb(57, 56, 53)), _closeBtn);
                    g.DrawString("r", new Font("Marlett", 7), new SolidBrush(Color.FromArgb(52, 50, 46)), new Rectangle((int)6.5, 8, 0, 0));

                    LinearGradientBrush xLgbMinimize1 = new LinearGradientBrush(_minBtn, Color.FromArgb(130, 129, 123), Color.FromArgb(103, 102, 96), 90);
                    g.FillEllipse(xLgbMinimize1, _minBtn);
                    g.DrawEllipse(new Pen(Color.FromArgb(57, 56, 53)), _minBtn);
                    g.DrawString("0", new Font("Marlett", 7), new SolidBrush(Color.FromArgb(52, 50, 46)), new Rectangle(26, (int)4.4, 0, 0));

                    if (_enableMaximize == true)
                    {
                        LinearGradientBrush xLgbMaximize = new LinearGradientBrush(_maxBtn, Color.FromArgb(130, 129, 123), Color.FromArgb(103, 102, 96), 90);
                        g.FillEllipse(xLgbMaximize, _maxBtn);
                        g.DrawEllipse(new Pen(Color.FromArgb(57, 56, 53)), _maxBtn);
                        g.DrawString("1", new Font("Marlett", 7), new SolidBrush(Color.FromArgb(52, 50, 46)), new Rectangle(46, 7, 0, 0));
                    }
                    break;
                case MouseState.Over:
                    if (_x > 3 && _x < 20)
                    {
                        LinearGradientBrush xLgbClose = new LinearGradientBrush(_closeBtn, Color.FromArgb(248, 152, 124), Color.FromArgb(231, 92, 45), 90);
                        g.FillEllipse(xLgbClose, _closeBtn);
                        g.DrawEllipse(new Pen(Color.FromArgb(57, 56, 53)), _closeBtn);
                        g.DrawString("r", new Font("Marlett", 7), new SolidBrush(Color.FromArgb(52, 50, 46)), new Rectangle((int)6.5, 8, 0, 0));
                    }
                    else if (_x > 23 && _x < 40)
                    {
                        LinearGradientBrush xLgbMinimize = new LinearGradientBrush(_minBtn, Color.FromArgb(196, 196, 196), Color.FromArgb(173, 173, 173), 90);
                        g.FillEllipse(xLgbMinimize, _minBtn);
                        g.DrawEllipse(new Pen(Color.FromArgb(57, 56, 53)), _minBtn);
                        g.DrawString("0", new Font("Marlett", 7), new SolidBrush(Color.FromArgb(52, 50, 46)), new Rectangle(26, (int)4.4, 0, 0));
                    }
                    else if (_x > 43 && _x < 60)
                    {
                        if (_enableMaximize == true)
                        {
                            LinearGradientBrush xLgbMaximize = new LinearGradientBrush(_maxBtn, Color.FromArgb(196, 196, 196), Color.FromArgb(173, 173, 173), 90);
                            g.FillEllipse(xLgbMaximize, _maxBtn);
                            g.DrawEllipse(new Pen(Color.FromArgb(57, 56, 53)), _maxBtn);
                            g.DrawString("1", new Font("Marlett", 7), new SolidBrush(Color.FromArgb(52, 50, 46)), new Rectangle(46, 7, 0, 0));
                        }
                    }
                    break;
            }

            e.Graphics.DrawImage((Image)b.Clone(), 0, 0);
            g.Dispose();
            b.Dispose();
        }
    }

    #endregion
    #region Button 1

    class AmbianceButton1 : Control
    {

        #region Variables

        private int _mouseState;
        private GraphicsPath _shape;
        private LinearGradientBrush _inactiveGb;
        private LinearGradientBrush _pressedGb;
        private LinearGradientBrush _pressedContourGb;
        private Rectangle _r1;
        private Pen _p1;
        private Pen _p3;
        private Image _image;
        private Size _imageSize;
        private StringAlignment _textAlignment = StringAlignment.Center;
        private Color _textColor = Color.FromArgb(150, 150, 150);
        private ContentAlignment _imageAlign = ContentAlignment.MiddleLeft;

        #endregion
        #region Image Designer

        private static PointF ImageLocation(StringFormat sf, SizeF area, SizeF imageArea)
        {
            PointF myPoint = default(PointF);
            switch (sf.Alignment)
            {
                case StringAlignment.Center:
                    myPoint.X = Convert.ToSingle((area.Width - imageArea.Width) / 2);
                    break;
                case StringAlignment.Near:
                    myPoint.X = 2;
                    break;
                case StringAlignment.Far:
                    myPoint.X = area.Width - imageArea.Width - 2;

                    break;
            }

            switch (sf.LineAlignment)
            {
                case StringAlignment.Center:
                    myPoint.Y = Convert.ToSingle((area.Height - imageArea.Height) / 2);
                    break;
                case StringAlignment.Near:
                    myPoint.Y = 2;
                    break;
                case StringAlignment.Far:
                    myPoint.Y = area.Height - imageArea.Height - 2;
                    break;
            }
            return myPoint;
        }

        private StringFormat GetStringFormat(ContentAlignment contentAlignment)
        {
            StringFormat sf = new StringFormat();
            switch (contentAlignment)
            {
                case ContentAlignment.MiddleCenter:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleLeft:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleRight:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.TopCenter:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.TopLeft:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopRight:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomCenter:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomLeft:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.BottomRight:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Far;
                    break;
            }
            return sf;
        }

        #endregion
        #region Properties

        public Image Image
        {
            get { return _image; }
            set
            {
                if (value == null)
                {
                    _imageSize = Size.Empty;
                }
                else
                {
                    _imageSize = value.Size;
                }

                _image = value;
                Invalidate();
            }
        }

        protected Size ImageSize
        {
            get { return _imageSize; }
        }

        public ContentAlignment ImageAlign
        {
            get { return _imageAlign; }
            set
            {
                _imageAlign = value;
                Invalidate();
            }
        }

        public StringAlignment TextAlignment
        {
            get { return this._textAlignment; }
            set
            {
                this._textAlignment = value;
                this.Invalidate();
            }
        }

        public override Color ForeColor
        {
            get { return this._textColor; }
            set
            {
                this._textColor = value;
                this.Invalidate();
            }
        }

        #endregion
        #region EventArgs

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _mouseState = 0;
            Invalidate();
            base.OnMouseUp(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            _mouseState = 1;
            Focus();
            Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _mouseState = 0;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnTextChanged(System.EventArgs e)
        {
            Invalidate();
            base.OnTextChanged(e);
        }

        #endregion

        public AmbianceButton1()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);

            BackColor = Color.Transparent;
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 12);
            ForeColor = Color.FromArgb(76, 76, 76);
            Size = new Size(177, 30);
            _textAlignment = StringAlignment.Center;
            _p1 = new Pen(Color.FromArgb(180, 180, 180));
            // P1 = Border color
        }

        protected override void OnResize(System.EventArgs e)
        {

            if (Width > 0 && Height > 0)
            {
                _shape = new GraphicsPath();
                _r1 = new Rectangle(0, 0, Width, Height);

                _inactiveGb = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.FromArgb(253, 252, 252), Color.FromArgb(239, 237, 236), 90f);
                _pressedGb = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.FromArgb(226, 226, 226), Color.FromArgb(237, 237, 237), 90f);
                _pressedContourGb = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.FromArgb(167, 167, 167), Color.FromArgb(167, 167, 167), 90f);

                _p3 = new Pen(_pressedContourGb);
            }

            var myDrawer = _shape;
            myDrawer.AddArc(0, 0, 10, 10, 180, 90);
            myDrawer.AddArc(Width - 11, 0, 10, 10, -90, 90);
            myDrawer.AddArc(Width - 11, Height - 11, 10, 10, 0, 90);
            myDrawer.AddArc(0, Height - 11, 10, 10, 90, 90);
            myDrawer.CloseAllFigures();
            Invalidate();
            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            PointF ipt = ImageLocation(GetStringFormat(ImageAlign), Size, ImageSize);

            switch (_mouseState)
            {
                case 0:
                    //Inactive
                    g.FillPath(_inactiveGb, _shape);
                    // Fill button body with InactiveGB color gradient
                    g.DrawPath(_p1, _shape);
                    // Draw button border [InactiveGB]
                    if (Image == null)
                    {
                        g.DrawString(Text, Font, new SolidBrush(ForeColor), _r1, new StringFormat
                        {
                            Alignment = _textAlignment,
                            LineAlignment = StringAlignment.Center
                        });
                    }
                    else
                    {
                        g.DrawImage(_image, ipt.X, ipt.Y, ImageSize.Width, ImageSize.Height);
                        g.DrawString(Text, Font, new SolidBrush(ForeColor), _r1, new StringFormat
                        {
                            Alignment = _textAlignment,
                            LineAlignment = StringAlignment.Center
                        });
                    }
                    break;
                case 1:
                    //Pressed
                    g.FillPath(_pressedGb, _shape);
                    // Fill button body with PressedGB color gradient
                    g.DrawPath(_p3, _shape);
                    // Draw button border [PressedGB]

                    if (Image == null)
                    {
                        g.DrawString(Text, Font, new SolidBrush(ForeColor), _r1, new StringFormat
                        {
                            Alignment = _textAlignment,
                            LineAlignment = StringAlignment.Center
                        });
                    }
                    else
                    {
                        g.DrawImage(_image, ipt.X, ipt.Y, ImageSize.Width, ImageSize.Height);
                        g.DrawString(Text, Font, new SolidBrush(ForeColor), _r1, new StringFormat
                        {
                            Alignment = _textAlignment,
                            LineAlignment = StringAlignment.Center
                        });
                    }
                    break;
            }
            base.OnPaint(e);
        }
    }

    #endregion
    #region Button 2

    class AmbianceButton2 : Control
    {

        #region Variables

        private int _mouseState;
        private GraphicsPath _shape;
        private LinearGradientBrush _inactiveGb;
        private LinearGradientBrush _pressedGb;
        private LinearGradientBrush _pressedContourGb;
        private Rectangle _r1;
        private Pen _p1;
        private Pen _p3;
        private Image _image;
        private Size _imageSize;
        private StringAlignment _textAlignment = StringAlignment.Center;
        private Color _textColor = Color.FromArgb(150, 150, 150);
        private ContentAlignment _imageAlign = ContentAlignment.MiddleLeft;

        #endregion
        #region Image Designer

        private static PointF ImageLocation(StringFormat sf, SizeF area, SizeF imageArea)
        {
            PointF myPoint = default(PointF);
            switch (sf.Alignment)
            {
                case StringAlignment.Center:
                    myPoint.X = Convert.ToSingle((area.Width - imageArea.Width) / 2);
                    break;
                case StringAlignment.Near:
                    myPoint.X = 2;
                    break;
                case StringAlignment.Far:
                    myPoint.X = area.Width - imageArea.Width - 2;

                    break;
            }

            switch (sf.LineAlignment)
            {
                case StringAlignment.Center:
                    myPoint.Y = Convert.ToSingle((area.Height - imageArea.Height) / 2);
                    break;
                case StringAlignment.Near:
                    myPoint.Y = 2;
                    break;
                case StringAlignment.Far:
                    myPoint.Y = area.Height - imageArea.Height - 2;
                    break;
            }
            return myPoint;
        }

        private StringFormat GetStringFormat(ContentAlignment contentAlignment)
        {
            StringFormat sf = new StringFormat();
            switch (contentAlignment)
            {
                case ContentAlignment.MiddleCenter:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleLeft:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleRight:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.TopCenter:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.TopLeft:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopRight:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomCenter:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomLeft:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.BottomRight:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Far;
                    break;
            }
            return sf;
        }

        #endregion
        #region Properties

        public Image Image
        {
            get { return _image; }
            set
            {
                if (value == null)
                {
                    _imageSize = Size.Empty;
                }
                else
                {
                    _imageSize = value.Size;
                }

                _image = value;
                Invalidate();
            }
        }

        protected Size ImageSize
        {
            get { return _imageSize; }
        }

        public ContentAlignment ImageAlign
        {
            get { return _imageAlign; }
            set
            {
                _imageAlign = value;
                Invalidate();
            }
        }

        public StringAlignment TextAlignment
        {
            get { return this._textAlignment; }
            set
            {
                this._textAlignment = value;
                this.Invalidate();
            }
        }

        public override Color ForeColor
        {
            get { return this._textColor; }
            set
            {
                this._textColor = value;
                this.Invalidate();
            }
        }

        #endregion
        #region EventArgs

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _mouseState = 0;
            Invalidate();
            base.OnMouseUp(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            _mouseState = 1;
            Focus();
            Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _mouseState = 0;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnTextChanged(System.EventArgs e)
        {
            Invalidate();
            base.OnTextChanged(e);
        }

        #endregion

        public AmbianceButton2()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);

            BackColor = Color.Transparent;
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            ForeColor = Color.FromArgb(76, 76, 76);
            Size = new Size(177, 30);
            _textAlignment = StringAlignment.Center;
            _p1 = new Pen(Color.FromArgb(162, 120, 101));
            // P1 = Border color
        }

        protected override void OnResize(System.EventArgs e)
        {

            if (Width > 0 && Height > 0)
            {
                _shape = new GraphicsPath();
                _r1 = new Rectangle(0, 0, Width, Height);

                _inactiveGb = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.FromArgb(253, 175, 143), Color.FromArgb(244, 146, 106), 90f);
                _pressedGb = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.FromArgb(244, 146, 106), Color.FromArgb(244, 146, 106), 90f);
                _pressedContourGb = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.FromArgb(162, 120, 101), Color.FromArgb(162, 120, 101), 90f);

                _p3 = new Pen(_pressedContourGb);
            }

            var myDrawer = _shape;
            myDrawer.AddArc(0, 0, 10, 10, 180, 90);
            myDrawer.AddArc(Width - 11, 0, 10, 10, -90, 90);
            myDrawer.AddArc(Width - 11, Height - 11, 10, 10, 0, 90);
            myDrawer.AddArc(0, Height - 11, 10, 10, 90, 90);
            myDrawer.CloseAllFigures();
            Invalidate();
            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            PointF ipt = ImageLocation(GetStringFormat(ImageAlign), Size, ImageSize);

            switch (_mouseState)
            {
                case 0:
                    //Inactive
                    g.FillPath(_inactiveGb, _shape);
                    // Fill button body with InactiveGB color gradient
                    g.DrawPath(_p1, _shape);
                    // Draw button border [InactiveGB]
                    if (Image == null)
                    {
                        g.DrawString(Text, Font, new SolidBrush(ForeColor), _r1, new StringFormat
                        {
                            Alignment = _textAlignment,
                            LineAlignment = StringAlignment.Center
                        });
                    }
                    else
                    {
                        g.DrawImage(_image, ipt.X, ipt.Y, ImageSize.Width, ImageSize.Height);
                        g.DrawString(Text, Font, new SolidBrush(ForeColor), _r1, new StringFormat
                        {
                            Alignment = _textAlignment,
                            LineAlignment = StringAlignment.Center
                        });
                    }
                    break;
                case 1:
                    //Pressed
                    g.FillPath(_pressedGb, _shape);
                    // Fill button body with PressedGB color gradient
                    g.DrawPath(_p3, _shape);
                    // Draw button border [PressedGB]

                    if (Image == null)
                    {
                        g.DrawString(Text, Font, new SolidBrush(ForeColor), _r1, new StringFormat
                        {
                            Alignment = _textAlignment,
                            LineAlignment = StringAlignment.Center
                        });
                    }
                    else
                    {
                        g.DrawImage(_image, ipt.X, ipt.Y, ImageSize.Width, ImageSize.Height);
                        g.DrawString(Text, Font, new SolidBrush(ForeColor), _r1, new StringFormat
                        {
                            Alignment = _textAlignment,
                            LineAlignment = StringAlignment.Center
                        });
                    }
                    break;
            }
            base.OnPaint(e);
        }
    }

    #endregion
    #region Label

    class AmbianceLabel : Label
    {

        public AmbianceLabel()
        {
            Font = new Font("Segoe UI", 11);
            ForeColor = Color.FromArgb(76, 76, 77);
            BackColor = Color.Transparent;
        }
    }

    #endregion
    #region Link Label
    class AmbianceLinkLabel : LinkLabel
    {

        public AmbianceLinkLabel()
        {
            Font = new Font("Segoe UI", 11, FontStyle.Regular);
            BackColor = Color.Transparent;
            LinkColor = Color.FromArgb(240, 119, 70);
            ActiveLinkColor = Color.FromArgb(221, 72, 20);
            VisitedLinkColor = Color.FromArgb(240, 119, 70);
            LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
        }
    }

    #endregion
    #region Header Label

    class AmbianceHeaderLabel : Label
    {

        public AmbianceHeaderLabel()
        {
            Font = new Font("Segoe UI", 11, FontStyle.Bold);
            ForeColor = Color.FromArgb(76, 76, 77);
            BackColor = Color.Transparent;
        }
    }

    #endregion
    #region Separator

    public class AmbianceSeparator : Control
    {

        public AmbianceSeparator()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            this.Size = new Size(120, 10);
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawLine(new Pen(Color.FromArgb(224, 222, 220)), 0, 5, Width, 5);
            e.Graphics.DrawLine(new Pen(Color.FromArgb(250, 249, 249)), 0, 6, Width, 6);
        }
    }

    #endregion
    #region ProgressBar

    public class AmbianceProgressBar : Control
    {

        #region Enums 

        public enum Alignment
        {
            Right,
            Center
        }

        #endregion
        #region Variables 

        private int _minimum;
        private int _maximum = 100;
        private int _value = 0;
        private Alignment _aln;
        private bool _drawHatch;

        private bool _showPercentage;
        private GraphicsPath _gp1;
        private GraphicsPath _gp2;
        private GraphicsPath _gp3;
        private Rectangle _r1;
        private Rectangle _r2;
        private LinearGradientBrush _gb1;
        private LinearGradientBrush _gb2;
        private int _i1;

        #endregion
        #region Properties 

        public int Maximum
        {
            get { return _maximum; }
            set
            {
                if (value < 1)
                    value = 1;
                if (value < _value)
                    _value = value;
                _maximum = value;
                Invalidate();
            }
        }

        public int Minimum
        {
            get { return _minimum; }
            set
            {
                _minimum = value;

                if (value > _maximum)
                    _maximum = value;
                if (value > _value)
                    _value = value;

                Invalidate();
            }
        }

        public int Value
        {
            get { return _value; }
            set
            {
                if (value > _maximum)
                    value = Maximum;
                _value = value;
                Invalidate();
            }
        }

        public Alignment ValueAlignment
        {
            get { return _aln; }
            set
            {
                _aln = value;
                Invalidate();
            }
        }

        public bool DrawHatch
        {
            get { return _drawHatch; }
            set
            {
                _drawHatch = value;
                Invalidate();
            }
        }

        public bool ShowPercentage
        {
            get { return _showPercentage; }
            set
            {
                _showPercentage = value;
                Invalidate();
            }
        }

        #endregion
        #region EventArgs

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Height = 20;
            Size minimumSize = new Size(58, 20);
            this.MinimumSize = minimumSize;
        }

        #endregion

        public AmbianceProgressBar()
        {
            _maximum = 100;
            _showPercentage = true;
            _drawHatch = true;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;
            DoubleBuffered = true;
        }

        public void Increment(int value)
        {
            this._value += value;
            Invalidate();
        }

        public void Deincrement(int value)
        {
            this._value -= value;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap b = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(b);

            g.Clear(Color.Transparent);
            g.SmoothingMode = SmoothingMode.HighQuality;

            _gp1 = RoundRectangle.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 4);
            _gp2 = RoundRectangle.RoundRect(new Rectangle(1, 1, Width - 3, Height - 3), 4);

            _r1 = new Rectangle(0, 2, Width - 1, Height - 1);
            _gb1 = new LinearGradientBrush(_r1, Color.FromArgb(255, 255, 255), Color.FromArgb(230, 230, 230), 90f);

            // Draw inside background
            g.FillRectangle(new SolidBrush(Color.FromArgb(244, 241, 243)), _r1);
            g.SetClip(_gp1);
            g.FillPath(new SolidBrush(Color.FromArgb(244, 241, 243)), RoundRectangle.RoundRect(new Rectangle(1, 1, Width - 3, Height / 2 - 2), 4));


            _i1 = (int)Math.Round((this._value - this._minimum) / (double)(this._maximum - this._minimum) * (this.Width - 3));
            if (_i1 > 1)
            {
                _gp3 = RoundRectangle.RoundRect(new Rectangle(1, 1, _i1, Height - 3), 4);

                _r2 = new Rectangle(1, 1, _i1, Height - 3);
                _gb2 = new LinearGradientBrush(_r2, Color.FromArgb(214, 89, 37), Color.FromArgb(223, 118, 75), 90f);

                // Fill the value with its gradient
                g.FillPath(_gb2, _gp3);

                // Draw diagonal lines
                if (_drawHatch == true)
                {
                    for (var i = 0; i <= (Width - 1) * _maximum / _value; i += 20)
                    {
                        g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(25, Color.White)), 10.0F), new Point(System.Convert.ToInt32(i), 0), new Point(i - 10, Height));
                    }
                }

                g.SetClip(_gp3);
                g.SmoothingMode = SmoothingMode.None;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.ResetClip();
            }

            // Draw value as a string
            string drawString = Convert.ToString(Convert.ToInt32(Value)) + "%";
            int textX = (int)(this.Width - g.MeasureString(drawString, Font).Width - 1);
            int textY = this.Height / 2 - (System.Convert.ToInt32(g.MeasureString(drawString, Font).Height / 2) - 2);

            if (_showPercentage == true)
            {
                switch (ValueAlignment)
                {
                    case Alignment.Right:
                        g.DrawString(drawString, new Font("Segoe UI", 8), Brushes.DimGray, new Point(textX, textY));
                        break;
                    case Alignment.Center:
                        g.DrawString(drawString, new Font("Segoe UI", 8), Brushes.DimGray, new Rectangle(0, 0, Width, Height + 2), new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        });
                        break;
                }
            }

            // Draw border
            g.DrawPath(new Pen(Color.FromArgb(180, 180, 180)), _gp2);

            e.Graphics.DrawImage((Image)b.Clone(), 0, 0);
            g.Dispose();
            b.Dispose();
        }
    }

    #endregion
    #region Progress Indicator

    class AmbianceProgressIndicator : Control
    {

        #region Variables

        private readonly SolidBrush _baseColor = new SolidBrush(Color.FromArgb(76, 76, 76));
        private readonly SolidBrush _animationColor = new SolidBrush(Color.Gray);

        private readonly Timer _animationSpeed = new Timer();
        private PointF[] _floatPoint;
        private BufferedGraphics _buffGraphics;
        private int _indicatorIndex;
        private readonly BufferedGraphicsContext _graphicsContext = BufferedGraphicsManager.Current;

        #endregion
        #region Custom Properties

        public Color PBaseColor
        {
            get { return _baseColor.Color; }
            set { _baseColor.Color = value; }
        }

        public Color PAnimationColor
        {
            get { return _animationColor.Color; }
            set { _animationColor.Color = value; }
        }

        public int PAnimationSpeed
        {
            get { return _animationSpeed.Interval; }
            set { _animationSpeed.Interval = value; }
        }

        #endregion
        #region EventArgs

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetStandardSize();
            UpdateGraphics();
            SetPoints();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            _animationSpeed.Enabled = this.Enabled;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            _animationSpeed.Tick += AnimationSpeed_Tick;
            _animationSpeed.Start();
        }

        private void AnimationSpeed_Tick(object sender, EventArgs e)
        {
            if (_indicatorIndex.Equals(0))
            {
                _indicatorIndex = _floatPoint.Length - 1;
            }
            else
            {
                _indicatorIndex -= 1;
            }
            this.Invalidate(false);
        }

        #endregion

        public AmbianceProgressIndicator()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);

            Size = new Size(80, 80);
            Text = string.Empty;
            MinimumSize = new Size(80, 80);
            SetPoints();
            _animationSpeed.Interval = 100;
        }

        private void SetStandardSize()
        {
            int size = Math.Max(Width, Height);
            Size = new Size(size, size);
        }

        private void SetPoints()
        {
            Stack<PointF> stack = new Stack<PointF>();
            PointF startingFloatPoint = new PointF(this.Width / 2f, this.Height / 2f);
            for (float i = 0f; i < 360f; i += 45f)
            {
                this.SetValue(startingFloatPoint, (int)Math.Round(this.Width / 2.0 - 15.0), i);
                PointF endPoint = this.EndPoint;
                endPoint = new PointF(endPoint.X - 7.5f, endPoint.Y - 7.5f);
                stack.Push(endPoint);
            }
            this._floatPoint = stack.ToArray();
        }

        private void UpdateGraphics()
        {
            if ((this.Width > 0) && (this.Height > 0))
            {
                Size size2 = new Size(this.Width + 1, this.Height + 1);
                this._graphicsContext.MaximumBuffer = size2;
                this._buffGraphics = this._graphicsContext.Allocate(this.CreateGraphics(), this.ClientRectangle);
                this._buffGraphics.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this._buffGraphics.Graphics.Clear(this.BackColor);
            int num2 = this._floatPoint.Length - 1;
            for (int i = 0; i <= num2; i++)
            {
                if (this._indicatorIndex == i)
                {
                    this._buffGraphics.Graphics.FillEllipse(this._animationColor, this._floatPoint[i].X, this._floatPoint[i].Y, 15f, 15f);
                }
                else
                {
                    this._buffGraphics.Graphics.FillEllipse(this._baseColor, this._floatPoint[i].X, this._floatPoint[i].Y, 15f, 15f);
                }
            }
            this._buffGraphics.Render(e.Graphics);
        }


        private double _rise;
        private double _run;
        private PointF _startingFloatPoint;

        private TX AssignValues<TX>(ref TX run, TX length)
        {
            run = length;
            return length;
        }

        private void SetValue(PointF startingFloatPoint, int length, double angle)
        {
            double circleRadian = Math.PI * angle / 180.0;

            _startingFloatPoint = startingFloatPoint;
            _rise = AssignValues(ref _run, length);
            _rise = Math.Sin(circleRadian) * _rise;
            _run = Math.Cos(circleRadian) * _run;
        }

        private PointF EndPoint
        {
            get
            {
                float locationX = Convert.ToSingle(_startingFloatPoint.Y + _rise);
                float locationY = Convert.ToSingle(_startingFloatPoint.X + _run);

                return new PointF(locationY, locationX);
            }
        }
    }

    #endregion
    #region  Toggle Button

    [DefaultEvent("ToggledChanged")]
    public class AmbianceToggle : Control
    {

        #region  Enums

        public enum _Type
        {
            OnOff,
            YesNo,
            Io
        }

        #endregion
        #region  Variables

        public delegate void ToggledChangedEventHandler();
        private ToggledChangedEventHandler _toggledChangedEvent;

        public event ToggledChangedEventHandler ToggledChanged
        {
            add
            {
                _toggledChangedEvent = (ToggledChangedEventHandler)System.Delegate.Combine(_toggledChangedEvent, value);
            }
            remove
            {
                _toggledChangedEvent = (ToggledChangedEventHandler)System.Delegate.Remove(_toggledChangedEvent, value);
            }
        }

        private bool _toggled;
        private _Type _toggleType;
        private Rectangle _bar;
        private Size _cHandle = new Size(15, 20);

        #endregion
        #region  Properties

        public bool Toggled
        {
            get
            {
                return _toggled;
            }
            set
            {
                _toggled = value;
                Invalidate();
                if (_toggledChangedEvent != null)
                    _toggledChangedEvent();
            }
        }

        public _Type Type
        {
            get
            {
                return _toggleType;
            }
            set
            {
                _toggleType = value;
                Invalidate();
            }
        }

        #endregion
        #region  EventArgs

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Width = 79;
            Height = 27;
        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Toggled = !Toggled;
            Focus();
        }

        #endregion

        public AmbianceToggle()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.Clear(Parent.BackColor);

            int switchXLoc = 3;
            Rectangle controlRectangle = new Rectangle(0, 0, Width - 1, Height - 1);
            GraphicsPath controlPath = RoundRectangle.RoundRect(controlRectangle, 4);

            LinearGradientBrush backgroundLgb = default(LinearGradientBrush);
            if (_toggled)
            {
                switchXLoc = 37;
                backgroundLgb = new LinearGradientBrush(controlRectangle, Color.FromArgb(231, 108, 58), Color.FromArgb(236, 113, 63), 90.0F);
            }
            else
            {
                switchXLoc = 0;
                backgroundLgb = new LinearGradientBrush(controlRectangle, Color.FromArgb(208, 208, 208), Color.FromArgb(226, 226, 226), 90.0F);
            }

            // Fill inside background gradient
            g.FillPath(backgroundLgb, controlPath);

            // Draw string
            switch (_toggleType)
            {
                case _Type.OnOff:
                    if (Toggled)
                    {
                        g.DrawString("ON", new Font("Segoe UI", 12, FontStyle.Regular), Brushes.WhiteSmoke, _bar.X + 18, (float)(_bar.Y + 13.5), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }
                    else
                    {
                        g.DrawString("OFF", new Font("Segoe UI", 12, FontStyle.Regular), Brushes.DimGray, _bar.X + 59, (float)(_bar.Y + 13.5), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }
                    break;
                case _Type.YesNo:
                    if (Toggled)
                    {
                        g.DrawString("YES", new Font("Segoe UI", 12, FontStyle.Regular), Brushes.WhiteSmoke, _bar.X + 18, (float)(_bar.Y + 13.5), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }
                    else
                    {
                        g.DrawString("NO", new Font("Segoe UI", 12, FontStyle.Regular), Brushes.DimGray, _bar.X + 59, (float)(_bar.Y + 13.5), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }
                    break;
                case _Type.Io:
                    if (Toggled)
                    {
                        g.DrawString("I", new Font("Segoe UI", 12, FontStyle.Regular), Brushes.WhiteSmoke, _bar.X + 18, (float)(_bar.Y + 13.5), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }
                    else
                    {
                        g.DrawString("O", new Font("Segoe UI", 12, FontStyle.Regular), Brushes.DimGray, _bar.X + 59, (float)(_bar.Y + 13.5), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }
                    break;
            }

            Rectangle switchRectangle = new Rectangle(switchXLoc, 0, Width - 38, Height);
            GraphicsPath switchPath = RoundRectangle.RoundRect(switchRectangle, 4);
            LinearGradientBrush switchButtonLgb = new LinearGradientBrush(switchRectangle, Color.FromArgb(253, 253, 253), Color.FromArgb(240, 238, 237), LinearGradientMode.Vertical);

            // Fill switch background gradient
            g.FillPath(switchButtonLgb, switchPath);

            // Draw borders
            if (_toggled == true)
            {
                g.DrawPath(new Pen(Color.FromArgb(185, 89, 55)), switchPath);
                g.DrawPath(new Pen(Color.FromArgb(185, 89, 55)), controlPath);
            }
            else
            {
                g.DrawPath(new Pen(Color.FromArgb(181, 181, 181)), switchPath);
                g.DrawPath(new Pen(Color.FromArgb(181, 181, 181)), controlPath);
            }
        }
    }

    #endregion
    #region CheckBox

    [DefaultEvent("CheckedChanged")]
    class AmbianceCheckBox : Control
    {

        #region Variables

        private GraphicsPath _shape;
        private LinearGradientBrush _gb;
        private Rectangle _r1;
        private Rectangle _r2;
        private bool _checked;
        public event CheckedChangedEventHandler CheckedChanged;
        public delegate void CheckedChangedEventHandler(object sender);

        #endregion
        #region Properties

        public bool Checked
        {
            get { return _checked; }
            set
            {
                _checked = value;
                if (CheckedChanged != null)
                {
                    CheckedChanged(this);
                }
                Invalidate();
            }
        }

        #endregion

        public AmbianceCheckBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);

            BackColor = Color.Transparent;
            DoubleBuffered = true;
            // Reduce control flicker
            Font = new Font("Segoe UI", 12);
            Size = new Size(171, 26);
        }

        protected override void OnClick(EventArgs e)
        {
            _checked = !_checked;
            if (CheckedChanged != null)
            {
                CheckedChanged(this);
            }
            Focus();
            Invalidate();
            base.OnClick(e);
        }

        protected override void OnTextChanged(System.EventArgs e)
        {
            Invalidate();
            base.OnTextChanged(e);
        }

        protected override void OnResize(System.EventArgs e)
        {
            if (Width > 0 && Height > 0)
            {
                _shape = new GraphicsPath();

                _r1 = new Rectangle(17, 0, Width, Height + 1);
                _r2 = new Rectangle(0, 0, Width, Height);
                _gb = new LinearGradientBrush(new Rectangle(0, 0, 25, 25), Color.FromArgb(213, 85, 32), Color.FromArgb(224, 123, 82), 90);

                var myDrawer = _shape;
                myDrawer.AddArc(0, 0, 7, 7, 180, 90);
                myDrawer.AddArc(7, 0, 7, 7, -90, 90);
                myDrawer.AddArc(7, 7, 7, 7, 0, 90);
                myDrawer.AddArc(0, 7, 7, 7, 90, 90);
                myDrawer.CloseAllFigures();
                Height = 15;
            }

            Invalidate();
            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var myDrawer = e.Graphics;
            myDrawer.Clear(Parent.BackColor);
            myDrawer.SmoothingMode = SmoothingMode.AntiAlias;

            myDrawer.FillPath(_gb, _shape);
            // Fill the body of the CheckBox
            myDrawer.DrawPath(new Pen(Color.FromArgb(182, 88, 55)), _shape);
            // Draw the border

            myDrawer.DrawString(Text, Font, new SolidBrush(Color.FromArgb(76, 76, 95)), new Rectangle(17, 0, Width, Height - 1), new StringFormat { LineAlignment = StringAlignment.Center });

            if (Checked)
            {
                myDrawer.DrawString("ü", new Font("Wingdings", 12), new SolidBrush(Color.FromArgb(255, 255, 255)), new Rectangle(-2, 1, Width, Height + 2), new StringFormat { LineAlignment = StringAlignment.Center });
            }
            e.Dispose();
        }
    }

    #endregion
    #region RadioButton

    [DefaultEvent("CheckedChanged")]
    class AmbianceRadioButton : Control
    {

        #region Enums

        public enum MouseState : byte
        {
            None = 0,
            Over = 1,
            Down = 2,
            Block = 3
        }

        #endregion
        #region Variables

        private bool _checked;
        public event CheckedChangedEventHandler CheckedChanged;
        public delegate void CheckedChangedEventHandler(object sender);

        #endregion
        #region Properties

        public bool Checked
        {
            get { return _checked; }
            set
            {
                _checked = value;
                InvalidateControls();
                if (CheckedChanged != null)
                {
                    CheckedChanged(this);
                }
                Invalidate();
            }
        }

        #endregion
        #region EventArgs

        protected override void OnTextChanged(System.EventArgs e)
        {
            Invalidate();
            base.OnTextChanged(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 15;
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            if (!_checked)
                Checked = true;
            base.OnMouseDown(e);
            Focus();
        }

        #endregion

        public AmbianceRadioButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;
            Font = new Font("Segoe UI", 12);
            Width = 193;
        }

        private void InvalidateControls()
        {
            if (!IsHandleCreated || !_checked)
                return;

            foreach (Control control in Parent.Controls)
            {
                if (!object.ReferenceEquals(control, this) && control is AmbianceRadioButton)
                {
                    ((AmbianceRadioButton)control).Checked = false;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var myDrawer = e.Graphics;

            myDrawer.Clear(Parent.BackColor);
            myDrawer.SmoothingMode = SmoothingMode.AntiAlias;

            // Fill the body of the ellipse with a gradient
            LinearGradientBrush lgb = new LinearGradientBrush(new Rectangle(new Point(0, 0), new Size(14, 14)), Color.FromArgb(213, 85, 32), Color.FromArgb(224, 123, 82), 90);
            myDrawer.FillEllipse(lgb, new Rectangle(new Point(0, 0), new Size(14, 14)));

            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(new Rectangle(0, 0, 14, 14));
            myDrawer.SetClip(gp);
            myDrawer.ResetClip();

            // Draw ellipse border
            myDrawer.DrawEllipse(new Pen(Color.FromArgb(182, 88, 55)), new Rectangle(new Point(0, 0), new Size(14, 14)));

            // Draw an ellipse inside the body
            if (_checked)
            {
                SolidBrush ellipseColor = new SolidBrush(Color.FromArgb(255, 255, 255));
                myDrawer.FillEllipse(ellipseColor, new Rectangle(new Point(4, 4), new Size(6, 6)));
            }
            myDrawer.DrawString(Text, Font, new SolidBrush(Color.FromArgb(76, 76, 95)), 16, 7, new StringFormat { LineAlignment = StringAlignment.Center });
            e.Dispose();
        }
    }

    #endregion
    #region  ComboBox

    public class AmbianceComboBox : ComboBox
    {

        #region  Variables

        private int _startIndex = 0;
        private Color _hoverSelectionColor; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.

        #endregion
        #region  Custom Properties

        public int StartIndex
        {
            get
            {
                return _startIndex;
            }
            set
            {
                _startIndex = value;
                try
                {
                    base.SelectedIndex = value;
                }
                catch
                {
                }
                Invalidate();
            }
        }

        public Color HoverSelectionColor
        {
            get
            {
                return _hoverSelectionColor;
            }
            set
            {
                _hoverSelectionColor = value;
                Invalidate();
            }
        }

        #endregion
        #region  EventArgs

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            LinearGradientBrush lgb = new LinearGradientBrush(e.Bounds, Color.FromArgb(246, 132, 85), Color.FromArgb(231, 108, 57), 90.0F);

            if (System.Convert.ToInt32(e.State & DrawItemState.Selected) == (int)DrawItemState.Selected)
            {
                if (!(e.Index == -1))
                {
                    e.Graphics.FillRectangle(lgb, e.Bounds);
                    e.Graphics.DrawString(GetItemText(Items[e.Index]), e.Font, Brushes.WhiteSmoke, e.Bounds);
                }
            }
            else
            {
                if (!(e.Index == -1))
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(242, 241, 240)), e.Bounds);
                    e.Graphics.DrawString(GetItemText(Items[e.Index]), e.Font, Brushes.DimGray, e.Bounds);
                }
            }
            lgb.Dispose();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            SuspendLayout();
            Update();
            ResumeLayout();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (!Focused)
            {
                SelectionLength = 0;
            }
        }

        #endregion

        public AmbianceComboBox()
        {
            SetStyle((ControlStyles)139286, true);
            SetStyle(ControlStyles.Selectable, false);

            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;

            BackColor = Color.FromArgb(246, 246, 246);
            ForeColor = Color.FromArgb(142, 142, 142);
            Size = new Size(135, 26);
            ItemHeight = 20;
            DropDownHeight = 100;
            Font = new Font("Segoe UI", 10, FontStyle.Regular);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            LinearGradientBrush lgb = default(LinearGradientBrush);
            GraphicsPath gp = default(GraphicsPath);

            e.Graphics.Clear(Parent.BackColor);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Create a curvy border
            gp = RoundRectangle.RoundRect(0, 0, Width - 1, Height - 1, 5);
            // Fills the body of the rectangle with a gradient
            lgb = new LinearGradientBrush(ClientRectangle, Color.FromArgb(253, 252, 252), Color.FromArgb(239, 237, 236), 90.0F);

            e.Graphics.SetClip(gp);
            e.Graphics.FillRectangle(lgb, ClientRectangle);
            e.Graphics.ResetClip();

            // Draw rectangle border
            e.Graphics.DrawPath(new Pen(Color.FromArgb(180, 180, 180)), gp);
            // Draw string
            e.Graphics.DrawString(Text, Font, new SolidBrush(Color.FromArgb(76, 76, 97)), new Rectangle(3, 0, Width - 20, Height), new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Near
            });
            e.Graphics.DrawString("6", new Font("Marlett", 13, FontStyle.Regular), new SolidBrush(Color.FromArgb(119, 119, 118)), new Rectangle(3, 0, Width - 4, Height), new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Far
            });
            e.Graphics.DrawLine(new Pen(Color.FromArgb(224, 222, 220)), Width - 24, 4, Width - 24, this.Height - 5);
            e.Graphics.DrawLine(new Pen(Color.FromArgb(250, 249, 249)), Width - 25, 4, Width - 25, this.Height - 5);

            gp.Dispose();
            lgb.Dispose();
        }
    }

    #endregion
    #region  NumericUpDown

    public class AmbianceNumericUpDown : Control
    {

        #region  Enums

        public enum _TextAlignment
        {
            Near,
            Center
        }

        #endregion
        #region  Variables

        private GraphicsPath _shape;
        private Pen _p1;

        private long _value;
        private long _minimum;
        private long _maximum;
        private int _xval;
        private bool _keyboardNum;
        private _TextAlignment _myStringAlignment;

        private Timer _longPressTimer = new Timer();

        #endregion
        #region  Properties

        public long Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value <= _maximum & value >= _minimum)
                {
                    _value = value;
                }
                Invalidate();
            }
        }

        public long Minimum
        {
            get
            {
                return _minimum;
            }
            set
            {
                if (value < _maximum)
                {
                    _minimum = value;
                }
                if (_value < _minimum)
                {
                    _value = Minimum;
                }
                Invalidate();
            }
        }

        public long Maximum
        {
            get
            {
                return _maximum;
            }
            set
            {
                if (value > _minimum)
                {
                    _maximum = value;
                }
                if (_value > _maximum)
                {
                    _value = _maximum;
                }
                Invalidate();
            }
        }

        public _TextAlignment TextAlignment
        {
            get
            {
                return _myStringAlignment;
            }
            set
            {
                _myStringAlignment = value;
                Invalidate();
            }
        }

        #endregion
        #region  EventArgs

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            Height = 28;
            MinimumSize = new Size(93, 28);
            _shape = new GraphicsPath();
            _shape.AddArc(0, 0, 10, 10, 180, 90);
            _shape.AddArc(Width - 11, 0, 10, 10, -90, 90);
            _shape.AddArc(Width - 11, Height - 11, 10, 10, 0, 90);
            _shape.AddArc(0, Height - 11, 10, 10, 90, 90);
            _shape.CloseAllFigures();
        }

        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseMove(e);
            _xval = e.Location.X;
            Invalidate();

            if (e.X < Width - 50)
            {
                Cursor = Cursors.IBeam;
            }
            else
            {
                Cursor = Cursors.Default;
            }
            if (e.X > this.Width - 25 && e.X < this.Width - 10)
            {
                Cursor = Cursors.Hand;
            }
            if (e.X > this.Width - 44 && e.X < this.Width - 33)
            {
                Cursor = Cursors.Hand;
            }
        }

        private void ClickButton()
        {
            if (_xval > this.Width - 25 && _xval < this.Width - 10)
            {
                if (Value + 1 <= _maximum)
                {
                    _value++;
                }
            }
            else
            {
                if (_xval > this.Width - 44 && _xval < this.Width - 33)
                {
                    if (Value - 1 >= _minimum)
                    {
                        _value--;
                    }
                }
                _keyboardNum = !_keyboardNum;
            }
            Focus();
            Invalidate();
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseClick(e);
            ClickButton();
            _longPressTimer.Start();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _longPressTimer.Stop();
        }
        private void LongPressTimer_Tick(object sender, EventArgs e)
        {
            ClickButton();
        }
        protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            try
            {
                if (_keyboardNum == true)
                {
                    _value = long.Parse(_value.ToString() + e.KeyChar.ToString().ToString());
                }
                if (_value > _maximum)
                {
                    _value = _maximum;
                }
            }
            catch (Exception)
            {
            }
        }

        protected override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.KeyCode == Keys.Back)
            {
                string temporaryValue = _value.ToString();
                temporaryValue = temporaryValue.Remove(Convert.ToInt32(temporaryValue.Length - 1));
                if (temporaryValue.Length == 0)
                {
                    temporaryValue = "0";
                }
                _value = Convert.ToInt32(temporaryValue);
            }
            Invalidate();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (e.Delta > 0)
            {
                if (Value + 1 <= _maximum)
                {
                    _value++;
                }
                Invalidate();
            }
            else
            {
                if (Value - 1 >= _minimum)
                {
                    _value--;
                }
                Invalidate();
            }
        }

        #endregion

        public AmbianceNumericUpDown()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);

            _p1 = new Pen(Color.FromArgb(180, 180, 180));
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(76, 76, 76);
            _minimum = 0;
            _maximum = 100;
            Font = new Font("Tahoma", 11);
            Size = new Size(70, 28);
            MinimumSize = new Size(62, 28);
            DoubleBuffered = true;

            _longPressTimer.Tick += LongPressTimer_Tick;
            _longPressTimer.Interval = 300;
        }

        public void Increment(int value)
        {
            this._value += value;
            Invalidate();
        }

        public void Decrement(int value)
        {
            this._value -= value;
            Invalidate();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap b = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(b);
            LinearGradientBrush backgroundLgb = default(LinearGradientBrush);

            backgroundLgb = new LinearGradientBrush(ClientRectangle, Color.FromArgb(246, 246, 246), Color.FromArgb(254, 254, 254), 90.0F);

            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.Clear(Color.Transparent); // Set control background color
            g.FillPath(backgroundLgb, _shape); // Draw background
            g.DrawPath(_p1, _shape); // Draw border

            g.DrawString("+", new Font("Tahoma", 14), new SolidBrush(Color.FromArgb(75, 75, 75)), new Rectangle(Width - 25, 1, 19, 30));
            g.DrawLine(new Pen(Color.FromArgb(229, 228, 227)), Width - 28, 1, Width - 28, this.Height - 2);
            g.DrawString("-", new Font("Tahoma", 14), new SolidBrush(Color.FromArgb(75, 75, 75)), new Rectangle(Width - 44, 1, 19, 30));
            g.DrawLine(new Pen(Color.FromArgb(229, 228, 227)), Width - 48, 1, Width - 48, this.Height - 2);

            switch (_myStringAlignment)
            {
                case _TextAlignment.Near:
                    g.DrawString(System.Convert.ToString(Value), Font, new SolidBrush(ForeColor), new Rectangle(5, 0, Width - 1, Height - 1), new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
                    break;
                case _TextAlignment.Center:
                    g.DrawString(System.Convert.ToString(Value), Font, new SolidBrush(ForeColor), new Rectangle(0, 0, Width - 1, Height - 1), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    break;
            }
            e.Graphics.DrawImage((Image)b.Clone(), 0, 0);
            g.Dispose();
            b.Dispose();
        }
    }

    #endregion
    #region  TrackBar

    [DefaultEvent("ValueChanged")]
    public class AmbianceTrackBar : Control
    {

        #region  Enums

        public enum ValueDivisor
        {
            By1 = 1,
            By10 = 10,
            By100 = 100,
            By1000 = 1000
        }

        #endregion
        #region  Variables

        private GraphicsPath _pipeBorder;
        private GraphicsPath _fillValue;
        private Rectangle _trackBarHandleRect;
        private bool _cap;
        private int _valueDrawer;

        private Size _thumbSize = new Size(15, 15);
        private Rectangle _trackThumb;

        private int _minimum = 0;
        private int _maximum = 10;
        private int _value = 0;

        private bool _drawValueString = false;
        private bool _jumpToMouse = false;
        private ValueDivisor _dividedValue = ValueDivisor.By1;

        #endregion
        #region  Properties

        public int Minimum
        {
            get
            {
                return _minimum;
            }
            set
            {

                if (value >= _maximum)
                {
                    value = _maximum - 10;
                }
                if (_value < value)
                {
                    _value = value;
                }

                _minimum = value;
                Invalidate();
            }
        }

        public int Maximum
        {
            get
            {
                return _maximum;
            }
            set
            {

                if (value <= _minimum)
                {
                    value = _minimum + 10;
                }
                if (_value > value)
                {
                    _value = value;
                }

                _maximum = value;
                Invalidate();
            }
        }

        public delegate void ValueChangedEventHandler();
        private ValueChangedEventHandler _valueChangedEvent;

        public event ValueChangedEventHandler ValueChanged
        {
            add
            {
                _valueChangedEvent = (ValueChangedEventHandler)System.Delegate.Combine(_valueChangedEvent, value);
            }
            remove
            {
                _valueChangedEvent = (ValueChangedEventHandler)System.Delegate.Remove(_valueChangedEvent, value);
            }
        }

        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    if (value < _minimum)
                    {
                        _value = _minimum;
                    }
                    else
                    {
                        if (value > _maximum)
                        {
                            _value = _maximum;
                        }
                        else
                        {
                            _value = value;
                        }
                    }
                    Invalidate();
                    if (_valueChangedEvent != null)
                        _valueChangedEvent();
                }
            }
        }

        public ValueDivisor ValueDivison
        {
            get
            {
                return _dividedValue;
            }
            set
            {
                _dividedValue = value;
                Invalidate();
            }
        }

        [Browsable(false)]
        public float ValueToSet
        {
            get
            {
                return _value / (int)_dividedValue;
            }
            set
            {
                Value = (int)(value * (int)_dividedValue);
            }
        }

        public bool JumpToMouse
        {
            get
            {
                return _jumpToMouse;
            }
            set
            {
                _jumpToMouse = value;
                Invalidate();
            }
        }

        public bool DrawValueString
        {
            get
            {
                return _drawValueString;
            }
            set
            {
                _drawValueString = value;
                if (_drawValueString == true)
                {
                    Height = 35;
                }
                else
                {
                    Height = 22;
                }
                Invalidate();
            }
        }

        #endregion
        #region  EventArgs

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            checked
            {
                bool flag = this._cap && e.X > -1 && e.X < this.Width + 1;
                if (flag)
                {
                    this.Value = this._minimum + (int)Math.Round((this._maximum - this._minimum) * (e.X / (double)this.Width));
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            bool flag = e.Button == MouseButtons.Left;
            checked
            {
                if (flag)
                {
                    this._valueDrawer = (int)Math.Round((this._value - this._minimum) / (double)(this._maximum - this._minimum) * (this.Width - 11));
                    this._trackBarHandleRect = new Rectangle(this._valueDrawer, 0, 25, 25);
                    this._cap = this._trackBarHandleRect.Contains(e.Location);
                    this.Focus();
                    flag = this._jumpToMouse;
                    if (flag)
                    {
                        this.Value = this._minimum + (int)Math.Round((this._maximum - this._minimum) * (e.X / (double)this.Width));
                    }
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _cap = false;
        }

        #endregion

        public AmbianceTrackBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer, true);

            Size = new Size(80, 22);
            MinimumSize = new Size(47, 22);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_drawValueString == true)
            {
                Height = 35;
            }
            else
            {
                Height = 22;
            }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            g.Clear(Parent.BackColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            _trackThumb = new Rectangle(8, 10, Width - 16, 2);
            _pipeBorder = RoundRectangle.RoundRect(1, 8, Width - 3, 5, 2);

            try
            {
                this._valueDrawer = (int)Math.Round((this._value - this._minimum) / (double)(this._maximum - this._minimum) * (this.Width - 11));
            }
            catch (Exception)
            {
            }

            _trackBarHandleRect = new Rectangle(_valueDrawer, 0, 10, 20);

            g.SetClip(_pipeBorder); // Set the clipping region of this Graphics to the specified GraphicsPath
            g.FillPath(new SolidBrush(Color.FromArgb(221, 221, 221)), _pipeBorder);
            _fillValue = RoundRectangle.RoundRect(1, 8, _trackBarHandleRect.X + _trackBarHandleRect.Width - 4, 5, 2);

            g.ResetClip(); // Reset the clip region of this Graphics to an infinite region

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.DrawPath(new Pen(Color.FromArgb(200, 200, 200)), _pipeBorder); // Draw pipe border
            g.FillPath(new SolidBrush(Color.FromArgb(217, 99, 50)), _fillValue);

            g.FillEllipse(new SolidBrush(Color.FromArgb(244, 244, 244)), this._trackThumb.X + (int)Math.Round(unchecked(this._trackThumb.Width * (this.Value / (double)this.Maximum))) - (int)Math.Round(this._thumbSize.Width / 2.0), this._trackThumb.Y + (int)Math.Round(this._trackThumb.Height / 2.0) - (int)Math.Round(this._thumbSize.Height / 2.0), this._thumbSize.Width, this._thumbSize.Height);
            g.DrawEllipse(new Pen(Color.FromArgb(180, 180, 180)), this._trackThumb.X + (int)Math.Round(unchecked(this._trackThumb.Width * (this.Value / (double)this.Maximum))) - (int)Math.Round(this._thumbSize.Width / 2.0), this._trackThumb.Y + (int)Math.Round(this._trackThumb.Height / 2.0) - (int)Math.Round(this._thumbSize.Height / 2.0), this._thumbSize.Width, this._thumbSize.Height);

            if (_drawValueString == true)
            {
                g.DrawString(System.Convert.ToString(ValueToSet), Font, Brushes.DimGray, 1, 20);
            }
        }
    }

    #endregion
    #region  Panel

    public class AmbiancePanel : ContainerControl
    {
        public AmbiancePanel()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, false);
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            this.Font = new Font("Tahoma", 9);
            this.BackColor = Color.White;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, Width, Height));
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, Width - 1, Height - 1));
            g.DrawRectangle(new Pen(Color.FromArgb(211, 208, 205)), 0, 0, Width - 1, Height - 1);
        }
    }

    #endregion
    #region TextBox

    [DefaultEvent("TextChanged")]
    class AmbianceTextBox : Control
    {
        #region Variables

        public TextBox AmbianceTb = new TextBox();
        private GraphicsPath _shape;
        private int _maxchars = 32767;
        private bool _readOnly;
        private bool _multiline;
        private HorizontalAlignment _alnType;
        private bool _isPasswordMasked = false;
        private Pen _p1;
        private SolidBrush _b1;

        #endregion
        #region Properties

        public HorizontalAlignment TextAlignment
        {
            get { return _alnType; }
            set
            {
                _alnType = value;
                Invalidate();
            }
        }
        public int MaxLength
        {
            get { return _maxchars; }
            set
            {
                _maxchars = value;
                AmbianceTb.MaxLength = MaxLength;
                Invalidate();
            }
        }

        public bool UseSystemPasswordChar
        {
            get { return _isPasswordMasked; }
            set
            {
                AmbianceTb.UseSystemPasswordChar = UseSystemPasswordChar;
                _isPasswordMasked = value;
                Invalidate();
            }
        }
        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                _readOnly = value;
                if (AmbianceTb != null)
                {
                    AmbianceTb.ReadOnly = value;
                }
            }
        }
        public bool Multiline
        {
            get { return _multiline; }
            set
            {
                _multiline = value;
                if (AmbianceTb != null)
                {
                    AmbianceTb.Multiline = value;

                    if (value)
                    {
                        AmbianceTb.Height = Height - 10;
                    }
                    else
                    {
                        Height = AmbianceTb.Height + 10;
                    }
                }
            }
        }

        #endregion
        #region EventArgs

        protected override void OnTextChanged(System.EventArgs e)
        {
            base.OnTextChanged(e);
            AmbianceTb.Text = Text;
            Invalidate();
        }

        private void OnBaseTextChanged(object s, EventArgs e)
        {
            Text = AmbianceTb.Text;
        }

        protected override void OnForeColorChanged(System.EventArgs e)
        {
            base.OnForeColorChanged(e);
            AmbianceTb.ForeColor = ForeColor;
            Invalidate();
        }

        protected override void OnFontChanged(System.EventArgs e)
        {
            base.OnFontChanged(e);
            AmbianceTb.Font = Font;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        private void _OnKeyDown(object obj, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                AmbianceTb.SelectAll();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                AmbianceTb.Copy();
                e.SuppressKeyPress = true;
            }
        }

        private void _Enter(object obj, EventArgs e)
        {
            _p1 = new Pen(Color.FromArgb(205, 87, 40));
            Refresh();
        }

        private void _Leave(object obj, EventArgs e)
        {
            _p1 = new Pen(Color.FromArgb(180, 180, 180));
            Refresh();
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            if (_multiline)
            {
                AmbianceTb.Height = Height - 10;
            }
            else
            {
                Height = AmbianceTb.Height + 10;
            }

            _shape = new GraphicsPath();
            var with1 = _shape;
            with1.AddArc(0, 0, 10, 10, 180, 90);
            with1.AddArc(Width - 11, 0, 10, 10, -90, 90);
            with1.AddArc(Width - 11, Height - 11, 10, 10, 0, 90);
            with1.AddArc(0, Height - 11, 10, 10, 90, 90);
            with1.CloseAllFigures();
        }

        protected override void OnGotFocus(System.EventArgs e)
        {
            base.OnGotFocus(e);
            AmbianceTb.Focus();
        }

        #endregion
        public void AddTextBox()
        {
            var tb = AmbianceTb;
            tb.Size = new Size(Width - 10, 33);
            tb.Location = new Point(7, 4);
            tb.Text = String.Empty;
            tb.BorderStyle = BorderStyle.None;
            tb.TextAlign = HorizontalAlignment.Left;
            tb.Font = new Font("Tahoma", 11);
            tb.UseSystemPasswordChar = UseSystemPasswordChar;
            tb.Multiline = false;
            AmbianceTb.KeyDown += _OnKeyDown;
            AmbianceTb.Enter += _Enter;
            AmbianceTb.Leave += _Leave;
            AmbianceTb.TextChanged += OnBaseTextChanged;

        }

        public AmbianceTextBox()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);

            AddTextBox();
            Controls.Add(AmbianceTb);

            _p1 = new Pen(Color.FromArgb(180, 180, 180)); // P1 = Border color
            _b1 = new SolidBrush(Color.White); // B1 = Rect Background color
            BackColor = Color.Transparent;
            ForeColor = Color.DimGray;

            Text = null;
            Font = new Font("Tahoma", 11);
            Size = new Size(135, 33);
            DoubleBuffered = true;
        }
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap b = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(b);

            g.SmoothingMode = SmoothingMode.AntiAlias;

            var tb = AmbianceTb;
            tb.Width = Width - 10;
            tb.TextAlign = TextAlignment;
            tb.UseSystemPasswordChar = UseSystemPasswordChar;

            g.Clear(Color.Transparent);
            g.FillPath(_b1, _shape); // Draw background
            g.DrawPath(_p1, _shape); // Draw border

            e.Graphics.DrawImage((Image)b.Clone(), 0, 0);
            g.Dispose();
            b.Dispose();
        }

    }

    #endregion
    #region RichTextBox

    [DefaultEvent("TextChanged")]
    class AmbianceRichTextBox : Control
    {

        #region Variables

        public RichTextBox AmbianceRtb = new RichTextBox();
        private bool _readOnly;
        private bool _wordWrap;
        private bool _autoWordSelection;
        private GraphicsPath _shape;
        private Pen _p1;

        #endregion
        #region Properties

        public override string Text
        {
            get { return AmbianceRtb.Text; }
            set
            {
                AmbianceRtb.Text = value;
                Invalidate();
            }
        }
        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                _readOnly = value;
                if (AmbianceRtb != null)
                {
                    AmbianceRtb.ReadOnly = value;
                }
            }
        }
        public bool WordWrap
        {
            get { return _wordWrap; }
            set
            {
                _wordWrap = value;
                if (AmbianceRtb != null)
                {
                    AmbianceRtb.WordWrap = value;
                }
            }
        }
        public bool AutoWordSelection
        {
            get { return _autoWordSelection; }
            set
            {
                _autoWordSelection = value;
                if (AmbianceRtb != null)
                {
                    AmbianceRtb.AutoWordSelection = value;
                }
            }
        }
        #endregion
        #region EventArgs

        protected override void OnForeColorChanged(System.EventArgs e)
        {
            base.OnForeColorChanged(e);
            AmbianceRtb.ForeColor = ForeColor;
            Invalidate();
        }

        protected override void OnFontChanged(System.EventArgs e)
        {
            base.OnFontChanged(e);
            AmbianceRtb.Font = Font;
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        protected override void OnSizeChanged(System.EventArgs e)
        {
            base.OnSizeChanged(e);
            AmbianceRtb.Size = new Size(Width - 13, Height - 11);
        }

        private void _Enter(object obj, EventArgs e)
        {
            _p1 = new Pen(Color.FromArgb(205, 87, 40));
            Refresh();
        }

        private void _Leave(object obj, EventArgs e)
        {
            _p1 = new Pen(Color.FromArgb(180, 180, 180));
            Refresh();
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);

            _shape = new GraphicsPath();
            var shape = _shape;
            shape.AddArc(0, 0, 10, 10, 180, 90);
            shape.AddArc(Width - 11, 0, 10, 10, -90, 90);
            shape.AddArc(Width - 11, Height - 11, 10, 10, 0, 90);
            shape.AddArc(0, Height - 11, 10, 10, 90, 90);
            shape.CloseAllFigures();
        }

        public void _TextChanged(object s, EventArgs e)
        {
            AmbianceRtb.Text = Text;
        }

        #endregion

        public void AddRichTextBox()
        {
            var rtb = AmbianceRtb;
            rtb.BackColor = Color.White;
            rtb.Size = new Size(Width - 10, 100);
            rtb.Location = new Point(7, 5);
            rtb.Text = string.Empty;
            rtb.BorderStyle = BorderStyle.None;
            rtb.Font = new Font("Tahoma", 10);
            rtb.Multiline = true;
        }

        public AmbianceRichTextBox()
            : base()
        {

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);

            AddRichTextBox();
            Controls.Add(AmbianceRtb);
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(76, 76, 76);

            _p1 = new Pen(Color.FromArgb(180, 180, 180));
            Text = null;
            Font = new Font("Tahoma", 10);
            Size = new Size(150, 100);
            WordWrap = true;
            AutoWordSelection = false;
            DoubleBuffered = true;

            AmbianceRtb.Enter += _Enter;
            AmbianceRtb.Leave += _Leave;
            TextChanged += _TextChanged;
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap b = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(b);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.Transparent);
            g.FillPath(Brushes.White, this._shape);
            g.DrawPath(_p1, this._shape);
            g.Dispose();
            e.Graphics.DrawImage((Image)b.Clone(), 0, 0);
            b.Dispose();
        }
    }

    #endregion
    #region  ListBox

    public class AmbianceListBox : ListBox
    {

        public AmbianceListBox()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.DrawMode = DrawMode.OwnerDrawFixed;
            IntegralHeight = false;
            ItemHeight = 18;
            Font = new Font("Seoge UI", 11, FontStyle.Regular);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            e.DrawBackground();
            LinearGradientBrush lgb = new LinearGradientBrush(e.Bounds, Color.FromArgb(246, 132, 85), Color.FromArgb(231, 108, 57), 90.0F);
            if (System.Convert.ToInt32(e.State & DrawItemState.Selected) == (int)DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(lgb, e.Bounds);
            }
            using (SolidBrush b = new SolidBrush(e.ForeColor))
            {
                if (base.Items.Count == 0)
                {
                    return;
                }
                else
                {
                    e.Graphics.DrawString(base.GetItemText(base.Items[e.Index]), e.Font, b, e.Bounds);
                }
            }

            lgb.Dispose();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Region myRegion = new Region(e.ClipRectangle);
            e.Graphics.FillRegion(new SolidBrush(this.BackColor), myRegion);

            if (this.Items.Count > 0)
            {
                for (int i = 0; i <= this.Items.Count - 1; i++)
                {
                    System.Drawing.Rectangle regionRect = this.GetItemRectangle(i);
                    if (e.ClipRectangle.IntersectsWith(regionRect))
                    {
                        if ((this.SelectionMode == SelectionMode.One && this.SelectedIndex == i) || (this.SelectionMode == SelectionMode.MultiSimple && this.SelectedIndices.Contains(i)) || (this.SelectionMode == SelectionMode.MultiExtended && this.SelectedIndices.Contains(i)))
                        {
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font, regionRect, i, DrawItemState.Selected, this.ForeColor, this.BackColor));
                        }
                        else
                        {
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font, regionRect, i, DrawItemState.Default, Color.FromArgb(60, 60, 60), this.BackColor));
                        }
                        myRegion.Complement(regionRect);
                    }
                }
            }
        }
    }

    #endregion
    #region  TabControl

    public class AmbianceTabControl : TabControl
    {

        public AmbianceTabControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();

            ItemSize = new Size(80, 24);
            Alignment = TabAlignment.Top;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle itemBoundsRect = new Rectangle();
            g.Clear(Parent.BackColor);
            for (int tabIndex = 0; tabIndex <= TabCount - 1; tabIndex++)
            {
                itemBoundsRect = GetTabRect(tabIndex);
                if (!(tabIndex == SelectedIndex))
                {
                    g.DrawString(TabPages[tabIndex].Text, new Font(Font.Name, Font.Size - 2, FontStyle.Bold), new SolidBrush(Color.FromArgb(80, 76, 76)), new Rectangle(GetTabRect(tabIndex).Location, GetTabRect(tabIndex).Size), new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center
                    });
                }
            }

            // Draw container rectangle
            g.FillPath(new SolidBrush(Color.FromArgb(247, 246, 246)), RoundRectangle.RoundRect(0, 23, Width - 1, Height - 24, 2));
            g.DrawPath(new Pen(Color.FromArgb(201, 198, 195)), RoundRectangle.RoundRect(0, 23, Width - 1, Height - 24, 2));

            for (int itemIndex = 0; itemIndex <= TabCount - 1; itemIndex++)
            {
                itemBoundsRect = GetTabRect(itemIndex);
                if (itemIndex == SelectedIndex)
                {

                    // Draw header tabs
                    g.DrawPath(new Pen(Color.FromArgb(201, 198, 195)), RoundRectangle.RoundedTopRect(new Rectangle(new Point(itemBoundsRect.X - 2, itemBoundsRect.Y - 2), new Size(itemBoundsRect.Width + 3, itemBoundsRect.Height)), 7));
                    g.FillPath(new SolidBrush(Color.FromArgb(247, 246, 246)), RoundRectangle.RoundedTopRect(new Rectangle(new Point(itemBoundsRect.X - 1, itemBoundsRect.Y - 1), new Size(itemBoundsRect.Width + 2, itemBoundsRect.Height)), 7));

                    try
                    {
                        g.DrawString(TabPages[itemIndex].Text, new Font(Font.Name, Font.Size - 1, FontStyle.Bold), new SolidBrush(Color.FromArgb(80, 76, 76)), new Rectangle(GetTabRect(itemIndex).Location, GetTabRect(itemIndex).Size), new StringFormat
                        {
                            LineAlignment = StringAlignment.Center,
                            Alignment = StringAlignment.Center
                        });
                        TabPages[itemIndex].BackColor = Color.FromArgb(247, 246, 246);
                    }
                    catch
                    {
                    }
                }
            }
        }
    }

    #endregion
}
