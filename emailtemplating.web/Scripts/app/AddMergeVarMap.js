function AddChoice() {
    if (!ValidateFields())
        return;
    var table = $("#MergeTagMapsTable");
    debugger;
    var mapItemCount = parseInt(document.getElementById('mapItemsCount').value);
    var newRow = "<tr class='form-group'>" +
                 "<td><label >Enter Variable Name: </label><input class='mandatory' type=\"text\" name=\"MapItems[" + mapItemCount + "].VariableName\" value=\"\" placeholder=\"Enter Variable Name\" data-val='true' /></td>" +
                 "<td><label  >Enter Property Name:</label><input class='mandatory' type=\"text\" name=\"MapItems[" + mapItemCount + "].PropertyName\" value=\"\" placeholder=\"Enter Property Name\" /></td>" +
                "</tr>";
    table.append(newRow);
    document.getElementById('mapItemsCount').value = parseInt(document.getElementById('mapItemsCount').value) + 1;
}
