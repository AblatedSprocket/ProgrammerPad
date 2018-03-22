using ProgrammerPad.Utilities;
using System;
using System.Windows;
using System.Windows.Media;

namespace ProgrammerPad.Editor
{
    public class Format : ObservableObject
    {
        private FontStyle _style;
        public FontStyle Style
        {
            get { return _style; }
            set
            {
                OnPropertyChanged(ref _style, value);
                IsItalicized = Style == FontStyles.Italic ? true : false;
            }
        }
        private bool _isItalicized;
        public bool IsItalicized
        {
            get { return _isItalicized; }
            set { OnPropertyChanged(ref _isItalicized, value); }
        }
        private FontWeight _weight;
        public FontWeight Weight
        {
            get { return _weight; }
            set
            {
                OnPropertyChanged(ref _weight, value);
                IsBold = Weight == FontWeights.Bold ? true : false;
            }
        }
        private bool _isBold;
        public bool IsBold
        {
            get { return _isBold; }
            set { OnPropertyChanged(ref _isBold, value); }
        }
        private FontFamily _family;
        public FontFamily Family
        {
            get { return _family; }
            set { OnPropertyChanged(ref _family, value); }
        }
        private CustomColor _color;
        public CustomColor Color
        {
            get { return _color; }
            set { OnPropertyChanged(ref _color, value); }
        }
        private TextWrapping _wrap;
        public TextWrapping Wrap
        {
            get { return _wrap; }
            set
            {
                OnPropertyChanged(ref _wrap, value);
                IsWrapped = Wrap == TextWrapping.Wrap ? true : false;
            }
        }
        private bool _isWrapped;
        public bool IsWrapped
        {
            get { return _isWrapped; }
            set { OnPropertyChanged(ref _isWrapped, value); }
        }
        private double _size;
        public double Size
        {
            get { return _size; }
            set { OnPropertyChanged(ref _size, value); }
        }
        public RelayCommand<string> UpdateColorCommand { get; }
        public Format()
        {
            Color = new CustomColor();
            UpdateColorCommand = new RelayCommand<string>(UpdateRGBFromHex);
        }

        private void UpdateRGBFromHex(string parameter)
        {
            if (parameter != null)
            {
                Color.GetCustomColorFromHex(parameter);
            }
            return;
        }
    }
}
