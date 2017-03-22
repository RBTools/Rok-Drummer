namespace RokDrummer.StageKit
{
    /// <summary>
    /// Provides methods for manipulating the Rock Band Stage Kit
    /// </summary>
    public class StageKitController
    {
        private const short BlueColor = unchecked(0x2000);
        private const short GreenColor = unchecked(0x4000);
        private const short YellowColor = unchecked(0x6000);
        private const short RedColor = unchecked((short)0x8000);
        private const short AllOff = unchecked((short)0xFFFF);
        private const short StrobeOff = 0x700;
        private const short FogOn = 0x100;
        private const short FogOff = 0x200;
        private readonly int _controller;
        public bool StrobeIsOn;
        public bool FoggerIsOn;

        /// <summary>
        /// Creates an instance of the stage kit controller.
        /// </summary>
        /// <param name="controller">A value from 1 to 4 representing the index of the controller which the 
        /// Rock Band Stage Kit is connected to. This is indicated by which light is illuminated on the controller's
        /// big X button.</param>
        public StageKitController(int controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Turns on or off LEDs on the LED Display based upon the values in the <see cref="LedDisplay"/> object.
        /// </summary>
        /// <param name="display">The LEDs to turn on or off.</param>
        public void DisplayLeds(LedDisplay display)
        {
            SetVibration(display.RedLedArray, RedColor);
            SetVibration(display.GreenLedArray, GreenColor);
            SetVibration(display.BlueLedArray, BlueColor);
            SetVibration(display.YellowLedArray, YellowColor);
        }

        public void DisplayRedAll(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.RedLedArray.Led1) { display.RedLedArray.Led1 = ledState; }
            if (ledState != display.RedLedArray.Led2) { display.RedLedArray.Led2 = ledState; }
            if (ledState != display.RedLedArray.Led3) { display.RedLedArray.Led3 = ledState; }
            if (ledState != display.RedLedArray.Led4) { display.RedLedArray.Led4 = ledState; }
            if (ledState != display.RedLedArray.Led5) { display.RedLedArray.Led5 = ledState; }
            if (ledState != display.RedLedArray.Led6) { display.RedLedArray.Led6 = ledState; }
            if (ledState != display.RedLedArray.Led7) { display.RedLedArray.Led7 = ledState; }
            if (ledState != display.RedLedArray.Led8) { display.RedLedArray.Led8 = ledState; }
            SetVibration(display.RedLedArray, RedColor);
        }

        public void DisplayBlueAll(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.BlueLedArray.Led1) { display.BlueLedArray.Led1 = ledState; }
            if (ledState != display.BlueLedArray.Led2) { display.BlueLedArray.Led2 = ledState; }
            if (ledState != display.BlueLedArray.Led3) { display.BlueLedArray.Led3 = ledState; }
            if (ledState != display.BlueLedArray.Led4) { display.BlueLedArray.Led4 = ledState; }
            if (ledState != display.BlueLedArray.Led5) { display.BlueLedArray.Led5 = ledState; }
            if (ledState != display.BlueLedArray.Led6) { display.BlueLedArray.Led6 = ledState; }
            if (ledState != display.BlueLedArray.Led7) { display.BlueLedArray.Led7 = ledState; }
            if (ledState != display.BlueLedArray.Led8) { display.BlueLedArray.Led8 = ledState; }
            SetVibration(display.BlueLedArray, BlueColor);
        }

        public void DisplayGreenAll(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.GreenLedArray.Led1) { display.GreenLedArray.Led1 = ledState; }
            if (ledState != display.GreenLedArray.Led2) { display.GreenLedArray.Led2 = ledState; }
            if (ledState != display.GreenLedArray.Led3) { display.GreenLedArray.Led3 = ledState; }
            if (ledState != display.GreenLedArray.Led4) { display.GreenLedArray.Led4 = ledState; }
            if (ledState != display.GreenLedArray.Led5) { display.GreenLedArray.Led5 = ledState; }
            if (ledState != display.GreenLedArray.Led6) { display.GreenLedArray.Led6 = ledState; }
            if (ledState != display.GreenLedArray.Led7) { display.GreenLedArray.Led7 = ledState; }
            if (ledState != display.GreenLedArray.Led8) { display.GreenLedArray.Led8 = ledState; }
            SetVibration(display.GreenLedArray, GreenColor);
        }

        public void DisplayYellowAll(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.YellowLedArray.Led1) { display.YellowLedArray.Led1 = ledState; }
            if (ledState != display.YellowLedArray.Led2) { display.YellowLedArray.Led2 = ledState; }
            if (ledState != display.YellowLedArray.Led3) { display.YellowLedArray.Led3 = ledState; }
            if (ledState != display.YellowLedArray.Led4) { display.YellowLedArray.Led4 = ledState; }
            if (ledState != display.YellowLedArray.Led5) { display.YellowLedArray.Led5 = ledState; }
            if (ledState != display.YellowLedArray.Led6) { display.YellowLedArray.Led6 = ledState; }
            if (ledState != display.YellowLedArray.Led7) { display.YellowLedArray.Led7 = ledState; }
            if (ledState != display.YellowLedArray.Led8) { display.YellowLedArray.Led8 = ledState; }
            SetVibration(display.YellowLedArray, YellowColor);
        }

        public void DisplayRedLed1(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.RedLedArray.Led1) { display.RedLedArray.Led1 = ledState; }
            SetVibration(display.RedLedArray, RedColor);
        }

        public void DisplayGreenLed1(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.GreenLedArray.Led1) { display.GreenLedArray.Led1 = ledState; }
            SetVibration(display.GreenLedArray, GreenColor);
        }

        public void DisplayBlueLed1(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.BlueLedArray.Led1) { display.BlueLedArray.Led1 = ledState; }
            SetVibration(display.BlueLedArray, BlueColor);
        }

        public void DisplayYellowLed1(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.YellowLedArray.Led1) { display.YellowLedArray.Led1 = ledState; }
            SetVibration(display.YellowLedArray, YellowColor);
        }

        public void DisplayRedLed2(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.RedLedArray.Led2) { display.RedLedArray.Led2 = ledState; }
            SetVibration(display.RedLedArray, RedColor);
        }

        public void DisplayGreenLed2(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.GreenLedArray.Led2) { display.GreenLedArray.Led2 = ledState; }
            SetVibration(display.GreenLedArray, GreenColor);
        }

        public void DisplayBlueLed2(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.BlueLedArray.Led2) { display.BlueLedArray.Led2 = ledState; }
            SetVibration(display.BlueLedArray, BlueColor);
        }

        public void DisplayYellowLed2(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.YellowLedArray.Led2) { display.YellowLedArray.Led2 = ledState; }
            SetVibration(display.YellowLedArray, YellowColor);
        }

        public void DisplayRedLed3(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.RedLedArray.Led3) { display.RedLedArray.Led3 = ledState; }
            SetVibration(display.RedLedArray, RedColor);
        }

        public void DisplayGreenLed3(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.GreenLedArray.Led3) { display.GreenLedArray.Led3 = ledState; }
            SetVibration(display.GreenLedArray, GreenColor);
        }

        public void DisplayBlueLed3(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.BlueLedArray.Led3) { display.BlueLedArray.Led3 = ledState; }
            SetVibration(display.BlueLedArray, BlueColor);
        }

        public void DisplayYellowLed3(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.YellowLedArray.Led3) { display.YellowLedArray.Led3 = ledState; }
            SetVibration(display.YellowLedArray, YellowColor);
        }

        public void DisplayRedLed4(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.RedLedArray.Led4) { display.RedLedArray.Led4 = ledState; }
            SetVibration(display.RedLedArray, RedColor);
        }

        public void DisplayGreenLed4(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.GreenLedArray.Led4) { display.GreenLedArray.Led4 = ledState; }
            SetVibration(display.GreenLedArray, GreenColor);
        }

        public void DisplayBlueLed4(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.BlueLedArray.Led4) { display.BlueLedArray.Led4 = ledState; }
            SetVibration(display.BlueLedArray, BlueColor);
        }

        public void DisplayYellowLed4(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.YellowLedArray.Led4) { display.YellowLedArray.Led4 = ledState; }
            SetVibration(display.YellowLedArray, YellowColor);
        }

        public void DisplayRedLed5(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.RedLedArray.Led5) { display.RedLedArray.Led5 = ledState; }
            SetVibration(display.RedLedArray, RedColor);
        }

        public void DisplayGreenLed5(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.GreenLedArray.Led5) { display.GreenLedArray.Led5 = ledState; }
            SetVibration(display.GreenLedArray, GreenColor);
        }

        public void DisplayBlueLed5(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.BlueLedArray.Led5) { display.BlueLedArray.Led5 = ledState; }
            SetVibration(display.BlueLedArray, BlueColor);
        }

        public void DisplayYellowLed5(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.YellowLedArray.Led5) { display.YellowLedArray.Led5 = ledState; }
            SetVibration(display.YellowLedArray, YellowColor);
        }

        public void DisplayRedLed6(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.RedLedArray.Led6) { display.RedLedArray.Led6 = ledState; }
            SetVibration(display.RedLedArray, RedColor);
        }

        public void DisplayGreenLed6(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.GreenLedArray.Led6) { display.GreenLedArray.Led6 = ledState; }
            SetVibration(display.GreenLedArray, GreenColor);
        }

        public void DisplayBlueLed6(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.BlueLedArray.Led6) { display.BlueLedArray.Led6 = ledState; }
            SetVibration(display.BlueLedArray, BlueColor);
        }

        public void DisplayYellowLed6(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.YellowLedArray.Led6) { display.YellowLedArray.Led6 = ledState; }
            SetVibration(display.YellowLedArray, YellowColor);
        }

        public void DisplayRedLed7(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.RedLedArray.Led7) { display.RedLedArray.Led7 = ledState; }
            SetVibration(display.RedLedArray, RedColor);
        }

        public void DisplayGreenLed7(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.GreenLedArray.Led7) { display.GreenLedArray.Led7 = ledState; }
            SetVibration(display.GreenLedArray, GreenColor);
        }

        public void DisplayBlueLed7(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.BlueLedArray.Led7) { display.BlueLedArray.Led7 = ledState; }
            SetVibration(display.BlueLedArray, BlueColor);
        }

        public void DisplayYellowLed7(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.YellowLedArray.Led7) { display.YellowLedArray.Led7 = ledState; }
            SetVibration(display.YellowLedArray, YellowColor);
        }

        public void DisplayRedLed8(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.RedLedArray.Led8) { display.RedLedArray.Led8 = ledState; }
            SetVibration(display.RedLedArray, RedColor);
        }

        public void DisplayGreenLed8(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.GreenLedArray.Led8) { display.GreenLedArray.Led8 = ledState; }
            SetVibration(display.GreenLedArray, GreenColor);
        }

        public void DisplayBlueLed8(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.BlueLedArray.Led8) { display.BlueLedArray.Led8 = ledState; }
            SetVibration(display.BlueLedArray, BlueColor);
        }

        public void DisplayYellowLed8(ref LedDisplay display, bool ledState)
        {
            if (ledState != display.YellowLedArray.Led8) { display.YellowLedArray.Led8 = ledState; }
            SetVibration(display.YellowLedArray, YellowColor);
        }

        /// <summary>
        /// Turns off the LED display, fog machine, and strobe light.
        /// </summary>
        public void TurnAllOff()
        {
            SetVibration(AllOff);
            FoggerIsOn = false;
            StrobeIsOn = false;
        }

        /// <summary>
        /// Turns on the strobe light at the speed specified.
        /// </summary>
        /// <param name="speed">The speed at which to have the strobe light flash.</param>
        public void TurnStrobeOn(StrobeSpeed speed)
        {
            SetVibration((short)speed);
            StrobeIsOn = true;
        }

        /// <summary>
        /// Turns off the strobe light.
        /// </summary>
        public void TurnStrobeOff()
        {
            SetVibration(StrobeOff);
            StrobeIsOn = false;
        }

        /// <summary>
        /// Turns the fog machine on.
        /// </summary>
        public void TurnFogOn()
        {
            SetVibration(FogOn);
            FoggerIsOn = true;
        }

        /// <summary>
        /// Turns the fog machine off.
        /// </summary>
        public void TurnFogOff()
        {
            SetVibration(FogOff);
            FoggerIsOn = false;
        }

        private void SetVibration(short left, short right)
        {
            // Sets the vibration on the controller using interop method calls. The XNA framework is not needed.
            var index = (PlayerIndex)(_controller - 1);
            GamePad.SetVibration(index, left, right);
            System.Threading.Thread.Sleep(10);
        }

        private void SetVibration(short right)
        {
            SetVibration(0x0, right);
        }
    }

    /// <summary>
    /// Represents the speed of the strobe light.
    /// </summary>
    public enum StrobeSpeed : short
    {
        /// <summary>
        /// Represents the slowest speed of the strobe light.
        /// </summary>
        Slow = 0x300,
        /// <summary>
        /// Represents the medium speed of the strobe light.
        /// </summary>
        Medium = 0x400,
        /// <summary>
        /// Represents the fast speed of the strobe light.
        /// </summary>
        Faster = 0x500,
        /// <summary>
        /// Represents the fastest speed of the strobe light.
        /// </summary>
        Fastest = 0x600,
    }
}
