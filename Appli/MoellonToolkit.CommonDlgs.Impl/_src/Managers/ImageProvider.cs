using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Interop;
using System.Windows;
using System.IO;
using System.Drawing.Imaging;

namespace MoellonToolkit.CommonDlgs.Impl
{
    public enum ImageCode
    {
        DlgQuestion,
        DlgInformation,
        DlgError,
        DlgWarning,

        BtnOk,
        BtnYes,
        BtnNo,   
        BtnCancel,

    }

    /// <summary>
    /// Mapping code - image.
    /// </summary>
    public class MapCodeImg
    {
        /// <summary>
        /// The code identifyng the image.
        /// </summary>
        public ImageCode ImageCode { get; set; }

        //public string Uri { get; set; }

        /// <summary>
        /// The image content
        /// </summary>
        //public BitmapImage BmpImage { get; set; }

        public BitmapSource BitmapSource { get; set; }

    }

    /// <summary>
    /// An images provider, by code.
    /// </summary>
    public class ImageProvider
    {
        // list of code - image
        List<MapCodeImg> _listMapCodeImg = new List<MapCodeImg>();

        /// <summary>
        /// Constructor.
        /// </summary>
        public ImageProvider()
        {
            LoadImages();
        }

        /// <summary>
        /// Get an image by the code.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        //public BitmapImage GetImg(ImageCode code)
        //{
        //    MapCodeImg mapCodeImg= _listMapCodeImg.Find(m => m.ImageCode == code);
        //    if (mapCodeImg == null)
        //        return null;
        //    return mapCodeImg.BmpImage;
        //}

        /// <summary>
        /// Get an image by the code.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public BitmapSource GetImg(ImageCode code)
        {
            MapCodeImg mapCodeImg = _listMapCodeImg.Find(m => m.ImageCode == code);
            if (mapCodeImg == null)
                return null;
            return mapCodeImg.BitmapSource;
        }

        //=====================================================================
        #region Privates Methods.

        /// <summary>
        /// Convert a bitmap image from a resource.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public BitmapSource BitmapToBitmapSource(Bitmap bitmap)
        {
            return Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        /// <summary>
        /// marche mal: pb de transparence!
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public BitmapImage ToBitmapSource(Bitmap bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Bmp);
                memory.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }
            return bitmapImage;
        }

        /// <summary>
        /// Load images from the ressources.
        /// </summary>
        private void LoadImages()
        {
            BitmapSource bmpSrc;
            MapCodeImg mapCodeImg;
 
            bmpSrc= BitmapToBitmapSource(Resources.Help_icon);
            mapCodeImg = new MapCodeImg { ImageCode = ImageCode.DlgQuestion, BitmapSource =bmpSrc };
            _listMapCodeImg.Add(mapCodeImg);

            // "warning-icon.png";
            bmpSrc= BitmapToBitmapSource(Resources.warning_icon);
            mapCodeImg = new MapCodeImg { ImageCode = ImageCode.DlgWarning, BitmapSource = bmpSrc };
            _listMapCodeImg.Add(mapCodeImg);

            // "X-red-border-128.png";
            bmpSrc = BitmapToBitmapSource(Resources.X_red_border_128);
            mapCodeImg = new MapCodeImg { ImageCode = ImageCode.DlgError, BitmapSource=bmpSrc};
            _listMapCodeImg.Add(mapCodeImg);

            // "Info-round-white-Blue-128.png";
            bmpSrc = BitmapToBitmapSource(Resources.Info_round_white_Blue_128);
            mapCodeImg = new MapCodeImg { ImageCode = ImageCode.DlgInformation, BitmapSource=bmpSrc};
            _listMapCodeImg.Add(mapCodeImg);

            // "check-green-24.png";
            bmpSrc = BitmapToBitmapSource(Resources.check_green_24);
            mapCodeImg = new MapCodeImg { ImageCode = ImageCode.BtnOk, BitmapSource= bmpSrc };
            _listMapCodeImg.Add(mapCodeImg);

            // "check-green-24.png";
            bmpSrc = BitmapToBitmapSource(Resources.check_green_24);
            mapCodeImg = new MapCodeImg { ImageCode = ImageCode.BtnYes, BitmapSource= bmpSrc};
            _listMapCodeImg.Add(mapCodeImg);

            // "cancel-red-24.png";
            bmpSrc = BitmapToBitmapSource(Resources.cancel_red_24);
            mapCodeImg = new MapCodeImg { ImageCode = ImageCode.BtnNo, BitmapSource= bmpSrc};
            _listMapCodeImg.Add(mapCodeImg);

            // "exit-arrow-door-24.png";
            bmpSrc = BitmapToBitmapSource(Resources.exit_arrow_door_24);
            mapCodeImg = new MapCodeImg { ImageCode = ImageCode.BtnCancel, BitmapSource= bmpSrc };
            _listMapCodeImg.Add(mapCodeImg);
        }

        //private void UseDefault_old()
        //{
        //    BitmapImage img;
        //    string uri;
        //    MapCodeImg mapCodeImg;
        //    string baseUri = "Assets/Images/";

        //    BitmapSource bmpSrc = BitmapToBitmapSource(Resources.Help_icon);
        //    //BitmapSource bmpSrc = BitmapToBitmapSource(Resources.Help_icon);
        //    img = ToBitmapSource(Resources.Help_icon);

        //    uri = baseUri + "Help-icon.png";
        //    //img = LoadImage(uri);
        //    mapCodeImg = new MapCodeImg { ImageCode = ImageCode.DlgQuestion, Uri = uri, BmpImage = img, BitmapSource = bmpSrc };
        //    _listMapCodeImg.Add(mapCodeImg);
        //    //uri = "Help-icon.png";
        //    ////img = ToBitmapSource(ResImg.Help_icon);
        //    //img = ToBitmapSource(uri);
        //    //mapCodeImg = new MapCodeImg { ImageCode = ImageCode.DlgQuestion, Uri = uri, BmpImage = img };
        //    //_listMapCodeImg.Add(mapCodeImg);

        //    uri = baseUri + "warning-icon.png";
        //    img = LoadImage(uri);
        //    mapCodeImg = new MapCodeImg { ImageCode = ImageCode.DlgWarning, Uri = uri, BmpImage = img };
        //    _listMapCodeImg.Add(mapCodeImg);

        //    uri = baseUri + "X-red-border-128.png";
        //    img = LoadImage(uri);
        //    mapCodeImg = new MapCodeImg { ImageCode = ImageCode.DlgError, Uri = uri, BmpImage = img };
        //    _listMapCodeImg.Add(mapCodeImg);

        //    // TODO:
        //    uri = baseUri + "Info-round-white-Blue-128.png";
        //    img = LoadImage(uri);
        //    mapCodeImg = new MapCodeImg { ImageCode = ImageCode.DlgInformation, Uri = uri, BmpImage = img };
        //    _listMapCodeImg.Add(mapCodeImg);

        //    uri = baseUri + "check-green-24.png";
        //    img = LoadImage(uri);
        //    mapCodeImg = new MapCodeImg { ImageCode = ImageCode.BtnOk, Uri = uri, BmpImage = img };
        //    _listMapCodeImg.Add(mapCodeImg);

        //    uri = baseUri + "check-green-24.png";
        //    img = LoadImage(uri);
        //    mapCodeImg = new MapCodeImg { ImageCode = ImageCode.BtnYes, Uri = uri, BmpImage = img };
        //    _listMapCodeImg.Add(mapCodeImg);

        //    uri = baseUri + "cancel-red-24.png";
        //    img = LoadImage(uri);
        //    mapCodeImg = new MapCodeImg { ImageCode = ImageCode.BtnNo, Uri = uri, BmpImage = img };
        //    _listMapCodeImg.Add(mapCodeImg);

        //    uri = baseUri + "exit-arrow-door-24.png";
        //    img = LoadImage(uri);
        //    mapCodeImg = new MapCodeImg { ImageCode = ImageCode.BtnCancel, Uri = uri, BmpImage = img };
        //    _listMapCodeImg.Add(mapCodeImg);

        //}

        /// <summary>
        /// TODO: n'est plus utilisé!
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private BitmapImage LoadImage(string uri)
        {
            try
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                image.UriSource = new Uri(uri, UriKind.RelativeOrAbsolute);
                image.EndInit();
                return image;
            }
            catch
            {
                return null;
                //return DependencyProperty.UnsetValue;
            }
        }

        #endregion
    }
}
