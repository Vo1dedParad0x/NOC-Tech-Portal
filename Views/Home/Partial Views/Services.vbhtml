@ModelType IEnumerable(Of Service)

@For Each group In Model.GroupBy(Function(item) item.GroupingID)
    @<button type="button" data-toggle="collapse" data-target="#@group.First(Function(e) e.ItemType = "Parent").GroupingID">@group.First(Function(e) e.ItemType = "Parent").Component</button>
    @<div id="@group.First(Function(e) e.ItemType = "Parent").GroupingID" class="collapse list-group">
        @For Each item In group.Where(Function(e) e.ItemType = "Child")
            @<span class="list-group-item">@item.Component</span>
        Next item
    </div>
Next group
