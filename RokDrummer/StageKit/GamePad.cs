using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

#if !XBOX
namespace RokDrummer.StageKit
{
    internal static class GamePad
    {
        private static readonly bool[] _disconnected;
        private static readonly long[] _lastReadTime;

        static GamePad()
        {
            _disconnected = new bool[4];
            _lastReadTime = new long[4];
        }

        public static bool SetVibration(PlayerIndex playerIndex, short leftMotor, short rightMotor)
        {
            XINPUT_VIBRATION xinput_vibration;
            xinput_vibration.LeftMotorSpeed = leftMotor;
            xinput_vibration.RightMotorSpeed = rightMotor;
            ErrorCodes success;
            if (ThrottleDisconnectedRetries(playerIndex))
            {
                success = ErrorCodes.NotConnected;
            }
            else
            {
                success = UnsafeMethods.SetState(playerIndex, ref xinput_vibration);
                ResetThrottleState(playerIndex, success);
            }
            if (success == ErrorCodes.Success) return true;
            if (((success != ErrorCodes.Success) && (success != ErrorCodes.NotConnected)) && (success != ErrorCodes.Busy))
            {
                throw new InvalidOperationException("Invalid Controller");
            }
            return false;
        }

        private static void ResetThrottleState(PlayerIndex playerIndex, ErrorCodes result)
        {
            if ((playerIndex < PlayerIndex.One) || (playerIndex > PlayerIndex.Four)) return;
            if (result == ErrorCodes.NotConnected)
            {
                _disconnected[(int)playerIndex] = true;
                _lastReadTime[(int)playerIndex] = Stopwatch.GetTimestamp();
            }
            else
            {
                _disconnected[(int)playerIndex] = false;
            }
        }

        private static bool ThrottleDisconnectedRetries(PlayerIndex playerIndex)
        {
            if (((playerIndex < PlayerIndex.One) || (playerIndex > PlayerIndex.Four)) ||
                !_disconnected[(int) playerIndex]) return false;
            var timestamp = Stopwatch.GetTimestamp();
            for (var i = 0; i < 4; i++)
            {
                if (!_disconnected[i]) continue;
                var num3 = timestamp - _lastReadTime[i];
                var frequency = Stopwatch.Frequency;
                if (i != (int)playerIndex)
                {
                    frequency /= 4L;
                }
                if ((num3 >= 0L) && (num3 <= frequency)) return true;
            }
            return false;
        }
    }

    internal enum ErrorCodes : uint
    {
        Busy = 170,
        NotConnected = 0x48f,
        Success = 0
    }

    internal static class UnsafeMethods
    {
        [DllImport("xinput1_3.dll", EntryPoint = "XInputSetState")]
        public static extern ErrorCodes SetState(PlayerIndex playerIndex, ref XINPUT_VIBRATION pVibration);
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct XINPUT_VIBRATION
    {
        public short LeftMotorSpeed;
        public short RightMotorSpeed;
    }

    internal enum PlayerIndex
    {
        One,
        Two,
        Three,
        Four
    }
}
#endif

