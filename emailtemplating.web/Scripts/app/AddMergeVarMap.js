function AddChoice() {
    if (!ValidateFields())
        return;
    var table = $("#MergeTagMapsTable");
    var mapItemCount = parseInt(document.getElementById('mapItemsCount').value);
    var newRow = "<tr >" +
                 "<td style='width: 20%;'><label class='col-sm-2 control-label'>VariableName: </label></td>" +
                 "<td style='width: 25%;'>" +
                 "<input type='hidden' name='MapItems[" + mapItemCount + "].MergeVarMapItemID' value='-1' class='hdMergeMap'>" +
                 "<input class='mandatory variable form-control' type=\"text\" name=\"MapItems[" + mapItemCount + "].VariableName\" value=\"\" placeholder=\"Enter Variable Name\" data-val='true' /></td>" +
                 "<td style='width: 20%;'><label class='col-sm-2 control-label'>PropertyName:</label></td>" +
                 "<td style='width: 20%;'><input class='mandatory property form-control' type=\"text\" name=\"MapItems[" + mapItemCount + "].PropertyName\" value=\"\" placeholder=\"Enter Property Name\" /></td>" +
                 "<td style='width: 10%; padding-left:15px;'><a href='#' onclick='delMergeVarMapRow(this);' >Delete</a></td>" +
                 "</tr>" +
                 "<tr style='height:10px;'></tr>";
    table.append(newRow);
    document.getElementById('mapItemsCount').value = parseInt(document.getElementById('mapItemsCount').value) + 1;
}
function delMergeVarMapRow(control,i) {

    $(control).closest('tr').remove();
    //$('#MergeTagMapsTable').deleteRow(i);
    document.getElementById('mapItemsCount').value = parseInt(document.getElementById('mapItemsCount').value) - 1;
    setControlID();
}
function setControlID() {
    var textVarList = $('.variable'), textPropList = $('.property'),hdMergeMapList=$('.hdMergeMap');
    var counter = $('#mapItemsCount').val();
    for (var i = 0; i < textVarList.length; i++) {
        $(textVarList[i]).attr('name', "MapItems[" + i + "].VariableName");
        $(textVarList[i]).attr('name', "MapItems[" + i + "].VariableName");
        $(textPropList[i]).attr('name', "MapItems[" + i + "].PropertyName");
        $(hdMergeMapList[i]).attr('name', "MapItems[" + i + "].MergeVarMapItemID")
                if (counter < i) {
            debugger
            $(textVarList[i]).closest('tr').remove();
        }
    }

}