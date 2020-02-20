using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace recepciondeDocumentos.publico.cargaDocumentos
{
    public partial class vista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            mobileBO.Facade _f = new mobileBO.Facade();
           // _f.utileriasResizeImage();
        }
    }
}