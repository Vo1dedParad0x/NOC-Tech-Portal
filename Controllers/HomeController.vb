Imports NOC_Tech_Portal.ObjectExtensions
Imports Microsoft.Crm.Sdk
Imports Microsoft.Xrm.Sdk
Imports System.Web.Mvc
Imports System.Data.Entity
Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports System.Web.Script.Serialization
Imports System.Text.Json
Imports System.Threading.Tasks
Imports System.Data.SqlClient

Public Class HomeController
    Inherits System.Web.Mvc.Controller

    <HttpGet>
    Function Index() As ActionResult
        Using db = New AMSAlarms()
            Dim res = db.AMSAlarms_vw.Where(Function(e) e.AccountStatusCode <> "I" Or e.EnitityGUID IsNot Nothing).ToList().OrderByDescending(Function(e) e.AlarmDateTime)
            Return View(res)
        End Using

    End Function

    Function Admin() As ActionResult
        Return View()
    End Function

    <HttpGet>
    Function Serviceability() As ActionResult
        Return View()
    End Function

    <HttpGet>
    Function AlarmInfo() As ActionResult
        Return View()
    End Function

    <HttpGet>
    Function TimeMap() As ActionResult
        Return View()
    End Function

    <HttpGet>
    Function CSRServiceability() As ActionResult
        Return View("Serviceability", "~/Views/Shared/_LayoutNoLink.vbhtml")
    End Function

    <HttpGet>
    Function Ops() As ActionResult
        'Return View(alarmEntities.AMSAlarms)
        Using db = New AMSAlarms()
            Return View(db.AMSAlarms_vw)
        End Using
    End Function

    <HttpGet>
    Function OutageMap() As ActionResult
        Return View()
    End Function

    <HttpGet>
    Function BulkTickets() As ActionResult
        Return View()
    End Function

    <HttpGet>
    Async Function GetPlantInfo(locid As String) As Task(Of JsonResult)
        Dim url = $"http://gis.lusfiber.net:6080/arcgis/rest/services/Networks1_Auto_Provisioning_Production/MapServer/exts/ProvisioningSOE/GetPlant?AddressPointID=" + GetAPID(locid) + "&f=pjson"
        Using client As New HttpClient()
            Using res As HttpResponseMessage = Await client.GetAsync(url)
                res.EnsureSuccessStatusCode()
                Using content As HttpContent = res.Content
                    Return Json(JsonSerializer.Deserialize(Of PlantInfo)(Await content.ReadAsStringAsync()), JsonRequestBehavior.AllowGet)
                End Using
            End Using
        End Using
    End Function

    <NonAction>
    Function GetAPID(locationid As String) As String
        'Dim apid
        Using db As New ServiceLocations()
            Return db.Omnia360_LUS_ServiceLocation.Where(Function(e) e.CrmLocationId = New Guid("{" + locationid + "}")).FirstOrDefault().LegacyKeyId
        End Using

    End Function

    Function SubmitNewTicket(args As FormCollection) As ActionResult

        Dim serviceUrl As New Uri("https://lft-p.lusfiber.com/XRMServices/2011/Organization.svc")
        Dim Credentials As New ServiceModel.Description.ClientCredentials

        Credentials.UserName.UserName = "LFTO360\APPCRONJOBS"
        Credentials.UserName.Password = "hgn?7L*qk6KcLYMD"

        Using service As New Client.OrganizationServiceProxy(serviceUrl, Nothing, Credentials, Nothing)
            Dim req As New Entity("incident")
            If HttpContext.IsDebuggingEnabled Then
                req.Attributes.Add("title", "DEBUGGING TICKET DO NOT DISPATCH: " + args("cusAddress"))
            Else
                req.Attributes.Add("title", args("cusAddress"))
            End If
            req.Attributes.Add("prioritycode", New OptionSetValue(args("prioritycode")))
            req.Attributes.Add("chr_reportedtroubleid", New EntityReference("chr_reportedtrouble", New Guid("{" + args("reportedtrouble") + "}")))
            req.Attributes.Add("chr_troubletypeid", New EntityReference("chr_troubletype", New Guid("{38A5EC87-E2A4-E911-80F2-00155D03AB48}")))
            req.Attributes.Add("chr_servicelocation", New EntityReference("chr_servicelocation", New Guid("{" + args("cusAddressGuid") + "}")))
            req.Attributes.Add("customerid", New EntityReference("account", New Guid("{" + args("cusNameGUID") + "}")))
            Dim reqQuery As New Query.QueryByAttribute("systemuser")
            With reqQuery
                .AddAttributeValue("domainname", User.Identity.Name)
                .TopCount = 1
            End With
            Dim systemUser = service.RetrieveMultiple(reqQuery)(0).GetAttributeValue(Of Guid)("systemuserid")
            req.Attributes.Add("ownerid", New EntityReference("systemuser", systemUser))

            Dim res = service.Create(req)

            If args("alarmID") <> "-1" Then
                Using db = New AlarmTblDBContext()
                    Dim rec = db.AMSAlarms.FirstOrDefault(Function(e) e.AlarmID = args("alarmID"))
                    If Not IsNothing(rec) Then
                        rec.EnitityGUID = res.ToString()
                        db.SaveChangesAsync()
                    End If
                End Using
            End If

            Dim extraInfoTemplate = $"
                

            "
            Dim extraInfoNote = New Entity("annotation")
            extraInfoNote.Attributes.Add("objectid", New EntityReference("incident", res))
            extraInfoNote.Attributes.Add("objecttypecode", "incident")
            extraInfoNote.Attributes.Add("subject", "Extra Info")
            extraInfoNote.Attributes.Add("notetext", extraInfoTemplate)
            service.Create(extraInfoNote)

            Dim servicesTemplate

            Dim servicesNote = New Entity("annotation")
            servicesNote.Attributes.Add("objectid", New EntityReference("incident", res))
            servicesNote.Attributes.Add("objecttypecode", "incident")
            servicesNote.Attributes.Add("subject", "Services Info")
            servicesNote.Attributes.Add("notetext", servicesTemplate)
            service.Create(servicesNote)

            Dim plantTemplate =
                $"
            APID: {If(args?("AddressPointID"), "")}
            Success: {If(args?("Success"), "")}
            NAP: {If(args?("NAP"), "")}
            NAP Fiber: {If(args?("NAPFiber"), "")}
            NAP Address: {If(args?("NAPAddress"), "")}
            LCP Port: {If(args?("LCPPort"), "")}
            Splitter Port: {If(args?("SplitterPort"), "")}
            LCP Address: {If(args?("LCPAddress"), "")}
            Distribution Fiber: {If(args?("DistributionFiber"), "")}
            OLT Port: {If(args?("OLTPort"), "")}
            Status: {If(args?("Status"), "")}
            "
            Dim plantNote = New Entity("annotation")
            plantNote.Attributes.Add("objectid", New EntityReference("incident", res))
            plantNote.Attributes.Add("objecttypecode", "incident")
            plantNote.Attributes.Add("subject", "Plant Info")
            plantNote.Attributes.Add("notetext", plantTemplate)
            service.Create(plantNote)

            If args("noteContent") <> "" Then
                Dim note As New Entity("annotation")
                note.Attributes.Add("objectid", New EntityReference("incident", res))
                note.Attributes.Add("objecttypecode", "incident")
                note.Attributes.Add("subject", "Creation Notes")
                note.Attributes.Add("notetext", args("noteContent"))
                service.Create(note)
            End If
            Dim queueReq As New Messages.AddToQueueRequest With {.Target = New EntityReference("incident", res), .DestinationQueueId = New Guid("{EBE8BE30-CAA4-E911-80F2-00155D03AB48}")}

            service.Execute(queueReq)
        End Using
        Return New EmptyResult()
    End Function

    <HttpGet>
    <OutputCache(Duration:=600, VaryByParam:="none")>
    Function GetAddresses(query As String) As JsonResult
        Dim serviceUrl As New Uri("https://lft-p.lusfiber.com/XRMServices/2011/Organization.svc")
        Dim Credentials As New ServiceModel.Description.ClientCredentials

        Credentials.UserName.UserName = "LFTO360\APPCRONJOBS"
        Credentials.UserName.Password = "hgn?7L*qk6KcLYMD"

        Using service As New Client.OrganizationServiceProxy(serviceUrl, Nothing, Credentials, Nothing)
            Dim reqQuery As New Query.QueryExpression()
            With reqQuery
                .EntityName = "chr_servicelocation_account"
                .ColumnSet = New Query.ColumnSet(True)
                .Criteria.AddCondition("accountid", Microsoft.Xrm.Sdk.Query.ConditionOperator.Equal, New Guid("{" + query + "}"))
                .Distinct = True
            End With
            reqQuery.LinkEntities.Add(New Query.LinkEntity("chr_servicelocation_account", "chr_servicelocation", "chr_servicelocationid", "chr_servicelocationid", Microsoft.Xrm.Sdk.Query.JoinOperator.Inner) With {.EntityAlias = "sl", .Columns = New Query.ColumnSet(True)})
            Dim res = service.RetrieveMultiple(reqQuery).Entities
            Dim results = From rec In res
                          Select New With {
                              .name = rec("sl.chr_name").Value,
                              .guid = rec("chr_servicelocationid")
                            }
            Return Json(results, JsonRequestBehavior.AllowGet)
        End Using
    End Function

    <HttpGet>
    <OutputCache(Duration:=600, VaryByParam:="none")>
    Function GetCustomers() As ContentResult
        Dim serviceUrl As New Uri("https://lft-p.lusfiber.com/XRMServices/2011/Organization.svc")
        Dim Credentials As New ServiceModel.Description.ClientCredentials

        Credentials.UserName.UserName = "LFTO360\APPCRONJOBS"
        Credentials.UserName.Password = "hgn?7L*qk6KcLYMD"

        Using service As New Client.OrganizationServiceProxy(serviceUrl, Nothing, Credentials, Nothing)
            Dim req = New Query.QueryExpression("account") With {
                .ColumnSet = New Query.ColumnSet("name", "accountid")
        }
            req.Criteria.AddCondition("chr_accountstatusid", Query.ConditionOperator.NotEqual, New Guid("{5F5FF6AD-3D7C-E211-8B29-00155DC86B6F}"))
            Dim res As New List(Of Entity)
            Dim resp As EntityCollection
            Do
                resp = service.RetrieveMultiple(req)
                res.AddRange(resp.Entities)
                req.PageInfo.PageNumber += 1
                req.PageInfo.PagingCookie = resp.PagingCookie
            Loop While resp.MoreRecords
            Dim results = From rec In res
                          Order By rec("name")
                          Select New With {
                                .name = rec("name"),
                                .guid = rec("accountid")
                            }
            Return SerializeLargeJSON(results)
        End Using
    End Function

    <NonAction>
    Private Function SerializeLargeJSON(obj As Object) As ContentResult
        Dim serializer As New JavaScriptSerializer With {
            .MaxJsonLength = Int32.MaxValue
        }
        Return New ContentResult() With {
            .Content = serializer.Serialize(obj),
            .ContentType = "application/json"
            }
    End Function

    <HttpGet>
    Public Function GetServices(cusid As String, locid As String) As PartialViewResult
        Using db As New ServiceLocations()
            Dim out As List(Of Service) = db.Database.SqlQuery(Of Service)("Exec LUS_GetServices @locationID,@accountID", New SqlParameter("@accountID", cusid), New SqlParameter("@locationID", locid)).ToList()
            Return PartialView("Partial Views/Services", out)
        End Using
    End Function


End Class
