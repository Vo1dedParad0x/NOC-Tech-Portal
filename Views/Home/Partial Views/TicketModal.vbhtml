<script src="~/Scripts/Page scripts/ticket-modal.js"></script>
<link href="~/Content/Page styles/ticket-modal.css" rel="stylesheet" />

<div id="createTicketModal" class="modal" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            @Using Html.BeginForm("SubmitNewTicket", "Home", FormMethod.Post, New With {.id = "ticketForm"})
                @Imports Microsoft.Crm.Sdk
                @Imports Microsoft.Xrm.Sdk
                @Html.AntiForgeryToken()
                @<text>
                    <div Class="modal-header">
                        <Button type="button" Class="close" data-dismiss="modal">&times;</Button>
                        <h4 Class="modal-title">Create New Ticket</h4>
                    </div>
                    <div class="modal-body">
                        <div class="pane-scrollbar">
                            <progress value="1" />
                        </div>
                        <div class="pane-container">
                            <div class="form-group">
                                <label>Priority Code</label>
                                <select class="form-control" name="prioritycode">
                                    <option value="973060004">Same Day</option>
                                    <option value="973060002">AM</option>
                                    <option value="973060003">PM</option>
                                    <option value="973060000">Unscheduled</option>
                                </select>
                            </div>
                            <div class="form-group">
                                <label>Reported Trouble</label>
                                <select class="form-control" name="reportedtrouble">
                                    @Code

                                        Dim serviceUrl As New Uri("https://lft-p.lusfiber.com/XRMServices/2011/Organization.svc")
                                        Dim Credentials As New ServiceModel.Description.ClientCredentials

                                        Credentials.UserName.UserName = "LFTO360\APPCRONJOBS"
                                        Credentials.UserName.Password = "hgn?7L*qk6KcLYMD"

                                        Using service As New Client.OrganizationServiceProxy(serviceUrl, Nothing, Credentials, Nothing)
                                            Dim req = New Query.QueryExpression("chr_reportedtrouble") With {
                                                .ColumnSet = New Microsoft.Xrm.Sdk.Query.ColumnSet("chr_name", "chr_reportedtroubleid")
                                            }

                                            req.AddOrder("chr_name", Query.OrderType.Ascending)
                                            req.Criteria.AddCondition(New Query.ConditionExpression("statecode", Query.ConditionOperator.Equal, 0))
                                            For Each e In service.RetrieveMultiple(req).Entities
                                            @<option value='@e.Attributes("chr_reportedtroubleid")'>@e.Attributes("chr_name")</option>
                                            Next e
                                        End Using
                                    End Code
                                </select>
                            </div>
                            <div Class="form-group">
                                <Label>
                                    Queue
                                </Label>
                                <select class="form-control" name="queueType">
                                    <option>Low Hanging Fiber</option>
                                    <option>OTDR</option>
                                    <option>Drop</option>
                                    <option>Splice</option>
                                    <option>Main Line</option>
                                    <option>DNA</option>
                                    <option>Tree Trim</option>
                                    <option>Pending Electric</option>
                                    <option>Site Survey</option>
                                    <option>Wholesale</option>
                                    <option>Electric</option>
                                    <option>Escalations</option>
                                    <option>Mainline/Service Drop</option>
                                </select>
                            </div>
                            <div Class="form-group">
                                <Label>
                                    Customer Name
                                </Label>
                                <input id="cusName" name="cusName" class="form-control typeahead" autocomplete="off" required />
                            </div>
                            <div Class="form-group">
                                <Label>
                                    Customer Address
                                </Label>
                                <input id="cusAddress" name="cusAddress" class="form-control typeahead" autocomplete="off" required />
                            </div>
                            <div Class="form-group">
                                <Label>
                                    Contact Name
                                </Label>
                                <input id="contactName" name="contactName" class="form-control" required />
                            </div>
                            <div Class="form-group">
                                <Label>
                                    Contact Phone Number
                                </Label>
                                <input type="tel" id="contactPhone" name="contactPhone" class="form-control" required />
                            </div>
                            <div Class="checkbox">
                                <Label> <input type="checkbox" name="hasPower" />Has Power?</Label>
                            </div>
                            <div Class="form-group">
                                <Label>
                                    Service Drop Location
                                </Label>
                                <select name="dropLocation" class="form-control">
                                    <option value="Overhead"> Overhead</option>
                                    <option value="Underground"> Underground</option>
                                </select>
                            </div>
                            <div Class="form-group">
                                <Label>
                                    Is the drop from the pole to the home damaged?
                                </Label>
                                <select name="dropDamage" class="form-control">
                                    <option value="N/A"> N / A</option>
                                    <option value="Low"> Low</option>
                                    <option value="Low And Impedes Traffic"> Low and Impedes Traffic</option>
                                    <option value="Cut"> Cut</option>
                                </select>
                            </div>
                            <div Class="checkbox">
                                <Label> <input type="checkbox" name="additionalDowns" />Are there any other down lines in the area?</Label>
                            </div>
                            <div Class="form-group">
                                <Label>
                                    Is this a total or partial loss of service?
                                </Label>
                                <select name="serviceLoss" class="form-control">
                                    <option value="Partial">Partial</option>
                                    <option value="Total"> Total</option>
                                </select>
                            </div>
                            <div class="services">
                            </div>
                            <div class="form-group">
                                <label> APID</label><input type="text" name="AddressPointID" class="form-control" />
                            </div>
                            <div class="form-group">
                                <label> Success</label><input type="text" name="Success" class="form-control" />
                            </div>
                            <div class="form-group">
                                <label> NAP</label><input type="text" name="NAP" class="form-control" />
                            </div>
                            <div class="form-group">
                                <label> NAP Fiber</label><input type="text" name="NAPFiber" class="form-control" />
                            </div>
                            <div class="form-group">
                                <label> NAP Address</label><input type="text" name="NAPAddress" class="form-control" />
                            </div>
                            <div class="form-group">
                                <label> LCP Port</label><input type="text" name="LCPPort" class="form-control" />
                            </div>
                            <div class="form-group">
                                <label> Splitter Port</label><input type="text" name="SplitterPort" class="form-control" />
                            </div>
                            <div class="form-group">
                                <label> LCP Address</label><input type="text" name="LCPAddress" class="form-control" />
                            </div>
                            <div class="form-group">
                                <label> Distribution Fiber</label><input type="text" name="DistributionFiber" class="form-control" />
                            </div>
                            <div class="form-group">
                                <label> OLT Port</label><input type="text" name="OLTPort" class="form-control" />
                            </div>
                            <div class="form-group">
                                <label> Status</label><input type="text" name="Status" class="form-control" />
                            </div>
                            <div Class="form-group">
                                <label> Other Notes</label>
                                <textarea id="noteContent" name="noteContent" class="form-control"></textarea>
                            </div>
                            <input id="alarmID" name="alarmID" type="hidden" value="-1" />
                        </div>
                    </div>
                    <div Class="modal-footer">
                        <button type="button" Class="btn btn-default" data-paneAction="backward">Back</button>
                        <button type="button" Class="btn btn-default" data-paneAction="forward">Next</button>
                        <Button type="submit" Class="btn btn-default">Submit</Button>
                        <Button type="button" Class="btn btn-default" data-dismiss="modal">Cancel</Button>
                    </div>
                </text>
                                        End Using
        </div>
    </div>
</div>
