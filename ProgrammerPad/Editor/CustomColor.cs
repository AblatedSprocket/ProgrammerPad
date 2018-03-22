using ProgrammerPad.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProgrammerPad.Editor
{
    public class CustomColor : ObservableObject
    {
        private static List<char> base16 = new List<char>
        {
            '0',
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9',
            'a',
            'b',
            'c',
            'd',
            'e',
            'f'
        };
        private int _red;
        public int Red
        {
            get { return _red; }
            set
            {
                OnPropertyChanged(ref _red, value);
                AdjustHexValue();
            }
        }
        private int _green;
        public int Green
        {
            get { return _green; }
            set
            {
                OnPropertyChanged(ref _green, value);
                AdjustHexValue();
            }
        }
        private int _blue;
        public int Blue
        {
            get { return _blue; }
            set
            {
                OnPropertyChanged(ref _blue, value);
                AdjustHexValue();
            }
        }
        private string _hex;
        public string Hex
        {
            get { return _hex; }
            set
            {
                OnPropertyChanged(ref _hex, value);
            }
        }
        private void AdjustHexValue()
        {
            Hex = string.Concat('#', base16[_red / 16], base16[_red % 16], base16[_green / 16], base16[_green % 16], base16[_blue / 16], base16[_blue % 16]);
        }
        public CustomColor()
        {
            Hex = "#000000";
        }
        public CustomColor GetCustomColorFromHex(string hex)
        {
            Match match;
            if ((match = Regex.Match(hex, "^#(?<red>[0-9A-F]{2}?)(?<green>[0-9A-F]{2}?)(?<blue>[0-9A-F]{2})$", RegexOptions.IgnoreCase)).Success)
            {
                string red = match.Result("${red}").ToLower();
                string green = match.Result("${green}").ToLower();
                string blue = match.Result("${blue}").ToLower();
                Red = base16.IndexOf(red[0]) * 16 + base16.IndexOf(red[1]);
                Green = base16.IndexOf(green[0]) * 16 + base16.IndexOf(green[1]);
                Blue = base16.IndexOf(blue[0]) * 16 + base16.IndexOf(blue[1]);
            }
            return null;
        }
    }
}