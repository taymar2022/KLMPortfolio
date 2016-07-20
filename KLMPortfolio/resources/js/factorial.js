
$('#factBtn').click(function(){

   
    var num = parseInt($('input[name=fact1]').val());
var prod = 1;
for (var i=1; i<=num;i++)
{ 
prod = prod * i;
}
$('#factResult').append(prod + " is the factorial");

});

    
   
 
 
