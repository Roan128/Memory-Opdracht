using Memory.BLL.BusinessObjects;
using Memory.DAL.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

        public List<CardImage> CardImages { get; set; } = new List<CardImage> { };

        public event EventHandler onClose;

        public SetCreationPopup()
        {
            InitializeComponent();
        }

        private void SetCreateBtn_Click(object sender, RoutedEventArgs e)
        {
            ImageSet set = new ImageSet(NameTextBox.Text);
            CardImages.ForEach(c => c.SetId = set.Id);
            SetService.UploadSet(set);
            ImageService.UploadImages(CardImages);

            onClose.Invoke(this, EventArgs.Empty);
            Close();
        }

        private void PickImagesBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.png; *.jpg; *.jpeg; *.gif; *.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All files (*.*)|*.*",
                Title = "Select Image",
                Multiselect = true // Allow selecting multiple images
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Handle the selected image files
                string[] selectedFiles = openFileDialog.FileNames;

                // Process the selected files (e.g., upload or save them)
                foreach (string filePath in selectedFiles)
                {
                    byte[] imageData = File.ReadAllBytes(filePath);
                    CardImage image = new CardImage(imageData);
                    CardImages.Add(image);
                }
            }
        }
    }
}
