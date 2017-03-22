namespace RokDrummer.StageKit
{
    /// <summary>
    /// Represents an array of LEDs of the same color on the LED Display. The LED Display contains 8
    /// of each color, each represented by a bit within this class.
    /// </summary>
    public class LedColor : System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// When overridden by a class, creates a new instance of the <see cref="LedColor"/> class.
        /// </summary>
        protected internal LedColor() { }

        /// <summary>
        /// When overridden by a class, creates a new instance of the <see cref="LedColor"/> class and sets it's value.
        /// </summary>
        /// <param name="value">The value of the LED array.</param>
        protected internal LedColor(byte value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets or sets the value of the first LED of this color. True for on, False for off.
        /// This is usually at the 12 o'clock position of the LED Display.
        /// </summary>
        public virtual bool Led1
        {
            get { return (Value & 0x1) == 0x1; }
            set
            {
                SetValue(value, 0);
                OnPropertyChanged("Led1");
            }
        }

        /// <summary>
        /// Gets or sets the value of the second LED of this color. True for on, False for off.
        /// This is usually half way between the 12 and 3 o'clock positions of the LED Display.
        /// </summary>
        public virtual bool Led2
        {
            get { return (Value & 0x2) == 0x2; }
            set
            {
                SetValue(value, 1);
                OnPropertyChanged("Led2");
            }
        }

        /// <summary>
        /// Gets or sets the value of the third LED of this color. True for on, False for off.
        /// This is usually at the 3 o'clock position of the LED Display.
        /// </summary>
        public virtual bool Led3
        {
            get { return (Value & 0x4) == 0x4; }
            set
            {
                SetValue(value, 2);
                OnPropertyChanged("Led3");
            }
        }

        /// <summary>
        /// Gets or sets the value of the fourth LED of this color. True for on, False for off.
        /// This is usually half way between the 3 and 6 o'clock positions of the LED Display.
        /// </summary>
        public virtual bool Led4
        {
            get { return (Value & 0x8) == 0x8; }
            set
            {
                SetValue(value, 3);
                OnPropertyChanged("Led4");
            }
        }

        /// <summary>
        /// Gets or sets the value of the fifth LED of this color. True for on, False for off.
        /// This is usually at the 6 o'clock position of the LED Display.
        /// </summary>
        public virtual bool Led5
        {
            get { return (Value & 0x10) == 0x10; }
            set
            {
                SetValue(value, 4);
                OnPropertyChanged("Led5");
            }
        }

        /// <summary>
        /// Gets or sets the value of the sixth LED of this color. True for on, False for off.
        /// This is usually half way between the 6 and 9 o'clock positions of the LED Display.
        /// </summary>
        public virtual bool Led6
        {
            get { return (Value & 0x20) == 0x20; }
            set
            {
                SetValue(value, 5);
                OnPropertyChanged("Led6");
            }
        }

        /// <summary>
        /// Gets or sets the value of the seventh LED of this color. True for on, False for off.
        /// This is usually at the 9 o'clock position of the LED Display.
        /// </summary>
        public virtual bool Led7
        {
            get { return (Value & 0x40) == 0x40; }
            set
            {
                SetValue(value, 6);
                OnPropertyChanged("Led7");
            }
        }

        /// <summary>
        /// Gets or sets the value of the eighth LED of this color. True for on, False for off.
        /// This is usually half way between the 9 and 12 o'clock position of the LED Display.
        /// </summary>
        public virtual bool Led8
        {
            get { return (Value & 0x80) == 0x80; }
            set
            {
                SetValue(value, 7);
                OnPropertyChanged("Led8");
            }
        }

        /// <summary>
        /// Implicitly converts this object into a short, for use with the SetVibration method for the controller.
        /// </summary>
        /// <param name="value">An instance of the <see cref="LedColor"/> class to convert.</param>
        /// <returns>A short representing the value of the <see cref="LedColor"/> class.</returns>
        public static implicit operator short(LedColor value)
        {
            return (short)(value.Value << 8);
        }

        /// <summary>
        /// Fired whenever any of the LEDs turn on or off.
        /// </summary>
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// When overriden by a class, gets or sets the value of this object.
        /// </summary>
        protected internal byte Value { get; set; }

        /// <summary>
        /// When overridden by a class, executes the PropertyChanged event whenever an LED's value is changed.
        /// </summary>
        /// <param name="name">The name of the property which represents the LED.</param>
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(name));
            }
        }

        private void SetValue(bool value, int shift)
        {
            Value = (byte)((value) ? (Value | (1 << shift)) : (Value ^ (1 << shift)));
        }
    }
}
