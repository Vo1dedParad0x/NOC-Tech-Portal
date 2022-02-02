Imports System.Data
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.Web.Http
Imports System.Web.Http.Description
Imports NOC_Tech_Portal.ObjectExtensions

Namespace Controllers
    Public Class AMSAlarmsController
        Inherits ApiController


        ' GET: api/AMSAlarms
        <HttpGet>
        <OutputCache(Duration:=600, VaryByParam:="none")>
        Function AMSAlarms() As IHttpActionResult
            Dim res
            Using db = New AMSAlarms()
                res = db.AMSAlarms_vw.ToList().OrderByDescending(Function(e) e.AlarmDateTime)
            End Using
            Return Json(res)

        End Function

        <HttpGet>
        <OutputCache(Duration:=600, VaryByParam:="none")>
        Function AMSAlarmInfo() As IHttpActionResult
            Dim res
            Using db = New AMSAlarms()
                res = db.AMSAlarmsOLTJoin_vw.ToList().OrderByDescending(Function(e) e.AlarmDateTime)
            End Using
            Return Json(Res)

        End Function

        <HttpGet>
        Function AMSAlarmTickets() As IHttpActionResult
            Dim serviceUrl As New Uri("https://lft-p.lusfiber.com/XRMServices/2011/Organization.svc")
            Dim Credentials As New ServiceModel.Description.ClientCredentials

            Credentials.UserName.UserName = "LFTO360\APPCRONJOBS"
            Credentials.UserName.Password = "hgn?7L*qk6KcLYMD"

            Dim service = New Microsoft.Xrm.Sdk.Client.OrganizationServiceProxy(serviceUrl, Nothing, Credentials, Nothing)
            Dim reqQuery As New Microsoft.Xrm.Sdk.Query.QueryByAttribute("incident")
            Dim incidents = service.RetrieveMultiple(reqQuery)
            Return Json(incidents)
        End Function

    End Class
End Namespace