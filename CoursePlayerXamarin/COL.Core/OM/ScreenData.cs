using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COL.Core
{
    public class ScreenData
    {
        private IDictionary<int, byte[]> _images;  //<position of the cell from 0 to 63, image data of the cell>

        public ScreenData()
        {
            _images = new Dictionary<int, byte[]>();
        }

        public IDictionary<int, byte[]> Images
        {
            get { return _images; }
            set { _images = value; }
        }
    }
}
