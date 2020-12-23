function exportRecords() {

    if (confirm("Do you want to Export Records ?")) {
        locationRef('~/Employee/Export');
    }
}
function locationRef(url) {

    window.location.href = url;
}




function onload1() {
   
    var url = '~/Employee/EmployeeList'
    var searchCol = [
        { type: "text" },
        { type: "text" },
        { type: "text" },
        { type: "text" },
        { type: "text" },
        { type: "text" },
        { type: "text" },

    ];

    abc(url, searchCol);
}
function abc(lurl) {

    $('#example').dataTable().fnDestroy();


    $('#example').dataTable({

        "aaSorting": [[0, "asc"]],
        "sDom": 'T<"clear ">lfrtip',
        "sPaginationType": "full_numbers",

        "oTableTools": {

            "aButtons": [

                {
                    "sExtends": "collection",
                    "sButtonText": 'Save <span/>',
                    "aButtons": [
                        {
                            "sExtends": "pdf",
                            "mColumns": "visible",
                            "oSelectorOpts": { "filter": "applied", "order": "current" },
                            "sPdfOrientation": "landscape",
                            "sTitle": "Student Report"
                        },
                        {
                            "sExtends": "xls",
                            "mColumns": "visible",
                            "oSelectorOpts": { "filter": "applied", "order": "current" },
                        },
                        "print"
                    ]
                }
            ]
        },
        "processing": true,
        "ajax": {
            "url": lurl,
            "dataSrc": "rows",
            "type": "GET"

        }
    });

}



function enableSearchButton(data) {
    if ('select' != data.value)
        document.getElementById("search").disabled = false;
}
