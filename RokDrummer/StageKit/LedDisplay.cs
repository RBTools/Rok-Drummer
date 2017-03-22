namespace RokDrummer.StageKit
{
    /// <summary>
    /// Represents the LED Display in the rock band stage kit.
    /// </summary>
    public class LedDisplay : System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Creates a new instance of the <see cref="LedDisplay"/> class.
        /// </summary>
        public LedDisplay()
        {
            RedLedArray = new LedColor();
            YellowLedArray = new LedColor();
            GreenLedArray = new LedColor();
            BlueLedArray = new LedColor();
            BindEvents();
        }

        /// <summary>
        /// Gets the on or off positions of the red LEDs on the LED Display.
        /// </summary>
        public LedColor RedLedArray { get; private set; }

        /// <summary>
        /// Gets the on or off positions of the blue LEDs on the LED Display.
        /// </summary>
        public LedColor BlueLedArray { get; private set; }

        /// <summary>
        /// Gets the on or off positions of the green LEDs on the LED Display.
        /// </summary>
        public LedColor GreenLedArray { get; private set; }

        /// <summary>
        /// Gets the on or off positions of the yellow LEDs on the LED Display.
        /// </summary>
        public LedColor YellowLedArray { get; private set; }
        
        /// <summary>
        /// Explicitly converts an instance of the LED Display to an integer. This is useful for creating
        /// a snapshot of the display to persist in memory or long term storage.
        /// </summary>
        /// <param name="display">An instance of an <see cref="LedDisplay"/> object who's values are to be
        /// converted to an integer.</param>
        /// <returns>An integer representing the values of the LED Display.</returns>
        public static explicit operator int(LedDisplay display)
        {
            return
                (display.RedLedArray.Value << 24) | 
                (display.GreenLedArray.Value << 16) | 
                (display.YellowLedArray.Value << 8) | 
                display.BlueLedArray.Value;
        }

        /// <summary>
        /// Explicitly converts an integer into an <see cref="LedDisplay"/> object. This is useful for
        /// recreating a snapshop of the LED Display from memory or long term storage.
        /// </summary>
        /// <param name="key">The integer which contains the values to recreate an <see cref="LedDisplay"/> object.</param>
        /// <returns>An instance of the <see cref="LedDisplay"/> object which is represented by the integer.</returns>
        public static explicit operator LedDisplay(int key)
        {
            var display = new LedDisplay
            {
                RedLedArray = new LedColor((byte) (key >> 24)),
                GreenLedArray = new LedColor((byte) (key >> 16)),
                YellowLedArray = new LedColor((byte) (key >> 8)),
                BlueLedArray = new LedColor((byte) key)
            };
            display.BindEvents();
            return display;
        }

        /// <summary>
        /// Fired whenever any of the LEDs turn on or off.
        /// </summary>
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        private void LedChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(sender, e);
            }
        }

        private void BindEvents()
        {
            RedLedArray.PropertyChanged += LedChanged;
            YellowLedArray.PropertyChanged += LedChanged;
            GreenLedArray.PropertyChanged += LedChanged;
            BlueLedArray.PropertyChanged += LedChanged;
        }
    }
}
