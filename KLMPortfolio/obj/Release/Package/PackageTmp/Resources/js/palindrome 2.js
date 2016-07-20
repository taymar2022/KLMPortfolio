
<!DOCTYPE html>
<html lang="en">
<head>
 <title>Bootstrap Example</title>
 <meta charset="utf-8">
 <meta name="viewport" content="width=device-width, initial-scale=1">
 <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">
 <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
 <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
 <script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js?lang=css&amp;skin=sunburst"></script>
    <style type="text/css">
          .myborder {
              border: solid black 1px;
              text-align: center;
        }
    </style>    
    <script type="text/javascript">
        function IsPalindrome(word) {
            $("#output1").val(word.split("").reverse().join(""));
            if ($("#output1").val() == word)
                $("#output2").val("Yes");
            else
                $("#output2").val("No");            
        }
    </script>
</head>
<body>
    <div class="container">
        <div class="row">
            <div class="col-md-6 myborder">
                <p><b>Palindrome exercise</b></p>                
                <p>...</p>
            </div>            
            <div class="col-md-6 myborder">
                <div class="row">
                    <label>Enter Word: </label>
                    <input type="text" id="input">
                    <button onclick="IsPalindrome($('#input').val())">Click Me</button>        
                <div>
                    <br>
                    <label>The reverse value is: </label>
                    <input type="text" id="output1" readonly="true">
                    <label>Is a palindrome? : </label>
                    <input type="text" id="output2" readonly="true">
                </div>
            </div>            
        </div>        
    </div>
</body>
</html>