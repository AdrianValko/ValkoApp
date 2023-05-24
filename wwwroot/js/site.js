var table = document.getElementById("myTable");
var rows = table.getElementsByTagName("tbody")[0].getElementsByTagName("tr");

for (var i = 0; i < rows.length; i++) {
    var durationCell = rows[i].getElementsByTagName("td")[1];
    var duration = durationCell.textContent.trim();

    if (workedLess(duration)) {
        rows[i].classList.add("highlight-row");
    }
}
function workedLess(duration) {
    var days = parseFloat(duration.split(":")[0]);
    return days < 4.04;
}