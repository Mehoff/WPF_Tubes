using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Tubes
{
    public sealed class ImageHolder
    {
        private static Dictionary<TubeType, Image> images = new Dictionary<TubeType, Image>();

        private ImageHolder()
        {
            string[] paths = new string[]
            {
                "/Tubes;component/Resources/Empty.jpg",
                "/Tubes;component/Resources/Crane.jpg",
                "/Tubes;component/Resources/StraightPipe0.jpg",
                "/Tubes;component/Resources/StraightPipe1.jpg",
                "/Tubes;component/Resources/TurnPipe0.jpg",
                "/Tubes;component/Resources/TurnPipe1.jpg",
                "/Tubes;component/Resources/TurnPipe2.jpg",
                "/Tubes;component/Resources/TurnPipe3.jpg",
                "/Tubes;component/Resources/StraightPipeWithWater0.jpg",
                "/Tubes;component/Resources/StraightPipeWithWater1.jpg",
                "/Tubes;component/Resources/TurnPipeWithWater0.jpg",
                "/Tubes;component/Resources/TurnPipeWithWater1.jpg",
                "/Tubes;component/Resources/TurnPipeWithWater2.jpg",
                "/Tubes;component/Resources/TurnPipeWithWater3.jpg",
            };
            for (int i = 0; i < Enum.GetValues(typeof(TubeType)).Length; i++)
            {
                Image newImage = new Image
                {
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    Stretch = Stretch.Fill
                };
                newImage.Source = new BitmapImage(new Uri(paths[i], UriKind.Relative));

                images.Add((TubeType)i, newImage);
            }
        }

        static ImageHolder() { }

        private static readonly ImageHolder instance = new ImageHolder();

        public static ImageHolder GetInstance() { return instance; }

        public Image GetImage(TubeType dir)
        {
            Image image = new Image
            {
                Source = images[dir].Source,
                Stretch = Stretch.Fill
            };
            return image;
        }
    }
}
