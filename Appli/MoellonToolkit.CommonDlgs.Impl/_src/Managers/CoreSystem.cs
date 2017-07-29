using MoellonToolkit.CommonDlgs.Defs;

namespace MoellonToolkit.CommonDlgs.Impl
{
    public class CoreSystem
    {
        /// <summary>
        /// Manager of images, icons for Question, Warning, Error,...
        /// </summary>
        private ImageProvider _imageProvider;

        private TranslationMgr _translationMgr;

        public CoreSystem()
        {
            _imageProvider = new ImageProvider();
            _translationMgr = new TranslationMgr();
        }

        public ImageProvider ImageProvider { get { return _imageProvider; } }

        public TranslationMgr TranslationMgr { get { return _translationMgr; } }
    }
}
