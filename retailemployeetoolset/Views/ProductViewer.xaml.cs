using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace retailemployeetoolset
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductViewer : Page
    {
        //WebReader wr = new WebReader();
        public StorageFile testFile;
       
        public string BetweenTwoStrings(string fullString)
        {
            string STR = fullString;
            string firstString = "<a href=\"/store/msusa/en_US/pdp/";
            string secondString = "\" pid";
            string finalString;

            int Pos1 = STR.IndexOf(firstString) + firstString.Length;

            int Pos2 = STR.IndexOf(secondString);

            finalString = STR.Substring(Pos1, Pos2 - Pos1);

            return finalString;

        }

        public ProductViewer()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        //private async void _TestRunSKU_Click(object sender, RoutedEventArgs e)
        //{        

        //    try
        //    {
        //        _textBlockMFG.Text = await DataManager.RetrieveProductMFG(_TestEnterSKU.Text);
        //        _textBlockName.Text = await DataManager.RetrieveProductName(_TestEnterSKU.Text);
        //        _textBlockDescription.Text = await DataManager.RetrieveProductDescription(_TestEnterSKU.Text);
        //        _ProductImage.Source = new BitmapImage(await DataManager.RetrieveProductImageURL(_TestEnterSKU.Text));
        //        _PageOutputBox.Text = ProductManager.productPageOutput1;
        //        _textBlockPrice.Text = await ProductManager.RetrieveProductPrice(_TestEnterSKU.Text);
        //        _textBlockHDDSize.Text = await ProductManager.RetrieveProductHDDSize(_TestEnterSKU.Text);
        //        _textBlockProcessor.Text = await ProductManager.RetrieveProductProcessor(_TestEnterSKU.Text);
        //        _textBlockRAM.Text = await ProductManager.RetrieveProductRAM(_TestEnterSKU.Text);
        //        _textBlockGPU.Text = await ProductManager.RetrieveProductGPU(_TestEnterSKU.Text);
        //        _textBlockWeight.Text = await ProductManager.RetrieveProductWeight(_TestEnterSKU.Text);
        //        _textBlockDisplay.Text = await ProductManager.RetrieveProductDisplayInfo(_TestEnterSKU.Text);

        //    }
        //    catch (Exception exc)
        //    {
        //        MessageDialog cake = new MessageDialog(exc.ToString());
        //        await cake.ShowAsync();
        //    }

        //}

        //private async void _testButtonLoadExcelDoc_Click(object sender, RoutedEventArgs e)
        //{
            

        //    var picker = new Windows.Storage.Pickers.FileOpenPicker();
        //    picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
        //    picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
        //    picker.FileTypeFilter.Add(".txt");
        //    picker.FileTypeFilter.Add(".csv");
        //    picker.FileTypeFilter.Add(".xls");
        //    StorageFile testFile = await picker.PickSingleFileAsync();
        //    if (testFile != null)
        //    {
        //        // Application now has read/write access to the picked file
                
        //        await ProductManager.NextLine(testFile);
        //        string teststring = "";
        //        foreach (var item in ProductManager.cake)
        //        {
        //            teststring = teststring + System.Environment.NewLine + item;
        //            _PageOutputBox.Text = teststring;
        //        }
        //    }
        //    else
        //    {
        //        _PageOutputBox.Text = "Operation cancelled.";
        //    }

            
        //}

        //private async void _testButtonPopulateListbox_Click(object sender, RoutedEventArgs e)
        //{

        //    try
        //    {
        //        await ProductManager.PopulateProductListFromFile();
        //    }
        //    catch (Exception exch)
        //    {
        //        MessageDialog cake1 = new MessageDialog("failed to call populate sku list");
        //        MessageDialog cake = new MessageDialog(exch.ToString());
        //        await cake1.ShowAsync();
        //       await cake.ShowAsync();
        //    }
           
        //}


    }
}

