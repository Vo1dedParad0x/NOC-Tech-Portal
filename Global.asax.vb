Imports System.Web.Optimization
Imports System.Web.Http
Imports System.Web.Routing
Imports StackExchange.Profiling
Imports StackExchange.Profiling.Mvc
Imports StackExchange.Profiling.Storage
Imports StackExchange.Profiling.EntityFramework6

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Sub Application_Start()
        GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)
        'AreaRegistration.RegisterAllAreas()
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
        If HttpContext.Current.IsDebuggingEnabled Then
            Dim ops As New MiniProfilerOptions() With {
            .PopupRenderPosition = RenderPosition.Right,
            .PopupMaxTracesToShow = 10,
            .ColorScheme = ColorScheme.Dark,
            .ResultsAuthorize = (Function(request) request.IsLocal),
            .ResultsListAuthorize = (Function(request) True),
            .StackMaxLength = 256,
            .TrackConnectionOpenClose = True
            }
            ops.ExcludeType("SessionFactory").ExcludeType("NHibernate").ExcludeType("Flush").AddViewProfiling()
            MiniProfilerEF6.Initialize()
            MiniProfiler.Configure(ops)
        End If
    End Sub

    Protected Sub Application_BeginRequest()
        If Request.IsLocal Then

            MiniProfiler.StartNew()
        End If
    End Sub

    Protected Sub Application_EndRequest()
        MiniProfiler.Current?.Stop()
    End Sub
End Class
