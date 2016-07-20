
$('#numBtn').click(function () {


    var a = parseInt($('input[name=num1]').val());
    var b = parseInt($('input[name=num2]').val());
    var c = parseInt($('input[name=num3]').val());
    var d = parseInt($('input[name=num4]').val());
    var e = parseInt($('input[name=num5]').val());

    
    var prod = a * b * c * d * e;
    $('#result2').append(prod + " is the product");

    var sum = a + b + c + d + e;
    $('#result3').append(sum + " is the sum");

    var mean = (sum / 5);
    $('#result4').append(mean + " is the mean");

    var max = Math.max(a, b, c, d, e);
    $('#result1').append(max + " is the greatest number")

    var min = Math.min(a, b, c, d, e);
    $('#result').append(min + "is the least number")
});
