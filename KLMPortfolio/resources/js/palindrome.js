$('#palBtn').click(function () {
    var word = $("#palInput").val();
    var reverse = word.split("").reverse().join("");

    
    if (word == "") {
        alert("please enter a  word");
    } else  {
        if (word == reverse)  {
            $("#palResultOutput").html("Yes");
        } else  {
            $("#palResultOutput").html("No");
        }
    }
});