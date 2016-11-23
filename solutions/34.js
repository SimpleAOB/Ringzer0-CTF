//My goal here was to preserve as much of the original code as possible

var k = new Array(176,214,205,246,264,255,227,237,242,244,265,270,283);
var u = "administrator"; //static
var p = ""; //static
var ar = []; //Temporary array to hold values
var code = 0; //Code iterator
var t = "true1"; //Follow where it failed (if it did)

if(u == "administrator") { //Unchanged
  for(i = 0; i < u.length; i++) { //Unchanged
  	ar[i] = String.fromCharCode(code); //Translates the code variable into a letter
    if((u.charCodeAt(i) + ar[i].charCodeAt(0) + i * 10) != k[i]) { //Replaced the second argument with the ar[] holder
      t = "false2:"+i+":"+((u.charCodeAt(i) + p.charCodeAt(i) + i * 10));  //Debug info
      code++; //Removed the break; command with code var incrementor
      i--; //Lazy solution to a problem
    } else {
    	p = p + ar[i]; //If it wouldn't "break", add it to the password string
    }
  }
} else {
  t = "false3";
}
if(t) {
  document.getElementById("resp").innerHTML = p; //Show final password: OhLord4309111
}
