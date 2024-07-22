// See https://aka.ms/new-console-template for more information
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Runtime.CompilerServices;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        ConfigureQuestPDF();
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                 page.ContinuousSize(50.8f, Unit.Millimetre);
                // page.Size(PageSizes.A4);
               // page.Size(new PageSize( 25.4f, 50.8f, Unit.Millimetre));
                // page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Content()
                    .Padding(1, Unit.Millimetre)
                    .AlignCenter()
                    .Height(25.4f, Unit.Millimetre)
                    .Column(x =>
                    {
                        x.Spacing(1);
                        foreach (var item in ProductsBarcodes)
                        {
                            x.Item().PaddingTop(1, Unit.Millimetre).Text(item.CompanyName).AlignCenter().FontSize(5);
                            x.Item().PaddingTop(1, Unit.Millimetre).AlignCenter().Text(item.Name).FontSize(10).ExtraBold();
                            x.Item().PaddingTop(1, Unit.Millimetre).AlignCenter().Text(item.Barcode).FontFamily("Libre Barcode 128").FontSize(20);
                            x.Item().TranslateY(-8).AlignCenter().Text(item.Barcode).FontSize(6);
                            x.Item().PaddingBottom(1, Unit.Millimetre).ContentFromRightToLeft().AlignCenter().Text(text =>
                            {
                                text.Span($"{item.Price:N2}").Bold().FontSize(9);
                                text.Span(" ر.س").Bold().FontSize(7);
                            });
                        }
                        //x.Item().PaddingTop(1,Unit.Millimetre).Text(ProductsBarcodes.FirstOrDefault().CompanyName).AlignCenter().FontSize(5);
                        //x.Item().PaddingTop(1,Unit.Millimetre).AlignCenter().Text(ProductsBarcodes.FirstOrDefault().Name).FontSize(10).ExtraBold();
                        //x.Item().PaddingTop(1,Unit.Millimetre).AlignCenter().Text(ProductsBarcodes.FirstOrDefault().Barcode).FontFamily("Libre Barcode 128").FontSize(20);
                        //x.Item().TranslateY(-8).AlignCenter().Text(ProductsBarcodes.FirstOrDefault().Barcode).FontSize(6);
                        //x.Item().PaddingBottom(1, Unit.Millimetre).ContentFromRightToLeft().AlignCenter().Text(text=>
                        //{
                        //    text.Span($"{ProductsBarcodes.FirstOrDefault().Price:N2}").Bold().FontSize(9);
                        //    text.Span(" ر.س").Bold().FontSize(7);
                        //});

                    });
            });
        });
        document.ShowInPreviewer();



        static void ConfigureQuestPDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            FontManager.RegisterFont(File.OpenRead("Fonts/Almarai-Regular.ttf"));
            FontManager.RegisterFont(File.OpenRead("Fonts/LibreBarcode39Extended-Regular.ttf"));
            FontManager.RegisterFont(File.OpenRead("Fonts/LibreBarcode128-Regular.ttf"));
        }
    }

    private static List<PrintProductsBarcodes> ProductsBarcodes =new List<PrintProductsBarcodes>() { 
    new PrintProductsBarcodes{Name="TV",CompanyName="Samsung Company", Barcode="5515154841515",Price=2000,CurrencyCode="ر.س"},
    new PrintProductsBarcodes{Name="Fan",CompanyName="Apple Company", Barcode="11225658455",Price=1000,CurrencyCode="ر.س"},
    new PrintProductsBarcodes{Name="LapTop",CompanyName="Microsoft Company", Barcode="22331454488",Price=5000,CurrencyCode="ر.س"},
    new PrintProductsBarcodes{Name="Mobile",CompanyName="Realme Company", Barcode="3365445458544",Price=1200,CurrencyCode="ر.س"},
    new PrintProductsBarcodes{Name="Router",CompanyName="TP-Link Company", Barcode="4484766644545",Price=500,CurrencyCode="ر.س"},
    };
}

public class PrintProductsBarcodes
{
    public string CompanyName { get; set; }
    public string Name { get; set; }
    public string Barcode { get; set; }
    public decimal Price { get; set; }
    public string CurrencyCode { get; set; }
}