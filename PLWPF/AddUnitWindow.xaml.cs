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
            try
            {
                
                checkExceptions();
                currentUnit.jacuzzi =  JacuzziCheckBox.IsChecked == true ? true : false;
                currentUnit.pool =  PoolCheckBox.IsChecked == true ? true : false;
                currentUnit.garden =  GardenCheckBox.IsChecked == true ? true : false;
                currentUnit.childrenAttractions = ChildrenAttractionsCheckBox.IsChecked == true ? true :false;
                currentUnit.typeArea =
                    AreaComboBox.SelectedIndex == 0 ? TypeAreaOfTheCountry.all :
                    AreaComboBox.SelectedIndex == 1 ? TypeAreaOfTheCountry.north :
                    AreaComboBox.SelectedIndex == 2 ? TypeAreaOfTheCountry.south :
                    AreaComboBox.SelectedIndex == 3 ? TypeAreaOfTheCountry.center :
                    TypeAreaOfTheCountry.jerusalem;
                currentUnit.typeOfUnit =
                    UnitComboBox.SelectedIndex == 0 ? TypeOfHostingUnit.all :
                    UnitComboBox.SelectedIndex == 1 ? TypeOfHostingUnit.zimmer :
                    UnitComboBox.SelectedIndex == 2 ? TypeOfHostingUnit.apartment :
                    UnitComboBox.SelectedIndex == 3 ? TypeOfHostingUnit.roomOfHotel :
                    TypeOfHostingUnit.tent;
                currentUnit.uris = new List<string>();
                if(unitPictures.Text!="") currentUnit.uris.Add(unitPictures.Text);
                if (unitPictures1.Text != "") 
                    currentUnit.uris.Add(unitPictures1.Text);
                bl.addHostingUnit(currentUnit);
                currentUnit = new HostingUnit();
                this.DataContext = currentUnit;
                this.Close();
            }
            catch(Exception ex)
            {               
                MessageBox.Show(ex.Message);
                if (ex.Message== "your unit has been registred sucessfully") this.Close();
            }
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
        private void checkExceptions()
        {
            bool f1 = false;
            bool f2 = false;
            bool f3 = false;
            for (int i = 0; i < NameHotelBox.Text.Count(); i++)
            {
                if (NameHotelBox.Text[i] < 65 || (NameHotelBox.Text[i] > 90 && NameHotelBox.Text[i] < 96) || NameHotelBox.Text[i] > 123)
                { nameError.Visibility = Visibility.Visible; f1 = true; }
            }
            if (f1 == false)
                nameError.Visibility = Visibility.Hidden;

            for (int i = 0; i < AdultsplacesBbox.Text.Count(); i++)
            {
                if (AdultsplacesBbox.Text[i] < 48 || AdultsplacesBbox.Text[i] > 57)
                { adultError.Visibility = Visibility.Visible; f2 = true; }
            }
            if (f2 == false)
                adultError.Visibility = Visibility.Hidden;
            for (int i = 0; i < ChildrenplacesBox.Text.Count(); i++)
            {
                if (ChildrenplacesBox.Text[i] < 48 || ChildrenplacesBox.Text[i] > 57)
                { childrenError.Visibility = Visibility.Visible; f3 = true; }
            }
            if (f3 == false)
                childrenError.Visibility = Visibility.Hidden;

            if (f1||f2|f3) throw new Exception("please check your items and try again");

        }

    }
}
