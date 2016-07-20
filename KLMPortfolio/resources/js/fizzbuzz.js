$('#fbBtn').click(function(){

var a = $('input[name=fb1]').val();
var b = $('input[name=fb2]').val();

var fbResult1='';
for (var i = 1; i <= 100; i++) {


    if ((i % b) === 0 && (i % a) === 0) {
        fbResult1 += "FizzBuzz ";
    }
    else if ((i % a) === 0) {
       fbResult1 += "Fizz " ;
    }
    else if ((i % b) === 0) {
        fbResult1 += "Buzz ";
    }
    else {
        fbResult1 += i + ' ';
    }
}
    $('#fbResult1').append(fbResult1)
})


 
