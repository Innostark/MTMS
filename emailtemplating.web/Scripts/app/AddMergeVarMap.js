function AddChoice() {
    if (!ValidateFields())
        return;
    var table = $("#MergeTagMapsTable");
    var mapItemCount = parseInt(document.getElementById('mapItemsCount').value);
    var newRow = "<tr class='form-group'>" +
                 "<td><label>Enter Variable Name: </label><input class='mandatory variable' type=\"text\" name=\"MapItems[" + mapItemCount + "].VariableName\" value=\"\" placeholder=\"Enter Variable Name\" data-val='true' /></td>" +
                 "<td><label>Enter Property Name:</label><input class='mandatory property' type=\"text\" name=\"MapItems[" + mapItemCount + "].PropertyName\" value=\"\" placeholder=\"Enter Property Name\" /></td>" +
                 "<td><a href='#' onclick='delMergeVarMapRow(this);' >Delete</a></td>" +
                "</tr>";
    table.append(newRow);
    document.getElementById('mapItemsCount').value = parseInt(document.getElementById('mapItemsCount').value) + 1;
}
function delMergeVarMapRow(control) {

    $(control).closest('tr').remove();
    document.getElementById('mapItemsCount').value = parseInt(document.getElementById('mapItemsCount').value) - 1;
    setControlID();
}
function setControlID() {
    var textVarList = $('.variable'), textPropList = $('.property');
    for (var i = 0; i < textVarList.length; i++) {
        $(textVarList[i]).attr('name', "MapItems[" + i + "].VariableName");
        $(textPropList[i]).attr('name', "MapItems[" + i + "].PropertyName");
    }

}