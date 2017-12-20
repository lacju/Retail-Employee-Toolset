using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace retailemployeetoolset.Common
{
    class CodeHoldert
    {
    }
}
//public static async void PopulateDataOnStart()
//{
//    await AzureDataService.Intialize();
//    _queueCustomerDatabase = await AzureDataService.GetQueueCustomers();
//    _solutionDatabase = await AzureDataService.GetSolutions();
//    _customerDatabase = await AzureDataService.GetCustomers();
//    _productDatabase = await AzureDataService.GetProducts();
//    _appointmentDatabase = await AzureDataService.GetAppointments();

//}

//public static async void RefreshDatabase()
//{
//    _queueCustomerDatabase = await AzureDataService.GetQueueCustomers();
//    _solutionDatabase = await AzureDataService.GetSolutions();
//    _customerDatabase = await AzureDataService.GetCustomers();
//    _productDatabase = await AzureDataService.GetProducts();
//}

//public static async Task CreateNewProductAddToDatabase(string productDetails)
//{

//    currentLine = productDetails;
//    Product cake = new Product(GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"));
//    await AzureDataService.AddProduct(cake);

//}

//public static async void UploadProductsToDatabaseFromFile()
//{
//    var picker = new Windows.Storage.Pickers.FileOpenPicker();
//    picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
//    picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
//    picker.FileTypeFilter.Add(".txt");
//    picker.FileTypeFilter.Add(".csv");
//    picker.FileTypeFilter.Add(".xls");
//    StorageFile productsToAdd = await picker.PickSingleFileAsync();
//    IList<string> productList = await FileIO.ReadLinesAsync(productsToAdd, Windows.Storage.Streams.UnicodeEncoding.Utf8);
//    if (productsToAdd != null)
//    {
//        List<Product> tempList = new List<Product>();
//        foreach (var productLine in productList)
//        {
//            currentLine = productLine;
//            tempList.Add(new Product(GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|")));
//        }
//        foreach (Product p in tempList)
//        {
//            await AzureDataService.AddProduct(p);
//        }
//    }
//    else
//    {
//        MessageDialog noFile = new MessageDialog("No file selected");
//        await noFile.ShowAsync();
//    }
//}
//public static async Task SetSkuList(StorageFile skulist)
//{
//    StorageFile skuListTemp = await localFolder.CreateFileAsync("SKUList.txt", CreationCollisionOption.ReplaceExisting);
//    listSkuList = await FileIO.ReadLinesAsync(skulist);
//    foreach (var item in listSkuList)
//    {
//        await FileIO.AppendTextAsync(skuListTemp, Environment.NewLine + item);
//    }
//}

//public static async Task CreateDatabase()
//{
//    StorageFile problemSkus = await localFolder.CreateFileAsync("ProblematicSKUs.txt", CreationCollisionOption.ReplaceExisting);
//    StorageFile productDatabase = await localFolder.CreateFileAsync("ProductDatabase.txt", CreationCollisionOption.ReplaceExisting);
//    await Windows.Storage.FileIO.WriteTextAsync(problemSkus, "Problem SKUs (No Product Page)");
//    string startTime = DateTime.Now.ToString();
//    foreach (var sku in listSkuList)
//    {
//        if (sku != null && sku != "")
//        {
//            string productPage = await QueryProductPage(sku);
//            if (productPage != "no product page results")
//            {
//                string GPU = RetrieveProductGPU(productPage);
//                string Name = RetrieveProductName(productPage);
//                string Display = RetrieveProductDisplayInfo(productPage);
//                string Weight = RetrieveProductWeight(productPage);
//                string Processor = RetrieveProductProcessor(productPage);
//                string Price = RetrieveProductPrice(productPage);
//                string RAM = RetrieveProductRAM(productPage);
//                string HDDCap = RetrieveProductHDDSize(productPage);
//                string Manufacturer = RetrieveProductMFG(productPage);
//                string description = RetrieveProductDescription(productPage);
//                string imageUrl = RetrieveProductImageURL(productPage);
//                await Windows.Storage.FileIO.AppendTextAsync(productDatabase, Environment.NewLine + ":" + sku + "|" + ":" + Manufacturer + "|" + ":" + Name + "|" + ":" + description + "|" + ":" + Price + "|" + ":" + HDDCap +
//                                  "|" + ":" + RAM + "|" + ":" + Processor + "|" + ":" + Weight + "|" + ":" + Display + "|" + ":" + GPU + "|" + ":" + imageUrl + "|");
//            }
//            else
//            {

//                await Windows.Storage.FileIO.AppendTextAsync(problemSkus, Environment.NewLine + sku);
//            }
//        }

//    }
//    string endTime = DateTime.Now.ToString();
//    MessageDialog finished = new MessageDialog("Finished Populating SKUs" + Environment.NewLine + "Start Time: " + startTime + Environment.NewLine + "End Time: " + endTime);
//    await finished.ShowAsync();
//}





//public static async Task PopulateProductDatabase()
//{

//    StorageFile productDatabase = await localFolder.GetFileAsync("ProductDatabase.txt");
//    IList<string> dataBaseList = await FileIO.ReadLinesAsync(productDatabase);
//    List<Product> tempList = new List<Product>();


//    foreach (var productLine in dataBaseList)
//    {
//        currentLine = productLine;
//        tempList.Add(new Product(GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|"), GetContentBetweenStrings(currentLine, ":", "|")));
//    }

//    await AzureDataService.Intialize();
//    foreach (Product p in tempList)
//    {
//        await AzureDataService.AddProduct(p);
//    }
//}