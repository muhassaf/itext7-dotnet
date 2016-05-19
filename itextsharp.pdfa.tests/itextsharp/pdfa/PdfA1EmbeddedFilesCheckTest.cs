using System;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Rules;
using iTextSharp.Kernel.Pdf;
using iTextSharp.Kernel.Pdf.Filespec;

namespace iTextSharp.Pdfa
{
	public class PdfA1EmbeddedFilesCheckTest
	{
		public const String sourceFolder = "../../resources/itextsharp/pdfa/";

		[Rule]
		public ExpectedException thrown = ExpectedException.None();

		/// <exception cref="System.IO.IOException"/>
		/// <exception cref="iTextSharp.Kernel.Xmp.XMPException"/>
		[Test]
		public virtual void FileSpecCheckTest01()
		{
			thrown.Expect(typeof(PdfAConformanceException));
			thrown.ExpectMessage(PdfAConformanceException.NameDictionaryShallNotContainTheEmbeddedFilesKey
				);
			PdfWriter writer = new PdfWriter(new MemoryStream());
			Stream @is = new FileStream(sourceFolder + "sRGB Color Space Profile.icm");
			PdfOutputIntent outputIntent = new PdfOutputIntent("Custom", "", "http://www.color.org"
				, "sRGB IEC61966-2.1", @is);
			PdfADocument pdfDocument = new PdfADocument(writer, PdfAConformanceLevel.PDF_A_1B
				, outputIntent);
			PdfDictionary fileNames = new PdfDictionary();
			pdfDocument.GetCatalog().Put(PdfName.Names, fileNames);
			PdfDictionary embeddedFiles = new PdfDictionary();
			fileNames.Put(PdfName.EmbeddedFiles, embeddedFiles);
			PdfArray names = new PdfArray();
			fileNames.Put(PdfName.Names, names);
			names.Add(new PdfString("some/file/path"));
			PdfFileSpec spec = PdfFileSpec.CreateEmbeddedFileSpec(pdfDocument, sourceFolder +
				 "sample.wav", "sample.wav", "sample", null, null, true);
			names.Add(spec.GetPdfObject());
			pdfDocument.AddNewPage();
			pdfDocument.Close();
		}

		/// <exception cref="System.IO.IOException"/>
		/// <exception cref="iTextSharp.Kernel.Xmp.XMPException"/>
		[Test]
		public virtual void FileSpecCheckTest02()
		{
			thrown.Expect(typeof(PdfAConformanceException));
			thrown.ExpectMessage(PdfAConformanceException.StreamObjDictShallNotContainForFFilterOrFDecodeParams
				);
			PdfWriter writer = new PdfWriter(new MemoryStream());
			Stream @is = new FileStream(sourceFolder + "sRGB Color Space Profile.icm");
			PdfOutputIntent outputIntent = new PdfOutputIntent("Custom", "", "http://www.color.org"
				, "sRGB IEC61966-2.1", @is);
			PdfADocument pdfDocument = new PdfADocument(writer, PdfAConformanceLevel.PDF_A_1B
				, outputIntent);
			PdfStream stream = new PdfStream();
			pdfDocument.GetCatalog().Put(new PdfName("testStream"), stream);
			PdfFileSpec spec = PdfFileSpec.CreateEmbeddedFileSpec(pdfDocument, sourceFolder +
				 "sample.wav", "sample.wav", "sample", null, null, true);
			stream.Put(PdfName.F, spec.GetPdfObject());
			pdfDocument.AddNewPage();
			pdfDocument.Close();
		}

		/// <exception cref="System.IO.IOException"/>
		/// <exception cref="iTextSharp.Kernel.Xmp.XMPException"/>
		[Test]
		public virtual void FileSpecCheckTest03()
		{
			thrown.Expect(typeof(PdfAConformanceException));
			thrown.ExpectMessage(PdfAConformanceException.FileSpecificationDictionaryShallNotContainTheEFKey
				);
			PdfWriter writer = new PdfWriter(new MemoryStream());
			Stream @is = new FileStream(sourceFolder + "sRGB Color Space Profile.icm");
			PdfOutputIntent outputIntent = new PdfOutputIntent("Custom", "", "http://www.color.org"
				, "sRGB IEC61966-2.1", @is);
			PdfADocument pdfDocument = new PdfADocument(writer, PdfAConformanceLevel.PDF_A_1B
				, outputIntent);
			PdfStream stream = new PdfStream();
			pdfDocument.GetCatalog().Put(new PdfName("testStream"), stream);
			PdfFileSpec spec = PdfFileSpec.CreateEmbeddedFileSpec(pdfDocument, sourceFolder +
				 "sample.wav", "sample.wav", "sample", null, null, true);
			stream.Put(new PdfName("fileData"), spec.GetPdfObject());
			pdfDocument.AddNewPage();
			pdfDocument.Close();
		}
	}
}