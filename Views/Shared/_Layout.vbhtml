@Imports StackExchange.Profiling
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1,user-scalable=no">
    <base href='@Url.Content("~/")' />
    <title>@ViewBag.Title</title>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
    @Scripts.Render("~/bundles/jquery")
    <script src="~/Scripts/Page scripts/general.js"></script>
    @Scripts.Render("~/bundles/bootstrap")
    @RenderSection("scripts", required:=False)
    @RenderSection("Styles", required:=False)
    


</head>
<body>
    @If HttpContext.Current.IsDebuggingEnabled Then
        MiniProfiler.Current.RenderIncludes()
    End If
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                @Html.ActionLink("LUS Fiber NOC Alarm Console", "Index", "Home", New With {.area = ""}, New With {.class = "navbar-brand"})
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav">
                    <li>@Html.ActionLink("Home", "Index", "Home")</li>
                    <li>@Html.ActionLink("Outage Map", "OutageMap", "Home")</li>
                    <li>@Html.ActionLink("Serviceability Map", "Serviceability", "Home")</li>
                    <li>@Html.ActionLink("Progression Map", "TimeMap", "Home")</li>
                    @If User.HasAllRoles("UTILITIES\Fiber OSS_BSS") Then
                        @<text>
                            <li class="dropdown">
                                <a class="dropdown-toggle" data-toggle="dropdown">
                                    Administration
                                    <span class="caret"></span>
                                </a>
                                <ul class="dropdown-menu">
                                    <li>@Html.ActionLink("Administration", "Admin", "Home")</li>
                                    <li>@Html.ActionLink("Bulk Ticket Admin", "BulkTickets", "Home")</li>
                                </ul>
                            </li>
                        </text>
                    End If
                </ul>
                <div Class="nav navbar-text navbar-right">@User.Identity.Name</div>
            </div>

        </div>
    </div>
    <div class="container body-content">
        @RenderBody()
        <hr />
        <footer>
            <p>&copy; @DateTime.Now.Year - LUSFiber</p>
        </footer>
    </div>


</body>
</html>
