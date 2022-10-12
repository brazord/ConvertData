using System;
using System.Collections.Generic;
using System.Text;

namespace ConvertData
{
    class YoloData
    {
        private const string MASK = "#.######";
        public int LabelID { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        private decimal _X_CENTER_NORM { get; set; }
        private decimal _Y_CENTER_NORM { get; set; }
        private decimal _WIDTH_NORM { get; set; }
        private decimal _HEIGHT_NORM { get; set; }

        public YoloData(string[] data, int width, int height)
        {
            try
            {
                Width = width;
                Height = height;
                decimal x1 = decimal.Parse(data[0]);
                decimal y1 = decimal.Parse(data[1]);
                decimal x2 = decimal.Parse(data[2]);
                decimal y2 = decimal.Parse(data[3]);
                int type = (int.Parse(data[4]) - 1); // class_id should be [from 0 to 5]

                LabelID = type;
                _X_CENTER_NORM = Math.Round(Math.Abs(x1 + ((x2 - x1) / 2)) / Width, 6);
                _Y_CENTER_NORM = Math.Round(Math.Abs(y1 + ((y2 - y1) / 2)) / Height, 6);
                _WIDTH_NORM = Math.Round((x2 - x1) / Width, 6);
                _HEIGHT_NORM = Math.Round((y2 - y1) / Height, 6);
            }
            catch (Exception ex)
            {
                throw new Exception("Error to create YoloData");
            }
        }

        public override string ToString()
        {
            return $"{LabelID} {_X_CENTER_NORM.FormatData()} {_Y_CENTER_NORM.FormatData()} {_WIDTH_NORM.FormatData()} {_HEIGHT_NORM.FormatData()}";
        }

        
    }
}
