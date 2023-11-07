using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;

namespace ApiPIM.Services
{
    public class GeraRelatorio
    {
        public void CreatePDF(string diretorio)
        {
            using (FileStream fs = new FileStream(diretorio, FileMode.Create))
            {
                PdfWriter writer = new PdfWriter(fs);
                PdfDocument pdf = new PdfDocument(writer);
                pdf.GetDocumentInfo().SetTitle("Holerite");
                
                Document document = new Document(pdf, PageSize.A4);

                Color grey = new DeviceRgb(209, 213, 219);
                Color green = new DeviceRgb(56, 142, 60);
                Color yellow = new DeviceRgb(255, 193, 7);
                Color red = new DeviceRgb(211, 47, 47);
                Color waterGreen = new DeviceRgb(0, 129, 139);
                Color grey700 = new DeviceRgb(88, 88, 88);
                Color grey600 = new DeviceRgb(126, 126, 126);


                document.SetMargins(50, 36, 100,36);

                //Titulo 
                Paragraph titulo = new Paragraph(new Text("Holerite - Setembro/2023"));
                document.Add(titulo);

                //Informacoes 
                Table mainInfo = new Table(2).UseAllAvailableWidth();
                float gapSize = 40;

                Div mainDiv = new Div();

                float firstTableWidth = 150;
                Table table1 = new Table(1).UseAllAvailableWidth();

                Cell table1Cell = new Cell(1, 1).
                    Add(new Paragraph("Nome: Gustavo Antonio Gonçalves\r\nSetor: Compras\r\nCargo: Comprador Pleno").SetVerticalAlignment(VerticalAlignment.MIDDLE))
                    .SetBorder(Border.NO_BORDER);

                table1.AddCell(table1Cell);

                Cell maincell1 = new Cell(1, 1).Add(table1).SetBorder(Border.NO_BORDER);

                mainInfo.AddCell(maincell1);

                Table table2 = new Table(1).UseAllAvailableWidth();

                Cell table2Cell = new Cell(1, 1).
                    Add(new Paragraph("CPF: 529.712.994-75\r\nData Pagamento: 07/11/2023\r\nSituação: A receber").SetVerticalAlignment(VerticalAlignment.MIDDLE))
                    .SetBorder(Border.NO_BORDER);

                table2.AddCell(table2Cell);

                Cell maincell2 = new Cell(1, 1).Add(table2).SetBorder(Border.NO_BORDER);

                mainInfo.AddCell(maincell2);

                document.Add(mainInfo.SetMarginBottom(30));

                Table CalculoTable = new Table(3).UseAllAvailableWidth();

                Cell nome = new Cell(1,1).Add(new Paragraph("DESCRIÇÃO DO VALOR").SetBold().SetBackgroundColor(grey).SetPadding(2));
                Cell tipo = new Cell(1, 1).Add(new Paragraph("TIPO").SetBold().SetBackgroundColor(grey).SetPadding(2));
                Cell valor = new Cell(1, 1).Add(new Paragraph("VALOR").SetBold().SetBackgroundColor(grey).SetPadding(2));

                CalculoTable.AddCell(nome).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHorizontalAlignment(HorizontalAlignment.CENTER);
                CalculoTable.AddCell(tipo).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHorizontalAlignment(HorizontalAlignment.CENTER);
                CalculoTable.AddCell(valor).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHorizontalAlignment(HorizontalAlignment.CENTER);

                Cell nome1 = new Cell(1, 1).Add(new Paragraph("Salário base"));
                Cell tipo1 = new Cell(1, 1).Add(new Paragraph("PROVENTO"));
                Cell valor1 = new Cell(1, 1).Add(new Paragraph("( + ) R$2.300,00").SetFontColor(green).SetBold());

                CalculoTable.AddCell(nome1).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHorizontalAlignment(HorizontalAlignment.CENTER);
                CalculoTable.AddCell(tipo1).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHorizontalAlignment(HorizontalAlignment.CENTER);
                CalculoTable.AddCell(valor1).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHorizontalAlignment(HorizontalAlignment.CENTER);

                Cell nome2 = new Cell(1, 1).Add(new Paragraph("Inss"));
                Cell tipo2 = new Cell(1, 1).Add(new Paragraph("DESCONTO"));
                Cell valor2 = new Cell(1, 1).Add(new Paragraph("( - ) R$180,00").SetFontColor(red).SetBold());

                CalculoTable.AddCell(nome2).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHorizontalAlignment(HorizontalAlignment.CENTER);
                CalculoTable.AddCell(tipo2).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHorizontalAlignment(HorizontalAlignment.CENTER);
                CalculoTable.AddCell(valor2).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHorizontalAlignment(HorizontalAlignment.CENTER);

                Cell nome3 = new Cell(1, 1).Add(new Paragraph("Irrf"));
                Cell tipo3 = new Cell(1, 1).Add(new Paragraph("DESCONTO"));
                Cell valor3 = new Cell(1, 1).Add(new Paragraph("( - ) R$130,00").SetFontColor(red).SetBold());

                CalculoTable.AddCell(nome3).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHorizontalAlignment(HorizontalAlignment.CENTER);
                CalculoTable.AddCell(tipo3).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHorizontalAlignment(HorizontalAlignment.CENTER);
                CalculoTable.AddCell(valor3).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHorizontalAlignment(HorizontalAlignment.CENTER);

                Cell nome4 = new Cell(1, 1).Add(new Paragraph("Falta"));
                Cell tipo4 = new Cell(1, 1).Add(new Paragraph("DESCONTO"));
                Cell valor4 = new Cell(1, 1).Add(new Paragraph("( - ) R$77,00").SetFontColor(red).SetBold());

                CalculoTable.AddCell(nome4).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHorizontalAlignment(HorizontalAlignment.CENTER);
                CalculoTable.AddCell(tipo4).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHorizontalAlignment(HorizontalAlignment.CENTER);
                CalculoTable.AddCell(valor4).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHorizontalAlignment(HorizontalAlignment.CENTER);

                Cell nome5 = new Cell(1, 1).Add(new Paragraph("Salário Líquido"));
                Cell valor5 = new Cell(1, 2).Add(new Paragraph(" R$1.913,00").SetBold());

                CalculoTable.AddCell(nome5).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHorizontalAlignment(HorizontalAlignment.CENTER);
                CalculoTable.AddCell(valor5).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHorizontalAlignment(HorizontalAlignment.CENTER);

                document.Add(CalculoTable.SetMarginBottom(60));

                Text nomeCpfColab = new Text("Gustavo Antonio Gonçalves \r\n" +
                                           "CPF: 529.712.994-75");
                Paragraph assinatura = new Paragraph().Add(nomeCpfColab).SetBorderTop(new SolidBorder(grey600, 1));

                document.Add(assinatura.SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetWidth(200));


                document.Close();
            }
        }
    }
}
