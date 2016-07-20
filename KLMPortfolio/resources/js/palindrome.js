$('#calcBtn').click(function () {
    var word = $("#palInput").val();
    var reverse = word.split("").reverse().join("");
    if (word == reverse)
        $("#palResult").html("Yes");
    else
        $("#palResult").html("No");
});