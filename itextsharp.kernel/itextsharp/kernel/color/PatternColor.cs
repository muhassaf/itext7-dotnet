/*
$Id$

This file is part of the iText (R) project.
Copyright (c) 1998-2016 iText Group NV
Authors: Bruno Lowagie, Paulo Soares, et al.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
*/
using System;
using iTextSharp.Kernel.Pdf.Colorspace;

namespace iTextSharp.Kernel.Color
{
	public class PatternColor : iTextSharp.Kernel.Color.Color
	{
		private PdfPattern pattern;

		private iTextSharp.Kernel.Color.Color underlyingColor;

		public PatternColor(PdfPattern coloredPattern)
			: base(new PdfSpecialCs.Pattern(), null)
		{
			// The underlying color for uncolored patterns. Will be null for colored ones.
			this.pattern = coloredPattern;
		}

		public PatternColor(PdfPattern.Tiling uncoloredPattern, iTextSharp.Kernel.Color.Color
			 color)
			: this(uncoloredPattern, color.GetColorSpace(), color.GetColorValue())
		{
		}

		public PatternColor(PdfPattern.Tiling uncoloredPattern, PdfColorSpace underlyingCS
			, float[] colorValue)
			: base(new PdfSpecialCs.UncoloredTilingPattern(underlyingCS), colorValue)
		{
			if (underlyingCS is PdfSpecialCs.Pattern)
			{
				throw new ArgumentException("underlyingCS");
			}
			this.pattern = uncoloredPattern;
			this.underlyingColor = iTextSharp.Kernel.Color.Color.MakeColor(underlyingCS, colorValue
				);
		}

		public PatternColor(PdfPattern.Tiling uncoloredPattern, PdfSpecialCs.UncoloredTilingPattern
			 uncoloredTilingCS, float[] colorValue)
			: base(uncoloredTilingCS, colorValue)
		{
			this.pattern = uncoloredPattern;
			this.underlyingColor = iTextSharp.Kernel.Color.Color.MakeColor(uncoloredTilingCS.
				GetUnderlyingColorSpace(), colorValue);
		}

		public virtual PdfPattern GetPattern()
		{
			return pattern;
		}

		public virtual void SetPattern(PdfPattern pattern)
		{
			this.pattern = pattern;
		}

		public override bool Equals(Object o)
		{
			if (!base.Equals(o))
			{
				return false;
			}
			iTextSharp.Kernel.Color.PatternColor color = (iTextSharp.Kernel.Color.PatternColor
				)o;
			return pattern.Equals(color.pattern) && (underlyingColor != null ? underlyingColor
				.Equals(color.underlyingColor) : color.underlyingColor == null);
		}
	}
}