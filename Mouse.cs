using Microsoft.Win32.SafeHandles;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ConsoleMouse
{
    public class Mouse
    {
        private static NativeMethods.ConsoleHandle handle = NativeMethods.GetStdHandle(NativeMethods.STD_INPUT_HANDLE);
        private static NativeMethods.INPUT_RECORD record;
        private static uint recordLen;

        public static int X, Y;

        public static bool LEFT;
        public static bool RIGHT;

        private static bool leftLock = false, currLeft = false;
        private static bool rightLock = false, currRight = false;

        public static void Initialize()
        {
            int mode = 0;
            if (!(NativeMethods.GetConsoleMode(handle, ref mode))) { throw new Win32Exception(); }

            mode |= NativeMethods.ENABLE_MOUSE_INPUT;
            mode &= ~NativeMethods.ENABLE_QUICK_EDIT_MODE;
            mode |= NativeMethods.ENABLE_EXTENDED_FLAGS;

            if (!(NativeMethods.SetConsoleMode(handle, mode))) { throw new Win32Exception(); }

            record = new NativeMethods.INPUT_RECORD();
            recordLen = 0;
        }

        public static void Update()
        {
            uint numEvents = 0;
			
            // Check if input is available
            if (NativeMethods.PeekConsoleInput(handle, ref record, 1, ref numEvents) && numEvents > 0)
            {
                if (!(NativeMethods.ReadConsoleInput(handle, ref record, 1, ref recordLen))) { throw new Win32Exception(); }

                if (record.EventType == NativeMethods.MOUSE_EVENT)
                {
                    X = record.MouseEvent.dwMousePosition.X;
                    Y = record.MouseEvent.dwMousePosition.Y;

                    currLeft = (record.MouseEvent.dwButtonState == 1);
                    currRight = (record.MouseEvent.dwButtonState == 2);

                }
            }

            LEFT = false;
            if (!LEFT && currLeft && !leftLock)
            {
	            LEFT = true;
	            leftLock = true;
            } else if (!currLeft && leftLock)
            {
	            leftLock = false;
            }
            
            RIGHT = false;
            if (!RIGHT && currRight && !rightLock)
            {
	            RIGHT = true;
	            rightLock = true;
            } else if (!currRight && rightLock)
            {
	            rightLock = false;
            }

        }

        private class NativeMethods
        {
            public const Int32 STD_INPUT_HANDLE = -10;

            public const Int32 ENABLE_MOUSE_INPUT = 0x0010;
            public const Int32 ENABLE_QUICK_EDIT_MODE = 0x0040;
            public const Int32 ENABLE_EXTENDED_FLAGS = 0x0080;

            public const Int32 MOUSE_EVENT = 2;

            [StructLayout(LayoutKind.Explicit)]
            public struct INPUT_RECORD
            {
                [FieldOffset(0)]
                public Int16 EventType;
                [FieldOffset(4)]
                public MOUSE_EVENT_RECORD MouseEvent;
            }

            [DebuggerDisplay("{dwMousePosition.X}, {dwMousePosition.Y}")]
            public struct MOUSE_EVENT_RECORD
            {
                public COORD dwMousePosition;
                public Int32 dwButtonState;
                public Int32 dwControlKeyState;
                public Int32 dwEventFlags;
            }

            [DebuggerDisplay("{X}, {Y}")]
            public struct COORD
            {
                public UInt16 X;
                public UInt16 Y;
            }

            public class ConsoleHandle : SafeHandleMinusOneIsInvalid
            {
                public ConsoleHandle() : base(false) { }

                protected override bool ReleaseHandle()
                {
                    return true; // Releasing console handle is not our business
                }
            }

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern Boolean GetConsoleMode(ConsoleHandle hConsoleHandle, ref Int32 lpMode);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern ConsoleHandle GetStdHandle(Int32 nStdHandle);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern Boolean ReadConsoleInput(ConsoleHandle hConsoleInput, ref INPUT_RECORD lpBuffer, UInt32 nLength, ref UInt32 lpNumberOfEventsRead);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern Boolean SetConsoleMode(ConsoleHandle hConsoleHandle, Int32 dwMode);

            // PeekConsoleInput - checks if input is available, without removing the event from the queue
            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern Boolean PeekConsoleInput(ConsoleHandle hConsoleInput, ref INPUT_RECORD lpBuffer, UInt32 nLength, ref UInt32 lpNumberOfEventsRead);
        }
    }
}
