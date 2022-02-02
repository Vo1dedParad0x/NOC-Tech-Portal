
@Using Html.BeginForm("SubmitNewTicket", "Home", FormMethod.Post, New With {.id = "ticketForm"})
    @Html.AntiForgeryToken()
    @<div Class="form-group">
        <Label>Ticket Name</Label>
        @*<input type="text" name="ticketName" class="form-control" required/>*@
        <select class="form-control" name="ticketName">
            <option value="DNA/Dying Gasp">DNA/Dying Gasp</option>
            <option value="Cut Fiber">Cut Fiber</option>
        </select>
    </div>
    @<div Class="form-group">
        <Label>
            Customer Name
        </Label>
        <input id="cusNameLabel" name="cusGUIDLabel" class="form-control typeahead" autocomplete="off" required />
        <input id="cusName" name="cusGUID" type="hidden" autocomplete="off" />
    </div>
    @<div Class="form-group">
        <Label>
            Customer Address
        </Label>
        <input id="cusAddressLabel" name="addrGUIDLabel" class="form-control typeahead" autocomplete="off" required />
        <input id="cusAddress" name="addrGUID" type="hidden" autocomplete="off" />
    </div>
    
    @<div Class="form-group">
        <label>Notes</label>
        <textarea id="noteContent" name="noteContent" class="form-control"></textarea>
    </div>
    @<input id="alarmID" name="alarmID" type="hidden" value="-1" />
    @<input type="Submit" class="form-control" id="submit" value="Submit Ticket" />
End Using
