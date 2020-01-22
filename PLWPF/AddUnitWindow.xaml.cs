using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE1;
using BL1;

namespace PLWPF
{
    /// <summary>
    /// Logique d'interaction pour AddUnitWindow.xaml
    /// </summary>
    public partial class AddUnitWindow : Window
    {
        int imageIndex;
        Viewbox vbImage;
        Image MyImage;
        public HostingUnit currentUnit;
        IBL bl;

        public AddUnitWindow(Host host)
        {
            vbImage = new Viewbox();
            currentUnit = new HostingUnit();
            currentUnit.owner = host;
            InitializeComponent();
            this.DataContext = currentUnit;
           
            bl = FactoryBL.getBL();
            UnitControlGrid.Children.Add(vbImage);
            imageIndex = 0;
            vbImage.Width = 75;
            vbImage.Height = 75;
            vbImage.Stretch = Stretch.Fill;
            Grid.SetColumn(vbImage, 2);
            Grid.SetRow(vbImage, 0);
            MyImage = CreateViewImage();
            vbImage.Child = null;
            vbImage.Child = MyImage;
            this.AreaComboBox.ItemsSource = Enum.GetValues(typeof(BE1.TypeAreaOfTheCountry));
            this.UnitComboBox.ItemsSource = Enum.GetValues(typeof(BE1.TypeOfHostingUnit));

        }

        private void butoun_addUnit(object sender, RoutedEventArgs e)

        {
            //Window sendmail = new SendMail();
            //sendmail.Show();
            //try
            {
                //checkExceptions();
                //currentUnit.jacuzzi =  JacuzziCheckBox.IsChecked == true ? true : false;
                //currentUnit.pool =  PoolCheckBox.IsChecked == true ? true : false;
                //currentUnit.garden =  GardenCheckBox.IsChecked == true ? true : false;
                //currentUnit.childrenAttractions =  ChildrenAttractionCheckBox.IsChecked == true ? true :false;
                currentUnit.uris = new List<string>();
                currentUnit.uris.Add(unitPictures.Text);
                currentUnit.uris.Add(unitPictures1.Text);
                bl.addHostingUnit(currentUnit);
                currentUnit = new HostingUnit();
                this.DataContext = currentUnit;               
            }
            /*catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }*/
        }

        private void AdultsPlacesBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private Image CreateViewImage()
        {
            Image dynamicImage = new Image();
            BitmapImage bitmap = new BitmapImage();
            //bitmap.BeginInit();
            //bitmap.UriSource = new Uri(@currentUnit.uris[imageIndex]);
            //bitmap.EndInit();
            // Set Image.Source
            dynamicImage.Source = bitmap;
            // Add Image to Window
            return dynamicImage;
        }
        private void vbImage_MouseLeave(object sender, MouseEventArgs e)
        {
            vbImage.Width = 75;
            vbImage.Height = 75;
        }
        private void vbImage_MouseEnter(object sender, MouseEventArgs e)
        {
            vbImage.Width = this.Width / 3;
            vbImage.Height = this.Height;
        }
        private void vbImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            vbImage.Child = null;
            imageIndex = (imageIndex + currentUnit.uris.Count - 1) % currentUnit.uris.Count;
            MyImage = CreateViewImage();
            vbImage.Child = MyImage;
        }

    }
}
