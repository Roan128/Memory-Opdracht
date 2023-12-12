using Memory.DAL.Services;
using System.Windows;

namespace Memory.GUI
{
    /// <summary>
    /// Interaction logic for SetCreationPopup.xaml
    /// </summary>
    public partial class SetCreationPopup : Window
    {
        public ImageService ImageService { get; set; } = new ImageService();

        public ImageSetService SetService { get; set; } = new ImageSetService();

        public SetCreationPopup()
        {
            InitializeComponent();
        }

        private void SetCreateBtn_Click(object sender, RoutedEventArgs e)
        {
            ImageService.UploadImages();
            SetService.
        }

        private void PickImagesBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
