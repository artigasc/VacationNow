#pragma checksum "C:\Users\Alonsso\Desktop\GoTour\GoTourSiteAdmin\Views\UserPortal\_TableUserPortal.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "26735193445a8a28cc80e7f41909523faa8afd16"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_UserPortal__TableUserPortal), @"mvc.1.0.view", @"/Views/UserPortal/_TableUserPortal.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/UserPortal/_TableUserPortal.cshtml", typeof(AspNetCore.Views_UserPortal__TableUserPortal))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\Alonsso\Desktop\GoTour\GoTourSiteAdmin\Views\_ViewImports.cshtml"
using GoTourSiteAdmin;

#line default
#line hidden
#line 2 "C:\Users\Alonsso\Desktop\GoTour\GoTourSiteAdmin\Views\_ViewImports.cshtml"
using GoTourSiteAdmin.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"26735193445a8a28cc80e7f41909523faa8afd16", @"/Views/UserPortal/_TableUserPortal.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2e500646c56979d21c8c038930ad2d9543f05163", @"/Views/_ViewImports.cshtml")]
    public class Views_UserPortal__TableUserPortal : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<GoTourSiteAdmin.Models.UserPortalViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(62, 822, true);
            WriteLiteral(@"<h3 style=""font-family:'BebasNeue'"">Todos los usuarios</h3>
<button onclick=""CallAddFormUser()"" class=""btn btn-sm btn-orange"">Nuevo Usuario</button>
<div class=""table-responsive py-3"">
    <table id=""TableUser"" class=""table bg-white text-dark"">
        <thead>
            <tr>
                <th scope=""col"">Usuario</th>
                <th scope=""col"">Nombre</th>
                <th scope=""col"">Apellidos</th>
                <th scope=""col"">Email</th>
                <th scope=""col"">Foto</th>
                <th scope=""col"">Teléfono</th>
                <th scope=""col"">Compañía</th>
                <th scope=""col"">Estado</th>
                <th scope=""col"">Fecha de Registro</th>

                <th scope=""col"" style=""width: 15%;""></th>
            </tr>
        </thead>

        <tbody>
");
            EndContext();
#line 23 "C:\Users\Alonsso\Desktop\GoTour\GoTourSiteAdmin\Views\UserPortal\_TableUserPortal.cshtml"
              
                if (Model != null && Model.Count() > 0) {
                    foreach (var vItem in Model) {

#line default
#line hidden
            BeginContext(1011, 62, true);
            WriteLiteral("                        <tr>\r\n                            <td>");
            EndContext();
            BeginContext(1074, 14, false);
#line 27 "C:\Users\Alonsso\Desktop\GoTour\GoTourSiteAdmin\Views\UserPortal\_TableUserPortal.cshtml"
                           Write(vItem.UserName);

#line default
#line hidden
            EndContext();
            BeginContext(1088, 39, true);
            WriteLiteral("</td>\r\n                            <td>");
            EndContext();
            BeginContext(1128, 15, false);
#line 28 "C:\Users\Alonsso\Desktop\GoTour\GoTourSiteAdmin\Views\UserPortal\_TableUserPortal.cshtml"
                           Write(vItem.FirstName);

#line default
#line hidden
            EndContext();
            BeginContext(1143, 39, true);
            WriteLiteral("</td>\r\n                            <td>");
            EndContext();
            BeginContext(1183, 19, false);
#line 29 "C:\Users\Alonsso\Desktop\GoTour\GoTourSiteAdmin\Views\UserPortal\_TableUserPortal.cshtml"
                           Write(vItem.FirstLastName);

#line default
#line hidden
            EndContext();
            BeginContext(1202, 1, true);
            WriteLiteral(" ");
            EndContext();
            BeginContext(1204, 19, false);
#line 29 "C:\Users\Alonsso\Desktop\GoTour\GoTourSiteAdmin\Views\UserPortal\_TableUserPortal.cshtml"
                                                Write(vItem.SecondLatName);

#line default
#line hidden
            EndContext();
            BeginContext(1223, 41, true);
            WriteLiteral("</td>\r\n\r\n                            <td>");
            EndContext();
            BeginContext(1265, 11, false);
#line 31 "C:\Users\Alonsso\Desktop\GoTour\GoTourSiteAdmin\Views\UserPortal\_TableUserPortal.cshtml"
                           Write(vItem.Email);

#line default
#line hidden
            EndContext();
            BeginContext(1276, 72, true);
            WriteLiteral("</td>\r\n                            <td><img class=\"img-fluid\" width=\"55\"");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 1348, "\"", 1369, 1);
#line 32 "C:\Users\Alonsso\Desktop\GoTour\GoTourSiteAdmin\Views\UserPortal\_TableUserPortal.cshtml"
WriteAttributeValue("", 1354, vItem.UrlPhoto, 1354, 15, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(1370, 64, true);
            WriteLiteral(" alt=\"Foto de Usuario\" /></td>\r\n                            <td>");
            EndContext();
            BeginContext(1435, 11, false);
#line 33 "C:\Users\Alonsso\Desktop\GoTour\GoTourSiteAdmin\Views\UserPortal\_TableUserPortal.cshtml"
                           Write(vItem.Phone);

#line default
#line hidden
            EndContext();
            BeginContext(1446, 39, true);
            WriteLiteral("</td>\r\n                            <td>");
            EndContext();
            BeginContext(1486, 17, false);
#line 34 "C:\Users\Alonsso\Desktop\GoTour\GoTourSiteAdmin\Views\UserPortal\_TableUserPortal.cshtml"
                           Write(vItem.CompanyName);

#line default
#line hidden
            EndContext();
            BeginContext(1503, 41, true);
            WriteLiteral("</td>\r\n                            <td>\r\n");
            EndContext();
#line 36 "C:\Users\Alonsso\Desktop\GoTour\GoTourSiteAdmin\Views\UserPortal\_TableUserPortal.cshtml"
                                 if (@vItem.State == 1) {

#line default
#line hidden
            BeginContext(1603, 85, true);
            WriteLiteral("                                    <span class=\"badge badge-success\">Activo</span>\r\n");
            EndContext();
#line 38 "C:\Users\Alonsso\Desktop\GoTour\GoTourSiteAdmin\Views\UserPortal\_TableUserPortal.cshtml"
                                } else {

#line default
#line hidden
            BeginContext(1730, 86, true);
            WriteLiteral("                                    <span class=\"badge badge-danger\">Inactivo</span>\r\n");
            EndContext();
#line 40 "C:\Users\Alonsso\Desktop\GoTour\GoTourSiteAdmin\Views\UserPortal\_TableUserPortal.cshtml"
                                }

#line default
#line hidden
            BeginContext(1851, 67, true);
            WriteLiteral("                            </td>\r\n                            <td>");
            EndContext();
            BeginContext(1919, 16, false);
#line 42 "C:\Users\Alonsso\Desktop\GoTour\GoTourSiteAdmin\Views\UserPortal\_TableUserPortal.cshtml"
                           Write(vItem.DateCreate);

#line default
#line hidden
            EndContext();
            BeginContext(1935, 80, true);
            WriteLiteral("</td>\r\n                            <td>\r\n                                <button");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 2015, "\"", 2029, 1);
#line 44 "C:\Users\Alonsso\Desktop\GoTour\GoTourSiteAdmin\Views\UserPortal\_TableUserPortal.cshtml"
WriteAttributeValue("", 2020, vItem.Id, 2020, 9, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2030, 188, true);
            WriteLiteral(" data-access-form=\"EditUser\" onclick=\"btnCallForm(this.id)\" type=\"button\" class=\"btn btn-sm btn-primary\"><i class=\"fas fa-pencil-alt\"></i></button>\r\n                                <button");
            EndContext();
            BeginWriteAttribute("id", " id=\"", 2218, "\"", 2232, 1);
#line 45 "C:\Users\Alonsso\Desktop\GoTour\GoTourSiteAdmin\Views\UserPortal\_TableUserPortal.cshtml"
WriteAttributeValue("", 2223, vItem.Id, 2223, 9, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2233, 165, true);
            WriteLiteral(" onclick=\"btnDelete(this.id)\" class=\"btn btn-sm btn-danger\"><i class=\"fas fa-trash\"></i></button>\r\n                            </td>\r\n                        </tr>\r\n");
            EndContext();
#line 48 "C:\Users\Alonsso\Desktop\GoTour\GoTourSiteAdmin\Views\UserPortal\_TableUserPortal.cshtml"
                    }
                } else {

#line default
#line hidden
            BeginContext(2447, 148, true);
            WriteLiteral("                    <tr class=\"text-center\">\r\n                        <td colspan=\"10\">No hay Registro de Usuarios</td>\r\n                    </tr>\r\n");
            EndContext();
#line 53 "C:\Users\Alonsso\Desktop\GoTour\GoTourSiteAdmin\Views\UserPortal\_TableUserPortal.cshtml"
                }
            

#line default
#line hidden
            BeginContext(2629, 50, true);
            WriteLiteral("\r\n\r\n        </tbody>\r\n    </table>\r\n</div>\r\n    \r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<GoTourSiteAdmin.Models.UserPortalViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591