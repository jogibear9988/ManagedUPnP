//////////////////////////////////////////////////////////////////////////////////
//  Managed UPnP
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://managedupnp.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    /// <summary>
    /// Encapsulates a Rich Text Box which can have the Auto Scroll
    /// enabled or disapled for appending new text.
    /// </summary>
    public partial class ctlAutoScrollRichTextBox : RichTextBox
    {
        #region Internal Classes

        /// <summary>
        /// Contains all API definitions used for the auto scroll rich text box.
        /// </summary>
        internal static class EnhTextBoxAPIFunctions
        {
            #region Enumerations

            /// <summary>
            /// Contains standard windows messages.
            /// </summary>
            internal enum Messages : int
            {
                /// <summary>
                /// Specifies user defined message.
                /// </summary>
                WM_USER = 0x0400,

                /// <summary>
                /// Sets whether a window can redraw.
                /// </summary>
                WM_SETREDRAW = 0x000B,

                /// <summary>
                /// Raised when the windows client area is painted.
                /// </summary>
                WM_PAINT = 0x000F,

                /// <summary>
                /// Raised when the windows non-client area is painted.
                /// </summary>
                WM_NCPAINT = 0x0085,

                /// <summary>
                /// Raised when the background of the window needs erasing.
                /// </summary>
                WM_ERASEBKGND = 0x0014,

                /// <summary>
                /// Gets the selection range for an edit box.
                /// </summary>
                EM_GETSEL = 0x00B0,

                /// <summary>
                /// Sets the selection range for an edit box.
                /// </summary>
                EM_SETSEL = 0x00B1,

                /// <summary>
                /// Gets the scroll position for an edit box.
                /// </summary>
                EM_GETSCROLLPOS = WM_USER + 221,

                /// <summary>
                /// Sets the scroll position for an edit box.
                /// </summary>
                EM_SETSCROLLPOS = WM_USER + 222
            }

            #endregion

            #region Structures

            /// <summary>
            /// Used to representing a point for API functions.
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            internal struct POINTAPI
            {
                #region Locals

                /// <summary>
                /// The x position of the point.
                /// </summary>
                internal int X;

                /// <summary>
                /// The y position of the point.
                /// </summary>
                internal int Y;

                #endregion

                #region Explicit Operators

                /// <summary>
                /// Casts a System.Drawing.Point to a POINTAPI.
                /// </summary>
                /// <param name="point">The System.Drawing.Point.</param>
                /// <returns>A new POINTAPI structure.</returns>
                public static explicit operator POINTAPI(Point point)
                {
                    return new POINTAPI()
                    {
                        X = point.X,
                        Y = point.Y
                    };
                }

                /// <summary>
                /// Casts a POINTAPI to System.Drawing.Point.
                /// </summary>
                /// <param name="point">The POINTAPI.</param>
                /// <returns>A new System.Drawing.Point structure.</returns>
                public static explicit operator Point(POINTAPI point)
                {
                    return new Point(point.X, point.Y);
                }

                #endregion
            }

            #endregion

            #region Methods

            /// <summary>
            /// Gets the caret position in logical coordinates.
            /// </summary>
            /// <param name="point">The point to receive the coordinates.</param>
            /// <returns>Standard api success / failure.</returns>
            [DllImport("user32.dll")]
            internal static extern int GetCaretPos(out POINTAPI point);

            /// <summary>
            /// Sends a message using the LParam as an int.
            /// </summary>
            /// <param name="handle">The handle of the window.</param>
            /// <param name="msg">The message id.</param>
            /// <param name="wParam">The WParam of the message.</param>
            /// <param name="lParam">The LParam of the message.</param>
            /// <returns>The return value of the message.</returns>
            [DllImport("user32.dll", EntryPoint = "SendMessageA", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            internal static extern int SendMessageLong(IntPtr handle, Messages msg, int wParam, int lParam);

            /// <summary>
            /// Sends a message using WParam and LParam as int pointers.
            /// </summary>
            /// <param name="handle">The handle of the window.</param>
            /// <param name="msg">The message id.</param>
            /// <param name="wParam">The WParam of the message.</param>
            /// <param name="lParam">The LParam of the message.</param>
            /// <returns>The return value of the message.</returns>
            [DllImport("user32.dll", EntryPoint = "SendMessageA", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            internal static extern int SendMessageLongRef(IntPtr handle, Messages msg, out int wParam, out int lParam);

            /// <summary>
            /// Sends a message using the LParam as a POINTAPI pointer.
            /// </summary>
            /// <param name="handle">The handle of the window.</param>
            /// <param name="msg">The message id.</param>
            /// <param name="wParam">The WParam of the message.</param>
            /// <param name="point">The referenced point to return the value in.</param>
            /// <returns>The return value of the message.</returns>
            [DllImport("user32.dll", EntryPoint = "SendMessageA", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            internal static extern int SendMessagePoint(IntPtr handle, Messages msg, int wParam, ref POINTAPI point);

            #endregion
        }

        #endregion

        #region Protected Constants

        /// <summary>
        /// The default auto scroll property value.
        /// </summary>
        protected const bool mbdefAutoScroll = true;

        #endregion

        #region Protected Locals

        /// <summary>
        /// The adjusting Y position scrolling factor for get scroll pos bug.
        /// </summary>
        protected double mdScrollPosYfactor = 1.0d;

        /// <summary>
        /// The updating level for formatting updates.
        /// </summary>
        protected int miFormatUpdatingLevel;

        /// <summary>
        /// The previously saved selection starting position.
        /// </summary>
        protected int miSavedOldStart;

        /// <summary>
        /// The previously saved selection length.
        /// </summary>
        protected int miSavedOldLen;

        /// <summary>
        /// The previously saved scroll location.
        /// </summary>
        protected Point mpSavedScrollPoint;

        /// <summary>
        /// True if previouslys saved selection is forward.
        /// </summary>
        protected bool mbSavedOldForwardSelection;

        /// <summary>
        /// True if painting is being suspended.
        /// </summary>
        protected bool mbSuspendPainting;

        /// <summary>
        /// The lock count for window updating.
        /// </summary>
        protected int miLockCount;

        /// <summary>
        /// True if auto scroll is enabled.
        /// </summary>
        protected bool mbAutoScroll = mbdefAutoScroll;

        #endregion

        #region Initialisation

        /// <summary>
        /// Creates a new auto scroll rich text box.
        /// </summary>
        public ctlAutoScrollRichTextBox()
        {
            InitializeComponent();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Returns the total number of lines in the rich edit.
        /// </summary>
        /// <returns>Total number of lines.</returns>
        protected int GetNumberOfLines()
        {
            return GetLineFromCharIndex(TextLength - 1);
        }

        /// <summary>
        /// Gets the scroll position of the text box.
        /// </summary>
        /// <returns>The point denoting the scroll position.</returns>
        protected Point GetScrollPos()
        {
            EnhTextBoxAPIFunctions.POINTAPI lpScrollPoint = new EnhTextBoxAPIFunctions.POINTAPI();
            EnhTextBoxAPIFunctions.SendMessagePoint(this.Handle, EnhTextBoxAPIFunctions.Messages.EM_GETSCROLLPOS, 0, ref lpScrollPoint);
            return new Point(lpScrollPoint.X, lpScrollPoint.Y);
        }

        /// <summary>
        /// Sets the scoll position of the text box.
        /// </summary>
        /// <remarks>This code compensates for the EM_GETSCROLLPOS message limitations.</remarks>
        /// <param name="value">The new scroll position.</param>
        protected void SetScrollPos(Point value)
        {
            // Save the original value
            EnhTextBoxAPIFunctions.POINTAPI lpOriginal = (EnhTextBoxAPIFunctions.POINTAPI)value;
            if (lpOriginal.Y < 0) lpOriginal.Y = 0;
            if (lpOriginal.X < 0) lpOriginal.X = 0;

            // Default factored value to original value with Y Factor compensation
            EnhTextBoxAPIFunctions.POINTAPI lpFactored = (EnhTextBoxAPIFunctions.POINTAPI)value;
            lpFactored.Y = (int)((double)lpOriginal.Y * mdScrollPosYfactor);

            // Current result
            EnhTextBoxAPIFunctions.POINTAPI lpResult = (EnhTextBoxAPIFunctions.POINTAPI)value;

            EnhTextBoxAPIFunctions.SendMessagePoint(this.Handle, EnhTextBoxAPIFunctions.Messages.EM_SETSCROLLPOS, 0, ref lpFactored);
            EnhTextBoxAPIFunctions.SendMessagePoint(this.Handle, EnhTextBoxAPIFunctions.Messages.EM_GETSCROLLPOS, 0, ref lpResult);

            int liLoopCount = 0;
            int liMaxLoop = 100;

            while (lpResult.Y != lpOriginal.Y)
            {
                // Adjust the input.
                if (lpResult.Y > lpOriginal.Y) lpFactored.Y -= (lpResult.Y - lpOriginal.Y) / 2 - 1;
                else
                    if (lpResult.Y < lpOriginal.Y) lpFactored.Y += (lpOriginal.Y - lpResult.Y) / 2 + 1;

                // test the new input.
                EnhTextBoxAPIFunctions.SendMessagePoint(this.Handle, EnhTextBoxAPIFunctions.Messages.EM_SETSCROLLPOS, 0, ref lpFactored);
                EnhTextBoxAPIFunctions.SendMessagePoint(this.Handle, EnhTextBoxAPIFunctions.Messages.EM_GETSCROLLPOS, 0, ref lpResult);

                // save new factor, test for exit.
                liLoopCount++;
                if (liLoopCount >= liMaxLoop || lpResult.Y == lpOriginal.Y)
                {
                    mdScrollPosYfactor = (double)lpFactored.Y / (double)lpOriginal.Y;
                    break;
                }
            }
        }

        /// <summary>
        /// Returns true if the selection is in a forward direction (cursor is after selection)
        /// or false if the selection is in a backward direction (cursor is before selection).
        /// </summary>
        /// <returns>True if selection is forward.</returns>
        protected bool IsSelectionForward()
        {
            EnhTextBoxAPIFunctions.POINTAPI lpCaretPos;

            EnhTextBoxAPIFunctions.GetCaretPos(out lpCaretPos);
            lpCaretPos.X++;
            lpCaretPos.Y++;

            int liCaretChar = GetCharIndexFromPosition(new Point(lpCaretPos.X, lpCaretPos.Y));

            int liStart, liLength;
            GetSel(out liStart, out liLength);

            if (liLength > 0 && liCaretChar <= liStart)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Sets the status of whether an update is in progress.
        /// </summary>
        /// <param name="updating">True if updating is beginning false otherwise.</param>
        protected void SetUpdating(bool updating)
        {
            if (!updating) miFormatUpdatingLevel--;

            if (miFormatUpdatingLevel == 0)
            {
                if (updating)
                {
                    GetSel(out miSavedOldStart, out miSavedOldLen);
                    mpSavedScrollPoint = ScrollPos;
                    mbSavedOldForwardSelection = IsSelectionForward();
                    LockWindow();
                }
                else
                {
                    if (mbSavedOldForwardSelection)
                        SetSel(miSavedOldStart, miSavedOldLen);
                    else
                        SetSel(miSavedOldStart + miSavedOldLen, -miSavedOldLen);

                    ScrollPos = mpSavedScrollPoint;
                    UnlockWindow();
                }
            }

            if (updating) miFormatUpdatingLevel++;
        }

        /// <summary>
        /// Processes all window messages
        /// </summary>
        /// <param name="m">The message received</param>
        protected override void WndProc(ref Message m)
        {
            EnhTextBoxAPIFunctions.Messages lmMsg = (EnhTextBoxAPIFunctions.Messages)m.Msg;
            bool lbProcessMsg = true;

            switch (lmMsg)
            {
                case EnhTextBoxAPIFunctions.Messages.WM_ERASEBKGND:
                    if (mbSuspendPainting) lbProcessMsg = false;
                    break;

                case EnhTextBoxAPIFunctions.Messages.WM_NCPAINT:
                    if (mbSuspendPainting) lbProcessMsg = false;
                    break;

                case EnhTextBoxAPIFunctions.Messages.WM_PAINT:
                    if (mbSuspendPainting) lbProcessMsg = false;
                    break;
            }

            if (lbProcessMsg)
                base.WndProc(ref m);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends text to the end of the text box following the
        /// auto scroll property settings.
        /// </summary>
        /// <param name="text">The text to append to the end of the text box.</param>
        public new void AppendText(string text)
        {
            if (text.Length > 0)
            {
                this.BeginFormattingUpdate();
                base.AppendText(text);
                if (mbAutoScroll) ScrollToEnd(true);
                this.EndFormattingUpdate();
            }
        }

        /// <summary>
        /// Scrolls to the end.
        /// </summary>
        /// <param name="preserveSelection">True to preserve the selection, false to move cursor to start of last line.</param>
        public void ScrollToEnd(bool preserveSelection)
        {
            ScrollToLine(GetNumberOfLines(), preserveSelection);
        }

        /// <summary>
        /// Gets the selection start and length.
        /// </summary>
        /// <param name="start">Start of selection.</param>
        /// <param name="length">Length of selection.</param>
        public void GetSel(out int start, out int length)
        {
            int liEnd = 0;
            EnhTextBoxAPIFunctions.SendMessageLongRef(Handle, EnhTextBoxAPIFunctions.Messages.EM_GETSEL, out start, out liEnd);
            length = liEnd - start;
        }

        /// <summary>
        /// Sets the selection start and length.
        /// </summary>
        /// <param name="start">The start of the selection.</param>
        /// <param name="length">The length of the selection.</param>
        public void SetSel(int start, int length)
        {
            EnhTextBoxAPIFunctions.SendMessageLong(Handle, EnhTextBoxAPIFunctions.Messages.EM_SETSEL, start, start + length);
        }

        /// <summary>
        /// Scrolls to a line.
        /// </summary>
        /// <param name="line">The line to scroll to.</param>
        /// <param name="preserveSelection">True to preserve the selection, false to move cursor to start of line.</param>
        public void ScrollToLine(int line, bool preserveSelection)
        {
            BeginFormattingUpdate();

            int liLines = GetNumberOfLines();
            if (line > liLines) line = liLines;
            if (line < 0) line = 0;
            int liPos = GetFirstCharIndexFromLine(line);

            int liStart = 0, liLength = 0;
            if (preserveSelection) GetSel(out liStart, out liLength);

            SetSel(liPos, 0);
            ScrollToCaret();
            mpSavedScrollPoint = ScrollPos;

            EndFormattingUpdate();
        }

        /// <summary>
        /// Locks the window so that no redrawing occurs.
        /// </summary>
        public void LockWindow()
        {
            if (miLockCount == 0)
            {
                mbSuspendPainting = true;
                EnhTextBoxAPIFunctions.SendMessageLong(Handle, EnhTextBoxAPIFunctions.Messages.WM_SETREDRAW, 0, 0);
            };

            miLockCount++;
        }

        /// <summary>
        /// Unlocks and invalidates the window to resume redrawing.
        /// </summary>
        public void UnlockWindow()
        {
            miLockCount--;

            if (miLockCount == 0)
            {
                mbSuspendPainting = false;
                EnhTextBoxAPIFunctions.SendMessageLong(Handle, EnhTextBoxAPIFunctions.Messages.WM_SETREDRAW, 1, 0);
                Invalidate();
            }

            if (miLockCount < 0) miLockCount = 0;
        }

        /// <summary>
        /// Begins udpating of the formatting.
        /// </summary>
        public void BeginFormattingUpdate()
        {
            SetUpdating(true);
        }

        /// <summary>
        /// Ends updating of the formatting.
        /// </summary>
        public void EndFormattingUpdate()
        {
            SetUpdating(false);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets whether to Auto Scroll the text box when
        /// text is appended.
        /// </summary>
        [DefaultValue(mbdefAutoScroll)]
        public bool AutoScroll
        {
            get
            {
                return mbAutoScroll;
            }
            set
            {
                mbAutoScroll = value;
            }
        }

        /// <summary>
        /// Gets or sets the scroll position of the text box.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Bindable(false)]
        public Point ScrollPos
        {
            get
            {
                return GetScrollPos();
            }
            set
            {
                SetScrollPos(value);
            }
        }

        #endregion
    }
}
