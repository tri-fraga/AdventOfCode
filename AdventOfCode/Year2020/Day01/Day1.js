var array = document.getElementsByTagName("pre")[0].innerText.trim().split("\n")

function get2020() { 
    for(var i = 0; i < array.length; i++)
    {
        for(var j = i; j < array.length; j++)
        {
            for(var k = j; k < array.length; k++)
            {
                if(Number(array[i]) + Number(array[j]) + Number(array[k]) == 2020){
                    return Number(array[i]) * Number(array[j] * Number(array[k]));
                }
            }
        }
    }
}

get2020()