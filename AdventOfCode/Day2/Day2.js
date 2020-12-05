var list = document.getElementsByTagName("pre")[0].innerText.trim().split("\n")

//9-11 b: bgbbbsbbbnbbbvbbbb
function getPassword() { 
    var validPasswordCount = 0;
    for(var i = 0; i < list.length; i++)
    {
		var temp = list[i].split(':');
		var pw = temp[1].trim();
		
		temp = temp[0].split(' ');
		var letter = temp[1].trim();
		
		temp = temp[0].split('-');
		var min = Number(temp[0]);
        var max = Number(temp[1]);      
		
		var pwsplit = pw.split(letter);
		var pwsplitlength = pwsplit.length - 1;
		
		console.log("min: " + min + ", max: " + max + ",letter: " + letter + ",pw: " + pw + ", splitlength: " + pwsplit.length);
		
		if(pwsplitlength >= min && pwsplitlength <= max) {
			console.log(list[i] + " is valid");
			validPasswordCount++;
		} else {
			console.log(list[i] + " is not valid");
		}

    }
	
	return validPasswordCount;
}

function getPassword2() { 
    var validPasswordCount = 0;
    for(var i = 0; i < list.length; i++)
    {
		var temp = list[i].split(':');
		var pw = temp[1].trim();
		
		temp = temp[0].split(' ');
		var letter = temp[1].trim();
		
		temp = temp[0].split('-');
		var min = Number(temp[0]) - 1;
        var max = Number(temp[1]) - 1;     

		console.log("min: " + min + ", max: " + max + ",letter: " + letter + ",pw: " + pw);
						
		if((pw.charAt(min) == letter && pw.charAt(max) != letter) || 
			(pw.charAt(min) != letter && pw.charAt(max) == letter)) {
			console.log(list[i] + " is valid");
			validPasswordCount++;
		} else {
			console.log(list[i] + " is not valid");
		}
		
		if(i == 10) break;
    }
	
	return validPasswordCount;
}