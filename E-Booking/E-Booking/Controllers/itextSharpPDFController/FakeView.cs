using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace itextSharpPDFController
{
    public class FakeView : IView
    {
        #region IView Members

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}